using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
    public class Stop
    {
        public SemaphoreSlim semaphore = new(1);
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual Flight? Flight { get; set; }
    }
}
