﻿// Generated 20 Oct 2021 07:34 - Singular Systems Object Generator Version 2.2.694
//<auto-generated/>
using System;
using Csla;
using Csla.Serialization;
using Csla.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singular;
using System.Data;
using System.Data.SqlClient;


namespace MELib.Maintenance
{
    [Serializable]
    public class CategoryList
     : MEBusinessListBase<CategoryList, Category>
    {
        #region " Business Methods "

        public Category GetItem(int CategoryID)
        {
            foreach (Category child in this)
            {
                if (child.CategoryID == CategoryID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Categorys";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public Criteria()
            {
            }

        }

        public static CategoryList NewCategoryList()
        {
            return new CategoryList();
        }

        public CategoryList()
        {
            // must have parameter-less constructor
        }

        public static CategoryList GetCategoryList()
        {
            return DataPortal.Fetch<CategoryList>(new Criteria());
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(Category.GetCategory(sdr));
            }
            this.RaiseListChangedEvents = true;
        }

        protected override void DataPortal_Fetch(Object criteria)
        {
            Criteria crit = (Criteria)criteria;
            using (SqlConnection cn = new SqlConnection(Singular.Settings.ConnectionString))
            {
                cn.Open();
                try
                {
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "GetProcs.getCategoryList";
                        using (SafeDataReader sdr = new SafeDataReader(cm.ExecuteReader()))
                        {
                            Fetch(sdr);
                        }
                    }
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        #endregion

    }

}