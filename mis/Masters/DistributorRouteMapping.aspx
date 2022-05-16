<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistributorRouteMapping.aspx.cs" Inherits="mis_Masters_DistributorRouteMapping" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                        <i class="fa fa-2x fa-question-circle"></i>&nbsp;&nbsp;
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
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Distributor Route Mapping</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
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
                                    <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                        ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Location<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                        ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnInit="ddlLocation_Init" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Route No<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRoute" InitialValue="0" ErrorMessage="Select Route." Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Distributor Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Distributor/Super Stockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Super Stockist Name !'></i>"
                                        ControlToValidate="ddlDist" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlDist" runat="server" OnInit="ddlDist_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Distributor Route Mapping Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Category</label>

                                <asp:DropDownList ID="ddlSearchItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Location</label>

                                <asp:DropDownList ID="ddlSearchLocation" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                    class="datatable table table-hover table-bordered pagination-ys"
                                    EmptyDataText="No Record Found." DataKeyNames="DistributorRouteMapping_Id" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("AreaName") %>' />
                                                <asp:Label ID="lblDistributorId" Visible="false" runat="server" Text='<%# Eval("DistributorId") %>' />
                                                <asp:Label ID="lblSubDistributorId" Visible="false" runat="server" Text='<%# Eval("SubDistributorId") %>' />
                                                <asp:Label ID="lblAreaId" Visible="false" runat="server" Text='<%# Eval("AreaId") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RName") %>' />
                                                <asp:Label ID="lblRouteId" runat="server" Visible="false" Text='<%# Eval("RouteId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDName" runat="server" Text='<%# Eval("DName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>' />
                                                <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>' />
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Statuus">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("AStatus") %>' />
                                                  <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CssClass="label label-primary" CommandArgument='<%#Eval("DistributorRouteMapping_Id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkDelete" CssClass="label label-danger" CommandName="RecordDelete" CommandArgument='<%#Eval("DistributorRouteMapping_Id") %>' runat="server" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
            lengthMenu: [10, 25, 50, 100],
            iDisplayLength: 200,
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
                    title: ('Distributor/SuperStockist Route Mapping Details').bold().fontsize(5).toUpperCase(),
                    filename: 'Distributor/SuperStockist Route Mapping Details',
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Distributor/SuperStockist Route Mapping Details').bold().fontsize(5).toUpperCase(),
                    filename: 'DistributorRouteMappingDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
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
    </script>
</asp:Content>
