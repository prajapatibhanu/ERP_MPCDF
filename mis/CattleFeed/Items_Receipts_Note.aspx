<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Items_Receipts_Note.aspx.cs" Inherits="mis_CattleFeed_Items_Receipts_Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <script type="text/javascript">
        function CalculateAmount() {
            debugger;

            var txtTotalGrossWeight = document.getElementById('<%=txtTotalGrossWeight.ClientID%>').value.trim();
            var txtTareWt = document.getElementById('<%=txtTareWt.ClientID%>').value.trim();

            if (txtTotalGrossWeight == "")
                txtTotalGrossWeight = "0";
            if (txtTareWt == "")
                txtTareWt = "0";

            document.getElementById('<%=lblNetWt.ClientID%>').value = (txtTotalGrossWeight - txtTareWt).toFixed(2);
            document.getElementById('<%=hdnAmt.ClientID%>').value = (txtTotalGrossWeight - txtTareWt).toFixed(2);
            document.getElementById('<%=hdnconverted.ClientID%>').value = ((txtTotalGrossWeight - txtTareWt) / 1000).toFixed(2);

            document.getElementById('<%=txtconverted.ClientID%>').value = ((txtTotalGrossWeight - txtTareWt) / 1000).toFixed(2);

        }
        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }
        function ShowReport() {
            $('#ReportModal').modal('show');
        }
        function Closeeport() {
            $('#ReportModal').modal('hide');
        }
        function printDiv() {
            var divName = 'printableArea';
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
        function onlyNumberKey(evt) {

            // Only ASCII charactar in that range allowed 
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        }
    </script>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Items Receipts Note Registration</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Items Receipts Note Entry(वस्तु रसीद नोट की प्रविष्टी)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Cattle Feed Plant<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vsissue" InitialValue="0"
                                                        ErrorMessage="Select Cattle Feed Plant" Text="<i class='fa fa-exclamation-circle' title='Select Cattle Feed Plant!'></i>"
                                                        ControlToValidate="ddlCFP" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlCFP" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCFP_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Purchase Order <span class="text-danger">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="vsissue" InitialValue="0"
                                                        ErrorMessage="Select Purchase order" Text="<i class='fa fa-exclamation-circle' title='Select PO!'></i>"
                                                        ControlToValidate="ddlPO" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlPO" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button Text="View" ID="btnView" ValidationGroup="vsissue" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnView_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button Text="Reset" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="POReceiptDetail" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Supplier Name<span class="text-danger"> *</span></label>

                                                <asp:Label ID="txtSupplier" runat="server" placeholder="Supplier Name..." class="form-control" MaxLength="150" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Factory Tin No<span class="text-danger"> *</span></label>

                                                <asp:Label ID="txtTinno" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Reference NO<span class="text-danger"> *</span></label>

                                                <asp:Label ID="lblRef" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Item Quantity(in
                                                    <label id="lblunit" runat="server"></label>
                                                    )<span class="text-danger"> *</span></label>

                                                <asp:Label ID="lblitemquantity" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Category (वस्तु का श्रेणी)<span style="color: red;">*</span></label>
                                                <asp:Label ID="lblItemCat" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Type (वस्तु की प्रकार)<span style="color: red;">*</span></label>
                                                <asp:Label ID="lblItemType" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                                <asp:Label ID="lblItemname" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>No of Carriage<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRecQuantity" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Item Quantity." Text="<i class='fa fa-exclamation-circle' title='Enter Item Quantity !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="saveissue" runat="server" ControlToValidate="txtRecQuantity" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtRecQuantity" placeholder="Enter Quantity" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10" onkeypress="return onlyNumberKey(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Truck No.<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttruckno" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter truck No." Text="<i class='fa fa-exclamation-circle' title='Enter truck No.!'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="saveissue"
                                                        ErrorMessage="Invalid truckno" Text="<i class='fa fa-exclamation-circle' title='Invalid truckno !'></i>"
                                                        ControlToValidate="txttruckno" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txttruckno" placeholder="Enter Truck No." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="15"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Item Received On Date (दिनांक) </label>
                                                <span style="color: red">*</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="saveissue" Display="Dynamic" ControlToValidate="txttruckRecdate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="saveissue" runat="server" Display="Dynamic" ControlToValidate="txttruckRecdate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                </span>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txttruckRecdate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>L.R  /  B.R NO. / Bill No.<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtlrbr" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter L.R. OR B.R. No." Text="<i class='fa fa-exclamation-circle' title='Enter L.R. OR B.R. No. !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="saveissue"
                                                        ErrorMessage="Invalid Bill No" Text="<i class='fa fa-exclamation-circle' title='Invalid Bill No !'></i>"
                                                        ControlToValidate="txtlrbr" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[a-zA-Z0-9_-]*$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtlrbr" placeholder="Enter L.R. OR B.R. No." autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Truck Loaded Date (दिनांक) </label>
                                                <span style="color: red">*</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="saveissue" Display="Dynamic" ControlToValidate="txtSupplierDate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="saveissue" runat="server" Display="Dynamic" ControlToValidate="txtSupplierDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                </span>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtSupplierDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Total Gross Weight(In Kg)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTotalGrossWeight" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Total Gross Weight" Text="<i class='fa fa-exclamation-circle' title='Enter Total Gross Weight !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="saveissue" runat="server" ControlToValidate="txtTotalGrossWeight" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtTotalGrossWeight" placeholder="Enter Total Gross Weight" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10" onchange="CalculateAmount();"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Less Tare Weight(In Kg.)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTareWt" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Less Tare Weight" Text="<i class='fa fa-exclamation-circle' title='Enter Less Tare Weight !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="saveissue" runat="server" ControlToValidate="txtTareWt" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTareWt" ControlToCompare="txtTotalGrossWeight" Type="Double" Operator="LessThan" Text="<i class='fa fa-exclamation-circle' title='Tare Weight should be less than Total Gross Weight !'></i>"></asp:CompareValidator>

                                                </span>
                                                <asp:TextBox ID="txtTareWt" placeholder="Enter Less Tare Weight" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10" onchange="CalculateAmount();"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Net Weight(In Kg.)<span style="color: red;"> *</span></label>
                                                <asp:HiddenField ID="hdnAmt" runat="server" Value="0" />
                                                <asp:TextBox ID="lblNetWt" CssClass="form-control Number" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Converted Weight(in
                                                    <label id="lblunit1" runat="server"></label>
                                                    )<span style="color: red;"> *</span></label>
                                                <asp:HiddenField ID="hdnconverted" runat="server" Value="0" />
                                                <asp:TextBox ID="txtconverted" CssClass="form-control Number" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--   <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Big G.R No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtGRNO" Display="Dynamic" ValidationGroup="a" ErrorMessage="Enter Big G.R No" Text="<i class='fa fa-exclamation-circle' title='Enter Big G.R No !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtGRNO" ErrorMessage="Please Enter Valid Number." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtGRNO" placeholder="Enter Big G.R No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                    </div>

                                    <div class="col-md-12">
                                      <%--  <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Small G.R No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtsmallGRNO" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Small G.R No" Text="<i class='fa fa-exclamation-circle' title='Enter Small G.R No !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9_-]*$" ValidationGroup="saveissue" runat="server" ControlToValidate="txtsmallGRNO" ErrorMessage="Please Enter Valid Small G.R No ." Text="<i class='fa fa-exclamation-circle' title='Please Enter Small G.R No . !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtsmallGRNO" placeholder="Enter Small G.R No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Small G.R Doc. No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtsmallgrdocument" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Small G.R No Document No" Text="<i class='fa fa-exclamation-circle' title='Enter Small G.R No Document No !'></i>"></asp:RequiredFieldValidator>
                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator13" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9_-]*$" ValidationGroup="saveissue" runat="server" ControlToValidate="txtsmallgrdocument" ErrorMessage="Please Enter Valid Small G.R doc No ." Text="<i class='fa fa-exclamation-circle' title='Please Enter Small G.R doc No . !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtsmallgrdocument" placeholder="Enter Small G.R No Document No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                          <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Small G.R No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtsmallGRNO" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Small G.R No" Text="<i class='fa fa-exclamation-circle' title='Enter Small G.R No !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtsmallGRNO" placeholder="Enter Small G.R No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Small G.R Doc. No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtsmallgrdocument" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Small G.R No Document No" Text="<i class='fa fa-exclamation-circle' title='Enter Small G.R No Document No !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtsmallgrdocument" placeholder="Enter Small G.R No Document No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--<div class="col-md-4">
                                            <div class="form-group">
                                                <label>Code / Invoice No<span style="color: red;"> *</span></label>

                                                <asp:Label ID="lblInvoiceno" CssClass="form-control Number" runat="server"></asp:Label>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Driver Name<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="saveissue"
                                                        ErrorMessage="Enter Driver Name" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name!'></i>"
                                                        ControlToValidate="txtDriver" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="saveissue"
                                                        ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                        ControlToValidate="txtDriver" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtDriver" runat="server" placeholder="Driver Name..." class="form-control" MaxLength="150" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Driver Contact NO.<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="saveissue"
                                                        ErrorMessage="Enter Driver Contact No" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Contact No!'></i>"
                                                        ControlToValidate="txtDriverContactNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDriverContactNo" ErrorMessage="Please Enter Valid Number." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtDriverContactNo" runat="server" placeholder="Driver Contact No..." onpaste="return false;" class="form-control" MaxLength="10" onkeypress="return onlyNumberKey(event);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remark (रिमार्क)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rvremrk" runat="server" ControlToValidate="txtRemark" Display="Dynamic" ValidationGroup="saveissue" ErrorMessage="Enter Remark." Text="<i class='fa fa-exclamation-circle' title='Enter Remark !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="saveissue"
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
                                                <asp:Button Text="Save" ID="btnSubmit" ValidationGroup="saveissue" CausesValidation="true" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button Text="Clear" ID="btnClear" CssClass="btn btn-block btn-default" runat="server" OnClick="btnClear_Click" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="box box-Manish">
                                            <div class="box-body">
                                                <fieldset>
                                                    <legend>Received Item List
                                                    </legend>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:HiddenField ID="hdnItemCatID" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdnItemID" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdnItemTypeID" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                                            <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" ItemStyle-Width="20%" />
                                                                    <asp:BoundField DataField="ItemName" HeaderText="Item" ItemStyle-Width="20%" />
                                                                    <asp:BoundField DataField="Received_Quantity" HeaderText="Quantity(In Bags)" ItemStyle-Width="10%" />
                                                                    <asp:BoundField DataField="Item_Recieved_On_Date" HeaderText="Recieved On" ItemStyle-Width="10%" />
                                                                    <asp:BoundField DataField="BigGRNo" HeaderText="Big G.R.No" ItemStyle-Width="8%" />
                                                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Material No" ItemStyle-Width="10%" />
                                                                    <asp:BoundField DataField="NET_Wt" HeaderText="NET Wt" ItemStyle-Width="7%" />
                                                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="false" CommandName="RecordDetail" CommandArgument='<%#Eval("CFP_Items_Receipts_Note_ID") %>' Text="Edit"><i class="fa fa-tasks"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFP_Items_Receipts_Note_ID") %>' Text="Edit" OnClientClick="return confirm('Item Detail will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("CFP_Items_Receipts_Note_ID") %>' CommandName="RecordReport" CausesValidation="false" runat="server" ToolTip="Report" Style="color: red;"><i class="fa fa-file"></i></asp:LinkButton>
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
                            </fieldset>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Recipe Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Recipe Details for
                                <asp:Label ID="lblpro" CssClass="Autoclr" runat="server"></asp:Label></span><br />
                            <asp:Label ID="llbMsg2" runat="server" Text=""></asp:Label>
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 40%">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Big G.R No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGRNO" Display="Dynamic" ValidationGroup="b" ErrorMessage="Enter Big G.R No" Text="<i class='fa fa-exclamation-circle' title='Enter Big G.R No !'></i>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9_-]*$" ValidationGroup="b" runat="server" ControlToValidate="txtGRNO" ErrorMessage="Please Enter Valid Number." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number. !'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtGRNO" placeholder="Enter Big G.R No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 60%">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>Code / Invoice / Material No.</label>

                                                <asp:TextBox ID="txtMaterialNo" placeholder="Enter Material No" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="25"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>


                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>

                            <asp:Button runat="server" ID="btnUpdate" class="btn btn-success" Text="Update" CausesValidation="true" ValidationGroup="b" OnClick="btnUpdate_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ReportModal" tabindex="-1" role="dialog" aria-labelledby="ReportModal" aria-hidden="true">
                <div class="modal-dialog" style="height: 100%; width: 55%;">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Invoice Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body" id="printableArea">

                            <div class="row" style="text-align: center; margin-left: 5px;">
                                <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="2" Width="90%">
                                    <table class="table table-bordered">
                                        <tr>
                                            <td style="text-align: center;" colspan="2">


                                                <span id="lblcfpname" runat="server" style="color: maroon; font-size: 22px; font-weight: bold"></span>
                                                <br />
                                                <span id="lblcfp" runat="server" style="color: maroon; font-size: 12px; font-weight: bold"></span>
                                                <br />

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="2">
                                                <span id="lblSmallGRNO" runat="server" style="color: maroon; font-size: 22px; font-weight: bold"></span>
                                                &nbsp;&nbsp;<span style="font-size: 12px;">M/S</span>&nbsp; <span id="lbSupplier" runat="server" style="color: black; font-weight: bold"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Received</span>&nbsp; <span id="lblReceivedqty" runat="server" style="color: black; font-weight: bold"></span></td>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Bags of</span>&nbsp; <span id="lblItem" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">On</span>&nbsp; <span id="lblItemReceivedDate" runat="server" style="color: black; font-weight: bold"></span></td>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Truck No</span>&nbsp; <span id="lblTruckNO" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">L.R Or R.R No</span>&nbsp; <span id="lbllrrno" runat="server" style="color: black; font-weight: bold"></span></td>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Date</span>&nbsp; <span id="lblTruckloadingDate" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="2">
                                                <span style="font-size: 17px;">Subject to Checking and Approved</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Total Gross Weight</span></td>
                                            <td style="text-align: left;"><span id="lblTotalGrosswt" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Less Tare Weight</span></td>
                                            <td style="text-align: left;"><span id="lbltarewt" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Net Weight</span></td>
                                            <td style="text-align: left;"><span id="lblNetWt1" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="2">
                                                <span style="font-size: 12px;">Remark</span>&nbsp; <span id="lblRemark" runat="server" style="color: black; font-weight: bold"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <span id="lblDriver" runat="server" style="color: black; font-weight: bold"></span>
                                                <br />
                                                <span id="lbDriverContact" runat="server" style="color: black; font-weight: bold"></span>
                                                <br />
                                                <span style="font-size: 12px;">(Driver Signature)</span>
                                            </td>

                                            <td style="text-align: left;">
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <span id="Span2" runat="server" style="color: black; font-weight: bold">Sig....</span><br />
                                                <span id="Span3" runat="server" style="color: black; font-weight: bold">Store Clerk / Store Supdt.</span>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;"><span style="font-size: 12px;">Code / Invoice No</span> &nbsp;&nbsp;&nbsp;<span id="lblInvoiceNo" runat="server" style="color: black; font-weight: bold"></span></td>
                                            <td style="text-align: left;"><span style="font-size: 12px;">BIg G.R No</span> &nbsp;&nbsp;&nbsp;<span id="lblGrNO" runat="server" style="color: black; font-weight: bold"></span></td>
                                        </tr>


                                    </table>
                                </asp:Panel>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-default" ID="btnclose" OnClientClick="Closeeport();" CausesValidation="false" Text="Close" />
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnprint" OnClientClick="printDiv();" CausesValidation="false" Text="Print" />
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

