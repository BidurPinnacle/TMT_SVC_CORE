using Microsoft.EntityFrameworkCore;

namespace TMT_Code_Migration1.Data
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options)
        {
            
        }
    }
}
