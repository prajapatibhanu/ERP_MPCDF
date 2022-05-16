<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EngneeringSectionEntryReport.aspx.cs" Inherits="mis_EngneeringDepartment_EngneeringSectionEntryReport" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint" >
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Engineering Section Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Year" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                ControlToValidate="ddlYear" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                     </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Month<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Month" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                ControlToValidate="ddlMonth" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem>January</asp:ListItem>
                                            <asp:ListItem>February</asp:ListItem>
                                            <asp:ListItem>March</asp:ListItem>
                                            <asp:ListItem>April</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>June</asp:ListItem>
                                            <asp:ListItem>July</asp:ListItem>
                                            <asp:ListItem>August</asp:ListItem>
                                            <asp:ListItem>September</asp:ListItem>
                                            <asp:ListItem>October</asp:ListItem>
                                            <asp:ListItem>November</asp:ListItem>
                                            <asp:ListItem>December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="margin-top: 20px;" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnExport" Visible="false" runat="server" Text="Export to Excel" Style="margin-top: 20px;" CssClass="btn btn-warning btn-block" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="pnlData" runat="server" visible="false">

                    <div class="box box-Manish">
                        <%--<div class="box-header">
                            <h3 class="box-title">Milk Or Product  Dues Report</h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-1 pull-right">
                                    <div class="form-group">
                                         <asp:Button ID="btnExportAll" Visible="false" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnprint_Click" />
                                       
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="misdetail" runat="server" class="page_content"></div>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                </div>
            </div>
        </section>
         <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div> 
        </section>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   <script type="text/javascript">
        function Print() {
            debugger;
            $("#ctl00_ContentBody_Print").show();
            $("#ctl00_ContentBody_Print1").hide();
            window.print();
            $("#ctl00_ContentBody_Print1").hide();
            $("#ctl00_ContentBody_Print").hide();
        }
    </script>
</asp:Content>

