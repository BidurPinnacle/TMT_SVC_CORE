using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.Models.Users;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface IUsers
    {
        List<GetUser> GetUsers();
        List<GetUserForSearch> GetUsersForSearch(UserRequestForSearch userRqst);
        List<GetUserForSearch> GetUsersForQASearch(UserRequestForSearch userRqst);
        UserspasswordResponse changepassword(UserspasswordRequest userspasswordRequest);
        Usersroleresponse changerole(Usersrolerequest usersrolerequest);
        List<AccessResponse> GetRoleMenuAccessDL1(AccessRequest request);
       
    }
}
