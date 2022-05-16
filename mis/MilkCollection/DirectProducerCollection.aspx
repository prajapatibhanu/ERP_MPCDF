<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DirectProducerCollection.aspx.cs" Inherits="mis_MilkCollection_DirectProducerCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Direct Milk Collection From Producer</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div id="div_timer"></div>
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
                                    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="add" ShowMessageBox="true" ShowSummary="false" />
                                </div>
                            </div>
                            <fieldset>
                                <legend>Office Details</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Dugdh Sangh <span style="color: red;">*</span></label><br />
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rq1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDS" InitialValue="0" ErrorMessage="Select Dugdh Sangh." Text="<i class='fa fa-exclamation-circle' title='Please Dugdh Sangh !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDS" runat="server" Width="100%" OnInit="ddlDS_Init" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lblOfficeTypeName" runat="server" Text="DCS"></asp:Label>
                                                <span style="color: red;">*</span></label><br />
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDCS" InitialValue="0" ErrorMessage="Select Dairy Corporative Society." Text="<i class='fa fa-exclamation-circle' title='Select Dairy Corporative Society !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDCS" runat="server" Width="100%" OnInit="ddlDCS_Init" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDate" ForeColor="Red" ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDate" ForeColor="Red" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtDate" placeholder="Select Date" MaxLength="10" CssClass="form-control" onkeypress="javascript: return false;" autocomplete="off" onpaste="return false;" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-end-date="0d" data-date-autoclose="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Time</label><span style="color: red;"> *</span>
                                        <div class="input-group bootstrap-timepicker timepicker">
                                            <asp:TextBox runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtTime" Text="09:30 AM"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shift</label><span style="color: red"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlshift" InitialValue="0" ErrorMessage="Select Shift." Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlshift" Width="100%" runat="server" OnInit="ddlshift_Init" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Member ID [Name]</label><span style="color: red"> *</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlMemberID" InitialValue="0" ErrorMessage="Select Member ID." Text="<i class='fa fa-exclamation-circle' title='Select Member ID !'></i>"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMemberID" Width="100%" runat="server" AutoPostBack="true" OnInit="ddlMemberID_Init" CssClass="form-control select2" OnSelectedIndexChanged="ddlMemberID_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Member Contact Number</label>
                                        <asp:TextBox runat="server" ReadOnly="true" placeholder="Member Contact Number" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtMemberName"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Milk Details</legend>

                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Milk Type</label><span style="color: red"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic" ValidationGroup="add" runat="server" ControlToValidate="ddlMilkType" InitialValue="0" ErrorMessage="Select Milk Type." Text="<i class='fa fa-exclamation-circle' title='Select Milk Type !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <br />
                                            <asp:DropDownList ID="ddlMilkType" Width="100%" CssClass="form-control select2" OnInit="ddlMilkType_Init" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Fat %</label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:CustomValidator ID="CV1" runat="server" ControlToValidate="txtFat" ClientValidationFunction="FatAcordingtoMilkType" ErrorMessage="Invalid FAT % for currect Milk type selection" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Invalid FAT % for currect Milk type selection. !'></i>" ValidationGroup="add"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" Display="Dynamic" ValidationGroup="add" runat="server" ControlToValidate="txtFat" ErrorMessage="Enter FAT %." Text="<i class='fa fa-exclamation-circle' title='Enter FAT %. !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <br />
                                            <asp:TextBox runat="server" placeholder="Enter Fat %" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtFat" onkeypress="return validateDec(this, event);" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CLR</label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:CustomValidator ID="CV2" runat="server" ControlToValidate="txtCLRSNF" ClientValidationFunction="AllowedCLR" ErrorMessage="Invalid CLR value allowed 24-30 only." SetFocusOnError="true" ForeColor="Red" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Invalid CLR value allowed 24-30 only. !'></i>" ValidationGroup="add"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="Red" Display="Dynamic" ValidationGroup="add" runat="server" ControlToValidate="txtCLRSNF" ErrorMessage="Enter CLR." Text="<i class='fa fa-exclamation-circle' title='Enter CLR. !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <br />
                                            <asp:TextBox runat="server" placeholder="Enter CLR" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtCLRSNF" onkeypress="return validateNum(event);" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Unit</label><span style="color: red"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="add" runat="server" ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Select Unit." Text="<i class='fa fa-exclamation-circle' title='Select Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <br />
                                            <asp:DropDownList ID="ddlUnit" Width="100%" CssClass="form-control select2" OnInit="ddlUnit_Init" runat="server" onchange="FillUnit()">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Milk Qty. (In
                                            <asp:Label ID="lblUnit" runat="server" Text="KG"></asp:Label>)</label><span style="color: red;"> *</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="add" runat="server" ControlToValidate="txtMilkQty" ErrorMessage="Enter Milk Qty." Text="<i class='fa fa-exclamation-circle' title='Enter Milk Qty. !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <br />
                                            <asp:TextBox runat="server" placeholder="Enter Milk Qty." CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtMilkQty" onkeypress="return validateDec(this, event);" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button ID="btnAdd" runat="server" ValidationGroup="add" Width="100%" Style="margin-top: 25px;" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="Pnl_mc_Temp_gv" runat="server">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gv_temp_CollectionDetail" ShowHeader="true" ShowFooter="true" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server" OnRowDataBound="gv_temp_CollectionDetail_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkType_id" runat="server" Visible="false" Text='<%# Eval("MilkType_id") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FAT %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFooter" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQty" runat="server" Text='<%# Eval("MilkQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMilkQty" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnit_id" runat="server" Visible="false" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate/Ltr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkCollectionRatePerKG" runat="server" Visible="false" Text='<%# Eval("MilkCollectionRatePerKG", "{0:0.00}") %>'></asp:Label>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RatePerLtr", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value (In Rs.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TotalValue", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalValue" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="pnl_mc_Main_gv" visible="false" runat="server">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gv_MC_Main" ShowHeader="true" ShowFooter="true" FooterStyle-BackColor="#eaeaea" DataKeyNames="DCSChild_Id" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server" OnRowDataBound="gv_temp_CollectionDetail_RowDataBound" OnRowCommand="gv_MC_Main_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDCSChild_Id" runat="server" Visible="false" Text='<%# Eval("DCSChild_Id") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkType_id" runat="server" Visible="false" Text='<%# Eval("MilkType_id") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FAT %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFooter" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQty" runat="server" Text='<%# Eval("MilkQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMilkQty" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnit_id" runat="server" Visible="false" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate/Ltr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkCollectionRatePerKG" runat="server" Visible="false" Text='<%# Eval("MilkCollectionRatePerKG", "{0:0.00}") %>'></asp:Label>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RatePerLtr", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value (In Rs.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TotalValue", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalValue" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("DCSChild_Id") %>' CommandName="DeleteRow" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnSave" ValidationGroup="a" Text="Save" CssClass="btn btn-primary btn-block" OnClientClick="return ValidatePage();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-default btn-block" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Details of Direct Milk Collection From Producer</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gv_MilkCollectionDetails" ShowHeader="true" DataKeyNames="DCSCollectionId" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server" OnRowCommand="gv_MilkCollectionDetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dugdh Sangh">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCSCollectionId" runat="server" Visible="false" Text='<%# Eval("DCSCollectionId") %>'></asp:Label>
                                                <asp:Label ID="lblParant_Office_Name" runat="server" Text='<%# Eval("Parant_Office_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dairy Cooperative Society">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CollectionDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShift" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Member ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMember_ID" runat="server" Text='<%# Eval("UserName") + " [" + Eval("ProducerName") + "]" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" CommandName="ViewRow" CommandArgument='<%#Eval("DCSCollectionId") %>' Style="color: gray;" runat="server" ToolTip="Click to View Milk Collection Detail's"><i class="fa fa-eye"></i></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="lblEdit" CommandName="EditRow" CommandArgument='<%#Eval("DCSCollectionId") %>' runat="server" ToolTip="Click to Edit Milk Collection Detail's"><i class="fa fa-pencil"></i></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("DCSCollectionId") %>' Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
                                    <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp; 
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

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="ViewModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 60%; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel2">Milk Collection Detail's</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label1" Text="Member ID [Name] : " runat="server"></asp:Label>
                                        <asp:Label ID="lblMemberId" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvPopup_ViewMilkCollectionDetails" ShowHeader="true" ShowFooter="true" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server" OnRowDataBound="gvPopup_ViewMilkCollectionDetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkType_id" runat="server" Visible="false" Text='<%# Eval("MilkType_id") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FAT %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFooter" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQty" runat="server" Text='<%# Eval("MilkQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMilkQty" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnit_id" runat="server" Visible="false" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate/Ltr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RatePerLtr", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value (In Rs.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TotalValue", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalValue" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">CLOSE </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $('#txtTime').timepicker();

        function FillUnit() {
            var ddlUnit = document.getElementById("<%=ddlUnit.ClientID %>");
            var selectedText = ddlUnit.options[ddlUnit.selectedIndex].innerHTML;
            var selectedValue = ddlUnit.value;

            if (selectedValue > 0) {
                document.getElementById('<%= lblUnit.ClientID %>').textContent = selectedText;

            }
            else {
                document.getElementById('<%= lblUnit.ClientID %>').textContent = "KG";
            }
        }

        function ValidatePage() {
            if (typeof (Page_ClientValidate)) {
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

        function FatAcordingtoMilkType(sender, args) {
            var mType = document.getElementById('<%=ddlMilkType.ClientID%>').value;
            var val = args.Value;

            if (mType == 1) { // Milk Type 1 for Cow
                if (val >= 3.2 && val <= 5.5) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else if (mType == 2) { // Milk Type 2 for Buffalo
                if (val >= 5.6 && val <= 10) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else {
                if (mType != 1 && mType != 2) { // Milk Type 3 for Mix
                    if (val >= 3.2 && val <= 10) {
                        args.IsValid = true;
                    }
                    else {
                        args.IsValid = false;
                    }
                }
            }
        }

        function AllowedCLR(sender, args) {
            if (args.Value >= 24 && args.Value <= 30) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function ViewDetailModal() {
            $('#ViewModal').modal('show');
            return false;
        }
    </script>
</asp:Content>

