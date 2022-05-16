<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MFI_By_DCS_Secretary.aspx.cs" Inherits="mis_DCSInformationSystem_MFI_By_DCS_Secretary" %>

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
                    <h3 class="box-title">Monthly Financial Information to be Feed By DCS</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                              <asp:Label ID="lblMFI_DCS_ID" Visible="false" runat="server"></asp:Label>
                              <asp:Label ID="lblTotal_operating_income" Visible="false" runat="server"></asp:Label>
                              <asp:Label ID="lblTotal_operating_Expense" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Monthly Financial Information
                        </legend>
                        <div class="row">
                            <div class="col-md-12">
                                 <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvBMC" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlSociety_SelectedIndexChanged" AutoPostBack="true">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblDCSCode" Text="(1) DCS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter DCS Code" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label41" Text="(2) Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMonth" ValidationGroup="a"
                                            ErrorMessage="Select Month" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth" runat="server" AutoPostBack="false" CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label46" Text="(3) Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;"  runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="false" CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label47" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox39" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>--%>
                            </div>


                            <fieldset>
                                <legend>(4) Operating Income(Rs)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label48" Text="(i) Received From Milk Union" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                      <%--   <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter Received From Milk Union" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Received From Milk Union !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label16" Text="(a) Milk Amount" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Milk_amount" ValidationGroup="a"
                                            ErrorMessage="Enter Milk Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Amount !'></i>"
                                            ControlToValidate="txtOI_Milk_amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtOI_Milk_amount" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label17" Text="(b) DCS Commission" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                   <asp:RequiredFieldValidator ID="rfvOI_DCS_Commition" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Commission" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Commission !'></i>"
                                            ControlToValidate="txtOI_DCS_Commition" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_DCS_Commition" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label18" Text="(c) Ghee Commission" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Ghee_Commition" ValidationGroup="a"
                                            ErrorMessage="Enter Ghee Commission" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Ghee Commission !'></i>"
                                            ControlToValidate="txtOI_Ghee_Commition" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Ghee_Commition" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" Text="(d) Cattle Feed Commission" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Cattle_feed_Commition" ValidationGroup="a"
                                            ErrorMessage="Enter Cattle Feed Commission" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Commission !'></i>"
                                            ControlToValidate="txtOI_Cattle_feed_Commition" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Cattle_feed_Commition" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" Text="(e) Miniral Mixture Commission" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Miniral_mixture_Commition" ValidationGroup="a"
                                            ErrorMessage="Enter  Miniral Mixture Commission" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Miniral Mixture Commission !'></i>"
                                            ControlToValidate="txtOI_Miniral_mixture_Commition" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Miniral_mixture_Commition" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" Text="f)Head Load" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Head_load" ValidationGroup="a"
                                            ErrorMessage="Enter Head Load" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Head Load !'></i>"
                                            ControlToValidate="txtOI_Head_load" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Head_load" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label4" Text="(g) BMC Chilling Charges" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_BMC_Chilling_charges" ValidationGroup="a"
                                            ErrorMessage="Enter BMC Chilling Charges" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter BMC Chilling Charges !'></i>"
                                            ControlToValidate="txtOI_BMC_Chilling_charges" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_BMC_Chilling_charges" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%-- <asp:Label ID="Label56" Text="3" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>

                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Label ID="Label5" Text="Rate(Rs/Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox5" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Label ID="Label6" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%--  <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox6" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label52" Text="(ii) Local Milk Sale Amount" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Local_milk_sale_amount" ValidationGroup="a"
                                            ErrorMessage="Enter Local Milk Sale Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Local Milk Sale Amount !'></i>"
                                            ControlToValidate="txtOI_Local_milk_sale_amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Local_milk_sale_amount" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label5" Text="(iii) Sample Milk Sale Amount" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Sample_milk_sale_amount" ValidationGroup="a"
                                            ErrorMessage="Enter Sample Milk Sale Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sample Milk Sale Amount !'></i>"
                                            ControlToValidate="txtOI_Sample_milk_sale_amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Sample_milk_sale_amount" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label6" Text="(iv) Other" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOI_Other" ValidationGroup="a"
                                            ErrorMessage="Enter Other Income" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Other Income !'></i>"
                                            ControlToValidate="txtOI_Other" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOI_Other" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Label ID="Label19" Text="Head Load" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%--  <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox21" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>(5) Operating Expenses(Rs)
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" Text="(i) Payment to Producer" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_Payment_to_producer" ValidationGroup="a"
                                            ErrorMessage="Enter Payment to Producer" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Payment to Producer !'></i>"
                                            ControlToValidate="txtOE_Payment_to_producer" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Payment_to_producer" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label8" Text="(ii) Head Load" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                   <asp:RequiredFieldValidator ID="rfvOE_Head_load" ValidationGroup="a"
                                            ErrorMessage="Enter Head Load Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Head Load Expenses!'></i>"
                                            ControlToValidate="txtOE_Head_load" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Head_load" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label9" Text="(iii) Chemical & Detergent" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_Camical_detergent" ValidationGroup="a"
                                            ErrorMessage="Enter Chemical & Detergent Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Chemical & Detergent Expenses!'></i>"
                                            ControlToValidate="txtOE_Camical_detergent" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                     <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Camical_detergent" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" Text="(iv) Traveling" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_Traveling" ValidationGroup="a"
                                            ErrorMessage="Enter Traveling Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Traveling Expenses!'></i>"
                                            ControlToValidate="txtOE_Traveling" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Traveling" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="labelStationery" Text="(v) Stationery" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_Stationary" ValidationGroup="a"
                                            ErrorMessage="Enter Stationery Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stationery Expenses!'></i>"
                                            ControlToValidate="txtOE_Stationary" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Stationary" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label11" Text="(vi) Computer Expenses" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                 <asp:RequiredFieldValidator ID="rfvOE_Computer_expense" ValidationGroup="a"
                                            ErrorMessage="Enter Computer Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Computer Expenses !'></i>"
                                            ControlToValidate="txtOE_Computer_expense" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                           <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Computer_expense" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label12" Text="(vii) Office Expenses" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                  <asp:RequiredFieldValidator ID="rfvOE_Office_expense" ValidationGroup="a"
                                            ErrorMessage="Enter Office Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Office Expenses !'></i>"
                                            ControlToValidate="txtOE_Office_expense" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                          <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Office_expense" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label19" Text="(viii) General Body Meeting" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                     <asp:RequiredFieldValidator ID="rfvOE_General_body_meeting" ValidationGroup="a"
                                            ErrorMessage="Enter  Meeting Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter  Meeting Expenses!'></i>"
                                            ControlToValidate="txtOE_General_body_meeting" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_General_body_meeting" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label56" Text="(ix) Salary to Staff" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                            ErrorMessage="Enter Salary to Staff" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Salary to Staff !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label20" Text="(a) Secretary" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_STS_Secretary" ValidationGroup="a"
                                            ErrorMessage="Enter Secretary Salary" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Secretary Salary!'></i>"
                                            ControlToValidate="txtOE_STS_Secretary" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onkeypress="return validateDec(this, event)" autocomplete="off" CssClass="form-control" ID="txtOE_STS_Secretary" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label5111" Text="(b) Tester/Helper" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_STS_Tester_helper" ValidationGroup="a"
                                            ErrorMessage="Enter Tester/Helper Salary" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tester/Helper Salary!'></i>"
                                            ControlToValidate="txtOE_STS_Tester_helper" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_STS_Tester_helper" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label666" Text="(c) AHC/AI Worker" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_STS_AHC_AIworker" ValidationGroup="a"
                                            ErrorMessage="Enter AHC/AI Worker Salary" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter AHC/AI Worker Salary!'></i>"
                                            ControlToValidate="txtOE_STS_AHC_AIworker" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_STS_AHC_AIworker" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label21" Text="(X) Other" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvOE_Other" ValidationGroup="a"
                                            ErrorMessage="Enter Other Expenses" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Other Expenses !'></i>"
                                            ControlToValidate="txtOE_Other" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOE_Other" MaxLength="150"  onchange="abc()" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%-- <asp:Label ID="Label22" Text="Sample Milk Sale Amount" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox25" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%-- <asp:Label ID="Label23" Text="Other" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%--   <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox26" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Label ID="Label19" Text="Head Load" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox27" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label69" Text="(6) Operating Profit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                          <%--    <asp:RequiredFieldValidator ID="rfvTotal_Profit_Loss" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtTotal_Profit_Loss" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:Label runat="server"  autocomplete="off" CssClass="form-control" ID="txtTotal_Profit_Loss" MaxLength="150"  ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>

                            </div>



                        </div>

                        <div class="row">
                            <hr />
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" OnClick="btnSubmit_Click"/>
                                 <asp:Button runat="server" CssClass="btn btn-block btn-primary" Visible="false" ValidationGroup="a" ID="btnupdate" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S"  OnClick="btnupdate_Click"/>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <a class="btn btn-block btn-default" href="MFI_By_DCS_Secretary.aspx">Clear</a>
                                </div>
                            </div>
                        </div>

                    </fieldset>


                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title">MOnthly Financial Information Detail</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                         <div class="col-md-12">
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Search"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetailflt" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetailflt"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetailflt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlSocietyflt" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSocietyflt" CssClass="form-control select2" runat="server">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Search"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth2" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth2" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlMonth2_SelectedIndexChanged" >
                                             <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlyear2" runat="server" CssClass="form-control"  OnSelectedIndexChanged="DDlMonth2_SelectedIndexChanged" >
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              <div class="col-md-2">
                         <asp:Button runat="server" BackColor="#2e9eff" CssClass="btn btn-success" ValidationGroup="Search" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvMFI_detail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowCommand="gvMFI_detail_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                           <%--  <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="DCS Name">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblDCSName" Text='<%# Eval("DCSName").ToString()%>' runat="server" />
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblMFI_DCS_ID" Text='<%# Eval("MFI_DCS_ID").ToString()%>' runat="server" />

                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblYear" Text='<%# Eval("Year").ToString()%>' runat="server" class="form-control"></asp:Label>

                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Month">
                                                        <ItemTemplate>
                                                         <asp:Label  ID="lblMonth" Text='<%# Eval("month_name").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Creation Date">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblCreatedAt" Text='<%# Eval("CreatedAt").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Un-Skilled *">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblNum_unskilled" Text='<%# Eval("Num_unskilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Semi Skilled">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblNum_semi_skilled" Text='<%# Eval("Num_semi_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skilled *">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblNum_skilled" Text='<%# Eval("Num_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Un-Skilled">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblWBA_unskilled" Text='<%# Eval("WBA_unskilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Semi Skilled">
                                                        <ItemTemplate>
                                                                <asp:Label  ID="lbWBA_semi_skilled" Text='<%# Eval("WBA_semi_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skilled *">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblWBA_skilled" Text='<%# Eval("WBA_skilled").ToString()%>'  runat="server" class="form-control"></asp:Label>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                   
                                                    <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-CssClass="text-center" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("MFI_DCS_ID") %>'></asp:ImageButton>
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">

        function abc() {
            debugger;
            var OI_Milk_amount = document.getElementById('<%=txtOI_Milk_amount.ClientID%>').value;
            var OI_DCS_Commition = document.getElementById('<%=txtOI_DCS_Commition.ClientID%>').value;
            var OI_Ghee_Commition = document.getElementById('<%=txtOI_Ghee_Commition.ClientID%>').value;
            var OI_Cattle_feed_Commition = document.getElementById('<%=txtOI_Cattle_feed_Commition.ClientID%>').value;
            var OI_Miniral_mixture_Commition = document.getElementById('<%=txtOI_Miniral_mixture_Commition.ClientID%>').value;
            var OI_Head_load = document.getElementById('<%=txtOI_Head_load.ClientID%>').value;
            var OI_BMC_Chilling_charges = document.getElementById('<%=txtOI_BMC_Chilling_charges.ClientID%>').value;
            var OI_Local_milk_sale_amount = document.getElementById('<%=txtOI_Local_milk_sale_amount.ClientID%>').value;
            var OI_Sample_milk_sale_amount = document.getElementById('<%=txtOI_Sample_milk_sale_amount.ClientID%>').value;
            var OI_Other = document.getElementById('<%=txtOI_Other.ClientID%>').value;

            var OE_Payment_to_producer = document.getElementById('<%=txtOE_Payment_to_producer.ClientID%>').value;
            var OE_Head_load = document.getElementById('<%=txtOE_Head_load.ClientID%>').value;
            var OE_Camical_detergent = document.getElementById('<%=txtOE_Camical_detergent.ClientID%>').value;
            var OE_Traveling = document.getElementById('<%=txtOE_Traveling.ClientID%>').value;
            var OE_Stationary = document.getElementById('<%=txtOE_Stationary.ClientID%>').value;
            var OE_Computer_expense = document.getElementById('<%=txtOE_Computer_expense.ClientID%>').value;
            var OE_Office_expense = document.getElementById('<%=txtOE_Office_expense.ClientID%>').value;
            var OE_General_body_meeting = document.getElementById('<%=txtOE_General_body_meeting.ClientID%>').value;
            var OE_STS_Secretary = document.getElementById('<%=txtOE_STS_Secretary.ClientID%>').value;
            var OE_STS_Tester_helper = document.getElementById('<%=txtOE_STS_Tester_helper.ClientID%>').value;
            var OE_STS_AHC_AIworker = document.getElementById('<%=txtOE_STS_AHC_AIworker.ClientID%>').value;
            var OE_Other = document.getElementById('<%=txtOE_Other.ClientID%>').value;

            var dat = { "OI_Milk_amount": OI_Milk_amount, "OI_DCS_Commition": OI_DCS_Commition, "OI_Ghee_Commition": OI_Ghee_Commition, "OI_Cattle_feed_Commition": OI_Cattle_feed_Commition, "OI_Miniral_mixture_Commition": OI_Miniral_mixture_Commition, "OI_Head_load": OI_Head_load, "OI_BMC_Chilling_charges": OI_BMC_Chilling_charges, "OI_Local_milk_sale_amount": OI_Local_milk_sale_amount, "OI_Sample_milk_sale_amount": OI_Sample_milk_sale_amount, "OI_Other": OI_Other, "OE_Payment_to_producer": OE_Payment_to_producer, "OE_Head_load": OE_Head_load, "OE_Camical_detergent": OE_Camical_detergent, "OE_Traveling": OE_Traveling, "OE_Stationary": OE_Stationary, "OE_Computer_expense": OE_Computer_expense, "OE_Office_expense": OE_Office_expense, "OE_General_body_meeting": OE_General_body_meeting, "OE_STS_Secretary": OE_STS_Secretary, "OE_STS_Tester_helper": OE_STS_Tester_helper, "OE_STS_AHC_AIworker": OE_STS_AHC_AIworker, "OE_Other": OE_Other }
            $.ajax(
            {

                url: "MFI_By_DCS_Secretary.aspx/Getcal",
                type: 'POST',
                data: JSON.stringify(dat),
               dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // alert(data.d)
                    //$("#divMyText").html(data.d);
                    //$("#divMyText1").html(data.d);
                    $("#txtTotal_Profit_Loss").text(data.d);



                },
                error: function () {
                    alert("error");
                }
            });

            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    url: "AMSDashboard_New.aspx/GetListItems",
            //    data: JSON.stringify(dat),
            //    dataType: "json",
            //    success: function (data) {

            //    },
            //    error: function (result) {
            //        alert("Sorry!!! Your session has expired. Please log in again";
            //    }
            //});
        };
        function validateDec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            var chkhyphen = el.value.split('-');
            //if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45) {
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if ((number.length > 1 && charCode == 46) || (chkhyphen.length > 1 && charCode == 45)) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }

    </script>
    <script type="text/javascript">
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 32 && charCode < 48) || charCode > 57) {

                return false;
            }
            return true;
        }
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
