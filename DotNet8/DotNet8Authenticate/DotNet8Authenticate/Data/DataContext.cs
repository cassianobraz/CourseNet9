using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNet8Authenticate.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}
