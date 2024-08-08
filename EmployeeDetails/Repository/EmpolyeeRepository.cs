
using Dapper;
using EmployeeDetails.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetails.Repository
{
    public class EmpolyeeRepository : IEmpolyeeRepository
    {
        private readonly IDbConnection _db;

        public EmpolyeeRepository(IConfiguration configuration) {
            _db = new SqlConnection(configuration.GetConnectionString("EmployeeContext")); 
        } 
        public void AddEmpolyee(EmployeeModel employee)
        {
            var employeeSql = "INSERT INTO employeeDetail (FirstName, LastName, Email, Address, City, Designation) VALUES (@FirstName, @LastName, @Email, @Address, @City, @Designation)";
            var employeeId = _db.Query<int>(employeeSql, new { 
                @FirstName = employee.FirstName,
                @LastName = employee.LastName,
                @Email = employee.email,
                @Address = employee.address,
                @City = employee.city,
                @Designation = employee.designation,
            }).FirstOrDefault();

            var projectSql = "INSERT INTO projectDetail (projectName, projectManagerName) VALUES (@ProjectName, @ProjectManagerName)";
            _db.Execute(projectSql, new { employee.Project.ProjectName, employee.Project.ProjectManagerName});
        }
        public void deleteEmpolyee(int id)
        {
            var projectSql = "DELETE FROM projectDetail WHERE projectID = @Id";
            _db.Execute(projectSql, new { Id = id });

            var employeeSql = "DELETE FROM employeeDetail WHERE employeeID = @Id";
            _db.Execute(employeeSql, new { Id = id });
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            var sql = "SELECT * FROM employeeDetail INNER JOIN projectDetail ON employeeDetail.employeeID = projectDetail.projectID";
            var employees = _db.Query<EmployeeModel, ProjectDetail, EmployeeModel>(
                sql,
                (employee, project) =>
                {
                    employee.Project = project;
                    return employee;
                },
                splitOn: "projectID");
            return employees;
        }

        public EmployeeModel GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void updateEmpolyee(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }
    }
}
