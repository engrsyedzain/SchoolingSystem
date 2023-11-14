using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolingSystem.Dtos;
using SchoolingSystem.Models;

namespace SchoolingSystem.Repositories
{
    public class StudentRepositories : IStudentRepositories
    {
        private readonly SchoolDbContext _schoolDbContext;

        public StudentRepositories(SchoolDbContext schoolDbContext)
        {           
            this._schoolDbContext = schoolDbContext;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _schoolDbContext.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> Get(int id)
        {
            return await _schoolDbContext.Students.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> Add(Student student)
        {
            _schoolDbContext.Students.Add(student);
            await _schoolDbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Update(Student student)
        {
            var studentDb = _schoolDbContext.Students.Attach(student);
            studentDb.State = EntityState.Modified;         
            await _schoolDbContext.SaveChangesAsync();
            return student;
          
        }

        public async Task<Student> Delete(int id)
        {
            var student = await _schoolDbContext.Students.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
            if(student != null)
            {
                _schoolDbContext.Students.Remove(student);
               await _schoolDbContext.SaveChangesAsync();
            }
            return student;
        }
    }
}
