<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ReceiveTankerAtQC_RMRD.aspx.cs" Inherits="mis_RMRD_ReceiveTankerAtQC_RMRD" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager runat="server" ID="SM1">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Receive Tanker at QC</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <asp:MultiView ID="mv_FormWizard" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="v_TankerDetails" runat="server">
                                    </asp:View>

                                    <asp:View ID="v_MilkQualityDetails" runat="server">
                                    </asp:View>
                                </asp:MultiView>
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

                                                    <asp:DropDownList ID="ddlReferenceNo" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlReferenceNo_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2 hidden">
                                                <div class="form-group">
                                                    <label>Sample No.<span style="color: red;">*</span></label>
                                                    <%-- <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvSampleNo" runat="server" Display="Dynamic" ControlToValidate="ddlSampleNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Challan No!'></i>" ErrorMessage="Enter Sample No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </span>--%>
                                                    <asp:DropDownList ID="ddlSampleNo" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlSampleNo_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-2 hidden">
                                                <label>Unit Name<span style="color: red;">*</span></label>
                                                <%--<span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvUnitName" runat="server" Display="Dynamic" ControlToValidate="ddlUnitName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Unit Name!'></i>" ErrorMessage="Select Unit Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>--%>
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


                                            <div class="col-md-2" id="dv_TankerType" runat="server" visible="true">
                                                <label>Tanker Type<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlTankerType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker Type!'></i>" ErrorMessage="Select Tanker Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
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
                                <asp:Panel ID="panel" runat="server" Visible="false">
                                    <fieldset>
                                        <legend>Sample Milk Quality Details</legend>
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvSampleMilkQualityDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="BMC Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBMCName" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblI_OfficeID" CssClass="hidden" runat="server" Text='<%# Eval("I_OfficeID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sample No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSampleNo" runat="server" Text='<%# Eval("V_SampleNo") %>'></asp:Label>
                                                            <asp:Label ID="lblBI_MilkCollectionID" runat="server" CssClass="hidden" Text='<%# Eval("BI_MilkCollectionID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("V_SealLocation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Quality">
                                                        <ItemTemplate>
                                                             <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvddlgvMilkQuality" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="ddlgvMilkQuality" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:DropDownList ID="ddlgvMilkQuality" runat="server" CssClass="form-control select2">
                                                            </asp:DropDownList>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat % (3.2 - 10)">
                                                        <ItemTemplate>
                                                              <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvtxtgvFat" runat="server" Display="Dynamic" ControlToValidate="txtgvFat" Text="<i class='fa fa-exclamation-circle' title='Enter FAT!'></i>" ErrorMessage="Enter FAT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revgvFat" Enabled="true" ControlToValidate="txtgvFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                                <asp:RangeValidator ID="rvgvFat" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtgvFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtgvFat" runat="server" CssClass="form-control" OnTextChanged="txtgvFat_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CLR (20 - 30)">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvtxtgvCLR" runat="server" Display="Dynamic" ControlToValidate="txtgvCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revCLR_S" Enabled="true" ControlToValidate="txtgvCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum CLR % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtgvCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                                 </span>
                                                            <asp:TextBox ID="txtgvCLR" runat="server" CssClass="form-control" OnTextChanged="txtgvCLR_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvtxtgvSNF" runat="server" Display="Dynamic" ControlToValidate="txtgvSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF!'></i>" ErrorMessage="Enter SNF" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtgvSNF" runat="server" CssClass="form-control"></asp:TextBox>

                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Temperature (°C)">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvtxtgvTemp" runat="server" Display="Dynamic" ControlToValidate="txtgvTemp" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature!'></i>" ErrorMessage="Enter Temperature" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revTemprature_S" ControlToValidate="txtgvTemp" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                                 </span>
                                                            <asp:TextBox ID="txtgvTemp" runat="server" CssClass="form-control"></asp:TextBox>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Milk Quality Details</legend>
                                            <div class="row">
                                                <div class="col-md-3 hidden">
                                                    <label>Chamber Type<span style="color: red;">*</span></label>
                                                    <%-- <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCompartmentType" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>

                                                    <asp:RequiredFieldValidator ID="rfvCompartmentType_S" Enabled="true" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                </span>--%>
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
                                                        <asp:RequiredFieldValidator ID="rfvMilkQuality_S" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server" OnInit="ddlMilkQuality_Init">
                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <label>Temperature (°C)<span style="color: red;">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvTemprature_S" runat="server" Display="Dynamic" ControlToValidate="txtTemprature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revTemprature_S" ControlToValidate="txtTemprature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                    </span>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtTemprature" autocomplete="off" CssClass="form-control" placeholder="Temperature" runat="server" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Acidity %<span style="color: red;">*</span></label>
                                                    <span class="pull-right">

                                                        <asp:RequiredFieldValidator ID="rfvAcidity_S" runat="server" Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Enter Acidity!'></i>" ErrorMessage="Enter Acidity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="revAcidity_S" ControlToValidate="txtAcidity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Minimum no. of Acidity % required 0 and maximum 1." Display="Dynamic" ControlToValidate="txtAcidity" Text="<i class='fa fa-exclamation-circle' title='Minimum Acidity % required 0 and maximum 1.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="0" MaximumValue="1"></asp:RangeValidator>
                                                    </span>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtAcidity" autocomplete="off" CssClass="form-control" placeholder="Acidity %" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>COB<span style="color: red;">*</span></label>
                                                    <span class="pull-right">


                                                        <asp:RequiredFieldValidator ID="rfvCOB_S" runat="server" Display="Dynamic" ControlToValidate="ddlCOB" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select COB!'></i>" ErrorMessage="Select COB" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>


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

                                                        <asp:RequiredFieldValidator ID="rfvFat_S" Enabled="true" runat="server" Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="revFat_S" Enabled="true" ControlToValidate="txtFat" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,1})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtFat" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                                    </span>
                                                    <asp:TextBox ID="txtFat" autocomplete="off" Width="100%" CssClass="form-control" placeholder="Fat %" runat="server" AutoPostBack="true" MaxLength="6" OnTextChanged="txtCLR_TextChanged"></asp:TextBox>
                                                </div>


                                                <div class="col-md-3">
                                                    <label>CLR (20 - 30)<span style="color: red;">*</span></label>
                                                    <span class="pull-right">


                                                        <asp:RequiredFieldValidator ID="rfvCLR_S" Enabled="true" runat="server" Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revCLR_S" Enabled="true" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>

                                                        <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum CLR % required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtCLR" autocomplete="off" Width="100%" CssClass="form-control" placeholder="CLR" runat="server" AutoPostBack="true" MaxLength="6" OnTextChanged="txtCLR_TextChanged"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3">
                                                    <label>SNF %<span style="color: red;">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvSNF_S" Enabled="true" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revSNF" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="revSNF_S" Enabled="true" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
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
                                                        <asp:RegularExpressionValidator ID="revMBRT_S" Enabled="true" ControlToValidate="txtMBRT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
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

                                                <div class="col-md-3">
                                                    <label>Adulteration Test<span style="color: red;">*</span></label>
                                                    <%--<span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Enabled="false" ControlToValidate="txtAlcoholperc" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RegularExpressionValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Enabled="false" ControlToValidate="txtAlcoholperc" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                </span>--%>
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlAdulterationTest" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlAdulterationTest_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>



                                            </div>
                                        </fieldset>
                                        <div class="row">

                                            <div class="col-md-6" runat="server" id="milktestdetail" visible="true">
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
                                                                        <asp:Label ID="lblSealLocation" runat="server" Text='<%# Eval("V_SealLocation").ToString() %>'></asp:Label>
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

                                </asp:Panel>


                                <div class="col-lg-12" style="text-align: right;">
                                    <asp:ValidationSummary runat="server" ID="VSummary" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                                    <asp:Button ID="btnPrevious" runat="server" CssClass="btn btn-default" Visible="false" Text="Previous" OnClick="btnPrevious_Click" />&nbsp;
                            <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary" Visible="false" Text="Next" OnClick="btnNext_Click" />&nbsp;
                            <asp:Button ID="btnSubmit" Visible="true" CssClass="btn btn-primary right" ValidationGroup="Save" runat="server" Text="Submit" OnClientClick="return ValidatePage();" />
                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-Manish" runat="server">
                <div class="box-header">
                    <h3 class="box-title">Received Tanker at QC Details</h3>
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
                                        <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_TankerArrivalDateAtQC"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="C_ReferenceNo" HeaderText="Reference No." />
                                <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                <asp:BoundField DataField="V_SampleNo" HeaderText="Sample No." />
                                <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />

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
                                <asp:BoundField DataField="D_FAT" HeaderText="FAT %" />
                                <asp:BoundField DataField="D_CLR" HeaderText="CLR" />
                                <asp:BoundField DataField="D_SNF" HeaderText="SNF %" />
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
    <%-- <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtArrivalTime').timepicker();
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
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

