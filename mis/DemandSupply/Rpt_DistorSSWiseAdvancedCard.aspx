<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_DistorSSWiseAdvancedCard.aspx.cs" Inherits="mis_DemandSupply_Rpt_DistorSSWiseAdvancedCard" %>

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
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Advanced Card Report </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Advanced Card Report
                                </legend>
                                 <div class="row">
                                          <div class="col-md-12">
                                               <div class="form-group">
                                        <span style="color:red">Note: From Month Should be less than To Month</span>
                                                   </div>
                                              </div>
                                    </div>
                                <div class="row">
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Month<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter From Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Month !'></i>"
                                                    ControlToValidate="txtFromMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromMonth" MaxLength="10" placeholder="Enter From Month" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Month<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter To Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter To Month !'></i>"
                                                    ControlToValidate="txtToMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToMonth" MaxLength="10" placeholder="Enter To Month" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                         <asp:Button ID="btnPrintRoutWise" runat="server" Visible="false" CssClass="btn btn-primary pull-right" Text="Print" OnClick="btnPrintRoutWise_Click" />
                                            </div>
                                    </div>

                            <div class="col-md-12">
                                 
                                 <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Distributor/Superstockist">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDistributorId" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Distributor/Superstockist") %>' CommandName="DistwiseRetailer" CommandArgument='<%#Eval("DistributorId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>

                               <%--<div id="divtable" runat="server"></div>--%>
                                            </div>
                            </div>
                        </div>
                            </fieldset>
                        </div>
                        
                    </div>
                </div>
            </div>
            <div class="modal" id="ItemDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="modalRootOrDistName" style="color:red" runat="server"></span>&nbsp;&nbsp;Date :<span id="modaldate" style="color:red" runat="server"></span>&nbsp;&nbsp;Shift : <span id="modelShift" style="color:red" runat="server"></span></h4>
                        </div>
                        <div class="modal-body">
                            <div id="divitem" runat="server">
                                <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                <div class="row">
                                   <div class="col-md-12" id="pnlpopupdata" runat="server" visible="false">
                                        <fieldset>
                                            <legend>Item Details</legend>
                                            <div class="row" style="height:250px;overflow:scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                      <asp:GridView ID="GridView4" runat="server" ShowFooter="true" OnRowDataBound="GridView4_RowDataBound" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                          AutoGenerateColumns="true" EmptyDataText="No Record Found."  EnableModelValidation="True">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Retailer Name" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBandOName" Text='<%# Eval("BName") %>' runat="server" />
                                                                        </ItemTemplate>
                                                       </asp:TemplateField>
                                                </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>                                          
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                            <button type="button" class="btn btn-primary" id="btnConsRoutePrint" runat="server" visible ="false" onclick="Print();">Consolidated Print </button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
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
         $("#txtFromMonth").datepicker({
             format: "mm/yyyy",
             viewMode: "months",
             minViewMode: "months",
             autoclose: true,
         });

         $("#txtToMonth").datepicker({
             format: "mm/yyyy",
             viewMode: "months",
             minViewMode: "months",
             autoclose: true,
         });

         function myItemDetailsModal() {
             $("#ItemDetailsModal").modal('show');

         }
         function Print() {
             debugger;

             window.print();

         }
    </script>
</asp:Content>
