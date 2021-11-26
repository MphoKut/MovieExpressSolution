using System;
using Singular.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web.Data;
using MELib.RO;
using MELib.Security;
using Singular;
using System.ComponentModel.DataAnnotations;


namespace MEWeb.Product
{
    public partial class ViewProducts : MEPageBase<ViewProductsVM>
    {
    }
    public class ViewProductsVM : MEStatelessViewModel<ViewProductsVM>
    {
        public MELib.Maintenance.ProductList ProductList { get; set; }
        public MELib.Basket.BasketList BasketList { get; set; }
        public MELib.Basket.BasketEntryList BasketEntryList { get; set; }
        public MELib.Basket.BasketEntry Item { get; set; }
        public MELib.Basket.Basket Basket { get; set; }
        public int? ProductID { get; set; }
        public int UserID { get; set; }
        public int? BasketRntryID { get; set; }
        public decimal Total { get; set; }
        public bool IsActiveInd { get; set; }

        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.Maintenance.CategoryList), UnselectedText = "Select", ValueMember = "CategoryID", DisplayMember = "CategoryName")]
        [Display(Name = "CategoryName")]

        public int? CategoryID { get; set; } 
        public ViewProductsVM()
        {

        }
        protected override void Setup()
        {
            base.Setup();
            UserID = Singular.Security.Security.CurrentIdentity.UserID;
            ProductList = MELib.Maintenance.ProductList.GetProductList(true);
            BasketList = MELib.Basket.BasketList.GetBasketList();
            BasketEntryList = MELib.Basket.BasketEntryList.GetBasketEntryList();
            Item = BasketEntryList.FirstOrDefault();
            Basket = BasketList.FirstOrDefault();
            Total = MELib.Basket.BasketList.GetBasketList(UserID).Sum(c => c.FinalAmount);

        }

        [WebCallable]

       
        public Result AddToBasket( int ProductID, MELib.Maintenance.ProductList ProductList, int UserID)
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
                        Basket.UserID = Singular.Security.Security.CurrentIdentity.UserID;
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