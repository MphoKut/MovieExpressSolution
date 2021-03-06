// Generated 28 May 2018 08:55 - Singular Systems Object Generator Version 2.2.694
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
	public class ROProvince
	 : METTReadOnlyBase<ROProvince>
	{
		#region " Properties and Methods "

		#region " Properties "

		public static PropertyInfo<int> ProvinceIDProperty = RegisterProperty<int>(c => c.ProvinceID, "ID", 0);
		/// <summary>
		/// Gets the ID value
		/// </summary>
		[Display(AutoGenerateField = false), Key]
		public int ProvinceID
		{
			get { return GetProperty(ProvinceIDProperty); }
		}

		public static PropertyInfo<String> ProvinceProperty = RegisterProperty<String>(c => c.Province, "Province", "");
		/// <summary>
		/// Gets the Province value
		/// </summary>
		[Display(Name = "Province", Description = "")]
		public String Province
		{
			get { return GetProperty(ProvinceProperty); }
		}

		public static PropertyInfo<int?> CountryIDProperty = RegisterProperty<int?>(c => c.CountryID, "Country", null);
		/// <summary>
		/// Gets the Country value
		/// </summary>
		[Display(Name = "Country", Description = "")]
		public int? CountryID
		{
			get { return GetProperty(CountryIDProperty); }
		}

		public static PropertyInfo<Boolean> IsActiveIndProperty = RegisterProperty<Boolean>(c => c.IsActiveInd, "Is Active", true);
		/// <summary>
		/// Gets the Is Active value
		/// </summary>
		[Display(Name = "Is Active", Description = "Indicates whether question is currently active")]
		public Boolean IsActiveInd
		{
			get { return GetProperty(IsActiveIndProperty); }
		}

		public static PropertyInfo<int> CreatedByProperty = RegisterProperty<int>(c => c.CreatedBy, "Created By", 0);
		/// <summary>
		/// Gets the Created By value
		/// </summary>
		[Display(AutoGenerateField = false)]
		public int? CreatedBy
		{
			get { return GetProperty(CreatedByProperty); }
		}

		public static PropertyInfo<SmartDate> CreatedDateTimeProperty = RegisterProperty<SmartDate>(c => c.CreatedDateTime, "Created Date Time", new SmartDate(DateTime.Now));
		/// <summary>
		/// Gets the Created Date Time value
		/// </summary>
		[Display(AutoGenerateField = false)]
		public SmartDate CreatedDateTime
		{
			get { return GetProperty(CreatedDateTimeProperty); }
		}

		public static PropertyInfo<int> ModifiedByProperty = RegisterProperty<int>(c => c.ModifiedBy, "Modified By", 0);
		/// <summary>
		/// Gets the Modified By value
		/// </summary>
		[Display(AutoGenerateField = false)]
		public int? ModifiedBy
		{
			get { return GetProperty(ModifiedByProperty); }
		}

		public static PropertyInfo<SmartDate> ModifiedDateTimeProperty = RegisterProperty<SmartDate>(c => c.ModifiedDateTime, "Modified Date Time", new SmartDate(DateTime.Now));
		/// <summary>
		/// Gets the Modified Date Time value
		/// </summary>
		[Display(AutoGenerateField = false)]
		public SmartDate ModifiedDateTime
		{
			get { return GetProperty(ModifiedDateTimeProperty); }
		}

		#endregion

		#region " Methods "

		protected override object GetIdValue()
		{
			return GetProperty(ProvinceIDProperty);
		}

		public override string ToString()
		{
			return this.Province;
		}

		#endregion

		#endregion

		#region " Data Access & Factory Methods "

		internal static ROProvince GetROProvince(SafeDataReader dr)
		{
			var r = new ROProvince();
			r.Fetch(dr);
			return r;
		}

		protected void Fetch(SafeDataReader sdr)
		{
			int i = 0;
			LoadProperty(ProvinceIDProperty, sdr.GetInt32(i++));
			LoadProperty(ProvinceProperty, sdr.GetString(i++));
			LoadProperty(CountryIDProperty, Singular.Misc.ZeroNothing(sdr.GetInt32(i++)));
			LoadProperty(IsActiveIndProperty, sdr.GetBoolean(i++));
			LoadProperty(CreatedByProperty, sdr.GetInt32(i++));
			LoadProperty(CreatedDateTimeProperty, sdr.GetSmartDate(i++));
			LoadProperty(ModifiedByProperty, sdr.GetInt32(i++));
			LoadProperty(ModifiedDateTimeProperty, sdr.GetSmartDate(i++));
		}

		#endregion

	}

}