using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using LeaveManagementSystem.Web.Models.Periods;

namespace LeaveManagementSystem.Web.MappingProfile
{
    public class LeaveAllocationAutoMapperProfile : Profile
    {
        public LeaveAllocationAutoMapperProfile()
        {
            CreateMap<ApplicationUser, EmployeeListVM>();

          CreateMap<LeaveAllocation, LeaveAllocationVM>()
            .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType)); // Map LeaveType explicitly

        CreateMap<Period, PeriodVM>();

        CreateMap<LeaveAllocation, LeaveAllocationEditVM>();

        // Add this mapping
        CreateMap<LeaveType, LeaveTypeReadOnlyVM>();

        }
    }
}