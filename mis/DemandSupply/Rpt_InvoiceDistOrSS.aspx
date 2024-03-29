﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_InvoiceDistOrSS.aspx.cs" Inherits="mis_DemandSupply_Rpt_InvoiceDistOrSS" %>

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

                                    <div class="col-md-3">
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

                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Location <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
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

                                    <div class="col-md-3" id="pnldistorss" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Distributor Name <%--<span style="color: red;"> *</span>--%></label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Distributor/Superstockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Superstockist Name !'></i>"
                                                    ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="pnlInstitution" runat="server" visible="false">
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
                            <h3 class="box-title">Invoice Details</h3>
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
                                                <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-outline" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="View" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                               &nbsp; <asp:LinkButton ID="lnkReject" CssClass="btn btn-danger btn-outline" OnClientClick="return confirm('Are you sure to Reject Order ?')" Visible='<%#Eval("MilkOrProductInvoiceId").ToString()=="" ? false : true %>' CommandName="RecordReject" CommandArgument='<%#Eval("MilkOrProductInvoiceId") %>' runat="server"><i class="fa fa-trash"></i> Reject</asp:LinkButton>
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
                                <div style="height: 350px; overflow: scroll;">
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
                                                            <div class="col-md-12" style="display:none">
                                                                <b>No.<asp:Label ID="lblDelivarydate" runat="server"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <b>Invoice No.<asp:Label ID="lblInvoiceNo" runat="server"></asp:Label></b>
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
                                                                            <asp:TemplateField HeaderText="Qty (In Pkt)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyTotalQty")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Advanced Card Qty (In Pkt.)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalAdvCardQty" Text='<%# Eval("TotalAdvCardQty")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Return Qty (In Pkt.)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalReturnQty" Text='<%# Eval("TotalReturnQty")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Inst Qty (In Pkt.)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalInstSupplyQty" Text='<%# Eval("TotalInstSupplyQty")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Advanced Card Qty (In Ltr.)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalAdvCardQtyInLtr" Text='<%# Eval("TotalAdvCardQtyInLtr")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Advanced Card Margin">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAdvCardComm" Text='<%# Eval("AdvCardComm")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Advanced Card Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAdvCardAmt" Text='<%# Eval("AdvCardAmt")%>' runat="server" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotalAdvCardAmt" runat="server"></asp:Label>
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
                                                                                    <asp:Label ID="lblPaybleAmount" Text='<%# Eval("PaybleAmount")%>' runat="server" />
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
                                                            Tds on Sales @ <asp:Label ID="lblTdsTax" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTdsTaxAmt" Font-Bold="true" runat="server"></asp:Label>
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
    </div>
        <!-- /.content -->
    <section class="content">
        <div id="Print" runat="server" class="NonPrintable"></div>
    </section>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

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
