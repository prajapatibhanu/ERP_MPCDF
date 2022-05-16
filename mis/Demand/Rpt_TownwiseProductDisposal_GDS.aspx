<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_TownwiseProductDisposal_GDS.aspx.cs" Inherits="mis_Demand_Rpt_TownwiseProductDisposal_GDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        .NonPrintable {
                  display: none;
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
           padding: 5px ;
           
           font-size:15px;
           text-align:center;
           
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
               .pagebreak { page-break-before: always; }
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
          .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
    padding: 1px 2px 1px 1px;
    font-size: 10px;
    vertical-align: middle;
    text-align: right;
}
             .table > thead > tr > td {
    padding: 1px 2px 1px 1px;
    font-size: 11px;
    vertical-align: middle;
    text-align: left;
    font-weight:600;
    
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Townwise Product Disposal Report</h3>


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
                                    <legend>Date,Section,Variant
                                    </legend>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter FromDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter FromDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter ToDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                  
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />
                                        </div>
                                    </div>
                                    
                                </fieldset>
                            </div>
                            <div class="row">
                                <div class="col-md-2" style="margin-top: 20px;">
                                       <div class="form-group">
                                        <asp:Button ID="btnPrintRoutWise" Visible="false" runat="server" CssClass="btn btn-success" Text="Print" OnClick="btnPrintRoutWise_Click" />
                                            <asp:Button ID="btnExportAll" Visible="false" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export"  />
                                           </div>
                                </div>
                            <div class="row">
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
            </div>
        </section>

        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>   
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('.loader').fadeOut();
            $('.loader').fadeOut();
            <%--$("#<%=btnSearch.ClientID%>").click((function () {
                 $('.loader').show();
            }));--%>

            function ValidatePage() {

                if (Page_IsValid) {

                    $('.loader').fadeOut();
                    return false;
                }

            }
            // }

        });
        </script>
    <script>

        $(function () {
            $('[id*=ddlItemName]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
         </script>
</asp:Content>
