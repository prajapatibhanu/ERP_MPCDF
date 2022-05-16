<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TransporterRouteHeadMapping.aspx.cs" Inherits="mis_Masters_TransporterRouteHeadMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
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
                <div class="modal-footer" style="text-align: center;">
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
                <div class="col-md-12">
                    <!-- SELECT2 EXAMPLE -->
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Transporter & Route Head Mapping</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12 ">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Transporter Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select Transporter" Text="<i class='fa fa-exclamation-circle' title='Select Transporter !'></i>"
                                                ControlToValidate="ddlTransporter" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>

                                        <asp:DropDownList ID="ddlTransporter" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                               
                               <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Route Head Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Select RouteHead" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                ControlToValidate="ddlHeadRoute" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlHeadRoute" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                        <asp:Button runat="server" OnClick="btnClear_Click" CssClass="btn btn-default" ID="btnClear" Text="Clear" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Transporter & Route Head Mapping Details</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Route Head Name</label>
                                        <asp:DropDownList ID="ddlSearchRouteHead" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-2" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." DataKeyNames="TransandRouteHeadId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transporter Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRouteHeadName" runat="server" Text='<%# Eval("Contact_Person") %>'></asp:Label>
                                                        <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RouteHeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("TransandRouteHeadId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
    <script>
       
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
            }
        }
    </script>
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
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            iDisplayLength: 100,
            lengthMenu: [10, 25, 50, 100],
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
                    title: ('Transporter and Route Head Mapping').bold().fontsize(5).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Transporter and Route Head Mapping').bold().fontsize(5).toUpperCase(),
                    filename: 'TransporterandRouteHeadMapping',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2]
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

    </script>
</asp:Content>
