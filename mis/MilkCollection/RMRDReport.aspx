<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRDReport.aspx.cs" Inherits="mis_MilkCollection_RMRDReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        @media print {
             
              .noprint {
                display: none;
            }
                table tfoot {
                display: table-row-group;
            } 
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                <div class="box-header noprint">
                    <h3 class="box-title">RMRD Report</h3>
                </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row noprint">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtToDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root</label>
                                        <%--<span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator144" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>--%>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                        <div class="col-md-12">
                            <div class="table-responsive">

                            <asp:GridView ID="gv_MilkCollectionChallanEntryDetails" ShowFooter="true" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="datatable table table-bordered" runat="server">
                                <Columns>
                                     <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("EntryDate"))).ToString("dd/MM/yyyy") %>'></asp:Label>                                            
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Society">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                            </ItemTemplate>                                          
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
      <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
   <script src="js/buttons.colVis.min.js"></script>
   <%-- <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100],
            iDisplayLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
			  "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true,
                footer:true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('RMRD Challan Entry Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('RMRD Challan Entry Details').bold().fontsize(3).toUpperCase(),
                    filename: 'RMRDChallanEntryDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true
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
</script>
</asp:Content>

