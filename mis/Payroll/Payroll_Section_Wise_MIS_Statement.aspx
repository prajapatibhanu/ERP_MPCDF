<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Payroll_Section_Wise_MIS_Statement.aspx.cs" Inherits="mis_Payroll_Payroll_Section_Wise_MIS_Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }
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
               .pagebreak { page-break-before: always; }
              
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


        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 1px;
            font-size: 14px;
            font-family: verdana;
        }

        .float_right {
            text-align: right;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border-bottom: 1px solid #9e9e9ea3 !important;
            border-right: 1px solid #9e9e9ea3 !important;
        }

        .table-bordered {
            border: 1px solid #9e9e9ea3;
        }

            thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
                border: 1px solid #9e9e9e5c !important;
            }



          @media print {
        .float_right {
            text-align: right !important;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
      <div class="loader"></div>
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label1">Section wise MIS Statement</h3>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice_Name" CssClass="form-control" ClientIDMode="Static" Enabled="false">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                         <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="a" ControlToValidate="ddlFinancialYear"
                                                    ErrorMessage="Select Year" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                             </span>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                      
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                          <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" ValidationGroup="a" ControlToValidate="ddlMonth"
                                                    ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                             </span>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-md-2" id="pnlshow" runat="server" visible="false" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" OnClientClick="window.print()" />
                                        <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_page_content" runat="server" class="page_content"></div>
                                  </div>
                                </div>
                           
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
          <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div> 
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.loader').fadeOut();
            $("#<%=btnShow.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));

        });
        
        </script>
</asp:Content>

