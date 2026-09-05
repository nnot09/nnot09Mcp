namespace nnot09Mcp.Models
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly BirthDay { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
