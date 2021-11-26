using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;

namespace MEWeb.Movies
{
  public partial class Movie : MEPageBase<MovieVM>
  {
  }
  public class MovieVM : MEStatelessViewModel<MovieVM>
  {

        public MELib.Movies.MovieList MovieList { get; set; }
        public MELib.Movies.UserMovieList UserMovieList { get; set; }
        

        public int MovieID { get; set; }
        public MovieVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
           MovieID = System.Convert.ToInt32(Page.Request.QueryString[0]);
            UserMovieList = MELib.Movies.UserMovieList.GetUserMovieList();
            MovieList = MELib.Movies.MovieList.GetMovieList(null,MovieID);



        }
        public Result RentNow(int MovieID)
        {
            Result sr = new Result();
            try {
            // ToDo Check User Balance
            decimal Price;
            var AccBalance = MELib.Accounts.AccountList.GetAccountList().Select(c => c.Balance).FirstOrDefault();
            Price = Price = MELib.Movies.MovieList.GetMovieList().Where(c => c.MovieID == MovieID).Select(c => c.Price).FirstOrDefault();
            var NewBalance = AccBalance - Price;

            if (AccBalance >= Price)
            {
                var newBalance = MELib.Accounts.AccountList.GetAccountList(Singular.Security.Security.CurrentIdentity.UserID).FirstOrDefault();
                newBalance.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                newBalance.Balance = NewBalance;
                newBalance.TrySave(typeof(MELib.Accounts.AccountList));

                   

                    MELib.Movies.UserMovie UserMovie = new MELib.Movies.UserMovie();
                    MELib.Movies.UserMovieList UserMovieList = new MELib.Movies.UserMovieList();

                    UserMovie.MovieID = MovieID;
                    UserMovie.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                    UserMovie.WatchedDate = UserMovie.CreatedDate;
                    UserMovie.IsActiveInd = true;
                    UserMovieList.Add(UserMovie);
                   var mpho= UserMovieList.TrySave();
                // ToDo Insert Data in Transctions
                MELib.Accounts.Transaction Transaction = new MELib.Accounts.Transaction();
                MELib.Accounts.TransactionList TransactionList = new MELib.Accounts.TransactionList();

                Transaction.UserID = Singular.Security.Security.CurrentIdentity.UserID;
                Transaction.TransactionTypeID = 6;
                Transaction.Amount = Price;
                Transaction.IsActiveInd = true;
                Transaction.Date = Transaction.CreatedDate;
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
                 return sr;
            }
             

            



        }
    }
}

