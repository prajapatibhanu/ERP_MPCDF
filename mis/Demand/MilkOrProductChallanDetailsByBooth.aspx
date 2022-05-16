<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="MilkOrProductChallanDetailsByBooth.aspx.cs" Inherits="mis_Demand_MilkOrProductChallanDetailsByBooth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #ffb4b4 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
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

        
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px dashed #000000 !important;
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
            <!-- SELECT2 EXAMPLE -->
            <div class="row">

           
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Challan Detail / चालान डिटैल </h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <fieldset>
                            <legend>Date ,Shift  / दिनांक ,शिफ्ट
                            </legend>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>


                            <div class="row">
                                 <div class="col-md-2">
                                 <div class="form-group">
                                <label>From Date /दिनांक से<span style="color: red;"> *</span></label>
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
                                <label>To Date / दिनांक तक<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" ControlToValidate="txtToDate"
                                        ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter ToDate !'></i>"
                                        Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                        ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter ToDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>    



                  

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift / शिफ्ट</label>
                                        <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                  <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category /वस्तु वर्ग</label>
                                            <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="col-md-1" style="margin-top: 22px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-1" style="margin-top: 22px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                                    </div>
                                </div>
                          </div>
                      

                        </fieldset>
                        <div class="row" id="pnldata" runat="server" visible="false">

                                <div class="col-lg-12">
                                    <fieldset>
                                        <legend>Challan Detail</legend>

                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand" EnableModelValidation="True" DataKeyNames="MilkOrProductDemandId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                             <asp:Label ID="lblItemCatid" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                            <asp:Label ID="lblDelivaryShiftid" Visible="false" Text='<%# Eval("DelivaryShift_id")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Retailer Name" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblBoothName" Text='<%# Eval("BoothName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Challan No">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkChallanNo" CommandName="ChallanNo" CssClass="btn btn-secondary" CommandArgument='<%#Eval("MilkOrProductDemandId") %>' Text='<%#Eval("ChallanNo") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   
                                                 <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" Text='<%# Eval("Delivery_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivaryShift" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="modal" id="ItemDetailsModal">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">×</span></button>
                                                <h4 class="modal-title">Item Details for <span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;Delivery Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Delivery Shift : <span id="modelShift" style="color: red" runat="server"></span>&nbsp;&nbsp; Catgory : <span id="modalcategory" style="color: red" runat="server"></span> &nbsp;&nbsp; Challan No : <span id="modalchallanno" style="color: red" runat="server"></span></h4>
                                            </div>
                                            <div class="modal-body">
                                                <div id="divitem" runat="server">
                                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                                <div style="height: 250px; overflow: scroll;">
                                                                    <div class="col-md-12">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="GridView2" runat="server" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                                AutoGenerateColumns="true" EnableModelValidation="True">
                                                                                  <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                                      </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                                                <button type="button" class="btn btn-primary" id="btnParlorWisePrint" runat="server"  onclick="window.print()">Print </button>
                                            </div>
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
   <script type="text/javascript">
       $('.datatable').DataTable({
           paging: true,
           iDisplayLength: 100,
           columnDefs: [{
               targets: 'no-sort',
               orderable: false
           }],
           dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
             '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
             '<"row"<"col-sm-5"i><"col-sm-7"p>>',
           fixedHeader: {
               header: true
           },
           buttons: {
               buttons: [{
                   extend: 'print',
                   text: '<i class="fa fa-print"></i> Print',
                   title: ('View Challan Report').bold().fontsize(5).toUpperCase(),
                   exportOptions: {
                       columns: [0, 1, 2, 3, 4, 5]
                   },
                   footer: false,
                   autoPrint: true
               }, {
                   extend: 'excel',
                   filename: 'Challan_Report',
                   title: ('View Challan Report').bold().fontsize(5).toUpperCase(),
                   text: '<i class="fa fa-file-excel-o"></i> Excel',

                   exportOptions: {
                       columns: [0, 1, 2, 3, 4, 5]
                   },
                   footer: false
               }],
               dom: {
                   container: {
                       className: 'dt-buttons'
                   },
                   button: {
                       className: 'btn btn-default'
                   }
               }
           }
       });
       function myItemDetailsModal() {
           $("#ItemDetailsModal").modal('show');

       }
    </script>
</asp:Content>
