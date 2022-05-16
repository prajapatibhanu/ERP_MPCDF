<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BMC_ReceiveTankerChallanQAOld.aspx.cs" Inherits="mis_MilkCollection_BMC_ReceiveTankerChallanQAOld" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box">
                <div class="box-header">
                    <h3 class="box-title">Receive BMC/MDP Tanker Challan  at QC</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">


                        <div class="col-md-12">

                            <fieldset>
                                <legend>Tanker Details</legend>
                                <div class="row">
                                    <div class="col-md-12 mb-1">
                                        <i style="color: red;">Note: All fields are mandatory.</i>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Reference No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvReferenceNo" runat="server" Display="Dynamic" ControlToValidate="ddlReferenceNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Reference No!'></i>" ErrorMessage="Enter Reference No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <%--<asp:TextBox ID="txtReferenceNo" CssClass="form-control" placeholder="Search Reference No." runat="server" OnTextChanged="txtReferenceNo_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlReferenceNo" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlReferenceNo_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Challan No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlchallanno" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Challan No!'></i>" ErrorMessage="Enter Challan No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlchallanno" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlchallanno_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <label>Unit Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvUnitName" runat="server" Display="Dynamic" ControlToValidate="ddlUnitName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Unit Name!'></i>" ErrorMessage="Select Unit Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlUnitName" Enabled="false" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
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
                                                <asp:TextBox ID="txtArrivalDate" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvArrivalTime" runat="server" Display="Dynamic" ControlToValidate="txtArrivalTime" Text="<i class='fa fa-exclamation-circle' title='Enter Arrival Time!'></i>" ErrorMessage="Enter Arrival Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                        </span>
                                        <div class="form-group">
                                            <label>Tanker Arrival Time<span style="color: red;">*</span></label>
                                            <div class="input-group bootstrap-timepicker timepicker">
                                                <asp:TextBox ID="txtArrivalTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Vehicle No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvVehicleNo" runat="server" Display="Dynamic" ControlToValidate="txtVehicleNo" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No.!'></i>" ErrorMessage="Enter Vehicle No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtVehicleNo" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtVehicleNo" Enabled="false" CssClass="form-control" placeholder="XX-00-XX-0000" MaxLength="13" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Driver Name<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDriverName" runat="server" Display="Dynamic" ControlToValidate="txtDriverName" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name!'></i>" ErrorMessage="Enter Driver Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtDriverName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Driver Mobile No.</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="revVehicleNo" ControlToValidate="txtDriverMobileNo" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Mobile No.!'></i>" ErrorMessage="Invalid Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtDriverMobileNo" Enabled="false" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2" id="dv_TankerType" runat="server" visible="false">
                                        <label>Tanker Type<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlTankerType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker Type!'></i>" ErrorMessage="Select Tanker Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlTankerType" Enabled="false" AutoPostBack="true" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Single Chamber" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Dual Chamber" Value="D"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                </div>
                            </fieldset>
                        </div>

                        <div class="col-md-12">

                            <div class="col-md-6" runat="server" id="divmqd" visible="false">
                                <fieldset>
                                    <legend>Milk Quality Details</legend>

                                    <i style="color: red; font-size: 12px;">Note:- All Milk Quality Details are mandatory.</i>
                                    <hr />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Chamber Type<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCompartmentType" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCompartmentType" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Milk Quality<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server" OnInit="ddlMilkQuality_Init">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Fat % (3.2 - 10)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revFat_S" ControlToValidate="txtNetFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtNetFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                                </span>
                                                <asp:TextBox ID="txtNetFat" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtNetFat_TextChanged" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>

                                            </div>

                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtNetCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtNetCLR" autocomplete="off" Width="100%" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtNetCLR_TextChanged" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>SNF %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtnetsnf" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revSNF_S" ControlToValidate="txtnetsnf" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtnetsnf" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Temperature (°C)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txttemperature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txttemperature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                                </span>
                                                <asp:TextBox ID="txttemperature" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Sample No<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtSampalNo" Text="<i class='fa fa-exclamation-circle' title='Enter Sample No!'></i>" ErrorMessage="Enter Sample No" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtSampalNo" Width="100%" CssClass="form-control" placeholder="Sample No" runat="server" MaxLength="6"></asp:TextBox>

                                            </div>
                                        </div>


                                    </div>
                                </fieldset>
                            </div>

                            <div class="col-md-6" runat="server" id="divadt" visible="false">
                                <fieldset>
                                    <legend>Adulteration Test Details
                                    </legend>


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

                    <div class="row">
                        <div class="col-md-12" runat="server" id="divaction" visible="false">
                            <fieldset>
                                <legend>Action</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Submit" OnClientClick="return ValidatePage();" AccessKey="S" />

                                        </div>
                                    </div>
                                </div> 
                            </fieldset>
                        </div>
                    </div>


                </div>
            </div>

            <div class="box box-Manish" runat="server">
                <div class="box-header">
                    <h3 class="box-title">Received Challan Details at QC</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">


                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtfilterdate" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtfilterdate_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvReceivedEntry" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found.">
                            <Columns>
                                <asp:TemplateField HeaderText="Arrival Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_TankerArrivalDate"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="C_ReferenceNo" HeaderText="Reference No." />
                                <asp:BoundField DataField="V_ReferenceCode" HeaderText="Challan No." />
                                <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />
                                <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
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
                                <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity (In KG)" />
                                <asp:BoundField DataField="FAT" HeaderText="FAT %" />
                                <asp:BoundField DataField="CLR" HeaderText="CLR" />
                                <asp:BoundField DataField="SNF" HeaderText="SNF %" /> 
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>

        </section>
    </div>


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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtArrivalTime').timepicker();

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
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

    </script>



</asp:Content>
