<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_Fin_CFP_PO.aspx.cs" Inherits="mis_CattleFeed_Rpt_Fin_CFP_PO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
     <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">

                        <div class="box-header">
                            <h3 class="box-title">Purchase Order Report</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Purchase Order Report
                                </legend>
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFromDate" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>To Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtToDate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-md-3" style="text-align: center;margin-top:23px;">
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="a" />
                                    &nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" CausesValidation="false" />
                                </div>

                                </div>
                            
                               
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-body">
                            <fieldset>
                                <legend>Purchase Order Details
                                </legend>
                                <div class="row">
                                    <asp:GridView ID="Gridview1" DataKeyNames="CFP_Purchase_Order_ID" OnRowCommand="Gridview1_RowCommand" 
                                        runat="server" EmptyDataText="No Record Found."  class="datatable table table-hover table-bordered" 
                                            AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="PO No.">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblPO_NO" Text='<%# Eval("PO_NO")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO Date">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblPO_Date" Text='<%# Eval("PO_Date")%>' runat="server" />
                                                         <asp:Label ID="lblPO_End_Date" Visible="false" Text='<%# Eval("PO_End_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tender Date">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblTenderDate" Text='<%# Eval("TenderDate")%>' runat="server" />
                                                         <asp:Label ID="lblSupplyScheduling" Visible="false" Text='<%# Eval("SupplyScheduling")%>' runat="server" />
                                                         <asp:Label ID="lblRemark" Visible="false" Text='<%# Eval("Remark")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Vendor Name">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblVendorName" Text='<%# Eval("VendorName")%>' runat="server" />
                                                           <asp:Label ID="lblVendorAddress" Visible="false" Text='<%# Eval("VendorAddress")%>' runat="server" />
                                                           <asp:Label ID="lblVendorContactNo" Visible="false" Text='<%# Eval("VendorContactNo")%>' runat="server" />
                                                         <asp:Label ID="lblGSTNNO" Visible="false" Text='<%# Eval("GSTNNO")%>' runat="server" />
                                                          <asp:Label ID="lblEmailAddress" Visible="false" Text='<%# Eval("EmailAddress")%>' runat="server" />
                                                         <asp:Label ID="lblReference_NO" Visible="false" Text='<%# Eval("Reference_NO")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                      <asp:LinkButton ID="lnkView" CommandArgument='<%#Eval("CFP_Purchase_Order_ID") %>' CommandName="RecordView" CausesValidation="false" runat="server" ToolTip="View" Style="color: red;"><i class="fa fa-eye"></i></asp:LinkButton>
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
            <div class="modal" id="ItemDetailsModal">
                    <div style="display: table; height: 100%; width: 100%;">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div id="div1" runat="server">
                                <asp:Label ID="lblMsgLeakage" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details</legend>
                                            <div class="row" style="height: 250px; overflow: scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView2"  DataKeyNames="CFP_Purchase_Order_ChildID" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-hover" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ItemCategory">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>'></asp:Label>
                                                                  
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Item Type">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Items">
                                                                <ItemTemplate>
                                                                       <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Item Commodity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Commodity" runat="server" Text='<%# Eval("Item_Commodity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Item Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem_Quantity" runat="server" Text='<%# Eval("Item_Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Item Rate">
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblItem_Rate" runat="server" Text='<%# Eval("Item_Rate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
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
                            <div class="row">
                                <div class="col-md-5"></div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                    
                                    </div>

                                </div>


                            </div>
                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                        </div>
                <!-- /.modal-dialog -->
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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
    <script>
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100, 200, 500],
            iDisplayLength: 200,
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
                    title: ('Purchase Order Report').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [1, 2, 3, 4]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Purchase Order Report').bold().fontsize(5).toUpperCase(),
                    filename: 'Order List',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [1, 2, 3, 4]
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

