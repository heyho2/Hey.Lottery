using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huach.Framework.Models
{
    public class PagingResult<TResult>
    {
        public List<TResult> Items { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }
}
