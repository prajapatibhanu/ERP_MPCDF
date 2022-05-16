<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistInvoice_JBL_datewise.aspx.cs" Inherits="mis_DemandSupply_DistInvoice_JBL_datewise" %>

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
               .pagebreak { page-break-before: always; }
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
      <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content no-print">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Distributor Invoice Datewise </h3>
                            <div class="col-md-12">
                                     <div class="pull-right">
                                     <asp:HyperLink runat="server" href="Trn_MilkInvoiceDetailsRuntime_JDS.aspx" ToolTip="View Invoice Details" class="btn btn-primary"><i class="fa fa-eye"> View Invoice Details</i></asp:HyperLink>
                                       
                                    </div>
                                         </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>View Distributor Invoice Datewise
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row no-print">                                        
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label> From Date <span style="color: red;">*</span></label>
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
                                         <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txttoDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txttoDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txttoDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                            
                                    </div>                                   
                                    <div class="col-md-2" id="pnlcategory" runat="server" visible="false">
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
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Distributor Name <span style="color: red;"> *</span></label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Distributor/Superstockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Superstockist Name !'></i>"
                                                    ControlToValidate="ddlDitributor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                           <%-- <asp:DropDownList ID="ddlDitributor" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>--%>
                                            <asp:ListBox runat="server" ID="ddlDitributor" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                      
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="View" />
                                              <asp:Button ID="btnSave" Visible="false"  OnClick="btnSave_Click" runat="server" Text="Save & Print" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                
                                </div>

                                
                            </fieldset>

                        </div>

                    </div>
                </div>
                
            </div>
        </section>

        <section class="content">
            <div class="row no-print">
                <div class="col-md-1 pull-right">
                                        <div class="form-group">

                                           <%-- <asp:Button ID="btnPrint" runat="server" Visible="false" OnClientClick="window.print()" Text="Print" CssClass="btn btn-block btn btn-primary" />--%>
                                          
                                        </div>
                                    </div>
            </div>
            <div class="row">
                       

                        <div class="col-md-12">
                            <div class="col-md-12">

                                <div id="div_page_content" runat="server" class="page_content"></div>
                                 <div id="div_page_content1" runat="server" class="page_content"></div>
                            </div>
                        </div>
                    </div>
        </section>
        <!-- /.content -->
        <%-- <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>   
        </section>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        function Print() {
            debugger;
            $("ctl00_ContentBody_pnlprint").show();
            window.print();
        }

    </script>
<%--    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
   <%-- <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>

    <script>

        $(function () {
            $('[id*=ddlDitributor]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
         </script>
</asp:Content>
