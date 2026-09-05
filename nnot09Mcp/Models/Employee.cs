namespace nnot09Mcp.Models
{
    public record Employee(string FirstName, string LastName, DateOnly BirthDay)
    {
        public int Age => DateTime.Now.Year - BirthDay.Year;
    }
}
