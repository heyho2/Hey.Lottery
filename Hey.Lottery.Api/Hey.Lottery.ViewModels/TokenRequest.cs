using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.ViewModels
{
    /// <summary>
    /// 请求
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; internal set; }
    }
}
