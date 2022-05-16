<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_InvoiceDistOrInst_JBL.aspx.cs" Inherits="mis_DemandSupply_Rpt_InvoiceDistOrInst_JBL" %>

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
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content no-print">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12 no-print">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Invoice</h3>
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
                                            <label>Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDeliveryDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDeliveryDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" id="pnlitemcategory" visible="false">
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
                                            <label>Location <%--<span style="color: red;">*</span>--%></label>
                                            <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>--%>
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift </label>
                                            <%--  <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Invoice For<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvif" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Invoice For" Text="<i class='fa fa-exclamation-circle' title='Select Invoice For !'></i>"
                                                    ControlToValidate="ddlInvoiceFor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlInvoiceFor" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceFor_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Distributor" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Institution" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2" id="pnldistorss" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Distributor Name <%--<span style="color: red;"> *</span>--%></label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Distributor Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor Name !'></i>"
                                                    ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="pnlInstitution" runat="server" visible="false">
                                        <div class="form-group">
                                            <%--   <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Institution" Text="<i class='fa fa-exclamation-circle' title='Select Institution !'></i>"
                                                    ControlToValidate="ddlInstitution" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <label>Institution <%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlInstitution" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
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
                            <h3 class="box-title">View Invoice Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                               
                            </div>
                        </div>
                        <div class="row no-print">
                           
                                 <div class="col-md-1 pull-right">
                                     <div class="form-group">
                                    <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                         </div>
                                </div>
                            <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                    AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>Total</b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistName" Text='<%# Eval("DistName")%>' runat="server" />
                                                <asp:Label ID="lblDistributorId" Visible="false" Text='<%# Eval("DistributorId")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRName" Text='<%# Eval("RName")%>' runat="server" />
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
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payble Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalPaybleAmount" Text='<%# Eval("TotalPaybleAmount")%>' runat="server" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblPAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="View" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-eye"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkEdit" CssClass="btn btn-info" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="EditRecord" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp; <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Reject Order ?')" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="RecordReject" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="GridView2" runat="server" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                    AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound" EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="2px" ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>Total</b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Institution">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                <asp:Label ID="lblBoothId" Visible="false" Text='<%# Eval("BoothId")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRName" Text='<%# Eval("RName")%>' runat="server" />
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
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalPaybleAmount" Text='<%# Eval("TotalPaybleAmount")%>' runat="server" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblPAmount" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-outline" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="View" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-eye"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkEdit" CssClass="btn btn-info" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="EditRecord" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp; <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Reject Order ?')" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="RecordReject" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-trash"></i></asp:LinkButton>
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
                                                                       <%--  <asp:Label ID="lblInstTotalAmt" Text='<%# Eval("Institute_Amount") %>' runat="server" />--%>
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
                                                                       <%-- <asp:Label ID="lblRatePerLtr" Text='<%# Convert.ToDecimal(Eval("RatePerLtr"))+(Convert.ToDecimal(Eval("DistOrSSComm"))+Convert.ToDecimal(Eval("TransComm")))%>' runat="server" />--%>
                                                                    <asp:Label ID="lblRatePerLtr" Text='<%# Eval("RatePerLtrNew")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" Text='<%# Eval("BillingAmountNew")%>' runat="server" />
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

                                                                    <asp:GridView ID="GridView4" runat="server" ShowFooter="true" class="table table-striped table-bordered" AllowPaging="false"
                                                                        AutoGenerateColumns="False" EmptyDataText="No Record Found" OnRowDataBound="GridView4_RowDataBound" EnableModelValidation="True">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Particulars">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <b>Total</b>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty(In Pkt)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Return Qty (In Pkt.)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalReturnQty" Text='<%# Eval("TotalReturnQty")%>' runat="server" />
                                                                                </ItemTemplate>
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
                                                                            <asp:TemplateField HeaderText="Rate (Per Ltr)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRatePerLtr" Text='<%# Eval("RatePerLtr")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Payble Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPaybleAmount" Text='<%# Eval("PaybleAmount")%>' runat="server" />
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
    
            <div class="modal" id="ItemDetailsModalInvoice">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="modalDistNameInvoice" style="color: red" runat="server"></span>&nbsp;&nbsp;
                                     <%--  &nbsp;&nbsp;Route :<span id="modalroutenameInvoice" style="color: red" runat="server"></span>
                                &nbsp;&nbsp;Challan No : <span id="modalChallanNoInvoice" style="color: red" runat="server"></span>
                                &nbsp;&nbsp; Date :<span id="modalreturndelivarydateInvoice" style="color: red" runat="server"></span>
                                &nbsp;&nbsp; Shift :<span id="modalshiftInvoice" style="color: red" runat="server"></span></h4>--%>
                        </div>
                        <div class="modal-body">
                            <div id="div2" runat="server">
                                <asp:Label ID="lblModalMsgInvoice" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details for Edit Invoice</legend>
                                            <div class="row" style="height: 280px; overflow: scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView5" runat="server" OnRowCommand="GridView5_RowCommand" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="MilkOrProductInvoiceChildId">
                                                            <Columns>
                                                                        <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Name / वस्तु नाम">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIName" Text='<%# Eval("IName")%>' runat="server" />
                                                                                <asp:Label ID="lblAdjustmentUpdatedBy" Visible="false" Text='<%# Eval("AdjustmentUpdatedBy")%>' runat="server" />
                                                                                <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                                  <asp:Label ID="lblRatePerLtr" Visible="false" Text='<%# Eval("RatePerLtr")%>' runat="server" />
                                                                                  <asp:Label ID="lblDisttransportRate_oncashsale" Visible="false" Text='<%# Eval("DisttransportRate_oncashsale")%>' runat="server" />
                                                                                <asp:Label ID="lblSupplyTotalQty" Visible="false" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                                  <asp:Label ID="lblSupplyTotalQtyInLtr" Visible="false" Text='<%# Eval("SupplyTotalQtyInLtr")%>' runat="server" />
                                                                                 <asp:Label ID="lblBillingQty" Visible="false" Text='<%# Eval("BillingQty")%>' runat="server" />
                                                                                  <asp:Label ID="lblBillingQtyInLtr" Visible="false" Text='<%# Eval("BillingQtyInLtr")%>' runat="server" />
                                                                                <asp:Label ID="lblBillingAmount" Visible="false" Text='<%# Eval("BillingAmount")%>' runat="server" />
                                                                                  <asp:Label ID="lblPaybleAmount" Visible="false" Text='<%# Eval("PaybleAmount")%>' runat="server" />
                                                                                <asp:Label ID="lblTcsTaxPer" Visible="false" Text='<%# Eval("TcsTaxPer")%>' runat="server" />
                                                                                <asp:Label ID="lblAdvCardAmt" Visible="false" Text='<%# Eval("AdvCardAmt")%>' runat="server" />
                                                                                 <asp:Label ID="lblInstTransCommAmt" Visible="false" Text='<%# Eval("InstTransCommAmt")%>' runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Supply Qty./ कुल आपूर्ति मात्रा">
                                                                            <ItemTemplate>
                                                                          <asp:TextBox runat="server" autocomplete="off" ReadOnly="true" Text='<%# Eval("SupplyTotalQty")%>' CssClass="form-control" ID="txtSupplyTotalQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Total Supply Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField>
                                                                              <HeaderTemplate>
                                                                                  Adjustment Qty./ मात्रा
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                               <asp:Label ID="lblAdjustmentQty" Text='<%# Eval("AdjustmentQty")%>' runat="server" />
                                                                                  <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                                ErrorMessage="Enter Adjusment Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Adjusment Qty. !'></i>"
                                                                                                ControlToValidate="txtAdjustmentQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="reAdjustmentqty" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Valid Number In Adjusment Qty Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Adjusment Qty Field !'></i>" ControlToValidate="txtAdjustmentQty"
                                                                                    ValidationExpression="^-?[1-9]+([0-9]+)*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                                                         <asp:TextBox runat="server" Visible="false" autocomplete="off" Text='<%# Eval("AdjustmentQty")%>' CausesValidation="true" CssClass="form-control" ID="txtAdjustmentQty" MaxLength="8" onpaste="return false;"  placeholder="Enter Adjusment Qty." ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Remark/ टिप्पणी">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarkAtAdjustment" Text='<%# Eval("RemarkAtAdjustment")%>' runat="server" />
                                                                                 <asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" CssClass="form-control" ID="txtRemarkAtAdjustment" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action / कार्यवाही करें">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" Visible='<%# ( Eval("AdjustmentUpdatedBy").ToString()=="" ? true:false) %>' CommandArgument='<%#Eval("MilkOrProductInvoiceChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;  <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductInvoiceChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                                &nbsp;&nbsp;&nbsp;  <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("MilkOrProductInvoiceChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
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
                        <div class="modal-footer">
                            <div class="row">
                               <%-- <div class="col-md-2" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ItemReplace" OnClientClick="return ValidatePage1();" ID="btnReplaceSubmit" Text="Save" />
                                    </div>
                                </div>--%>
                               <%-- <div class="col-md-2 pull-right" style="margin-top: 20px;">

                                    <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('Are you sure to Final Save?')" OnClick="lnkFinalSubmitReplace_Click" CssClass="btn btn-success" Text="Final Save" runat="server"></asp:LinkButton>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
         </section>
    </div>
        <!-- /.content -->
    <section class="content">
        <div id="Print" runat="server" class="NonPrintable"></div>
    </section>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <asp:Label  ID="lblremark" Visible="false" runat="Server"></asp:Label>

    <script type="text/javascript">

        //alert($('.printtext').attr('title'));
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function myInvoiceItemModal() {
            $("#ItemDetailsModalInvoice").modal('show');

        }
        function Print() {
            debugger;
            window.print();

        }

        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
    </script>
</asp:Content>

