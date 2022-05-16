<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OpeningStock.aspx.cs" Inherits="mis_Warehouse_OpeningStock" EnableSessionState="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <%-- <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlupdate" runat="server">
        <ContentTemplate>--%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="vgos" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-success">
				                <div class="box-header">
                    <h3 class="box-title">Item Inward Entry (वस्तु आवक प्रविष्टि)</h3>
                    <br /><br />
                    <p style="color:blue; font-family: Verdana; font-size:13px;"><b>Note:</b>
                        कृपया  1 अप्रैल की ओपनिंग के लिए, 31 मार्च की क्लोजिंग की प्रविष्टी करें |                        
                        &nbsp;&nbsp; अतः 31 मार्च का चयन करें, ताकि ये क्लोजिंग 1 अप्रैल के लिए  ओपनिंग बन जाए | </p>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date (दिनांक) </label>
                                <span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="vgos" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="vgos" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtTransactionDt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Group (वस्तु का समूह)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" ValidationGroup="vgos" InitialValue="0" ErrorMessage="Select Item Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Group !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlitemcategory" runat="server" Width="100%" CssClass="form-control select2"  AutoPostBack="true" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Sub-Group (वस्तु की श्रेणी)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" ValidationGroup="vgos" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlitemtype" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="vgos" ErrorMessage="Select Item Name." Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlitems" runat="server" Width="100%" CssClass="form-control select2"  AutoPostBack="true" OnSelectedIndexChanged="ddlitems_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Unit</label>
                                <asp:DropDownList ID="ddlUnit" Enabled="false" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Quantity (मात्रा)<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="txtQty" Display="Dynamic" ValidationGroup="vgos" ErrorMessage="Enter Item Quantity." Text="<i class='fa fa-exclamation-circle' title='Enter Item Quantity !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,5})?$" ValidationGroup="vgos" runat="server" ControlToValidate="txtQty" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtQty" placeholder="Quantity" onchange="CalculateAmount();" autocomplete="off" onpaste="return false;" CssClass="form-control Number" runat="server" MaxLength="8"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Rate/(दर)<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate" Display="Dynamic" ValidationGroup="vgos" ErrorMessage="Enter Item Rate." Text="<i class='fa fa-exclamation-circle' title='Enter Item Rate !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgos" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtRate" placeholder="Enter Rate" autocomplete="off" onchange="CalculateAmount();" onpaste="return false;" CssClass="form-control Number" onkeypress="return validateDec(this,event)" runat="server" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>Amount/(राशि)<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount" Display="Dynamic" ValidationGroup="vgos" ErrorMessage="Enter Item Amount." Text="<i class='fa fa-exclamation-circle' title='Enter Item Amount !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="vgos" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox ID="txtAmount" placeholder="Enter Amount" autocomplete="off" onpaste="return false;" onchange="CalculateRate();" CssClass="form-control Number" onkeypress="return validateDec(this,event)" runat="server" MaxLength="15"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Warehouse (गोदाम)<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" InitialValue="0" ValidationGroup="vgos" Display="Dynamic" ControlToValidate="ddlWarehouse" ErrorMessage="Select Warehouse." Text="<i class='fa fa-exclamation-circle' title='Select Warehouse !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control select2"  OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                    <asp:ListItem Text="Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button Text="Save" ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="vgos" runat="server" OnClientClick="return ValidatePage();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Stock Detail (स्टॉक विवरण)</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office</label>
                                <asp:DropDownList ID="ddlOffice" Enabled="false" Width="100%" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlSearchWarehouse_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Warehouse (गोदाम)</label>
                                <asp:DropDownList ID="ddlSearchWarehouse" Width="100%" runat="server" CssClass="form-control select2"  AutoPostBack="true" OnSelectedIndexChanged="ddlSearchWarehouse_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-end-date="0d" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                  <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date (दिनांक) </label>
                                        <span class="pull-right">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="vgos" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(?:^(?:(?:(?:(?:(?:0?[13578]|1[02])/31)|(?:(?:0?[13-9]|1[0-2])/(?:29|30)))/(?:1[6-9]|[2-9]\d)\d{2})|(?:0?2/29/(?:(?:(?:1[6-9]|[2-9]\d)(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))/(?:0?[1-9]|1\d|2[0-8])/(?:(?:1[6-9]|[2-9]\d)\d{2}))$)$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-end-date="0d" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOpeningStock" DataKeyNames="ItmStock_id" runat="server" OnRowDataBound="gvOpeningStock_RowDataBound" EmptyDataText="No records Found" OnRowCommand="gvOpeningStock_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblItmStock_id" runat="server" Text='<%#Eval("ItmStock_id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inward Date <br /> (आवक तिथि)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTranDt" runat="server" Text='<%# Eval("TranDt", "{0:yyyy/MM/dd}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Group <br />(वस्तु का समूह)">
                                            <ItemTemplate>
                                                <%# Eval("ItemCatName") %>
                                                <asp:Label ID="lblWarehouse_id" Visible="false" runat="server" Text='<%# Eval("Warehouse_id") %>'></asp:Label>
                                                <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                <asp:Label ID="lblTransactionFrom" Visible="false" runat="server" Text='<%# Eval("TransactionFrom") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Sub-Group <br />(वस्तु की श्रेणी)">
                                            <ItemTemplate>
                                                <%# Eval("ItemTypeName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name <br />(वस्तु का नाम)">
                                            <ItemTemplate>
                                                <%# Eval("ItemName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity <br />(मात्रा)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCr" runat="server" Text='<%# Eval("Cr") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit <br />(इकाई)">
                                            <ItemTemplate>
                                                <%# Eval("UQCCode") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate <br />(दर)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount <br />(राशि)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Quantity<br />(मात्रा)<br />(In MT)">
                                            <ItemTemplate>
                                                <%# Eval("UQCCode") != "KG" ? (Convert.ToDecimal(Eval("Cr"))/1000) + " MT" : "-" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Warehouse <br />(गोदाम) ">
                                            <ItemTemplate>
                                                <%# Eval("WarehouseName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ItmStock_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ItmStock_id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
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
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        $('.select2').select2()

        $(function () {
            $("#txtTransactionDt").datepicker({
                endDate: "0d",
                //startDate: "-365d",
               // showDate: "-365d",
                autoclose: true
            });
            //$("#txtFromDate").datepicker({
            //    dateFormat: 'yyyy/MM/dd',
            //    autoclose: true
            //});
            //$("#txtTodate").datepicker({
            //    dateFormat: 'yyyy/MM/dd',
            //    autoclose: true
            //});
        });

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
                    title: ('Stock Detail (स्टॉक विवरण)').bold().toUpperCase().fontsize(5),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Stock_Detail_Report',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: ('Stock Detail (स्टॉक विवरण)').bold().toUpperCase().fontsize(5),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
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
        function CalculateAmount() {
            debugger;
            var Quantity = document.getElementById('<%=txtQty.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
        }
        function CalculateRate() {
            debugger;
            var Quantity = document.getElementById('<%=txtQty.ClientID%>').value.trim();
            var TotalAmount = document.getElementById('<%=txtAmount.ClientID%>').value.trim();
            //var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "") {
                document.getElementById('<%=txtQty.ClientID%>').value = "1";
                Quantity = "1";
            }
            if (TotalAmount == "")
                TotalAmount = "0";

            document.getElementById('<%=txtRate.ClientID%>').value = (TotalAmount / Quantity).toFixed(2);
        }

        function validateDecUnit(el, evt) {
            var digit = 4;
             var charCode = (evt.which) ? evt.which : event.keyCode;
             if (digit == 0 && charCode == 46) {
                 return false;
             }

             var number = el.value.split('.');
             if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             //just one dot (thanks ddlab)
             if (number.length > 1 && charCode == 46) {
                 return false;
             }
             //get the carat position
             var caratPos = getSelectionStart(el);
             var dotPos = el.value.indexOf(".");
             if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                 return false;
             }
             return true;
         }

    </script>
</asp:Content>

