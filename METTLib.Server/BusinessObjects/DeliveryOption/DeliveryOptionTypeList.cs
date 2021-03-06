// Generated 15 Nov 2021 11:23 - Singular Systems Object Generator Version 2.2.694
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


namespace MELib.DeliveryOption
{
    [Serializable]
    public class DeliveryOptionTypeList
     : SingularBusinessListBase<DeliveryOptionTypeList, DeliveryOptionType>
    {
        #region " Business Methods "

        public DeliveryOptionType GetItem(int DeliveryOptionTypeID)
        {
            foreach (DeliveryOptionType child in this)
            {
                if (child.DeliveryOptionTypeID == DeliveryOptionTypeID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Delivery Option Types";
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

        public static DeliveryOptionTypeList NewDeliveryOptionTypeList()
        {
            return new DeliveryOptionTypeList();
        }

        public DeliveryOptionTypeList()
        {
            // must have parameter-less constructor
        }

        public static DeliveryOptionTypeList GetDeliveryOptionTypeList()
        {
            return DataPortal.Fetch<DeliveryOptionTypeList>(new Criteria());
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(DeliveryOptionType.GetDeliveryOptionType(sdr));
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
                        cm.CommandText = "GetProcs.getDeliveryOptionTypeList";
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