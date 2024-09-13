using Core.Application.Responses;
using Domain.Entities.GeneralManagements;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid
{
    public class GetByGidLogAuthorizationErrorResponse : IResponse
    {
        public Guid Gid { get; set; }
public Guid? GidUserFK { get; set; }
public User UserFK { get; set; }

public string? IpAddress { get; set; }
public string PageInfo { get; set; }
public string? Operation { get; set; }
public string? JSonData { get; set; }

    }
}