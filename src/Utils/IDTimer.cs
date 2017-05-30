using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcsos
{
    public class TimerIDs : System.Timers.Timer
    {
        public int ID { get; private set; }
        public TimerIDs(int id) : base() { ID = id; }
        public TimerIDs(double interval, int id) : base(interval) { ID = id; }
    }
}
