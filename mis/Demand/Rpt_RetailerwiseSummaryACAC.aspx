<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_RetailerwiseSummaryACAC.aspx.cs" Inherits="mis_Demand_Rpt_RetailerwiseSummaryACAC" %>

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
                            <h3 class="box-title">Retailer Supply Summary</h3>


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
                                    <legend>Date,Location ,Route
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
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route <span style="color: red;"> *</span></label>
                                              <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="ddlRoute"
                                                    ErrorMessage="Select Route" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                  </span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Retailer <span style="color: red;"> *</span></label>
                                         
                                            <asp:ListBox runat="server" ID="ddlRetailer" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" OnClientClick="return ValidatePage();" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                  
                                    
                                </fieldset>
                            </div>
                            <div class="row">
                                <div class="col-md-2" style="margin-top: 20px;">
                                       <div class="form-group">
                                        <asp:Button ID="btnPrint" runat="server" Visible="false" OnClientClick="window.print()" Text="Print" CssClass="btn btn-primary" />
                                           <asp:Button ID="btnExcel" CssClass="btn btn-primary" Visible="false" Text="Excel" runat="server" OnClick="btnExcel_Click" />
                                           </div>
                                </div>
                            <div class="row">
                                 <div class="col-md-12">
                            <div class="col-md-12">

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
        function Print() {
            debugger;
            $("ctl00_ContentBody_btnPrint").show();
            window.print();
        }
        </script>

    <script>

        $(function () {
            $('[id*=ddlRetailer]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
         </script>
</asp:Content>

