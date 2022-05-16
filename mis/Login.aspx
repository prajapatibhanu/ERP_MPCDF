<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>MPCDF</title>
	<link rel="shortcut icon" href="image/favicon.ico" type="image/ico" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/AdminLTE.css" rel="stylesheet" />
    <link href="css/blue.css" rel="stylesheet" />
    <link href="css/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <style type="text/css">
        body {
            background: #0074e2 url(image/login_left_bg.jpg);
        }

        .content-wrapper {
            margin-left: 0;
            color: #fff;
        }

        label {
            color: #fff;
        }

        .row {
            display: flex;
            flex-wrap: wrap;
            margin-right: -15px;
            margin-left: -15px;
        }

        .align-self-end {
            align-self: flex-end !important;
        }

        .form-group label {
            line-height: 1.4rem;
            vertical-align: top;
            margin-bottom: .5rem;
        }


        .input-group-text {
            font-weight: 400;
            line-height: 1;
            color: #495057;
            text-align: center;
            white-space: nowrap;
        }

        .flex-grow {
            flex-grow: 1;
        }

        .text-white {
            color: #ffffff !important;
        }

        .d-flex {
            display: flex;
        }

        .auth .login-half-bg {
            height: 100vh;
        }


        @media (min-width: 768px) {
            .auth.auth-img-bg .auth-form-transparent {
                margin: 20px auto;
            }
        }

        .auth .brand-logo {
            margin-bottom: 2rem;
        }



        .auth .form .auth-link:hover {
            color: initial;
        }

        .form-control {
            height: 40px;
            border-right: 0px;
            background-color: transparent;
            border-color: #fff;
            color: #fff;
        }

        .input-group {
            position: relative;
            display: flex;
            flex-wrap: wrap;
            align-items: stretch;
            width: 100%;
        }

        .p-3 {
            padding: 1rem !important;
        }

        .input-group > .form-control {
            position: relative;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
        }

        .input-group-text {
            display: flex;
            align-items: center;
            padding: 0.875rem 1.375rem;
            margin-bottom: 0;
            font-weight: 400;
            line-height: 1;
            color: #fff;
            font-size: 16px;
            text-align: center;
            white-space: nowrap;
            background-color: #fff;
            border: 1px solid #fff;
            border-left: 0px;
        }

        .bg-transparent {
            background-color: transparent !important;
        }

        .input-group-prepend, .input-group-append {
            display: flex;
        }

        ::-webkit-input-placeholder { /* Edge */
            color: red;
        }

        :-ms-input-placeholder { /* Internet Explorer */
            color: red;
        }

        ::placeholder {
            color: red;
        }

        .form-control:focus {
            border-color: #d2d6de;
        }

        .leftbg {
            background: url(image/login_left_bg.jpg);
        }

        .rightbg {
            background: #e8f2fc url(image/right_bg.jpg) no-repeat center center;
        }

        a {
            color: #fff;
        }

        .btn-login {
            color: #0569e1;
            background-color: #fff;
            border-color: rgba(255, 255, 255, 0.2);
            border-radius: 3px !important;
            text-transform: uppercase;
            font-weight: 600;
            letter-spacing: 1px;
        }

        .col-md-6 {
            width: 50%;
        }

        @media (max-width: 600px) {
            .rightbg {
                display: none;
            }

            .col-md-6 {
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="content-wrapper d-flex align-items-stretch auth auth-img-bg">
                    <div class="row flex-grow">
                        <div class="col-md-6 login-half-bg d-flex flex-row rightbg">
                            <h5 class="text-white font-weight-medium text-center flex-grow align-self-end">एम. पी. स्टेट को ऑपरेटिव डेयरी फेडरेशन</h5>
                        </div>
                        <div class="col-md-6 d-flex align-items-center justify-content-center leftbg">
                            <div class="auth-form-transparent text-left p-3">
                                <div class="brand-logo text-center text-uppercase">
                                     <%--  <p class="text-center flex-grow align-self-start text-center" style="margin-top: 20px;">
                                        <img src="image/erp_txt.png" />
                                    </p>
                                    <h4 style="margin: 40px 0;">Madhya Pradesh State Cooperative<br />
                                        Dairy Federation Ltd.</h4>
                                    <%--<img src="image/ds_logo_icon.png" />--%>
                                   
									
                                   <%-- <h4 style="padding: 20px 0; font-weight: 600">“मुनाफ़ा नहीं, सेवा ही लक्ष्य है”</h4>--%>
								    <p class="text-center flex-grow align-self-start text-center" style="margin-top: 15px;">
                                        <img src="image/erp_txt.png" />
                                    </p>
                                    <h4 style="margin: 30px 0;">Madhya Pradesh State Cooperative<br />
                                        Dairy Federation Ltd.</h4>
                                    <%--<img src="image/ds_logo_icon.png" />--%>
                                    <img src="image/logonew123.jpeg" />
									
                                    <h4 style="padding: 10px 0; font-weight: 600">“शुद्धता का संकल्प”</h4>
                                </div>
                                <%--<h6 class="font-weight-light">Happy to see you again!</h6>--%>
                                <div class="pt-3 form">
                                    <div class="form-group has-feedback">
                                        <asp:Label ID="LblMsg" runat="server"></asp:Label>
                                    </div>
                                    <asp:ValidationSummary ID="vsforgetpassword" runat="server" ValidationGroup="continue" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Login" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="final" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
                                    <div id="ForgetPassword" runat="server" visible="false">
                                        <asp:Panel ID="forget" runat="server" Visible="false" DefaultButton="btnContinue">
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="txtMobileNo" runat="server" autocomplete="off" class="form-control" onchange="CheckFUserName()" placeholder="Enter UserName" MaxLength="10"></asp:TextBox>
                                                <span class="fa fa-user form-control-feedback"></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Please Enter UserName" Display="None" SetFocusOnError="true" ValidationGroup="continue"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo" ValidationExpression="^[a-zA-Z0-9]*$" ErrorMessage="Invalid UserName Exp: XX0001" SetFocusOnError="true" Display="None" ValidationGroup="continue"></asp:RegularExpressionValidator>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="otp" runat="server" Visible="false" DefaultButton="btnValidate">
                                            <div class="form-group has-feedback">
                                                <asp:TextBox ID="txtOtp" runat="server" class="form-control" autocomplete="off" onchange="CheckOtp()" placeholder="Enter OTP" MaxLength="6" onkeypress="return validateNum(event);"></asp:TextBox>
                                                <span class="fa fa-lock form-control-feedback"></span>
                                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtOtp" ErrorMessage="Please Enter OTP" Display="None" SetFocusOnError="true" ValidationGroup="validate"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revOtp" runat="server" ControlToValidate="txtOtp" ValidationExpression="^\d+" ErrorMessage="Allowed Number only!" SetFocusOnError="true" Display="None" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="ChangePass" runat="server" Visible="false" DefaultButton="btnSave">
                                            <div class="form-group has-feedback">
                                                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" Display="None" ControlToValidate="txtNewPassword" ForeColor="Red" ErrorMessage="Enter Your New Password" Text="<i class='fa fa-exclamation-circle' title='Please Enter Your New Password !'></i>" SetFocusOnError="true" ValidationGroup="final"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="txtNewPassword" ValidationExpression="^[a-zA-z0-9-_@#!*$&^]+$" ErrorMessage="Special Character allowed only (-_@#!*$&^)." SetFocusOnError="true" ValidationGroup="final"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="CustomValidator1" ForeColor="Red" Display="None" SetFocusOnError="true" runat="server" ValidationGroup="final" ClientValidationFunction="ValidateStringLength" ControlToValidate="txtNewPassword" Text="<i class='fa fa-exclamation-circle' title='New Password lenght Minimum 6 and Maximum 15 characters allowed.!'></i>" ErrorMessage="New Password lenght Minimum 6 and Maximum 15 characters allowed."></asp:CustomValidator>
                                                <asp:TextBox ID="txtNewPassword" runat="server" ValidationGroup="final" onchange="ValNewPassword()" placeholder="Enter New Password (min length 6 character)" class="form-control" MaxLength="32" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                                                <span class="fa fa-lock form-control-feedback"></span>
                                            </div>
                                            <div class="form-group has-feedback">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server" Text="<i class='fa fa-exclamation-circle' title='Please Enter Confirm Password !'></i>" ControlToValidate="txtConfirmPassword" ForeColor="Red" ErrorMessage="Enter Confirm Password" SetFocusOnError="true" ValidationGroup="final"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" ControlToValidate="txtConfirmPassword" ValidationExpression="^[a-zA-z0-9-_@#!*$&^]+$" ErrorMessage="Special Character allowed only (-_@#!*$&^)." SetFocusOnError="true" ValidationGroup="final"></asp:RegularExpressionValidator>
                                                <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="Red" ControlToCompare="txtNewPassword" Display="None" Text="<i class='fa fa-exclamation-circle' title='Password Not Match!'></i>" ErrorMessage="Password Not Match!" SetFocusOnError="true" Operator="Equal" Type="String" ValidationGroup="final"></asp:CompareValidator>
                                                <asp:CustomValidator ID="cvNotes" runat="server" Display="None" ForeColor="Red" SetFocusOnError="true" ValidationGroup="final" ClientValidationFunction="ValidateStringLength" ControlToValidate="txtConfirmPassword" Text="<i class='fa fa-exclamation-circle' title='Confirm Password lenght Minimum 6 and Maximum 15 characters allowed.!'></i>" ErrorMessage="Confirm Password lenght Minimum 6 and Maximum 15 characters allowed."></asp:CustomValidator>
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" ValidationGroup="final" onchange="ValConfirmPassword()" placeholder="Enter Confirm Password (min length 6 character)" class="form-control" MaxLength="40" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                                                <span class="fa fa-lock form-control-feedback"></span>
                                            </div>
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-xs-6 pull-left">
                                                <asp:LinkButton ID="lnkbtnBack" runat="server" Text="Back to Login" OnClick="lnkbtnBack_Click"></asp:LinkButton>
                                            </div>
                                            <div class="col-xs-6 text-right">
                                                <asp:Button ID="btnValidate" runat="server" ValidationGroup="validate" Visible="false" class="btn btn-login btn-flat" Text="Validate" OnClientClick="return ValidateUserOtp();" OnClick="btnValidate_Click" />
                                                <asp:Button ID="btnContinue" runat="server" ValidationGroup="continue" Visible="false" class="btn btn-login btn-flat" Text="Continue" OnClientClick="return ValidateUserNameEmail();" OnClick="btnContinue_Click" />
                                                <asp:Button ID="btnSave" runat="server" ValidationGroup="final" Visible="false" class="btn btn-login btn-flat" Text="Save" OnClientClick="return ValidateNewPassword();" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="login" runat="server" Visible="true" DefaultButton="btnLogin">
                                        <div class="form-group">
                                            <label for="exampleInputEmail">Username</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtUserName" runat="server" autocomplete="off" class="form-control form-control-lg border-left-0" onchange="CheckBlankUserName()" placeholder="User Name" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUserId" runat="server" Display="None" ControlToValidate="txtUserName" Text="<i class='fa fa-exclamation-circle' title='Required to Fill User Name!'></i>" ErrorMessage="Required to Fill User Name!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="None" runat="server" ControlToValidate="txtUserName" ValidationExpression="^^[a-zA-Z0-9]*$" ErrorMessage="Invalid UserName Exp. XX0001" SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                                                <div class="input-group-prepend bg-transparent">
                                                    <span class="input-group-text bg-transparent">
                                                        <i class="fa fa-user"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword">Password</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtUserPassword" runat="server" autocomplete="off" class="form-control form-control-lg border-left-0" onchange="CheckBlankPassword()" placeholder="Password" TextMode="Password" MaxLength="50"></asp:TextBox>
                                                <div class="input-group-prepend bg-transparent">
                                                    <span class="input-group-text bg-transparent">
                                                        <i class="fa fa-lock"></i>
                                                    </span>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvpass" runat="server" Display="None" ControlToValidate="txtUserPassword" ErrorMessage="Required to Fill Password!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="None" runat="server" ControlToValidate="txtUserPassword" ValidationExpression="^[a-zA-z0-9-_@#!*$&^]+$" ErrorMessage="Special Character allowed only (-_@#!*$&^)." SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="text-left" style="padding-top: 5px; float: left;">
                                                <%--<asp:CheckBox ID="cbRemember" runat="server" Text="Remember" /><br />--%>
                                                <b>
                                                    <asp:LinkButton ID="lnkForget" runat="server" Text="Forgot Password?" OnClientClick="return ForgetPassword()" OnClick="lnkForget_Click"></asp:LinkButton></b>
                                            </div>
                                            <div class="right" style="float: right;">
                                                <asp:Button ID="btnLogin" runat="server" class="btn btn-login btn-flat" ValidationGroup="Login" OnClientClick="return ValidatePage();" Text="Sign In" OnClick="btnLogin_Click" />
                                                <%-- <asp:Button ID="btnLogin" runat="server" class="btn btn-success btn-block btn-flat" ValidationGroup="Login" Text="Sign In" OnClick="btnLogin_Click" />--%>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <%--<div class="my-3">
                  <a class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" href="../../index.html">LOGIN</a>
                </div>
                <div class="text-center mt-4 font-weight-light">
                  Don't have an account? <a href="register-2.html" class="text-primary">Create</a>
                </div>--%>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <script src="js/jquery.js"></script>
        <script src="js/bootstrap.js"></script>
        <script src="js/icheck.min.js"></script>
        <script src="js/sha512.js"></script>
        <script type="text/javascript">
            function PopOTPVerification() {
                $('#myModal').modal({ backdrop: 'static', keyboard: false }, 'show');
            }
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                });
            });
        </script>
        <script>
            function ValidatePage() {
                debugger;
                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate('Login');
                }
                if (Page_IsValid) {
                    if (document.getElementById('<%= txtUserPassword.ClientID %>').value.length != 128) {
                        document.getElementById('<%= txtUserPassword.ClientID %>').value =
                        SHA512(SHA512(document.getElementById('<%= txtUserPassword.ClientID %>').value) +
                    '<%= ViewState["RandomText"].ToString() %>');
                    }
                }
                else {
                    if (document.getElementById('<%= txtUserName.ClientID %>').value == "") {
                        $("input[name='txtUserName']").removeClass('TextBoxSuccess');
                        $("input[name='txtUserName']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtUserName']").removeClass('TextBoxError');
                        $("input[name='txtUserName']").addClass('TextBoxSuccess');
                    }
                    if (document.getElementById('<%= txtUserPassword.ClientID %>').value == "") {
                        $("input[name='txtUserPassword']").removeClass('TextBoxSuccess');
                        $("input[name='txtUserPassword']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtUserPassword']").removeClass('TextBoxError');
                        $("input[name='txtUserPassword']").addClass('TextBoxSuccess');
                    }
                    return false;
                }
            }

            function ValidateUserNameEmail() {
                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate('continue');
                }
                if (!Page_IsValid) {
                    if (document.getElementById('<%= txtMobileNo.ClientID %>').value == "") {
                        $("input[name='txtMobileNo']").removeClass('TextBoxSuccess');
                        $("input[name='txtMobileNo']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtMobileNo']").removeClass('TextBoxError');
                        $("input[name='txtMobileNo']").addClass('TextBoxSuccess');
                    }
                }
            }

            function ValidateUserOtp() {
                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate('validate');
                }
                if (!Page_IsValid) {
                    if (document.getElementById('<%= txtOtp.ClientID %>').value == "") {
                        $("input[name='txtOtp']").removeClass('TextBoxSuccess');
                        $("input[name='txtOtp']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtOtp']").removeClass('TextBoxError');
                        $("input[name='txtOtp']").addClass('TextBoxSuccess');
                    }
                }
            }

            function CheckFUserName() {
                if (document.getElementById('<%= txtMobileNo.ClientID %>').value == "") {
                    $("input[name='txtMobileNo']").removeClass('TextBoxSuccess');
                    $("input[name='txtMobileNo']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtMobileNo']").removeClass('TextBoxError');
                    $("input[name='txtMobileNo']").addClass('TextBoxSuccess');
                }
            }

            function CheckOtp() {
                if (document.getElementById('<%= txtOtp.ClientID %>').value == "") {
                    $("input[name='txtOtp']").removeClass('TextBoxSuccess');
                    $("input[name='txtOtp']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtOtp']").removeClass('TextBoxError');
                    $("input[name='txtOtp']").addClass('TextBoxSuccess');
                }
            }

            function CheckBlankUserName() {
                if (document.getElementById('<%= txtUserName.ClientID %>').value == "") {
                    $("input[name='txtUserName']").removeClass('TextBoxSuccess');
                    $("input[name='txtUserName']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtUserName']").removeClass('TextBoxError');
                    $("input[name='txtUserName']").addClass('TextBoxSuccess');
                }
            }

            function CheckBlankPassword() {
                if (document.getElementById('<%= txtUserPassword.ClientID %>').value == "") {
                    $("input[name='txtUserPassword']").removeClass('TextBoxSuccess');
                    $("input[name='txtUserPassword']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtUserPassword']").removeClass('TextBoxError');
                    $("input[name='txtUserPassword']").addClass('TextBoxSuccess');
                }
            }

            function ValidateNewPassword() {
                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate('final');
                }
                if (!Page_IsValid) {
                    if (document.getElementById('<%= txtNewPassword.ClientID %>').value == "") {
                        $("input[name='txtNewPassword']").removeClass('TextBoxSuccess');
                        $("input[name='txtNewPassword']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtNewPassword']").removeClass('TextBoxError');
                        $("input[name='txtNewPassword']").addClass('TextBoxSuccess');
                    }
                    if (document.getElementById('<%= txtConfirmPassword.ClientID %>').value == "") {
                        $("input[name='txtConfirmPassword']").removeClass('TextBoxSuccess');
                        $("input[name='txtConfirmPassword']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtConfirmPassword']").removeClass('TextBoxError');
                        $("input[name='txtConfirmPassword']").addClass('TextBoxSuccess');
                    }
                    return false;
                }
                else {
                    document.getElementById('<%= txtNewPassword.ClientID %>').value =
                        SHA512(document.getElementById('<%= txtNewPassword.ClientID %>').value);

                    document.getElementById('<%= txtConfirmPassword.ClientID %>').value =
                        SHA512(document.getElementById('<%= txtConfirmPassword.ClientID %>').value);
                    return true;
                }
            }

            function ValNewPassword() {
                if (document.getElementById('<%= txtNewPassword.ClientID %>').value == "") {
                        $("input[name='txtNewPassword']").removeClass('TextBoxSuccess');
                        $("input[name='txtNewPassword']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtNewPassword']").removeClass('TextBoxError');
                        $("input[name='txtNewPassword']").addClass('TextBoxSuccess');
                    }
                }

                function ValConfirmPassword() {
                    if (document.getElementById('<%= txtConfirmPassword.ClientID %>').value == "") {
                        $("input[name='txtConfirmPassword']").removeClass('TextBoxSuccess');
                        $("input[name='txtConfirmPassword']").addClass('TextBoxError');
                    }
                    else {
                        $("input[name='txtConfirmPassword']").removeClass('TextBoxError');
                        $("input[name='txtConfirmPassword']").addClass('TextBoxSuccess');
                    }
                }

                function ValidateStringLength(source, arguments) {
                    var slen = arguments.Value.length;
                    // alert(arguments.Value + '\n' + slen);
                    if (slen >= 6 && slen <= 15) {
                        arguments.IsValid = true;
                    }
                    else {
                        arguments.IsValid = false;
                    }
                }
        </script>
		<script type="text/javascript">
            function preventBack() { window.history.forward(); }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
        </script>
    </form>
</body>
</html>
