using EmployeeDetails.Models;

namespace EmployeeDetails.Repository
{
    public interface IEmpolyeeRepository
    {
        EmployeeModel GetByID(int id);

        IEnumerable<EmployeeModel> GetAll();

        void AddEmpolyee(EmployeeModel employee);

        void updateEmpolyee(EmployeeModel employee);

        void deleteEmpolyee(int id);
    }
}
