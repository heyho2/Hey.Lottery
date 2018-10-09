using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huach.Framework
{
    public class ServiceProvider
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider Current
        {
            get
            {
                if (!IsServiceProviderSet)
                {
                    throw new InvalidOperationException("必须先调用SetServiceProvider赋值");
                }
                return _serviceProvider;
            }
        }
        public static bool IsServiceProviderSet
        {
            get
            {
                return _serviceProvider != null;
            }
        }
        public static void SetServiceProvider(IServiceProvider newProvider)
        {
            _serviceProvider = newProvider;
        }
    }
}
