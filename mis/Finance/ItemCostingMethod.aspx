<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemCostingMethod.aspx.cs" Inherits="mis_Finance_ItemCostingMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .new_back_button {
            float:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Item Costing Method</h3>
                    <asp:HyperLink ID="hyperlink" runat="server" NavigateUrl="RptStockSummaryItem.aspx" CssClass="badge bg-teal new_back_button">Click Here to go to Stock Summary Page</asp:HyperLink>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlOffice" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlItem" runat="server" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Costing Method</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlcostingmethod" runat="server" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlcostingmethod_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Average Costing">Average Costing</asp:ListItem>
                                    <asp:ListItem Value="Standard Costing">Standard Costing</asp:ListItem>
                                    <asp:ListItem Value="NRV">NRV</asp:ListItem>
                                    <asp:ListItem Value="FIFO">FIFO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Standard Cost</label><span class="text-danger">*</span>
                                <asp:TextBox ID="txtstandardcost" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Enter Standard Cost"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" ID="btnUpdate" Text="Update" OnClientClick="return validateform();" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="ItemCostingMethod.aspx" id="btnClear" runat="server" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Name. \n";
            }
            if (document.getElementById('<%=ddlcostingmethod.ClientID%>').selectedIndex == 0) {
                msg += "Select Costing Method. \n";
            }
            if (document.getElementById('<%=ddlcostingmethod.ClientID%>').selectedIndex == 2) {
                if (document.getElementById('<%=txtstandardcost.ClientID%>').value.trim() == "") {
                    msg += "Enter Standard Cost. \n";
                }
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                return true;

            }
        }
        function onchange() {
            debugger;
            var textbox = document.getElementsByName("txtstandardcost");
            var Index = document.getElementById('<%=ddlcostingmethod.ClientID%>').selectedIndex;
            if (Index == 1) {
                textbox.setAttribute("disabled", false);
            }
            else {

            }
        }
    </script>
</asp:Content>

