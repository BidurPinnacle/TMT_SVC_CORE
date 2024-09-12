namespace TMT_Code_Migration1.Models.Authentication
{
    public class UserLoginRequest
    {
        public required string username { get; set; }
        public required string password { get; set; }

    }
    public class LoginResponse
    {
        public string? RoleName { get; set; }
        public string? UserId { get; set; }
        public string? username { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? CompanyID { get; set; }
        public string? CName { get; set; }
        public object? Token { get; set; }

    }
    public class UserRegistrationRequest
    {
        public  string? userid { get; set; }
        public string? companyid { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public  string? roleid { get; set; }
        public  string? StatementType { get; set; }
        public int? userstatus { get; set; }
        public string? encPwd { get; set; }
    }
    public class UserRegistrationResponse
    {
        public bool IsInserted { get; set; }
    }
}
