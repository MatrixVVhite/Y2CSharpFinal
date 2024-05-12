using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    internal interface ICommandable
    {
        public Action Action { get; set; }
        public Action<object> Action1 { get; set; }
        public Action<object, object> Action2 { get; set; }
        public Action<object, object, object> Action3 { get; set; }
        public Func<object> Function1 { get; set; }
        public Func<object, object> Function2 { get; set; }
        public Func<object, object, object> Function3 { get; set; }

    }
}
