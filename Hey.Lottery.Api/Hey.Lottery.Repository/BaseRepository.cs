using Hey.Lottery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Repository
{
    /// <summary>
    /// 实现对数据库的操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> where T : BaseModel
    {
    }
}
