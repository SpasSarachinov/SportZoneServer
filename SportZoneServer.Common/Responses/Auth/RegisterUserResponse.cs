namespace SportZoneServer.Common.Responses.Auth;

public class RegisterUserResponse
{
    public required string Email { get; set; } 
    
    public required string PasswordHash { get; set; } 
}
