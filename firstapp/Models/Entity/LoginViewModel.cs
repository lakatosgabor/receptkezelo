using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

public class LoginViewModel
{
    [Required(ErrorMessage = "Felhasználónév kötelező")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Jelszó kötelező")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Emlékezz rám")]
    public bool RememberMe { get; set; }
}
