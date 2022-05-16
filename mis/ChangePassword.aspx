<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="mis_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <h3 class="box-title">CHANGE PASSWORD</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">

                                <div class="col-md-12">
                                    <%--  <h3 class="box-title">CHANGE PASSWORD</h3>--%>
                                    <br />
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    <asp:ValidationSummary ID="vsumary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="save" HeaderText="Errors: " />
                                </div>

                            </div>
                            <div class="row" runat="server" id="dv_PasswordSection">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OLD PASSWORD<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvOldPass" runat="server" ControlToValidate="txtOldPassword" ForeColor="Red" ErrorMessage="Enter Your Old Password" Text="<i class='fa fa-exclamation-circle' title='Please Enter Your Old Password !'></i>" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtOldPassword" runat="server" placeholder="Enter Old Password (min length 6 character)" class="form-control" MaxLength="32" autocomplete="off" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>NEW PASSWORD<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" Display="Dynamic" ControlToValidate="txtNewPassword" ForeColor="Red" ErrorMessage="Enter Your New Password" Text="<i class='fa fa-exclamation-circle' title='Please Enter Your New Password !'></i>" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" runat="server" ValidationGroup="save" ClientValidationFunction="ValidateStringLength" ControlToValidate="txtNewPassword" Text="<i class='fa fa-exclamation-circle' title='New Password lenght Minimum 6 characters and Maximum 15 characters allowed.!'></i>" ErrorMessage="Password lenght Minimum 6 characters and Maximum 15 characters allowed."></asp:CustomValidator>
                                        </span>
                                        <asp:TextBox ID="txtNewPassword" runat="server" placeholder="Enter New Password (min length 6 character)" class="form-control" MaxLength="32" autocomplete="off" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>CONFIRM NEW PASSWORD<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" Text="<i class='fa fa-exclamation-circle' title='Please Enter Confirm Password !'></i>" ControlToValidate="txtConfirmPassword" ForeColor="Red" ErrorMessage="Enter Confirm Password" SetFocusOnError="true" ValidationGroup="save"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="Red" ControlToCompare="txtNewPassword" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Password Not Match!'></i>" ErrorMessage="Password Not Match!" SetFocusOnError="true" Operator="Equal" Type="String" ValidationGroup="save"></asp:CompareValidator>
                                            <asp:CustomValidator ID="cvNotes" runat="server" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="save" ClientValidationFunction="ValidateStringLength" ControlToValidate="txtConfirmPassword" Text="<i class='fa fa-exclamation-circle' title='Confirm Password lenght Minimum 6 characters and Maximum 15 characters allowed.!'></i>" ErrorMessage="Password lenght Minimum 6 characters and Maximum 15 characters allowed."></asp:CustomValidator>
                                        </span>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Enter Confirm Password (min length 6 character)" class="form-control" MaxLength="40" autocomplete="off" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnGenerateOtp" runat="server" Text="Generate OTP" ValidationGroup="save" class="btn btn-primary" OnClientClick="return ValidatePage();" OnClick="btnGenerateOtp_Click" />

                                        <asp:Button ID="Button2" runat="server" Text="Clear" class="btn btn-default" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
							<div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblNote" runat="server" Text="Note:OTP will be received on registered Email."></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="dv_otpSection" runat="server" visible="false">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OTP<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOTP" ForeColor="Red" ErrorMessage="Enter your OTP" Text="<i class='fa fa-exclamation-circle' title='Please Enter your OTP !'></i>" SetFocusOnError="true" ValidationGroup="confirm"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtOTP" runat="server" autocomplete="off" placeholder="Enter OTP)" class="form-control" MaxLength="6" onkeypress="return validateNum(event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Style="margin-top: 20px;" Text="Confirm" ValidationGroup="confirm" class="btn btn-primary" OnClientClick="return ConfirmPage();" OnClick="btnChange_Click" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" class="btn btn-default" OnClick="btnCancel_Click" />
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
    <script src="../js/sha512.js"></script>
    <script type="text/javascript">

        function ValidateStringLength(source, arguments) {
            var slen = arguments.Value.length;
            // alert(arguments.Value + '\n' + slen);
            if (slen >= 6 && slen <= 15) {
                arguments.IsValid = true;
            } else {
                arguments.IsValid = false;
            }
        }

        function GetConversion() {
            document.getElementById('<%= txtOldPassword.ClientID %>').value =
                SHA512(SHA512(document.getElementById('<%= txtOldPassword.ClientID %>').value) +
                '<%= ViewState["RandomText"].ToString() %>');

            document.getElementById('<%= txtNewPassword.ClientID %>').value =
                SHA512(document.getElementById('<%= txtNewPassword.ClientID %>').value);

            document.getElementById('<%= txtConfirmPassword.ClientID %>').value =
               SHA512(document.getElementById('<%= txtConfirmPassword.ClientID %>').value);
        }

        function ConfirmPage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('confirm');
            }

            if (Page_IsValid) {
                if (confirm('Are you sure you want to confirm OTP and change password?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {
                GetConversion();
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

