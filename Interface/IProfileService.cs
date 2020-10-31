using Microsoft.AspNetCore.Http;
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
  public interface IProfileService
  {
    Account GetProfile(string authorization);
    Task<Account> UpdateProfile(string authorization, UpdateProfile newProfile);
  }
  public class ProfileService : BaseService,IProfileService
  {

    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public ProfileService(DatabaseContext context, IOptions<AppSettings> appSettings) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public Account GetProfile(string authorization)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);
      var userID = tokenContent.userId;
      var account = context.Account.Include(user => user.Profile).SingleOrDefault(account => account.Id == userID);
      return account;
    }

    public async Task<Account> UpdateProfile(string authorization, UpdateProfile newProfile)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);
 
      var userID = tokenContent.userId;
      var account = context.Account.Include(user => user.Profile).SingleOrDefault(account => account.Id == userID);

      var currentProfile = account.Profile;

      if (newProfile != null)
      {
        account.Phonenumber = newProfile.Phonenumber;
        account.Profile.Fullname = newProfile.Fullname;
        account.Profile.Email = newProfile.Email;
        account.Profile.Avatar = newProfile.Avatar;
      }

      await context.SaveChangesAsync();
      return account;
    }
  }
}
