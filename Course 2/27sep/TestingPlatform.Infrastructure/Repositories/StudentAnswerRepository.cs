using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Enum;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentAnswerRepository(AppDbContext appDbContext, IMapper mapper) : IStudentAnswerRepository
    {
        public async Task CreateAsync(UserAttemptAnswerDto userAttemptAnswerDto)
        {
            // Загружаем попытку, к которой добавляется ответ студента.
            // Нужно включить ("Include") коллекцию UserAttemptAnswers,
            // чтобы EF Core подгрузил список ответов к этой попытке.
            var attempt = await appDbContext.Attempts
                .Include(a => a.UserAttemptAnswers)
                .FirstOrDefaultAsync(a => a.Id == userAttemptAnswerDto.AttemptId);

            if (attempt == null)
                throw new EntityNotFoundException("Попытка не найдена.");

            // Нельзя добавлять ответы в попытку, которая уже завершена
            if (attempt.SubmittedAt != null)
                throw new InvalidOperationException("Нельзя добавлять ответ в уже сданную попытку.");

            // Загружаем вопрос, на который отвечает студент.
            // Подгружаем список вариантов ответов,
            // чтобы ниже проверить правильность выбора.
            var question = await appDbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == userAttemptAnswerDto.QuestionId);

            if (question == null)
                throw new EntityNotFoundException("Вопрос не найден.");

            // Создаём новую сущность UserAttemptAnswer.
            // Это объект, который будет связан с Attempt и Question.
            var userAttemptAnswer = new UserAttemptAnswer
            {
                AttemptId = attempt.Id,
                QuestionId = question.Id,
                UserSelectedOptions = new List<UserSelectedOption>(),
                UserTextAnswer = null,
                IsCorrect = false,
                ScoreAwarded = 0
            };

            // Логика расчёта по типу ответа
            switch (question.AnswerType)
            {
                // Ответ с одним правильным вариантом
                case AnswerType.SingleChoice:
                    {
                        // ожидаем ровно один выбранный id
                        var selected = userAttemptAnswerDto.UserSelectedOptions?.FirstOrDefault();

                        // Если студент не выбрал вариант -- ошибка
                        if (selected == 0 || selected is null)
                            throw new InvalidOperationException("Ожидается выбранный вариант ответа.");

                        // проверим, что выбранный вариант существует среди Answers
                        var selectedAnswerEntity = question.Answers.FirstOrDefault(a => a.Id == selected);
                        if (selectedAnswerEntity == null)
                            throw new EntityNotFoundException("Выбранный вариант ответа не найден.");

                        userAttemptAnswer.IsCorrect = selectedAnswerEntity.IsCorrect;

                        // Начисляем балл, если вопрос оценивается
                        if (question.IsScoring)
                        {
                            var max = question.MaxScore ?? 1;
                            userAttemptAnswer.ScoreAwarded = selectedAnswerEntity.IsCorrect ? max : 0;
                        }
                        else
                        {
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        // сохраняем ответ
                        userAttemptAnswer.UserSelectedOptions.Add(new UserSelectedOption
                        {
                            AnswerId = (int)selected
                        });

                        break;
                    }

                // Ответ с несколькими правильными вариантами
                case AnswerType.MultipleChoice:
                    {
                        var selectedIds = userAttemptAnswerDto.UserSelectedOptions ?? new List<int>();
                        if (selectedIds.Count == 0)
                            throw new InvalidOperationException("Ожидается как минимум один выбранный вариант для множественного выбора.");

                        // набор правильных вариантов
                        var correctAnswerIds = question.Answers
                            .Where(a => a.IsCorrect)
                            .Select(a => a.Id)
                            .ToHashSet();

                        // проверим, что все выбранные ответы существуют среди вариантов
                        var allAnswerIds = question.Answers.Select(a => a.Id).ToHashSet();
                        if (selectedIds.Any(id => !allAnswerIds.Contains(id)))
                            throw new EntityNotFoundException("Один или несколько выбранных вариантов не существуют в вопросе.");

                        // Exact Match -- полностью совпадают ли выбранные варианты с правильными
                        var selectedSet = selectedIds.ToHashSet();
                        var isExactMatch = selectedSet.SetEquals(correctAnswerIds);

                        userAttemptAnswer.IsCorrect = isExactMatch;

                        if (question.IsScoring)
                        {
                            // если задано максимальное количество баллов, то записываем его, иначе 1 балл
                            var max = question.MaxScore ?? 1;

                            // Начисляем баллы только в случае полного совпадения
                            userAttemptAnswer.ScoreAwarded = isExactMatch ? max : 0;
                        }
                        else
                        {
                            // если ответ не оценивается, записываем 0 баллов
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        // создадим UserSelectedOption для каждого выбранного варианта (фиксируем выбранные варианты)
                        foreach (var aid in selectedIds)
                        {
                            userAttemptAnswer.UserSelectedOptions.Add(new UserSelectedOption
                            {
                                AnswerId = aid
                            });
                        }

                        break;
                    }

                // Текстовый ответ
                case AnswerType.Text:
                    {
                        var text = userAttemptAnswerDto.UserTextAnswers?.Trim();

                        // Сохраняем текст ответа
                        userAttemptAnswer.UserTextAnswer = new UserTextAnswer
                        {
                            TextAnswer = text
                        };

                        // Если студент что-то написал -- засчитываем ответ как правильный
                        if (!string.IsNullOrEmpty(text))
                        {
                            // если ученик хоть что-то написал
                            userAttemptAnswer.IsCorrect = true;
                            userAttemptAnswer.ScoreAwarded = question.MaxScore ?? 0;
                        }
                        else
                        {
                            // если оставил пустым
                            userAttemptAnswer.IsCorrect = false;
                            userAttemptAnswer.ScoreAwarded = 0;
                        }

                        break;
                    }

                default:
                    throw new NotSupportedException($"Тип ответа {question.AnswerType} не поддерживается.");
            }

            await appDbContext.AddAsync(userAttemptAnswer);
            await appDbContext.SaveChangesAsync();
        }
    }
}
