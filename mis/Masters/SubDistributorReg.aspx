<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SubDistributorReg.aspx.cs" Inherits="mis_Common_SubDistributorReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                    <h3 class="box-title">Sub Distributor Registration</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Sub Distributor Details
                        </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Distributor/SuperStockist<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Distributor/SuperStockist" Text="<i class='fa fa-exclamation-circle' title='Select Distributor/SuperStockist !'></i>"
                                            ControlToValidate="ddlDistributor" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        
                                    </span>
                                    <asp:DropDownList ID="ddlDistributor" OnInit="ddlDistributor_Init" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                     <label>Sub Distributor Name<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficename" ValidationGroup="a"
                                            ErrorMessage="Enter Sub Distributor Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sub Distributor Name !'></i>"
                                            ControlToValidate="txtSubDistributorName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtSubDistributorName"

                                             ErrorMessage="Enter Sub Distributor Name" 
                                             Text="<i class='fa fa-exclamation-circle' title='Invalid Sub Distributor Name'></i>" 
                                             SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.]+$">

                                         </asp:RegularExpressionValidator>--%>
                                       
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSubDistributorName" MaxLength="50" placeholder="Enter Sub Distributor Name" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Sub-Distributor Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Sub-Distributor Type !" 
                                            Text="<i class='fa fa-exclamation-circle' title='Select Sub-Distributor Type !'></i>"
                                            ControlToValidate="ddlSubDistributorType" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlSubDistributorType" runat="server" OnInit="ddlSubDistributorType_Init"  CssClass="form-control select2"  ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>


                            <div class="col-md-3">
                                   <div class="form-group">
                                <label>Sub Distributor Code<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic"
                                         ValidationGroup="a" runat="server" ControlToValidate="txtSDCode" 
                                        ErrorMessage="Enter Sub Distributor Code" 
                                        Text="<i class='fa fa-exclamation-circle' title='Enter Sub Distributor Code !'></i>">

                                    </asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Sub Distributor Code !" ForeColor="Red" 
                                         Text="<i class='fa fa-exclamation-circle' title='Enter Valid Sub Distributor Code !'></i>"
                                          ControlToValidate="txtSDCode"
                                            ValidationExpression="^[a-zA-Z0-9]+$">
                                        </asp:RegularExpressionValidator>


                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSDCode" MaxLength="10" placeholder="Enter Sub Distributor Code" ClientIDMode="Static"></asp:TextBox>
                            </div>
                                </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Sub Distributor Landline No.<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                            ErrorMessage="Enter Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact No. !'></i>"
                                            ControlToValidate="txtDistributorContactNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Sub Distributor Landline No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Sub Distributor Landline No. !'></i>" ControlToValidate="txtSDContactNo"
                                            ValidationExpression="^[0-9]{11}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSDContactNo" MaxLength="11" onkeypress="return validateNum(event);" placeholder="example- 07552732558"></asp:TextBox>
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
                                      <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtContactPerson"

                                             ErrorMessage="Invalid Distributor/Super Stockist Name" Text="<i class='fa fa-exclamation-circle' title='Enter Distributor/Super Stockist Name'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>

                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtContactPerson" MaxLength="60" placeholder="Enter Person"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                      <label>Contact Person Moible No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contact No. !'></i>"
                                            ControlToValidate="txtContactPersonMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Invalid Contact Person Mobile No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact Person Mobile No. !'></i>" ControlToValidate="txtContactPersonMobileNo"
                                            ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtContactPersonMobileNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Mobile No."></asp:TextBox>
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
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select District" 
                                            Text="<i class='fa fa-exclamation-circle' title='Select District !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" 
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
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Block" 
                                            Text="<i class='fa fa-exclamation-circle' title='Select Block !'></i>"
                                            ControlToValidate="ddlBlock_Name" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlBlock_Name" runat="server" AutoPostBack="true"
                                    ClientIDMode="Static" CssClass="form-control select2">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Town/Village<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                            ErrorMessage="Enter Town/village" Text="<i class='fa fa-exclamation-circle' title='Enter Town/village !'></i>"
                                            ControlToValidate="txtTownOrvillage" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red" ValidationGroup="a" 
                                            Display="Dynamic" runat="server" ControlToValidate="txtAddress"
                                             ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow" 
                                            Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>
                                  --%>
                                          </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTownOrvillage" MaxLength="80" placeholder="Enter Town Or Village"></asp:TextBox>
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
                                        <%--<asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtAddress" 
                                              ErrorMessage="Alphanumeric,space and some special symbols like '.,/-:' allowed"
                                             Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>"
                                             SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>

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
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>
                            Commission Rate & Limit
                        </legend>
                        <div class="row">
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Approved Rate<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red"
                                             ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtApprovedRate"
                                             ErrorMessage="Invalid Approved Rate(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Approved Rate(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtApprovedRate" MaxLength="10" onkeypress="return validateDec(this,event)" placeholder="Enter Approved Rate"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Trans. Rate<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtTransRate" ErrorMessage="Invalid Trans. Rate(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Trans. Rate(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" 
                                        ID="txtTransRate" MaxLength="10" onkeypress="return validateDec(this,event)" placeholder="Trans. Rate"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
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
                                    <label>Limit<%--<span style="color: red;"> *</span>--%></label>
                                    <span class="pull-right">
                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ForeColor="Red" 
                                          ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtLimit"
                                           ErrorMessage="Invalid Limit(Valid Decimal number with maximum 2 decimal places)" Text="<i class='fa fa-exclamation-circle' title='Invalid Limit(Valid Decimal number with maximum 2 decimal places) !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtLimit" MaxLength="10" onkeypress="return validateDec(this,event)" placeholder="Enter Limit"></asp:TextBox>
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
                                    <span class="pull-right">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtBranchName"

                                             ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allowed" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>
                                    
                                            </span>


                                     <asp:TextBox runat="server" automplete="off" CssClass="form-control" ID="txtBranchName" MaxLength="50"></asp:TextBox>
                                   <%--  <asp:DropDownList ID="ddlBranchName" AutoPostBack="true" runat="server" OnInit="ddlBranchName_Init" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                              --%>
                                      </div>
                            </div>
                            <div class="col-md-3"  id="pnlifsc" runat="server" >
                                <div class="form-group">
                                    <label>IFSC Code <span style="color: red;" id="pnlNewIFSC" runat="server" visible="false"> *</span></label>
                                    <span class="pull-right" >
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a" Enabled="false"
                                             ErrorMessage="Enter IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Enter IFSC Code !'></i>"
                                            ControlToValidate="txtIFSCCode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                            ErrorMessage="Invalid IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Invalid IFSC Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]{11}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" automplete="off" CssClass="form-control" ID="txtIFSCCode" MaxLength="11" ></asp:TextBox>
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
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtBankAccountNo"   MaxLength="18" placeholder="Account No. can be of max 18 digits" onkeypress="return validateNum(event);"></asp:TextBox>
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
                    
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Sub Distributor Registration Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" EmptyDataText="No Record Found." DataKeyNames="SubDistributorId">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Distributor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDName" runat="server" Text='<%# Eval("DName") %>' />
                                        <asp:Label ID="lblDistributorId" Visible="false" runat="server" Text='<%# Eval("DistributorId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Distributor Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDivision_ID" Visible="false" runat="server" Text='<%# Eval("Division_ID") %>' />
                                        <asp:Label ID="lblDistrict_ID" Visible="false" runat="server" Text='<%# Eval("District_ID") %>' />
                                        <asp:Label ID="lblBlock_ID" Visible="false" runat="server" Text='<%# Eval("Block_ID") %>' />
                                        <asp:Label ID="lblTownOrVillage" Visible="false" runat="server" Text='<%# Eval("TownOrVillage") %>' />
                                        <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%# Eval("Email") %>' />
                                        <asp:Label ID="lblDAddress" Visible="false" runat="server" Text='<%# Eval("SDAddress") %>' />
                                        <asp:Label ID="lblDPincode" Visible="false" runat="server" Text='<%# Eval("SDPincode") %>' />
                                        <asp:Label ID="lblBank_id" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                       <%-- <asp:Label ID="lblBranch_id" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>' />--%>
                                           <asp:Label ID="lblBranchName" Visible="false" runat="server" Text='<%# Eval("BranchName") %>' />
                                         <asp:Label ID="lblIFSCCode" Visible="false" runat="server" Text='<%# Eval("IFSC_Code") %>' />
                                      <%--  <asp:Label ID="lblIFSC" Visible="false" runat="server" Text='<%# Eval("IFSC") %>' />--%>
                                        <asp:Label ID="lblBankAccountNo" Visible="false" runat="server" Text='<%# Eval("BankAccountNo") %>' />
                                         <asp:Label ID="lblSDName" runat="server" Text='<%# Eval("SDName") %>' />
                                        <asp:Label ID="lblSDContactNo" Visible="false" runat="server" Text='<%# Eval("SDContactNo") %>' />
                                           <asp:Label ID="lblVendorTypeId" Visible="false" runat="server" Text='<%# Eval("VendorTypeId") %>' />
                                        <asp:Label ID="lblPANNo" runat="server" Visible="false" Text='<%# Eval("PANNo") %>' />
                                        <asp:Label ID="lblGSTNo" runat="server" Visible="false" Text='<%# Eval("GSTNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Distributor Username">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Distributor Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSDCode" runat="server" Text='<%# Eval("SDCode") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSDCPersonName" runat="server" Text='<%# Eval("SDCPersonName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person Mobile No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSDCPersonMobileNo" runat="server" Text='<%# Eval("SDCPersonMobileNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Approved Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComsApprovedRate" runat="server" Text='<%# Eval("ComsApprovedRate") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Trans. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComsTransRate" runat="server" Text='<%# Eval("ComsTransRate") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Dist. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComsDistRate" runat="server" Text='<%# Eval("ComsDistRate") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Limit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLimit" runat="server" Text='<%# Eval("Limit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("SubDistributorId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("SubDistributorId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
            <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50],
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
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Sub Distributor Registration Details',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
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

