<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSMilkReceive.aspx.cs" Inherits="mis_MilkCollection_DCSMilkReceive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="content-wrapper">

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

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Milk Receive</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Office Detail</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Block <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtBlock" CssClass="form-control" MaxLength="20" placeholder="Block" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>DateTime</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate"  onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shift</label>

                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>






                    <asp:UpdatePanel ID="updatepanel" runat="server">
                        <ContentTemplate>

                            <div class="row" runat="server" id="divselfMilkCollection">

                                <div class="col-lg-12">
                                    <fieldset>
                                        <legend>DCS Milk Collection Bifurcation</legend>

                                        <%--Cow--%>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>DCS<span style="color: red;">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="ddlDCS" Text="<i class='fa fa-exclamation-circle' title='Select DCS'></i>" ErrorMessage="Select DCS" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    </span>
                                                    <asp:DropDownList ID="ddlDCS" Width="100%" CssClass="form-control select2" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Type<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMilkType" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="txtMQty_Cow" Text="<i class='fa fa-exclamation-circle' title='Select Milk Type!'></i>" ErrorMessage="Select Milk Type!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                   
                                                </span>
                                                <asp:DropDownList ID="ddlMilkType"  CssClass="form-control select2" runat="server">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Buf">Buf</asp:ListItem>
                                                    <asp:ListItem Value="Cow">Cow</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:TextBox ID="txtMilkType_Cow" Enabled="false" Width="100%" CssClass="form-control" Text="Cow" runat="server" MaxLength="6"></asp:TextBox>--%>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quality<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlMQuality_Cow" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                    <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ControlToValidate="txtMQty_Cow" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtMQty_Cow" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtMQty_Cow" Width="100%" CssClass="form-control" onkeypress="return validateDec(this,event)" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Fat % (2.0 - 10)<span style="color: red;">*</span></label>
                                               <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic" ControlToValidate="txtFat_Cow" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtFat_Cow" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum FAT % required 2.0 and maximum 10." Display="Dynamic" ControlToValidate="txtFat_Cow" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 2.0 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="2.0" MaximumValue="10"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtFat_Cow" AutoPostBack="true" OnTextChanged="txtFat_Cow_TextChanged" onkeypress="return validateDec(this,event)" Width="100%" CssClass="form-control" onchange="GetMilkType()"  placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic" ControlToValidate="txtCLR_Cow" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtCLR_Cow" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Minimum CLR required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR_Cow" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR  required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtCLR_Cow" AutoPostBack="true" OnTextChanged="txtCLR_Cow_TextChanged" onkeypress="return validateDec(this,event)" Width="100%" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>SNF %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic" ControlToValidate="txtSNF_Cow" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtSNF_Cow" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSNF_Cow" Enabled="false" Width="100%" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Temperature (°C)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtTemp_Cow" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="txtTemp_Cow" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txtTemp_Cow" Text="4" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                        </div>
                                        <%--<div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Type<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtMilkType_Buff" Enabled="false" Width="100%" CssClass="form-control" Text="Buf" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quality<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlMQuality_Buff" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                    <asp:ListItem Value="Sour">Curd</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic" ControlToValidate="txtMQty_Buff" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtMQty_Buff" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtMQty_Buff" Width="100%" CssClass="form-control" onkeypress="return validateDec(this,event)" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Fat % (5.6 - 10)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic" ControlToValidate="txtFat_Buff" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtFat_Buff" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Minimum FAT % required 5.6 and maximum 10." Display="Dynamic" ControlToValidate="txtFat_Buff" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 5.6 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="5.6" MaximumValue="10"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtFat_Buff" AutoPostBack="true" OnTextChanged="txtFat_Buff_TextChanged" onkeypress="return validateDec(this,event)" Width="100%" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" ControlToValidate="txtCLR_Buff" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtCLR_Buff" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR_Buff" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtCLR_Buff" AutoPostBack="true" OnTextChanged="txtCLR_Buff_TextChanged" onkeypress="return validateDec(this,event)" Width="100%" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>SNF %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Display="Dynamic" ControlToValidate="txtSNF_Buff" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtSNF_Buff" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSNF_Buff" Enabled="false" Width="100%" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Temperature (°C)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txtTemp_Buf" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTemp_Buf" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txtTemp_Buf" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" Text="4" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                            </div>--%>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" ValidationGroup="a"  Text="Add" OnClick="btnAdd_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Society">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Type">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Quality">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Milk Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FAT">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblClr" runat="server" Text='<%# Eval("Clr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Temp">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>

                            <div class="row" runat="server" id="div1">

                                <div class="col-lg-12">
                                    <fieldset>
                                        <legend>Self Milk Collection Bifurcation</legend>

                                        <%--Cow--%>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Type<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="ddlSelfMilkType" Text="<i class='fa fa-exclamation-circle' title='Select DCS'></i>" ErrorMessage="Select Milk Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                </span>
                                                <asp:DropDownList ID="ddlSelfMilkType" Width="100%" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlSelfMilkType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Cow">Cow</asp:ListItem>
                                                    <asp:ListItem Value="Buf">Buf</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quality<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="ddlSelfMilKQuality" Text="<i class='fa fa-exclamation-circle' title='Select DCS'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                </span>
                                                <asp:DropDownList ID="ddlSelfMilKQuality" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                    <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtSelfMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtSelfMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSelfMilkQuantity" Width="100%" CssClass="form-control" onkeypress="return validateDec(this,event)" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Fat % (3.2 - 10)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtSelfFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtSelfFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtSelfFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtSelfFat" onkeypress="return validateDec(this,event)" Width="100%" OnTextChanged="txtSelfFat_TextChanged" ClientIDMode="Static" onchange="GetSelfMilkType()" AutoPostBack="true" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtSelfCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtSelfCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Minimum CLR required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtSelfCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtSelfCLR" onkeypress="return validateDec(this,event)" Width="100%" CssClass="form-control" OnTextChanged="txtSelfCLR_TextChanged" AutoPostBack="true" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>SNF %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtSelfSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtSelfSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSelfSNF" Enabled="false" Width="100%" CssClass="form-control" placeholder="" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                               <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Temperature (°C)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="txtSelfTemp" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtSelfTemp" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txtSelfTemp" Text="4" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>



                                    </fieldset>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Action</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" Enabled="true" CssClass="btn btn-primary" ValidationGroup="Save" ID="btnSubmit" Text="Submit" OnClientClick="return ValidatePage();" AccessKey="S" />
                                            &nbsp; &nbsp;<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>


                </div>
            </div>


            <div class="box box-success" runat="server" id="div_milkdetails">
                <div class="box-header">
                    <h3 class="box-title">Milk Collection Details</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Date</label>
                             <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                            <asp:TextBox ID="txtFilterDate" CssClass="form-control DateAdd" runat="server" OnTextChanged="txtFilterDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                        </div>
                    <div class="col-md-12">
                        <asp:GridView ID="GrdMilkCollectionDetails" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found.">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shift">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Society">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Type">
                                    <ItemTemplate>

                                        <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Quality">
                                    <ItemTemplate>

                                        <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FAT">
                                    <ItemTemplate>

                                        <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CLR">
                                    <ItemTemplate>

                                        <asp:Label ID="lblClr" runat="server" Text='<%# Eval("Clr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SNF">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </section>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function GetMilkType()
        {
            var Fat = document.getElementById('<%=txtFat_Cow.ClientID%>').value;
            if(Fat >= 2.0 && Fat <= 5.5)
            {
                document.getElementById('<%=ddlMilkType.ClientID%>').value = 'Cow'
            }
            if (Fat >= 5.6 && Fat <= 10) {
                document.getElementById('<%=ddlMilkType.ClientID%>').value = 'Buf'
            }
        }
        function GetSelfMilkType() {
            var Fat = document.getElementById('<%=txtSelfFat.ClientID%>').value;
            if (Fat >= 2.0 && Fat <= 5.5) {
                document.getElementById('<%=ddlSelfMilkType.ClientID%>').value = 'Cow'
            }
            if (Fat >= 5.6 && Fat <= 10) {
                document.getElementById('<%=ddlSelfMilkType.ClientID%>').value = 'Buf'
            }
        }
    </script>
</asp:Content>
