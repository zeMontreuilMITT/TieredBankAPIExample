using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TieredBankAPI.Models;

namespace TieredBankAPI.Data
{
    public class TieredBankAPIContext : DbContext
    {
        public TieredBankAPIContext (DbContextOptions<TieredBankAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TieredBankAPI.Models.Account> Account { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
    }
}
