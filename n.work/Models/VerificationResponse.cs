using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class VerificationResponse
  {
    public string AuthToken { get; set; }

    public Account Account { get; set; }

    public VerificationResponse(Account Account,string AuthToken)
    {
      this.Account = Account;
      this.AuthToken = AuthToken;
    }
  }
}
