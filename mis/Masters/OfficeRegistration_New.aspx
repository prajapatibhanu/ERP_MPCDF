<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="OfficeRegistration_New.aspx.cs" Inherits="mis_Masters_OfficeRegistration_New" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
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
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Office Registration</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Milk Collection Unit Details</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Milk Collection Unit<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Office Type" Text="<i class='fa fa-exclamation-circle' title='Select Office Type !'></i>"
                                            ControlToValidate="ddlOfficeType" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlOfficeType" AutoPostBack="true" Width="100%" OnInit="ddlOfficeType_Init" runat="server" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="spanDivision" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Divison<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                            ControlToValidate="ddlDivision" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDivision" OnInit="ddlDivision_Init" Width="100%" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="spanDistrict" runat="server" visible="false">
                                <div class="form-group">
                                    <label>District<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Please Select District" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="100%" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divBlock" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Block</label>
                                    
                                    <asp:DropDownList ID="ddlBlock_ID" runat="server" Width="100%" CssClass="form-control select2" OnSelectedIndexChanged="ddlBlock_ID_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2" id="divAssembly" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Assembly</label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Please Select Assembly" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlAssembly_ID" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>--%>
                                    <asp:DropDownList ID="ddlAssembly_ID" runat="server" Width="100%" CssClass="form-control select2" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="ReportingUnit" runat="server">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Milk Supply to</label>
                                        <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="ddlMilkSupply" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Suppy to!'></i>" ErrorMessage="Select BMC Available" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlMilkSupply" OnSelectedIndexChanged="ddlMilkSupply_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Supply Unit</label>
                                        <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic" ControlToValidate="ddlSupplyUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Supply Unit!'></i>" ErrorMessage="Select Supply Unit" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSupplyUnit" Width="100%" CssClass="form-control select2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <asp:Panel ID="pnlDataDiv" runat="server">
                        <fieldset>
                            <legend>Milk Collection Details</legend>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblOfficeName" Text="Name (In Hindi)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                                ErrorMessage="Enter Office Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Name(In Hindi) !'></i>"
                                                ControlToValidate="txtOfficeName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtOfficeName"
                                                ValidationExpression="^[^'@%#$&=^!~?]+$"
                                                Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet  Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficeName" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblOffice_Name_E" Text="Name (In English)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Enter Office Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Name(In English) !'></i>"
                                                ControlToValidate="txtOfficeName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtOffice_Name_E"
                                                ValidationExpression="^[^'@%#$&=^!~?]+$"
                                                Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet  Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Name_E" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblOfficeCode" Text="Office Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Code" MaxLength="10" placeholder="Enter Office Code" onkeypress="return alpha(event);" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" Text="Society Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label><%--<span style="color: red;"> *</span>--%>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                ErrorMessage="Enter Office Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Code !'></i>"
                                                ControlToValidate="txtSocietyCode" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtSocietyCode"
                                                ErrorMessage="Only Alphanumeric Allow in Society Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in Society Code !'></i>"
                                                SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSocietyCode" MaxLength="10" placeholder="Enter Society Code" onkeypress="return alpha(event);" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblOfficeContactNo" Text="Contact No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label>
                                        <span class="pull-right">
                                            
                                            <asp:RegularExpressionValidator ID="revofficecontactno" runat="server" Display="Dynamic" ValidationGroup="a"
                                                ErrorMessage="Enter Valid Contact No. !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact No !'></i>" ControlToValidate="txtOfficeContactNo"
                                                ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtOfficeContactNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Contact No."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblOfficeEmail" Text="Email" Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label>
                                        <span class="pull-right">
                                            <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtOffice_Email"
                                                ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Email" MaxLength="50" placeholder="Enter Email" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" Text="Secretary Name" Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label>
                                        <span class="pull-right">
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter Secretary Name" Text="<i class='fa fa-exclamation-circle' title='Enter Secretary Name !'></i>"
                                            ControlToValidate="txtOfficerName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtOfficerName" ErrorMessage="Alphanumeric ,space and some special symbols like .,/-: allow" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like .,/-: allow !'></i>" SetFocusOnError="true" ValidationExpression="^[^'@%#$&=^!~?]+$"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficerName" MaxLength="140" placeholder="Enter Officer Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Secretary Mobile No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label>
                                        <span class="pull-right">
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Officer Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Officer Mobile No. !'></i>"
                                            ControlToValidate="txtofficermobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="a"
                                                ErrorMessage="Enter Valid Mobile No. !" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No !'></i>" ControlToValidate="txtofficermobileNo"
                                                ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtofficermobileNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Mobile No."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Society Category</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="ddlSocietyCategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society Category!'></i>" ErrorMessage="Select Society Category" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>-
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSocietyCategory" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Registered Society"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Dairy Help Group"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Proposed Society"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                    </div>
                                </div>
								<div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            DCS Member</label>
                                       <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlSocietyCategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society Category!'></i>" ErrorMessage="Select Society Category" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>--%>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDCSMember" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                   
                                                </asp:DropDownList>
                                            </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                           Death Claim Category</label>
                                        
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDeathClaimCategory" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                                    <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Bank Details</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank Name<%--<span style="color: red;"> *</span>--%></label><%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Bank Name" Text="<i class='fa fa-exclamation-circle' title='Select Bank Name !'></i>"
                                            ControlToValidate="ddlBank" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>--%><asp:DropDownList ID="ddlBank" Width="100%" AutoPostBack="true" onkeypress="return alpha(event);" runat="server" OnInit="ddlBank_Init" CssClass="form-control select2" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Branch Name<%--<span style="color: red;"> *</span>--%></label><%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Branch Name" Text="<i class='fa fa-exclamation-circle' title='Select Branch Name !'></i>"
                                            ControlToValidate="ddlBranchName" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>--%><asp:TextBox runat="server" automplete="off" CssClass="form-control" onkeypress="return alpha(event);" ID="txtBranchName" placeholder="Enter Branch Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>IFSC Code</label>
                                        <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                             ErrorMessage="Enter IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Enter IFSC Code !'></i>"
                                            ControlToValidate="ddlBank" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                            ErrorMessage="Invalid IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Invalid IFSC Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>--%>
                                        <asp:TextBox runat="server" automplete="off" CssClass="form-control" onkeypress="return alpha(event);" ID="txtIFSCCode" placeholder="Enter IFSC Code"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank Account No.<%--<span style="color: red;"> *</span>--%></label><%-- <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankAccountNo"
                                            ErrorMessage="Invalid Bank Account No." Text="<i class='fa fa-exclamation-circle' title='Invalid Bank Account No. !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>--%><asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBankAccountNo" MaxLength="20" placeholder="Enter Account No" onkeypress="return validateNum(event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Account Holder Name<%--<span style="color: red;"> *</span>--%></label><%-- <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankAccountNo"
                                            ErrorMessage="Invalid Bank Account No." Text="<i class='fa fa-exclamation-circle' title='Invalid Bank Account No. !'></i>"
                                            SetFocusOnError="true" ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>--%><asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAccountHolderName" MaxLength="100" placeholder="Enter Account Holder Name"></asp:TextBox>
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
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Office Registration Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Milk Collection Unit<span style="color: red;"> *</span></label>

                                <asp:DropDownList ID="ddlOfficeType_flt" OnInit="ddlOfficeType_flt_Init" Width="100%" OnSelectedIndexChanged="ddlOfficeType_flt_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="divcc" runat="server" visible="false">
                            <div class="form-group">
                                <label>Chilling Center<span style="color: red;"> *</span></label>

                                <asp:DropDownList ID="ddlChillingCenter"  Width="100%" OnSelectedIndexChanged="ddlChillingCenter_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                   

                     



                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"  DataKeyNames="Office_ID" >
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Milk Collection Type" DataField="OfficeTypeName" />
                                <asp:BoundField HeaderText="Name" DataField="Office_Name" />
                                <asp:BoundField HeaderText="Name In English" DataField="Office_Name_E" />
                                <asp:BoundField HeaderText="Office Code" DataField="Office_Code" />
								<asp:BoundField HeaderText="Society Code" DataField="SocietyCode" />
                                <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                <asp:BoundField HeaderText="Milk Supply to" DataField="MilkSupplyto" />
                                <asp:BoundField HeaderText="Supply Unit" DataField="SupplyUnit" />
                                <%--<asp:BoundField HeaderText="Total Producer" DataField="P_count" />--%>
                                <asp:BoundField HeaderText="Secretary Name" DataField="OfficerName" />
                                <asp:BoundField HeaderText="Secretary Mobile No" DataField="OfficerMobileNo" />
                                <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                                <asp:BoundField HeaderText="Bank AccountNo" DataField="BankAccountNo" />
                                <asp:BoundField HeaderText="IFSC" DataField="IFSC" />
								<asp:BoundField HeaderText="Account Holder Name" DataField="AccountHolderName" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("Office_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11,12,13,14]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: 'Office Registration Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11,12,13,14]
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

