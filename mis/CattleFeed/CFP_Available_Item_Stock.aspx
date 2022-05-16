<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Available_Item_Stock.aspx.cs" Inherits="mis_CattleFeed_CFP_Available_Item_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Available Stock</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Available Item Stock
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Cattle Feed<span class="text-danger">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Cattle Feed Plant" Text="<i class='fa fa-exclamation-circle' title='Select  Cattle Feed Plant!'></i>"
                                                        ControlToValidate="ddlcfp" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlcfp" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-md-4">
                                            <div class="form-group">
                                                <label>दिनांक<span style="color: red;"> *</span></label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtdate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                       

                                    </div>
                                    <div class="col-md-12">
                                         <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Category (श्रेणी)<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlItemCategory" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Type (प्रकार) <span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item (वस्तु/प्रोडक्ट्स)<span class="text-danger">*</span></label>
                                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    <div class="col-md-12" style="text-align:center;">
                                        <asp:Button ID="btnview" runat="server" Text="View Stock" OnClick="btnview_Click" CssClass="btn btn-primary" CausesValidation="true" ValidationGroup="a" />
                                    </div>

                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Available  Stock
                                </legend>
                                <div class="col-md-12">
                                    <asp:GridView ID="grdlist" runat="server" PageSize="20" AllowPaging="true" OnPageIndexChanging="grdlist_PageIndexChanging" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemTypeName" HeaderText="Type" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                             <asp:TemplateField HeaderText="Received QTY<br> (By Local and PO)">
                                                <ItemTemplate>
                                                   <%#Eval("Itemreceived") %>
                                                </ItemTemplate>
                                                 </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Itemreceived" HeaderText="Received QTY (By Local and PO)" />--%>
                                            <asp:BoundField DataField="StockItem" HeaderText="Stock" />
                                            <asp:BoundField DataField="IssuedQuty" HeaderText="Issued Qty" />
                                            <asp:BoundField DataField="AvaiableQTY" HeaderText="Avaiable QTY" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    
 
</asp:Content>

