namespace EmployeeDetails.Repository
{
    public interface IUserRepository
    {
        bool ValidateUser(string username, string password);
    }
}
