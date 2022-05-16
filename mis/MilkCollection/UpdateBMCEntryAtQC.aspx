<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateBMCEntryAtQC.aspx.cs" Inherits="mis_MilkCollection_UpdateBMCEntryAtQC" MaintainScrollPositionOnPostback="true" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
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
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">BMC/DCS Milk Collection Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row noprint">


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Reference No.<span style="color: red;"> *</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select Reference No" Text="<i class='fa fa-exclamation-circle' title='Select Reference No !'></i>"
                                                ControlToValidate="ddlReferenceNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlReferenceNo" Width="100%" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlReferenceNo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Tanker Arrival Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtArrivalDate" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Arrival Date!'></i>" ErrorMessage="Enter Tanker Arrival Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtArrivalDate" Enabled="false" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click"/>
                            </div>
                        </div>--%>
                            </div>
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="updatepnl" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvBMCDetails" CssClass="table table-bordered" AutoGenerateColumns="false" runat="server" OnRowDataBound="gvBMCDetails_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%-- <asp:CheckBox ID="chkSelect" Visible="false" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"/>--%>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Eval("RowVisible").ToString()=="Yes"?(Container.DataItemIndex +1).ToString():"" %>'></asp:Label>
                                                                <%-- <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID") %>'></asp:Label>--%>
                                                                <asp:Label ID="lblType" CssClass="hidden" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Name_E" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Temp">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Temp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvTemp" runat="server" Display="Dynamic" ControlToValidate="txtV_Temp" Text="<i class='fa fa-exclamation-circle' title='Enter Temp!'></i>" ErrorMessage="Enter Temp" Enabled="true" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtV_Temp" Visible="false" Text='<%# Eval("Temp") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                                <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQty" runat="server" Display="Dynamic" Enabled="true" ControlToValidate="txtD_MilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>--%>
                                                                <asp:TextBox ID="txtD_MilkQuantity" Text='<%# Eval("Quantity") %>' onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtD_MilkQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvFAT" runat="server" Display="Dynamic" Enabled="true" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Enter FAT!'></i>" ErrorMessage="Enter FAT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtFAT" Text='<%# Eval("FAT") %>' onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtFAT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" Enabled="false" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Minimum CLR required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtCLR" Text='<%# Eval("CLR") %>' onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtCLR_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                                <asp:TextBox ID="txtSNF" Text='<%# Eval("SNF") %>' Enabled="true" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kg Fat">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKgFat" runat="server" Text='<%# Eval("FatKg") %>'></asp:Label>
                                                                <asp:TextBox ID="txtKgFat" Enabled="false" Text='<%# Eval("FatKg") %>' Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kg SNF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKgSNF" runat="server" Text='<%# Eval("SnfKg") %>'></asp:Label>
                                                                <asp:TextBox ID="txtKgSNF" Enabled="false" Text='<%# Eval("SnfKg") %>' Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                <div id="divdetail" runat="server" visible="false">
                                                    <div class="col-md-12">
                                                        <fieldset>
                                                            <legend>Other Detail</legend>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Chamber<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlCompartmentType" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlCompartmentType" InitialValue="0"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>"
                                                                            ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlCompartmentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Oral Test<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlOption" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlOption" InitialValue="0"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Oral Test!'></i>"
                                                                            ErrorMessage="Select Oral Test" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlOption" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>COB<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvddlCOB" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlCOB"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select COB!'></i>"
                                                                            ErrorMessage="Select COB" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlCOB" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Acidity<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvAcidity" runat="server" Display="Dynamic"
                                                                            ControlToValidate="txtAcidity"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Enter Acidity!'></i>"
                                                                            ErrorMessage="Enter Acidity" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:TextBox ID="txtAcidity" runat="server" CssClass="form-control">                                                    
                                                                    </asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Urea<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvUrea" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlUrea"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Urea!'></i>"
                                                                            ErrorMessage="Select Urea" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlUrea" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Neutralizer<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvNeutralizer" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlNeutralizer"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Neutralizer!'></i>"
                                                                            ErrorMessage="Select Neutralizer" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlNeutralizer" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Maltodextrin<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvMaltodextrin" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlMaltodextrin"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Maltodextrin!'></i>"
                                                                            ErrorMessage="Select Maltodextrin" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlMaltodextrin" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Glucose<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvGlucose" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlGlucose"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Glucose!'></i>"
                                                                            ErrorMessage="Select Glucose" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlGlucose" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Sucrose<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvSucrose" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlSucrose"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Sucrose!'></i>"
                                                                            ErrorMessage="Select Sucrose" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlSucrose" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Salt<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvSalt" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlSalt"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Salt!'></i>"
                                                                            ErrorMessage="Select Salt" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlSalt" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Starch<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvStarch" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlStarch"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Starch!'></i>"
                                                                            ErrorMessage="Select Starch" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlStarch" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Detergent<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvDetergent" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlDetergent"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Detergent!'></i>"
                                                                            ErrorMessage="Select Detergent" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlDetergent" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Nitrate Test<span style="color: red;">*</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="rfvNitrateTest" runat="server" Display="Dynamic"
                                                                            ControlToValidate="ddlNitrateTest"
                                                                            Text="<i class='fa fa-exclamation-circle' title='Select Nitrate Test!'></i>"
                                                                            ErrorMessage="Select Nitrate Test" SetFocusOnError="true" ForeColor="Red"
                                                                            ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlNitrateTest" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                                        <asp:ListItem Value="Negative" Selected="True">Negative</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnAdd" runat="server" Visible="false" Style="margin-top: 20px;" ValidationGroup="Add" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Chamber Type" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblChamberType" runat="server" Text='<%# Eval("ChamberType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Oral Test" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOralTest" runat="server" Text='<%# Eval("OralTest") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="COB" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCOB" runat="server" Text='<%# Eval("COB") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Acidity" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAcidity" runat="server" Text='<%# Eval("Acidity") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Urea" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUrea" runat="server" Text='<%# Eval("Urea") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Neutralizer" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNeutralizer" runat="server" Text='<%# Eval("Neutralizer") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Maltodextrin" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMaltodextrin" runat="server" Text='<%# Eval("Maltodextrin") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Glucose" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGlucose" runat="server" Text='<%# Eval("Glucose") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sucrose" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSucrose" runat="server" Text='<%# Eval("Sucrose") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Salt" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSalt" runat="server" Text='<%# Eval("Salt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Starch" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStarch" runat="server" Text='<%# Eval("Starch") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Detergent" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDetergent" runat="server" Text='<%# Eval("Detergent") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="NitrateTest" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNitrateTest" runat="server" Text='<%# Eval("NitrateTest") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkQDDelete" runat="server" ToolTip="DeleteQDDetails" OnClick="lnkQDDelete_Click" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>

                                                    <div class="col-lg-12">
                                                        <fieldset>
                                                            <legend>Tanker Seal Details</legend>
                                                            <b>Tanker Seal Details</b>
                                                            <br />
                                                            <br />
                                                            <asp:GridView ID="gvTankerSealDetails" runat="server" AutoGenerateColumns="false" CssClass="table">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Seal Type" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealType" runat="server" Text='<%# Eval("V_SealType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Seal Location" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealLocation" runat="server" Text='<%# Eval("V_SealLocation") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seal Color" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealColor" runat="server" Text='<%# Eval("V_SealColor") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Seal Remark" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#a9a9a9" HeaderStyle-ForeColor="White">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealRemark" runat="server" Text='<%# Eval("V_SealRemark") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>





                                                                </Columns>
                                                            </asp:GridView>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Seal Type </label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="ddlSealtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Sale Type!'></i>" ErrorMessage="Select Sale Type." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlSealtype" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Old">Old</asp:ListItem>
                                                                        <asp:ListItem Value="Broken">Broken</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Chamber Type </label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="ddlChamberType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlChamberType" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Chamber">Chamber</asp:ListItem>
                                                                        <asp:ListItem Value="ValveBox">ValveBox</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Seal Color </label>
                                                                    <asp:RequiredFieldValidator ID="rfvSealColor" runat="server" Display="Dynamic" ControlToValidate="ddlSealColor" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlSealColor" OnInit="ddlSealColor_Init" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Seal No.</label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="c"
                                                                            ErrorMessage="Enter Seal No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Seal No !'></i>"
                                                                            ControlToValidate="txtV_SealNo" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9a-z-A-Z]+$" ValidationGroup="c" runat="server" ControlToValidate="txtV_SealNo" ErrorMessage="Enter Valid Seal Number." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Seal Number. !'></i>"></asp:RegularExpressionValidator>
                                                                    </span>
                                                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealNo" MaxLength="20" placeholder="Enter Seal No"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Seal Remark </label>
                                                                    <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealRemark" MaxLength="200" placeholder="Enter Seal Remark"></asp:TextBox>
                                                                </div>
                                                            </div>



                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnTankerSealDetails_Click" Style="margin-top: 20px;" ValidationGroup="c" ID="btnTankerSealDetails" Text="Add Seal" />
                                                                </div>
                                                            </div>

                                                            <hr />

                                                            <asp:GridView ID="gv_SealInfo" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Seal Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSealtype" runat="server" Text='<%# Eval("Sealtype") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Chamber Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblChamberType" runat="server" Text='<%# Eval("ChamberType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Seal Color">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealColor" runat="server" Text='<%# Eval("V_SealColor") %>'></asp:Label>
                                                                            <asp:Label ID="lblTI_SealColor" Visible="false" runat="server" Text='<%# Eval("TI_SealColor") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Seal No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealNo" runat="server" Text='<%# Eval("V_SealNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Seal Remark">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblV_SealRemark" runat="server" Text='<%# Eval("V_SealRemark") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="DeleteSeal" OnClick="lnkDelete_Click" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>

                                                        </fieldset>
                                                    </div>



                                                    <div class="row">

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>Seal Verification Documents [Panchnama]</label>
                                                                <div class="input-group">
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text">
                                                                            <i class="far fa-calendar-alt"></i>
                                                                        </span>
                                                                    </div>
                                                                    <asp:FileUpload runat="server" ID="FuSealVerificationfile" CssClass="form-control"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <fieldset id="fs_action" runat="server" visible="true">
                                                        <legend>Action</legend>
                                                        <div class="row">
                                                            <div class="col-md-1">
                                                                <div class="form-group">
                                                                    <asp:Button runat="server" Style="margin-top: 20px;" Enabled="false" CssClass="btn btn-block btn-primary" ValidationGroup="Save" ID="btnSave" OnClick="btnSave_Click" Text="Save" OnClientClick="return ValidatePage();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnClear" runat="server" Style="margin-top: 20px;" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-block btn-default" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>


                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function tabFocus(e) {
            document.getElementById("HiddenField1").value = e.id;
        }
    </script>
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtTime').timepicker();

    </script>


</asp:Content>

