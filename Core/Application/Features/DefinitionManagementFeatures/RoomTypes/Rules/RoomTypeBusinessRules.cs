using Application.Features.DefinitionManagementFeatures.RoomTypes.Constants;
using Application.Repositories.DefinitionManagementRepos.RoomTypeRepo;
using Core.Application;
using Core.CrossCuttingConcern.Exceptions;
using X = Domain.Entities.DefinitionManagements;

namespace Application.Features.DefinitionManagementFeatures.RoomTypes.Rules;

public class RoomTypeBusinessRules : BaseBusinessRules
{


    private readonly IRoomTypeReadRepository _roomTypeReadRepository;

    public RoomTypeBusinessRules(IRoomTypeReadRepository roomTypeReadRepository)
    {
        _roomTypeReadRepository = roomTypeReadRepository;
    }

    public async Task RoomTypeShouldExistWhenSelected(X.RoomType? item)
    {
        if (item == null)
            throw new BusinessException(RoomTypesBusinessMessages.RoomTypeNotExists);
    }

    public async Task CheckRoomTypeNameIsUnique(string roomTypeName, Guid? roomTypeGuid = null)
    {
        var roomType = await _roomTypeReadRepository.GetAsync(predicate: x => x.Name.ToLower() == roomTypeName.ToLower() && (roomTypeGuid == null || x.Gid != roomTypeGuid));
        if (roomType != null)
            throw new BusinessException(RoomTypesBusinessMessages.RoomTypeIsAlreadyExists);
    }

    public async Task CheckRoomTypeCodeIsUnique(string roomTypeCode, Guid? roomTypeGuid = null)
    {
        var roomType = await _roomTypeReadRepository.GetAsync(predicate: x => x.Code.ToLower() == roomTypeCode.ToLower() && (roomTypeGuid == null || x.Gid != roomTypeGuid));
        if (roomType != null)
            throw new BusinessException(RoomTypesBusinessMessages.RoomTypeCodeIsAlreadyExists);
    }
}