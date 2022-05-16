<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_CitizenAdvancedCard_Distributor.aspx.cs" Inherits="mis_Demand_Rpt_CitizenAdvancedCard_Distributor" %>

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
                            <h3 class="box-title">Retailer Advanced Card Report </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Month,Shift
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
                                     <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                              <asp:Button ID="btnPrintRoutWise" runat="server" Visible="false" CssClass="btn btn-success" Text="Print" OnClick="btnPrintRoutWise_Click" />
                                            </div>
                                </div>
                                <div class="row">
                                     <div class="col-md-12">
                                          <div class="form-group">
                                               <div id="div_page_content" runat="server" class="page_content"></div>
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
    </script>
</asp:Content>

