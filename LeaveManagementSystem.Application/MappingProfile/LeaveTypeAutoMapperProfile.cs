using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveTypes;

namespace LeaveManagementSystem.Application.MappingProfile;

public class LeaveTypeAutoMapperProfile : Profile
{
    
    public LeaveTypeAutoMapperProfile()
    {
        CreateMap<LeaveType, IndexVM>();

        CreateMap<LeaveType, DetailsVM>();

        CreateMap<LeaveTypeCreateVM, LeaveType>();

        CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();

        CreateMap<LeaveTypeDeleteVm, LeaveType>().ReverseMap();
        
    }

}
