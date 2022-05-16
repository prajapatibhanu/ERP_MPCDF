<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" Culture="en-IN" AutoEventWireup="true" CodeFile="ReceiveTankerAtSecurity.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="mis_MilkCollection_ReceiveTankerAtSecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish" style="min-height: 250px;">
                <div class="box-header">
                    <h3 class="box-title">Update Tanker Gross Weight</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Update Gross Weight</legend>
                        <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Reference No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvReferenceNo" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlReferenceNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Reference No!'></i>" ErrorMessage="Enter Reference No." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlReferenceNo" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnInit="ddlReferenceNo_Init" OnSelectedIndexChanged="ddlReferenceNo_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <%--<div class="col-md-2">
                                <div class="form-group">
                                    <label>Challan No.<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlchallanno" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Challan No!'></i>" ErrorMessage="Select Challan No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlchallanno" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlchallanno_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>

                            <div class="col-md-2" id="dv_TankerType" runat="server">
                                <label>Tanker Type<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlTankerType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker Type!'></i>" ErrorMessage="Select Tanker Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlTankerType" Enabled="false" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlTankerType_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Single Chamber" Value="S"></asp:ListItem>
                                        <asp:ListItem Text="Dual Chamber" Value="D"></asp:ListItem>
                                    </asp:DropDownList>
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
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ForeColor="Red" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtD_GrossWeight" ErrorMessage="Gross Weight Required" Text="<i class='fa fa-exclamation-circle' title='Gross Weight Required !'></i>" SetFocusOnError="true" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
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
						<div class="row" id="divccsealdetails" runat="server" visible="false">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>CC Seal Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvCCSealDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="CC" DataField="Office_Name" />
                                                <asp:BoundField HeaderText="Seal No" DataField="V_SealNo" />
                                                <asp:BoundField HeaderText="Seal Location" DataField="V_SealLocation" />
												 <asp:BoundField HeaderText="Seal Color" DataField="V_SealColor" />
                                                <asp:BoundField HeaderText="Seal Remark" DataField="V_SealRemark" />
                                               
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6" id="div_SealVerification_Single_Challan" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Seal Verificaiton for Challan No.
                                            <asp:Label ID="lblFirstChallan" Font-Bold="true" runat="server" Text="#"></asp:Label></legend>
                                        <div class="mb-1">
                                            <i style="color: red;"><b>Note</b>:-
                                            <br />
                                                (a)<b>Single Chamber:</b> Minimum chamber seal required 1 and maximum 10 and valve seal required minimum 1 and maximum 2<br />
                                                (b)<b>Dual Chamber:</b> Minimum chamber seal required 2 and maximum 10 and valve seal required minimum 1 and maximum 2<br />
                                                (c)<b>Seal Verification File:</b> If Seal Broken / Damage etc. then please upload file.
                                            </i>
                                            <hr />
                                        </div>


                                        <div class="row" runat="server" id="Divsealvarfication" visible="false">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Total Seals<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvTotalSeals" runat="server" Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Enter Total Seals!'></i>" ErrorMessage="Enter Total Seals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rvTotalSeals" runat="server" Display="Dynamic" ControlToValidate="txtTotalSeals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add" Type="Integer" MinimumValue="5" MaximumValue="10"></asp:RangeValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtTotalSeals" Text="1" CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Button ID="btnAdd" Visible="false" CssClass="btn btn-facebook button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="Add" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="rowSealDetails" runat="server" style="display: none;">
                                            <div class="col-md-12">
                                                <div class="form-group table-responsive">
                                                    <table class="table-bordered table">
                                                        <tr>
                                                            <th>Seal No.</th>
                                                            <th>Seal&nbsp;Location</th>
                                                            <th>Seal Color</th>
                                                            <th>Seal Remark</th>
                                                        </tr>
                                                        <manish id="dvSealDetails" runat="server"></manish>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>


                                    </fieldset>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6" id="div_SealVerification_Dual_Challan" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Seal Verificaiton for Challan No.
                                            <asp:Label ID="lblSecondChallan" Font-Bold="true" runat="server" Text="#"></asp:Label>
                                        </legend>
                                        <div class="mb-1">
                                            <i style="color: red;"><b>Note</b>:-
                                            <br />
                                                (a)<b>Single Chamber:</b> Minimum chamber seal required 1 and maximum 10 and valve seal required minimum 1 and maximum 2<br />
                                                (b)<b>Dual Chamber:</b> Minimum chamber seal required 2 and maximum 10 and valve seal required minimum 1 and maximum 2<br />
                                                (c)<b>Seal Verification File:</b> If Seal Broken / Damage etc. then please upload file.
                                            </i>
                                            <hr />
                                        </div>


                                        <div class="row" runat="server" id="Div1">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Total Seals<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfv_SecondTotalSeals" runat="server" Display="Dynamic" ControlToValidate="txtSecondTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Enter Total Seals!'></i>" ErrorMessage="Enter Total Seals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSecond"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rv_SecondTotalSeals" runat="server" Display="Dynamic" ControlToValidate="txtSecondTotalSeals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSecond" Type="Integer" MinimumValue="5" MaximumValue="10"></asp:RangeValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtSecondTotalSeals" Text="1" CssClass="form-control" runat="server"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Button ID="btnAddSecondSealDetails" Visible="false" CssClass="btn btn-facebook button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAddSecondSealDetails_Click" ValidationGroup="AddSecond" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="Div_SecondSealDetails" runat="server" style="display: none;">
                                            <div class="col-md-12">
                                                <div class="form-group table-responsive">
                                                    <table class="table-bordered table">
                                                        <tr>
                                                            <th>Seal No.</th>
                                                            <th>Seal&nbsp;Location</th>
                                                            <th>Seal Color</th>
                                                            <th>Seal Remark</th>
                                                        </tr>
                                                        <div id="dv_SecondSealDetails" runat="server"></div>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </div>
                            </div>
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

                    <fieldset id="fs_action" runat="server" visible="false">
                        <legend>Action</legend>
                        <div class="row">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" Style="margin-top: 20px;" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Update" Visible="false" OnClientClick="return ValidatePage();" AccessKey="S" />
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

            </div>
            <!-- /.box-body -->
            <%--<div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Reference Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">

                        <asp:GridView ID="gv_viewreferenceno" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSealNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								 <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerDispatchDate" runat="server" Text='<%# Eval("DT_TankerDispatchDate", "{0:dd-MMM-yyyy hh:mm tt}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reference No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

								<asp:TemplateField HeaderText="Office Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
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

                                <asp:TemplateField HeaderText="TankerArrival Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# Eval("DT_TankerArrivalDate", "{0:dd-MMM-yyyy hh:mm tt}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receipt No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Eval("WeightReceiptNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Gross Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_GrossWeight" runat="server" Text='<%# Eval("D_GrossWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>--%>

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Updated Tanker Gross Weight by Security</h3>
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

                            <asp:GridView ID="gv_TodayReceivedTankerDetails" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
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
									
									<asp:TemplateField HeaderText="Challan No/Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChallan_No" runat="server" Text='<%# Eval("Challan_No") %>'></asp:Label>
										<asp:Label ID="lbltcs" ForeColor="Red" Font-Bold="true" runat="server" Text='<%# Eval("Challan_No").ToString() == "" ? "Pending" : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  
                                    <asp:TemplateField HeaderText="Office Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
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
        </section>
        <!-- /.content -->

    </div>
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


