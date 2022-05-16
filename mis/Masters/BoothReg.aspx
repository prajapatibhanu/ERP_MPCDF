<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BoothReg.aspx.cs" Inherits="mis_Common_BoothReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
    </style>


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
                    <h3 class="box-title">Retailer Registration</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Retailer Registration Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Location<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                            ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Route No<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRoute" InitialValue="0" ErrorMessage="Select Route." Text="<i class='fa fa-exclamation-circle' title='Select Route Sangh !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlRoute" OnInit="ddlRoute_Init" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlRetailerType" InitialValue="0" ErrorMessage="Select Retailer Type." Text="<i class='fa fa-exclamation-circle' title='Select Retailer Type !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlRetailerType" OnSelectedIndexChanged="ddlRetailerType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Panel ID="pnlInstitution" Visible="false" runat="server">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Institution Type<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlOrganizationType" InitialValue="0" ErrorMessage="Select Institution Type." Text="<i class='fa fa-exclamation-circle' title='Select Institution Type !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlOrganizationType" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Government" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Private" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Others" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Delivery Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
                                                InitialValue="0" ForeColor="Red" ErrorMessage="Select Delivery Type" Text="<i class='fa fa-exclamation-circle' title='Select Delivery Type !'></i>"
                                                ControlToValidate="ddlDeliveryType" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlDeliveryType" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Distributor/Super Stockist" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Sub-Distributor" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="Self" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                            ErrorMessage="Enter Retailer Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sub Distributor Name !'></i>"
                                            ControlToValidate="txtBoothName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBoothName"
                                            ErrorMessage="Only Alphabet allow in Retailer Name" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet allow in Retailer Name !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-zA-Z\s.]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBoothName" MaxLength="100" placeholder="Enter Retailer Name" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer Name(Hindi)</label>
                                    <span class="pull-right">
                                        <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBoothName"
                                            ErrorMessage="Only Alphabet allow in Retailer Name" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet allow in Retailer Name !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-zA-Z\s.]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBoothNameHindi" MaxLength="100" placeholder="Enter Retailer Name(Hindi)" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer Code<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtBCode" ErrorMessage="Enter Retailer Code" Text="<i class='fa fa-exclamation-circle' title='Enter Retailer Code !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBCode" MaxLength="10" placeholder="Enter Retailer Code" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retailer Landline No.<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            ErrorMessage="Enter Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact No. !'></i>"
                                            ControlToValidate="txtDistributorContactNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Retailer Landline No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Retailer Landline No !'></i>" ControlToValidate="txtBContactNo"
                                            ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBContactNo" MaxLength="11" onkeypress="return validateNum(event);" placeholder="example- 07552732558"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Contact Person<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person !'></i>"
                                            ControlToValidate="txtContactPerson" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtContactPerson" ErrorMessage="Alphabet and space allow" Text="<i class='fa fa-exclamation-circle' title='Alphabet and space allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtContactPerson" MaxLength="100" placeholder="Enter Contact Person"></asp:TextBox>
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
                                            ErrorMessage="Enter Valid Contact Person Mobile No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact Person Mobile No. !'></i>" ControlToValidate="txtContactPersonMobileNo"
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
                                        <%--<asp:RequiredFieldValidator ID="rfvOfficeEmail" ValidationGroup="a"
                                            ErrorMessage="Enter Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Email !'></i>"
                                            ControlToValidate="txtEmail" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtEmail" MaxLength="70" placeholder="Enter Email" ClientIDMode="Static"></asp:TextBox>
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
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                        CssClass="form-control select2" ClientIDMode="Static">
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
                                        <asp:RegularExpressionValidator ID="revcv" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server"
                                            ControlToValidate="txtTownOrvillage" ErrorMessage="Only alphabet allowed"
                                            Text="<i class='fa fa-exclamation-circle' title='Only alphabet allowed'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
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
                                        <%--   <asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" 
                                            ControlToValidate="txtAddress"   ErrorMessage="Alphanumeric,space and some special symbols like '.,/-:' allowed"
                                             Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>"
                                             SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,-:_/-:]+$"></asp:RegularExpressionValidator>  --%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAddress" MaxLength="140" placeholder="Enter Address"></asp:TextBox>
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
                                    <label>Latitude</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtLatitude" ErrorMessage="Invalid Latitude" Text="<i class='fa fa-exclamation-circle' title='Invalid Latitude !'></i>" SetFocusOnError="true" ValidationExpression="^-?([1-9]?[0-9])\.{1}\d{1,12}"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtLatitude" MaxLength="30" placeholder="Enter Latitude"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Longitude</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtLongitude" ErrorMessage="Invalid Longitude" Text="<i class='fa fa-exclamation-circle' title='Invalid Longitude !'></i>" SetFocusOnError="true" ValidationExpression="^-?([1-9]?[0-9])\.{1}\d{1,12}"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtLongitude" MaxLength="30" placeholder="Enter Longitude"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PAN Number</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ForeColor="Red" ControlToValidate="txtPanCard"
                                            ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" Display="Dynamic" ErrorMessage="Invalid PAN Card" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid PAN Card !'></i>" />


                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPanCard" MaxLength="10" placeholder="Enter PAN Number"></asp:TextBox>
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
                        <legend>Bank Details</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bank Name<span style="color: red;" id="pnlBankName" runat="server" visible="false"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" Enabled="false"
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
                    <h3 class="box-title">Retailer Registration Details</h3>

                    <p style="font-size: 12px; line-height: 24px; font-weight: normal; color: black; padding: 0; margin: 0;"><b>For Inactive Route:</b> <span style="width: 15px; height: 15px; margin: auto; display: inline-block; border: 1px solid gray; vertical-align: middle; border-radius: 2px; background: #f05959"></span></p>

                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Location<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="b"
                                        InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                        ControlToValidate="ddlSearchLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlSearchLocation" OnSelectedIndexChanged="ddlSearchLocation_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Route No</label>
                                <asp:DropDownList ID="ddlSearchRoute" runat="server" CssClass="form-control select2">
                                   <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2" style="margin-top:20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="b" ID="btnSearch" Text="Search" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
                                EmptyDataText="No Record Found." PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="BoothId">
                                <Columns>

                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRouteId" Visible="false" runat="server" Text='<%# Eval("RouteId") %>' />
                                            <asp:Label ID="lblName_EH" runat="server" Text='<%#  Eval("RetailerName_Hi").ToString()=="" ?  Eval("BName") : Eval("BName") + " (" + Eval("RetailerName_Hi") + ")"  %>' />
                                            <asp:Label ID="lblBName" Visible="false" runat="server" Text='<%# Eval("BName") %>' />
                                            <asp:Label ID="lblRetailerName_Hi" Visible="false" runat="server" Text='<%# Eval("RetailerName_Hi") %>' />
                                            <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>' />
                                            <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>' />
                                            <asp:Label ID="lblBlock_ID" Visible="false" runat="server" Text='<%# Eval("Block_ID") %>' />
                                            <%--  <asp:Label ID="lblTownOrVillage" runat="server" Visible="false" Text='<%# Eval("TownOrVillage") %>' />--%>
                                            <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%# Eval("Email") %>' />
                                            <asp:Label ID="lblBAddress" Visible="false" runat="server" Text='<%# Eval("BAddress") %>' />
                                            <asp:Label ID="lblBPincode" Visible="false" runat="server" Text='<%# Eval("BPincode") %>' />
                                            <asp:Label ID="lblBank_id" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                            <%-- <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>' />--%>
                                            <asp:Label ID="lblBranchName" Visible="false" runat="server" Text='<%# Eval("BranchName") %>' />
                                            <asp:Label ID="lblIFSCCode" Visible="false" runat="server" Text='<%# Eval("IFSC_Code") %>' />
                                            <asp:Label ID="lblBankAccountNo" Visible="false" runat="server" Text='<%# Eval("BankAccountNo") %>' />
                                            <asp:Label ID="lblBContactNo" Visible="false" runat="server" Text='<%# Eval("BContactNo") %>' />
                                            <asp:Label ID="lblRetailerTypeID" Visible="false" runat="server" Text='<%# Eval("RetailerTypeID") %>' />
                                            <%-- <asp:Label ID="lblBLatitude" Visible="false" runat="server" Text='<%# Eval("B_Latitude") %>' />
                                        <asp:Label ID="lblBLongitude" Visible="false" runat="server" Text='<%# Eval("B_Longitude") %>' />--%>
                                            <asp:Label ID="lblRStatus" Text='<%# Eval("RMIsActive")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblOrganization_Type" Visible="false" runat="server" Text='<%# Eval("Organization_Type") %>' />
                                            <asp:Label ID="lblDeliveryType" Visible="false" runat="server" Text='<%# Eval("Delivery_Type") %>' />
                                            <asp:Label ID="lblAreaId" Visible="false" runat="server" Text='<%# Eval("AreaId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBCode" runat="server" Text='<%# Eval("BCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route Name/No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRName" runat="server" Text='<%# Eval("RName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRetailerTypeName" runat="server" Text='<%# Eval("RetailerTypeName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCPersonName" runat="server" Text='<%# Eval("CPersonName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCPersonMobileNo" runat="server" Text='<%# Eval("CPersonMobileNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision_Name" runat="server" Text='<%# Eval("Division_Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# Eval("District_Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Block">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBlock_Name" runat="server" Text='<%# Eval("Block_Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City / Village">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTownOrVillage" runat="server" Text='<%# Eval("TownOrVillage") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Latitude">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBLatitude" runat="server" Text='<%# Eval("B_Latitude") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Longitude">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBLongitude" runat="server" Text='<%# Eval("B_Longitude") %>' />
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
                                    <asp:TemplateField HeaderText="Citizen Services">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HFCitizenCervices" Value='<%#Eval("CitizenServices") %>' runat="server" />
                                            <asp:LinkButton ID="lnkCitizenCervices" CommandArgument='<%#Eval("BoothId") %>' CommandName="CitizenCervices" OnClientClick="return confirm('Are you sure to Update Citizen Services Status?')" CssClass='<%# Eval("CitizenServices").ToString()=="True" ? "label label-success" : Eval("CitizenServices").ToString()=="" ? "label label-warning" : "label label-danger" %>' runat="server" Text='<%# Eval("CitizenServices").ToString()=="True" ? "Active" : Eval("CitizenServices").ToString()=="" ? "To be Held" : "Not Active" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("BoothId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkCitizenCervices"  runat="server" ToolTip="Update Citizen Services Status" Style="color: red;"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("BoothId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>

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
     <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
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
            iDisplayLength: 50,
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
                    title: ('Retailer Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Retailer Details').bold().fontsize(3).toUpperCase(),
                    filename: 'RetailerRegistrationDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]
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

