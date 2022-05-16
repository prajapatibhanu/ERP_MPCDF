<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Product_Supply.aspx.cs" Inherits="mis_CattelFeed_CFP_Product_Supply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
        .rbl input[type="radio"] {
            margin-left: 6px;
            margin-right: 1px;
        }
    </style>
    <script type="text/javascript">
        function ShowPopupAddDates() {
            $('#myModal').modal('show');
        }
        function ShowReport() {
            $('#ReportModal').modal('show');
        }
        function Closeeport() {
            $('#ReportModal').modal('hide');
        }
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

        <%--  function CalculateAmount() {
            debugger;
            var Quantity = document.getElementById('<%=txtBags.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtrate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmt.ClientID%>').value = (Quantity * Rate).toFixed(2);
        }--%>
    </script>

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Generate Sale Invoice</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Sale / Generate Sale Invoice
                                </legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sale Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="ddlOfficeTypeTo" InitialValue="0" ValidationGroup="dataissue" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlOfficeTypeTo" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplierTo_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="ddsupply" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Sale To<span style="color: red;"> *</span></label>

                                            <asp:DropDownList ID="ddlOffice" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="tsupplier" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Sale To<span style="color: red;"> *</span></label>

                                            <asp:TextBox ID="txtOffice" runat="server" placeholder="Name..." class="form-control" MaxLength="150" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Production Unit<span class="hindi">(उत्पादन इकाई)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlcfp" InitialValue="0" ValidationGroup="dataissue" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                            </span>>
                                                <asp:DropDownList ID="ddlcfp" CssClass="form-control select2" runat="server">
                                                    <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="margin-top: 17px;">
                                        <div class="form-group">
                                            <asp:Button ID="ButtonAdd" runat="server" ValidationGroup="dataissue" CausesValidation="true" Text="Click here to Add Product" CssClass="btn btn-info" OnClick="ButtonAdd_Click" />

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <asp:GridView ID="Gridview1" runat="server" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" CssClass="table table-hover table-bordered pagination-ys" OnRowDataBound="OnRowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNO.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="hdnpackagingsize" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnproductavilableQuantity" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdnCFPProductid" runat="server" Value="0" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlProduct" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                            <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bags">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBags" runat="server" Enabled="false" placeholder="Total Bag to Dispatch..." class="form-control" AutoPostBack="true" OnTextChanged="txtBags_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity<br>In Metric Tonne">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Enabled="false" runat="server" placeholder="Metric Ton..." class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate/दर<br>(Ex- factor rate Per Bag)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtrate" runat="server" Enabled="false" placeholder="Rate..." class="form-control" autocomplete="off" onpaste="return false;" AutoPostBack="true" OnTextChanged="txtrate_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmt" runat="server" Enabled="false" placeholder="Amount..." class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="row" id="notDC" runat="server" visible="false">
                                    <div class="col-md-5">

                                        <div class="form-group" style="margin-top: 15px;">
                                            <asp:RadioButtonList runat="server" ID="rbtnRateType" RepeatDirection="Horizontal" CssClass="rbl form-control" CellPadding="3" CellSpacing="2"></asp:RadioButtonList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Supply Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtsupplydate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Supplier<span style="color: red;"> *</span></label>

                                                <asp:DropDownList ID="ddlSupplier" CssClass="form-control select2" runat="server">
                                                    <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Distance<span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtDistance" runat="server" placeholder="Distance..." class="form-control" MaxLength="5" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Vehicle No.<span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtVehicleNo" runat="server" placeholder="Vehicle No..." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Empty Bag </label>
                                            <asp:TextBox ID="txtEmptyBag" runat="server" placeholder="Empty Bag..." class="form-control" OnTextChanged="txtEmptyBag_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Grand Total</label>
                                            <asp:TextBox ID="txtGrandTotal" runat="server" placeholder="Grand Total..." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Replace Item </label>
                                            <asp:TextBox ID="txtReplaceItem" runat="server" placeholder="Replace Item..." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="col-md-12" id="bttonpnl" runat="server">
                                    <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                    <%-- <asp:HiddenField ID="hdnproductavilableQuantity" runat="server" Value="0" />
                                    
                                    <asp:HiddenField ID="hdnCFPProductid" runat="server" Value="0" />--%>
                                    <asp:Button Text="Save/सुरक्षित" ID="btnsave" ValidationGroup="a" CausesValidation="true" CssClass="btn btn-success" runat="server" OnClick="btnsave_Click" Visible="false" />
                                    &nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" CausesValidation="false" CssClass="btn btn-default" Text="Reset/रीसेट" OnClick="btnClear_Click" Visible="false" />

                                </div>
                            </fieldset>
                            <div class="col-md-12">
                                <div class="box box-Manish">
                                    <div class="box-body">
                                        <fieldset>
                                            <legend>Registered Invoice
                                            </legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="hdnproductsize" runat="server" Value="0" />
                                                    <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Invoiceno" HeaderText="Invoice no" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="Office" HeaderText="Supply to" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" />
                                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFP_Product_Invoice_ID") %>' Text="Edit" ToolTip="View Product included in Invoice"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LnkReport" runat="server" CausesValidation="false" CommandName="Report" CommandArgument='<%#Eval("CFP_Product_Invoice_ID") %>' Text="Report" ToolTip="View Invoice Report"><i class="fa fa-archive"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="false" CommandName="RecordDelete" CommandArgument='<%#Eval("CFP_Product_Invoice_ID") %>' Text="Delete" Style="color: red;" OnClientClick="return confirm('Invoice will be discarded. Are you sure want to continue?');" ToolTip="Discard this Invoice"><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>

    </div>
    <!-- Modal style="height: 100%; width: 100%;"-->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog" style="height: 100%; width: 60%;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <span style="color: white">Product Details</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView2" runat="server" class="table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Packaging_Size" HeaderText="Packaging Size" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="TotalBagPurchased" HeaderText="Bags" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="ProductQuantity" HeaderText="Product Quantity" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" />

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="ReportModal" tabindex="-1" role="dialog" aria-labelledby="ReportModal" aria-hidden="true">
        <div class="modal-dialog" style="height: 100%; width: 60%;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <span style="color: white">Invoice Details</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body" id="printableArea">
                    <asp:Panel ID="pnl" runat="server" BorderColor="Black" BorderWidth="2">
                        <div class="row">

                            <div class="col-md-12" style="text-align: center">
                                <asp:Label ID="lblcfpname" runat="server" Font-Bold="true" Font-Size="21px"></asp:Label><br />
                                <asp:Label ID="lblAddress" runat="server" Font-Bold="false" Font-Size="16px"></asp:Label><br />
                                <asp:Label ID="lblphone" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label><br />
                                <asp:Label ID="lblemail" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label><br />
                                <asp:Label ID="Label1" runat="server" Text="INVOICE ( CASH/CREDIT )" Font-Bold="true" Font-Size="16px"></asp:Label>
                            </div>
                            <div class="col-md-12" style="text-align: center">
                                <table class="table table-bordered">
                                    <tr>
                                        <td style="font-size: 12px;">PAN:
                                        <asp:Label ID="lblpan" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label><br />
                                        </td>
                                        <td style="font-size: 12px;">GSTN:
                                        <asp:Label ID="lblGSTN" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label><br />
                                        </td>
                                        <td style="font-size: 12px;">TAN:
                                        <asp:Label ID="lblTAN" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label><br />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="font-size: 12px;">Consignee:-
                                        <asp:Label ID="lblConsignee" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label><br />
                                        </td>
                                        <td style="font-size: 12px;">Buyer(if othr than Consignee)
                                      
                                        </td>
                                        <td style="font-size: 12px; text-align: left">Invoice No:
                                        <asp:Label ID="lblinvoiceno" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>&nbsp;Date:
                                        <asp:Label ID="lblinvoicedate" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label><br />
                                            Dispatch Through:<asp:Label ID="lbldispatch" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label><br />
                                            Terms Of Delivery:<asp:Label ID="lblTerms" runat="server" Font-Bold="false" Font-Size="12px"></asp:Label>

                                        </td>

                                    </tr>

                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grdinvoice" runat="server" ShowFooter="true" class="table table-hover table-bordered pagination-ys" EmptyDataText="No Record Available" AutoGenerateColumns="False" OnRowDataBound="grdinvoice_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Product" HeaderText="DESCRIPTION OF GOODS " ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TotalBagPurchased" HeaderText="QTY BAGS" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="ProductQuantity" HeaderText="QTY MT" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="UNIT" HeaderText="UNIT" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />

                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div class="row">
                                                <div class="col-md-6" style="text-align: left;">
                                                    <span style="font-size: 12px; font-weight: bold;">Empty Bags (Cost 25 Rs/- Per bag) : </span>&nbsp;<asp:Label ID="lblEmptyBag" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                                <div class="col-md-6" style="text-align: right;">
                                                    <span style="font-size: 12px; font-weight: bold;">Empty Bag Charge :</span>&nbsp;<asp:Label ID="lblEmptyBagCharge" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6" style="text-align: left;">
                                                    <span style="font-size: 12px; font-weight: bold;">Replace Item : </span>&nbsp;<asp:Label ID="lblReplaceItem" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                                <div class="col-md-6">
                                                </div>

                                            </div>
                                            <div class="row">

                                                <div class="col-md-6">
                                                </div>
                                                <div class="col-md-6" style="text-align: right;">
                                                    <span style="font-size: 12px; font-weight: bold;">TDS Amount : </span>&nbsp;<asp:Label ID="lblTDSAmount" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                </div>
                                                <div class="col-md-6" style="text-align: right;">
                                                    <span style="font-size: 12px; font-weight: bold;">Transportation Charge :</span>&nbsp;<asp:Label ID="lblTPC" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6" style="text-align: left;">
                                                    <span style="font-size: 12px; font-weight: bold;">Rs</span>&nbsp;<asp:Label ID="lblword" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                                <div class="col-md-6" style="text-align: right;">
                                                    <span style="font-size: 12px; font-weight: bold;">Grand Total</span>&nbsp;<asp:Label ID="lblgrand" runat="server" Text="0000001" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align: left;">
                                            <span style="text-decoration: underline; font-weight: bold;">Terms & Condition</span>
                                            <br />
                                            <div class="col-md-12">
                                                <div class="col-md-6" style="text-align: left;">
                                                    All diputes subject to 'SEHORE' Jurisdication only.<br />
                                                    BANK- AXIX BANK LTD, BRANCH A/C -913010056548697<br />
                                                    IFSC-UTIC0000684, SEHORE (M.P.)
                                                </div>
                                                <div class="col-md-6" style="text-align: right;">For&nbsp;<asp:Label ID="lblcfp" runat="server" Font-Bold="true"></asp:Label>)</div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-6" style="text-align: left;">Checked By -----------</div>
                                                <div class="col-md-6" style="text-align: right;">Authorised signatory</div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-default" id="btnclose" onclick="Closeeport()">Close</button>
                    <button type="submit" class="btn btn-primary" onclick="printDiv('printableArea')">Print</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        function onlyNumberKey(evt) {

            // Only ASCII charactar in that range allowed 
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

