namespace _27sep.Requests.Test
{
    public class TestFilterRequest
    {
        public bool? IsPublic { get; set; }
        public List<int> GroupIds { get; set; } = new();
        public List<int> StudentIds { get; set; } = new();
    }
}
