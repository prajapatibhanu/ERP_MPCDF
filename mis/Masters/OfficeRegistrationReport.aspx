<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OfficeRegistrationReport.aspx.cs" Inherits="mis_Masters_OfficeRegistrationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Office Registration Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    
                    <div class="box-body">
                        <fieldset>
                            <legend>
                               Filter
                            </legend>
                             <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Milk Supply to<span style="color:red"></span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="ddlMilkSupplyUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Suppy to!'></i>" ErrorMessage="Select BMC Available" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlMilkSupplyUnit" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlMilkSupplyUnit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Supply Unit<span style="color:red"></span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic" ControlToValidate="ddlSupplyUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Supply Unit!'></i>" ErrorMessage="Select Supply Unit" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSupplyUnit" Width="100%" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" style="margin-top:21px;" runat="server" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                </div>
                        </fieldset>

                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Report</legend>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                                        ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Office_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Milk Collection Type ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeTypeName" runat="server" Text='<%# Eval("OfficeTypeName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
											 <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("Office_Name") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name In English">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOffice_Name_E" runat="server" Text='<%# Eval("Office_Name_E") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSocietyCategory" runat="server" Text='<%# Eval("SocietyCategory") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Milk Supply to">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMilkSupplyto" runat="server" Text='<%# Eval("MilkSupplyto") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supply Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplunit" runat="server" Text='<%# Eval("SupplyUnit") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
											<asp:TemplateField HeaderText="Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBank" runat="server" Text='<%# Eval("BankName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
											<asp:TemplateField HeaderText="Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountNo" runat="server" Text='<%# Eval("AccountNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
											<asp:TemplateField HeaderText="IFSC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIFSC" runat="server" Text='<%# Eval("IFSC") %>' />
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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../../js/jquery-1.10.2.js"></script>
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
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text("OfficeDetail"),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: 'Office Registration Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11]
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

