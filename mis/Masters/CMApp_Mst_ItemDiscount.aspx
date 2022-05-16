<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CMApp_Mst_ItemDiscount.aspx.cs" Inherits="mis_Masters_CMApp_Mst_ItemDiscount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
  <%--  <link href="../../css/datatable/dataTables.bootstrap.min.css" rel="stylesheet" />--%>
     <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />

   <%-- <script type="text/javascript">
        function CalculateDiscount() {
          
            var ir = document.getElementById("<%=txtItemRate.ClientID %>").value;
            var d = document.getElementById("<%=txtDiscount.ClientID %>").value

            if (ir == "") {
                ir = 0.0;
            }
            if (d == "") {
                d = 0;
            }

            var finalsevalue = (parseFloat(ir) -(parseFloat(ir) * parseInt(d) / 100));
            if (!isNaN(finalsevalue)) {
                document.getElementById("<%=txtRateAfterDiscount.ClientID %>").value = parseFloat(finalsevalue);
            }

        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="modal fade" id="myModalMaping" tabindex="-1" role="dialog" aria-labelledby="myModalLabelMapping" aria-hidden="true">
         <div style="display: table; height: 100%; width: 100%;">
           <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
             <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                 <div class="modal-header" style="background-color: #d9d9d9;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelMapping">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlertMapping" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnMYes" OnClick="btnMSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="btnMNo" ValidationGroup="noo" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
      </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                
              
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Item Discount Master </h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Item Discount Master</legend>
                                
                                <div class="row">
                                   
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Category/वस्तु वर्ग<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                        ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>

                                                <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Name/वस्तु नाम<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                        InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                        ControlToValidate="ddlItem" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Discount (In %) / डिस्काउंट<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                        ErrorMessage="Enter Discount" Text="<i class='fa fa-exclamation-circle' title='Enter Discount !'></i>"
                                                        ControlToValidate="txtDiscount" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="b"
                                                        ErrorMessage="Invalid Discount, only numeric allow. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Item Discount, only numeric allow. !'></i>" ControlToValidate="txtDiscount"
                                                        ValidationExpression="^[0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDiscount" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter Discount" ClientIDMode="Static"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Effective From Date / दिनांक से प्रभावी<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                        ErrorMessage="Enter Effective From Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective From Date !'></i>"
                                                        ControlToValidate="txtEffectiveFromDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveFromDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveFromDate" MaxLength="10" placeholder="Enter Effective From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Effective To Date / दिनांक तक प्रभावी<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="b"
                                                        ErrorMessage="Enter Effective To Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective To Date !'></i>"
                                                        ControlToValidate="txtEffectiveToDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveToDate"
                                                        ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveToDate" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" onpaste="return false;" onkeypress="return false;" MaxLength="10" placeholder="Enter Effective From Date" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-3" style="margin-top:20px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnMSubmit" Text="Save" OnClientClick="return MappingValidatePage();" AccessKey="V" />

                                                <asp:Button ID="btnMClear" runat="server" OnClick="btnMClear_Click" Text="Clear" CssClass="btn btn-default" />
                                            </div>
                                        </div>
                                </div>
                            </fieldset>
                            <div class="row" id="pnldata" runat="server" visible="true">

                           
                            <fieldset>
                                <legend>Item Dicount Details</legend>
                                <div class="row">
                                          
                                       <%--<div class="col-md-12" style="font-style:normal;color:red">Note-: Item Quantity can only be Updated </div>
                                    <br />--%>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." DataKeyNames="ItemRateDiscount_Id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No /क्र.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Category/वस्तु वर्ग">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>'></asp:Label>
                                                            <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name/वस्तु नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                            <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Discount (In %) / डिस्काउंट">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscount_InPer" runat="server" Text='<%# Eval("Discount_InPer") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective FromDate/दिनांक से प्रभावी ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveFromDate" runat="server" Text='<%# Eval("EffectiveFromDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective ToDate/दिनांक तक प्रभावी">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveToDate" runat="server" Text='<%# Eval("EffectiveToDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="Action/कार्यवाही करें">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" CommandName="RecordEdit" CommandArgument='<%#Eval("ItemRateDiscount_Id") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("ItemRateDiscount_Id") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("ItemRateDiscount_Id") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkMappingDelete" CommandArgument='<%#Eval("ItemRateDiscount_Id") %>' CommandName="MappingRecordDel" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   <%-- <script src="../../js/datatable/jquery.dataTables.min.js"></script>
    <script src="../../js/datatable/dataTables.bootstrap.min.js"></script>
    <script src="../../js/datatable/dataTables.buttons.min.js"></script>
    <script src="../../js/datatable/buttons.flash.min.js"></script>
    <script src="../../js/datatable/jszip.min.js"></script>
     <script src="../../js/datatable/pdfmake.min.js"></script>
    <script src="../../js/datatable/vfs_fonts.js"></script>
    <script src="../../js/datatable/buttons.html5.min.js"></script>
    <script src="../../js/datatable/buttons.print.min.js"></script>--%>

    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    
   
    <script src="../../js/datatable/vfs_fonts.js"></script>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100, 150, 200, 500, 1000],
            iDisplayLength: 100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
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
                    title: ('Item Discount Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Item Discount Details').bold().fontsize(3).toUpperCase(),
                    filename: 'Item Discount',
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
        function MappingValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnMSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlertMapping.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalMaping').modal('show');
                    return false;
                }
            }
        }
        $("#txtEffectiveFromDate").datepicker({
            autoclose: true
        });
        $("#txtEffectiveToDate").datepicker({
            autoclose: true
        });

    </script>
</asp:Content>
