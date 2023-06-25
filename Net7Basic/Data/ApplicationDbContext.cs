using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Net7Basic.Models;

namespace Net7Basic.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
    }
}
