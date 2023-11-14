using SchoolingSystem.Dtos;
using SchoolingSystem.Models;
using System.Collections.Generic;

namespace SchoolingSystem.Repositories
{
    public interface IStudentRepositories
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> Get(int id);
        Task<Student> Add(Student student);
        Task<Student> Update(Student student);
        Task<Student> Delete(int id);
    }
}
