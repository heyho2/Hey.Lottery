using Snowflake.Net;

namespace Huach.Framework.NumberGenerator
{
    /// <summary>
    /// 动态生产有规律的ID  snowflake
    /// </summary>
    public class IdGenerate
    {
        private static IdWorker worker = new IdWorker(1, 1);

        public static long NewId()
        {
            return worker.NextId();
        }

    }
}
