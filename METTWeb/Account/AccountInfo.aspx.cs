using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular;
using Singular.Web;

namespace MEWeb.Account
{
    public partial class AccountInfo : MEPageBase<AccountInfoVM>
    {
       
    }
    public class AccountInfoVM : MEStatelessViewModel<AccountInfoVM>
    {
        public MELib.Accounts.AccountList UserAccountList { get; set; }
        public MELib.Accounts.Account UserAccount { get; set; }
        public MELib.RO.ROUserList UserList { get; set; }
        public MELib.RO.ROUser Account { get; set; }
        public MELib.Security.User EditingUser { get; set; }
        public MELib.Basket.BasketList BasketList { get; set; }
        public MELib.Maintenance.ProductList ProductList { get; set; }
        public int? ProductID { get; set; }
        public decimal Total { get; set; }
        public int UserID { get; set; }
        public MELib.Accounts.Account EditAccount { get; set; }

        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.Maintenance.CategoryList), UnselectedText = "Select", ValueMember = "CategoryID", DisplayMember = "CategoryName")]
        public int? CategoryID { get; set; }
        public AccountInfoVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            UserID = System.Convert.ToInt32(Page.Request.QueryString[0]);
            UserList = MELib.RO.ROUserList.GetROUserList(UserID);
            Account = UserList.GetItem(UserID);
            BasketList = MELib.Basket.BasketList.GetBasketList(UserID,0,0);
            ProductList = MELib.Maintenance.ProductList.GetProductList(true);
            UserAccountList = MELib.Accounts.AccountList.GetAccountList(UserID);
            UserAccount = UserAccountList.FirstOrDefault();

            Total = MELib.Basket.BasketList.GetBasketList(UserID,0,0).Sum(c => c.FinalAmount);

            
        }

        [WebCallable]


        public Result AddToBasket(int ProductID, MELib.Maintenance.ProductList ProductList, int UserID)
        {
            Result sr = new Result();
            var ProdCount = ProductList.GetItem(ProductID).ProdCount;
            if (ProdCount > 0)
            {
                try
                {
                    //getting data for a specific product
                    var Product = MELib.Maintenance.ProductList.GetProductList(0, ProductID).FirstOrDefault();
                    var Amt = Product.Price;
                    var StockCount = Product.Quantity;

                    //getting data to update a basket entry if it exists
                    var BasketItem = MELib.Basket.BasketList.GetBasketList(UserID, 0, ProductID).FirstOrDefault();
                    int BasketID;

                    if (BasketItem != null)
                    {
                        BasketID = BasketItem.BasketID;
                    }
                    else
                    {
                        BasketID = 0;
                    }

                    var BasketExist = MELib.Basket.BasketList.GetBasketList(UserID, BasketID, 0).FirstOrDefault();


                    //Adding the product to the basket
                    MELib.Basket.Basket Basket = new MELib.Basket.Basket();



                    if (BasketExist != null && ProductID == BasketExist.ProductID)
                    {
                        BasketExist.ItemsCount += ProdCount;
                        BasketExist.FinalAmount = Amt * BasketExist.ItemsCount;


                    }
                    else
                    {
                        Basket.ItemsCount = ProdCount;
                        Basket.ProductID = ProductID;
                        Basket.IsActiveInd = true;
                        Basket.UserID = UserID;
                        Basket.FinalAmount = Amt * Basket.ItemsCount;
                    }



                    if (StockCount >= Basket.ItemsCount || StockCount >= BasketExist.ItemsCount)
                    {

                        Basket.TrySave(typeof(MELib.Basket.BasketList));
                        if (BasketExist != null)
                        {
                            BasketExist.TrySave(typeof(MELib.Basket.BasketList));
                        }

                        //Updating the product quantity
                        var UpdateProduct = MELib.Maintenance.ProductList.GetProductList().GetItem(ProductID);
                        UpdateProduct.Quantity -= ProdCount;
                        UpdateProduct.TrySave(typeof(MELib.Maintenance.ProductList));
                        sr.Success = true;


                    }
                    else
                    {
                        sr.ErrorText = "Not enough in stock";
                        sr.Success = false;

                    }
                }
                catch
                {
                    sr.ErrorText = "Not enough in stock";
                    sr.Success = false;

                }
            }
            else
            {
                sr.ErrorText = "Quantity needs to be greater then zero";
                sr.Success = false;

            }
            return sr;

        }
        [WebCallable]
        public string ViewBasket(int UserID)
        {
            try
            {
                var url = VirtualPathUtility.ToAbsolute("~/Account/UserBasket.aspx?UserID=" + HttpUtility.UrlEncode((UserID.ToString())));
                return url;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
        [WebCallable]
        public string ViewTransactions(int UserID)
        {
            try
            {
                var url = VirtualPathUtility.ToAbsolute("~/Profile/UserTransactions.aspx?UserID=" + HttpUtility.UrlEncode((UserID.ToString())));
                return url;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public Result FilterProducts(int CategoryID, int ResetInd)
        {
            Result sr = new Result();
            try
            {
                if (ResetInd == 0)
                {
                    MELib.Maintenance.ProductList ProductList = MELib.Maintenance.ProductList.GetProductList(CategoryID, 0);
                    sr.Data = ProductList;
                }
                else
                {
                    MELib.Maintenance.ProductList ProductList = MELib.Maintenance.ProductList.GetProductList();
                    sr.Data = ProductList;
                }
                sr.Success = true;
            }
            catch (Exception e)
            {
                WebError.LogError(e, "Page: ViewProducts.aspx | Method: FilterProducts", $"(int CategoryID, ({CategoryID})");
                sr.Data = e.InnerException;
                sr.ErrorText = "Could not filter products by category.";
                sr.Success = false;
            }
            return sr;
        }
    }
}