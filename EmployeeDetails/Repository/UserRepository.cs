using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetails.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("EmployeeContext"));
        }

        public bool ValidateUser(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM UserDetail WHERE Username = @Username AND Password = @Password";

            // Execute the query and return true if a matching user is found
            var result = _db.ExecuteScalar<int>(query, new { Username = username, Password = password });
            return result > 0;
        }
    }
}
