using Picums.Data.Domain;

namespace Picums.Mvc.UserAccess.Claims
{
    public interface IUser : IAggregateRoot, IPermissionContainer, IIdentityUser
    {
    }
}