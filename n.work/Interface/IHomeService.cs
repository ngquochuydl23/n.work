using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Entity;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public interface IHomeService
  {
    GetStarted GetStarted(string authorization);

    List<ItemInstruction> GetListInstruction(string authorization);
  }

  public class HomeService : BaseService, IHomeService
  {
    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public HomeService(DatabaseContext context, IOptions<AppSettings> appSettings) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public List<ItemInstruction> GetListInstruction(string authorization)
    {
      return null;
    }

    public GetStarted GetStarted(string authorization)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);
      var userID = tokenContent.userId;
      var profile = GetUser(userID);

      var responseHome = new GetStarted()
      {
        helloUser = "Hello " + profile.Fullname + " !",
        wishUser = SessionsOfTheDay() + ", welcome back"
      };
      return responseHome;
    }

    public Profile GetUser(int userId)
    {
      var account = context.Profile.FirstOrDefault(profile => profile.AccountId == userId);
      return account;
    }

    public string SessionsOfTheDay()
    {
      var time = DateTime.UtcNow.AddHours(5.5).Hour;
      
      if (time < 12)
        return "Good Morning";
      else if (time < 17)
        return "Good Afternoon";
      else if (time < 20)
        return "Good Evening";
      return "Good Night";
    }

   
  }
}
