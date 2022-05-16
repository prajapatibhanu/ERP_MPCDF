<%@ Page Title="" Language="C#" Culture="en-IN" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="OfficeRegistration.aspx.cs" Inherits="mis_Common_OfficeRegistration" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<style>
        .AutoExtender {
            font-size: 12px;
            color: #000;
            padding: 3px 5px;
            border: 1px solid #999;
            background: #fff;
            width: auto;
            float: left;
            z-index: 9999999999;
            position: absolute;
            margin-left: 0px;
            list-style: none;
            font-weight: bold;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>
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
                    <h3 class="box-title">Office Registration Master</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Milk Collection Unit Details
                        </legend>
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
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Please Select Block" Text="<i class='fa fa-exclamation-circle' title='Select !'></i>"
                                            ControlToValidate="ddlBlock_ID" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>--%>
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
                                            <asp:DropDownList ID="ddlMilkSupply" OnSelectedIndexChanged="ddlMilkSupply_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server">
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
                                            <asp:DropDownList ID="ddlSupplyUnit" Width="100%" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <asp:Panel ID="pnlDataDiv" runat="server">
                        <fieldset>
                            <legend>Milk Collection Details
                            </legend>
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
                                        <asp:Label ID="lblOfficeContactNo" Text="Contact No." Style="display: inline-block; max-width: 100%; margin-bottom: 5px;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficecontactno" ValidationGroup="a"
                                            ErrorMessage="Enter Office Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Contact No. !'></i>"
                                            ControlToValidate="txtOfficeContactNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
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
                            </div>
                            <div class="row">
                                <div id="DivAHW" runat="server">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                AI Center</label>
                                            <%--  <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="DdlAICenter" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Suppy to!'></i>" ErrorMessage="Select AI Center" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                            <div class="form-group">
                                                <asp:DropDownList ID="DdlAICenter" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>AHW Name</label>
                                            <span class="pull-right">
                                                <%--<asp:RequiredFieldValidator ID="rfvFather" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAhwName" ErrorMessage="Enter Ahw Name." Text="<i class='fa fa-exclamation-circle' title='Enter Ahw Name !'></i>"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ID="REFatherHusbandName" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAhwName"
                                                    ValidationExpression="^[^'@%#$&=^!~?]+$"
                                                    Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                            </span>
                                            <asp:TextBox ID="txtAhwName" placeholder="Enter AHW Name" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>AHW Mobile</label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtAHWMobile" MaxLength="10" placeholder="Enter AHW Mobile" onkeypress="return validateNum(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblAssetDetail" runat="server">Asset Details</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtAssetDetail" MaxLength="100" placeholder="Enter Asset Detail" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="DivDCS" runat="server">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Electronic Equipment</label>
                                                <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtElectronicEquip" ErrorMessage="Enter Equipment Name." Text="<i class='fa fa-exclamation-circle' title='Enter Equipment Name !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtElectronicEquip"
                                            ValidationExpression="^[a-zA-z,\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>--%>
                                                <asp:TextBox ID="txtElectronicEquip" placeholder="Enter Electronic Equipment" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblRegistered" runat="server">Producers (Count)</asp:Label>
                                                <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="ddlMilkSupply" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Suppy to!'></i>" ErrorMessage="Select BMC Available" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtRegisteredProducers" CssClass="form-control" runat="server" onkeypress="return validateNum(event);" placeholder="Enter Producers (Count)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="DivDCSandBMC" runat="server">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Registration NO</label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtRegistrationNo" ErrorMessage="Enter Family Members No." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtRegistrationNo"
                                            ValidationExpression="^[1-9][0-9]*|0$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Required!'></i>" ErrorMessage="Enter Valid No." />
                                    </span>--%>
                                            <asp:TextBox ID="txtRegistrationNo" placeholder="Enter Registration" onkeypress="return alpha(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Registration Date</label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtRegistrationDate" Text="<i class='fa fa-exclamation-circle' title='Enter Registration Date!'></i>" ErrorMessage="Enter Registration Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                            </div>
                                            <asp:TextBox ID="txtRegistrationDate" autocomplete="off" CssClass="form-control DateAdd" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2" id="spanCapacity" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblCapacity" runat="server">Capacity</asp:Label>
                                        <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" ValidationGroup="a"
                                            ErrorMessage="Enter Capacity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Capacity !'></i>"
                                            ControlToValidate="txtCapacity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,32})?$" ValidationGroup="a" runat="server" ControlToValidate="txtCapacity" ErrorMessage="Enter Valid Number or three decimal value in Capacity." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number or three decimal value in Capacity. !'></i>"></asp:RegularExpressionValidator>
                                    </span>--%>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control CapitalClass PanCard" ID="txtCapacity" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Capacity"></asp:TextBox>
                                    </div>
                                </div>
                                <div id="SupplyUnit" runat="server">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Whether Cashless</label>
                                            <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="ddlcashless" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Suppy to!'></i>" ErrorMessage="Select Whether Cashless" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlcashless" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
									<div id="divSocietyCategory" runat="server" visible="false">
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

                                </div>
                            </div>
                            <div id="DivMDPOrCC" runat="server">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblAddress" runat="server">Address</asp:Label>
                                        <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                        <asp:TextBox ID="txtAddress" MaxLength="100" placeholder="Enter Address" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblContactPerson" runat="server">Contact Person</asp:Label>
                                        <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                        <asp:TextBox ID="txtContactPerson" MaxLength="100" placeholder="Enter Contact Person" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div id="noOfEmp" runat="server">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblNoOfEmployees" runat="server">No. of Employees</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtNoOfEmployees" MaxLength="100" placeholder="Enter No. of Employees" CssClass="form-control" onkeypress="return validateNum(event);" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblParmanent_Concontractual" runat="server">Parmanent employee</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtParmanent_Concontractual" MaxLength="100" placeholder="Enter Parmanent employee" onkeypress="return validateNum(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblSanctionedMPEB" runat="server">Sanctioned MPEB Load</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtSanctionedMPEB" MaxLength="10" placeholder="Enter Sanctioned MPEB Load" onkeypress="return validateNum(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblActualMPEB" runat="server">Actual MPEB Load</asp:Label>
                                                <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                                <asp:TextBox ID="txtActualMPEB" MaxLength="10" placeholder="Enter Actual MPEB Load" onkeypress="return validateNum(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="MDPFields" runat="server">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblProcessing" runat="server">Processing Capacity</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtProcessingCapacity" MaxLength="10" placeholder="Enter Processing Capacity" CssClass="form-control" onkeypress="return validateNum(event);" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblManufacturing" runat="server">Is Manufacturing Unit</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:DropDownList ID="ddlManufacturing" Width="100%" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblProductionName" runat="server">Production Name</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtProductionName" MaxLength="25" placeholder="Enter Production Name" CssClass="form-control" onkeypress="return alpha(event);" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblProductionCapacity" runat="server">Production Capacity</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtProductionCapacity" MaxLength="10" placeholder="Enter Production Capacity" onkeypress="return validateNum(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server">FSSI Licence No</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtFSSI" MaxLength="18" placeholder="Enter FSSI Licence No" onkeypress="return alpha(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblFactoryLicence" runat="server">Factory Licence No</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtFactoryLicence" MaxLength="18" placeholder="Enter Factory Licence No" onkeypress="return alpha(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblBoilerLicence" runat="server">Boiler Licence No</asp:Label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAHWMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAHWMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>--%>
                                            <asp:TextBox ID="txtBoilerLicence" MaxLength="18" placeholder="Enter Boiler Licence No" onkeypress="return alpha(event);" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="DCSFileds" runat="server">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Scheme Name</label>
                                            <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvtxtScheme" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtScheme" ErrorMessage="Enter Scheme Name." Text="<i class='fa fa-exclamation-circle' title='Enter Scheme Name !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REtxtScheme" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtScheme"
                                            ValidationExpression="^[a-zA-z\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>--%>
                                            <asp:TextBox ID="txtScheme" placeholder="Enter Scheme" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Women DCS</label>
                                            <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="ddlWomenDCS" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select WomenDCS!'></i>" ErrorMessage="Select WomenDCS" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>--%>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlWomenDCS" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnldcs" runat="server">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BMC Available</label>
                                            <%--  <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="ddlBMCAvail" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select BMC Available!'></i>" ErrorMessage="Select BMC Available" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>--%>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlBMCAvail" Width="100%" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="Available" Text="Available"></asp:ListItem>
                                                    <asp:ListItem Value="Not Available" Text="Not Available"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-md-2 hidden">
                                    <label>Accounting</label>
                                    <asp:RadioButtonList ID="rbAccounting" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                    </asp:RadioButtonList>
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
                                    </span>--%><asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtAccountHolderName" MaxLength="20" placeholder="Enter Account Holder Name"></asp:TextBox>
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
                    </asp:Panel>
                </div>
            </div>
            <!-- /.box-body -->

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Milk Collection Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                                <div class="form-group">
                                    <label>Milk Collection Unit<span style="color: red;"> *</span></label>
                                 
                                    <asp:DropDownList ID="ddlOfficeType_flt" OnInit="ddlOfficeType_flt_Init" Width="100%"  OnSelectedIndexChanged="ddlOfficeType_flt_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Office_ID">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Milk Collection Type" DataField="OfficeTypeName" />
                                <asp:BoundField HeaderText="Name" DataField="Office_Name" />
                                <asp:BoundField HeaderText="Name In English" DataField="Office_Name_E" />
                                <asp:BoundField HeaderText="Code" DataField="Office_Code" />
                                <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                <asp:BoundField HeaderText="Milk Supply to" DataField="MilkSupplyto" />
                                <asp:BoundField HeaderText="Supply Unit" DataField="SupplyUnit" />
                                 <asp:BoundField HeaderText="Total Producer" DataField="P_count" />
                               <%-- <asp:TemplateField HeaderText="Milk Collection Type">
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
                                        <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch") %>' />
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
								
								<asp:TemplateField HeaderText="Name In English">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Name_E" runat="server" Text='<%# Eval("Office_Name_E") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
								
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
								 <asp:TemplateField HeaderText="Milk Supply to">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficePan" runat="server" Text='<%# Eval("MilkSupplyto") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supply Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOfficeGst" runat="server" Text='<%# Eval("SupplyUnit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
								
								<asp:TemplateField HeaderText="Total Producer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblP_count" runat="server" Text='<%# Eval("P_count") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
								--%>
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

        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    
    <script src="../../js/jquery-1.10.2.js"></script>
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
   <%-- <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 25,
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
                        columns: [0, 1, 2, 3, 4, 5, 6,7,8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6,7,8]
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

