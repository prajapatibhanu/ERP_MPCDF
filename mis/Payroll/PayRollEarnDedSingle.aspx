<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollEarnDedSingle.aspx.cs" Inherits="mis_Payroll_PayRollEarnDedSingle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements  -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label1">Set Single Head Earning/Deduction </h3>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <!----1---->

                                <div class="col-md-4">
                                    <div class="error-content">
                                        <h4 style="color: blue;">Loans & Contributions
                                           <%-- <asp:HyperLink ID="hlLoanContribution" NavigateUrl="~/mis/Payroll/PayRollEarnDedSingleLoan.aspx" runat="server" Target="_blank" CssClass="btn btn-flat btn-info btn-xs">Click Here</asp:HyperLink></h4>--%>
											<asp:HyperLink ID="hlLoanContribution" NavigateUrl="~/mis/Payroll/PayRollEarnDedSingleLoanAuto.aspx" runat="server" CssClass="btn btn-flat btn-info btn-xs">Click Here</asp:HyperLink></h4>
                                        <p>
                                            <asp:Label ID="lblLoanContribution" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <!---2----->
                                <div class="col-md-4">
                                    <div class="error-content">
                                        <h4 style="color: blue;">Auto Calculated Heads
                                            <asp:HyperLink ID="hlAutoCal" NavigateUrl="~/mis/Payroll/PayRollEarnDedSingleAuto.aspx" runat="server" Target="_blank" CssClass="btn btn-flat btn-info btn-xs">Click Here</asp:HyperLink></h4>
                                        <p>
                                            <asp:Label ID="lblAutoCal" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <!---3----->
                                <div class="col-md-4">
                                    <div class="error-content">
                                        <h4 style="color: blue;">Remaining Single Heads
                                            <asp:HyperLink ID="htSingleHead" NavigateUrl="~/mis/Payroll/PayRollEarnDedSingleOther.aspx" runat="server" Target="_blank" CssClass="btn btn-flat btn-info btn-xs">Click Here</asp:HyperLink></h4>
                                        <p>
                                            <asp:Label ID="lblSingleHead" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

