namespace AspNetCoreRestfulApi.Dto.Request
{
    public class UserRequestDTO
    {
        public String Name { get; set; }

        public String Email { get; set; }

        public UserRequestDTO(string name, string email)
        {
            Name = name;
            Email = email;
        }

    }
}
