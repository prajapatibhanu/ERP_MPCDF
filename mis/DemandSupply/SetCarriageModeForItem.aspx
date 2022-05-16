<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SetCarriageModeForItem.aspx.cs" Inherits="mis_DemandSupply_SetCarriageModeForItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">  
    <style>
        .aa
        {
            top: 50px !important; 
            left: 540.75px !important; 
            z-index: 9999 !important; 
            display: block !important;
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
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; 
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Set Item Carriage Mode</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Category,Item,Carriage Mode
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Category <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged"
                                                runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Name <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Name" Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"
                                                    ControlToValidate="ddlItemName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItemName" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Carriage Mode </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Carriage Mode" Text="<i class='fa fa-exclamation-circle' title='Select Carriage Mode !'></i>"
                                                    ControlToValidate="ddlCarriageMode" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlCarriageMode" AutoPostBack="true" OnInit="ddlCarriageMode_Init" OnSelectedIndexChanged="ddlCarriageMode_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-3" id="pnlcratecolor" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Crate Colour </label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvcratecolour" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Crate Color" Text="<i class='fa fa-exclamation-circle' title='Select Crate Color !'></i>"
                                                    ControlToValidate="ddlCrateColor" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlCrateColor" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Quantity <span id="spanqtydetail" runat="server"></span><span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                    ControlToValidate="txtQtyPerCarriageType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                        ErrorMessage="First digit can't be 0(Zero)!" ForeColor="Red" 
                                                     Text="<i class='fa fa-exclamation-circle' title='First digit can't be 0(Zero)!'></i>" ControlToValidate="txtQtyPerCarriageType"
                                                         ValidationExpression="^[1-9][0-9]*$">
                                                  </asp:RegularExpressionValidator>
                                             </span>
                                            <asp:TextBox ID="txtQtyPerCarriageType" autocomplete="off" onkeypress="return validateNum(event);" CssClass="form-control" MaxLength="6" placeorder="ex:20" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Not Issue Quantity </label>
                                             <span class="pull-right">
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                        ErrorMessage="Only digit allow in field Not Issue Quantity!" ForeColor="Red" 
                                                     Text="<i class='fa fa-exclamation-circle' title='Only digit allow in field Not Issue Quantity!'></i>" ControlToValidate="txtNotIssueQty"
                                                         ValidationExpression="^[0-9]*$">
                                                  </asp:RegularExpressionValidator>
                                             </span>
                                            <asp:TextBox ID="txtNotIssueQty" autocomplete="off" onkeypress="return validateNum(event);" CssClass="form-control" MaxLength="3" placeorder="ex:1" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Crate(Per Thappi) </label>
                                            <asp:TextBox ID="txtCratePerThaapi" autocomplete="off" onkeypress="return validateNum(event);" CssClass="form-control" MaxLength="3" placeorder="ex:1" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                     <div class="form-group">
                                        <label>Effective Date <span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Enter Effective Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Effective Date !'></i>"
                                                ControlToValidate="txtEffectiveDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveDate"
                                                ErrorMessage="Invalid Effective Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Effective Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                          <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEffectiveDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                     </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>IsActive </label>
                                            <asp:CheckBox ID="chkIsActive" Checked="true" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                     <div class="col-md-1" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit"
                                                Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                        </div>
                                    </div>
                                     <div class="col-md-1" style="margin-top:20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Reset" CssClass="btn btn-block btn-secondary" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>Set Item Carriage Mode Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True" DataKeyNames="ItemCarriageModeId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lblItemCat_id" Visible="false" Text='<%# Eval("ItemCat_id")%>' runat="server" />
                                                            <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                            <asp:Label ID="lblCarriageModeID" Visible="false" Text='<%# Eval("CarriageModeID")%>' runat="server" />
                                                            <asp:Label ID="lblOffice_ID" Visible="false" Text='<%# Eval("Office_ID")%>' runat="server" />
                                                            <asp:Label ID="lblCrateColorID" Visible="false" Text='<%# Eval("CrateColorID")%>' runat="server" />
                                                            <asp:Label ID="lblItemQty" Visible="false" Text='<%# Eval("ItemQtyByCarriageMode")%>' runat="server" />
                                                              <asp:Label ID="lblIsActive" Visible="false" Text='<%# Eval("IsActive")%>' runat="server" />
                                                             <asp:Label ID="lblQty" Visible="false" Text='<%# Eval("ItemQtyByCarriageMode")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dugdh Sang">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Carriage Mode (Carriage Color) ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCarriageModeName" Text='<%# Eval("CarriageModeID").ToString()=="1" ? Eval("CarriageModeName") + " (" + Eval("V_SealColor") + ")" : Eval("CarriageModeName")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemQtyByCarriageMode" Text='<%# Eval("ItemQtyByCarriageMode") + " Per " + Eval("CarriageModeName")  %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Not IssueQty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNotIssueQty" Text='<%# Eval("NotIssueQty")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Crate (Per Thappi)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCratePerThaapi" Text='<%# Eval("CratePerThaapi")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Effective Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEffectiveDate" Text='<%# Eval("EffectiveDate")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                          <asp:CheckBox ID="chkActiveStatus" Enabled="false" CssClass="checkbox" Checked='<%# Eval("IsActive")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ItemCarriageModeId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ItemCarriageModeId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Active Or DeActive?')"><i class="fa fa-trash"></i></asp:LinkButton>--%>
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
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
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
                    title: ('Item Carriage Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2,3,4,5,6,7,8]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'ItemCarriageDetails',
                    title: ('Item Carriage Details').bold().fontsize(3).toUpperCase(),
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5,6,7,8]
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
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
            }
        }

        //$("#txtOrderDate").datepicker({
        //    autoclose: true ,
        //    startDate: "1d",
        //});

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
    </script>
</asp:Content>

