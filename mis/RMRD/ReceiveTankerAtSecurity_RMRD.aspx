<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ReceiveTankerAtSecurity_RMRD.aspx.cs" Inherits="mis_RMRD_ReceiveTankerAtSecurity_RMRD" %>

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
                    <h3 class="box-title">Tanker Gross Weight</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
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
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
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
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Gross Weight (In KG) <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ValidationGroup="a"
                                            ErrorMessage="Enter Gross Weight" Text="<i class='fa fa-exclamation-circle' title='Enter Gross Weight !'></i>"
                                            ControlToValidate="txtD_GrossWeight" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:RangeValidator ID="rvalidator" runat="server" ControlToValidate="txtD_GrossWeight" Type="Double" ErrorMessage="Required Minimum Value 999!" Text="<i class='fa fa-exclamation-circle' title='Required Minimum Value 999!'></i>" SetFocusOnError="true" ForeColor="Red" Display="Dynamic" ValidationGroup="a" MinimumValue="999" MaximumValue="9999999999"></asp:RangeValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtD_GrossWeight" ErrorMessage="Gross Weight Required" Text="<i class='fa fa-exclamation-circle' title='Gross Weight Required !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtD_GrossWeight_TextChanged" autocomplete="off" CssClass="form-control" ID="txtD_GrossWeight" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="10" placeholder="Enter Gross Weight"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Receipt No. <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ValidationGroup="a"
                                            ErrorMessage="Enter Receipt No." Text="<i class='fa fa-exclamation-circle' title='Enter Receipt No.!'></i>"
                                            ControlToValidate="txtReceiptNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtReceiptNo" ClientIDMode="Static" MaxLength="12" placeholder="Enter Receipt No."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <div class="row" runat="server" visible="false" id="divsealdetail">
                        <div class="col-lg-6">
                            <fieldset>
                                <legend>Milk Collection Unit Details </legend>
                                 <i style="color: red; font-size: 12px;">Note:- Milk Collection Unit Details are mandatory.</i>
                                <hr />

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>BMC Root<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator144" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                       <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>


                               

                                <asp:GridView ID="gv_BMCDetails" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BMC Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_OfficeID" Visible="false" runat="server" Text='<%# Eval("I_OfficeID") %>'></asp:Label>
                                                <asp:Label ID="lblccname" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                        <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                    </Columns>
                                </asp:GridView>

                            </fieldset>
                        </div>
                        <div class="col-lg-6">
                            <fieldset>
                                <legend>Tanker Seal Details</legend>

                                
                                 <div class="mb-1">
                                            <i style="color: red;"><b>Note</b>:-
                                            <br />
                                                (a)<b>Single Chamber:</b> Minimum chamber seal required 1 and maximum 10 and valve seal required minimum 1 and maximum 2<br />
                                                (b)<b>Dual Chamber:</b> Minimum chamber seal required 2 and maximum 10 and valve seal required minimum 1 and maximum 2<br />
                                                (c)<b>Seal Verification File:</b> If Seal Broken / Damage etc. then please upload file.
                                            </i>
                                            <hr />
                                        </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Chamber Type </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic" ControlToValidate="ddlChamberType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlChamberType" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                           <%-- <asp:ListItem Value="Chamber">Chamber</asp:ListItem>
                                            <asp:ListItem Value="ValveBox">ValveBox</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Seal Color </label>
                                        <asp:RequiredFieldValidator ID="rfvSealColor" runat="server" Display="Dynamic" ControlToValidate="ddlSealColor" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSealColor" OnInit="ddlSealColor_Init" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Seal No.</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="c"
                                                ErrorMessage="Enter Seal No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Seal No !'></i>"
                                                ControlToValidate="txtV_SealNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" Display="Dynamic" ValidationExpression="^[0-9a-z-A-Z]+$" ValidationGroup="c" runat="server" ControlToValidate="txtV_SealNo" ErrorMessage="Enter Valid Seal Number." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Seal Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealNo" MaxLength="20" placeholder="Enter Seal No"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Seal Remark </label>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealRemark" MaxLength="200" placeholder="Enter Seal Remark"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnTankerSealDetails_Click"  ValidationGroup="c" ID="btnTankerSealDetails" Text="Add Seal" />
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
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[^'@%#$&=^!~?]+$" ValidationGroup="a" runat="server" ControlToValidate="txtV_DriverName" ErrorMessage="Enter Valid Driver Name." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Driver Name. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control capitalize ui-autocomplete-12" ID="txtV_DriverName" ClientIDMode="Static" MaxLength="40" placeholder="Enter Driver Name"></asp:TextBox>
                                    <asp:HiddenField ID="hfV_DriverName" runat="server" ClientIDMode="Static" />
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
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" ValidationGroup="a" runat="server" ControlToValidate="txtV_DriverMobileNo" ErrorMessage="Enter Driver Mobile Number." Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile Number. !'></i>"></asp:RegularExpressionValidator>
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




                    <div class="row" runat="server">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                <a class="btn btn-default"  href="ReceiveTankerAtSecurity_RMRD.aspx">Clear</a>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <!-- /.box-body -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Tanker Gross Weight At Security</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Arrival Date</label>
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

                            <asp:GridView ID="gv_TodayReceivedTankerDetailsRMRD" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Arrival Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_TankerArrivalDate"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reference No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  
                                 <%--   <asp:TemplateField HeaderText="Office Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

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



                                    <asp:TemplateField HeaderText="Gross Weight (In KG)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblD_GrossWeight" runat="server" Text='<%# Eval("D_GrossWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Gross Weight ReceiptNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightReceiptNo" runat="server" Text='<%# Eval("WeightReceiptNo") %>'></asp:Label>
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
                            <asp:LinkButton runat="server" class="close" ID="btnCrossButton"><span aria-hidden="true">×</span></asp:LinkButton>
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
                                                                                <%--<asp:ListItem Value="CC">CC</asp:ListItem>--%>
                                                                                <asp:ListItem Value="BMC">BMC</asp:ListItem>


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

                                                                        <asp:TemplateField HeaderText="Action" Visible="false">
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
                SS
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
     <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            debugger;
            $("#<%=txtV_DriverName.ClientID %>").autocomplete({

                source: function (request, response) {
                    $.ajax({

                        url: '<%=ResolveUrl("ReceiveTankerAtSecurity_RMRD.aspx/SearchDrivers") %>',
                        data: "{ 'Driver_Name': '" + $('#txtV_DriverName').val() + "'}",
                            //  var param = { ItemName: $('#txtItem').val() };
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {

                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                        //val: item.split('-')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                select: function (e, i) {
                    $("#<%=hfV_DriverName.ClientID %>").val(i.item.val);
                    var DriverName = i.item.value;
                    callback2Function(DriverName);
                    },
                    minLength: 1

            });

        });
        function callback2Function(DriverName)
        {
            debugger
            var dat = { "DriverName": DriverName,}
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "ReceiveTankerAtSecurity_RMRD.aspx/FillDriverDetail",
                data: JSON.stringify(dat),
                dataType: "json",
                success: function (data) {
                    $("#<%=txtV_DriverMobileNo.ClientID %>").val(data.d[0]);
                    $("#<%=txtNV_DriverDrivingLicenceNo.ClientID %>").val(data.d[1]);
                },
                error: function (result) {
                    alert("Sorry!!! Your session has expired. Please log in again");
                }
            });
        }
    </script>

    
</asp:Content>

