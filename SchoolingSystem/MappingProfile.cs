using AutoMapper;
using SchoolingSystem.Dtos;
using SchoolingSystem.Models;
using System.Net;

namespace SchoolingSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}
