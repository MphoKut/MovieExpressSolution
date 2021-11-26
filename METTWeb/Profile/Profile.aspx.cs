using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;

namespace MEWeb.Profile
{
  public partial class Profile : MEPageBase<ProfileVM>
  {
  }
  public class ProfileVM : MEStatelessViewModel<ProfileVM>
  {
        public MELib.Accounts.AccountList UserAccountList { get; set; }
        public MELib.Accounts.Account UserAccount { get; set; }

        public MELib.RO.ROUserList UserList { get; set; }
        public int UserID { get; set; }

        public ProfileVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
            UserAccountList = MELib.Accounts.AccountList.GetAccountList();
            UserAccount = UserAccountList.FirstOrDefault();
            UserID = Singular.Security.Security.CurrentIdentity.UserID;
            UserList = MELib.RO.ROUserList.GetROUserList(UserID);
        }
  }
}

