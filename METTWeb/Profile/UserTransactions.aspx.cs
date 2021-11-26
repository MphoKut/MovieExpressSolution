using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MEWeb.Profile
{
    public partial class UserTransactions : MEPageBase<UserTransactionsVM>
    {
    }
    public class UserTransactionsVM : MEStatelessViewModel<UserTransactionsVM>
    {
        public MELib.Accounts.TransactionList TransactionList { get; set; }
        public int UserID { get; set; }
        public UserTransactionsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            UserID = System.Convert.ToInt32(Page.Request.QueryString[0]);
            TransactionList = MELib.Accounts.TransactionList.GetTransactionList(UserID);
        }
    }
}