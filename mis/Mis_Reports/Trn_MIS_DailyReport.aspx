<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Trn_MIS_DailyReport.aspx.cs" Inherits="mis_Mis_Reports_Trn_MIS_DailyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }
             @page {
                size: landscape;
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
            padding: 5px;
            font-size: 15px;
            text-align: center;
        }
        
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
}
        .table1 td {
            word-break: break-word;
            padding:3px;
            min-width:90px !important;
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
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="mpr" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Daily MIS Report</h3>
                </div>
              

                <div class="box-body">
                    <div class="row">
                        <fieldset>
                            <legend>Daily MIS Report</legend>
                            <div class="row mainborder">
                                  <div class="col-md-12">
                                      <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="mpr" ControlToValidate="txtEntryDate"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="mpr" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtEntryDate" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                               <div class="col-md-3" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" ClientIDMode="Static" ValidationGroup="mpr" CssClass="btn btn-primary" Text="Search" runat="server"></asp:LinkButton>
                                        </div>
                                    </div>
                              
                            </div>
                        </fieldset>
                    </div>
                   
                </div>
            </div>
                </div>
            <div class="row" id="pnlData" runat="server" visible="false">
                          
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnprint_Click"  />
                                        <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                    </div>

                                </div>
         
                                <div class="col-md-12">
                                 <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content" runat="server" class="page_content"></div>
                                  </div>
                                     </div>
                                </div>
             
                                <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content1" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                              <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content2" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                                  <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content3" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                                  <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content4" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                                 <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content5" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content6" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content7" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>

                               <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content8" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>
                  <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content9" runat="server" class="page_content"></div>
                                  </div>
                                        </div>
                                </div>

                <div class="col-md-12">
                                    <div class="form-group">
                                    <div class="table-responsive">
                                        <div id="div_page_content10" runat="server" class="page_content"></div>
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

