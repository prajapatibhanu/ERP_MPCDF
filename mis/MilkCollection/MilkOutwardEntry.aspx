<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MilkOutwardEntry.aspx.cs" Inherits="mis_CCDS_MilkInwardEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-body">
                <div class="box-header">
                    <h3 class="box-title">Milk Dispatch Entry</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row" style="border: 1px solid #ced4da; padding: 10px 5px;">
                                <div class="col-md-12">
                                    <div class="fancy-title  title-dotted-border">
                                        <h5>Tanker Details</h5>
                                    </div>
                                </div>
                                <div class="col-md-12 mb-1">
                                    <i style="color: red;">Note: All fields are mandatory.</i>

                                </div>
                                <div class="col-md-4">
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
                                            <asp:TextBox ID="txtDate" autocomplete="off" Enabled="false" CssClass="form-control DateAdd" data-date-start-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Tanker Arrival Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtArrivalDate" Text="<i class='fa fa-exclamation-circle' title='Enter Arrival Date!'></i>" ErrorMessage="Enter Arrival Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtArrivalDate" autocomplete="off" placeholder="Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Tanker Arrival Time<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvArrivalTime" runat="server" Display="Dynamic" ControlToValidate="txtArrivalTime" Text="<i class='fa fa-exclamation-circle' title='Enter Arrival Time!'></i>" ErrorMessage="Enter Arrival Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

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
                                </div>
                                <%--<asp:Panel runat="server" ID="Panel2">

                                    <asp:Panel runat="server" ID="pnl_DispatchTime" Visible="false">
                                        <div class="col-md-4">
                                            <label>Dispatch Time<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDispatchTime" runat="server" Display="Dynamic" ControlToValidate="txtDispatchTime" Text="<i class='fa fa-exclamation-circle' title='Enter Dispatch Time!'></i>" ErrorMessage="Enter Dispatch Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-clock-o"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDispatchTime" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </asp:Panel>--%>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Vehicle No.<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvVehicleNo" runat="server" Display="Dynamic" ControlToValidate="txtVehicleNo" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No.!'></i>" ErrorMessage="Enter Vehicle No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtVehicleNo" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtVehicleNo" CssClass="form-control" MaxLength="13" placeholder="XX-00-XX-0000" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Driver Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDriverName" runat="server" Display="Dynamic" ControlToValidate="txtDriverName" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name!'></i>" ErrorMessage="Enter Driver Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revDriverName" ControlToValidate="txtDriverName" Display="Dynamic" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtDriverName" CssClass="form-control" placeholder="Driver Name" runat="server" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Driver Mobile No.</label>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="rfvDriverMobileNo" runat="server" Display="Dynamic" ControlToValidate="txtDriverMobileNo" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile No.!'></i>" ErrorMessage="Enter Driver Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator ID="revVehicleNo" ControlToValidate="txtDriverMobileNo" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Mobile No.!'></i>" ErrorMessage="Invalid Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtDriverMobileNo" CssClass="form-control" placeholder="Mobile Number" MaxLength="10" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>CC Representative Name</label>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                            <asp:RegularExpressionValidator ID="revRepresentativeName" ControlToValidate="txtRepresentativeName" Display="Dynamic" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtRepresentativeName" CssClass="form-control" placeholder="Representative Name" runat="server" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Representative Mobile No.</label>
                                        <span class="pull-right">
                                            <asp:RegularExpressionValidator ID="revRepresentativeMobileNo" ControlToValidate="txtRepresentativeMobileNo" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Mobile No.!'></i>" ErrorMessage="Invalid Mobile No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtRepresentativeMobileNo" CssClass="form-control" placeholder="Mobile Number" MaxLength="10" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Tanker Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlTankerType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker Type!'></i>" ErrorMessage="Select Tanker Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlTankerType" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlTankerType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Single Chamber" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="Dual Chamber" Value="D"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Closing Balance<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvClosingBalanceAfterDispatch" runat="server" Display="Dynamic" ControlToValidate="txtClosingBalanceAfterDispatch" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance after Dispatch!'></i>" ErrorMessage="Enter Closing Balance after Dispatch" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revClosingBalanceAfterDispatch" ControlToValidate="txtClosingBalanceAfterDispatch" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtClosingBalanceAfterDispatch" CssClass="form-control" MaxLength="10" runat="server" placeholder="Closing Balance"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Next Tanker Date<%--<span style="color: red;">*</span>--%></label>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="rfvNextTankerRequiredDate" runat="server" Display="Dynamic" ControlToValidate="txtNextTankerRequiredDate" Text="<i class='fa fa-exclamation-circle' title='Enter Next Tanker Required Date!'></i>" ErrorMessage="Enter Next Tanker Required Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                            <asp:RegularExpressionValidator ID="revNextTankerRequiredDate" ControlToValidate="txtNextTankerRequiredDate" Display="Dynamic" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Date input!'></i>" ErrorMessage="Invalid Date input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtNextTankerRequiredDate" autocomplete="off" placeholder="Next Tanker Date" CssClass="form-control DateAdd" data-date-start-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Next Tanker Shift</label>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlNextTankerShift" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Morning" Value="Morning"></asp:ListItem>
                                            <asp:ListItem Text="Evening" Value="Evening"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Tanker Capacity</label>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                            <asp:RegularExpressionValidator ID="revTankerCapacity" ControlToValidate="txtTankerCapacity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtTankerCapacity" CssClass="form-control" placeholder="Tanker Capacity" runat="server" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Required No. Of Tanker</label>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="rfvRepresentativeName" runat="server" Display="Dynamic" ControlToValidate="txtRepresentativeName" Text="<i class='fa fa-exclamation-circle' title='Enter Representative Name!'></i>" ErrorMessage="Enter Representative Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                            <asp:RegularExpressionValidator ID="revTankerCount" ControlToValidate="txtTankerCount" Display="Dynamic" ValidationExpression="^\d+" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtTankerCount" CssClass="form-control" placeholder="Required No. Of Tanker" runat="server" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row mt-2" style="border: 1px solid #ced4da; padding: 10px 5px;">
                                <div class="col-md-12">
                                    <div class="fancy-title  title-dotted-border">
                                        <h5>Seal Details</h5>
                                    </div>
                                    <div class="mb-1">
                                        <i style="color: red;">(Note:-
                                            <br />
                                            (a)Single : Minimum no. of Chamber seal required 1 and maximum 10. & Min. 1 and Max. 2 valve box seal)
                                            <br />
                                            (b)Dual : Minimum no. of Chamber seal required 2 and maximum 10. & Min. 1 and Max. 2 valve box when Front & Rear Chamber has filled.)</i>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Total Seals<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTotalSeals" runat="server" Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Enter Total Seals!'></i>" ErrorMessage="Enter Total Seals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfv_S" runat="server" Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Enter Total Seals!'></i>" ErrorMessage="Enter Total Seals" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                    <asp:RangeValidator ID="rvTotalSeals" runat="server" ErrorMessage="Minimum no. of Chamber seal required 1 and maximum 10." Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Minimum no. of Chamber seal required 1 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add" Type="Integer" MinimumValue="1" MaximumValue="10"></asp:RangeValidator>
                                                    <asp:RangeValidator ID="rvTotalSeals_S" runat="server" ErrorMessage="Minimum no. of Chamber seal required 1 and maximum 10." Display="Dynamic" ControlToValidate="txtTotalSeals" Text="<i class='fa fa-exclamation-circle' title='Minimum no. of Chamber seal required 1 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Integer" MinimumValue="1" MaximumValue="10"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtTotalSeals" Text="1" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" Visible="false" CssClass="btn btn-dark button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="Add" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="rowSealDetails" runat="server" style="display: none;">
                                        <div class="col-md-8">
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
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="col-md-12">
                                <div class="fancy-title  title-dotted-border">
                                    <h5>Milk Quality Details</h5>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 36px;">
                                <div class="col-md-4">
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
                                <div class="col-md-4">
                                    <label>Milk Quality<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvMilkQuality_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Good" Selected="True" Value="Good"></asp:ListItem>
                                            <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                            <asp:ListItem Text="Off Taste" Value="Off Taste"></asp:ListItem>
                                            <asp:ListItem Text="Slightly Off Taste" Value="Slightly Off Taste"></asp:ListItem>
                                            <asp:ListItem Text="Sour" Value="Sour"></asp:ListItem>
                                            <asp:ListItem Text="Curd" Value="Curd"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuantity" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvMilkQuantity_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revMilkQuantity" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revMilkQuantity_S" Enabled="false" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtMilkQuantity" CssClass="form-control" Width="100%" placeholder="Milk Quantity (In KG)" runat="server" MaxLength="7"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Temperature (°C)<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature!'></i>" ErrorMessage="Enter Temperature" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvTemprature_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature!'></i>" ErrorMessage="Enter Temperature" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revTemprature_S" Enabled="false" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtTemprature" CssClass="form-control" placeholder="Temperature" runat="server" MaxLength="6"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
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
                                        <asp:TextBox ID="txtAcidity" CssClass="form-control" placeholder="Acidity %" runat="server" MaxLength="6"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
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

                                <div class="col-md-4">
                                    <label>Fat %<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFat" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvFat_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revFat" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revFat_S" Enabled="false" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Minimum FAT % required 1 and maximum 100." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 1 and maximum 100.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality" Type="Double" MinimumValue="1" MaximumValue="100"></asp:RangeValidator>

                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 1 and maximum 100." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 1 and maximum 100.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="1" MaximumValue="100"></asp:RangeValidator>

                                    </span>
                                    <asp:TextBox ID="txtFat" Width="100%" AutoPostBack="true" OnTextChanged="txtCLR_TextChanged" CssClass="form-control" placeholder="Fat %" runat="server" MaxLength="6"></asp:TextBox>
                                </div>


                                <div class="col-md-4">
                                    <label>CLR<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvCLR_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revCLR_S" Enabled="false" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox ID="txtCLR" Width="100%" AutoPostBack="true" OnTextChanged="txtCLR_TextChanged" CssClass="form-control" placeholder="CLR" runat="server" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>SNF %<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvSNF_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revSNF" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revSNF_S" Enabled="false" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox ID="txtSNF" Width="100%" Enabled="false" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>
                                </div>
                                <div class="col-md-4 mt-1">
                                    <label>MBRT</label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="rfvMBRT" runat="server" Display="Dynamic" ControlToValidate="txtMBRT" Text="<i class='fa fa-exclamation-circle' title='Enter MBRT!'></i>" ErrorMessage="Enter MBRT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvMBRT_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtMBRT" Text="<i class='fa fa-exclamation-circle' title='Enter MBRT!'></i>" ErrorMessage="Enter MBRT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>

                                        <asp:RegularExpressionValidator ID="revMBRT" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revMBRT_S" Enabled="false" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtMBRT" CssClass="form-control" placeholder="MBRT Exp. 0.00" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--<div class="col-md-4 mt-1">
                                    <label>Alcohol</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMBRT" runat="server" Display="Dynamic" ControlToValidate="txtAlcohol" Text="<i class='fa fa-exclamation-circle' title='Enter Alcohol!'></i>" ErrorMessage="Enter Alcohol" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvMBRT_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txtAlcohol" Text="<i class='fa fa-exclamation-circle' title='Enter Alcohol!'></i>" ErrorMessage="Enter Alcohol" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtAlcohol" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>

                                        <asp:RegularExpressionValidator ID="revAlcohol" Enabled="false" ControlToValidate="txtAlcohol" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtAlcohol" CssClass="form-control" placeholder="Alcohol" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="col-md-1" runat="server" id="dv_gvMilkQualityDeailsAddButton" visible="false">
                                    <div class="form-group">
                                        <asp:Button ID="btnAddQualityDetails" CssClass="btn btn-dark button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAddQualityDetails_Click" ValidationGroup="AddQuality" />
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
                                                <%--<asp:TemplateField HeaderText="Alcohol">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAlcohol" runat="server" Text='<%# Eval("V_Alcohol") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                                        <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete this record?')" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 20px;" runat="server" id="dv_MilkSampleDetails" visible="false">
                                    <div class="fancy-title  title-dotted-border">
                                        <h5>Milk Sample Details</h5>

                                    </div>
                                    <i style="color: red; font-size: 12px;">Note:- Atleast 1 Sample Detail is mandatory.</i>
                                </div>
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
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtSampleNo" ValidationExpression="^[a-zA-Z0-9]{1,40}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvSampleNo" runat="server" Display="Dynamic" ControlToValidate="txtSampleNo" Text="<i class='fa fa-exclamation-circle' title='Enter Sample No.!'></i>" ErrorMessage="Enter Sample No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSampleNo" placeholder="Sample No." CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" id="dv_SampleRemark" visible="false">
                                    <label>Sample Remark</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="revSampleRemark" runat="server" Display="Dynamic" ControlToValidate="txtSampleRemark" ValidationExpression="^[a-zA-Z0-9'.\s]{1,200}$" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddSampleNo"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSampleRemark" placeholder="Sample Remark" CssClass="form-control" runat="server" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1" runat="server" id="dv_SampleNoGrid" visible="false">
                                    <div class="form-group">
                                        <asp:Button ID="BtnAddSample" CssClass="btn btn-dark button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="BtnAddSample_Click" ValidationGroup="AddSampleNo" />
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px;" runat="server" id="dv_gv_sampleNoDetails" visible="false">
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



                                <div class="col-md-12" style="margin-top: 20px;" runat="server" id="dv_AdulterationHeaderSection" visible="false">
                                    <div class="fancy-title  title-dotted-border">
                                        <h5>Adulteration Test Details</h5>

                                    </div>
                                    <i style="color: red; font-size: 12px;">Note:- All Adulteration Tests are mandatory.</i>
                                </div>
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
                                        <asp:DropDownList ID="ddlAdulterationType" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Urea" Value="Urea"></asp:ListItem>
                                            <asp:ListItem Text="Neutralizer" Value="Neutralizer"></asp:ListItem>
                                            <asp:ListItem Text="Maltodextrin" Value="Maltodextrin"></asp:ListItem>
                                            <asp:ListItem Text="Glucose" Value="Glucose"></asp:ListItem>
                                            <asp:ListItem Text="Sucrose" Value="Sucrose"></asp:ListItem>
                                            <asp:ListItem Text="Salt" Value="Salt"></asp:ListItem>
                                            <asp:ListItem Text="Starch" Value="Starch"></asp:ListItem>
                                            <asp:ListItem Text="Detergent" Value="Detergent"></asp:ListItem>
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
                                        <asp:Button ID="btnAdulterationTest" CssClass="btn btn-dark button-mini" Style="margin-top: 20px;" runat="server" Text="Add" OnClick="btnAdulterationTest_Click" ValidationGroup="AddAdulteration" />
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px;" runat="server" id="dv_AdulterationTestGridVIew" visible="false">
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
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12">
                            <asp:ValidationSummary runat="server" ID="VSummary" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                            <div class="form-group text-center">
                                <label><span id="Span1" runat="server"></span></label>
                                <asp:Button ID="btnSubmit" Visible="false" CssClass="btn btn-primary right" ValidationGroup="Save" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="col-md-12" style="border: 1px solid #ced4da; padding: 10px 5px;">
                            <div class="table-responsive">
                                <div class="fancy-title  title-dotted-border">
                                    <h5>Dispatch Entry Details</h5>
                                </div>
                                <asp:GridView runat="server" ID="gvDispatchEntry" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                        <asp:BoundField DataField="V_ReferenceCode" HeaderText="Reference No." />
                                        <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />
                                        <asp:BoundField DataField="DT_ArrivalDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Arrival Time" />
                                        <asp:BoundField DataField="DT_DispatchDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Dispatch Time" />
                                        <asp:BoundField DataField="V_MilkType" HeaderText="Milk Type" />
                                        <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity (In KG)" />
                                        <asp:BoundField DataField="TotalSeal" HeaderText="Total Seals" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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

