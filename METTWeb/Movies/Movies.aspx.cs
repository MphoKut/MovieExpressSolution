using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Singular.Web;

namespace MEWeb.Movies
{
  public partial class Movies : MEPageBase<MoviesVM>
  {
  }
  public class MoviesVM : MEStatelessViewModel<MoviesVM>
  {
    public MELib.Movies.MovieList MovieList { get; set; }
    public MELib.Accounts.AccountList AccountLIst { get; set; }

        // Filter Criteria
        public DateTime ReleaseFromDate { get; set; }
    public DateTime ReleaseToDate { get; set; }

    /// <summary>
    /// Gets or sets the Movie Genre ID
    /// </summary>
    [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROMovieGenreList), UnselectedText = "Select", ValueMember = "MovieGenreID", DisplayMember = "Genre")]
    [Display(Name = "Genre")]
    public int? MovieGenreID { get; set; }

    public MoviesVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();

      MovieList = MELib.Movies.MovieList.GetMovieList();
      AccountLIst = MELib.Accounts.AccountList.GetAccountList();
    }

    [WebCallable(LoggedInOnly = true)]
    public string RentMovie(int MovieID)
    {
            try
            {
                var url = VirtualPathUtility.ToAbsolute("~/Movies/Movie.aspx?MovieID=" + HttpUtility.UrlEncode((MovieID.ToString())));
                return url;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    [WebCallable]
    public static Result WatchMovie(int MovieID)
    {
      Result sr = new Result();
      try
      {

                // ToDo Check User Balance
                decimal Price;
                var AccBalance = MELib.Accounts.AccountList.GetAccountList().Select(c => c.Balance).FirstOrDefault();
                Price = MELib.Movies.MovieList.GetMovieList().Select(c=> c.Price).FirstOrDefault();
                var NewBalance = AccBalance - Price;

                if (AccBalance >= Price)
                {
                    var newBalance = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
                    newBalance.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    newBalance.Balance = NewBalance;
                    newBalance.TrySave(typeof(MELib.Accounts.AccountList));
                    
                    // ToDo Insert Data in Transctions
                    MELib.Accounts.Transaction Transaction = new MELib.Accounts.Transaction();
                    MELib.Accounts.TransactionList TransactionList = new MELib.Accounts.TransactionList();

                    Transaction.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    Transaction.TransactionTypeID = 6;
                    Transaction.Amount = Price;
                    Transaction.IsActiveInd = true;
                    Transaction.TrySave(typeof(MELib.Accounts.TransactionList));
                    sr.Success = true;
                    return sr;


                }
                else
                {
                    sr.Success = false;
                    return sr;
                }
      }
      catch (Exception e)
      {
        sr.Data = e.InnerException;
        sr.Success = false;
      }
      return sr;
    }

    [WebCallable]
    public Result FilterMovies(int MovieGenreID)
    {
      Result sr = new Result();
      try
      {
        sr.Data = MELib.Movies.MovieList.GetMovieList(MovieGenreID,0);
        sr.Success = true;
      }
      catch (Exception e)
      {
        WebError.LogError(e, "Page: LatestReleases.aspx | Method: FilterMovies", $"(int MovieGenreID, ({MovieGenreID})");
        sr.Data = e.InnerException;
        sr.ErrorText = "Could not filter movies by category.";
        sr.Success = false;
      }
      return sr;
    }

  }
}

