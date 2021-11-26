using Singular.Web.Security;

namespace MEWeb.Maintenance
{
  /// <summary>
  /// The Security page class
  /// </summary>
  public partial class Security : MEPageBase<SecurityVM>
  {
  }

  /// <summary>
  /// The SecurityVM ViewModel class
  /// </summary>
  public class SecurityVM: StatelessSecurityVM
  {
  }
}
