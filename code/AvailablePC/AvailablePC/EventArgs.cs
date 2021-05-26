using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AvailablePC.Entity;

namespace AvailablePC
{
    public class TestEventArgs : EventArgs
    {
        public Test Test { get; set; }
        public bool? ScoreOnly { get; set; }
        public TestEventArgs()
        {
            ScoreOnly = null;
        }

    }

    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }

    public class RequestEventArgs : EventArgs
    {
        public Request Request { get; set; }
    }

    public class IEnumerableEventArgs<T> : EventArgs
    {
        public IEnumerable<T> List { get; set; }

    }

    public class QueryEventArgs<T> : EventArgs
    {
        public List<T> List { get; set; }
    }

}
