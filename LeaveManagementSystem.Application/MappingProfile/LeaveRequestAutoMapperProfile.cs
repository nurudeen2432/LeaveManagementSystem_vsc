using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Models.LeaveTypes;

namespace LeaveManagementSystem.Application.MappingProfile;

public class LeaveRequestAutoMapperProfile : Profile
{
    
    public LeaveRequestAutoMapperProfile()
    {
        

        CreateMap<LeaveRequestCreateVM, LeaveRequest>();

      
        
    }

}