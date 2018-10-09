using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Repository
{
    public class DBContextFactory
    {
        private DBContextFactory() { }
        public static DbContext GetCurrentDbContext
        {
            get
            {
                DbContext context = CallContext.GetData(nameof(DbContext)) as LotteryDBContext;
                if (context == null)
                {
                    context = new LotteryDBContext();
                    //将值设置到数据槽里面去
                    CallContext.SetData(nameof(DbContext), context);
                }
                return context;
            }
        }
    }
}
