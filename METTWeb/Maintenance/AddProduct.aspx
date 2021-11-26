<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddProduct.aspx.cs" Inherits="MEWeb.Maintenance.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
    <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%
        using (var h = this.Helpers)
        {
            var MainDiv = h.DivC("row pad-top-10");
            {
                var PanelContainer = MainDiv.Helpers.DivC("col-md-12 p-n-lr");
                {
                    var HomeContainer = PanelContainer.Helpers.DivC("tabs-container");
                    {
                        var AssessmentsTab = HomeContainer.Helpers.TabControl();
                        {
                            AssessmentsTab.Style.ClearBoth();
                            AssessmentsTab.AddClass("nav nav-tabs");
                            var ProductTab = AssessmentsTab.AddTab("Products");
                            {
                                var Row = ProductTab.Helpers.DivC("row margin0");
                                {
                                    var RowCol = Row.Helpers.DivC("col-md-12");
                                    {
                                        RowCol.Helpers.HTML().Heading2("Add Products");

                                        var ProductList = RowCol.Helpers.TableFor<MELib.Maintenance.Product>((c) => c.ProductList, true, true);
                                        ProductList.AddClass("table table-striped table-bordered table-hover");
                                        var ProductListRow = ProductList.FirstRow;
                                        {
                                            var CategoryID = ProductListRow.AddColumn(c => c.CategoryID);
                                            {
                                                CategoryID.Style.Width = "300px";
                                            }
                                            var ProductName = ProductListRow.AddColumn(c => c.ProductName);
                                            {
                                                ProductName.Style.Width = "300px";
                                            }
                                            var ProductPrice = ProductListRow.AddColumn(c => c.Price);
                                            {
                                                ProductPrice.Style.Width = "300px";
                                            }
                                            var Quantity = ProductListRow.AddColumn(c => c.Quantity);
                                            {
                                                Quantity.Style.Width = "300px";
                                            }
                                            var Active = ProductListRow.AddColumn(c => c.IsActiveInd);
                                            {
                                                Active.Style.Width = "80px";
                                            }

                                        }
                                    }
                                    var SaveList = RowCol.Helpers.DivC("col-md-12 text-right");
                                    {
                                        var SaveBtn = SaveList.Helpers.Button("Save", Singular.Web.ButtonMainStyle.NoStyle, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                        {
                                            SaveBtn.AddClass("btn-primary btn btn btn-primary");
                                            SaveBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "SaveProductList($data)");
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



        var SaveProductList = function (obj) {
            ViewModel.CallServerMethod('SaveProductList', { ProductList: ViewModel.ProductList.Serialise(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Successfully Saved", 'center', 'success', 3000);
                }
                else {
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                }
            });
        }



    </script>
    t>
</asp:Content>
