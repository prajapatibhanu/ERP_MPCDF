<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistOrStockistParlourMapping.aspx.cs" Inherits="mis_DistOrStockistParlourMapping" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                    <h3 class="box-title">Distributor/SuperStockist Retailer Mapping</h3>
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
                                <label>Type<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                        ControlToValidate="ddlDSType" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <%-- <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control select2"></asp:DropDownList>
                                --%>
                                <asp:DropDownList ID="ddlDSType" runat="server" OnSelectedIndexChanged="ddlDSType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3" id="panelDist" runat="server" visible="false">
                            <div class="form-group">
                                <label>Distributor/Super Stockist Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Distributor/Super Stockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Super Stockist Name !'></i>"
                                        ControlToValidate="ddlDist" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlDist" runat="server" OnInit="ddlDist_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="panelSubDist" runat="server" visible="false">
                            <div class="form-group">
                                <label>Sub-Distributor/Stockist Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Distributor/Super Stockist Name" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/Super Stockist Name !'></i>"
                                        ControlToValidate="ddlSubDist" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlSubDist" runat="server" OnInit="ddlSubDist_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" 
                                            ValidationGroup="a" runat="server" ControlToValidate="ddlRetailerType" InitialValue="0" 
                                            ErrorMessage="Select Retailer Type." Text="<i class='fa fa-exclamation-circle' title='Select Retailer Type !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlRetailerType" OnInit="ddlRetailerType_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlRetailerType_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                    
                                    </asp:DropDownList>
                                </div>
                            </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Retailer<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Retailer" Text="<i class='fa fa-exclamation-circle' title='Select Retailer !'></i>"
                                        ControlToValidate="ddlRetailer" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
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
                    <h3 class="box-title">Distributor/Super Stockist Retailer Mapping Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                      class="datatable table table-hover table-bordered pagination-ys"
                                    EmptyDataText="No Record Found." DataKeyNames="DistOrSubDistParlour_Id" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistributorId" Visible="false" runat="server" Text='<%# Eval("DistributorId") %>' />
                                                <asp:Label ID="lblSubDistributorId" Visible="false" runat="server" Text='<%# Eval("SubDistributorId") %>' />
                                                <asp:Label ID="lblOfficeType_ID" runat="server" Visible="false" Text='<%# Eval("OfficeType_ID") %>' />
                                                <asp:Label ID="lblOfficeType_Title" runat="server" Text='<%# Eval("OfficeTypeName") %>' />
                                                 <asp:Label ID="lblRetailerTypeID" Visible="false" runat="server" Text='<%# Eval("RetailerTypeID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Distributor/Super Stockist">
                                            <ItemTemplate>


                                                <asp:Label ID="lblDName" runat="server" Text='<%# Eval("DName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sub Distributor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSDName" runat="server" Text='<%# Eval("SDName") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Retailer Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRetailerType" runat="server" Text='<%# Eval("RetailerTypeName") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Retailer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBName" runat="server" Text='<%# Eval("BandOName") %>' />
                                                <asp:Label ID="lblBoothId" runat="server" Visible="false" Text='<%# Eval("BoothId") %>' />
                                                <asp:Label ID="lblOrganizationId" runat="server" Visible="false" Text='<%# Eval("OrganizationId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("DistOrSubDistParlour_Id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("DistOrSubDistParlour_Id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
            lengthMenu: [10, 25, 50, 100],
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
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Distributor/SuperStockist Parlour Mapping Details',
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
