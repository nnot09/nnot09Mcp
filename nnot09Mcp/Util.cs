using nnot09Mcp.Models;

namespace nnot09Mcp
{
    public static class Util
    {
        public static EmployeeEntity ToEntity(this Employee employee) => new EmployeeEntity()
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDay = employee.BirthDay
        };

        public static Employee ToDto(this EmployeeEntity entity) => new Employee(entity.FirstName, entity.LastName, entity.BirthDay);
    }
}
