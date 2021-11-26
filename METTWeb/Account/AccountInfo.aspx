<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="MEWeb.Account.AccountInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%
        using (var h = this.Helpers)
        {

            var MainHDiv = h.DivC("row pad-top-10");
            {
                var PanelContainer = MainHDiv.Helpers.DivC("col-md-12 p-n-lr");
                {
                    var HomeContainer = PanelContainer.Helpers.DivC("tabs-container");
                    {
                        var AssessmentsTab = HomeContainer.Helpers.TabControl();
                        {
                            AssessmentsTab.Style.ClearBoth();
                            AssessmentsTab.AddClass("nav nav-tabs");
                            var HomeContainerTab = AssessmentsTab.AddTab("Customer");
                            {
                                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                                {
                                    var RowCol = Row.Helpers.DivC("col-md-9");
                                    {
                                        RowCol.Helpers.HTML().Heading3("User Information");
                                        var UserInfo = RowCol.Helpers.BootstrapTableFor<MELib.RO.ROUser>(c => c.Account, false, false, "");
                                        var UserInfoRow = UserInfo.FirstRow;
                                        {
                                            UserInfoRow.AddReadOnlyColumn(c => c.FirstName, 150);
                                            UserInfoRow.AddReadOnlyColumn(c => c.LastName, 150);
                                            UserInfoRow.AddReadOnlyColumn(c => c.UserName, 150);
                                            UserInfoRow.AddReadOnlyColumn(c => c.EmailAddress, 150);
                                        }
                                        var TransBtn = RowCol.Helpers.Button("User Transactions", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.money);
                                        {
                                            TransBtn.AddClass("btn btn-primary btn-outline");
                                            TransBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "ViewTransactions($data)");
                                        }


                                        RowCol.Helpers.HTML().Heading3("Available Products");

                                        var ProductList = RowCol.Helpers.BootstrapTableFor<MELib.Maintenance.Product>((c) => c.ProductList, false, false, "");
                                        var BasketList = RowCol.Helpers.BootstrapTableFor<MELib.Basket.Basket>((c) => c.BasketList, false, false, "");
                                        {
                                            var ProductListRow = ProductList.FirstRow;
                                            var BasketListRow = BasketList.FirstRow;
                                            {


                                                ProductListRow.AddReadOnlyColumn(c => c.ProductName, 150);
                                                ProductListRow.AddReadOnlyColumn(c => c.Price, 150);

                                                var Quantity = ProductListRow.AddColumn("Quantity");
                                                {
                                                    Quantity.Style.Width = "100px";
                                                    var QuantitText = Quantity.Helpers.EditorFor(c => c.ProdCount);
                                                }
                                                var BaskBtn = ProductListRow.AddColumn("");
                                                {
                                                    var BasketBtn = BaskBtn.Helpers.Button("Add to Basket", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shopping_basket);
                                                    {
                                                        BasketBtn.AddClass("btn btn-primary btn-outline");
                                                        BasketBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "AddToBasket($data)");
                                                    }
                                                }



                                                {

                                                    {
                                                        var RowColRight = Row.Helpers.DivC("col-md-3");
                                                        {

                                                            var AnotherCardDiv = RowColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                                            {
                                                                var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                                                {
                                                                    CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                                    CardTitleDiv.Helpers.HTML().Heading5("User Account Info");
                                                                }
                                                                var CardTitleToolsDiv = CardTitleDiv.Helpers.DivC("ibox-tools");
                                                                {
                                                                    var aToolsTag = CardTitleToolsDiv.Helpers.HTMLTag("a");
                                                                    aToolsTag.AddClass("collapse-link");
                                                                    {
                                                                        var iToolsTag = aToolsTag.Helpers.HTMLTag("i");
                                                                        iToolsTag.AddClass("fa fa-chevron-up");
                                                                    }
                                                                }
                                                                var ContentDiv = AnotherCardDiv.Helpers.DivC("ibox-content");
                                                                {
                                                                    var RowContentDiv = ContentDiv.Helpers.DivC("row");
                                                                    {

                                                                        var AccountContentDiv = RowContentDiv.Helpers.DivC("col-md-12");
                                                                        {
                                                                            var AccountDiv = AccountContentDiv.Helpers.With<MELib.Accounts.Account>(c => c.UserAccount);
                                                                            {


                                                                                AccountDiv.Helpers.HTML("Account Balance: ");
                                                                                AccountDiv.Helpers.Span(c => "R" + c.Balance);
                                                                                AccountDiv.Helpers.HTML("<br> </br>");



                                                                                AccountDiv.Helpers.HTML("Account Type: ");
                                                                                AccountDiv.Helpers.Span(c => c.AccountType);
                                                                                AccountDiv.Helpers.HTML("<br> </br>");

                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        var RowColLeft = Row.Helpers.DivC("col-md-3");
                                                        {

                                                            var AnotherCardDiv = RowColLeft.Helpers.DivC("ibox float-e-margins paddingBottom");
                                                            {
                                                                var CardTitleDiv = AnotherCardDiv.Helpers.DivC("ibox-title");
                                                                {
                                                                    CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                                    CardTitleDiv.Helpers.HTML().Heading5("Basket Amount");
                                                                }
                                                                var ContentDiv = AnotherCardDiv.Helpers.DivC("ibox-content");
                                                                {
                                                                    var RowContentDiv = ContentDiv.Helpers.DivC("row");
                                                                    {

                                                                        var CategoryContentDiv = RowContentDiv.Helpers.DivC("col-md-12");
                                                                        {
                                                                            var CategoryEditor = CategoryContentDiv.Helpers.Span(c => ViewModel.Total);

                                                                            var BaskPageBtn = CategoryContentDiv.Helpers.DivC("col-md-3");
                                                                            {
                                                                                var BasketViewBtn = BaskPageBtn.Helpers.Button("View User Basket", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shopping_basket);
                                                                                {
                                                                                    BasketViewBtn.AddClass("btn-primary btn btn btn-primary");
                                                                                    BasketViewBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "GoToBasket($data)");
                                                                                }
                                                                            }
                                                                            CategoryEditor.AddClass("form-control marginBottom20 ");

                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }

                            }
                        }
                    }
                }
            }
        }
    %>

    <script type="text/javascript">
        Singular.OnPageLoad(function () {
            $("#menuItem5").addClass("active");
            $("#menuItem5 > ul").addClass("in");
        });
        var AddToBasket = function (obj) {
            ViewModel.CallServerMethod('AddToBasket', { ProductID: obj.ProductID(), ProductList: ViewModel.ProductList.Serialise(), UserID: ViewModel.UserID(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Added to basket", 'center', 'success', 7000);
                    location.reload();
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });
        }

        var GoToBasket = function (obj) {

            //Edit
            ViewModel.CallServerMethod('ViewBasket', { UserID: obj.UserID(), ShowLoadingBar: true }, function (result) {
                window.location = result.Data;
            });

        };
        var ViewTransactions = function (obj) {

            //Edit
            ViewModel.CallServerMethod('ViewTransactions', { UserID: obj.UserID(), ShowLoadingBar: true }, function (result) {
                window.location = result.Data;
            });

        };

        var FilterProducts = function (obj) {
            ViewModel.CallServerMethod('FilterProducts', { CategoryID: obj.CategoryID(), ResetInd: 0, ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Products filtered successfully.", 'center', 'info', 7000);
                    ViewModel.ProductList.Set(result.Data);

                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            })
        };

        var FilterReset = function (obj) {
            ViewModel.CallServerMethod('FilterProducts', { CategoryID: obj.CategoryID(), ResetInd: 1, ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Movies reset successfully.", 'center', 'info', 1000);
                    ViewModel.ProductList.Set(result.Data);
                    // Set Drop Down to 'Select'
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            })
        };


    </script>
</asp:Content>
