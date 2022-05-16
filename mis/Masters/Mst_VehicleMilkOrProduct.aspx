<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_VehicleMilkOrProduct.aspx.cs" Inherits="mis_Masters_Mst_VehicleMilkOrProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">        
            <div class="modal-dialog" style="width: 340px;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                   </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <i class="fa fa-2x fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer" style="text-align:center;">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
             <div class="row">
                <div class="col-md-5">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Vehicle Master</h3>
                </div>
                
                <div class="box-body">
                    <div class="row">
                         <div class="col-md-12 ">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Type<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                ControlToValidate="ddlVendorType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlVendorType" OnInit="ddlVendorType_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorType_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                       </div>
                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Name<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Name" Text="<i class='fa fa-exclamation-circle' title='Select Name !'></i>"
                                                ControlToValidate="ddlVendorName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlVendorName" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                       </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Vehicle No.<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                        ErrorMessage="Enter Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                        ControlToValidate="txtVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtVehicleNo" Display="Dynamic" 
                                        ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server"
                                         Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>"
                                         ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a">
                                    </asp:RegularExpressionValidator>

                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleNo" placeholder="XX-00-XX-000" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                            <div class="col-md-6">
                            <div class="form-group">
                                <label>Vehicle Type<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Enter Vehicle Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Type. !'></i>"
                                        ControlToValidate="txtVehicleType" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                        ErrorMessage="Invalid Vehicle Type. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Type !'></i>" ControlToValidate="txtVehicleType"
                                        ValidationExpression="^[a-zA-Z0-9\s]+$">
                                    </asp:RegularExpressionValidator>

                                </span>

                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleType" MaxLength="10" placeholder="Enter Vehicle Type"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                               <label> IsActive</label>

                              <asp:CheckBox ID="chkIsActive" CssClass="form-control" Checked="true" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSave" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            
                                <asp:Button runat="server" OnClick="btnClear_Click" CssClass="btn btn-default" ID="btnClear" Text="Clear" />
                            </div>
                        </div>
                    </div>
                    </div>
                    
                </div>
                    </div>
                <div class="col-md-7">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Vehicle Master Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." DataKeyNames="VehicleMilkOrProduct_ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVendorTypeName" runat="server" Text='<%# Eval("VendorTypeName") %>'></asp:Label>
                                                        <asp:Label ID="lblVendorTypeId" Visible="false" runat="server" Text='<%# Eval("VendorTypeId") %>'></asp:Label>
                                                         <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                         <asp:Label ID="lblTransporterId" Visible="false" runat="server" Text='<%# Eval("TransporterId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact_Person" runat="server" Text='<%# Eval("Contact_Person") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Vehicle No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" runat="server" Text='<%# Eval("VehicleNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleType" runat="server" Text='<%# Eval("VehicleType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IsActive">
                                                    <ItemTemplate>
                                                      <asp:CheckBox ID="chkbxisactive" Enabled="false" Checked='<%# Eval("IsActive") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("VehicleMilkOrProduct_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
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
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50],
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
                    title: ('Vehicle Master').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3,4,5]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Vehicle Master').bold().fontsize(5).toUpperCase(),
                    filename: 'VehicleMaster',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3,4,5]
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

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

