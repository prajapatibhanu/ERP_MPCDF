<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_DistwiseInvoice_JBL.aspx.cs" Inherits="mis_DemandSupply_Rpt_DistwiseInvoice_JBL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }
            @page {
                size: landscape;
            }
            .pagebreak {
                page-break-before: always;
            }
        }

        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }

        .thead {
            display: table-header-group;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;

        }
        .lblpagenote {
            display: block;
            background: #e0eae7;
            text-align: -webkit-center;
            margin-bottom: 12px;
            padding: 10px;
        }   
        /*.table2 > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
    padding: 1px 2px 1px 1px;
    font-size: 10px;
    vertical-align: middle;
    text-align: right;
}
             .table2 > thead > tr > td {
    padding: 1px 2px 1px 1px;
    font-size: 11px;
    vertical-align: middle;
    text-align: left;
    font-weight:600;
        }*/ 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content no-print">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12 no-print">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Invoice Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Invoice Report
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row no-print">

                                    <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Enter From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                        ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                        ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Select From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        ErrorMessage="Enter To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter To Date !'></i>"
                                        ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                        ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <%--<asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" OnTextChanged="txtOrderDate_TextChanged" AutoPostBack="true" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Select To Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" Enabled="false" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                   
                                 
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn btn-default" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
                <div class="col-md-12" id="pnlData" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Invoice Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row no-print">
                                  <div class="col-md-1 pull-left">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="btnExport_Click" Text="Export" />
                            </div>
                        </div>
                                 <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                        AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Distributor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistName" Text='<%# Eval("DistName")%>' runat="server" />
                                                     <asp:Label ID="lblDistributorId" Visible="false" Text='<%# Eval("DistributorId")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivaryDate" Text='<%# Eval("Delivary_Date")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shift">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                </ItemTemplate>
                                                  <FooterTemplate>
                                                    <b>Total</b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalPaybleAmount" Text='<%# Eval("TotalPaybleAmount")%>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPAmount" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="View Details" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-outline" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="View" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                        </ItemTemplate>
                                               </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    
                                </div>
                                     </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="modal" id="ItemDetailsModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Invoice Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Shift : <span id="modelShift" style="color: red" runat="server"></span></h4>
                </div>
                <div class="modal-body">
                    <div id="divitem" runat="server">
                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                        <div class="row no-print">
                            <div class="col-md-12">
                                <div style="height: 400px; overflow: scroll;">
                                    <div class="box box-Manish">
                                        <div class="box-header">
                                            <h3 class="box-title">Invoice Details</h3>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body">


                                            <div class="row">

                                                <div class="col-md-2 pull-right">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary btn-block no-print" OnClientClick="Print()" />

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div id="pnlprint" runat="server" class="">
                                                    <div class="col-md-12">

                                                        <div class="row">
                                                            <div class="col-md-4">
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div style="text-align: center">
                                                                    <b><span style="text-align: center">
                                                                        <asp:Label ID="lblOName1" runat="server"></asp:Label><%--Bhopal Sahakari Dugdha Sang Maryadit--%></span><br />
                                                                        <span style="text-align: center">Bill Book</span><br />
                                                                        <span style="text-align: center">G.S.T/U.I.N NO:-
                                                                            <asp:Label ID="lblGST" runat="server"></asp:Label>
                                                                            <%--23AAAB0221D1ZE--%></span></b>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <b>No.<asp:Label ID="lblDelivarydate" runat="server"></asp:Label></b>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <b>Shri/M/s
                                            <asp:Label ID="lblMsName" runat="server"></asp:Label></b>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <b>
                                                                    <asp:Label ID="lblDelishift" runat="server" class="pull-left"></asp:Label><asp:Label class="pull-right" ID="lblVehicleNo" runat="server"></asp:Label></b>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <b>
                                                                    <asp:Label ID="lbldelidate" runat="server" class="pull-left"></asp:Label><asp:Label class="pull-right" ID="lblRouteName" runat="server"></asp:Label></b>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="GridView3" runat="server" ShowFooter="true" class="table table-striped table-bordered" AllowPaging="false"
                                                                        AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowDataBound="GridView3_RowDataBound" EnableModelValidation="True">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Particulars">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <b>Total</b>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty (In Pkt)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Return Qty (In Pkt.)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalReturnQty" Text='<%# Eval("TotalReturnQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Adv. Card Qty (In Pkt.)" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardQty" Text='<%# Eval("TotalAdvCardQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Adv. Card Price" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRatePerLtrAdCard" Text='<%# Eval("TotalAdvCardQty").ToString()=="0.000"? "" : Eval("RatePerLtrAdCard")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Total Adv. Card Amt" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardAmount" Text='<%# (Convert.ToDecimal(Eval("TotalAdvCardQtyInLtr")) * Convert.ToDecimal(Eval("RatePerLtrAdCard"))).ToString("0.000") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                       <FooterTemplate>
                                                                        <asp:Label ID="lblTAdvCAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Adv. Card Margin" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdvCardComm" Text='<%# Eval("AdvCardComm")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Adv. Card Margin Amt" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAdvCardAmt" Text='<%# Eval("AdvCardAmt")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalAdvCardAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Qty" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstQty" Text='<%# Eval("TotalInstSupplyQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Total Amt" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstTotalAmt" Text='<%# (Convert.ToDecimal(Eval("TotalInstSupplyQtyInLtr")) * Convert.ToDecimal(Eval("InstRatePerLtr"))).ToString("0.000") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                     <FooterTemplate>
                                                                        <asp:Label ID="lblFITAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Margin" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstTransComm" Text='<%# Eval("InstTransComm")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Inst. Tran Margin Amt" HeaderStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstTransCommAmt" Text='<%# Eval("InstTransCommAmt")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                        <FooterTemplate>
                                                                        <asp:Label ID="lblFInstMarAmt" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Billing Qty (In Pkt.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillingQty" Text='<%# Eval("BillingQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Billing Qty (In Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillingQtyInLtr" Text='<%# Eval("BillingQtyInLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate (Per Ltr.)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRatePerLtr" Text='<%# Eval("RatePerLtr")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" Text='<%# Eval("BillingAmount")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTAmount" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Payble Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaybleAmount" Text='<%# Eval("PaybleAmount") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalPAmount" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                            <div class="col-md-3 pull-right">
                                                <table class="table table1-bordered">
                                                    <tr>
                                                        <td>
                                                            Tcs on Sales @ <asp:Label ID="lblTcsTax" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTcsTaxAmt" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           Final Payble Amount
                                                        </td>
                                                        <td>
                                                           <asp:Label ID="lblFinalPaybleAmount" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                 
                                            </div>
                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <span class="pull-left">Prepared & Checked by </span><span class="pull-right">For 
                                                                    <asp:Label ID="lblOName2" runat="server"></asp:Label>
                                                                    <%--Bhopal Sahakari Dugdha Sang Maryadit--%></span>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div style="text-align: center; padding-top: 15px;">
                                                                    <span>General Manager (Marketing)/Asst.Gen.Manager (Marketing)</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row" style="padding-top: 8px;">
                                                            <div class="col-md-12">
                                                                <ul type="none">
                                                                    <li>Note: 1 . Bills not Paid within 15 Days of presentation will be liable an Interest @18% per annum.</li>
                                                                    <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2 . Please quote our Bill No. while remiting the amount.</li>
                                                                    <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3 . All Payment to be made by Bank Draft payable to
                                                                        <asp:Label ID="lblOName3" runat="server"></asp:Label></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                        <%-- </fieldset>--%>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
        </section>
        
        <!-- /.content -->
         <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <asp:Label  ID="lblremark" Visible="false" runat="Server"></asp:Label>
    <script type="text/javascript">

        //alert($('.printtext').attr('title'));
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function Print() {
            debugger;
            window.print();

        }

        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
    </script>
</asp:Content>

