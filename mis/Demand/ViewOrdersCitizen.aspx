<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ViewOrdersCitizen.aspx.cs" Inherits="mis_Demand_ViewOrdersCitizen" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #F5BB3E !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentBody" runat="Server">

  <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
             <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
            <!-- SELECT2 EXAMPLE -->
            <div class="row">

           
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">View Order Report / आर्डर रिपोर्ट देखें </h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        
                        <div class="row" id="pnldata" runat="server" visible="false">

                                <div class="col-lg-12">
                                    <fieldset>
                                        <legend>Citizen Demand Details</legend>

                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." 
                                                EnableModelValidation="True" DataKeyNames="MobileNo">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                             <asp:Label ID="lblCitizenName" Visible="false" Text='<%# Eval("CitizenName")%>' runat="server" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="InVoice No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblInvoiceNo" CommandName="ItemOrdered" CommandArgument='<%#Eval("MobileNo") %>' Text='<%#Eval("InvoiceNo") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                     <asp:TemplateField HeaderText="Demand Date">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblDemand_Date" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Shift">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblDeliveryShift_id" Visible="false" Text='<%# Eval("DeliveryShift_id")%>' runat="server" />
                                                             <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delivery Date / वितरण दिनांक ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivery_Date" Text='<%# Eval("Delivery_Date","{0:dd/MM/yyyy}")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Payment Confirmation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivaryShift" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Approval Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-xs btn-outline"
                                                                 CommandName="ItemOrdered" CommandArgument='<%#Eval("MobileNo") %>'
                                                                 runat="server"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                         <%--<asp:TemplateField HeaderText="Demand Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDStatus" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Details" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" CssClass="btn btn-primary btn-primary" Visible='<%# Eval("DStatus").ToString() == "Yes" ? true : false %>' CommandName="ItemOrdered" CommandArgument='<%#Eval("tmp_MilkOrProductDemandId") %>' runat="server"><i class="fa fa-eye"></i> View Details</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                                <h4 class="modal-title">Citizen Name:<span id="modalBoothName" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Date :<span id="modaldate" style="color: red" runat="server"></span>&nbsp;&nbsp;Order Shift : <span id="modelShift" style="color: red" runat="server"></span></h4>
                                            </div>
                                            <div class="modal-body">
                                                <div id="divitem" runat="server">
                                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                                <div style="height: 250px; overflow: scroll;">
                                                                    <div class="col-md-12">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="GridView2" runat="server" 
                                                                                OnRowDataBound="GridView2_RowDataBound" 
                                                                                EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                                                AutoGenerateColumns="False" EnableModelValidation="True"
                                                                                 DataKeyNames="MobileNo">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SNo./ क्र." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग">
                                     <ItemTemplate>
                                           <asp:Label ID="lblMobileNo" runat="server" visible="false" Text='<%# Eval("MobileNo") %>' />
                                        <asp:Label ID="lblItemCat_id" runat="server" visible="false" Text='<%# Eval("ItemCat_id") %>' />
                                          <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                                   <asp:TemplateField HeaderText="Item Name/ वस्तु नाम">
                                     <ItemTemplate>

                                        <asp:Label ID="lblItemType_id" runat="server"  visible="false"  Text='<%# Eval("ItemType_id") %>' />
                                         <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Variant Name/ वस्तु नाम">
                                     <ItemTemplate>
                                          <asp:Label ID="lblItem_id" runat="server"  visible="false"  Text='<%# Eval("Item_id") %>' />
                                        <asp:Label ID="lblItemVarName" runat="server" Text='<%# Eval("ItemName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity/ मात्रा">
                                    <ItemTemplate>

                                        <asp:Label ID="lblQtyInNo" runat="server" Text='<%# Eval("QtyInNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate/ दाम(in Rs.)">
                                     <ItemTemplate>

                                        <asp:Label ID="lblCTotalAmount" runat="server" Text='<%# Eval("CTotalAmount") %>' />
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
           paging: false,
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
                   exportOptions: {
                       columns: [0, 1, 2, 3, 4, 5, 6, 7]
                   },
                   footer: false,
                   autoPrint: true
               }, {
                   extend: 'excel',
                   filename: 'Ordered Report',
                   text: '<i class="fa fa-file-excel-o"></i> Excel',

                   exportOptions: {
                       columns: [0, 1, 2, 3, 4, 5, 6, 7]
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

