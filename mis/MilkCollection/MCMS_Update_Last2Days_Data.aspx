<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MCMS_Update_Last2Days_Data.aspx.cs" Inherits="mis_mcms_reports_MCMS_Update_Last2Days_Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-Manish">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">MCMS Update Last 2 Days</h3>
                    <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                </div>
                <!-- /.card-header -->
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date</label><span style="color: red;"> *</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="glyphicon glyphicon-calendar"></i>
                                    </span>
                                    <asp:TextBox ID="txtFromDate" data-date-end-date="0d" data-date-start-date="-3d"  autocomplete="off" CssClass="form-control DateAdd"  runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date</label><span style="color: red;"> *</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="glyphicon glyphicon-calendar"></i>
                                    </span>
                                    <asp:TextBox ID="txtToDate" data-date-end-date="0d" data-date-start-date="-3d"   autocomplete="off" CssClass="form-control DateAdd"   runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Milk Collection Type</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCollectionType" AutoPostBack="true" OnSelectedIndexChanged="ddlCollectionType_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                        <asp:ListItem Value="In">In</asp:ListItem>
                                        <asp:ListItem Value="Out">Out</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <label>DS/CC</label><span style="color: red;"> *</span>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlDSName3" CssClass="form-control select2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="Save" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">

                            <asp:GridView runat="server" ID="gvReceivedEntry" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found.">
                                <Columns>
                                    <asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                    <%--<asp:BoundField DataField="Office_Name" HeaderText="Office Name" />--%>

                                    <asp:BoundField DataField="V_ReferenceCode" HeaderText="Reference No." />
                                    <asp:BoundField DataField="ChallanNo" HeaderText="Challan No." />
                                    <asp:BoundField DataField="CC_Name" HeaderText="CC Name" />
                                    <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />

                                    <asp:BoundField DataField="DT_ArrivalDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Arrival Time" />
                                    <asp:BoundField DataField="V_MilkType" HeaderText="Milk Type" />
                                    <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity (In KG)" />
                                    <asp:BoundField DataField="V_SealLocation" HeaderText="Chamber Type" />
                                    <asp:BoundField DataField="TotalSeal" HeaderText="Total Seals" />
                                    <asp:TemplateField HeaderText="More Info">
                                        <ItemTemplate>
                                            <asp:Label ID="lblV_SealLocation" runat="server" Visible="false" Text='<%# Eval("V_SealLocation") %>'></asp:Label>
                                            <asp:Label ID="lblChallanNo" runat="server" Visible="false" Text='<%# Eval("ChallanNo") %>'></asp:Label>
                                            <asp:Label ID="lblI_EntryID" runat="server" Visible="false" Text='<%# Eval("I_EntryID") %>'></asp:Label>
                                            <asp:Label ID="lblBI_MilkInOutRefID" Visible="false" runat="server" Text='<%# Eval("BI_MilkInOutRefID") %>'></asp:Label>
                                            <asp:LinkButton ID="LinkButton1" Style="padding: 3px; border-radius: 3px;" CommandArgument='<%# Eval("I_EntryID") %>' OnClick="LinkButton1_Click" CommandName="ViewEntry" runat="server" Visible='<%# Eval("Status").ToString() == "Received" ? true : false %>' Text="Edit Challan Quality Details" CssClass="label label-warning"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                </div>
            </div>
            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="pu_QCDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel"><span style="font-size: medium;">Challan No. :</span>
                                <asp:Label ID="lblVehicleNo" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>
                            </h5>
                            <button type="button" class="close pull-right" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                            </button>

                        </div>
                        <div class="clearfix"></div>
                        <div class="card-body">
                            <div class="row">
                                <asp:Label ID="lblmsgpopup" runat="server"></asp:Label>
                                <div class="col-md-12 table-responsive-md">
                                    <fieldset>
                                        <legend>Milk Quality Details
                                        </legend>
                                        <p><i style="color: red;">Note: All fields are mandatory.</i> </p>
                                        <div class="row">
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
                                                <label>Fat % (3.2 - 10)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvFat_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revFat" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator ID="revFat_S" Enabled="false" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtFat" autocomplete="off" Width="100%" AutoPostBack="true" OnTextChanged="txtCLR_TextChanged" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvCLR_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator ID="revCLR_S" Enabled="false" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>

                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtCLR" autocomplete="off" Width="100%" AutoPostBack="true" OnTextChanged="txtCLR_TextChanged" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>SNF %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvSNF_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revSNF" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator ID="revSNF_S" Enabled="false" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSNF" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3">
                                                <label>MBRT</label>
                                                <span class="pull-right">

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


                                            <div class="col-md-1" runat="server" id="dv_gvMilkQualityDeailsAddButton">
                                                <div class="form-group">
                                                    <asp:Button ID="btnAddQualityDetails" CssClass="btn btn-primary right" Style="margin-top: 20px;" runat="server" Text="Update" OnClick="btnAddQualityDetails_Click" ValidationGroup="AddQuality" />
                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>

                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <%--ConfirmationModal End --%>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowQCDetails() {
            $('#pu_QCDetails').modal('show');
            return false;
        }
    </script>
</asp:Content>

