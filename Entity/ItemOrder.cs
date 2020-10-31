using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ItemOrder
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ActivityId { get; set; } 

    public string Address { get; set; }

    public DateTime CreatedOn { get; set; }

    public string Status { get; set; }

    public decimal Price { get; set; }

    public virtual OrderDetail ActivityDetail { get; set; }
  }
}
