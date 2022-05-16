<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Monthly_BMC_Info_By_DCS_Secretary.aspx.cs" Inherits="mis_DCSInformationSystem_Monthly_BMC_Info_By_DCS_Secretary_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
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
                    <h3 class="box-title">Monthly BMC Information to be Feed By DCS</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                             <asp:Label ID="lblBMC_Info_ID" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>
                    
                    <fieldset>
                        <legend>Monthly BMC Information
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
                                    <asp:Label ID="lblDCSCode" Text=" DCS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label41" Text=" Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMonth" ValidationGroup="a"
                                            ErrorMessage="Select Month" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:DropDownList ID="DDlMonth" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
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
                                    <asp:Label ID="Label46" Text=" Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvyear" ValidationGroup="a"
                                            ErrorMessage="Select Year" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                            ControlToValidate="ddlyear" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                     <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
                                                <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                            </asp:DropDownList>
                                </div>
                            </div>
                              <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label47" Text=" Milk Handled During Month(Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilk_handeled_during_month" ValidationGroup="a"
                                            ErrorMessage="Enter  Milk Handled During Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter  Milk Handled During Month !'></i>"
                                            ControlToValidate="txtMilk_handeled_during_month" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtMilk_handeled_during_month" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                                 </div>
                              <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label9" Text="BMC Running Hours" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvBMC_running_hours" ValidationGroup="a"
                                            ErrorMessage="Enter BMC Running Hours" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter BMC Running Hours !'></i>"
                                            ControlToValidate="txtBMC_running_hours" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtBMC_running_hours" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label10" Text="Repair & Maintenance Expense" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvRepair_mantenance_expence" ValidationGroup="a"
                                            ErrorMessage="Enter Repair & Maintenance Expense" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Repair & Maintenance Expense !'></i>"
                                            ControlToValidate="txtRepair_mantenance_expence" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                     <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtRepair_mantenance_expence" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                                  </div>

                             
                             <fieldset>
                        <legend> Electricity
                        </legend>
                        <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label48" Text="(i) Unit Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <asp:RequiredFieldValidator ID="rfvElectricity_unit" ValidationGroup="a"
                                            ErrorMessage="Enter Electricity Unit Consumed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Electricity Unit Consumed !'></i>"
                                            ControlToValidate="txtElectricity_unit" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtElectricity_unit" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label16" Text="(ii) Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvElectricity_Amount" ValidationGroup="a"
                                            ErrorMessage="Enter Electricity Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Electricity Amount !'></i>"
                                            ControlToValidate="txtElectricity_Amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtElectricity_Amount" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                           
                                 </div>
                           
                            </div>
                                 </fieldset>

                            <fieldset>
                        <legend> Diesel
                        </legend>
                        <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label1" Text="(i) Consumed (Ltrs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                     <asp:RequiredFieldValidator ID="rfvDiesel_consumed" ValidationGroup="a"
                                            ErrorMessage="Enter Diesel Consumed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Diesel Consumed !'></i>"
                                            ControlToValidate="txtDiesel_consumed" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--   <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                   <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtDiesel_consumed" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" Text="(ii) Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <asp:RequiredFieldValidator ID="rfvDiesel_Amount" ValidationGroup="a"
                                            ErrorMessage="Enter Diesel Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Diesel Amount !'></i>"
                                            ControlToValidate="txtDiesel_Amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtDiesel_Amount" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                           
                                 </div>
                           
                            </div>
                                 </fieldset>
                            <fieldset>
                        <legend> Oil
                        </legend>
                        <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label3" Text="(i) Consumed (Ltrs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                     <asp:RequiredFieldValidator ID="rfvOil_consumed" ValidationGroup="a"
                                            ErrorMessage="Enter Oil Consumed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Oil Consumed !'></i>"
                                            ControlToValidate="txtOil_consumed" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                         <%--  <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOil_consumed" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label4" Text="(ii) Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvOil_Amount" ValidationGroup="a"
                                            ErrorMessage="Enter Oil Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Oil Amount !'></i>"
                                            ControlToValidate="txtOil_Amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOil_Amount" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                           
                                 </div>
                           
                            </div>
                                 </fieldset>
                            <fieldset>
                        <legend> Cleaning Agent
                        </legend>
                        <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label5" Text="(i) Consumed (Ltrs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvCleaning_agent_consumed" ValidationGroup="a"
                                            ErrorMessage="Enter Cleaning Agent Consumed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cleaning Agent Consumed !'></i>"
                                            ControlToValidate="txtCleaning_agent_consumed" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                   <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtCleaning_agent_consumed" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label6" Text="(ii) Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvCleaning_agent_Amount" ValidationGroup="a"
                                            ErrorMessage="Enter Cleaning Agent Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cleaning Agent Amount !'></i>"
                                            ControlToValidate="txtCleaning_agent_Amount" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtCleaning_agent_Amount" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                           
                                 </div>
                           
                            </div>
                                 </fieldset>
                            <fieldset>
                        <legend>(11) Additional Manpower Deployed
                        </legend>
                        <div class="row">
                             <div class="col-md-12">
                           <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label7" Text="(i) Number" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAdditional_M_P_deploy_num" ValidationGroup="a"
                                            ErrorMessage="Enter Additional Manpower Number" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Additional Manpower Number !'></i>"
                                            ControlToValidate="txtAdditional_M_P_deploy_num" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateNum(event)" CssClass="form-control" ID="txtAdditional_M_P_deploy_num" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label8" Text="2) Amount Paid as Salary(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvAdditional_M_P_deploy_Amount_salary" ValidationGroup="a"
                                            ErrorMessage="Enter Additional Manpower Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Additional Manpower Amount !'></i>"
                                            ControlToValidate="txtAdditional_M_P_deploy_Amount_salary" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtAdditional_M_P_deploy_Amount_salary" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                           
                                 </div>
                           
                            </div>
                                 </fieldset>

                            

                             
                            
                        </div>
                       
                        <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                   <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" AccessKey="S" />
                                   <asp:Button runat="server" CssClass="btn btn-block btn-primary" Visible="false" ValidationGroup="a" ID="btnupdate" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S"  OnClick="btnupdate_Click"/>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="Monthly_BMC_Info_By_DCS_Secretary.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                         
                    </fieldset>

                    
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title">BMC Info Detail</h3>
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
                                   <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlSocietyflt" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSocietyflt" CssClass="form-control select2" runat="server">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:Label ID="Label12" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                              <div class="col-md-4">
                         <asp:Button runat="server" BackColor="#2e9eff" ValidationGroup="Search" CssClass="btn btn-success" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvBMC_detail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false"  OnRowCommand="gvBMC_detail_RowCommand" >
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
                                                               <asp:Label  ID="lblBMC_Info_ID" Text='<%# Eval("BMC_Info_ID").ToString()%>' runat="server" />

                                                     
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
                                                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("BMC_Info_ID") %>'></asp:ImageButton>
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
        function onlyNumber(ob) {
            var invalidChars = /\D+/g;
            if (invalidChars.test(ob.value)) {
                ob.value = ob.value.replace(invalidChars, "");
            }
        }
        function validateDec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            var chkhyphen = el.value.split('-');
            //if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45) {
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) ) {

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

