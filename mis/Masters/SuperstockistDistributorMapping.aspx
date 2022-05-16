<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SuperstockistDistributorMapping.aspx.cs" Inherits="mis_Masters_SuperstockistDistributorMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
            <!-- SELECT2 EXAMPLE -->




            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">SuperStockist and Distributor Mapping</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>SuperStockist Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                        ErrorMessage="Enter Super Stockist Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Super Stockist Name !'></i>"
                                        ControlToValidate="ddlSuperStockistName" Display="Dynamic" InitialValue="0" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList runat="server" autocomplete="off" CssClass="form-control select2" ID="ddlSuperStockistName">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2">
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Location<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                        ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlLocation" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Distributor Name<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Enter Super Stockist Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Super Stockist Name !'></i>"
                                        ControlToValidate="ddlDistributor" Display="Dynamic" InitialValue="0" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:ListBox runat="server" autocomplete="off" CssClass="form-control" SelectionMode="Multiple" ID="ddlDistributor">
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                              &nbsp; &nbsp; &nbsp;   <asp:Button ID="btnClear" runat="server" Text="Clear" Style="margin-top: 20px;" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>

                </div>
                </div>
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Super Stockist and Distributor Mapping Details</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
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
                                    EmptyDataText="No Record Found." DataKeyNames="SuperStockistDistributorMapping_Id" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAreaName" runat="server" Text='<%# Eval("AreaName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SuperStockist Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSSName" runat="server" Text='<%# Eval("SSName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDName" runat="server" Text='<%# Eval("DName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("AStatus") %>' />
                                                  <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="lnkDelete" CssClass="label label-danger" Visible='<%# (Eval("IsActive").ToString()=="True" ? true:false) %>' CommandName="RecordDelete" CommandArgument='<%#Eval("SuperStockistDistributorMapping_Id") %>' OnClientClick="return confirm('Are you sure to Delete?')" runat="server" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                        </div>
                    </div>

                </div>
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('[id*=ddlDistributor]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


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
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100, 200, 500],
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
                    title: ('SuperStockist Registration Details').bold().fontsize(5).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('SuperStockist Registration Details').bold().fontsize(5).toUpperCase(),
                    filename: 'SuperStockist Registration Details',
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
    </script>
</asp:Content>

