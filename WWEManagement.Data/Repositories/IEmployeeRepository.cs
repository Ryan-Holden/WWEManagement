using WWEManagement.Data.Models;

namespace WWEManagement.Data.Repositories;

public interface IEmployeeRepository
{
    List<Employee> GetAll();

    Employee? GetById(int id);

    int Add(Employee employee);

    bool Update(Employee employee);

    bool Delete(int id);
}