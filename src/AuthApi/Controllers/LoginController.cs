using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using AuthApi.Model;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Controllers;

[ApiController]
[Route("api/auth")]
public class LoginController : ControllerBase
{
    // public UserCurrent _userDefault { get; set; } = 
    //     new UserCurrent { UserName = "silvaam.guilherme@gmail.com", Senha = "Senha123@"};
    // public UserCurrent _userAdmin{ get; set; } = 
    //     new UserCurrent { UserName = "admin@admin.com", Senha = "Admin123@"};
    // public UserCurrent _userGestor { get; set; } = 
    //     new UserCurrent { UserName = "gestor@gestor.com", Senha = "Gestor123@"};

    public List<UserCurrent> _users = new List<UserCurrent>()
    {
        new UserCurrent {Senha = "Senha123@", UserName = "silvaam.guilherme@gmail.com", 
            FirstName = "Guilherme", Claim = new Claim("Home", "Default")},
        new UserCurrent {UserName = "gestor@gestor.com", Senha = "Gestor123@", 
            FirstName = "Gestor", Claim = new Claim("Home", "Admin")},
        new UserCurrent { UserName = "admin@admin.com", Senha = "Admin123@", 
            FirstName = "Admin", Claim = new Claim("Home", "Admin")}
    };
    [HttpPost]
    public async Task<ActionResult> Login(Login login)
    {
        if (ModelState.IsValid)
        {
            foreach (var user in _users)
            {
                if (login.UserName == user.UserName && login.Senha == user.Senha)
                {
                    return Ok(new
                    {
                        Success = true,
                        AccessToken = await GenerateToke(user),
                        Name = user.FirstName
                    });
                }
            }
            return BadRequest(new
            {
                Success = false,
                Erro = new List<string>
                {
                   "Erro",
                }
            });
        }
        return BadRequest(new
        {
            Success = false,
            Erro = new List<string>
            {
                "Erro",
            }
        });
    }

    private async Task<string> GenerateToke(UserCurrent user)
    {
        var claims = new List<Claim>();
        
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.UserName));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(user.Claim);
        var calimIdetity = new ClaimsIdentity(claims);
        var tokeHandler = new JwtSecurityTokenHandler();

        var token  = tokeHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = "AuthApi",
            Audience = "https://localhost",
            Subject = calimIdetity,
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("comeequetapowcomeequetapowcomeequetapow")), SecurityAlgorithms.HmacSha256Signature)
        });
        
        return await Task.FromResult(tokeHandler.WriteToken(token));
    }
    
}