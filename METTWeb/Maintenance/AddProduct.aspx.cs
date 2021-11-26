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

namespace MEWeb.Maintenance
{
    public partial class AddProduct : MEPageBase<ProductsVM>
    {
    }
    public class ProductsVM : MEStatelessViewModel<ProductsVM>
    {
        public MELib.Maintenance.ProductList ProductList { get; set; }
		[Singular.DataAnnotations.DropDownWeb(typeof(MELib.Maintenance.CategoryList), UnselectedText = "Select", ValueMember = "CategoryID", DisplayMember = "CategoryName")]
		[Display(Name = "CategoryName")]

		public int? CategoryID { get; set; }

		public ProductsVM()
        {
        }
        protected override void Setup()
        {
            base.Setup();
            ProductList = MELib.Maintenance.ProductList.GetProductList();
        }

		



		[WebCallable]
		public Result SaveProductList(MELib.Maintenance.ProductList ProductList)
		{
			Result sr = new Result();
			
			if (ProductList.IsValid)

			{
				var Product = MELib.Maintenance.ProductList.GetProductList().FirstOrDefault();
				Product.IsActiveInd = true;
				var SaveResult = ProductList.TrySave();
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
				sr.ErrorText = ProductList.GetErrorsAsHTMLString();
				return sr;
			}
		}
	}
}