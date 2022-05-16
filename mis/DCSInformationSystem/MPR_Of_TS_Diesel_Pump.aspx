<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MPR_Of_TS_Diesel_Pump.aspx.cs" Inherits="mis_DCSInformationSystem_MPR_Of_TS_Diesel_Pump" %>

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
                    <h3 class="box-title">Monthly Progress Report Of Transport Section</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                             <asp:Label ID="lblDP_id" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>(a) Diesel Pump
                        </legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="rfvDDlMonth" ValidationGroup="a"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
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
                                        <asp:Label ID="Label20" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control"  >
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
                                         <asp:Label ID="lblOpeningBalance" Text="(1) Opening Balance " Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                         <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>--%>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="Label41" Text="Quantity(Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvOB_Quantity" ValidationGroup="a"
                                            ErrorMessage="Enter Opening Balance Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Balance Quantity !'></i>"
                                            ControlToValidate="txtOB_Quantity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                         </span>
                                         <asp:TextBox runat="server"  Text="0" onchange="calopen()"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOB_Quantity" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="Label46" Text="Rate(Rs/Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvOB_rate" ValidationGroup="a"
                                            ErrorMessage="Enter Opening Balance Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Opening Balance Rate !'></i>"
                                            ControlToValidate="txtOB_rate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                         </span>
                                         <asp:TextBox runat="server" Text="0" onchange="calopen()"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOB_rate" MaxLength="150" placeholder="ruppes/Ltr" ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3">
                                     <div class="form-group">
                                         <asp:Label ID="Label47" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                         <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="rfvOB_AMT" ValidationGroup="a"
                                            ErrorMessage="Enter Opening Balance Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter  Opening Balance Amount !'></i>"
                                            ControlToValidate="txtOB_AMT" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                         <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                         </span>
                                         <asp:TextBox runat="server"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtOB_AMT" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                     </div>
                                 </div>
                             </div>


                            <fieldset>
                                <legend>(2) Purchase During Month
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div style="float: right">
                                            <%--  <asp:Label ID="Label2" runat="server" Text="You can add maximum 10 rows" Style="color: #ff0000"></asp:Label>--%>
                                            <asp:Button ID="btnAddrow" runat="server" Text="+ Add New Row" CssClass="btn btn-block btn-primary" OnClick="btnAddrow_Click" Width="150px" Style="padding: 5px; font-size: 14px" />
                                        </div>

                                        <asp:GridView runat="server" ID="gvPurchaseDuringMonth" CssClass="table table-bordered" OnDataBound="gvPurchaseDuringMonth_DataBound" ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnRowDeleting="gvPurchaseDuringMonth_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <asp:Label ID="lblsno" Visible="false" Text='<%# Eval("Sno").ToString()%>' runat="server" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date(DD-MM-YYYY)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPurchase_date" Text='<%# Eval("Purchase_date").ToString()%>' runat="server" placeholder="DD-MM-YYYY" class="form-control" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPurchase_date" ValidationGroup="a"
                                                            ErrorMessage="Enter  date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter date !'></i>"
                                                            ControlToValidate="txtPurchase_date" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator >
                                                         <asp:RegularExpressionValidator  ID="rEvPurchase_date" ValidationGroup="a"
                                                            ErrorMessage="Enter date in correct formate" ValidationExpression="(0?[1-9]|[12][0-9]|3[01])-(0?[1-9]|1[012])-((19|20|21)[0-9][0-9])" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date in correct formate !'></i>"
                                                            ControlToValidate="txtPurchase_date" Display="Dynamic" runat="server">

                                                         </asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Quantity(Ltr)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Text='<%# Eval("Quantity").ToString()%>' onchange="Calculationingrid()" onkeypress="return validateDec(this, event)" runat="server" Height="10%" class="form-control" ClientIDMode="Static"/>
                                                        <asp:RequiredFieldValidator ID="rfvQuantity" ValidationGroup="a"
                                                            ErrorMessage="Enter Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                            ControlToValidate="txtQuantity" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate(Rs/Ltr)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRate" Text='<%# Eval("Rate").ToString()%>' onchange="Calculationingrid()" onkeypress="return validateDec(this, event)" runat="server" Height="10%" class="form-control" ClientIDMode="Static" />
                                                        <asp:RequiredFieldValidator ID="rfvRate" ValidationGroup="a"
                                                            ErrorMessage="Enter Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Rate !'></i>"
                                                            ControlToValidate="txtRate" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount(Rs)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" Text='<%# Eval("Amount").ToString()%>' onkeypress="return validateDec(this, event)" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                      <%--  <asp:RequiredFieldValidator ID="rfvAmount" ValidationGroup="a"
                                                            ErrorMessage="Enter Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Amount !'></i>"
                                                            ControlToValidate="txtAmount" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label61" Text="(3) Issued to Own Vehicles" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" Text="Quantity(Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvITOV_Quantity" ValidationGroup="a"
                                            ErrorMessage="Enter Issued to Own Vehicles Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Own Vehicles Quantity !'></i>"
                                            ControlToValidate="txt_ITOV_Quantity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="calIOW()" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txt_ITOV_Quantity" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" Text="Rate(Rs/Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="rfvITOV_Rate" ValidationGroup="a"
                                            ErrorMessage="Enter Issued to Own Vehicles Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Own Vehicles Rate !'></i>"
                                            ControlToValidate="txt_ITOV_Rate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="calIOW()" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txt_ITOV_Rate" MaxLength="150" placeholder="rupees/Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvITOV_AMT" ValidationGroup="a"
                                            ErrorMessage="Enter Issued to Own Vehicles Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Own Vehicles Amount !'></i>"
                                            ControlToValidate="txt_ITOV_AMT" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server"   autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txt_ITOV_AMT" MaxLength="150"  ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label65" Text="(4) Issued to Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" Text="Quantity(Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvITO_Quantity" ValidationGroup="a"
                                            ErrorMessage="Enter Issued to Others Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Others Quantity !'></i>"
                                            ControlToValidate="txt_ITO_Quantity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="calIO()" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txt_ITO_Quantity" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" Text="Rate(Rs/Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter Issued to Others Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Others Rate !'></i>"
                                            ControlToValidate="txt_ITO_Rate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- 
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="calIO()"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txt_ITO_Rate" MaxLength="150" placeholder="rupees/Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                          <asp:RequiredFieldValidator ID="rfvITO_AMT" ValidationGroup="a"
                                            ErrorMessage="Enter Issued to Others Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Issued to Others Amount !'></i>"
                                            ControlToValidate="txt_ITO_AMT" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%-- 
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server"   autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txt_ITO_AMT" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label69" Text="(5) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter Name" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" Text="Quantity(Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="rfvCBQuantity" ValidationGroup="a"
                                            ErrorMessage="Enter Closing Balance Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance Quantity !'></i>"
                                            ControlToValidate="txtCB_Quantity" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" Text="0" onchange="calClose()"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtCB_Quantity"  placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" Text="Rate(Rs/Ltr)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           
                                           <asp:RequiredFieldValidator ID="rfvCB_Rate" ValidationGroup="a"
                                            ErrorMessage="Enter Closing Balance Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance Rate !'></i>"
                                            ControlToValidate="txtCB_Rate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server" Text="0"  onchange="calClose()" autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtCB_Rate"  placeholder="rupees/Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                         
                                           <asp:RequiredFieldValidator ID="rfvCB_AMT" ValidationGroup="a"
                                            ErrorMessage="Enter Closing Balance Amount" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance Amount !'></i>"
                                            ControlToValidate="txtCB_AMT" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:TextBox runat="server"   autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtCB_AMT" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



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
                                    <a class="btn btn-block btn-default" href="MPR_Of_TS_Diesel_Pump.aspx">Clear</a>
                                </div>
                            </div>
                        </div>

                    </fieldset>


                </div>
            </div>
            <!-- /.box-body -->
             <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title"> Diesel Pump Detail</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body" id="divdetail" runat="server">
                    <div class="table-responsive">
                        <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:Label ID="Label2" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                        <asp:GridView runat="server" ID="gvDPdetail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false"  OnRowCommand="gvDPdetail_RowCommand" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                           <%--  <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />--%>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false" >
                                                        <ItemTemplate>
                                                               <asp:Label Visible="false" ID="lblMPR_DP_Id" Text='<%# Eval("MPR_DP_Id").ToString()%>' runat="server" />

                                                     
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
                                                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("MPR_DP_Id") %>'></asp:ImageButton>
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
        function Calculationingrid() {
            debugger;
            var grid = document.getElementById("<%= gvPurchaseDuringMonth.ClientID%>");  
           // $('#gvPurchaseDuringMonth tr').each(function (index) {
               // var gdrows = grid.getElementsByTagName("tr");
                for (var i = 0; i < grid.rows.length - 1; i++) {

                    //var txtQuantity = $grid.rows[i].txtQuantity.val();
                    //var txtRate = <%#((GridViewRow)Container).FindControl("txtStartDate").ClientID %>'

                    var txtQuantity = $("input[id*=txtQuantity]");
                    var txtRate = $("input[id*=txtRate]");
                    var txtAmount = $("input[id*=txtAmount]");
                
                    var Amount = "0";
                    Amount = (parseFloat(txtQuantity[i].value) * parseFloat(txtRate[i].value)).toFixed(2);
                    txtAmount[i].value = Amount;
                    //$(("input[id*=txtAmount]").val(Amount));
                  
                }  
        }
        
       // )}
        
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
        function calgrid() {

            var QTY = document.getElementById('<%=txtOB_Quantity.ClientID%>');
            var Rate = document.getElementById('<%=txtOB_rate.ClientID%>');

            var Amount = 0;

            Amount = (parseFloat(QTY.value) * parseFloat(Rate.value)).toFixed(2);
            document.getElementById('<%=txtOB_AMT.ClientID%>').value = Amount;


            return true;
        }
        function calopen() {

            var QTY = document.getElementById('<%=txtOB_Quantity.ClientID%>');
            var Rate = document.getElementById('<%=txtOB_rate.ClientID%>');
           
            var Amount = 0;

            Amount = (parseFloat(QTY.value) * parseFloat(Rate.value)).toFixed(2);
            document.getElementById('<%=txtOB_AMT.ClientID%>').value = Amount;


            return true;
        }

        function calIOW() {

            var QTY = document.getElementById('<%=txt_ITOV_Quantity.ClientID%>');
            var Rate = document.getElementById('<%=txt_ITOV_Rate.ClientID%>');

            var Amount = 0;

            Amount = (parseFloat(QTY.value) * parseFloat(Rate.value)).toFixed(2);
            document.getElementById('<%=txt_ITOV_AMT.ClientID%>').value = Amount;


            return true;
        }

        function calIO() {

            var QTY = document.getElementById('<%=txt_ITO_Quantity.ClientID%>');
            var Rate = document.getElementById('<%=txt_ITO_Rate.ClientID%>');

            var Amount = 0;

            Amount = (parseFloat(QTY.value) * parseFloat(Rate.value)).toFixed(2);
            document.getElementById('<%=txt_ITO_AMT.ClientID%>').value = Amount;


            return true;
        }

        function calClose() {

            var QTY = document.getElementById('<%=txtCB_Quantity.ClientID%>');
            var Rate = document.getElementById('<%=txtCB_Rate.ClientID%>');

            var Amount = 0;

            Amount = (parseFloat(QTY.value) * parseFloat(Rate.value)).toFixed(2);
            document.getElementById('<%=txtCB_AMT.ClientID%>').value = Amount;


            return true;
        };




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

