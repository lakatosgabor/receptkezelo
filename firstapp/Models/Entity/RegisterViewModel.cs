using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required(ErrorMessage = "A név megadása kötelező.")]
    [Display(Name = "Név")]
    public string Name { get; set; }

    [Required(ErrorMessage = "A User név megadása kötelező.")]
    [Display(Name = "User név")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "A város név megadása kötelező.")]
    [Display(Name = "Város")]
    public string City { get; set; }

    [Required(ErrorMessage = "Az ország név megadása kötelező.")]
    [Display(Name = "Ország")]
    public string Country { get; set; }

    [Display(Name = "Profil kép")]
    public string ProfilePictureUrl { get; set; }

    [Required(ErrorMessage = "Az email cím megadása kötelező.")]
    [EmailAddress(ErrorMessage = "Helytelen email cím formátum.")]
    [Display(Name = "Email cím")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A jelszó megadása kötelező.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "A {0} legalább {2} és legfeljebb {1} karakter hosszú lehet.", MinimumLength = 6)]
    [Display(Name = "Jelszó")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Jelszó megerősítése")]
    [Compare("Password", ErrorMessage = "A jelszó és a megerősítő jelszó nem egyezik.")]
    public string ConfirmPassword { get; set; }
}