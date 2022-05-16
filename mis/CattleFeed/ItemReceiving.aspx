<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemReceiving.aspx.cs" Inherits="mis_CattleFeed_ItemReceiving" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function CalculateAmount() {
            debugger;
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
             var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
             if (Quantity == "")
                 Quantity = "0";
             if (Rate == "")
                 Rate = "0";

             document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            document.getElementById('<%=hdnamt.ClientID%>').value = (Quantity * Rate).toFixed(2);
         }
    </script>
    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Local Item Receiving (वस्तु प्राप्त करना)</h3>
                    </div>
                    <div class="box-body">
                        <fieldset>
                            <legend>Register Item Received (प्राप्त किये गए वस्तु की प्रविष्टी)
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            <label>Date (दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="vgdmissue" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtTransactionDt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Cattel Feed Plant <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="ddlcfp" ValidationGroup="vgdmissue" InitialValue="0" ErrorMessage="Select CFP." Text="<i class='fa fa-exclamation-circle' title='Select CFP !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlcfp" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlcfp_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Category (वस्तु का श्रेणी)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" ValidationGroup="vgdmissue" InitialValue="0" ErrorMessage="Select Item Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Group !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitemcategory" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Type (वस्तु की प्रकार)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitemtype" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--  <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Purchase Order No.(क्रय आदेश क्रमांक) <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="txtPONo" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter PO Number." Text="<i class='fa fa-exclamation-circle' title='Enter Item Quantity !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtPONo" placeholder="Purchase Order No." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>PO Date (दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="vgdmissue" Display="Dynamic" ControlToValidate="txtpodate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtpodate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtpodate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgdmissue" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitems" runat="server" Width="100%" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ddlitems_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Quantity (मात्रा)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Item Quantity." Text="<i class='fa fa-exclamation-circle' title='Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtQuantity" placeholder="Enter Quantity" onchange="CalculateAmount();" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Unit(इकाई)</label>
                                            <asp:DropDownList ID="ddlUnit" Enabled="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Rate/दर(per unit) <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRate" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Item Rate." Text="<i class='fa fa-exclamation-circle' title='Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtRate" placeholder="Enter Rate" autocomplete="off" onchange="CalculateAmount();" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Amount <span style="color: red;">*</span></label>
                                            <%--  <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRate" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Item Rate." Text="<i class='fa fa-exclamation-circle' title='Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgdmissue" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                            </span>--%>
                                            <asp:HiddenField ID="hdnamt" runat="server" Value="0" />
                                            <asp:TextBox ID="txtAmount" placeholder="Enter Amount" Enabled="false" autocomplete="off" onchange="CalculateRate();" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Invoice Order No.(चालान आदेश क्र) <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtinvoiceno" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Invoice Number." Text="<i class='fa fa-exclamation-circle' title='Enter Invoice no. !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="vgdmissue"
                                                    ErrorMessage="Invalid Invoice No" Text="<i class='fa fa-exclamation-circle' title='Invalid Invoice !'></i>"
                                                    ControlToValidate="txtinvoiceno" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[a-zA-Z0-9_-]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtinvoiceno" placeholder="Invoice No." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Invoice Date (चालान आदेश दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="vgdmissue" Display="Dynamic" ControlToValidate="txtInvoiceDate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtInvoiceDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Supplier Name (आपूर्तिकर्ता का नाम)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSupplier" Display="Dynamic" ValidationGroup="vgdmissue" ErrorMessage="Enter Item Supplier." Text="<i class='fa fa-exclamation-circle' title='Enter Item Supplier !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vgdmissue"
                                                    ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                    ControlToValidate="txtSupplier" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtSupplier" placeholder="Enter Supplier" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Receive Date (प्राप्त दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="vgdmissue" Display="Dynamic" ControlToValidate="txtSupplyDate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="vgdmissue" runat="server" Display="Dynamic" ControlToValidate="txtSupplyDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtSupplyDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Upload Invoice </label>
                                            <asp:FileUpload ID="Fileinvoice" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark (रिमार्क)<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="vgdmissue"
                                                ErrorMessage="Invalid Remark" Text="<i class='fa fa-exclamation-circle' title='Invalid Remark !'></i>"
                                                ControlToValidate="txtRemark" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtRemark" placeholder="Enter Remark" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Save" ID="btnSubmit" ValidationGroup="vgdmissue" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Clear" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <div class="box box-Manish">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Received Item List
                                        </legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                                <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReceivedDate" HeaderText="Received Date" />
                                                        <%-- <asp:BoundField DataField="PONo" HeaderText="PO No" />
                                                        <asp:BoundField DataField="PODate" HeaderText="PO Date" />--%>
                                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                                                        <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier Name" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="10%" />
                                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("ItemReceivedID") %>' Text="Edit" OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="false" CommandName="RecordDelete" CommandArgument='<%#Eval("ItemReceivedID") %>' Text="Delete" Style="color: red;" OnClientClick="return confirm('CFP Entry will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script src="../../js/jquery-1.10.2.js"></script>
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print" style="display:none;"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o" style="display:none;"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
</asp:Content>

