using Hey.Lottery.Models;
using Huach.Framework.Log;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Repository
{
    public class LotteryDBContext : DbContext
    {
        private readonly Logger logger = Logger.CreateLogger(typeof(LotteryDBContext));
        public LotteryDBContext() : base("name=LotteryConnection")
        {
            Database.Log = msg => logger.Debug(msg);
        }
        static LotteryDBContext()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<LotteryDBContext, Configuration>());
        }

        public virtual DbSet<Prize> PrizeSet { get; set; }
        public virtual DbSet<PrizeType> PrizeTypeSet { get; set; }
        public virtual DbSet<Employee> EmployeeSet { get; set; }
    }
}
