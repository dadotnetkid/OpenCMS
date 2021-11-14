namespace OpenCMS.Domain.Models
{
    public class LoginModel
    {
        public  string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
