<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_CrateMgmtAtDistOrSuperStockist.aspx.cs" Inherits="mis_DemandSupply_Rpt_CrateMgmtAtDistOrSuperStockist" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Crate Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Date, Shift, Category / दिनांक, शिफ्ट, वर्ग
                                </legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date /दिनांक से<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter From Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date / दिनांक तक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" ValidationGroup="a" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter To Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter To Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
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
                                            <label>Retailer/विक्रेता</label>
                                            <asp:DropDownList ID="ddlRetailer" OnInit="ddlRetailer_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                   <%--   <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category /वस्तु वर्ग</label>
                                            <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2" style="margin-top: 22px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" AccessKey="S" />
                                        
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                            <div class="row" id="pnldata" runat="server" visible="false">

                                <div class="col-lg-12">
                                    <fieldset>
                                        <legend>Retailer/Parlor Crate Details</legend>

                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="MilkCrateMgmtId" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Booth Name" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBName" Text='<%# Eval("BName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDelivaryDate" Text='<%# Eval("DelivaryDate") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delivery Shift" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDelivaryShift" Text='<%# Eval("DelivaryShift") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Category" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Received Date" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceivedDate" Text='<%# Eval("ReceivedDate") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supplied Crate" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblTotalSuppliedCrate" Text='<%# Eval("Total_SuppliedCrate") %>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Received Crate" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblTotalReceivedCrate" Text='<%# Eval("Total_ReceivedCrate") %>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Broken Crate" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblTotalBrokenCrate" Text='<%# Eval("Total_BrokenCrate") %>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Missing Crate" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblTotalMissingCrate" Text='<%# Eval("Total_MissingCrate") %>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblCStatus" Text='<%# Eval("CStatus") %>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
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
                    title: ('Crate Management Report Of Retailer').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6,7,8,9,10]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Crate_Management_At_DistOrSuperstockist',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: ('Crate Management Report Of Retailer').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
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

