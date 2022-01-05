using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AuthApi.Model;

public class Login
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Senha { get; set; }
}

public class UserCurrent
{
    public UserCurrent()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string Senha { get; set; }
    
    public Claim Claim { get; set; }
}

public class Register
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Senha { get; set; }
    [Compare("Senha")]
    public string ComfirmarSenha { get; set; }
}