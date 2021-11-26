using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;

namespace MEWeb.Profile
{
  public partial class Transactions : MEPageBase<TransactionsVM>
  {
  }
  public class TransactionsVM : MEStatelessViewModel<TransactionsVM>
  {
        public MELib.Accounts.TransactionList TransactionList { get; set; }
        public int UserID { get; set; }
        public TransactionsVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
            UserID = Singular.Security.Security.CurrentIdentity.UserID;
            TransactionList = MELib.Accounts.TransactionList.GetTransactionList(UserID);
    }
  }
}

