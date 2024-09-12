namespace TMT_Code_Migration1.Models.Users
{
    public class GetUser
    {
        public string? UserId { get; set; }
        public string? UserType { get; set; }
        public string? UserName { get; set; }
        public int? CompanyID { get; set; }
        public string? Company { get; set; }
        public string? firstname { get; set; }
        public string? lastName { get; set; }
        public int? roleid { get; set; }
        public int? status { get; set; }
    }
    public class GetUserForSearch
    {
        public string? USERID { get; set; }
        public string? FULLNAME { get; set; }
    }
    public class UserRequestForSearch
    {
        public string? UserId { get; set; }
    }
    public class UserspasswordRequest
    {
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? OldPassword { get; set; }

    }

    public class UserspasswordResponse
    {
        public bool? isChanged { get; set; }
        public string? Message { get; set; }

    }
    public class Usersrolerequest
    {
        public string? UserId { get; set; }
        public string? roleid { get; set; }

    }

    public class Usersroleresponse
    {
        public bool? isChanged { get; set; }
        public string? Message { get; set; }

    }
    public class AccessRequest
    {
        public int? companyid { get; set; }
        public string? userid { get; set; }
    }
    public class AccessResponse
    {
        public int? role_id { get; set; }
        public int? menu_id { get; set; }
        public string? menu_text { get; set; }
        public string? menu_path { get; set; }
        public string? icontext { get; set; }
        // Navigation property for Submenus
        public virtual ICollection<Submenu>? Submenus { get; set; }
    }
    public class Submenu
    {
        public int? SUBMENU_ID { get; set; }
        public string? SUBMENU_TEXT { get; set; }
        public string? SUBMENU_PATH { get; set; }
        public string? SUBICONTEXT { get; set; }

        // Foreign key property for AccessResponse
        public int? AccessResponseId { get; set; }
        public virtual AccessResponse? AccessResponse { get; set; }
    }
}
