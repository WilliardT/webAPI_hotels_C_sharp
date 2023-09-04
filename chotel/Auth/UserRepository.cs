namespace chotel.Auth
{
    public class UserRepository : IUserRepository
    {
        private List<UserDto> _users => new()
        {
            new UserDto("Lari", "123"),
            new UserDto("Petra", "123"),
            new UserDto("Gvatemala", "123"),
        };
        public UserDto GetUser(UserModel userModel) =>
            _users.FirstOrDefault(user => 
            string.Equals(user.UserName, userModel.UserName) &&
            string.Equals(user.Password, userModel.Password)) ??
            throw new Exception("User not found");
    }
}
