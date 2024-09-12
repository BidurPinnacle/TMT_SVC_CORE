using Microsoft.AspNetCore.Identity.Data;
using TMT_Code_Migration1.Models.Authentication;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface IAuthentication
    {
        LoginResponse LoginUsers(UserLoginRequest loginRequest);
        UserRegistrationResponse RegisterUser(UserRegistrationRequest userRegistrationRequest);
        Object GetToken();
    }
}
