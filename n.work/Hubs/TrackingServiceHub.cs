using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Interface;
using n.work.Models;
using Twilio.Rest.Api.V2010.Account;

namespace n.work.Hubs
{
  public class TrackingServiceHub : Hub
  {
    private OrderService orderService;

    private readonly DatabaseContext context;

    string ON_ORDER_ACCEPT = "ON_ORDER_ACCEPT";
    string ON_RECEIVE_JOB = "ON_RECEIVE_JOB";
    string ON_ACCEPT_JOB = "ON_ACCEPT_JOB";
    string ON_HAD_FOUND_WORKER = "ON_HAD_FOUND_WORKER";
    string ON_PROGRESS_ORDER = "ON_PROGRESS_ORDER";

    public TrackingServiceHub(DatabaseContext _context, IOptions<AppSettings> appSettings)
    {
      context = _context;
      orderService = new OrderService(_context, appSettings);
    }

    public async Task FindWorkerFromUser(string user, RequestOrder requestOrder)
    {

      //orderService.CreateOrder(requestOrder,);
      await Clients.All.SendAsync(ON_ORDER_ACCEPT, user, "Your order is accepted");
      await Clients.All.SendAsync(ON_RECEIVE_JOB, user, requestOrder);
    }

    public async Task AcceptOrderFromWorker(string user)
    {
      var newWorker = new ResponseWorker()
      {
        Name = "Đoàn Khánh Xuân",
        Motor = "Honda Wave Rs - 49P1-1568",
        Rate = 5.0,
        Avatar = "https://scontent-hkg4-1.xx.fbcdn.net/v/t1.15752-9/122259495_675688720040397_4824557373910494864_n.jpg?_nc_cat=103&_nc_sid=ae9488&_nc_ohc=fVPNvmBwGd4AX-Ms7vs&_nc_ht=scontent-hkg4-1.xx&oh=8702950c7cebaf31f81dee16a5513420&oe=5FB318F5"
      };

      var address = new Address()
      {
        Latitude = 11.9486,
        Longitude = 108.4280,
        AddressDetail = "Hẻm Mai Hắc Đế, Phường 6, Thành phố Đà Lạt"
      };
      await Clients.All.SendAsync(ON_HAD_FOUND_WORKER, user, newWorker);
      await Clients.All.SendAsync(ON_ACCEPT_JOB, user, address);
    }

    public async Task CancelOrderFromWorker(string user, RequestOrder requestOrder)
    {
      await Clients.All.SendAsync(ON_HAD_FOUND_WORKER, user, requestOrder.DesiredAmount);
    }

    public async Task StartedMoving(string user, RequestOrder requestOrder)
    {
      await Clients.All.SendAsync(ON_PROGRESS_ORDER, user, requestOrder.DesiredAmount);
    }
  }
}
