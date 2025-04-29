using Domain.Entities.GeneralManagements;

namespace Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(User user, int minute = 600);
        Token CreateAccessToken(Admin admin, int minute = 600);

    }
}
