using Microsoft.AspNetCore.Identity;

namespace Store.Sts.Model
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationProfile Profile { get; set; }
    }
}
