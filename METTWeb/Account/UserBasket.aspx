<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserBasket.aspx.cs" Inherits="MEWeb.Account.UserBasket" %>

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
                            var HomeContainerTab = AssessmentsTab.AddTab("Basket");
                            {
                                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                                {
                                    var RowCol = Row.Helpers.DivC("col-md-6");
                                    {
                                        RowCol.Helpers.HTML().Heading2("Your Basket");
                                        RowCol.Helpers.HTML("<p>Your Basket contains the following products: </p>");


                                        var Basket = RowCol.Helpers.TableFor<MELib.Basket.Basket>((c) => c.BasketList, false, true);
                                        var BasketAmt = RowCol.Helpers.TableFor<MELib.Order.Order>((c) => c.OrderList, false, false);
                                       
                                        {
                                            Basket.AddClass("table table-striped table-bordered table-hover");
                                            var BasketRow = Basket.FirstRow;
                                            var BasketAmtRow = BasketAmt.FirstRow;
                                            {
                                                RowCol.Helpers.HTML().Heading3("Basket Amount: R" + ViewModel.Total.ToString("F"));
                                                
                                                var ProductName = BasketRow.AddColumn("Product Name");
                                                {
                                                    ProductName.Style.Width = "200px";
                                                    var ProductNameText = ProductName.Helpers.Span(c => c.ProductName);

                                                }
                                                var Price = BasketRow.AddColumn("Price");
                                                {
                                                    Price.Style.Width = "200px";
                                                    var PriceText = Price.Helpers.Span(c => "R" + c.FinalAmount);
                                                }
                                                var Quantity = BasketRow.AddColumn("Quantity");
                                                {
                                                    Quantity.Style.Width = "100px";
                                                    var QuantitText = Quantity.Helpers.EditorFor(c => c.ItemsCount);
                                                }
                                                var BTN = BasketRow.AddColumn("");
                                                {
                                                    var UpdateBtn = BTN.Helpers.Button("Update", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.check);
                                                    {
                                                        UpdateBtn.AddClass("btn btn-primary btn-outline");
                                                        UpdateBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "UpdateBasket($data)");
                                                    }
                                                }


                                                
                                                RowCol.Helpers.HTML().Heading5("If you choose to have your order delivered, a fee of R50 will be charged");
                                                RowCol.Helpers.LabelFor(c => ViewModel.DeliveryOptionTypeID);
                                                var CategoryEditor = RowCol.Helpers.EditorFor(c => ViewModel.DeliveryOptionTypeID);

                                            }
                                        }


                                        var CheckOut = RowCol.Helpers.DivC("col-md-12 text-right");
                                        {
                                            var CheckOutBtn = CheckOut.Helpers.Button("CheckOut", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.money);
                                            {
                                                CheckOutBtn.AddClass("btn-primary btn btn btn-primary");
                                                CheckOutBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "CheckOut($data)");
                                            }
                                            {
                                                var SaveBtn = CheckOut.Helpers.Button("Save Basket", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.shopping_basket);
                                                {
                                                    SaveBtn.AddClass("btn-primary btn btn btn-primary");
                                                    SaveBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "SaveBasketList($data)");
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

        var CheckOut = function (obj) {
            ViewModel.CallServerMethod('Checkout', { BasketID: obj.BasketID(), BasketList: ViewModel.BasketList.Serialise(), DeliveryOptionTypeID: obj.DeliveryOptionTypeID(), UserID: ViewModel.UserID(), Total: ViewModel.Total(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Transaction was successful", 'center', 'success', 6000);
                    location.reload();
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });
        }
        var SaveBasketList = function (obj) {
            ViewModel.CallServerMethod('SaveBasketList', { BasketID: obj.BasketID(), BasketList: ViewModel.BasketList.Serialise(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Successfully Saved", 'center', 'success', 6000);
                    location.reload();
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });

        }

        var UpdateBasket = function (obj) {
            ViewModel.CallServerMethod('UpdateBasket', { BasketID: obj.BasketID(), BasketList: ViewModel.BasketList.Serialise(), UserID: ViewModel.UserID(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Successfully Updated", 'center', 'success', 3000);
                    location.reload();
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });

        }
    </script>
</asp:Content>
