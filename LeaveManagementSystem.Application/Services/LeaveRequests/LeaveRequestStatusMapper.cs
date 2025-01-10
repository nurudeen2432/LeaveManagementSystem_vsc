using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using static LeaveManagementSystem.Application.Services.LeaveRequests.LeaveRequestsService;

namespace LeaveManagementSystem.Application.Services.LeaveRequests;

  public static class LeaveRequestStatusMapper
{
    private static readonly Dictionary<LeaveRequestStatuses, Guid> StatusToGuidMap = new()
    {
        { LeaveRequestStatuses.Pending, Guid.Parse("2caff07e-1485-4603-813d-839098f2cc62") },
        { LeaveRequestStatuses.Approved, Guid.Parse("a07fcf48-e449-4638-94ea-07be6c232f55") },
        { LeaveRequestStatuses.Declined, Guid.Parse("388d0617-2de5-49a2-9408-f9f5819b0d5a") },
        { LeaveRequestStatuses.Canceled, Guid.Parse("c565c527-ff11-41ef-af5d-6f1d66ba3418") }
    };

    public static readonly Dictionary<Guid, LeaveRequestStatuses> GuidToStatusMap =
        StatusToGuidMap.ToDictionary(kv => kv.Value, kv => kv.Key);

    public static Guid ToGuid(LeaveRequestStatuses status) =>
        StatusToGuidMap.TryGetValue(status, out var guid) ? guid : throw new ArgumentOutOfRangeException(nameof(status));

    public static LeaveRequestStatuses ToStatus(Guid guid) =>
        GuidToStatusMap.TryGetValue(guid, out var status) ? status : throw new ArgumentOutOfRangeException(nameof(guid));
}
