<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkInwardOutwardReferenceDetails_New.aspx.cs" Inherits="mis_MilkCollection_MilkInwardOutwardReferenceDetails_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Generate Gate Pass</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Movement Type</label>
                                <asp:DropDownList ID="ddlMovemnetType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlMovemnetType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Empty Tanker">Empty Tanker</asp:ListItem>
                                    <asp:ListItem Value="Filled Tanker"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Vehicle/Tanker Details</legend>
                        <div class="row">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Tanker No.<span style="color: red;"> *</span>
                                        <asp:LinkButton ID="lblAddTanker" CausesValidation="false" ToolTip="Add New Tanker Details" OnClick="lblAddTanker_Click" runat="server"><b>[Add]</b></asp:LinkButton>
                                        <asp:HyperLink ID="HyperLink1" Visible="false" NavigateUrl="~/mis/Masters/Tanker_Master.aspx" Target="_blank" runat="server">Add Tanker</asp:HyperLink>
                                    </label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="b"
                                            InitialValue="0" ErrorMessage="Select Tanker Detail" Text="<i class='fa fa-exclamation-circle' title='Select Tenker Detail !'></i>"
                                            ControlToValidate="ddlTankerDetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlTankerDetail" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlTankerDetail_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>

                            <div runat="server" id="Divaddtanker" visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Tanker Type<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VehicleType" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Tanker Capacity<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtD_VehicleCapacity" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Transporter Name<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VenderName" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Transporter Contact No<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VendorContactNo" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Tanker Status<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTankerStatus" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <div class="row">

                        <div class="col-lg-6">
                            <fieldset>
                                <legend>Chilling Center Details </legend>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>CC Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                InitialValue="0" ErrorMessage="Select CC Name" Text="<i class='fa fa-exclamation-circle' title='Select CC Name !'></i>"
                                                ControlToValidate="ddlccdetails" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlccdetails" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Sequence No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="b"
                                                ErrorMessage="Enter SequenceNo" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sequence No !'></i>"
                                                ControlToValidate="txtTI_SequenceNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="\d{1}" ValidationGroup="b" runat="server" ControlToValidate="txtTI_SequenceNo" ErrorMessage="Enter Valid Number or one digit value in Sequence." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number or one digit value in Sequence. !'></i>"></asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="rValidator" Display="Dynamic" MinimumValue="1" MaximumValue="2" ValidationGroup="b" runat="server" ControlToValidate="txtTI_SequenceNo" Type="Integer" SetFocusOnError="true" ErrorMessage="Invalid Value (Allow value 1-2 only)" Text="<i class='fa fa-exclamation-circle' title='Invalid Value (Allow value 1-2 only)!'></i>"></asp:RangeValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtTI_SequenceNo" MaxLength="1" placeholder="Enter Sequence No"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnAddcc_Click" Style="margin-top: 20px;" ValidationGroup="b" ID="btnAddcc" Text="Add CC" />
                                    </div>
                                </div>

                                <hr />

                                <asp:GridView ID="gv_CCDetails" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CC Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_OfficeID" Visible="false" runat="server" Text='<%# Eval("I_OfficeID") %>'></asp:Label>
                                                <asp:Label ID="lblccname" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sequence No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTI_SequenceNo" runat="server" Text='<%# Eval("TI_SequenceNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteCC" OnClick="lnkDeleteCC_Click" runat="server" ToolTip="DeleteCC" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </fieldset>
                        </div>

                        <div class="col-lg-6">
                            <fieldset>
                                <legend>Tanker Valve Seal Details (OPTIONAL)</legend>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Seal No/Digi Lock No.</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="c"
                                                ErrorMessage="Enter Seal/Digi Lock No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Seal/Digi Lock No !'></i>"
                                                ControlToValidate="txtV_SealNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[0-9a-z-A-Z]+$" ValidationGroup="a" runat="server" ControlToValidate="txtV_SealNo" ErrorMessage="Enter Valid Seal Number." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Seal Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealNo" MaxLength="20" placeholder="Enter Seal/Digi Lock No"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Seal Remark </label>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealRemark" MaxLength="200" placeholder="Enter Seal Remark"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Seal Color </label>
                                        <asp:RequiredFieldValidator ID="rfvSealColor" runat="server" Display="Dynamic" ControlToValidate="ddlSealColor" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Reference No!'></i>" ErrorMessage="Enter Reference No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSealColor" CssClass="form-control" OnInit="ddlSealColor_Init" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnTankerValveSealDetails_Click" Style="margin-top: 20px;" ValidationGroup="c" ID="btnTankerValveSealDetails" Text="Add Seal" />
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
                                        <asp:TemplateField HeaderText="Seal/Digi Lock No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_SealNo" runat="server" Text='<%# Eval("V_SealNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seal Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_SealRemark" runat="server" Text='<%# Eval("V_SealRemark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_SealColor" runat="server" Text='<%# Eval("V_SealColor") %>'></asp:Label>
                                                <asp:Label ID="lblTI_SealColor" Visible="false" runat="server" Text='<%# Eval("TI_SealColor") %>'></asp:Label>
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

                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <fieldset>

                                <legend>Driver Details </legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Enter Driver Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                ErrorMessage="Enter Driver Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name !'></i>"
                                                ControlToValidate="txtV_DriverName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[a-zA-Z'.\s]{1,200}$" ValidationGroup="a" runat="server" ControlToValidate="txtV_DriverName" ErrorMessage="Enter Valid Driver Name." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Driver Name. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" oncopy="return false" onpaste="return false" runat="server" CssClass="form-control" ID="txtV_DriverName" MaxLength="40" placeholder="Enter Driver Name"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Enter Driver Mobile No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                ErrorMessage="Enter Driver Mobile No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile !'></i>"
                                                ControlToValidate="txtV_DriverMobileNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" ValidationGroup="a" runat="server" ControlToValidate="txtV_DriverMobileNo" ErrorMessage="Enter Driver Mobile Number." Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_DriverMobileNo" MaxLength="10" placeholder="Enter Driver Mobile"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Enter Driver Licence No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
                                                ErrorMessage="Enter Driver Licence No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Licence No !'></i>"
                                                ControlToValidate="txtNV_DriverDrivingLicenceNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtNV_DriverDrivingLicenceNo" MaxLength="20" placeholder="Enter Driver Licence No"></asp:TextBox>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div id="v_MilkQualityDetails" runat="server" visible="false">
                                <fieldset>
                                    <legend>Milk Quality Details
                                    </legend>
                                    <p><i style="color: red;">Note: All fields are mandatory.</i> </p>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>CC<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ControlToValidate="ddlCC" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select CC!'></i>" ErrorMessage="Select CC" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCC" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Front" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Rear" Value="R"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Chamber Type<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCompartmentType" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvCompartmentType_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCompartmentType" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Front" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Rear" Value="R"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Milk Quality<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvMilkQuality_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server" OnInit="ddlMilkQuality_Init">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Good" Selected="True" Value="Good"></asp:ListItem>
                                            <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                            <asp:ListItem Text="Off Taste" Value="Off Taste"></asp:ListItem>
                                            <asp:ListItem Text="Slightly Off Taste" Value="Slightly Off Taste"></asp:ListItem>
                                            <asp:ListItem Text="Sour" Value="Sour"></asp:ListItem>
                                            <asp:ListItem Text="Curd" Value="Curd"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQuantity" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvMilkQuantity_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="revMilkQuantity" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revMilkQuantity_S" Enabled="false" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtMilkQuantity" AutoPostBack="true" OnTextChanged="txtMilkQuantity_TextChanged" autocomplete="off" CssClass="form-control" Width="100%" placeholder="Milk Quantity (In KG)" runat="server" MaxLength="7"></asp:TextBox>
                                                <asp:Label ID="lblD_VehicleCapacity" Visible="false" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <label>Temperature (°C)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature!'></i>" ErrorMessage="Enter Temperature" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvTemprature_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature!'></i>" ErrorMessage="Enter Temperature" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revTemprature_S" Enabled="false" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtTemprature" CssClass="form-control" placeholder="Temperature" runat="server" MaxLength="4" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Acidity %<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvAcidity" runat="server" Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Enter Acidity!'></i>" ErrorMessage="Enter Acidity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvAcidity_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Enter Acidity!'></i>" ErrorMessage="Enter Acidity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="revAcidity" ControlToValidate="txtAcidity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revAcidity_S" Enabled="false" ControlToValidate="txtAcidity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Minimum Acidity % required 0 and maximum 1." Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Minimum Acidity % required 0 and maximum 1.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="0" MaximumValue="1"></asp:RangeValidator>

                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Minimum no. of Acidity % required 0 and maximum 1." Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Minimum Acidity % required 0 and maximum 1.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="0" MaximumValue="1"></asp:RangeValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtAcidity" autocomplete="off" CssClass="form-control" placeholder="Acidity %" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>COB<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCOB" runat="server" Display="Dynamic" ControlToValidate="ddlCOB" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select COB!'></i>" ErrorMessage="Select COB" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvCOB_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlCOB" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select COB!'></i>" ErrorMessage="Select COB" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCOB" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Positive" Value="Positive"></asp:ListItem>
                                                    <asp:ListItem Text="Negative" Value="Negative"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <label>Fat % (0 - 10)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvFat_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="revFat" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revFat_S" Enabled="false" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum FAT % required 0 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 0 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="0" MaximumValue="10"></asp:RangeValidator>

                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 0 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 0 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="0" MaximumValue="10"></asp:RangeValidator>

                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtFat" autocomplete="off" Width="100%" AutoPostBack="true" OnTextChanged="txtCLR_TextChanged" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>CLR (20 - 35)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvCLR_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revCLR_S" Enabled="false" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 35." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 35.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="20" MaximumValue="35"></asp:RangeValidator>

                                                <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 35." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 35.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="20" MaximumValue="35"></asp:RangeValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtCLR" autocomplete="off" Width="100%" AutoPostBack="true" OnTextChanged="txtCLR_TextChanged" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>SNF %<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvSNF_S" Enabled="true" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="revSNF" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revSNF_S" Enabled="true" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtSNF" Width="100%" Enabled="true" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <label>MBRT</label>
                                            <span class="pull-right">
                                                <%--<asp:RequiredFieldValidator ID="rfvMBRT" runat="server" Display="Dynamic" ControlToValidate="txtMBRT" Text="<i class='fa fa-exclamation-circle' title='Enter MBRT!'></i>" ErrorMessage="Enter MBRT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvMBRT_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtMBRT" Text="<i class='fa fa-exclamation-circle' title='Enter MBRT!'></i>" ErrorMessage="Enter MBRT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                                <asp:RegularExpressionValidator ID="revMBRT" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                <asp:RegularExpressionValidator ID="revMBRT_S" Enabled="false" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtMBRT" autocomplete="off" CssClass="form-control" placeholder="MBRT Exp. 0.00" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <label>Alcohol</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvAlcohol" runat="server" Display="Dynamic" ControlToValidate="ddlAlcohol" Text="<i class='fa fa-exclamation-circle' title='Select Alcohol!'></i>" ErrorMessage="Select Alcohol" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvAlcohol_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlAlcohol" Text="<i class='fa fa-exclamation-circle' title='Select Alcohol!'></i>" ErrorMessage="Select Alcohol" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <%--<asp:TextBox ID="txtAlcohol" CssClass="form-control" placeholder="Alcohol" runat="server"></asp:TextBox>--%>

                                                <asp:DropDownList ID="ddlAlcohol" AutoPostBack="true" OnSelectedIndexChanged="ddlAlcohol_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Positive" Value="Positive"></asp:ListItem>
                                                    <asp:ListItem Text="Negative" Value="Negative"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-md-3" runat="server" id="DivAlcoholper" visible="false">
                                            <label>Alcohol In %<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="revtxtAlcoholperc1" Enabled="false" ControlToValidate="txtAlcoholperc" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>
                                                <asp:RegularExpressionValidator ID="revtxtAlcoholperc2" Enabled="false" ControlToValidate="txtAlcoholperc" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtAlcoholperc" CssClass="form-control" placeholder="Alcohol In %" runat="server" MaxLength="2" autocomplete="off"></asp:TextBox>

                                            </div>
                                        </div>


                                        <div class="col-md-1" runat="server" id="dv_gvMilkQualityDeailsAddButton" visible="false">
                                            <div class="form-group">
                                                <asp:Button ID="btnAddQualityDetails" CssClass="btn btn-primary right" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAddQualityDetails_Click" ValidationGroup="AddQuality" />
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 20px;" runat="server" id="dv_gvMilkQualityDeails" visible="false">
                                            <div class="form-group table-responsive">
                                                <asp:GridView ID="gv_MilkQualityDetail" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblI_OfficeID" Visible="false" runat="server" Text='<%# Eval("I_OfficeID") %>'></asp:Label>
                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chamber Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSealLocation" Visible="false" runat="server" Text='<%# Eval("V_SealLocation") %>'></asp:Label>
                                                                <asp:Label ID="V_SealLocationAlias" runat="server" Text='<%# Eval("V_SealLocationAlias") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Quality">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("V_MilkQuality") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Temp">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("V_Temp") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acidity (%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAcidity" runat="server" Text='<%# Eval("V_Acidity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="COB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCOB" runat="server" Text='<%# Eval("V_COB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alcohol">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAlcohol" runat="server" Text='<%# Eval("V_Alcohol") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MBRT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMBRT" runat="server" Text='<%# Eval("V_MBRT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FAT (%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("D_FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF (%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("D_SNF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("D_CLR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDeleteQD" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')" OnClick="lnkDeleteQD_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="col-md-6" runat="server" id="milktestdetail" visible="false">
                                    <fieldset>
                                        <legend>Adulteration Test Details
                                        </legend>

                                        <i style="color: red; font-size: 12px;">Note:- All Adulteration Tests are mandatory.</i>
                                        <hr />
                                        <div class="form-group table-responsive">
                                            <asp:GridView ID="gvmilkAdulterationtestdetail" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Chamber Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSealLocation" runat="server" Text='<%# Eval("V_SealLocation").ToString() == "F" ? "Front" : Eval("V_SealLocation").ToString() == "S" ? "Single" : "Rear" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Adulteration Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAdulterationType" runat="server" Text='<%# Eval("V_AdulterationType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Test Value">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlAdelterationTestValue" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Negative" Value="Negative"></asp:ListItem>
                                                                <asp:ListItem Text="Positive" Value="Positive"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" runat="server">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
    <!-- /.box-body -->

    <div class="box box-Manish">
        <div class="box-header">
            <h3 class="box-title">Generate Gate Pass Details</h3>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
            <div class="row">
                <div class="col-md-3">
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
                            <asp:TextBox ID="txtDate" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">

                    <asp:GridView ID="gv_viewreferenceno" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Generate Gate Pass Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDT_TankerDispatchDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_CreatedOn"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reference No">
                                <ItemTemplate>
                                    <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="CC Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Challan No/Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblChallan_No" runat="server" Text='<%# Eval("Challan_No") %>'></asp:Label>
                                    <asp:Label ID="lbltcs" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# Eval("Challan_No").ToString() == "" ? "Pending" : "" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status Current">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" ToolTip='<%# Eval("RefCancelRemark") %>' runat="server" Text='<%# Eval("RefCancelStatus") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Vehicle No">
                                <ItemTemplate>
                                    <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Driver Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblV_DriverName" runat="server" Text='<%# Eval("V_DriverName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Driver Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblV_DriverMobileNo" runat="server" Text='<%# Eval("V_DriverMobileNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="Status Current">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" ToolTip='<%# Eval("RefCancelRemark") %>' runat="server" Text='<%# Eval("RefCancelStatus") %>'></asp:Label>
                                        <asp:Label ID="lblRefCancelStatusF" Visible="false" runat="server" Text='<%# Eval("RefCancelStatusF") %>'></asp:Label>
                                        <asp:Label ID="lblBI_MilkInOutRefID" Visible="false" runat="server" Text='<%# Eval("BI_MilkInOutRefID") %>'></asp:Label>
                                        <asp:Label ID="lblChallan_Validation" Visible="false" runat="server" Text='<%# Eval("Challan_Validation") %>'></asp:Label>
                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <a href='../MilkCollection/GatePassReferenceDetails.aspx?Rid=<%# new APIProcedure().Encrypt(Eval("BI_MilkInOutRefID").ToString()) %>' target="_blank" title="Print Gate Pass"><i class="fa fa-print"></i></a>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>

    <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

    <div class="modal" id="ItemDetailsModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="height: 420px;">
                <div class="modal-header">
                    <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                    <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                </button>--%>
                    <h4 class="modal-title">Tanker Details</h4>
                </div>
                <div class="modal-body">

                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row" style="height: 250px; overflow: scroll;">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <section class="content">
                                            <!-- SELECT2 EXAMPLE -->
                                            <div class="box box-Manish" style="min-height: 250px;">
                                                <div class="box-header">
                                                    <h3 class="box-title">Tanker Details</h3>
                                                </div>
                                                <!-- /.box-header -->
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <fieldset>
                                                        <legend>Select Vehicle/Tanker  </legend>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Vehicle Type<span style="color: red;"> *</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="save"
                                                                            InitialValue="0" ErrorMessage="Select Tanker Type" Text="<i class='fa fa-exclamation-circle' title='Select Vehicle Type !'></i>"
                                                                            ControlToValidate="ddlTankerDetailT" ForeColor="Red" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator></span>
                                                                    <asp:DropDownList ID="ddlTankerDetailT" Width="100%" AutoPostBack="false" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                        <asp:ListItem Value="S">Single Chamber</asp:ListItem>
                                                                        <asp:ListItem Value="D">Dual Chamber</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Vehicle No<span style="color: red;"> *</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="save"
                                                                            ErrorMessage="Enter Vehicle No" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No !'></i>"
                                                                            ControlToValidate="txtV_VehicleNoT" ForeColor="Red" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator></span>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtV_VehicleNoT" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RegularExpressionValidator>
                                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VehicleNoT" ClientIDMode="Static" MaxLength="13" placeholder="XX-00-XX-0000"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Capacity - KG<span style="color: red;"> *</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="save"
                                                                            ErrorMessage="Enter Vehicle Capacity (In KG)" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Capacity (In KG)!'></i>"
                                                                            ControlToValidate="txtD_VehicleCapacityT" ForeColor="Red" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator></span>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="save" Display="Dynamic" runat="server" ControlToValidate="txtD_VehicleCapacityT" ErrorMessage="Invalid Vehicle Capacity (In KG)" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Capacity (In KG)!'></i>" SetFocusOnError="true" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtD_VehicleCapacityT" onkeypress="return validateDec(this,event)" placeholder="Enter Vehicle Capacity (In KG)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Vendor Name<span style="color: red;"> *</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="save"
                                                                            ErrorMessage="Enter Vendor Name" Text="<i class='fa fa-exclamation-circle' title='Enter Vendor Name !'></i>"
                                                                            ControlToValidate="txtV_VenderNameT" ForeColor="Red" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator></span>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ForeColor="Red" ValidationGroup="save" Display="Dynamic" runat="server" ControlToValidate="txtV_VenderNameT" ErrorMessage="Invalid Vendor Name" Text="<i class='fa fa-exclamation-circle' title='Invalid Vendor Name !'></i>" SetFocusOnError="true" ValidationExpression="^[a-zA-Z'.\s]{1,200}$"></asp:RegularExpressionValidator>
                                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VenderNameT" placeholder="Enter Vendor Name"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Mobile No.<span style="color: red;"> *</span></label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="save"
                                                                        ErrorMessage="Enter Vendor Contact No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vendor Contact No. !'></i>"
                                                                        ControlToValidate="txtV_VendorContactNoT" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic" ValidationGroup="save"
                                                                        ErrorMessage="Enter Valid Vendor Contact No. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Vendor Contact No. !'></i>" ControlToValidate="txtV_VendorContactNoT"
                                                                        ValidationExpression="^[6-9]{1}[0-9]{9}$">
                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VendorContactNoT" MaxLength="10" placeholder="Enter Vendor Contact No"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Milk Collection From<span style="color: red;"> *</span></label>
                                                                    <span class="pull-right">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="save"
                                                                            InitialValue="0" ErrorMessage="Select Select Milk Collection From" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection From !'></i>"
                                                                            ControlToValidate="ddlMilkCollectionFrom" ForeColor="Red" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator></span>
                                                                    <asp:DropDownList ID="ddlMilkCollectionFrom" Width="100%" AutoPostBack="false" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                                        <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                                                                        <asp:ListItem Value="CC">CC</asp:ListItem>
                                                                        <%-- <asp:ListItem Value="BMC">BMC</asp:ListItem>
                                                                                <asp:ListItem Value="MDP">MDP</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>Active Status<span style="color: red;"> *</span></label><br />
                                                                    <asp:CheckBox runat="server" ID="chkIsActive" Checked="true" />
                                                                </div>
                                                            </div>




                                                        </div>
                                                    </fieldset>

                                                    <div class="row" runat="server">
                                                        <hr />
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <%--<asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="save" ID="btnaddtanker" Text="Save" OnClientClick="return ValidateT()" />--%>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                            <!-- /.box-body -->

                                            <div class="box box-Manish">
                                                <div class="box-header">
                                                    <h3 class="box-title">Tanker Details</h3>
                                                </div>
                                                <!-- /.box-header -->
                                                <div class="box-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gv_TankerDetails" ShowHeader="true" OnRowDataBound="gv_TankerDetails_RowDataBound" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowCommand="gv_TankerDetails_RowCommand" DataKeyNames="I_TankerID">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vehicle Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTType" runat="server" Text='<%# Eval("TType") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblV_Vendor" runat="server" Text='<%# Eval("V_VenderName") %>'></asp:Label>
                                                                        <asp:Label ID="LblvendorType" runat="server" Text='<%# Eval("V_VehicleType") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblTankerStatus" runat="server" Text='<%# Eval("TankerStatus") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor ContactNo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblV_VendorContact" runat="server" Text='<%# Eval("V_VendorContactNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vehicle No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Vehicle Capacity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblV_VehicleCapacity" runat="server" Text='<%# Eval("D_VehicleCapacity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Active Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive").ToString() == "False" ? "InActive" : "Active" %>'></asp:Label>
                                                                        <asp:Label ID="lblTstatus" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Tanker Current Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltcs" runat="server" Text='<%# Eval("TankerStatus").ToString() == "False" ? "In Process" : "Available" %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Milk Collection From">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMilkCollectionFrom" runat="server" Text='<%# Eval("MilkCollectionFrom") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("I_TankerID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="save" ID="btnSaveTankerDetails" Text="Submit" OnClientClick="return ValidateT()" />
                    <%--<asp:Button runat="server" ID="btnClose" CssClass="btn btn-default" Text="CLOSE" OnClick="btnClose_Click" />--%>
                    <%--<button type="button" class="btn btn-default" data-dismiss="modal"> </button>--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    </section>
        <!-- /.content -->

    </div>

    <div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #d9d9d9;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT" OnClick="btnYesT_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


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



        function ValidateT() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSaveTankerDetails.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSaveTankerDetails.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }




        function AddTeankerMaster() {

            $('#myModal1').modal('show');
            return false;
        }



        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }


    </script>

    <script>
        function GetLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(success);
            } else {
                alert("There is Some Problem on your current browser to get Geo Location!");
            }
        }

        function success(position) {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
        }
    </script>

</asp:Content>
