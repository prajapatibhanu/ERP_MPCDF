<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MCURegistration.aspx.cs" Inherits="mis_Common_MCURegistration" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" OnClick="btnSubmit_Click" />
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
                    <h3 class="box-title">MCU Master</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Milk Collection Unit Type Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Milk Collection Unit Type<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Office Type" Text="<i class='fa fa-exclamation-circle' title='Select Office Type !'></i>"
                                            ControlToValidate="ddlOfficeType" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlOfficeType" AutoPostBack="true" OnInit="ddlOfficeType_Init" runat="server" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" id="spanDivision" runat="server" visible="false">
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
                            <div class="col-md-3" id="spanDistrict" runat="server" visible="false">
                                <div class="form-group">
                                    <label>District<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Please Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Milk Collection Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblOfficeName" Text="Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                            ErrorMessage="Enter Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Name !'></i>"
                                            ControlToValidate="txtOfficeName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficeName" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblOfficeCode" Text="Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvrfvofficcode" ValidationGroup="a"
                                            ErrorMessage="Enter Office Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Code !'></i>"
                                            ControlToValidate="txtOffice_Code" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficecode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtOffice_Code"
                                            ErrorMessage="Only Alphanumeric Allow in Office Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in Office Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Code" MaxLength="10" placeholder="Enter Code" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblOfficeContactNo" Text="Contact No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficecontactno" ValidationGroup="a"
                                            ErrorMessage="Enter Office Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Contact No. !'></i>"
                                            ControlToValidate="txtOfficeContactNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficecontactno" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Contact No. !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact No !'></i>" ControlToValidate="txtOfficeContactNo"
                                            ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtOfficeContactNo" MaxLength="11" onkeypress="return validateNum(event);" placeholder="Enter Contact No."></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblOfficeEmail" Text="Email." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvOfficeEmail" ValidationGroup="a"
                                            ErrorMessage="Enter Office Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Email !'></i>"
                                            ControlToValidate="txtOffice_Email" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtOffice_Email"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Email" MaxLength="50" placeholder="Enter Email" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <%--<div class="col-md-3 hidden">
                            <div class="form-group">
                                <label>Block Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlBlock_Name" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblOfficeAddress" Text="Address" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficeaddress" ValidationGroup="a"
                                            ErrorMessage="Enter Office Address" Text="<i class='fa fa-exclamation-circle' title='Enter Office Address !'></i>"
                                            ControlToValidate="txtOfficeAddress" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtOfficeAddress" ErrorMessage="Alphanumeric ,space and some special symbols like .,/-: allow" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like .,/-: allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficeAddress" MaxLength="140" placeholder="Enter Address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblOfficePincode" Text="Pincode" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficepincode" ValidationGroup="a"
                                            ErrorMessage="Enter Office Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtOfficePincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficepincode" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtOfficePincode" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficePincode" MaxLength="6" placeholder="Enter Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlUnit" OnInit="ddlUnit_Init1" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
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
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control CapitalClass PanCard" ID="txtCapacity" MaxLength="10" placeholder="Enter Capacity"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlBank" OnInit="ddlBank_Init1" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged1" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlBranchName" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
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
                                    <asp:TextBox runat="server" automplete="off" CssClass="form-control" ID="txtIFSCCode"></asp:TextBox>
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
                                     <a class="btn btn-block btn-default" href="MCURegistration.aspx">Clear</a>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <!-- /.box-body -->

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Milk Collection Unit Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Office_ID">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Collection Type ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>' />
                                        <asp:Label ID="lblOfficeType_ID" Visible="false" runat="server" Text='<%# Eval("OfficeType_ID") %>' />
                                        <asp:Label ID="lblOffice_ContactNo" Visible="false" runat="server" Text='<%# Eval("Office_ContactNo") %>' />
                                        <asp:Label ID="lblOffice_Email" Visible="false" runat="server" Text='<%# Eval("Office_Email") %>' />
                                        <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>' />
                                        <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>' />
                                        <asp:Label ID="lblOffice_Address" Visible="false" runat="server" Text='<%# Eval("Office_Address") %>' />
                                        <asp:Label ID="lblOffice_Pincode" Visible="false" runat="server" Text='<%# Eval("Office_Pincode") %>' />
                                        <asp:Label ID="lblBank_id" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                        <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>' />
                                        <asp:Label ID="lblIFSC" Visible="false" runat="server" Text='<%# Eval("IFSC") %>' />
                                        <asp:Label ID="lblBankAccountNo" Visible="false" runat="server" Text='<%# Eval("BankAccountNo") %>' />
                                        <asp:Label ID="lblCapacity" Visible="false" runat="server" Text='<%# Eval("Capacity") %>' />
                                        <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>' />
                                        <asp:Label ID="lblOfficeTypeName" runat="server" Text='<%# Eval("OfficeTypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("Office_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GST">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeGst" runat="server" Text='<%# Eval("Office_Gst") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficePan" runat="server" Text='<%# Eval("Office_Pan") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("Office_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("Office_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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

