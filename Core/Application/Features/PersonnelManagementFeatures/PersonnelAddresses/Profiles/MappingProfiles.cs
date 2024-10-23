using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Create;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Delete;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Update;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByUserGid;
using Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<X.PersonnelAddress, CreatePersonnelAddressCommand>().ReverseMap();
        CreateMap<X.PersonnelAddress, CreatedPersonnelAddressResponse>().ReverseMap();
        CreateMap<X.PersonnelAddress, UpdatePersonnelAddressCommand>().ReverseMap();
        CreateMap<X.PersonnelAddress, UpdatedPersonnelAddressResponse>().ReverseMap();
        CreateMap<X.PersonnelAddress, DeletePersonnelAddressCommand>().ReverseMap();
        CreateMap<X.PersonnelAddress, DeletedPersonnelAddressResponse>().ReverseMap();

        CreateMap<X.PersonnelAddress, GetByGidPersonnelAddressResponse>().ReverseMap();

        CreateMap<X.PersonnelAddress, GetListPersonnelAddressListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelAddress>, GetListResponse<GetListPersonnelAddressListItemDto>>().ReverseMap();

        CreateMap<X.PersonnelAddress, GetByUserGidListPersonnelAddressListItemDto>().ReverseMap();
        CreateMap<IPaginate<X.PersonnelAddress>, GetListResponse<GetByUserGidListPersonnelAddressListItemDto>>().ReverseMap();

    }
}