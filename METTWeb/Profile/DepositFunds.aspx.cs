using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;

namespace MEWeb.Profile
{
  public partial class DepositFunds : MEPageBase<DepositFundsVM>
  {
  }
	public class DepositFundsVM : MEStatelessViewModel<DepositFundsVM>
	{
		public MELib.Accounts.WalletList WalletList { get; set; }
		public MELib.Accounts.AccountList AccountLIst { get; set; }
		public MELib.Accounts.Account DepositAmt { get; set; }
		public int UserID { get; set; }
		public DepositFundsVM()
		{

		}
		protected override void Setup()
		{
			base.Setup();
			UserID = Singular.Security.Security.CurrentIdentity.UserID;
			WalletList = MELib.Accounts.WalletList.GetWalletList();
			AccountLIst = MELib.Accounts.AccountList.GetAccountList(UserID);
			DepositAmt = AccountLIst.FirstOrDefault();

		}


		[WebCallable]
		public Result FundAccount(MELib.Accounts.AccountList Account)
		{
			Result sr = new Result();
			try
			{
				decimal Balance = Account.FirstOrDefault().Balance;
				string BalanceString = Account.FirstOrDefault().Balance.ToString();
				bool Stringresult = decimal.TryParse(BalanceString, out Balance);
				if (Stringresult && Balance > 0)
				{
					var newBalance = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
					newBalance.UserID = Singular.Security.Security.CurrentIdentity.UserID;
					newBalance.Balance += Account.FirstOrDefault().Balance;
					newBalance.TrySave(typeof(MELib.Accounts.AccountList));

					MELib.Accounts.Transaction Transaction = new MELib.Accounts.Transaction();
					MELib.Accounts.TransactionList TransactionList = new MELib.Accounts.TransactionList();

					Transaction.UserID = Singular.Security.Security.CurrentIdentity.UserID;
					Transaction.TransactionTypeID = 2;
					Transaction.Amount = Account.FirstOrDefault().Balance;
					Transaction.IsActiveInd = true;
					Transaction.Date = Transaction.CreatedDate;
					TransactionList.Add(Transaction);
					TransactionList.TrySave();
					sr.Success = true;
					return sr;
				}
				else
				{
					sr.ErrorText = "Please enter a numeric value or a value greater then 0";
					sr.Success = false;
					return sr;
				}
			}
            catch 
			{
				sr.ErrorText = "Please enter a numeric value or a value greater then 0";
				sr.Success = false;
				return sr;
			}
			


			
		}
	}
}

