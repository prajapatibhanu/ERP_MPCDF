<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DistributorReg.aspx.cs" Inherits="mis_Common_DistributorReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Distributor Registration</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Distributor Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Distributor Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                            ErrorMessage="Enter Distributor Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor Name !'></i>"
                                            ControlToValidate="txtDistributorName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtDistributorName"

                                             ErrorMessage="Enter Distributor/Super Stockist Name" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor/Super Stockist Name'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.]+$"></asp:RegularExpressionValidato--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistributorName" MaxLength="60" placeholder="Enter Distributor Name" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Distributor Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Distributor Type"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Distributor Type !'></i>"
                                            ControlToValidate="ddlDistributorType" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlDistributorType" runat="server" OnInit="ddlDistributorType_Init" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Distributor Code<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic"
                                        ValidationGroup="a" runat="server"
                                        ControlToValidate="txtDistributorCode" ErrorMessage="Enter Distributor Code"
                                        Text="<i class='fa fa-exclamation-circle' title='Enter Distributor Code !'></i>">

                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="Dynamic"
                                        ValidationGroup="a"
                                        ErrorMessage="Invalid Distributor Code. !"
                                        ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Distributor Code !'></i>"
                                        ControlToValidate="txtDistributorCode"
                                        ValidationExpression="[0-9a-zA-Z]+$">
                                    </asp:RegularExpressionValidator>



                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistributorCode" MaxLength="10" placeholder="Enter Distributor Code" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Distributor Landline No.<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            ErrorMessage="Enter Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact No. !'></i>"
                                            ControlToValidate="txtDistributorContactNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic"
                                            ValidationGroup="a"
                                            ErrorMessage="Invalid Valid Distributor Landline No. !"
                                            ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Valid Distributor Landline No !'></i>"
                                            ControlToValidate="txtDistributorContactNo"
                                            ValidationExpression="^[0-9]{11}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistributorContactNo" MaxLength="11" onkeypress="return validateNum(event);" placeholder="example- 07552732558"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact Person<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person!'></i>"
                                            ControlToValidate="txtContactPerson" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtContactPerson"

                                             ErrorMessage="Enter Contact Person Name"
                                                Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person Name'></i>" 
                                               SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtContactPerson" MaxLength="60" placeholder="Enter Contact Person"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact Person Mobile No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Person Mobile No. !'></i>"
                                            ControlToValidate="txtContactPersonMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Invalid Valid Contact Person Mobile No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Valid Contact Person Mobile No. !'></i>" ControlToValidate="txtContactPersonMobileNo"
                                            ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtContactPersonMobileNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Contact Person Mobile No."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Email<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--  <asp:RequiredFieldValidator ID="rfvOfficeEmail" ValidationGroup="a"
                                            ErrorMessage="Enter Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Email !'></i>"
                                            ControlToValidate="txtEmail" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEmail" MaxLength="50" placeholder="Enter Email" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PAN Number</label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person!'></i>"
                                            ControlToValidate="txtContactPerson" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ForeColor="Red" ControlToValidate="txtPanCard"
                                            ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" Display="Dynamic" ErrorMessage="Invalid PAN Card" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid PAN Card !'></i>" />


                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPanCard" MaxLength="10" placeholder="Enter PAN Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>GST Number<%-- <span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person!'></i>"
                                            ControlToValidate="txtContactPerson" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                            ForeColor="Red" ControlToValidate="txtGSTIN"
                                            ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"
                                            Display="Dynamic" ErrorMessage="Invalid GSTIN" ValidationGroup="a"
                                            Text="<i class='fa fa-exclamation-circle' title='Invalid GSTIN !'></i>" />

                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtGSTIN" MaxLength="15" placeholder="Enter GST Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Divison<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                            ControlToValidate="ddlDivision" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDivision" OnInit="ddlDivision_Init" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>District<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select District" Text="<i class='fa fa-exclamation-circle' title='Select District !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Block Name<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv4" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red"
                                            ErrorMessage="Select Block" Text="<i class='fa fa-exclamation-circle' title='Select Block !'></i>"
                                            ControlToValidate="ddlBlock_Name" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlBlock_Name" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>


                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>City/Village<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                            ErrorMessage="Enter City/village" Text="<i class='fa fa-exclamation-circle' title='Enter City/village !'></i>"
                                            ControlToValidate="txtTownOrvillage" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtTownOrvillage" ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTownOrvillage" MaxLength="80" placeholder="Enter City Or Village"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Address<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficeaddress" ValidationGroup="a"
                                            ErrorMessage="Enter Address" Text="<i class='fa fa-exclamation-circle' title='Enter Address !'></i>"
                                            ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtAddress"

                                             ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allowed" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAddress" MaxLength="150" placeholder="Enter Address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Pincode<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficepincode" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficepincode" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtPincode" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPincode" MaxLength="6" placeholder="Enter Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Aadhaar No.</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtAadhaarNo" ErrorMessage="Invalid Aadhaar" Text="<i class='fa fa-exclamation-circle' title='Invalid Aadhaar !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9]{12}$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAadhaarNo" MaxLength="12" placeholder="Enter Aadhar" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Product Rate Type(Only for Product)</label>
                                    <asp:DropDownList ID="ddlProductRateType" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Tcs Tax(Is Applicable)</label>
                                    <asp:CheckBox ID="chkIsTcsTax" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>Tds Tax(Is Applicable)</label>
                                    <asp:CheckBox ID="chkIsTdsTax" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Self SuperStockist</label>
                                    <asp:CheckBox ID="ChkSelfSuperStockist" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Registration Date</label>
                                    <span class="pull-right">
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter mDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtRegistrationDate"
                                            ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtRegistrationDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Max Supply Limit
                        </legend>
                        <div class="row">
                            <div class="col-md-3" runat="server" id="pnlApprovedrate" visible="false">
                                <div class="form-group">
                                    <label>Approved Rate<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtApprovedRate" ErrorMessage="Invalid Approved Rate(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Approved Rate(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtApprovedRate" MaxLength="10" onkeypress="return validateDec(this,event)" placeholder="Enter Approved Rate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="pnlTransrate" visible="false">
                                <div class="form-group">
                                    <label>Trans. Rate<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtTransRate" ErrorMessage="Invalid Trans. Rate(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Trans. Rate(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTransRate" MaxLength="10" onkeypress="return validateDec(this,event)" placeholder="Trans. Rate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="pnldisrate" visible="false">
                                <div class="form-group">
                                    <label>Dist. Rate<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDistRate" ErrorMessage="Invalid Dist. Rate(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Dist. Rate(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDistRate" MaxLength="10" onkeypress="return validateDec(this,event)" placeholder="Enter Dist. Rate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Max Supply Limit<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RFV" ValidationGroup="a"
                                            ErrorMessage="Enter Max Supply Limit" Text="<i class='fa fa-exclamation-circle' title='Enter Max Supply Limit !'></i>"
                                            ControlToValidate="txtLimit" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtLimit" ErrorMessage="Invalid Limit(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Limit(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtLimit" MaxLength="12" onkeypress="return validateDec(this,event)" placeholder="Enter Max Supply Limit"></asp:TextBox>
                                </div>
                            </div>
							<div class="col-md-3">
                                <div class="form-group">
                                    <label>Security Deposit<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                            ErrorMessage="Enter Security Deposit" Text="<i class='fa fa-exclamation-circle' title='Enter Security Deposit !'></i>"
                                            ControlToValidate="txtSecurityDeposit" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSecurityDeposit" ErrorMessage="Invalid Security Deposit(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Limit(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSecurityDeposit" Text="0" MaxLength="12" onkeypress="return validateDec(this,event)" placeholder="Enter Security Deposit"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bank Guarantee<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                            ErrorMessage="Enter Bank Guarantee" Text="<i class='fa fa-exclamation-circle' title='Enter Bank Guarantee !'></i>"
                                            ControlToValidate="txtBankGuarantee" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator16" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankGuarantee" ErrorMessage="Invalid Bank Guarantee(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Limit(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBankGuarantee" Text="0" MaxLength="12" onkeypress="return validateDec(this,event)" placeholder="Enter Bank Guarantee"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Bank Details</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bank Name<span style="color: red;" id="pnlBankName" runat="server" visible="false"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" Enabled="false"
                                            InitialValue="0" ErrorMessage="Select Bank Name" Text="<i class='fa fa-exclamation-circle' title='Select Bank Name !'></i>"
                                            ControlToValidate="ddlBank" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlBank" AutoPostBack="true" runat="server" OnInit="ddlBank_Init" CssClass="form-control select2" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Branch Name<%--<span style="color: red;"> *</span>--%></label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Branch Name" Text="<i class='fa fa-exclamation-circle' title='Select Branch Name !'></i>"
                                            ControlToValidate="ddlBranchName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                    </span>--%>
                                    <%-- <asp:DropDownList ID="ddlBranchName" AutoPostBack="true" runat="server" OnInit="ddlBranchName_Init" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static">
                                    </asp:DropDownList>--%>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ForeColor="Red" ValidationGroup="a"
                                            Display="Dynamic" runat="server" ControlToValidate="txtBranchName"
                                            ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allowed" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>

                                    </span>
                                    <asp:TextBox runat="server" automplete="off" CssClass="form-control" ID="txtBranchName" MaxLength="50"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-3" id="pnlifsc" runat="server">
                                <div class="form-group">
                                    <label>IFSC Code <span style="color: red;" id="pnlNewIFSC" runat="server" visible="false">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a" Enabled="false"
                                            ErrorMessage="Enter IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Enter IFSC Code !'></i>"
                                            ControlToValidate="txtIFSCCode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                            ErrorMessage="Invalid IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Invalid IFSC Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]{11}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" automplete="off" CssClass="form-control" ID="txtIFSCCode" MaxLength="11"></asp:TextBox>
                                    <%--<asp:AutoCompleteExtender ServiceMethod="SearchIFSC"
                                        MinimumPrefixLength="2"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtIFSCCode" CompletionListCssClass="AutoExtender"
                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bank Account No.<span style="color: red;" id="pnlAccntNo" runat="server" visible="false"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a" Enabled="false"
                                            ErrorMessage="Enter Account No" Text="<i class='fa fa-exclamation-circle' title='Enter Account No. !'></i>"
                                            ControlToValidate="txtBankAccountNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankAccountNo"
                                            ErrorMessage="Invalid Bank Account No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Bank Account No. !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9]{10,18}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBankAccountNo" MaxLength="18" placeholder="Account No. can be of max 18 digits" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <!-- /.box-body -->

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Distributor Registration Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Distributor Name</label>
                                <asp:DropDownList ID="ddlDist" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" PageSize="50" runat="server"
                                class="datatable table table-hover table-bordered pagination-ys"
                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found." DataKeyNames="DistributorId">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>' />
                                            <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>' />
                                            <asp:Label ID="lblBlock_ID" Visible="false" runat="server" Text='<%# Eval("Block_ID") %>' />
                                            <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%# Eval("Email") %>' />
                                            <asp:Label ID="lblDAddress" Visible="false" runat="server" Text='<%# Eval("DAddress") %>' />
                                            <asp:Label ID="lblDPincode" Visible="false" runat="server" Text='<%# Eval("DPincode") %>' />
                                            <asp:Label ID="lblBank_id" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                            <%-- <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>' />--%>
                                            <asp:Label ID="lblBranchName" Visible="false" runat="server" Text='<%# Eval("BranchName") %>' />
                                            <asp:Label ID="lblIFSCCode" Visible="false" runat="server" Text='<%# Eval("IFSC_Code") %>' />
                                            <asp:Label ID="lblBankAccountNo" Visible="false" runat="server" Text='<%# Eval("BankAccountNo") %>' />
                                            <asp:Label ID="lblDContactNo" Visible="false" runat="server" Text='<%# Eval("DContactNo") %>' />
                                            <asp:Label ID="lblDName" runat="server" Text='<%# Eval("DName") %>' />
                                            <asp:Label ID="lblTownOrVillage" runat="server" Visible="false" Text='<%# Eval("TownOrVillage") %>' />
                                            <asp:Label ID="lblVendorTypeId" Visible="false" runat="server" Text='<%# Eval("VendorTypeId") %>' />

                                            <asp:Label ID="lblGSTNo" runat="server" Visible="false" Text='<%# Eval("GSTNo") %>' />
                                            <asp:Label ID="lblComsApprovedRate" Visible="false" runat="server" Text='<%# Eval("ComsApprovedRate") %>' />
                                            <asp:Label ID="lblComsDistRate" Visible="false" runat="server" Text='<%# Eval("ComsDistRate") %>' />
                                            <asp:Label ID="lblComsTransRate" Visible="false" runat="server" Text='<%# Eval("ComsTransRate") %>' />
                                            <asp:Label ID="lblProductRateTypeId" Visible="false" runat="server" Text='<%# Eval("ProductRateTypeId") %>' />
                                            <asp:Label ID="lblIsTcsTax" Visible="false" runat="server" Text='<%# Eval("IsTcsTax") %>' /> 
                                            <asp:Label ID="lblIsTdsTax" Visible="false" runat="server" Text='<%# Eval("IsTdsTax") %>' />
                                            <asp:Label ID="lblSelfSuperStockist" Visible="false" runat="server" Text='<%# Eval("SelfSuperStockist") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor UserName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCode" runat="server" Text='<%# Eval("DCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCPersonName" runat="server" Text='<%# Eval("DCPersonName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person Mobile No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDCPersonMobileNo" runat="server" Text='<%# Eval("DCPersonMobileNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAN No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPANNo" runat="server" Text='<%# Eval("PANNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aadhaar No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAadhaarNo" runat="server" Text='<%# Eval("AadhaarNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Eval("Registration_Date") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Limit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLimit" runat="server" Text='<%# Eval("Limit") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="Security Deposit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecurityDeposit" runat="server" Text='<%# Eval("SecurityDeposit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Guarantee">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankGuarantee" runat="server" Text='<%# Eval("BankGuarantee") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("DistributorId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("DistributorId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
 <asp:Label ID="lblDCoDE" runat="server" Visible="false" />  
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
            lengthMenu: [10, 25, 50, 100, 200, 500],
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
                    title: ('Distributor Details').bold().fontsize(3).toUpperCase(),
                    text: '<i class="fa fa-print"></i> Print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Distributor Details').bold().fontsize(3).toUpperCase(),
                    filename: 'Distributor Registration Details',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
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

