using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillPayerCore.DataModels;

namespace BillPayerCore.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<HouseHold> HouseHolds { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillSplit> BillSplits { get; set; }
    }
}
