<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Supplier_Location_Mapping_Rate.aspx.cs" Inherits="mis_CattleFeed_CFP_Supplier_Location_Mapping_Rate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Supplier Location Mapping</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Supplier Location Mapping (आपूर्तिकर्ता के स्थान जानकारी प्रविष्ट करें)
                                </legend>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Supplier (आपूर्तिकर्ता)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="ddlSupplier" ValidationGroup="a" InitialValue="0" ErrorMessage="Select Supplier." Text="<i class='fa fa-exclamation-circle' title='Select Supplier !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" Width="100%" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Location Type (स्थान का प्रकार)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddllocationtype" ValidationGroup="a" InitialValue="0" ErrorMessage="Select Location Type." Text="<i class='fa fa-exclamation-circle' title='Select Location Type !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddllocationtype" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddllocationtype_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" id="loc" runat="server">
                                        <div class="form-group">
                                            <label>Location (स्थान)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlLocation" ValidationGroup="a" InitialValue="0" ErrorMessage="Select Location." Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" runat="server" Width="100%" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:RadioButtonList runat="server" ID="rbtnRateType" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="2"></asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Rate(दर)(Per M.T.)<span class="text-danger"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>"
                                                    ControlToValidate="txtRate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ValidationGroup="a" ErrorMessage="Decimal Only"  Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>" ControlToValidate="txtRate"
                                                    ValidationExpression="^[1-9]\d*(\.\d+)?$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtRate" runat="server" placeholder="Rate...." class="form-control" MaxLength="5"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Total Distance(कुल दूरी)<span class="text-danger"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Distance" Text="<i class='fa fa-exclamation-circle' title='Enter Distance !'></i>"
                                                    ControlToValidate="txtDistance" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtDistance" runat="server" placeholder="Distance...." class="form-control" MaxLength="5"  onkeypress="return onlyNumberKey(event)"></asp:TextBox>
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

                                            <asp:Button runat="server" CssClass="btn btn-block btn-default" ID="btnclear" Text="Clear" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                            <div class="col-md-12">
                                <div class="box box-Manish">
                                    <div class="box-body">
                                        <fieldset>
                                            <legend>Registered Supplier Location Mapping
                                            </legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                                    <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="grdCatlist_RowCommand" OnPageIndexChanging="grdCatlist_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Supplier_Name" HeaderText="Supplier" />
                                                            <asp:BoundField DataField="LocationType" HeaderText="Location Type" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                            <asp:BoundField DataField="RateType" HeaderText="Rate Type" ItemStyle-Width="30%" />
                                                            <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="TotalDistance" HeaderText="Total Distance" ItemStyle-Width="10%" />
                                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("SupplierLocationID") %>' Text="Edit" OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="false" CommandName="RecordDelete" CommandArgument='<%#Eval("SupplierLocationID") %>' Text="Delete" Style="color: red;" OnClientClick="return confirm('CFP Entry will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>
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
     <script>

         function onlyNumberKey(evt) {

             // Only ASCII charactar in that range allowed 
             var ASCIICode = (evt.which) ? evt.which : evt.keyCode
             if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                 return false;
             return true;
         }
    </script>
</asp:Content>

