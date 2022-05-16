<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Suppliers_Registration.aspx.cs" Inherits="mis_CattleFeed_CFP_Suppliers_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Supplier Registration</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Supplier Entry (आपूर्तिकर्ता की जानकारी प्रविष्ट करें)
                                </legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Name(नाम)<span class="text-danger"> *</span></label>

                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rvtname" ValidationGroup="a"
                                                    ErrorMessage="Enter Supplier" Text="<i class='fa fa-exclamation-circle' title='Enter Supplier !'></i>"
                                                    ControlToValidate="txtName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                              <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a"
                                                    ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                    ControlToValidate="txtName" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9\s? )(,;:./_-]+$">
                                                </asp:RegularExpressionValidator>--%>
                                            </span>
                                            <asp:TextBox ID="txtName" runat="server" placeholder="Supplier Name..." class="form-control" MaxLength="150" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Mobile(मोबाइल)<span class="text-danger"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No. !'></i>"
                                                    ControlToValidate="txtmobile" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rvDigits" Display="Dynamic" runat="server" ControlToValidate="txtmobile" ErrorMessage="Enter numbers only till 10 digit" Text="<i class='fa fa-exclamation-circle' title='Enter  Contact No !'></i>" ValidationGroup="a" ForeColor="Red" ValidationExpression="\d{10}" />
                                            </span>
                                            <asp:TextBox ID="txtmobile" runat="server" placeholder="Mobile No...." onpaste="return false;" class="form-control" MaxLength="10" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>GSTN<span class="text-danger"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter GSTN" Text="<i class='fa fa-exclamation-circle' title='Enter GSTN !'></i>"
                                                    ControlToValidate="txtGSTN" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                          <%--      <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a"
                                                    ErrorMessage="Invalid GSTN" Text="<i class='fa fa-exclamation-circle' title='Invalid GSTN !'></i>"
                                                    ControlToValidate="txtGSTN" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                </asp:RegularExpressionValidator>--%>
                                            </span>
                                            <asp:TextBox ID="txtGSTN" runat="server" placeholder="GSTN..." class="form-control" MaxLength="15"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Email(ईमेल)<%--<span class="text-danger"> *</span>--%></label>
                                           <%-- <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Invalid Email" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>"
                                                    ControlToValidate="txtemail" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </span>--%>
                                            <asp:TextBox ID="txtemail" runat="server" placeholder="Email...." class="form-control" MaxLength="100"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Contract from (दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtfromDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtfromDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtfromDt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Contract To (दिनांक) </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtto" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtto" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtto" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Is TDS Applicable </label>
                                            <asp:CheckBox ID="chkTDS" Checked="false" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Address(पता)<span class="text-danger"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    ErrorMessage="Enter Address" Text="<i class='fa fa-exclamation-circle' title='Enter Address !'></i>"
                                                    ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                             <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a"
                                                    ErrorMessage="Invalid Address" Text="<i class='fa fa-exclamation-circle' title='Invalid Address !'></i>"
                                                    ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator>--%>
                                            </span>
                                            <asp:TextBox ID="txtAddress" runat="server" placeholder="Address..." class="form-control" MaxLength="300" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSave" ValidationGroup="a" CausesValidation="true" Text="Save" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                            <asp:Button runat="server" CssClass="btn btn-block btn-default" ID="btnclear" Text="Clear" OnClick="btnclear_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Registered Suppliers (प्रविष्टित आपूर्तिकर्ता की जानकारी)
                                </legend>
                                <asp:GridView ID="grdlist" runat="server" PageSize="20" AllowPaging="true" OnPageIndexChanging="grdlist_PageIndexChanging" OnRowCommand="grdlist_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory_Name" runat="server" Text='<%# Eval("SupplierName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GSTN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAbbreviation" runat="server" Text='<%# Eval("GSTN") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contract From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContractFrom" runat="server" Text='<%# Eval("ContractFrom") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ContractTo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContractTo" runat="server" Text='<%# Eval("ContractTo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS Applicable">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTdsApplicable" runat="server" Text='<%# Eval("IsTdsApplicable") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("SupplierRegistrationID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("SupplierRegistrationID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
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

