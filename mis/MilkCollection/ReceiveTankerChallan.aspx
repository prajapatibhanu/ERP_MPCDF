<%@ Page Title="" Language="C#" Culture="en-IN" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ReceiveTankerChallan.aspx.cs" Inherits="mis_MilkCollection_ReceiveTankerChallan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
 <asp:ScriptManager runat="server" ID="SM1">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box">
                <div class="box-header">
                    <h3 class="box-title">Receive Tanker Challan  at QC</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
					
					 <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="20000">
                    </asp:Timer>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="navbar-custom-menu pull-right">
                                <ul class="nav navbar-nav">
                                    <!-- Notifications: style can be found in dropdown.less -->
                                    <li class="dropdown notifications-menu">
                                        <a href="ReceiveTankerChallan.aspx" class="dropdown-toggle" data-toggle="dropdown">
                                            <i class="fa fa-bell-o"></i>
                                            <span class="label label-warning">
                                                <asp:Label ID="lblNotificationCount_Top" runat="server"></asp:Label></span>
                                        </a>
                                        <ul class="dropdown-menu" style="border: 1px solid #0044cc;">
                                            <li class="header">You have
                                            <asp:Label ID="lblNotificationCount" runat="server"></asp:Label>
                                                notifications</li>
                                            <li>
                                                <!-- inner menu: contains the actual data -->
                                                <ul class="menu">
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <ItemTemplate>
                                                            <li>
                                                                <a href="MilkTestingRequestToQCFeedBack.aspx">
                                                                    <i class="fa fa-bell"></i><%# Eval("C_ReferenceNo").ToString()%>
                                                                </a>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>


                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <asp:MultiView ID="mv_FormWizard" runat="server" ActiveViewIndex="0">
                            <asp:View ID="v_TankerDetails" runat="server">
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
                                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                                                        <asp:RequiredFieldValidator ID="rfvReferenceNo" runat="server" Display="Dynamic" ControlToValidate="ddlReferenceNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Reference No!'></i>" ErrorMessage="Enter Reference No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlchallanno" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Challan No!'></i>" ErrorMessage="Enter Challan No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlchallanno" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlchallanno_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <label>Unit Name<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvUnitName" runat="server" Display="Dynamic" ControlToValidate="ddlUnitName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Unit Name!'></i>" ErrorMessage="Select Unit Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlUnitName" Enabled="false" CssClass="form-control" runat="server" OnInit="ddlUnitName_Init">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Tanker Arrival Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtArrivalDate" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Arrival Date!'></i>" ErrorMessage="Enter Tanker Arrival Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                                                    <asp:RequiredFieldValidator ID="rfvArrivalTime" runat="server" Display="Dynamic" ControlToValidate="txtArrivalTime" Text="<i class='fa fa-exclamation-circle' title='Enter Arrival Time!'></i>" ErrorMessage="Enter Arrival Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                </span>
                                                <div class="form-group">
                                                    <label>Tanker Arrival Time<span style="color: red;">*</span></label>
                                                    <div class="input-group bootstrap-timepicker timepicker">
                                                        <asp:TextBox ID="txtArrivalTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                                    </div>
                                                </div>
                                            </div>

                                            <%-- <div class="col-md-4">
                                    <label>Tanker Arrival Time<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvArrivalTime" runat="server" Display="Dynamic" ControlToValidate="txtArrivalTime" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker Arrival Time!'></i>" ErrorMessage="Enter Tanker Arrival Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-clock"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtArrivalTime" autocomplete="off" CssClass="form-control timepicker" runat="server"></asp:TextBox>

                                    </div>
                                </div> 
                                    
                                    <div class="col-md-4">
                                    <label>Dispatch Time<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDispatchTime" runat="server" Display="Dynamic" ControlToValidate="txtDispatchTime" Text="<i class='fa fa-exclamation-circle' title='Enter Dispatch Time!'></i>" ErrorMessage="Enter Dispatch Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-clock"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtDispatchTime" ReadOnly="true" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>--%>


                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Vehicle No.<span style="color: red;">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvVehicleNo" runat="server" Display="Dynamic" ControlToValidate="txtVehicleNo" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No.!'></i>" ErrorMessage="Enter Vehicle No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtVehicleNo" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtVehicleNo" Enabled="false" CssClass="form-control" placeholder="XX-00-XX-0000" MaxLength="13" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Driver Name<span style="color: red;">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvDriverName" runat="server" Display="Dynamic" ControlToValidate="txtDriverName" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name!'></i>" ErrorMessage="Enter Driver Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtDriverName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Driver Mobile No.</label>
                                                    <span class="pull-right">
                                                        <%--<asp:RequiredFieldValidator ID="rfvDriverMobileNo" runat="server" Display="Dynamic" ControlToValidate="txtDriverMobileNo" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile No.!'></i>" ErrorMessage="Enter Driver Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator ID="revVehicleNo" ControlToValidate="txtDriverMobileNo" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Mobile No.!'></i>" ErrorMessage="Invalid Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtDriverMobileNo" Enabled="false" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Representative Name</label>
                                                    <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>--%>
                                                    <asp:TextBox ID="txtRepresentativeName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Representative Mobile</label>
                                                    <span class="pull-right">
                                                        <asp:RegularExpressionValidator ID="revRepresentativeMobileNo" ControlToValidate="txtRepresentativeMobileNo" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Mobile No.!'></i>" ErrorMessage="Invalid Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtRepresentativeMobileNo" Enabled="false" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2" id="dv_TankerType" runat="server" visible="false">
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
                                                    <label>Closing Balance</label>
                                                    <span class="pull-right">
                                                        <%--<asp:RequiredFieldValidator ID="rfvClosingBalanceAfterDispatch" runat="server" Display="Dynamic" ControlToValidate="txtClosingBalanceAfterDispatch" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance after Dispatch!'></i>" ErrorMessage="Enter Closing Balance after Dispatch" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                    </span>
                                                    <asp:TextBox ID="txtClosingBalanceAfterDispatch" CssClass="form-control" Enabled="false" MaxLength="10" runat="server" Text="0"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Next Tanker Date</label>
                                                    <span class="pull-right">
                                                        <%-- <asp:RequiredFieldValidator ID="rfvNextTankerRequiredDate" runat="server" Display="Dynamic" ControlToValidate="txtNextTankerRequiredDate" Text="<i class='fa fa-exclamation-circle' title='Enter Next Tanker Required Date!'></i>" ErrorMessage="Enter Next Tanker Required Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                    </span>
                                                    <div class="input-group">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text">
                                                                <i class="far fa-calendar-alt"></i>
                                                            </span>
                                                        </div>
                                                        <asp:TextBox ID="txtNextTankerRequiredDate" autocomplete="off" Enabled="false" CssClass="form-control DateAdd" data-date-format="dd/MM/yyyy" data-date-start-date="0d" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Next Tanker Shift</label>
                                                    <span class="pull-right">
                                                        <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                    </span>
                                                    <asp:DropDownList ID="ddlNextTankerShift" Enabled="false" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Morning" Value="Morning"></asp:ListItem>
                                                        <asp:ListItem Text="Evening" Value="Evening"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Tanker Capacity</label>
                                                    <span class="pull-right">
                                                        <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                                        <asp:RegularExpressionValidator ID="revTankerCapacity" ControlToValidate="txtTankerCapacity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtTankerCapacity" CssClass="form-control" Enabled="false" placeholder="Tanker Capacity" runat="server" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Required No. Of Tanker</label>
                                                    <span class="pull-right">
                                                        <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                                        <asp:RegularExpressionValidator ID="revTankerCount" ControlToValidate="txtTankerCount" Display="Dynamic" ValidationExpression="^\d+" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtTankerCount" CssClass="form-control" Enabled="false" placeholder="Required No. Of Tanker" runat="server" MaxLength="2"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </asp:View>
                            <asp:View ID="v_MilkQualityDetails" runat="server">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Milk Quality Details</legend>
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
                                                        <%--   <asp:ListItem Text="Front" Value="F"></asp:ListItem>
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
                                                        <%-- <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                            <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                            <asp:ListItem Text="Off Taste" Value="Off Taste"></asp:ListItem>
                                            <asp:ListItem Text="Slightly Off Taste" Value="Slightly Off Taste"></asp:ListItem>
                                            <asp:ListItem Text="Sour" Value="Sour"></asp:ListItem>
                                            <asp:ListItem Text="Curd" Value="Curd"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--<div class="col-md-4">
                                    <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuantity" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvMilkQuantity_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revMilkQuantity" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revMilkQuantity_S" Enabled="false" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtMilkQuantity" CssClass="form-control" Width="100%" placeholder="Milk Quantity (In KG)" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                                            <div class="col-md-3">
                                                <label>Temperature (°C)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvTemprature_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator ID="revTemprature_S" Enabled="false" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtTemprature" autocomplete="off" CssClass="form-control" placeholder="Temperature" runat="server" MaxLength="4"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>Acidity %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvAcidity" runat="server" Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Enter Acidity!'></i>" ErrorMessage="Enter Acidity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvAcidity_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Enter Acidity!'></i>" ErrorMessage="Enter Acidity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revAcidity" ControlToValidate="txtAcidity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator ID="revAcidity_S" Enabled="false" ControlToValidate="txtAcidity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Minimum Acidity % required 0 and maximum 1." Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Minimum Acidity % required 0 and maximum 1.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="0" MaximumValue="1"></asp:RangeValidator>

                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Minimum no. of Acidity % required 0 and maximum 1." Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Minimum Acidity % required 0 and maximum 1.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="0" MaximumValue="1"></asp:RangeValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtAcidity" autocomplete="off" CssClass="form-control" placeholder="Acidity %" runat="server"></asp:TextBox>
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
                                                <label>Fat % (1.5 - 10)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvFat_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revFat" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator ID="revFat_S" Enabled="false" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum FAT % required 1.5 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 1.5 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="1.5" MaximumValue="10"></asp:RangeValidator>

                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 1.5 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 1.5 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="1.5" MaximumValue="10"></asp:RangeValidator>

                                                </span>
                                                <asp:TextBox ID="txtFat" autocomplete="off" Width="100%" CssClass="form-control" placeholder="Fat %" runat="server" AutoPostBack="true" MaxLength="6" OnTextChanged="txtCLR_TextChanged"></asp:TextBox>
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
                                                <asp:TextBox ID="txtCLR" autocomplete="off" Width="100%" CssClass="form-control" placeholder="CLR" runat="server" AutoPostBack="true" MaxLength="6" OnTextChanged="txtCLR_TextChanged"></asp:TextBox>
                                            </div>
                                            
                                            <div class="col-md-3">
                                                <label>SNF %<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvSNF_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revSNF" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="revSNF_S" Enabled="false" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSNF" Enabled="false" Width="100%" CssClass="form-control" placeholder="SNF %" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3">
                                                <label>MBRT</label>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="rfvMBRT" runat="server" Display="Dynamic" ControlToValidate="txtMBRT" Text="<i class='fa fa-exclamation-circle' title='Enter MBRT!'></i>" ErrorMessage="Enter MBRT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvMBRT_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtMBRT" Text="<i class='fa fa-exclamation-circle' title='Enter MBRT!'></i>" ErrorMessage="Enter MBRT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="revMBRT" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="revMBRT_S" Enabled="false" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtMBRT" autocomplete="off" CssClass="form-control" placeholder="MBRT Exp. 0.00" runat="server"></asp:TextBox>
                                            </div>


                                            <div class="col-md-3">
                                                <label>Alcohol</label>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="rfvAlcohol" runat="server" Display="Dynamic" ControlToValidate="ddlAlcohol" Text="<i class='fa fa-exclamation-circle' title='Select Alcohol!'></i>" ErrorMessage="Select Alcohol" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>--%>

                                                    <%--<asp:RequiredFieldValidator ID="rfvAlcohol_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlAlcohol" Text="<i class='fa fa-exclamation-circle' title='Select Alcohol!'></i>" ErrorMessage="Select Alcohol" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
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
                                                            <asp:TemplateField HeaderText="Chamber Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSealLocation" Visible="false" runat="server" Text='<%# Eval("V_SealLocation") %>'></asp:Label>
                                                                    <asp:Label ID="V_SealLocationAlias" runat="server" Text='<%# Eval("V_SealLocationAlias") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                                            <asp:TemplateField HeaderText="MBRT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMBRT" runat="server" Text='<%# Eval("V_MBRT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Alcohol">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAlcohol" runat="server" Text='<%# Eval("V_Alcohol") %>'></asp:Label>
                                                                </ItemTemplate>
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
                                    </fieldset>
                                    <div class="row">
                                        <div class="col-md-6" runat="server" id="dv_MilkSampleDetails" visible="false">
                                            <fieldset>
                                                <legend>Milk Sample Details</legend>
                                                <p><i style="color: red; font-size: 12px;">Note:- Atleast 1 Sample Detail is mandatory.</i></p>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-3" runat="server" id="dv_ChamberOfSampleDetails" visible="false">
                                                        <label>Chamber<span style="color: red;">*</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfvChamberOfSampleNo" runat="server" Display="Dynamic" ControlToValidate="ddlChamberOfSampleNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RequiredFieldValidator>
                                                        </span>
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlChamberOfSampleNo" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="Single" Value="S"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" runat="server" id="dv_SampleNo" visible="false">
                                                        <label>Sample No.<span style="color: red;">*</span></label>
                                                        <span class="pull-right">
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtSampleNo" ValidationExpression="^[a-zA-Z0-9]{1,40}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="rfvSampleNo" runat="server" Display="Dynamic" ControlToValidate="txtSampleNo" Text="<i class='fa fa-exclamation-circle' title='Enter Sample No.!'></i>" ErrorMessage="Enter Sample No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RequiredFieldValidator>
                                                        </span>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSampleNo" autocomplete="off" placeholder="Sample No." CssClass="form-control" runat="server" MaxLength="25"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" runat="server" id="dv_SampleRemark" visible="false">
                                                        <label>Sample Remark</label>
                                                        <span class="pull-right">
                                                            <asp:RegularExpressionValidator ID="revSampleRemark" runat="server" Display="Dynamic" ControlToValidate="txtSampleRemark" ValidationExpression="^[a-zA-Z0-9'.\s]{1,200}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RegularExpressionValidator>
                                                        </span>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtSampleRemark" autocomplete="off" placeholder="Sample Remark" CssClass="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1" runat="server" id="dv_SampleNoGrid" visible="false">
                                                        <div class="form-group">
                                                            <asp:Button ID="BtnAddSample" CssClass="btn btn-primary right" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="BtnAddSample_Click" ValidationGroup="AddSampleNo" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" runat="server" id="dv_gv_sampleNoDetails" visible="false">
                                                        <div class="form-group table-responsive">
                                                            <asp:GridView ID="gv_SampleNoDetails" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Chamber Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSealLocation" runat="server" Text='<%# Eval("V_SealLocation").ToString() == "F" ? "Front" : Eval("V_SealLocation").ToString() == "S" ? "Single" : "Rear" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sample No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSampleNo" runat="server" Text='<%# Eval("V_SampleNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sample Remark">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSampleRemark" runat="server" Text='<%# Eval("V_SampleRemark") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtnSampleNoDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')" OnClick="lnkbtnSampleNoDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>


                                        <%--<div class="col-md-12" runat="server" id="dv_AdulterationHeaderSection" visible="false">
                                        <fieldset>
                                            <legend>Adulteration Test Details</legend>
                                            <p><i style="color: red; font-size: 12px;">Note:- All Adulteration Tests are mandatory.</i></p>

                                            <div class="col-md-3" runat="server" id="dv_CompartmentTypeforAdulteration" visible="false">
                                                <label>Chamber<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCompartmentTypeforQuality" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentTypeforQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddAdulteration"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlCompartmentTypeforQuality" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Single" Value="S"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" id="dv_AdulterationType" visible="false">
                                                <label>Test Type<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvAdulterationType" runat="server" Display="Dynamic" ControlToValidate="ddlAdulterationType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Adulteration Test Type!'></i>" ErrorMessage="Select COB" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddAdulteration"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlAdulterationType" CssClass="form-control" runat="server" OnInit="ddlAdulterationType_Init">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" id="dv_AdulterationValue" visible="false">
                                                <label>Test Value<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvAdulterationValue" runat="server" Display="Dynamic" ControlToValidate="ddlAdulterationValue" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Adulteration Value!'></i>" ErrorMessage="Enter Adulteration Value" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddAdulteration"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlAdulterationValue" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Positive" Value="Positive"></asp:ListItem>
                                                        <asp:ListItem Text="Negative" Value="Negative"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-1" runat="server" id="dv_btnAdulterationTest" visible="false">
                                                <div class="form-group">
                                                    <asp:Button ID="btnAdulterationTest" CssClass="btn btn-facebook button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAdulterationTest_Click" ValidationGroup="AddAdulteration" />
                                                </div>
                                            </div>
                                            <div class="col-md-12" style="margin-top: 20px;" runat="server" id="dv_AdulterationTestGridVIew" visible="false">
                                                <div class="form-group table-responsive">
                                                    <asp:GridView ID="gv_AdulterationTestDetails" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
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
                                                                    <asp:Label ID="lblAdulterationValue" runat="server" Text='<%# Eval("V_AdulterationValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAdulterationDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')" OnClick="lnkAdulterationDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>--%>

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
                                <%--<div class="col-md-4">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Seal Details</legend>
                                        <div class="mb-1">
                                            <i style="color: red;"><b>Note</b>:-
                                            <br />
                                                (a)<b>Single Chamber:</b> Minimum seal required 1 and maximum 10.<br />
                                                (b)<b>Dual Chamber:</b> Minimum seal required 2 and maximum 10.</i><hr />
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Total Seals<span style="color: red;"> *</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvTotalSeals" runat="server" Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Enter Total Seals!'></i>" ErrorMessage="Enter Total Seals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="rvTotalSeals" runat="server" ErrorMessage="Minimum no. of seal required 1 and maximum 10." Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Minimum no. of seal required 1 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add" Type="Integer" MinimumValue="5" MaximumValue="10"></asp:RangeValidator>
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
                                                            <th>Seal Location</th>
                                                            <th>Seal Remark</th>
                                                        </tr>
                                                        <div id="dvSealDetails" runat="server"></div>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>--%>
                            </asp:View>
                        </asp:MultiView>
                        <div class="col-lg-12" style="text-align: right;">
                            <asp:ValidationSummary runat="server" ID="VSummary" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                            <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-default" Visible="false" Text="Previous" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnSubmit" Visible="false" CssClass="btn btn-primary right" ValidationGroup="Save" runat="server" Text="Submit" OnClientClick="return ValidatePage();" />
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
                                <asp:BoundField DataField="Office_Name" HeaderText="CC Name" />
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
                                <%--<asp:BoundField DataField="TotalSeal" HeaderText="Total Seals" />
										<asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
										<asp:BoundField DataField="DT_ArrivalDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Arrival Time" />
                                        <asp:BoundField DataField="V_MilkType" HeaderText="Milk Type" />
										
                                --%>
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtArrivalTime').timepicker();

    </script>

    <script>
        $(document).ready(function () {
            $(".dataTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            $("header").addClass("fix");
        });


        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Submit');
            }
            debugger;
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
