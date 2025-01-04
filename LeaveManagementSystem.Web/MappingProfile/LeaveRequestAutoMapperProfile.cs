using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.MappingProfile;

public class LeaveRequestAutoMapperProfile : Profile
{
    
    public LeaveRequestAutoMapperProfile()
    {
        

        CreateMap<LeaveRequestCreateVM, LeaveRequest>();

      
        
    }

}