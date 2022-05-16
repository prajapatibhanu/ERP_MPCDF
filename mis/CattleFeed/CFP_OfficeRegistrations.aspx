<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_OfficeRegistrations.aspx.cs" Inherits="mis_CattelFeed_CFP_OfficeRegistrations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
                           <i class="fa fa-2x fa-question-circle"></i>&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-5">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Cattle Feed Plant Registration</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Plant Registration
                                </legend>
                                <div class="row">                                
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Office Name<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Plant Name<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Name" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Name !'></i>"
                                                        ControlToValidate="txtOfficeName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a"
                                                        ErrorMessage="Invalid Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Name !'></i>"
                                                        ControlToValidate="txtOfficeName" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtOfficeName" runat="server" placeholder="Plant Name..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Plant Name (Hindi)<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Name" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Name !'></i>"
                                                        ControlToValidate="txtofficeNameHI" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtofficeNameHI" runat="server" placeholder="Plant Name..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Plant Code<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Code" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Code !'></i>"
                                                        ControlToValidate="txtofficeCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtofficeCode" runat="server" placeholder="Office Code..." class="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Production Capacity(in MT)<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                        ErrorMessage="Enter Production Capacity" Text="<i class='fa fa-exclamation-circle' title='Enter Production Capacity !'></i>"
                                                        ControlToValidate="txtcapacity" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:TextBox ID="txtcapacity" runat="server" placeholder="Production Capacity..." onpaste="return false;" class="form-control" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Contact No</label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="rvDigits" Display="Dynamic" runat="server" ControlToValidate="txtcontactno" ErrorMessage="Enter numbers only till 10 digit" Text="<i class='fa fa-exclamation-circle' title='Enter  Contact No !'></i>" ValidationGroup="a" ForeColor="Red" ValidationExpression="\d{10}" />
                                                </span>
                                                <asp:TextBox ID="txtcontactno" runat="server" placeholder="Contact No..." onpaste="return false;" class="form-control" MaxLength="10" onkeypress="return onlyNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Pincode</label>
                                            <asp:TextBox ID="txtPincode" runat="server" placeholder="Pindoce..." class="form-control" MaxLength="6" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        </div>
                                    </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Email<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a"
                                                        ErrorMessage="Invalid Email" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>"
                                                        ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email..." class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Division<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                                        ControlToValidate="ddlDivision" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>District<span class="text-danger"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a" InitialValue="0"
                                                        ErrorMessage="Select District" Text="<i class='fa fa-exclamation-circle' title='Select District !'></i>"
                                                        ControlToValidate="ddlDistrict" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>GSTN</label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a"
                                                        ErrorMessage="Invalid GSTN" Text="<i class='fa fa-exclamation-circle' title='Invalid GSTN !'></i>"
                                                        ControlToValidate="txtGSTN" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtGSTN" runat="server" placeholder="GSTN..." MaxLength="15" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>PAN No.(10 digit)</label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a"
                                                        ErrorMessage="Invalid PAN" Text="<i class='fa fa-exclamation-circle' title='Invalid PAN !'></i>"
                                                        ControlToValidate="txtPAN" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtPAN" runat="server" placeholder="PAN NO..." MaxLength="10" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>TAN NO</label>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="a"
                                                        ErrorMessage="Invalid TAN" Text="<i class='fa fa-exclamation-circle' title='Invalid TAN !'></i>"
                                                        ControlToValidate="txtTAN" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^(?=.*[a-zA-Z])(?=.*[0-9])[A-Za-z0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtTAN" runat="server" placeholder="TAN NO..." MaxLength="10" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Address<span class="text-danger"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                    ErrorMessage="Enter Address" Text="<i class='fa fa-exclamation-circle' title='Enter Address !'></i>"
                                                    ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a"
                                                    ErrorMessage="Invalid Address" Text="<i class='fa fa-exclamation-circle' title='Invalid Address !'></i>"
                                                    ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" runat="server" ValidationExpression="^[A-Za-z0-9? ,_-]+$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" placeholder="Address..." class="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                 </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClientClick="return ValidatePage();" ID="btnSave" Text="Save" CausesValidation="true" ValidationGroup="a" />
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
                <div class="col-md-7">
                    <div class="box box-Manish">
                        <div class="box-body">
                            <fieldset>
                                <legend>Registered Plant List
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdCatlist" PageSize="20" runat="server" class="table table-hover table-bordered pagination-ys"
                                            AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Office_ID" OnRowCommand="grdCatlist_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DS Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPOffice_Name" runat="server" Text='<%# Eval("POffice_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Parant_ID" Visible="false" runat="server" Text='<%# Eval("Office_Parant_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_ContactNo" Visible="false" runat="server" Text='<%# Eval("Office_ContactNo") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Email" Visible="false" runat="server" Text='<%# Eval("Office_Email") %>'></asp:Label>
                                                        <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Address" Visible="false" runat="server" Text='<%# Eval("Office_Address") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Pincode" Visible="false" runat="server" Text='<%# Eval("Office_Pincode") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Gst" Visible="false" runat="server" Text='<%# Eval("Office_Gst") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Name_E" Visible="false" runat="server" Text='<%# Eval("Office_Name_E") %>'></asp:Label>
                                                        <asp:Label ID="lblTAN_NO" Visible="false" runat="server" Text='<%# Eval("TAN_NO") %>'></asp:Label>
                                                        <asp:Label ID="lblOffice_Pan" Visible="false" runat="server" Text='<%# Eval("Office_Pan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Plant Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Production Capacity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCapacity" runat="server" Text='<%# Eval("Capacity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkSelect" runat="server" CausesValidation="false" CommandName="RecordUpdate" CommandArgument='<%#Eval("Office_ID") %>' Text="Edit" OnClientClick="return confirm('CFP Entry will be edit. Are you sure want to continue?');"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <%-- <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="false" CommandName="RecordDelete" CommandArgument='<%#Eval("Office_ID") %>' Text="Delete" Style="color: red;" OnClientClick="return confirm('CFP Entry will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>--%>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function onlyNumberKey(evt) {

            // Only ASCII charactar in that range allowed 
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        }

        function ValidatePage() {
            debugger;
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>

</asp:Content>

