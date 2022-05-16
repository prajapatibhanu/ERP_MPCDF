<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Items_Regitration.aspx.cs" Inherits="mis_CattelFeed_CFP_Items_Regitration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <script type="text/javascript">
        function ShowPopupImplement() {
            $('#ImplementModel').modal('show');
        }
        function ShowPopupEditImplement() {
            $('#EditImplementModel').modal('show');
        }
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkoffice.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "white";
            }
            else {
                //If not checked change back to original color
                row.style.backgroundColor = "white";
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Cattle Feed Plant Items Registration</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Items Registration (वस्तु प्रविष्टी)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Name<span class="text-danger"> *</span></label>

                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Enter Item Name" Text="<i class='fa fa-exclamation-circle' title='Enter Item Name!'></i>"
                                                        ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a"
                                                        ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                        ControlToValidate="txtItemName" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtItemName" runat="server" placeholder="Item Name..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Name (Hindi)<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                        ErrorMessage="Enter Item Name" Text="<i class='fa fa-exclamation-circle' title='Enter Item Name!'></i>"
                                                        ControlToValidate="txtItemNameHi" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a"
                                                        ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                        ControlToValidate="txtItemNameHi" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtItemNameHi" runat="server" placeholder="Item Name Hi..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Code(Alias)<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Item Code" Text="<i class='fa fa-exclamation-circle' title='Enter Item Code!'></i>"
                                                        ControlToValidate="txtItemCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a"
                                                        ErrorMessage="Invalid Code" Text="<i class='fa fa-exclamation-circle' title='Invalid Code !'></i>"
                                                        ControlToValidate="txtItemCode" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code..." class="form-control" MaxLength="10"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Category<span class="text-danger">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category!'></i>"
                                                        ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlItemCategory" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Group<span class="text-danger">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Item Group" Text="<i class='fa fa-exclamation-circle' title='Select Item Group!'></i>"
                                                        ControlToValidate="ddlGroup" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Item Unit<span class="text-danger">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Item Unit" Text="<i class='fa fa-exclamation-circle' title='Select Item Unit!'></i>"
                                                        ControlToValidate="ddlUnit" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">

                                        <div class="form-group">
                                            <label>Item Specification</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a"
                                                    ErrorMessage="Invalid Specification" Text="<i class='fa fa-exclamation-circle' title='Invalid Specification !'></i>"
                                                    ControlToValidate="txtItemSpecification" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtItemSpecification" autocomplete="off" runat="server" placeholder="Item Specification..." CssClass="form-control capitalize" MaxLength="50" ClientIDMode="Static"></asp:TextBox>

                                        </div>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSave" Text="Save" CausesValidation="true" ValidationGroup="a" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <asp:Button runat="server" CssClass="btn btn-block btn-default" ID="btnclear" Text="Clear" CausesValidation="false" OnClick="btnclear_Click" />
                                        </div>
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
                                <legend>Items Registration (प्रविष्टित वस्तु की सूची )
                                </legend>
                                <div class="row">

                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hdncount" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />

                                        <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ItemTypeName" HeaderText="Type (प्रकार)" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                <asp:BoundField DataField="Item_Code" HeaderText="Item Code  (वस्तु का कोड)" />
                                                <asp:BoundField DataField="UnitName" HeaderText="Unit (ईकाई)" />
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="False" CommandName="RecordUpdate" CommandArgument='<%#Eval("Item_id") %>' Text="Edit" ToolTip="Edit" OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');" Visible='<%#Convert.ToBoolean(Eval("iseditable")) %>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="RecordDelete" CommandArgument='<%#Eval("Item_id") %>' Text="Delete" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('CFP Entry will be deleted. Are you sure want to continue?');" Visible='<%#Convert.ToBoolean(Eval("iseditable")) %>'> <i class="fa fa-trash"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnimplemention" runat="server" Text="Implement To Cattle Feed Plant" ToolTip="Implement To Cattle Feed Plant" CssClass="btn btn-primary" CausesValidation="False" CommandArgument='<%#Eval("Item_id") %>' CommandName="implement"> <i class="fa fa-tasks"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnEditimplemention" runat="server" Text="Edit Implementation" CssClass="btn btn-bitbucket" CausesValidation="False" ToolTip="Edit Implemented Cattle Feed Plant" CommandArgument='<%#Eval("Item_id") %>' CommandName="Editimplement" Visible='<%#Convert.ToBoolean(Eval("isimplemented")) %>'><i class="fa fa-tasks"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ImplementModel" tabindex="-1" role="dialog" aria-labelledby="ImplementModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Implementation Item to Cattle Feed Plants</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Assign Item to Cattle Feed Plants</span>
                            <div class="row">
                                <div class="col-md-12" id="ImplementationEntry" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:GridView ID="chkoffice" runat="server" CssClass="table table-bordered"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                Font-Size="11pt" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                                            <asp:HiddenField ID="hdnDS" runat="server" Value='<%#Eval("CFPOfficeID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CFPName" HeaderText="Plant Name with Code" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one DS." ValidationGroup="m"
                                                ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12" id="ImplementationDetail" runat="server">
                                    <asp:GridView ID="grdimplementation" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Data Availble">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DSNAME" HeaderText="DS NAME" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <%-- OnClick="btn_save_Click" OnClick="btnimplement_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button runat="server" ID="btnimplement" class="btn btn-success" Text="Save" CausesValidation="true" ValidationGroup="m" OnClick="btnimplement_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="EditImplementModel" tabindex="-1" role="dialog" aria-labelledby="EditImplementModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Update Implementated Cattle feed plants</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Edit Assigned Item to Cattle feed plants</span>
                            <div class="row">
                                <div class="col-md-12" id="Div1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:CheckBoxList ID="chkDS" runat="server" Width="160%" CellPadding="3" CellSpacing="3" CssClass="table table-bordered"></asp:CheckBoxList>

                                        </div>

                                    </div>
                                </div>

                            </div>

                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button runat="server" ID="btneditimplementation" class="btn btn-success" Text="Save" OnClick="btneditimplementation_Click" CausesValidation="false" ValidationGroup="m"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print" style="display:none;"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o" style="display:none;"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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

