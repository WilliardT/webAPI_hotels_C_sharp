using chotel.Auth;

namespace chotel.APIs
{
    public class AuthApi : IApi
    {
        public void Register(WebApplication app) 
        {
            app.MapGet("/login", [AllowAnonymous] async (HttpContext context,
               ITokenService tokenService, IUserRepository userRepository) =>
            {
                UserModel user = new()
                {
                    UserName = context.Request.Query["username"],
                    Password = context.Request.Query["password"]
                };
                var userDto = userRepository.GetUser(user);
                if (userDto is null) return Results.Unauthorized();
                var token = tokenService.BuildToken(app.Configuration["Jwt:Key"],
                    app.Configuration["Jwt:Issuer"], userDto);
                return Results.Ok(token);
            });
        }
    }
}
