using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class RequestOrder
  {
    public string Request { get; set; }

    public double DesiredAmount { get; set; }

    public string NoteWorker { get; set; }
  }
}
