<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Monthly_Chilling_center_report_bymanager_CC.aspx.cs" Inherits="mis_DCSInformationSystem_Monthly_Chilling_center_report_bymanager" %>

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
                    <h3 class="box-title">Monthly Chilling Centre Report Feeded by Manager CC</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblMCCR_ID" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>
                    <%--<fieldset>
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
                    </fieldset>--%>
                    <fieldset>
                        <legend>Monthly Chilling Centre Report
                        </legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label142" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:DropDownList ID="DDlMonth" runat="server" CssClass="form-control" >
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label143" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </br>
                             <div class="col-md-12">
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="lblDCScode" Text="(1) Opening Balance Of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right"></span>
                                         <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="Label41" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="rfvOB_M_Qty" ValidationGroup="a"
                                                 ErrorMessage="Enter Opening Balance Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter  Opening Balance Quantity!'></i>"
                                                 ControlToValidate="txtOB_M_Qty" Display="Dynamic" runat="server">
                                             </asp:RequiredFieldValidator>
                                             <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                         </span>
                                         <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtOB_M_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="Label46" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="rfvOB_M_Fat" ValidationGroup="a"
                                                 ErrorMessage="Enter Opening Balance SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Balance SNF !'></i>"
                                                 ControlToValidate="txtOB_M_Fat" Display="Dynamic" runat="server">
                                             </asp:RequiredFieldValidator>
                                             <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                         </span>
                                         <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtOB_M_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="Label47" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right">
                                             <asp:RequiredFieldValidator ID="rfvOB_M_SNF" ValidationGroup="a"
                                                 ErrorMessage="Enter Opening Balance SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Balance SNF !'></i>"
                                                 ControlToValidate="txtOB_M_SNF" Display="Dynamic" runat="server">
                                             </asp:RequiredFieldValidator>
                                             <%--</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                         </span>
                                         <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtOB_M_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                             </div>


                            <fieldset>
                                <legend>(2) Milk Purchase From DCS
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label48" Text="(i) Good" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label49" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_good_Qty" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Good Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Good Quantity!'></i>"
                                                        ControlToValidate="txtMilk_pur_good_Qty" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtMilk_pur_good_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label50" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_good_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Good Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Good Fat!'></i>"
                                                        ControlToValidate="txtMilk_pur_good_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_pur_good_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label51" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_good_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Good SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Good SNF!'></i>"
                                                        ControlToValidate="txtMilk_pur_good_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_pur_good_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label52" Text="(ii) Sour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_Sour_Qty" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Sour Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Sour Quantity!'></i>"
                                                        ControlToValidate="txtMilk_pur_Sour_Qty" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtMilk_pur_Sour_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_Sour_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Sour Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Sour Fat!'></i>"
                                                        ControlToValidate="txtMilk_pur_Sour_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_pur_Sour_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_Sour_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Sour SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Sour SNF!'></i>"
                                                        ControlToValidate="txtMilk_pur_Sour_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_pur_Sour_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label56" Text="(iii) Curdle" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label4" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_Curdle_Qty" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Curdle Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Curdle Quantity!'></i>"
                                                        ControlToValidate="txtMilk_pur_Curdle_Qty" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtMilk_pur_Curdle_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label5" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_Curdle_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Curdle Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Curdle Fat!'></i>"
                                                        ControlToValidate="txtMilk_pur_Curdle_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_pur_Curdle_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label6" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_pur_Curdle_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Purchase Curdle SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Purchase Curdle SNF!'></i>"
                                                        ControlToValidate="txtMilk_pur_Curdle_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_pur_Curdle_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label61" Text="(3) S/C Milk Allocated For Product" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%-- <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
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
                                        <asp:Label ID="Label62" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSC_milk_for_prod_Qty" ValidationGroup="a"
                                                ErrorMessage="Enter S/C Milk Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter S/C Milk Quantity!'></i>"
                                                ControlToValidate="txtSC_milk_for_prod_Qty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtSC_milk_for_prod_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label63" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSC_milk_for_prod_Fat" ValidationGroup="a"
                                                ErrorMessage="Enter S/C Milk Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter S/C Milk Fat!'></i>"
                                                ControlToValidate="txtSC_milk_for_prod_Fat" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtSC_milk_for_prod_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label64" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSC_milk_for_prod_SNF" ValidationGroup="a"
                                                ErrorMessage="Enter S/C Milk SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter S/C Milk SNF!'></i>"
                                                ControlToValidate="txtSC_milk_for_prod_SNF" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtSC_milk_for_prod_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label65" Text="(4) Milk Dispatch to Dairy Plant" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right"></span>
                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label66" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilk_dispatch_dairy_Qty" ValidationGroup="a"
                                                ErrorMessage="Enter Milk Dispatch Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Dispatch Quantity!'></i>"
                                                ControlToValidate="txtMilk_dispatch_dairy_Qty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_dispatch_dairy_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label67" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilk_dispatch_dairy_Fat" ValidationGroup="a"
                                                ErrorMessage="Enter Milk Dispatch Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Dispatch Fat!'></i>"
                                                ControlToValidate="txtMilk_dispatch_dairy_Fat" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_dispatch_dairy_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label68" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilk_dispatch_dairy_SNF" ValidationGroup="a"
                                                ErrorMessage="Enter Milk Dispatch SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Dispatch SNF!'></i>"
                                                ControlToValidate="txtMilk_dispatch_dairy_SNF" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_dispatch_dairy_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label69" Text="(5) Closing Balance Of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right"></span>
                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label70" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCB_M_Qty" ValidationGroup="a"
                                                ErrorMessage="Enter Closing Balance Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance Quantity!'></i>"
                                                ControlToValidate="txtCB_M_Qty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtCB_M_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label71" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCB_M_Fat" ValidationGroup="a"
                                                ErrorMessage="Enter Closing Balance Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance Fat!'></i>"
                                                ControlToValidate="txtCB_M_Fat" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCB_M_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label72" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvCB_M_SNF" ValidationGroup="a"
                                                ErrorMessage="Enter Closing Balance SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance SNF!'></i>"
                                                ControlToValidate="txtCB_M_SNF" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCB_M_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <fieldset>
                                <legend>(6) White Butter Mfg from S/C MIlk
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label74" Text="(i) Opening Balance Of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label75" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_OB_Qty" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter Opening Balance Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter Opening Balance Quantity!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_OB_Qty" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtWhite_butter_SC_OB_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label76" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_OB_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter Opening Balance Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter Opening Balance Fat!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_OB_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_OB_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label77" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_OB_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter Opening Balance SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter Opening Balance SNF!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_OB_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_OB_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label78" Text="(ii) WB Manufactured" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label79" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_WBM_Qty" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter WB Manufactured Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter WB Manufactured Quantity!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_WBM_Qty" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_WBM_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label80" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_WBM_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter WB Manufactured Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter WB Manufactured Fat!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_WBM_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_WBM_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label81" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_WBM_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter WB Manufactured SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter WB Manufactured SNF!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_WBM_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_WBM_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label82" Text="(iii) Closing Balance Of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label83" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_CB_Qty" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter Closing Balance Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter Closing Balance Quantity!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_CB_Qty" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtWhite_butter_SC_CB_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label84" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvWhite_butter_SC_CB_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter Closing Balance Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter Closing Balance Fat!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_CB_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_CB_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label85" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv" ValidationGroup="a"
                                                        ErrorMessage="Enter White Butter Closing Balance SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter White Butter Closing Balance SNF!'></i>"
                                                        ControlToValidate="txtWhite_butter_SC_CB_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtWhite_butter_SC_CB_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>(7) Milk Handling Variation
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label114" Text="(i) Quantity(Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label115"  Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%--  <asp:RequiredFieldValidator ID="rfvMilk_Hand_variation_Q_Qty" ValidationGroup="a"
                                            ErrorMessage="Enter Milk Handling Variation Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Handling Variation Quantity!'></i>"
                                            ControlToValidate="txtMilk_Hand_variation_Q_Qty" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server"   autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_Hand_variation_Q_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label116" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_Hand_variation_Q_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Handling Variation Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Handling Variation Fat!'></i>"
                                                        ControlToValidate="txtMilk_Hand_variation_Q_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_Hand_variation_Q_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label117" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_Hand_variation_Q_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Handling Variation SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Handling Variation SNF!'></i>"
                                                        ControlToValidate="txtMilk_Hand_variation_Q_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_Hand_variation_Q_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label118" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label119" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="rfvMilk_Hand_variation_Per_Qty" ValidationGroup="a"
                                             ErrorMessage="Enter Milk Handling Variation %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Handling Variation %!'></i>"
                                            ControlToValidate="txtMilk_Hand_variation_Per_Qty" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server"   autocomplete="off"  CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_Hand_variation_Per_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label120" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_Hand_variation_Per_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Handling Variation % Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Handling Variation % Fat!'></i>"
                                                        ControlToValidate="txtMilk_Hand_variation_Per_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_Hand_variation_Per_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label121" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilk_Hand_variation_Per_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Milk Handling Variation % SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Handling Variation % SNF!'></i>"
                                                        ControlToValidate="txtMilk_Hand_variation_Per_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtMilk_Hand_variation_Per_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>(8) Product Mfg Variation
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label122" Text="(i) Quantity(Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label123" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="rfvProduct_mfg_variation_Q_Qty" ValidationGroup="a"
                                           ErrorMessage="Enter Product Mfg Variation Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Mfg Variation Quantity!'></i>"
                                            ControlToValidate="txtProduct_mfg_variation_Q_Qty" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server"   autocomplete="off"  onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtProduct_mfg_variation_Q_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label124" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvProduct_mfg_variation_Q_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Product Mfg Variation Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Mfg Variation Fat!'></i>"
                                                        ControlToValidate="txtProduct_mfg_variation_Q_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtProduct_mfg_variation_Q_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label125" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvProduct_mfg_variation_Q_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Product Mfg Variation SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Mfg Variation SNF!'></i>"
                                                        ControlToValidate="txtProduct_mfg_variation_Q_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtProduct_mfg_variation_Q_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label126" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label127" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="rfvProduct_mfg_variation_Per_Qty" ValidationGroup="a"
                                            ErrorMessage="Enter Product Mfg Variation %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Mfg Variation %!'></i>"
                                            ControlToValidate="txtProduct_mfg_variation_Per_Qty" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server"   autocomplete="off"  CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtProduct_mfg_variation_Per_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label128" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvProduct_mfg_variation_Per_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Product Mfg Variation % Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Mfg Variation % Fat!'></i>"
                                                        ControlToValidate="txtProduct_mfg_variation_Per_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtProduct_mfg_variation_Per_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label129" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvProduct_mfg_variation_Per_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Product Mfg Variation % SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Product Mfg Variation % SNF!'></i>"
                                                        ControlToValidate="txtProduct_mfg_variation_Per_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtProduct_mfg_variation_Per_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label138" Text="(9) Tanker Milk Received by DP" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right"></span>
                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label139" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTanker_milk_Rec_DP_Qty" ValidationGroup="a"
                                                ErrorMessage="Enter Tanker Milk Received by DP Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Received by DP Quantity!'></i>"
                                                ControlToValidate="txtTanker_milk_Rec_DP_Qty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="cal()" autocomplete="off" onkeypress="return validateDec(this, event)"  CssClass="form-control" ID="txtTanker_milk_Rec_DP_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label140" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTanker_milk_Rec_DP_Fat" ValidationGroup="a"
                                                ErrorMessage="Enter Tanker Milk Received by DP Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Received by DP Fat!'></i>"
                                                ControlToValidate="txtTanker_milk_Rec_DP_Fat" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_Rec_DP_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label141" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTanker_milk_Rec_DP_SNF" ValidationGroup="a"
                                                ErrorMessage="Enter Tanker Milk Received by DP SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Received by DP SNF!'></i>"
                                                ControlToValidate="txtTanker_milk_Rec_DP_SNF" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_Rec_DP_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <fieldset>
                                <legend>(10) Tanker Milk Variation
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label130" Text="(i) Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label131" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="rfvTanker_milk_variation_Q_Qty" ValidationGroup="a"
                                            ErrorMessage="Enter Tanker Milk Variation Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Variation Quantity!'></i>"
                                            ControlToValidate="txtTanker_milk_variation_Q_Qty" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server"  autocomplete="off"  CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_variation_Q_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label132" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTanker_milk_variation_Q_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Tanker Milk Variation Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Variation Fat!'></i>"
                                                        ControlToValidate="txtTanker_milk_variation_Q_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_variation_Q_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label133" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTanker_milk_variation_Q_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Tanker Milk Variation SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Variation SNF!'></i>"
                                                        ControlToValidate="txtTanker_milk_variation_Q_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_variation_Q_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label134" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label135" Text="Quantity (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <%--  <asp:RequiredFieldValidator ID="rfvTanker_milk_variation_Per_Qty" ValidationGroup="a"
                                           ErrorMessage="Enter Tanker Milk Variation  %" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Variation  %!'></i>"
                                            ControlToValidate="txtTanker_milk_variation_Per_Qty" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:TextBox runat="server"   autocomplete="off"  CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_variation_Per_Qty" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label136" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTanker_milk_variation_Per_Fat" ValidationGroup="a"
                                                        ErrorMessage="Enter Tanker Milk Variation  % Fat" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Variation  % Fat!'></i>"
                                                        ControlToValidate="txtTanker_milk_variation_Per_Fat" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"   ID="txtTanker_milk_variation_Per_Fat" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label137" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTanker_milk_variation_Per_SNF" ValidationGroup="a"
                                                        ErrorMessage="Enter Tanker Milk Variation  % SNF" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Milk Variation  % SNF!'></i>"
                                                        ControlToValidate="txtTanker_milk_variation_Per_SNF" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtTanker_milk_variation_Per_SNF" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>


                            <fieldset>
                                <legend>(11) Stocks
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label8" Text="(i) Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label9" Text="WB (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_OB_WB" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Opening Balance WB" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Opening Balance WB!'></i>"
                                                        ControlToValidate="txtStock_OB_WB" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_OB_WB" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_OB_Ghee" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Opening Balance Ghee" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Opening Balance Ghee!'></i>"
                                                        ControlToValidate="txtStock_OB_Ghee" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_OB_Ghee" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label11" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_OB_Cattlefeed" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Opening Balance Cattle Feed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Opening Balance Cattle Feed!'></i>"
                                                        ControlToValidate="txtStock_OB_Cattlefeed" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_OB_Cattlefeed" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label12" Text="(ii) Manufactured" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" Text="WB (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Manufa_WB" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Manufactured WB" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Manufactured Cattle WB!'></i>"
                                                        ControlToValidate="txtStock_Manufa_WB" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Manufa_WB" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label14" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Manufa_Ghee" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Manufactured Ghee" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Manufactured Ghee!'></i>"
                                                        ControlToValidate="txtStock_Manufa_Ghee" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Manufa_Ghee" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label15" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Manufa_Cattlefeed" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Manufactured Cattle Feed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Manufactured Cattle Feed!'></i>"
                                                        ControlToValidate="txtStock_Manufa_Cattlefeed" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Manufa_Cattlefeed" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label16" Text="(iii) Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label17" Text="WB (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Rec_WB" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Received WB" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Received WB!'></i>"
                                                        ControlToValidate="txtStock_Rec_WB" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Rec_WB" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label18" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Rec_Ghee" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Received Ghee" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Received Ghee!'></i>"
                                                        ControlToValidate="txtStock_Rec_Ghee" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Rec_Ghee" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label19" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Rec_Cattlefeed" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Received Cattle Feed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Received Cattle Feed!'></i>"
                                                        ControlToValidate="txtStock_Rec_Cattlefeed" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Rec_Cattlefeed" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label20" Text="(iv) Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label21" Text="WB (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Sold_WB" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Sold WB" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Sold WB!'></i>"
                                                        ControlToValidate="txtStock_Sold_WB" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Sold_WB" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label22" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Sold_Ghee" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Sold Ghee" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Sold Ghee!'></i>"
                                                        ControlToValidate="txtStock_Sold_Ghee" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Sold_Ghee" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label23" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Sold_Cattlefeed" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Sold Cattle Feed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Sold Cattle Feed!'></i>"
                                                        ControlToValidate="txtStock_Sold_Cattlefeed" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Sold_Cattlefeed" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label24" Text="(v) Dispatch to DP" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label25" Text="WB (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Dispatch_DP_WB" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Dispatch to DP WB" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Dispatch to DP WB!'></i>"
                                                        ControlToValidate="txtStock_Dispatch_DP_WB" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Dispatch_DP_WB" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label26" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Dispatch_DP_Ghee" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Dispatch to DP Ghee" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Dispatch to DP Ghee!'></i>"
                                                        ControlToValidate="txtStock_Dispatch_DP_Ghee" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Dispatch_DP_Ghee" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label27" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_Dispatch_DP_Cattlefeed" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Dispatch to DP Cattle Feed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Dispatch to DP Cattle Feed!'></i>"
                                                        ControlToValidate="txtStock_Dispatch_DP_Cattlefeed" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_Dispatch_DP_Cattlefeed" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label28" Text="(vi) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label29" Text="WB (Kg)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_CB_WB" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Closing Balance WB" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Closing Balance WB!'></i>"
                                                        ControlToValidate="txtStock_CB_WB" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_CB_WB" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label30" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_CB_Ghee" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Closing Balance Ghee" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Closing Balance Ghee!'></i>"
                                                        ControlToValidate="txtStock_CB_Ghee" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_CB_Ghee" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label31" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvStock_CB_Cattlefeed" ValidationGroup="a"
                                                        ErrorMessage="Enter Stock Closing Balance Cattle Feed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stock Closing Balance Cattle Feed!'></i>"
                                                        ControlToValidate="txtStock_CB_Cattlefeed" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtStock_CB_Cattlefeed" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>


                            <fieldset>
                                <legend>(12) DCS to CC Transportation Expenses
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label33" Text="(i) Headload(DCS No.s)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label34" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDCS_CCTE_Headload_dcsnos_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Transportation Expenses Headload Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Transportation Expenses Headload Unit!'></i>"
                                                        ControlToValidate="txtDCS_CCTE_Headload_dcsnos_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtDCS_CCTE_Headload_dcsnos_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label35" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDCS_CCTE_Headload_dcsnos_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Transportation Expenses Headload Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Transportation Expenses Headload Amount!'></i>"
                                                        ControlToValidate="txtDCS_CCTE_Headload_dcsnos_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtDCS_CCTE_Headload_dcsnos_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label36" Text="(ii) Vehicle(No.s)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label37" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDCS_CCTE_Vehicle_nos_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Transportation Expenses Vehicle Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Transportation Expenses Vehicle Unit!'></i>"
                                                        ControlToValidate="txtDCS_CCTE_Vehicle_nos_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtDCS_CCTE_Vehicle_nos_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label38" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDCS_CCTE_Vehicle_nos_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Transportation Expenses Vehicle Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Transportation Expenses Vehicle Amount!'></i>"
                                                        ControlToValidate="txtDCS_CCTE_Vehicle_nos_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtDCS_CCTE_Vehicle_nos_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>(13) Cattle Feed Transportation
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label40" Text="(i) Vehicle(No.)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label42" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCattle_FT_Vehicle_nos_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Transportation Vehicle Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Transportation Vehicle Unit!'></i>"
                                                        ControlToValidate="txtCattle_FT_Vehicle_nos_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCattle_FT_Vehicle_nos_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label43" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCattle_FT_Vehicle_nos_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Transportation Vehicle Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Transportation Vehicle Amount!'></i>"
                                                        ControlToValidate="txtCattle_FT_Vehicle_nos_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCattle_FT_Vehicle_nos_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label44" Text="(ii) Loading(Bag)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label45" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCattle_FT_Loading_nos_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Transportation Loading Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Transportation Loading Unit!'></i>"
                                                        ControlToValidate="txtCattle_FT_Loading_nos_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCattle_FT_Loading_nos_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label53" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCattle_FT_Loading_nos_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Transportation Loading Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Transportation Loading Amount!'></i>"
                                                        ControlToValidate="txtCattle_FT_Loading_nos_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCattle_FT_Loading_nos_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label54" Text="(iii) Unloading(Bag)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label55" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCattle_FT_UnLoading_nos_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Transportation Unloading Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Transportation Unloading Unit!'></i>"
                                                        ControlToValidate="txtCattle_FT_UnLoading_nos_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCattle_FT_UnLoading_nos_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label57" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCattle_FT_UnLoading_nos_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Cattle Feed Transportation Unloading Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle Feed Transportation Unloading Amount!'></i>"
                                                        ControlToValidate="txtCattle_FT_UnLoading_nos_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtCattle_FT_UnLoading_nos_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </fieldset>



                            <fieldset>
                                <legend>(14) Expenditures
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" Text="(i) Electricity(KWh)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label32" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Electricity_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Electricity Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Electricity Unit!'></i>"
                                                        ControlToValidate="txtExpend_Electricity_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtExpend_Electricity_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label39" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Electricity_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Electricity Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Electricity Amount!'></i>"
                                                        ControlToValidate="txtExpend_Electricity_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtExpend_Electricity_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label58" Text="(ii) Diesel(Ltrs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label59" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Deisel_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Diesel Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Diesel Unit!'></i>"
                                                        ControlToValidate="txtExpend_Deisel_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtExpend_Deisel_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label60" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Deisel_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Diesel Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Diesel Amount!'></i>"
                                                        ControlToValidate="txtExpend_Deisel_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" ID="txtExpend_Deisel_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label88" Text="(iii) Chemical" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label73" Text="(a) Acid(Lrts)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label86" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Che_acid_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Chemical Acid Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Chemical Acid Unit!'></i>"
                                                        ControlToValidate="txtExpend_Che_acid_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)"  ID="txtExpend_Che_acid_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label87" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Che_acid_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Chemical Acid Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Chemical Acid Amount!'></i>"
                                                        ControlToValidate="txtExpend_Che_acid_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control"  onkeypress="return validateDec(this, event)" ID="txtExpend_Che_acid_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label89" Text="(b) Alcohol(Ltrs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label90" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Che_Alcohol_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Chemical Alcohol Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Chemical Alcohol Unit!'></i>"
                                                        ControlToValidate="txtExpend_Che_Alcohol_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Che_Alcohol_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label91" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Che_Alcohol_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Chemical Alcohol Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Chemical Alcohol Amount!'></i>"
                                                        ControlToValidate="txtExpend_Che_Alcohol_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Che_Alcohol_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label92" Text="(iv) Detergent" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 800;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <%-- <span class="pull-right">
                                               
                                            </span>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label93" Text=" (a) Soap Solution(Ltrs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label94" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Detgt_SS_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Soap Solution Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Soap Solution Unit!'></i>"
                                                        ControlToValidate="txtExpend_Detgt_SS_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Detgt_SS_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label95" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Detgt_SS_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Soap Solution Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Soap Solution Amount!'></i>"
                                                        ControlToValidate="txtExpend_Detgt_SS_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Detgt_SS_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label96" Text="(b) Caustic Soda(Kgs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label97" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Detgt_CS_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Caustic Soda Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Caustic Soda Unit!'></i>"
                                                        ControlToValidate="txtExpend_Detgt_CS_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Detgt_CS_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label98" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Detgt_CS_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Caustic Soda Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Caustic Soda Amount!'></i>"
                                                        ControlToValidate="txtExpend_Detgt_CS_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Detgt_CS_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label99" Text="(c) Washing Soda(Kgs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label100" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Detgt_WS_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Washing Soda Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Washing Soda Unit!'></i>"
                                                        ControlToValidate="txtExpend_Detgt_WS_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Detgt_WS_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label101" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Detgt_WS_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Washing Soda Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Washing Soda Amount!'></i>"
                                                        ControlToValidate="txtExpend_Detgt_WS_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Detgt_WS_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label102" Text="(v) Contract Labour(Nos)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label103" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_CLabour_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Contract Labour Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contract Labour Unit!'></i>"
                                                        ControlToValidate="txtExpend_CLabour_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_CLabour_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label104" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_CLabour_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Contract Labour Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Contract Labour Amount!'></i>"
                                                        ControlToValidate="txtExpend_CLabour_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_CLabour_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label105" Text="(vi) Security(Nos)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label106" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Security_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Security Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Security Unit!'></i>"
                                                        ControlToValidate="txtExpend_Security_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Security_Unit" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label107" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Security_AMT" ValidationGroup="a"
                                                        ErrorMessage="Enter Security Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Security Amount!'></i>"
                                                        ControlToValidate="txtExpend_Security_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this, event)" ID="txtExpend_Security_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label108" Text="(vii) Stationery" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right"></span>
                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label109" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Stationary_Unit" ValidationGroup="a"
                                                        ErrorMessage="Enter Stationery Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stationery Unit!'></i>"
                                                        ControlToValidate="txtExpend_Stationary_Unit" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" ID="txtExpend_Stationary_Unit" onkeypress="return validateDec(this, event)" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label110" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvExpend_Stationary_AMT" ValidationGroup="a"
                                                        E ErrorMessage="Enter Stationery Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Stationery Amount!'></i>"
                                                        ControlToValidate="txtExpend_Stationary_AMT" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" Text="0" autocomplete="off" CssClass="form-control" ID="txtExpend_Stationary_AMT" onkeypress="return validateDec(this, event)" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <fieldset>
                                        <legend>Other Expenditures
                                        </legend>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div style="float: right">
                                                    <%--  <asp:Label ID="Label2" runat="server" Text="You can add maximum 10 rows" Style="color: #ff0000"></asp:Label>--%>
                                                    <asp:Button ID="btnAddrow" runat="server"  Text="+ Add New Row" CssClass="btn btn-block btn-primary" OnClick="btnAddrow_Click" Width="150px" Style="padding: 5px; font-size: 14px" />
                                                </div>

                                                <asp:GridView runat="server" ID="gvOtherexpenditure" CssClass="table table-bordered" OnDataBound="gvOtherexpenditure_DataBound" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowDeleting="gvOtherexpenditure_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                <asp:Label ID="lblsno" Visible="false" Text='<%# Eval("Sno").ToString()%>' runat="server" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Expenditure Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExpendditure_name" Text='<%# Eval("Expendditure_name").ToString()%>' runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvExpendditure_name" ValidationGroup="a"
                                                                    ErrorMessage="Enter Expenditure Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Expenditure Name !'></i>"
                                                                    ControlToValidate="txtExpendditure_name" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Unit(No.)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtUnit" Text='<%# Eval("Unit").ToString()%>' onkeypress="return validateDec(this, event)" runat="server" Height="10%" class="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvUnit" ValidationGroup="a"
                                                                    ErrorMessage="Enter Expenditure Unit" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Expenditure Unit !'></i>"
                                                                    ControlToValidate="txtUnit" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount(Rs)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" Text='<%# Eval("Amount").ToString()%>' onkeypress="return validateDec(this, event)" runat="server" Height="10%" class="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvAmount" ValidationGroup="a"
                                                                    ErrorMessage="Enter Expenditure Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Expenditure Amount !'></i>"
                                                                    ControlToValidate="txtAmount" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--  <asp:TemplateField HeaderText="Amount(Rs)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" Text='<%# Eval("Amount").ToString()%>' onkeypress="return validateDec(this, event)" runat="server" class="form-control" />
                                                        <asp:RequiredFieldValidator ID="rfvAmount" ValidationGroup="a"
                                                            ErrorMessage="Enter Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Amount !'></i>"
                                                            ControlToValidate="txtAmount" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderStyle-Width="50px" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="Delete" runat="server" Width="50%" CausesValidation="False" ImageUrl="~/mis/image/Del.png" CommandName="Delete" OnClientClick="return confirm('The item will be deleted. Are you sure want to continue?');"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>


                                            </div>

                                        </div>
                                    </fieldset>
                                </div>
                            </fieldset>
                        </div>



                    </fieldset>

                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server"  CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" AccessKey="S" />
                                <asp:Button runat="server"  CssClass="btn btn-block btn-primary" Visible="false" ValidationGroup="a" ID="btnupdate" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S" OnClick="btnupdate_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="Monthly_Chilling_center_report_bymanager.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body">
                <div class="box-header">
                    <h3 class="box-title">Monthly Chilling Centre Report  Detail</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                          <div class="col-md-12" >
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label111" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label112" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                         <asp:Button runat="server" BackColor="#2e9eff" CssClass="btn btn-success" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvMCCRdetail" Visible="false" CssClass="table table-bordered" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowCommand="gvMCCRdetail_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <%--  <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MCCR_ID").ToString()%>' runat="server" />


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# Eval("Year").ToString()%>' runat="server" class="form-control"></asp:Label>


                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# Eval("month_name").ToString()%>' runat="server" class="form-control"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Creation Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedAt" Text='<%# Eval("CreatedAt").ToString()%>' runat="server" class="form-control"></asp:Label>

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
                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" CausesValidation="False" ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("MCCR_ID") %>'></asp:ImageButton>
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

        <%--       function abc() {
            debugger;
            var SC_milk_for_prod_Qty = document.getElementById('<%=txtSC_milk_for_prod_Qty.ClientID%>').value;
            var Milk_dispatch_dairy_Qty = document.getElementById('<%=txtMilk_dispatch_dairy_Qty.ClientID%>').value;
            var CB_M_Qty = document.getElementById('<%=txtCB_M_Qty.ClientID%>').value;
            var OB_M_Qty = document.getElementById('<%=txtOB_M_Qty.ClientID%>').value;
            var Milk_pur_good_Qty = document.getElementById('<%=txtMilk_pur_good_Qty.ClientID%>').value;
            var Milk_pur_Sour_Qty = document.getElementById('<%=txtMilk_pur_Sour_Qty.ClientID%>').value;
            var Milk_pur_Curdle_Qty = document.getElementById('<%=txtMilk_pur_Curdle_Qty.ClientID%>').value;



            var dat = { "SC_milk_for_prod_Qty": SC_milk_for_prod_Qty, "Milk_dispatch_dairy_Qty": Milk_dispatch_dairy_Qty, "CB_M_Qty": CB_M_Qty, "OB_M_Qty": OB_M_Qty, "Milk_pur_good_Qty": Milk_pur_good_Qty, "Milk_pur_Sour_Qty": Milk_pur_Sour_Qty, "Milk_pur_Curdle_Qty": Milk_pur_Curdle_Qty }
            $.ajax(
            {

                url: "Monthly_Chilling_center_report_bymanager_CC.aspx/Getcal1",
                type: 'POST',
                data: JSON.stringify(dat),
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    // alert(data.d)
                    //$("#divMyText").html(data.d);
                    //$("#divMyText1").html(data.d);
                    $("#txtMilk_Hand_variation_Q_Qty").text(data.d);



                },
                error: function () {
                    alert("error");
                }
            });
        };--%>

        function cal() {
            
            var num3 = document.getElementById('<%=txtSC_milk_for_prod_Qty.ClientID%>');
            var num4 = document.getElementById('<%=txtMilk_dispatch_dairy_Qty.ClientID%>');
            var num5 = document.getElementById('<%=txtCB_M_Qty.ClientID%>');
            var num1 = document.getElementById('<%=txtOB_M_Qty.ClientID%>');
            var num21 = document.getElementById('<%=txtMilk_pur_good_Qty.ClientID%>');
            var num22 = document.getElementById('<%=txtMilk_pur_Sour_Qty.ClientID%>');
            var num23 = document.getElementById('<%=txtMilk_pur_Curdle_Qty.ClientID%>');

            var num61 = document.getElementById('<%=txtWhite_butter_SC_OB_Qty.ClientID%>');
            var num62 = document.getElementById('<%=txtWhite_butter_SC_WBM_Qty.ClientID%>');
            var num63 = document.getElementById('<%=txtWhite_butter_SC_CB_Qty.ClientID%>');

            
            var num9 = document.getElementById('<%=txtTanker_milk_Rec_DP_Qty.ClientID%>');
           // var num4 = document.getElementById('<%=txtMilk_dispatch_dairy_Qty.ClientID%>');
           <%-- var num22 = document.getElementById('<%=txtMilk_pur_Sour_Qty.ClientID%>');
            var num23 = document.getElementById('<%=txtMilk_pur_Curdle_Qty.ClientID%>');--%>
          
            var a7 = 0, b7 = 0, a8 = 0, b8 = 0, a10 = 0, b10 = 0;
           
            a7 = ((parseFloat(num3.value) + parseFloat(num4.value) + parseFloat(num5.value)) - (parseFloat(num1.value) + parseFloat(num21.value) + parseFloat(num22.value) + parseFloat(num23.value))).toFixed(2);
            document.getElementById('<%=txtMilk_Hand_variation_Q_Qty.ClientID%>').value = a7;

            b7 = (a7*100 /(parseFloat(num1.value) + parseFloat(num21.value) + parseFloat(num22.value) + parseFloat(num23.value))).toFixed(2);
            document.getElementById('<%=txtMilk_Hand_variation_Per_Qty.ClientID%>').value = b7;



            a8 = ((parseFloat(num62.value) + parseFloat(num63.value)) - (parseFloat(num3.value) + parseFloat(num61.value))).toFixed(2);
            document.getElementById('<%=txtProduct_mfg_variation_Q_Qty.ClientID%>').value = a8;

            b8 = (a8 * 100 / (parseFloat(num3.value) + parseFloat(num61.value))).toFixed(2);
            document.getElementById('<%=txtProduct_mfg_variation_Per_Qty.ClientID%>').value = b8;



            a10 = (parseFloat(num9.value) - parseFloat(num4.value)).toFixed(2);
            document.getElementById('<%=txtTanker_milk_variation_Q_Qty.ClientID%>').value = a10;

            b10 = (a10 * 100 / (parseFloat(num4.value))).toFixed(2);
             document.getElementById('<%=txtTanker_milk_variation_Per_Qty.ClientID%>').value = b10;

            return true;
        };
       
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

