<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="MEWeb.Profile.Transactions" %>

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
            var MainContent = h.DivC("row pad-top-10");
            {
                var MainContainer = MainContent.Helpers.DivC("col-md-12 p-n-lr");
                {
                    var PageContainer = MainContainer.Helpers.DivC("tabs-container");
                    {
                        var PageTab = PageContainer.Helpers.TabControl();
                        {
                            PageTab.Style.ClearBoth();
                            PageTab.AddClass("nav nav-tabs");
                            var ContainerTab = PageTab.AddTab("Transaction History");
                            {
                                var RowContentDiv = ContainerTab.Helpers.DivC("row");
                                {

                                    #region Left Column / Data
                                    var LeftColRight = RowContentDiv.Helpers.DivC("col-md-9");
                                    {

                                        var CardDiv = LeftColRight.Helpers.DivC("ibox float-e-margins paddingBottom");
                                        {
                                            var CardTitleDiv = CardDiv.Helpers.DivC("ibox-title");
                                            {
                                                CardTitleDiv.Helpers.HTML("<i class='ffa-lg fa-fw pull-left'></i>");
                                                CardTitleDiv.Helpers.HTML().Heading5("Transactions");

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
                                            var FormContent = CardDiv.Helpers.TableFor<MELib.Accounts.Transaction>(c => c.TransactionList, false, false);
                                            {
                                                var TransactionDiv = FormContent.Helpers.DivC("col-md-12");
                                                {
                                                    var TransactionDescription = TransactionDiv.Helpers.DivC("col-md-12");
                                                    {
                                                        var Transaction = FormContent.FirstRow;

                                                        var TransactionType = Transaction.AddColumn("Transaction Type");
                                                        {
                                                            TransactionType.Style.Width = "200px";
                                                            var TransactionTypeText = TransactionType.Helpers.Span(c => c.TransactionTypeName);
                                                        }
                                                        var TransactionAmt = Transaction.AddColumn("Transaction Amount");
                                                        {
                                                            TransactionAmt.Style.Width = "200px";
                                                            var TransactionAmtText = TransactionAmt.Helpers.Span(c => c.Amount);
                                                        } 
                                                        
                                                        var TransactionDate = Transaction.AddColumn("Transaction Date");
                                                        {
                                                            TransactionDate.Style.Width = "200px";
                                                            var TransactionDateText = TransactionDate.Helpers.Span(c => c.Date);
                                                        }

                                                    }



                                                }
                                            }
                                        }
                                        #endregion

                                       
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


    </script>
</asp:Content>
