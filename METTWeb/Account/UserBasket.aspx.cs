using Singular.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace MEWeb.Account
{
	public partial class UserBasket : MEPageBase<UserBasketVM>
	{
	}
	public class UserBasketVM : MEStatelessViewModel<UserBasketVM>
	{
		public MELib.Basket.BasketList BasketList { get; set; }
		public MELib.Order.OrderList OrderList { get; set; }
		public MELib.Accounts.AccountList AccountLIst { get; set; }
		public MELib.RO.ROUserList UserList { get; set; }
		public MELib.RO.ROUser Account { get; set; }
		public int? BasketID { get; set; }
		public decimal Total { get; set; }
		public int UserID { get; set; }
		[Display(Name = "Delivery Option Type", Description = "")]
		[Singular.DataAnnotations.DropDownWeb(typeof(MELib.Basket.DeliveryOptionTypeList), UnselectedText = "Option", ValueMember = "DeliveryOptionTypeID", DisplayMember = "DeliveryOptionTypeName")]
		public int? DeliveryOptionTypeID { get; set; }





		public UserBasketVM()
		{

		}
		protected override void Setup()
		{
			base.Setup();
			UserID = System.Convert.ToInt32(Page.Request.QueryString[0]);
			BasketList = MELib.Basket.BasketList.GetBasketList(UserID,0,0);
			OrderList = MELib.Order.OrderList.GetOrderList();
			AccountLIst = MELib.Accounts.AccountList.GetAccountList();
			Total = BasketList.Sum(c => c.FinalAmount);
			UserList = MELib.RO.ROUserList.GetROUserList(UserID);
			Account = UserList.GetItem(UserID);

		}
		public Result SaveBasketList(int BasketID, MELib.Basket.BasketList BasketList)
		{
			Result sr = new Result();

			
			if (BasketList.IsValid)
			{
				var SaveResult = BasketList.TrySave();

				if (SaveResult.Success)
				{
					sr.Data = SaveResult.SavedObject;
					sr.Success = true;
				}
				else
				{
					sr.ErrorText = SaveResult.ErrorText;
					sr.Success = false;
				}
				return sr;
			}
			else
			{
				sr.ErrorText = BasketList.GetErrorsAsHTMLString();
				return sr;
			}
		}

		public Result UpdateBasket(int BasketID, MELib.Basket.BasketList BasketList, int UserID)
		{
			Result sr = new Result();
			try
			{
				//Saving the basket after removing products from it
				var Basket = MELib.Basket.BasketList.GetBasketList(UserID, BasketID,0).FirstOrDefault();
				var newQuantity = BasketList.GetItem(BasketID).ItemsCount;
				var ProdID = Basket.ProductID;
				var Product = MELib.Maintenance.ProductList.GetProductList(0, ProdID).FirstOrDefault();
				var Amt = Product.Price;
				Basket.ItemsCount = newQuantity;
				Basket.FinalAmount = newQuantity * Amt;
				if (newQuantity > Product.Quantity)
				{
					sr.ErrorText = "Not enough in stock";
					sr.Success = false;
					
				}
				else
				{
					BasketList.Add(Basket);
					BasketList.TrySave();
					sr.Success = true;
				
				}
			}
            catch (Exception e)
            {

				sr.ErrorText = e.Message;
				sr.Success = false;
			}
			return sr;

		}
		public Result Checkout(int BasketID, MELib.Basket.BasketList BasketList, int DeliveryOptionTypeID, int UserID, decimal Total, MELib.RO.ROUserList UserList)
		{
			Result sr = new Result();
			try
			{
				//getting the total of the products in the basket and new account balance
				var AccBalance = MELib.Accounts.AccountList.GetAccountList(UserID).Select(c => c.Balance).FirstOrDefault();

				var NewBalance = AccBalance - Total;

				var newBalance = MELib.Accounts.AccountList.GetAccountList(UserID).FirstOrDefault();
				newBalance.UserID = UserID;
				newBalance.Balance = NewBalance;
				newBalance.TrySave(typeof(MELib.Accounts.AccountList));

				//Transfering the basket into an order
				MELib.Order.Order Order = new MELib.Order.Order();
				MELib.Order.OrderList OrderList = new MELib.Order.OrderList();

				Order.UserID = UserID;
				Order.BasketID = BasketList.Select(c => c.BasketID).FirstOrDefault();
				Order.DeliveryOptionTypeID = DeliveryOptionTypeID;
				if (DeliveryOptionTypeID == 1)
				{
					Order.Amount = Total + 50;
				}
				else
				{
					Order.Amount = Total;
				}

				Order.IsActiveInd = true;

				OrderList.Add(Order);
				OrderList.TrySave();

				//Clearing the basket
				BasketList = MELib.Basket.BasketList.GetBasketList(UserID);
				BasketList.ToList().ForEach(c => { c.IsActiveInd = false; });
				BasketList.TrySave();

				//Making a transaction for the order or basket
				MELib.Accounts.Transaction Transaction = new MELib.Accounts.Transaction();
				MELib.Accounts.TransactionList TransactionList = new MELib.Accounts.TransactionList();

				if (AccBalance >= Total)
				{
					OrderList = MELib.Order.OrderList.GetOrderList();
					Transaction.UserID = UserID;
					Transaction.OrderID = OrderList.Select(c => c.OrderID).LastOrDefault();
					Transaction.TransactionTypeID = 1;
					Transaction.Amount = Order.Amount;
					Transaction.IsActiveInd = true;
					Transaction.Date = Transaction.CreatedDate;
					Transaction.TrySave(typeof(MELib.Accounts.TransactionList));

					//Email function
					var GetEmail = MELib.RO.ROUserList.GetROUserList(UserID);
					var email = GetEmail.GetItem(UserID);
					MailMessage message = new MailMessage("kutoanempho@gmail.com", email.EmailAddress, "Order Confirmation", "Your order and transaction of R" + Transaction.Amount + " was successful! Thank you for shopping with us. " + email.FullName);

					SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
					client.EnableSsl = true;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.UseDefaultCredentials = false;
					client.Credentials = new System.Net.NetworkCredential("kutoanempho@gmail.com", "Mpho95!k");
					client.Send(message);

					sr.Success = true;
					
				}
				else
				{
					sr.ErrorText = "Insufficent funds";
					sr.Success = false;
				
				}
			}
			catch (Exception e)
            {
				sr.ErrorText = e.Message;
				sr.Success = false;
				
			}
			return sr;

		}




	}
}