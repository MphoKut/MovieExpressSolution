using System.Web;
using System.Web.UI;
using Singular.Web;
using Singular.Web.Security;
using MELib.Security;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System;
using System.Linq;
using Singular.Web.Data;

namespace MEWeb.Account
{
  public partial class Login: MEPageBase<LoginVM>
  {
  }

  public class LoginVM: MEStatelessViewModel<LoginVM>
  {
		public PagedDataManager<LoginVM> UserListPageManager { get; set; }

		/// <summary>
		/// User Criteria
		/// </summary>
		public ROUserPagedList.Criteria UserCriteria { get; set; }

		/// <summary>
		/// User List
		/// </summary>
		public ROUserPagedList UserList { get; set; }


		/// <summary>
		/// The Editing User
		/// </summary>
		public MELib.Security.User EditingUser { get; set; }

		/// <summary>
		/// The login details
		/// </summary>
		public LoginDetails LoginDetails { get; set; }
		public LoginVM()
        {
			this.UserListPageManager = new PagedDataManager<LoginVM>((c) => c.UserList, (c) => c.UserCriteria, "UserName", 20);
			this.UserCriteria = new ROUserPagedList.Criteria();
			this.UserList = new ROUserPagedList();
		}
    /// <summary>
    /// The location to redirect to after login
    /// </summary>
    public string RedirectLocation { get; set; }
		public bool ShowForgotPasswordInd { get; set; }

		[Display(Name = "Enter your email address", Description = "")]
		public string ForgotEmail { get; set; }
    /// <summary>
    /// Setup the Login ViewModel
    /// </summary>
    protected override void Setup()
    {
      base.Setup();

      this.LoginDetails = new LoginDetails();
	  this.ShowForgotPasswordInd = false;
      this.ValidationMode = ValidationMode.OnSubmit;
      this.ValidationDisplayMode = ValidationDisplayMode.Controls;
		this.ValidationDisplayMode = ValidationDisplayMode.Controls | ValidationDisplayMode.SubmitMessage;

		this.UserList = (ROUserPagedList)UserListPageManager.GetInitialData();

			this.RedirectLocation = VirtualPathUtility.ToAbsolute(Security.GetSafeRedirectUrl(Page.Request.QueryString["ReturnUrl"], "~/default.aspx"));
    }


		/// <summary>
		/// Check the login details
		/// </summary>
		//<param name="loginDetails">Login details</param>
		/// <returns>True if the login details are valid</returns>
		[WebCallable(LoggedInOnly = false)]
		public Result Login(LoginDetails loginDetails)
		{
			Result ret = new Result();

			try
			{
				MEIdentity.Login(loginDetails);
				ret.Success = true;
                

            }
			catch
			{
				ret.ErrorText = "";
				ret.Success = false;
			}

			return ret;
		}

		[WebCallable(LoggedInOnly = false)]
		public Result ResetPassword(string Email)
		{
			Result ret = new Result();
			try
			{
				MELib.Security.User.ResetPassword(Email);
				ret.Success = true;
			}
			catch (Exception ex)
			{
				ret.Success = false;
				ret.ErrorText = ex.Message;
			}
			return ret;
		}
        [WebCallable]
        public static MELib.Security.User GetUser(int userId)
        {
            return MELib.Security.UserList.GetUserList(userId).First();
        }

        /// <summary>
        /// Save changes to a user
        /// </summary>
        /// <param name="user">A user instance</param>
        /// <returns>The save result</returns>
        [WebCallable(LoggedInOnly = false)]
        public static Result SaveUser(MELib.Security.User user)
        {
            Result results = new Singular.Web.Result();
            try
            {
                if (user.SecurityGroupUserList.Count == 0)
                {
                    //add a default security group of General User
                    Singular.Security.SecurityGroupUser securityGroupUser = Singular.Security.SecurityGroupUser.NewSecurityGroupUser();
                    securityGroupUser.SecurityGroupID = ROSecurityGroupList.GetROSecurityGroupList(true).FirstOrDefault(c => c.SecurityGroup == "General User")?.SecurityGroupID;
                    user.SecurityGroupUserList.Add(securityGroupUser);
                }

                user.LoginName = user.EmailAddress;

                



                MELib.Security.User SavedUser = new MELib.Security.User();
                MELib.Security.UserList UserList = new MELib.Security.UserList();

                SavedUser.FirstName = user.FirstName;
                SavedUser.Surname = user.Surname;
                SavedUser.Password = "p";
                SavedUser.LoginName = user.LoginName;
                SavedUser.EmailAddress = user.EmailAddress;


                if (SavedUser != null)
                {
                    results.Success = true;
                    results.Data = SavedUser;
                    UserList.Add(SavedUser);
                    UserList.TrySave();
                }
                else
                {
                    results.Success = false;
                    results.ErrorText = "User not saved";
                }
            }
            catch 
            {
                results.Success = false;
                results.ErrorText = "User not saved";
            }
            return results;
        }


      
    }
}
