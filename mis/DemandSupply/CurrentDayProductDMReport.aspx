<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CurrentDayProductDMReport.aspx.cs" Inherits="mis_DemandSupply_CurrentDayProductDMReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
            padding: 2px 5px;
           
        } 
         @media print {
              .NonPrintable {
                  display: block;
              }
              .noprint {
                display: none;
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


<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
      <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Current Day Demand Report </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Multi Demand Report
                                </legend>
                                <div class="row">
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDeliveryDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift </label>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                                            <label>Route</label>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                             <label>DM Type</label>
                                              <asp:ListBox runat="server" ID="ddlDMType" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">
                                                <asp:ListItem Text="Current Demand" Selected="True" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Current Ghee Demand" Selected="True" Value="2"></asp:ListItem>

                                              </asp:ListBox>
                                            </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click"  ValidationGroup="a" ID="btnSearch" Text="Search" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" Text="Reset" OnClick="btnClear_Click" CssClass="btn btn-block btn-secondary" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        
                    </div>
                </div>
            </div>
            
      
         <div class="row">
               <div class="col-md-12" id="pnlData" runat="server" visible="false">
                     
                         <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Multi Demand Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                   <div class="col-md-2 pull-right">
                                       <div class="form-group">
                                <asp:Button ID="btnPrintRoutWise" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrintRoutWise_Click" />
                                      <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export"  />
                                     </div>

                                   </div>
                                <div class="col-md-12">
                      
                      <div class="row">
                          <div class="col-md-12">
                              <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                    <Columns>
                                                         <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderId" Text='<%#Eval("OrderId") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Retailer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkRetailer" Text='<%#Eval("BName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFtext" Font-Bold="true" Text="Total Amt/Pkt" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost of Product">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCostOfProduct" Text='<%#Eval("Cost of Product") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                              <FooterTemplate>
                                                                  <asp:Label ID="lblFCostOfProduct" Font-Bold="true" runat="server"></asp:Label>
                                                             </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TcsTax Amt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTcsTaxAmt" Text='<%#Eval("TcsTax Amt") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                  <asp:Label ID="lblFTcsTaxAmt" Font-Bold="true" runat="server"></asp:Label>
                                                             </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Final Amt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFinalAmt" Text='<%#Eval("Final Amt") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                  <asp:Label ID="lblFFinalAmt" Font-Bold="true" runat="server"></asp:Label>
                                                             </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Total Pkt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalPkt" Text='<%#Eval("Total Pkt") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                  <asp:Label ID="lblFTotalPkt" Font-Bold="true" runat="server"></asp:Label>
                                                             </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                   <div id="divtable" runat="server"></div>
                                  </div>
                          </div>
                         <%-- <div class="col-md-12" id="pnltotalPDM" runat="server" visible="false">
                                            <label>Total DM :</label>
                                            <asp:Label ID="lblTotalSupplyValue" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                          <div class="col-md-12" id="pnltotalcost" runat="server" visible="false">
                                            <label>Cost Of Product. : </label>
                                            <asp:Label ID="lblCostOfProduct" Font-Bold="true" runat="server"></asp:Label>

                                        </div>--%>
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
             <div id="ExportAllData" runat="server"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             //$('.loader').fadeOut();
             $('.loader').fadeOut();
             $("#<%=btnSearch.ClientID%>").click((function () {

                 if (Page_IsValid) {
                     $('.loader').show();
                     return true;

                 }
             }));

         });
         function Print() {
             debugger;
             $("#ctl00_ContentBody_Print").show();
             $("#ctl00_ContentBody_Print1").hide();
             window.print();
             $("#ctl00_ContentBody_Print1").hide();
             $("#ctl00_ContentBody_Print").hide();
         }
         function Print1() {

             $("#ctl00_ContentBody_Print1").show();
             $("#ctl00_ContentBody_Print").hide();
             window.print();
             $("#ctl00_ContentBody_Print1").hide();
             $("#ctl00_ContentBody_Print").hide();
         }
         function myItemDetailsModal() {
             $("#ItemDetailsModal").modal('show');

         }
         $(function () {
             $('[id*=ddlDMType]').multiselect({
                 includeSelectAllOption: true,
                 includeSelectAllOption: true,
                 buttonWidth: '100%',

             });

         });
    </script>
</asp:Content>
