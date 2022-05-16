<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Route_wise_onlineAdvanceCard_Report.aspx.cs" Inherits="mis_Demand_Route_wise_onlineAdvanceCard_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th {
            text-align: center;
        }

        .nonprintable {
            display: none;
        }

        @media print {
            .nonprintable {
                display: block;
            }

            .noprint {
                display: none;
            }
        }

        .exportborder {
            border: 1px solid black;
        }

        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }

        .NonPrintable {
            display: none;
        }

        .NonPrintable1 {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .NonPrintable1 {
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
            border: 1px dashed #000000 !important;
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

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
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
                            <h3 class="box-title">Online Advance Card Route Wise Report</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Advance Card Report
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Route<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Select Route" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList runat="server" ID="ddlRoute" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>

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
                                    <div class="col-md-2" style="padding-top:19px">
                                        <asp:Button runat="server" ID="btnsearch" ValidationGroup="a" OnClick="btnsearch_Click" CssClass="btn btn-primary" Text="Search"></asp:Button>
                                        <asp:Button runat="server" CssClass="btn btn-default" Text="Clear" ID="btnclear" OnClick="btnclear_Click"></asp:Button>
                                    </div>
                                    
                                </div>
                             </fieldset>
                    <div runat="server" id="divdatavisible">
                      
                            <span style="margin-left: auto">
                                <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                                <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="ExportToExcel"><i class="fa fa-file-excel-o ">&nbsp;Excel</i></asp:LinkButton>

                            </span>
                       
                        <div class="box-body">
                            <div class="table-responsive table">
                                <div id="div1" runat="server"></div>


                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <hr /> 
                                    <div runat="server" id="divtab2"></div>
                                </div>
                            </div>

                        </div>
                    </div>
                               

                        </div>
                    </div>
                </div>

            </div>
        </section>
        <section class="content nonprintable">

            <div id="printdiv" runat="server"></div>
            <hr />
            <div id="Sumprintdiv" runat="server"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $("#txtMonth").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
    </script>

</asp:Content>

