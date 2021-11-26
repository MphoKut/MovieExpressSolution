﻿// Generated 20 Jan 2021 07:53 - Singular Systems Object Generator Version 2.2.694
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


namespace MELib.Accounts
{
  [Serializable]
  public class AccountList
   : MEBusinessListBase<AccountList, Account>
  {
    #region " Business Methods "

    public Account GetItem(int AccountID)
    {
      foreach (Account child in this)
      {
        if (child.AccountID == AccountID)
        {
          return child;
        }
      }
      return null;
    }

    public override string ToString()
    {
      return "Accounts";
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

      public int? UserID { get; set; }
    }

    public static AccountList NewAccountList()
    {
      return new AccountList();
    }

    public AccountList()
    {
      // must have parameter-less constructor
    }

    public static AccountList GetAccountList()
    {
      return DataPortal.Fetch<AccountList>(new Criteria());
    }
        public static AccountList GetAccountList(int userID)
        {
            return DataPortal.Fetch<AccountList>(new Criteria() { UserID = userID }); ;
        }

        protected void Fetch(SafeDataReader sdr)
    {
      this.RaiseListChangedEvents = false;
      while (sdr.Read())
      {
        this.Add(Account.GetAccount(sdr));
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
            cm.CommandText = "GetProcs.getAccountList";
                        //cm.Parameters.AddWithValue("@MovieID", Singular.Misc.ZeroDBNull(crit.MovieID));
                        cm.Parameters.AddWithValue("@UserID", Singular.Misc.ZeroDBNull(crit.UserID));

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