namespace AspNetCoreRestfulApi.Dto.Request
{
    public class UserRequestDto(string name, string email)
    {
        public String Name { get; set; } = name;

        public String Email { get; set; } = email;
    }
}
