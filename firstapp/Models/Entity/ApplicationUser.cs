using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ProfilePictureUrl { get; set; }
}