<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Trn_Rpt_Summary_Dtl_Routewise_Advance_card.aspx.cs" Inherits="mis_Demand_Trn_Rpt_Summary_Dtl_Routewise_Advance_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
            display: none;
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
            padding: 5px;
            font-size: 15px;
            text-align: center;
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

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 1px;
            font-size: 10px;
            border: 1px solid black !important;
            font-family: verdana;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
      <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Online Advance Card Route Wise Summary Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <fieldset>
                                    <legend>Monthly Report</legend>
                                    <div class="row">
                                        <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Month <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                    ControlToValidate="txtMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtMonth" MaxLength="8" placeholder="Select Month" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Location<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                        InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                        ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlLocation" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Route <span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Select Route" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                        ControlToValidate="ddlRout" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlRout" runat="server" CssClass="form-control select2"></asp:DropDownList>

                                            </div>
                                        </div>
                                   <%--     <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Retailer<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Select Area" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                                        ControlToValidate="ddlRetailer" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control "></asp:DropDownList>

                                            </div>
                                        </div>--%>
                                        <div class="col-md-2" style="padding-top: 19px">

                                            <asp:Button runat="server" ID="btnSearch" ValidationGroup="a" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                                          
                                            <asp:Button runat="server" CssClass="btn btn-default" Text="Clear" ID="btnClr" OnClick="btnClr_Click" />
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="row">
                                    <div class="col-md-2" style="margin-top: 50px;">
                                        <div class="form-group">
                                            <asp:LinkButton  ID="btnPrintRoutWise" Visible="false" runat="server" CssClass="btn fa fa-print btn-primary" Text="Print" OnClick="btnPrintRoutWise_Click" ></asp:LinkButton>
                                            <asp:LinkButton ID="btnExportAll" Visible="false" runat="server" CssClass="btn fa fa-file-excel-o btn-success" OnClick="btnExportAll_Click" Text="Export" ></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <%--<asp:GridView runat="server" ID="GvReport" CssClass="table table-bordered" AutoGenerateColumns="false" OnDataBound="GvReport_DataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="R_Name" HeaderText="Route" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="B_Name" HeaderText="Booth ID/Name" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="OrderId" HeaderText="Customer ID" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="Customer_Dtl" HeaderText="Customer Details" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="Product" HeaderText="Product" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="Packet_Size" HeaderText="Packet Size" ItemStyle-Width="150" />
                                                        <asp:BoundField DataField="ShiftName" HeaderText="Shift" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="ItemQty" HeaderText="Qty" ItemStyle-Width="150" />
                                                         <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="150" />

                                                    </Columns>
                                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                                </asp:GridView>--%>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div runat="server" id="htmDiv"></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 20px;">
                                            <div class="col-md-12">
                                                <div runat="server" id="DivHtml"></div>
                                            </div>
                                        </div>


                                    </div>

                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        //$(function () {
        //    $('[id*=ddlDitributor]').multiselect({
        //        includeSelectAllOption: true,
        //        includeSelectAllOption: true,
        //        buttonWidth: '100%',

        //    });


        //});

        $("#txtMonth").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
    </script>
</asp:Content>

