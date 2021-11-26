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

namespace MEWeb.Category
{
    public partial class Category : MEPageBase<CategoryVM>
    {
    }

    public class CategoryVM : MEStatelessViewModel<CategoryVM>
    {
        public MELib.Maintenance.CategoryList CategoryList { get; set; }
        public CategoryVM()
        {

        }
        protected override void Setup()
            {
                base.Setup();
			CategoryList = MELib.Maintenance.CategoryList.GetCategoryList();
            }

        [WebCallable]
		public Result SaveCategoryList(MELib.Maintenance.CategoryList CategoryList)
		{
			Result sr = new Result();
			if (CategoryList.IsValid)
			{
				var SaveResult = CategoryList.TrySave();
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
				sr.ErrorText = CategoryList.GetErrorsAsHTMLString();
				return sr;
			}
		}
	}
    
    
}