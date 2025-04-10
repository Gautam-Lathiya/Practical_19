using AutoMapper;
using Practical_17.Models;
using Practical_17.ViewModels;

namespace Practical_17.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
        }
    }
}
