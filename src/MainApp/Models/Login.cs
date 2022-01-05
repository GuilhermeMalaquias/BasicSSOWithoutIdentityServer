using System.ComponentModel.DataAnnotations;

namespace MainApp.Models;

public class Login
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Senha { get; set; }
}