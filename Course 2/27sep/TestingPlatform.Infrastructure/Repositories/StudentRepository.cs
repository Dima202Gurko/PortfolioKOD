using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(
                  List<int>? groupIds = null,
                  string? searchName = null,
                  string? sortBy = null,
                  bool sortDescending = true,
                  int pageNumber = 1,
                  int pageSize = 10
                )
        {
            // Базовый запрос
            var students = _context.Students
                .Include(s => s.User)
                .Include(s => s.Groups)
                .AsNoTracking()
                .AsQueryable();

            // Фильтр по группам
            if (groupIds != null && groupIds.Any())
                students = students.Where(s => s.Groups.Any(g => groupIds.Contains(g.Id)));

            // Поиск по ФИО
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                searchName = searchName.Trim().ToLower();
                students = students.Where(s =>
                    (s.User.LastName.ToLower().Contains(searchName)) ||
                    (s.User.FirstName.ToLower().Contains(searchName)) ||
                    (s.User.MiddleName != null && s.User.MiddleName.ToLower().Contains(searchName))
                );
            }

            // Сортировка по выбранному полю
            students = sortBy?.ToLower() switch
            {
                "lastname" => sortDescending ? students.OrderByDescending(s => s.User.LastName) : students.OrderBy(s => s.User.LastName),
                "firstname" => sortDescending ? students.OrderByDescending(s => s.User.FirstName) : students.OrderBy(s => s.User.FirstName),
                "id" => sortDescending ? students.OrderByDescending(s => s.Id) : students.OrderBy(s => s.Id),
                _ => students.OrderBy(s => s.Id) // сортировка по умолчанию
            };

            // Пропускаем элементы до нужной страницы и берём pageSize элементов
            students = students
                .Skip((pageNumber - 1) * pageSize) // сколько элементов пропустить
                .Take(pageSize); // сколько элементов взять

            // Выполняем запрос к базе данных
            var result = await students.ToListAsync();

            return _mapper.Map<IEnumerable<StudentDto>>(result);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Tests)
                .AsNoTracking()
                .FirstOrDefaultAsync(student => student.Id == id);

            if (student == null)
            {
                throw new EntityNotFoundException("Студент", id);
            }

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<int> CreateAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

            var studentId = await _context.AddAsync(student);
            await _context.SaveChangesAsync();

            return studentId.Entity.Id;
        }

        public async Task UpdateAsync(StudentDto studentDto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(student => student.Id == studentDto.Id);

            if (student == null)
            {
                throw new EntityNotFoundException("Студент", studentDto.Id);
            }

            student.Phone = studentDto.Phone;
            student.VkProfileLink = studentDto.VkProfileLink;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                throw new EntityNotFoundException("Студент", id);
            }

            _context.Users.Remove(student.User);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentDto?> GetStudentByUserId(int userId)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Tests)
                .AsNoTracking()
                .FirstOrDefaultAsync(student => student.UserId == userId);

            if (student == null)
            {
                return null;

            }

            return _mapper.Map<StudentDto>(student);
        }
        public async Task UpdateAvatarAsync(StudentDto studentDto)
        {
            var student = await _context.Students.FirstOrDefaultAsync(student => student.Id == studentDto.Id);

            if (student == null)
            {
                throw new EntityNotFoundException("Студент не найден.");
            }

            student.AvatarPath = studentDto.AvatarPath;
            await _context.SaveChangesAsync();
        }
    }
}
