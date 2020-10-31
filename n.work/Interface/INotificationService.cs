using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public interface INotificationService
  {
    Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data);
  }

  public class NotificationService : INotificationService
  {
    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public NotificationService(DatabaseContext context)
    {
      this.context = context;
    }

    public NotificationService(DatabaseContext context, IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public async Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data)
    {
      throw new NotImplementedException();
    }
  }

}
