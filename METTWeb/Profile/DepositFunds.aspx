<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepositFunds.aspx.cs" Inherits="MEWeb.Profile.DepositFunds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Add page specific styles and JavaScript classes below -->
    <link href="../Theme/Singular/Custom/home.css" rel="stylesheet" />
    <link href="../Theme/Singular/Custom/customstyles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitleContent" runat="server">
    <!-- Placeholder not used in this example -->
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
                            var HomeContainerTab = AssessmentsTab.AddTab("Deposit");
                            {
                                var Row = HomeContainerTab.Helpers.DivC("row margin0");
                                {
                                    var RowCol = Row.Helpers.DivC("col-md-5");
                                    {

                                        var CardDiv = RowCol.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = CardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='fa fa-book fa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Deposit Funds");
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
                                            var ContentDiv = CardDiv.Helpers.DivC("ibox-content");
                                            {
                                                var RowContentDiv = ContentDiv.Helpers.DivC("row");
                                                {
                                                    var LeftColContentDiv = RowContentDiv.Helpers.DivC("col-md-2");
                                                    {
                                                        // Place holder
                                                    }
                                                    var MiddleColContentDiv = RowContentDiv.Helpers.DivC("col-md-6");
                                                    {
                                                        var FormContent = MiddleColContentDiv.Helpers.TableFor<MELib.Accounts.Account>(c => c.AccountLIst, false, false);
                                                        {
                                                            var DepositAmtDiv = FormContent.Helpers.DivC("col-md-12");
                                                            {
                                                                var DepositDescription = DepositAmtDiv.Helpers.DivC("col-md-12");
                                                                {
                                                                    
                                                                    var DepositAmt = FormContent.FirstRow.AddColumn(c => c.Balance);

                                                                }
                                                                var SaveBtn = MiddleColContentDiv.Helpers.Button("Deposit", Singular.Web.ButtonMainStyle.Primary, Singular.Web.ButtonSize.Normal, Singular.Web.FontAwesomeIcon.None);
                                                                {
                                                                    SaveBtn.AddBinding(Singular.Web.KnockoutBindingString.click, "Fund($data)");
                                                                    SaveBtn.AddClass("btn btn-primary");
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
        // Place page specific JavaScript here or in a JS file and include in the HeadContent section
        Singular.OnPageLoad(function () {
            $("#menuItem1").addClass('active');
            $("#menuItem1 > ul").addClass('in');
        });
        var Fund = function (data) {
            ViewModel.CallServerMethod('FundAccount', { Account: data.AccountLIst.Serialise(), ShowLoadingBar: true }, function (result) {
                if (result.Success) {
                    MEHelpers.Notification("Deposit was successful", 'center', 'success', 3000);
                    window.location = '../Account/Home.aspx';
                    Singular.AddMessage(3, 'Save', 'Deposit was successful').Fade(2000);
                }
                else {
                    //MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);
                    MEHelpers.Notification(result.ErrorText, 'center', 'warning', 5000);

                }
            });
        }

    </script>
</asp:Content>
