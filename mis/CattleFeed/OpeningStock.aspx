<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OpeningStock.aspx.cs" Inherits="mis_CattleFeed_OpeningStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Item Inward Entry (वस्तु आवक प्रविष्टि)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Item Inward Entry (वस्तु आवक प्रविष्टि)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Date (दिनांक) </label>
                                                <span style="color: red">*</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="vgos" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="vgos" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                </span>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtTransactionDt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cattel Feed Plant <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="ddlcfp" ValidationGroup="vgos" InitialValue="0" ErrorMessage="Select CFP." Text="<i class='fa fa-exclamation-circle' title='Select CFP !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlcfp" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlcfp_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Category (वस्तु का श्रेणी)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" ValidationGroup="vgos" InitialValue="0" ErrorMessage="Select Item Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Group !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlitemcategory" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Type (वस्तु की प्रकार)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" ValidationGroup="vgos" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlitemtype" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgos" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlitems" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitems_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Unit(इकाई)</label>
                                                <asp:DropDownList ID="ddlUnit" runat="server" Enabled="false" CssClass="form-control">
                                                    <asp:ListItem Text="Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Quantity (मात्रा)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="txtQty" Display="Dynamic" ValidationGroup="vgos" ErrorMessage="Enter Item Quantity." Text="<i class='fa fa-exclamation-circle' title='Enter Item Quantity !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,5})?$" ValidationGroup="vgos" runat="server" ControlToValidate="txtQty" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtQty" placeholder="Quantity" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="8"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%--<div class="col-md-3">
                                            <div class="form-group">
                                                <label>Rate/(दर)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate" Display="Dynamic" ValidationGroup="vgos" ErrorMessage="Enter Item Rate." Text="<i class='fa fa-exclamation-circle' title='Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgos" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtRate" placeholder="Enter Rate" autocomplete="off" onchange="CalculateAmount();" onpaste="return false;" CssClass="form-control Number" onkeypress="return validateDec(this,event)" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Amount/(राशि)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount" Display="Dynamic" ValidationGroup="vgos" ErrorMessage="Enter Item Amount." Text="<i class='fa fa-exclamation-circle' title='Enter Item Amount !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgos" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtAmount" placeholder="Enter Amount" autocomplete="off" onpaste="return false;" onchange="CalculateRate();" CssClass="form-control Number" onkeypress="return validateDec(this,event)" runat="server" MaxLength="15"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button Text="Save" ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="vgos" runat="server" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="box box-primary">

                        <div class="box-body">
                            <fieldset>
                                <legend>Stock Detail (स्टॉक विवरण)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                            <asp:GridView ID="gvOpeningStock" DataKeyNames="CFPItemStockID" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" OnRowCommand="gvOpeningStock_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TranDt" HeaderText="Inward Date (आवक तिथि)" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity (मात्रा)" />
                                                    <asp:BoundField DataField="Unit" HeaderText="Unit (इकाई)" />
                                                    <%-- <asp:BoundField DataField="Rate" HeaderText="Rate (दर)" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount (राशि)" />--%>
                                                    <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFPItemStockID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("CFPItemStockID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
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

