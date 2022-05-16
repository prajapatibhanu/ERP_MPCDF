<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkOrProductOrderByCitizen.aspx.cs" Inherits="mis_Demand_MilkOrProductOrderByCitizen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #F5BB3E !important;
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
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 340px;">
            <div class="modal-content">
                <div class="modal-header">
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
    <%--ConfirmationModal End --%>

    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- SELECT2 EXAMPLE -->
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Citizen Place Order</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>CITIZEN NAME, ADDRESS, MOBILE NO.
                                </legend>

                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                     <div class="col-md-3">
                                <div class="form-group">
                                <label>Citizen Name<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" 
                                    ControlToValidate="txtName" ErrorMessage="Enter Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                               
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="save" runat="server" ControlToValidate="txtName" 
                                    ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="*Valid characters: Alphabets."  Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                              <asp:TextBox ID="txtName" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Name"
                                   CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>

                                     <%--<div class="col-md-3">
                                <div class="form-group">
                                <label>Address<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic" ValidationGroup="a" 
                                     runat="server" ControlToValidate="txtAddress" ErrorMessage="Enter Address" 
                                    Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>">

                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="save" runat="server" 
                                    ControlToValidate="txtAddress" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="*Valid characters: Alphabets." 
                                     Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                              <asp:TextBox ID="txtAddress" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Address" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>--%>


                                     <div class="col-md-3">
                                <div class="form-group">
                                    <label>Mobile No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Contact Person Mobile No." ForeColor="Red"
                                             Text="<i class='fa fa-exclamation-circle' title='Enter Person Mobile No. !'></i>"
                                            ControlToValidate="txtMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                            Display="Dynamic" ValidationGroup="a"
                                            ErrorMessage="Enter Valid Contact Person Mobile No. !" ForeColor="Red"
                                             Text="<i class='fa fa-exclamation-circle' title='Enter Valid Contact Person Mobile No. !'></i>"
                                             ControlToValidate="txtMobileNo"
                                            ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" 
                                        ID="txtMobileNo" MaxLength="10" onkeypress="return validateNum(event);" 
                                        placeholder="Enter Contact Person Mobile No."></asp:TextBox>
                                </div>
                            </div>
                                       
                                      <div class="col-md-2" id="Div31" runat="server"  style="margin-top:18px">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" 
                                     ID="btnOTP" OnClick="btnOTP_Click" Text="Generate OTP" 
                                    OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                                       
                                    

                                     <div class="col-md-2" id="pnlOTP" runat="server" visible="false">
                                <div class="form-group">
                                <label>OTP<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red" Display="Dynamic" 
                                    ValidationGroup="a" 
                                     runat="server" ControlToValidate="txtOTP" ErrorMessage="Enter OTP." 
                                    Text="<i class='fa fa-exclamation-circle' title='Please Enter OTP!'></i>"></asp:RequiredFieldValidator>
                                
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="save" runat="server" 
                                    ControlToValidate="txtOTP" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="OTP" 
                                     Text="<i class='fa fa-exclamation-circle' title='Please Enter OTP !'></i>" />
                            </span>
                                    <asp:TextBox ID="txtOTP" autocomplete="off" Width="100%"  runat="server" placeholder="Enter OTP"
                                         CssClass="form-control" MaxLength="4" ClientIDMode="Static"></asp:TextBox>
                                 
                                   
                                    </div>
</div>
                                    
                                    
                                 
                                    <div class="col-md-2" id="pnlOk" style="margin-top:18px"  runat="server" visible="false" >
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary"  ID="btnOK" OnClick="btnOK_Click"
                                     Text="Validate OTP" 
                                    OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>

                                           </div>
                                    
                                    </fieldset>
                                    </div>
                                     <div class="box-body" id="pnlitemInfo" runat="server" visible="false" >
                                  <fieldset>
                                      <legend>
                                          DATE, SHIFT, CATEGORY, ITEM, QUANTITY
                                      </legend>

                                      <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Delivery Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                              
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" 
                                                    Display="Dynamic" ControlToValidate="txtDeliveryDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" 
                                                    SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                               
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" AutoPostBack="true" CssClass="form-control"
                                                 ID="txtDeliveryDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker"
                                                 onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" 
                                                data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                      <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Shift <span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                               
                                            </span>
                                            <asp:DropDownList ID="ddlShift"  OnInit="ddlShift_Init" AutoPostBack="true"  runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>


                                    
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="d"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged"  runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                <div class="form-group">
                                    <label>Item Type Name<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Item type " Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                            ControlToValidate="ddlItem" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2"
                                        OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"
                                         AutoPostBack="true" ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                    <div class="col-md-3">
                                <div class="form-group">
                                    <label>Item Name(Variant)<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
                                            InitialValue="0" ForeColor="Red" ErrorMessage="Select Item"
                                             Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                            ControlToValidate="ddlItemName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%></span>
                                    <asp:DropDownList ID="ddlItemName" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"  runat="server" CssClass="form-control select2"
                                         AutoPostBack="true" ClientIDMode="Static" >
                                       <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                     <div class="col-md-3">
                                <div class="form-group">
                                <label>Quantity<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" ControlToValidate="txtQty" ErrorMessage="Enter Item Variant Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator70" ValidationGroup="save" runat="server" ControlToValidate="txtQty" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="*Valid characters: Alphabets."  Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                              <asp:TextBox ID="txtQty" OnTextChanged="txtQty_TextChanged" AutoPostBack="true" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Item Quantity" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>


                                      <div class="col-md-3" >
                                <div class="form-group">
                                <label>Rate<span class="text-danger">*</span></label>
                                <span class="pull-right">
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  
                                    runat="server" ControlToValidate="txtRate" ErrorMessage="Enter Item Variant Name" 
                                    Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ValidationGroup="save" runat="server" 
                                    ControlToValidate="txtRate" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" Text="<i class='fa fa-exclamation-circle' ></i>" />
                            </span>
                              <asp:TextBox ID="txtRate" autocomplete="off" Width="100%"  runat="server" Enabled="false" 
                                   placeholder="Enter Rate" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>



                                      <div class="col-md-3" >
                                <div class="form-group">
                                <label>Total Rate<span class="text-danger">*</span></label>
                                <%--<span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" ControlToValidate="txtItemVariantName" ErrorMessage="Enter Item Variant Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="save" runat="server" ControlToValidate="txtItemVariantName" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="*Valid characters: Alphabets."  Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>--%>
                              <asp:TextBox ID="txtTotalRate" autocomplete="off" Width="100%"  runat="server" Enabled="false"  
                                  placeholder="" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>

                                      <div class="col-md-3" id="pnlQtyCitizenWise" runat="server" visible="false">
                                <div class="form-group">
                                <label>CitizenWise Quantity<span class="text-danger">*</span></label>
                                <%--<span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" ControlToValidate="txtQty" ErrorMessage="Enter Item Variant Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="save" runat="server" ControlToValidate="txtQty" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="*Valid characters: Alphabets."  Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>--%>
                              <asp:TextBox ID="txtxCitizenwiseQty"  AutoPostBack="true" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Item Quantity" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>

                                       <div class="col-md-3" id="pnlTotalQty" runat="server" visible="false">
                                <div class="form-group">
                                <label>Total Quantity<span class="text-danger">*</span></label>
                                <%--<span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a"  runat="server" ControlToValidate="txtQty" ErrorMessage="Enter Item Variant Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="save" runat="server" ControlToValidate="txtQty" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[0-9]+$" ErrorMessage="*Valid characters: Alphabets."  Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>--%>
                              <asp:TextBox ID="txtTotalQty"  AutoPostBack="true" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Item Quantity" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>


                                      </fieldset>

                                          
                                          <div class="col-md-2" id="pnlBtnAddCart" runat="server" visible="false" >
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnAddToCart" 
                                    Text="Add To Cart" OnClick="btnAddToCart_Click"
                                     OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                                         <div class="col-md-2" id="pnlBtnAddMoreCart" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a"  ID="btnAddMore" 
                                    Text="+ Add More Item" OnClick="btnAddMore_Click" 
                                     OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                                         <div class="col-md-2" id="pnlContinue" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnCitizenInfo"
                                     Text="Enter Information" OnClick="btnCitizenInfo_Click" 
                                    OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                                         </div>

                                         <div class="box-body" id="pnlCitizenInfo" runat="server" visible="false">
                                  <fieldset>
                                      <legend>
                                          Enter Information
                                      </legend>
                                         <div class="col-md-3">
                                <div class="form-group">
                                    <label>State<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                            ErrorMessage="Select State" Text="<i class='fa fa-exclamation-circle' title='Select State !'></i>"
                                            ControlToValidate="ddlState" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator7" ForeColor="Red"
                                             ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtTownOrvillage"
                                             ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allow" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special symbols like '.,/-:' allow !'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control select2"
                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                         AutoPostBack="true" ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>City<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficeaddress"  ValidationGroup="b"
                                            ErrorMessage="City" Text="<i class='fa fa-exclamation-circle' title='Enter City !'></i>"
                                            ControlToValidate="ddlCity" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="revofficeaddress" ForeColor="Red" ValidationGroup="a"
                                             Display="Dynamic" runat="server" ControlToValidate="txtAddress"

                                             ErrorMessage="Alphanumeric ,space and some special symbols like '.,/-:' allowed" Text="<i class='fa fa-exclamation-circle' title='Alphanumeric ,space and some special .,/-: allowed'></i>" SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s.,/-:]+$"></asp:RegularExpressionValidator>--%>
                                    </span>
                                       <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control select2"
                                        
                                      ClientIDMode="Static">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                     <div class="col-md-3">
                                <div class="form-group">
                                <label>Address line 1<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic"
                                     ValidationGroup="b" 
                                     runat="server" ControlToValidate="txtAddress1" ErrorMessage="Enter Address" 
                                    Text="<i class='fa fa-exclamation-circle' title='Please Enter Address!'></i>">

                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="b" runat="server" 
                                    ControlToValidate="txtAddress1" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="*Valid characters: Alphabets." 
                                     Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                              <asp:TextBox ID="txtAddress1" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Address line 1" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>


                                              
                                     <div class="col-md-3">
                                <div class="form-group">
                                <label>Address line 2<span class="text-danger">*</span></label>
                              <%--  <span class="pull-right">
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="save" runat="server" 
                                    ControlToValidate="txtAddress2" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z\s]+$" ErrorMessage="*Valid characters: Alphabets." 
                                     Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>--%>
                              <asp:TextBox ID="txtAddress2" autocomplete="off" Width="100%"  runat="server" placeholder="Enter Address line 2" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                 </div>
                              </div>
                                          
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Pincode<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvofficepincode" ValidationGroup="b"
                                            ErrorMessage="Enter Pincode" Text="<i class='fa fa-exclamation-circle' title='Enter Office Pincode !'></i>"
                                            ControlToValidate="txtPincode" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revofficepincode" ForeColor="Red" ValidationGroup="b" 
                                            Display="Dynamic" runat="server" ControlToValidate="txtPincode" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtPincode"
                                         MaxLength="6" placeholder="Enter Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
                                </div>
                            </div>
                                      


                                      </fieldset>
                                              <div class="col-md-2" id="pnlBtnViewCart" runat="server" >
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnProceed"
                                     Text="Proceed to Payment" OnClick="btnProceed_Click"
                                    OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                                             
                                              <div class="col-md-2" id="pnlSubmit" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnSubmit" 
                                    Text="Save"   OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2" id="Div5" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button ID="Button2" runat="server" Text="Reset" CssClass="btn btn-block btn-primary" />
                            </div>
                        </div>
                                </div>

                        </div>
      
                      </div>
                   

                    
                                </div>
                  
                    <div class="row">
                        <%--<div class="col-md-2" id="pnlSubmit" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="b" ID="btnSubmit" 
                                    Text="Save"   OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>--%>
                        <div class="col-md-2" id="pnlClear" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" Text="Reset" CssClass="btn btn-block btn-primary" />
                            </div>
                        </div>
                    </div>

              

        
              <div class="row"  id="pnlCart" runat="server" visible="false" >
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Cart Information</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" EmptyDataText="No Record Found." runat="server" 
                            class="table table-hover table-bordered pagination-ys" ShowFooter="true"
                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="MilkOrProductDemandCitizenId"> 
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग">
                                     <ItemTemplate>
                                          <asp:Label ID="lblCitizenName" runat="server" visible="false" Text='<%# Eval("CitizenName") %>' />
                                          <asp:Label ID="lblMobileNo" runat="server" visible="false" Text='<%# Eval("MobileNo") %>' />
                                        <asp:Label ID="lblItemCat_id" runat="server" visible="false" Text='<%# Eval("ItemCat_id") %>' />
                                          <asp:Label ID="lblItemCatName" runat="server" Text='<%# Eval("ItemCatName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name/ वस्तु नाम">
                                     <ItemTemplate>

                                        <asp:Label ID="lblItemType_id" runat="server"  visible="false"  Text='<%# Eval("ItemType_id") %>' />
                                         <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Variant Name/ वस्तु नाम">
                                     <ItemTemplate>
                                          <asp:Label ID="lblItem_id" runat="server"  visible="false"  Text='<%# Eval("Item_id") %>' />
                                        <asp:Label ID="lblItemVarName" runat="server" Text='<%# Eval("ItemName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity/ मात्रा">
                                    <ItemTemplate>
                                         <asp:Label ID="lblDeliveryShift_id" runat="server" visible="false" Text='<%# Eval("DeliveryShift_id") %>' />
                                        <asp:Label ID="lblItemQuantity" runat="server" Text='<%# Eval("QtyInNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate/ दाम(in Rs.)">
                                     <ItemTemplate>

                                        <asp:Label ID="lblItemRate" runat="server" Text='<%# Eval("CTotalAmount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Delivery Date / वितरण दिनांक">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDD" runat="server"  Text='<%# Eval("Delivery_Date","{0:dd/MM/yyyy}") %>' />
                                         <asp:Label ID="lblCState" runat="server"  Visible="false" Text='<%# Eval("CState")%>' />
                                         <asp:Label ID="lblCCity" runat="server"  Visible="false" Text='<%# Eval("CCity")%>' />
                                         <asp:Label ID="lblCAddrress1" runat="server" Visible="false"  Text='<%# Eval("CAddrress1")%>' /> 
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                 <asp:TemplateField HeaderText="Invoice No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceNo" runat="server"  Text='<%# Eval("InvoiceNo") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>    



                          
                               
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("MilkOrProductDemandCitizenId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("MilkOrProductDemandCitizenId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="row">
                     <div class="col-md-2" id="Div2" runat="server" >
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary"  ID="btnPay" Text="Pay Now"
                                    Enabled="false" OnClick="btnPay_Click"
                                      AccessKey="S" />
                            </div>
                        </div>
                        </div>
                </div>

            </div>
        </div>
              </div>
         
    
    </section>
     </div>
        <!-- /.content -->

  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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


