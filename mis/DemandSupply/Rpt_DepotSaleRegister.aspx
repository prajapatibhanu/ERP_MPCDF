<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_DepotSaleRegister.aspx.cs" Inherits="mis_DemandSupply_Rpt_DepotSaleRegister" %>

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
                            <h3 class="box-title">Depot Sale Register</h3>


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
                                    <legend>Location ,Distributor,Month
                                    </legend>  
                                                                   
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                          
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Distributor <span style="color: red;"> *</span></label>
                                         <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="ddlDistributor"
                                                    ErrorMessage="Select Distributor" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Distributor !'></i>"
                                                    Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                             </span>
                                            <asp:DropDownList runat="server" ID="ddlDistributor" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
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

                                <div id="div_page_content" runat="server" class="page_content"></div>
                           
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
            $("ctl00$ContentBody$btnPrint").show();
            window.print();
        }
        </script>

    <script>
        $("#txtMonth").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
         </script>
</asp:Content>

