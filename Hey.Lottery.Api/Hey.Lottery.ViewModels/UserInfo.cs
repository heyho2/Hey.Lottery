using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels
{
    /// <summary>
    /// 登陆用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 用户上下文
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public static UserInfo Info { get; set; }
    }
}
