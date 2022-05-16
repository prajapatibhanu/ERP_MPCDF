<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMCEntryAtQC.aspx.cs" Inherits="mis_MilkCollection_BMCEntryAtQC" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>  
                        <div class="col-md-2">
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTime" runat="server" Display="Dynamic" ControlToValidate="txtTime" Text="<i class='fa fa-exclamation-circle' title='Enter Time!'></i>" ErrorMessage="Enter Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        </span>
                                        <div class="form-group">
                                            <label>Tanker Arrival Time<span style="color: red;">*</span></label>
                                            <div class="input-group bootstrap-timepicker timepicker">
                                                <asp:TextBox ID="txtTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                            </div>
                                        </div>
                                    </div>                    
                        <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root<span style="color: red;">*</span></label>
                                       <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvBMCRoot" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>
                          
                    <div class="row">
                       
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="updatepnl" runat="server">  
                                    <ContentTemplate>  
                                        <asp:GridView ID="gvBMCDetails" CssClass="table table-bordered"  AutoGenerateColumns="false" runat="server" OnRowDataBound="gvBMCDetails_RowDataBound">
                                    
                                     <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" Visible="false" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"/>
                                                <%--<asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>--%>
                                            <%--<asp:Label ID="lblType" CssClass="hidden" runat="server" Text='<%# Eval("Type") %>'></asp:Label>--%>
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name_E" runat="server" Text='<%# Eval("Office_Name_E") %>'></asp:Label>
                                                 <asp:Label ID="lblOffice_ID" CssClass="hidden" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                               <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvTankerNo"  runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlTankerNo" Text="<i class='fa fa-exclamation-circle' title='Select Tanker No!'></i>" ErrorMessage="Select Tanker No" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                                 <asp:DropDownList ID="ddlTankerNo"  Visible="false" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Temp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Temp" runat="server" Text='<%# Eval("V_Temp") %>'></asp:Label>
                                                 <span class="pull-right">
                                                <asp:RequiredFieldValidator  ID="rfvTemp" runat="server" Display="Dynamic" ControlToValidate="txtV_Temp" Text="<i class='fa fa-exclamation-circle' title='Enter Temp!'></i>" ErrorMessage="Enter Temp" Enabled="false" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                                <asp:TextBox ID="txtV_Temp" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("D_MilkQuantity") %>'></asp:Label>
                                                <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQty" runat="server" Display="Dynamic" Enabled="false" ControlToValidate="txtD_MilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                                 <asp:TextBox ID="txtD_MilkQuantity" onkeypress="return validateDec(this,event)" Visible="false" runat="server"  CssClass="form-control" OnTextChanged="txtD_MilkQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                 <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvFAT" runat="server" Display="Dynamic" Enabled="false" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Enter FAT!'></i>" ErrorMessage="Enter FAT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                                <asp:TextBox ID="txtFAT" onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtFAT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" Enabled="false" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                                 <asp:TextBox ID="txtCLR" onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtCLR_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label> 
                                                <asp:TextBox ID="txtSNF" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kg Fat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKgFat" runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                <asp:TextBox ID="txtKgFat" Enabled="false" Visible="false" runat="server" CssClass="form-control" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kg SNF">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKgSNF" runat="server" Text='<%# Eval("KgSNF") %>'></asp:Label>
                                                <asp:TextBox ID="txtKgSNF" Enabled="false" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type" Visible="false">
                                            <ItemTemplate>
                                               <asp:Label ID="lblType"  runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div id="divdetail" runat="server">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>Other Detail</legend>
                                              <div class="col-md-2">
                                                  <div class="form-group">
                                                      <label>Oral Test<span style="color: red;">*</span></label>
                                                      <span class="pull-right">
                                                      <asp:RequiredFieldValidator ID="rfvddlOption" runat="server" Display="Dynamic"
                                                        ControlToValidate="ddlOption" InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Oral Test!'></i>"
                                                        ErrorMessage="Select Oral Test" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="a" ></asp:RequiredFieldValidator>
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
                                                        ValidationGroup="a" InitialValue="0" Enabled="true"></asp:RequiredFieldValidator>
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
                                                        ControlToValidate="ddlCOB"
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
                                            </fieldset>
                                        </div>
                                    </div>
                                        <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" ValidationGroup="a" Visible="false" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();"/>
                                    </div>
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
            <asp:HiddenField id="HiddenField1" runat="server" ClientIDMode="Static"/>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
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

