using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.MappingProfile;

public class AutoMapperProfile : Profile
{
    
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, IndexVM>();

        CreateMap<LeaveType, DetailsVM>();

        CreateMap<LeaveTypeCreateVM, LeaveType>();

        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();

        CreateMap<LeaveTypeDeleteVm, LeaveType>().ReverseMap();
        
    }

}