using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public interface IOrderService
  {
    OrderDetail CreateOrder(RequestOrder requestOrder, int customerId);
  }
  public class OrderService : BaseService, IOrderService
  {

    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public OrderService(DatabaseContext context, IOptions<AppSettings> appSettings) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public OrderDetail CreateOrder(RequestOrder requestOrder,int customerId)
    {
      var newActivity = new OrderDetail()
      {
        
        CreatedOn = DateTime.Now
      };
      return newActivity;
    }
  }
}
