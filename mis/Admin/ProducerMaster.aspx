<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProducerMaster.aspx.cs" Inherits="mis_Common_ProducerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
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
                    <h3 class="box-title">Producer Registration</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Office Details</legend>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Dugdh Sangh <span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rq1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDS" InitialValue="0" ErrorMessage="Select Dugdh Sangh." Text="<i class='fa fa-exclamation-circle' title='Please Dugdh Sangh !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDS" runat="server" OnInit="ddlDS_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    <asp:Label ID="lblOfficeTypeName" runat="server" Text="DCS"></asp:Label>
                                    <span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDCS" InitialValue="0" ErrorMessage="Select Dairy Corporative Society." Text="<i class='fa fa-exclamation-circle' title='Select Dairy Corporative Society !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDCS" runat="server" OnInit="ddlDCS_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Producer Details</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Producer Name <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtProducerName" ErrorMessage="Enter Producer Name." Text="<i class='fa fa-exclamation-circle' title='Enter Producer Name !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtProducerName"
                                            ValidationExpression="^[a-zA-z\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtProducerName" placeholder="Enter Producer Name" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Mobile No. <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtMoibleNo" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtMoibleNo"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>
                                    <asp:TextBox ID="txtMoibleNo" AutoPostBack="true" OnTextChanged="txtMoibleNo_TextChanged" placeholder="Enter Mobile No" onkeypress="return validateNum(event);" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Email<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtProducerName" ErrorMessage="Enter Producer Name." Text="<i class='fa fa-exclamation-circle' title='Enter Producer Name !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                    </span>
                                    <asp:TextBox ID="txtEmail" placeholder="Enter Producer Name" MaxLength="80" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Gender <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="Select Gender." Text="<i class='fa fa-exclamation-circle' title='Select Gender !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlGender" OnInit="ddlGender_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Category <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Select Category." Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlCategory" OnInit="ddlCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ration Card No. (Optional)</label>
                                    <span class="pull-right">
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtRationCardNo" ErrorMessage="Enter Ration Card No." Text="<i class='fa fa-exclamation-circle' title='Enter Ration Card No !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" runat="server" ControlToValidate="txtRationCardNo"
                                            ValidationExpression="^[0-9a-zA-z]+$" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric allow !'></i>" ErrorMessage="Only Alphanumeric allow" />
                                    </span>
                                    <asp:TextBox ID="txtRationCardNo" AutoPostBack="true" OnTextChanged="txtRationCardNo_TextChanged" placeholder="Enter Ration Card No." MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Aadhaar No. (Optional)</label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAadhaarNo" ErrorMessage="Enter Aadhaar No." Text="<i class='fa fa-exclamation-circle' title='Enter Aadhaar No!'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" ValidationGroup="a" runat="server" ControlToValidate="txtAadhaarNo"
                                            ValidationExpression="^([0-9]{12})$" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='InValid Aadhaar No !'></i>" ErrorMessage="InValid Aadhaar No" />
                                    </span>
                                    <asp:TextBox ID="txtAadhaarNo" AutoPostBack="true" OnTextChanged="txtAadhaarNo_TextChanged" placeholder="XXXXXXXXXXXX" MaxLength="12" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>DOB (Optional)</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="revdate" ForeColor="Red" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDOB" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOB" onkeypress="javascript: return false" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-end-date="-5110d" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Milk Card <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMilkCard" InitialValue="0" ErrorMessage="Select Milk Card." Text="<i class='fa fa-exclamation-circle' title='Select Milk Card !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlMilkCard" Width="100%" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDivision_Name" InitialValue="0" ErrorMessage="Select Division." Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <label>Division Name<span style="color: red;"> *</span></label>
                                    <asp:DropDownList runat="server" ID="ddlDivision_Name" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnInit="ddlDivision_Name_Init" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>District <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="Select District." Text="<i class='fa fa-exclamation-circle' title='Select District !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Address <span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAddress" ErrorMessage="Enter Address." Text="<i class='fa fa-exclamation-circle' title='Enter Address. !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ForeColor="Red" ValidationGroup="a" runat="server" ControlToValidate="txtAddress"
                                        ValidationExpression="^[a-zA-z0-9\s-,.]+$" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphanuermic ,space & special symbols like '(),.-' allow in Address !'></i>" ErrorMessage="'Only Alphanuermic ,space & special symbols like '(),.-' allow in Address." />
                                </span>
                                <asp:TextBox ID="txtAddress" placeholder="Enter Full Address" MaxLength="150" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>Pin Code (Optional)</label>
                                <span class="pull-right">
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtPinCode" ErrorMessage="Enter Village." Text="<i class='fa fa-exclamation-circle' title='Enter Village !'></i>"></asp:RequiredFieldValidator>
                                    --%><asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" ValidationGroup="a" runat="server" ControlToValidate="txtPinCode"
                                        ValidationExpression="\d{6}-?(\d{4})?$" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Number allow Max 6 digit!'></i>" ErrorMessage="'Only Number allow Max 6 digit" />
                                </span>
                                <asp:TextBox ID="txtPinCode" MaxLength="6" placeholder="Enter Pin Code" CssClass="form-control" runat="server" onkeypress="return validateNum(event);"></asp:TextBox>
                            </div>

                        </div>

                    </fieldset>
                    <div class="clearfix"></div>
                    <fieldset>
                        <legend>Bank Details <span style="color: green;">(Optional)</span></legend>
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
                                    <asp:DropDownList ID="ddlBranchName" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
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
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                            ErrorMessage="Invalid IFSC Code Use format like [XXXX0000000]!" Text="<i class='fa fa-exclamation-circle' title='Invalid IFSC Code Use format like [XXXX0000000]!'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[A-Za-z]{4}\d{7}$">
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
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankAccountNo"
                                            ErrorMessage="Invalid Bank Account No. should have 9-18 digits" Text="<i class='fa fa-exclamation-circle' title='Invalid Bank Account No. should have 9-18 digits !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{9,18}">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBankAccountNo" MaxLength="18" placeholder="Account No" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <div class="row" style="padding-top: 20px;">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button Text="Save" ID="btnSubmit" CssClass="btn btn-block btn-primary" OnClientClick="return ValidatePage();" ValidationGroup="a" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-default" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Producer Details</h3>
                </div>
                <div class="box-body">
                    <%--<div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office</label>
                                <asp:DropDownList ID="ddlOffice" Enabled="false" runat="server" CssClass="form-control select2" OnInit="ddlOffice_Init"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Division Name<span class="hindi">(संभाग नाम)</span></label>
                            <asp:DropDownList ID="ddlSearchDivision" runat="server" CssClass="form-control select2" OnInit="ddlSearchDivision_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchDivision_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label>District Name<span class="hindi">(जिला नाम)</span></label>
                            <asp:DropDownList ID="ddlSearchDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchDistrict_SelectedIndexChanged" CssClass="form-control select2" OnInit="ddlSearchDistrict_Init"></asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." DataKeyNames="ProducerId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Member Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerId" runat="server" Visible="false" Text='<%# Eval("ProducerId") %>'></asp:Label>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                <asp:Label ID="lblAddress" Visible="false" runat="server" Text='<%# Eval("PAddress") %>'></asp:Label>
                                                <asp:Label ID="lblPincode" Visible="false" runat="server" Text='<%# Eval("PPincode") %>'></asp:Label>
                                                <asp:Label ID="lblBank_AccountNo" Visible="false" runat="server" Text='<%# Eval("Bank_AccountNo") %>'></asp:Label>
                                                <asp:Label ID="lblBank_id" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>'></asp:Label>
                                                <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>'></asp:Label>
                                                <asp:Label ID="lblIFSC" Visible="false" runat="server" Text='<%# Eval("IFSC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ration CardNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRationCardNo" runat="server" Text='<%# Eval("RationCardNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AadharNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAadharNo" runat="server" Text='<%# Eval("AadharNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                <asp:Label ID="lblGend_id" Visible="false" runat="server" Text='<%# Eval("Gend_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCasteCategory" runat="server" Text='<%# Eval("CasteCategory") %>'></asp:Label>
                                                <asp:Label ID="lblCasteCat_id" Visible="false" runat="server" Text='<%# Eval("CasteCat_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Milk Card">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkCardStatus" Visible="false" runat="server" Text='<%# Eval("MilkCardStatus") %>'></asp:Label>
                                                <asp:Label ID="lblMCStatus" runat="server" Text='<%# Eval("MCStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("PAddress") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblPincode" runat="server" Text='<%# Eval("PPincode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Division">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivision_Name" runat="server" Text='<%# Eval("Division_Name") %>'></asp:Label>
                                                <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProducerId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ProducerId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
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
        //function tMobileValue(txt) {
        //    varat = txt.value;
        //    $.ajax({
        //        type: "POST",
        //        url: "../../WebServiceCheck.asmx/CheckMobileNo",
        //        data: { userm: varat },
        //        success: function (response) {


        //            if (response == 1) {
        //                alert('Mobile No : ' + varat + ' has already been taken');
        //                txt.value = '';
        //                txt.focus();
        //                return false;
        //            }
        //            {

        //            }
        //        }
        //    });
        //}
        //function tAadharValue(txt) {
        //    aarat = txt.value;
        //    $.ajax({
        //        type: "POST",
        //        url: "../../WebServiceCheck.asmx/CheckAadharNo",
        //        data: { userra: aarat },
        //        success: function (response) {
        //            if (response.d == 1) {
        //                alert('Aadhar No : ' + aarat + ' has already been taken');
        //                txt.value = '';
        //                txt.focus();
        //                return false;
        //            }
        //        }
        //    });
        //}
        //function tRationValue(txt) {
        //    rcno = txt.value;
        //    $.ajax({
        //        type: "POST",
        //        url: "../../WebServiceCheck.asmx/CheckRationCardNo",
        //        data: { usera: rcno },
        //        success: function (response) {
        //            if (response.d = 1) {
        //                alert('Ration CardNo : ' + rcno + ' has already been taken');
        //                txt.value = '';
        //                txt.focus();
        //                return false;
        //            }
        //        }
        //    });
        //}
    </script>

</asp:Content>

