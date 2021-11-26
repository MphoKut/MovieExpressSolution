﻿// Generated 22 Jan 2019 08:52 - Singular Systems Object Generator Version 2.2.694
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


namespace METTLib.RO
{
  [Serializable]
  public class ROLanguage
   : METTReadOnlyBase<ROLanguage>
  {
    #region " Properties and Methods "

    #region " Properties "

    public static PropertyInfo<int> LanguageIDProperty = RegisterProperty<int>(c => c.LanguageID, "ID", 0);
    /// <summary>
    /// Gets the ID value
    /// </summary>
    [Display(AutoGenerateField = false), Key]
    public int LanguageID
    {
      get { return GetProperty(LanguageIDProperty); }
    }

    public static PropertyInfo<String> LanguageProperty = RegisterProperty<String>(c => c.Language, "Language", "");
    /// <summary>
    /// Gets the Language value
    /// </summary>
    [Display(Name = "Language", Description = "Language description")]
    public String Language
    {
      get { return GetProperty(LanguageProperty); }
    }

    public static PropertyInfo<String> CultureCodeProperty = RegisterProperty<String>(c => c.CultureCode, "Culture Code", "");
    /// <summary>
    /// Gets the Culture Code value
    /// </summary>
    [Display(Name = "Culture Code", Description = "")]
    public String CultureCode
    {
      get { return GetProperty(CultureCodeProperty); }
    }

    #endregion

    #region " Methods "

    protected override object GetIdValue()
    {
      return GetProperty(LanguageIDProperty);
    }

    public override string ToString()
    {
      return this.Language;
    }

    #endregion

    #endregion

    #region " Data Access & Factory Methods "

    internal static ROLanguage GetROLanguage(SafeDataReader dr)
    {
      var r = new ROLanguage();
      r.Fetch(dr);
      return r;
    }

    protected void Fetch(SafeDataReader sdr)
    {
      int i = 0;
      LoadProperty(LanguageIDProperty, sdr.GetInt32(i++));
      LoadProperty(LanguageProperty, sdr.GetString(i++));
      LoadProperty(CultureCodeProperty, sdr.GetString(i++));
    }

    #endregion

  }

}