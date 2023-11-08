using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tasks
{
    public class Task
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime Deadline { get; set; }
        public byte Priority { get; set; }
    }
}
