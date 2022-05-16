<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_SupplimentryBill.aspx.cs" Inherits="mis_Payroll_Rpt_SupplimentryBill" %>

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
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row noprint">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Supplymentary Bill Report</h3>


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
                                    <legend>Office Name,Month,Year
                                    </legend>
                                     <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name</label>
                              
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Year<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" ControlToValidate="ddlYear"
                                                    ErrorMessage="Select Year" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Month<span style="color: red;">*</span></label>
                                              <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" ControlToValidate="ddlMonth"
                                                    ErrorMessage="Select Month" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" ClientIDMode="Static">
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
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-block btn-primary" ValidationGroup="a"  />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                   <div class="col-md-2" style="margin-top: 20px;">
                                       <div class="form-group">
                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Visible="false" Text="Print" CssClass="btn btn-primary" />
                                           <asp:Button ID="btnExcel" OnClick="btnExcel_Click" CssClass="btn btn-success" Visible="false" Text="Excel" runat="server" />
                                           </div>
                                </div>
                                    
                                </fieldset>
                            </div>
                     <div>
                         
                     </div>
                    </div>
                </div>
                    </div>
            </div>
            <div class="row">
                         <div class="col-md-12">
                                 <div class="table-responsive">
                            <div class="col-md-12">

                                <div id="div_page_content" runat="server" class="page_content"></div>
                            </div>
                                     </div>
                        </div>
                             </div>
        </section>
        <%-- <section class="content">
            <div id="divprint" runat="server" class="NonPrintable"></div>   
        </section>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.loader').fadeOut();
            $("#<%=btnSearch.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));

        });
        </script>
</asp:Content>