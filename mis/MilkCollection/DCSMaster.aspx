<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSMaster.aspx.cs" Inherits="mis_Common_DCSMaster" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
            <div class="box box-body">
                <div class="box-header">
                    <h3 class="box-title">DCS Master</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Office Type Details
                        </legend>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Office Type:-</label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvSContainerTypeName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="RblContainerTypeName" ErrorMessage="Select Container Type" Text="<i class='fa fa-exclamation-circle' title='Select Container Type!'></i>">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <div style="margin-left: -70px;">
                                        <asp:RadioButtonList runat="server" ID="RblContainerTypeName" RepeatDirection="Horizontal" CssClass="pull-left" AutoPostBack="true" ClientIDMode="Static">
                                            <asp:ListItem Value="1">DS &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">MCU</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Milk Collection Unit<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Milk Collection Unit" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit !'></i>"
                                            ControlToValidate="ddlMilkCollectionUnit" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlMilkCollectionUnit" OnInit="ddlMilkCollectionUnit_Init" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group" id="snapName" runat="server" visible="false">
                                    <asp:Label ID="lblName" Text="Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select  Milk Collection Unit Name" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit Name !'></i>"
                                            ControlToValidate="ddlMilkColUnitName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlMilkColUnitName" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Office Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDCSName" Text="DCS Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDCSname" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Name !'></i>"
                                            ControlToValidate="txtDCSName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCSName"
                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                    </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCSName" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDCSCode" Text="DCS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCS_Code" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCS_Code"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCS_Code" MaxLength="10" placeholder="Enter DCS Code" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblSecretaryPerson" Text="Secretary Person" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Secretary Person" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Secretary Person !'></i>"
                                            ControlToValidate="txtSecretaryPerson" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSecretaryPerson"
                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                    </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSecretaryPerson" placeholder="Enter Secretary Person."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblContactPerson" Text="Contact Person" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvContactPerson" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person. !'></i>"
                                            ControlToValidate="txtContactPerson" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person. !" Text="<i class='fa fa-exclamation-circle' title='Enter Contact Person !'></i>" ControlToValidate="txtContactPerson"
                                            ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtContactPerson" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Enter Contact Person."></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlDivision" OnInit="ddlDivision_Init" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>District<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Please Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDCSContactNo" Text="DCS Contact No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDCScontactno" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Contact No. !'></i>"
                                            ControlToValidate="txtDCSContactNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Contact No. !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact No !'></i>" ControlToValidate="txtDCSContactNo"
                                            ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtDCSContactNo" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Enter Contact No."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDCSEmail" Text="DCS Email." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfDCSEmail" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Email !'></i>"
                                            ControlToValidate="txtDCS_Email" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtDCS_Email"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCS_Email" MaxLength="50" placeholder="Enter Email" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDCSAddress" Text="DCS Address" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficeaddress" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Address" Text="<i class='fa fa-exclamation-circle' title='Enter Office Address !'></i>"
                                            ControlToValidate="txtDCSAddress" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCSAddress" ErrorMessage="Alphanumeric ,space and some special symbols like .,/-: allow" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like .,/-: allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCSAddress" MaxLength="140" placeholder="Enter Address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblDCSPincode" Text="DCS Pincode" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDCSpincode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtDCSPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDCSepincode" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCSPincode" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid DCS Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCSPincode" MaxLength="6" placeholder="Enter Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>GST Number<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                        <%-- <asp:RequiredFieldValidator ID="rfv7" ValidationGroup="a"
                                    ErrorMessage="Please Enter GST Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter GST Number !'></i>"
                                    ControlToValidate="txtGstNumber" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtGstNumber"
                                            ErrorMessage="Invalid GST Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid GST Number !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control CapitalClass" ID="txtGstNumber" MaxLength="15" placeholder="Enter GST Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PAN Number&nbsp;(ex: ABCDF1234H)<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                    ErrorMessage="Please Enter PAN Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter PAN Number !'></i>"
                                    ControlToValidate="txtPanNumber" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtPanNumber"
                                            ErrorMessage="Invalid PAN Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid PAN Number !'></i>"
                                            SetFocusOnError="true" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control CapitalClass PanCard" ID="txtPanNumber" MaxLength="10" placeholder="Enter PAN Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" id="spanUnit" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Unit<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Unit" Text="<i class='fa fa-exclamation-circle' title='Select Unit !'></i>"
                                            ControlToValidate="ddlUnit" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlUnit" OnInit="ddlUnit_Init" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="spanCapacity" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Capacity<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                            ErrorMessage="Enter Capacity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Capacity !'></i>"
                                            ControlToValidate="txtCapacity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,32})?$" ValidationGroup="a" runat="server" ControlToValidate="txtCapacity" ErrorMessage="Enter Valid Number or three decimal value in Capacity." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number or three decimal value in Capacity. !'></i>"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control CapitalClass PanCard" ID="txtCapacity" MaxLength="10" placeholder="Enter Capacity" Text="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Bank Details</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bank Name<%--<span style="color: red;"> *</span>--%></label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Bank Name" Text="<i class='fa fa-exclamation-circle' title='Select Bank Name !'></i>"
                                            ControlToValidate="ddlBank" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>--%>
                                    <asp:DropDownList ID="ddlBank" OnInit="ddlBank_Init" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlBranchName" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>IFSC Code</label>
                                    <span class="pull-right">
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                             ErrorMessage="Enter IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Enter IFSC Code !'></i>"
                                            ControlToValidate="ddlBank" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                            ErrorMessage="Invalid IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Invalid IFSC Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" placeholder="XXXX0000000" automplete="off" CssClass="form-control" ID="txtIFSCCode"></asp:TextBox>
                                    <%--  <asp:AutoCompleteExtender ServiceMethod="SearchIFSC"
                                        MinimumPrefixLength="2"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtIFSCCode" CompletionListCssClass="AutoExtender"
                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                    </asp:AutoCompleteExtender>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bank Account No.<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a"
                                             ErrorMessage="Enter Account No" Text="<i class='fa fa-exclamation-circle' title='Enter Account No. !'></i>"
                                            ControlToValidate="txtBankAccountNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankAccountNo"
                                            ErrorMessage="Invalid Bank Account No." Text="<i class='fa fa-exclamation-circle' title='Invalid Bank Account No. !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBankAccountNo" MaxLength="20" placeholder="Account No" onkeypress="return validateNum(event);"></asp:TextBox>
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
                                <a class="btn btn-block btn-default" href="DCSMaster.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body">
                <div class="box-header">
                    <h3 class="box-title">DCS Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="DCSMaster_ID" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("DCSMaster_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DCS Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDCSMaster_ID" Visible="false" runat="server" Text='<%# Eval("DCSMaster_ID") %>' />
                                        <asp:Label ID="lblOfficeType_ID" Visible="false" runat="server" Text='<%# Eval("OfficeType_ID") %>' />
                                        <asp:Label ID="lblMilkCollectionUnit_ID" Visible="false" runat="server" Text='<%# Eval("MilkCollectionUnit_ID") %>' />
                                        <asp:Label ID="lblSecretary_Person" Visible="false" runat="server" Text='<%# Eval("Secretary_Person") %>' />
                                        <asp:Label ID="lblContact_Person" Visible="false" runat="server" Text='<%# Eval("Contact_Person") %>' />
                                        <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>' />
                                        <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>' />
                                        <asp:Label ID="lblDCSContact_No" Visible="false" runat="server" Text='<%# Eval("DCSContact_No") %>' />
                                        <asp:Label ID="lblDCS_Email" Visible="false" runat="server" Text='<%# Eval("DCS_Email") %>' />
                                        <asp:Label ID="lblDCS_Address" Visible="false" runat="server" Text='<%# Eval("DCS_Address") %>' />
                                        <asp:Label ID="lblDCS_Pincode" Visible="false" runat="server" Text='<%# Eval("DCS_Pincode") %>' />
                                        <asp:Label ID="lblBank_id" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                        <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>' />
                                          <asp:Label ID="lblBranchName" Visible="false" runat="server" Text='<%# Eval("BranchName") %>' />
                                          <asp:Label ID="lblIFSC" Visible="false" runat="server" Text='<%# Eval("IFSC") %>' />
                                        <asp:Label ID="lblBankAccountNo" Visible="false" runat="server" Text='<%# Eval("BankAccountNo") %>' />
                                        <asp:Label ID="lblCapacity" Visible="false" runat="server" Text='<%# Eval("Capacity") %>' />
                                        <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>' />
                                        <asp:Label ID="OfficeType_Code" runat="server" Text='<%# Eval("OfficeType_Code") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Collection Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeTypeName" runat="server" Text='<%# Eval("OfficeTypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Collection Unit Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit_Name" runat="server" Text='<%# Eval("MilkCollectionUnit_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DCS Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDCS_Name" runat="server" Text='<%# Eval("DCS_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DCS Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDCS_Code" runat="server" Text='<%# Eval("DCS_Code") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GST Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGST_Number" runat="server" Text='<%# Eval("GST_Number") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAN Card Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAN_Number" runat="server" Text='<%# Eval("PAN_Number") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("DCSMaster_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("DCSMaster_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
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

