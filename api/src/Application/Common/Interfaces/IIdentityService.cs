using Domain.Identity.Users;
using LanguageExt;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Option<UserId> GetUserId();
    Option<string> GetUserEmail();
    Option<string> GetUserName();
    Option<string[]> GetUserRoles();
}
