using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huach.Framework
{
    public static class ServiceProviderExtesions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
        public static T GetService<T>(this IServiceProvider serviceProvider, Type type)
        {
            return (T)(serviceProvider.GetService(type) ?? Activator.CreateInstance(type));
        }
    }
}
