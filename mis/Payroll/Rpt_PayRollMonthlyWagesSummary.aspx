<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_PayRollMonthlyWagesSummary.aspx.cs" Inherits="mis_Payroll_Rpt_PayRollMonthlyWagesSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
    
        .header {
  padding : 20px 0 20px 0;
  margin-bottom:20px;
  overflow :auto;
  
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
              @page {
                size: portrait;
            }
               @page {
                margin: 0 0 0 0;
            }
               .pagebreak { page-break-before: always; }
              
            }
          }
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }
        .thead
        {
            display:table-header-group;
        }
        .text-center{
            text-align: center;
        }
        .text-right{
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
            .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 12px;
    border: 1px solid black !important;
    font-family: verdana;
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
                                    <h3 class="box-title" id="Label1">WAGES SUMMARY</h3>
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



