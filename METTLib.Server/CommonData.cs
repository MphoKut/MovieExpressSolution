using System;
using Singular.CommonData;

namespace MELib
{
  public class CommonData : CommonDataBase<MELib.CommonData.MECachedLists>
  {
    [Serializable]
    public class MECachedLists : CommonDataBase<MECachedLists>.CachedLists
    {
      /// <summary>
      /// Gets cached ROUserList
      /// </summary>
      public MELib.RO.ROUserList ROUserList
      {
        get
        {
          return RegisterList<MELib.RO.ROUserList>(Misc.ContextType.Application, c => c.ROUserList, () => { return MELib.RO.ROUserList.GetROUserList(); });
        }
      }
      /// <summary>
      /// Gets cached ROMovieGenreList
      /// </summary>
      public RO.ROMovieGenreList ROMovieGenreList
      {
        get
        {
          return RegisterList<MELib.RO.ROMovieGenreList>(Misc.ContextType.Application, c => c.ROMovieGenreList, () => { return MELib.RO.ROMovieGenreList.GetROMovieGenreList(); });
        }
      }    
      /// <summary>
      /// Gets cached ROMovieGenreList
      /// </summary>
      public Maintenance.CategoryList CategoryList
      {
        get
        {
          return RegisterList<MELib.Maintenance.CategoryList>(Misc.ContextType.Application, c => c.CategoryList, () => { return MELib.Maintenance.CategoryList.GetCategoryList(); });
        }                                
      }
            public Basket.DeliveryOptionTypeList DeliveryOptionTypeList
            {
                get
                {
                    return RegisterList<MELib.Basket.DeliveryOptionTypeList>(Misc.ContextType.Application, c => c.DeliveryOptionTypeList, () => { return MELib.Basket.DeliveryOptionTypeList.GetDeliveryOptionTypeList(); });
                }
            }
            public Account.AccountTypeList AccountTypeList
            {
                get
                {
                    return RegisterList<MELib.Account.AccountTypeList>(Misc.ContextType.Application, c => c.AccountTypeList, () => { return MELib.Account.AccountTypeList.GetAccountTypeList(); });
                }
            }

            public Maintenance.SupplierList SupplierList
            {
                get
                {
                    return RegisterList<MELib.Maintenance.SupplierList>(Misc.ContextType.Application, c => c.SupplierList, () => { return MELib.Maintenance.SupplierList.GetSupplierList(); });
                }
            }
            //public DeliveryOption.DeliveryOptionTypeList DeliveryOptionsTypeList
            //{
            //    get
            //    {
            //        return RegisterList<MELib.DeliveryOption.DeliveryOptionTypeList>(Misc.ContextType.Application, c => c.DeliveryOptionsTypeList, () => { return MELib.DeliveryOption.DeliveryOptionTypeList.GetDeliveryOptionTypeList(); });
            //    }
            //}

        }
  }

  public class Enums
  {
		public enum AuditedInd
		{
			Yes = 1,
			No = 0
		}
    public enum DeletedInd
    {
      Yes = 1,
      No = 0
    }
  }
}
