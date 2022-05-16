<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ReceiveCanesAtQA.aspx.cs" Inherits="mis_MilkCollection_ReceiveCanesAtQA" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box">
                <div class="box-header">
                    <h3 class="box-title">Canes QC Entry</h3>
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
                                <legend>Canes Details</legend>
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
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="Save"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                                    <div class="col-md-2">
                                        <label>Society<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvUnitName" runat="server" Display="Dynamic" ControlToValidate="ddlUnitName" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Unit Name!'></i>" ErrorMessage="Select Unit Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlUnitName"  CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
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
                                            <label>Sample No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlchallanno" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Challan No!'></i>" ErrorMessage="Enter Challan No." SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlchallanno" Width="100%" AutoPostBack="true" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlchallanno_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
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
                                        <%--<div class="col-md-6">
                                            <label>Milk Quality<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server" OnInit="ddlMilkQuality_Init">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>

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
                                                <label>CLR (22 - 30)<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtNetCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 22 and maximum 30." Display="Dynamic" ControlToValidate="txtNetCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 22 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="22" MaximumValue="30"></asp:RangeValidator>
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
                                                <asp:TextBox ID="txttemperature" Text="4" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>     
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Acidity<span style="color: red;">*</span></label>
                                                 <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtACIDITY" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtACIDITY" 
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter ACIDITY!'></i>"
                                                        ErrorMessage="Enter ACIDITY"  SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </span>
                                            <asp:TextBox ID="txtACIDITY" Text="0.117" autocomplete="off" runat="server" 
                                                 CssClass="form-control" MaxLength="10">                                               
                                            </asp:TextBox>
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
                    <h3 class="box-title">Canes QC Entry Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">


                    <div class="row">
                        <div class="col-md-2">
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
                                    <asp:TextBox ID="txtfilterdate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlCC_flt" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlCC_flt"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>


                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvReceivedEntry" ShowFooter="True" CssClass="datatable table table-bordered" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found." OnRowCommand="gvReceivedEntry_RowCommand">
                            <Columns>
                              <%--  <asp:TemplateField HeaderText="Arrival Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerArrivalDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("DT_TankerArrivalDate"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="C_ReferenceNo" HeaderText="Reference No." />
                                <asp:BoundField DataField="V_SampleNo" HeaderText="Sample No." />                              
                                <asp:BoundField DataField="Office_Name" HeaderText="Society" />
                                <asp:BoundField DataField="Office_Code" HeaderText="Society Code" />
                                <asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="13%" ItemStyle-Width="13%">                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                        <asp:DropDownList ID="gvddlV_MilkType" Visible="false" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="Buf">Buf</asp:ListItem>
                                            <asp:ListItem Value="Cow">Cow</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="13%" ItemStyle-Width="13%">                     
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_MilkQuality" runat="server" Text='<%# Eval("V_MilkQuality") %>'></asp:Label>
                                        <%--<span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvBufMilkQuantity" runat="server" Display="Dynamic"
                                                                    ControlToValidate="gvtxtI_MilkQuantity"
                                                                    Text="<i class='fa fa-exclamation-circle' title='दूध की मात्रा दर्ज करें'></i>"
                                                                    ErrorMessage="दूध की मात्रा दर्ज करें'" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="update" ></asp:RequiredFieldValidator>
                                                              <asp:RangeValidator ID="rvMilkQuantity" runat="server" ErrorMessage="मात्रा शून्य नहीं होनी चाहिए।" Display="Dynamic" ControlToValidate="gvtxtI_MilkQuantity" Text="<i class='fa fa-exclamation-circle' title='मात्रा शून्य नहीं होनी चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update" Type="Double" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
                                                            </span>--%>
                                        <asp:DropDownList ID="gvddlV_MilkQuality" CssClass="form-control" Visible="false" runat="server" >
                                             <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                            <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                            <asp:ListItem Value="Slightly Off Taste">Slightly Off Taste</asp:ListItem>
                                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                                             <asp:ListItem Value="Off Taste">Off Taste</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Quantity (In KG)" HeaderStyle-Width="13%" ItemStyle-Width="13%">                     
                                    <ItemTemplate>
                                        <asp:Label ID="lblI_MilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                        <%--<span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvBufMilkQuantity" runat="server" Display="Dynamic"
                                                                    ControlToValidate="gvtxtI_MilkQuantity"
                                                                    Text="<i class='fa fa-exclamation-circle' title='दूध की मात्रा दर्ज करें'></i>"
                                                                    ErrorMessage="दूध की मात्रा दर्ज करें'" SetFocusOnError="true" ForeColor="Red"
                                                                    ValidationGroup="update" ></asp:RequiredFieldValidator>
                                                              <asp:RangeValidator ID="rvMilkQuantity" runat="server" ErrorMessage="मात्रा शून्य नहीं होनी चाहिए।" Display="Dynamic" ControlToValidate="gvtxtI_MilkQuantity" Text="<i class='fa fa-exclamation-circle' title='मात्रा शून्य नहीं होनी चाहिए।'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update" Type="Double" MinimumValue="1" MaximumValue="10000"></asp:RangeValidator>
                                                            </span>--%>
                                        <asp:TextBox ID="gvtxtI_MilkQuantity" CssClass="form-control" Visible="false" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FAT %" HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_FAT" runat="server" Text='<%# Eval("D_FAT") %>'></asp:Label>
                                       <%--<span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtD_FAT" runat="server" Display="Dynamic" ControlToValidate="gvtxtD_FAT" Text="<i class='fa fa-exclamation-circle' title='Enter Fat %!'></i>" ErrorMessage="Enter Fat %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revtxtD_FAT" ControlToValidate="gvtxtD_FAT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="gvtxtD_FAT" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>

                                                </span>--%>
                                         <asp:TextBox ID="gvtxtD_FAT" CssClass="form-control" Visible="false" runat="server" Text='<%# Eval("D_FAT") %>' OnTextChanged="gvtxtD_FAT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CLR" HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                    <EditItemTemplate>
                                        
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_CLR" runat="server" Text='<%# Eval("D_CLR") %>'></asp:Label>
                                        <%--<span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" ControlToValidate="gvtxtD_CLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="gvtxtD_CLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 30." Display="Dynamic" ControlToValidate="gvtxtD_CLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                </span>--%>
                                        <asp:TextBox ID="gvtxtD_CLR" CssClass="form-control" Visible="false" runat="server" Text='<%# Eval("D_CLR") %>' OnTextChanged="gvtxtD_CLR_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_SNF" runat="server" Text='<%# Eval("D_SNF") %>'></asp:Label>
                                        <asp:TextBox ID="gvtxtD_SNF" CssClass="form-control" Enabled="false" Visible="false" runat="server" Text='<%# Eval("D_SNF") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Temperature" HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_Temp" runat="server" Text='<%# Eval("V_Temp") %>'></asp:Label>
                                        <%--<span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="gvtxtV_Temp" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="gvtxtV_Temp" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="update"></asp:RegularExpressionValidator>

                                                </span>--%>
                                        <asp:TextBox ID="gvtxtV_Temp" CssClass="form-control" Visible="false" runat="server" Text='<%# Eval("V_Temp") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acidity" HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                    <EditItemTemplate>
                                       
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_Acidity" runat="server" Text='<%# Eval("V_Acidity") %>'></asp:Label>
                                       <%--  <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtACIDITY" runat="server" Display="Dynamic"
                                                        ControlToValidate="gvtxtV_Acidity" 
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter ACIDITY!'></i>"
                                                        ErrorMessage="Enter ACIDITY"  SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="update"></asp:RequiredFieldValidator>
                                                </span>--%>
                                         <asp:TextBox ID="gvtxtV_Acidity" CssClass="form-control" Visible="false" runat="server" Text='<%# Eval("V_Acidity") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("I_SampleID") %>' Visible='<%# Eval("Status").ToString()==""?true:false %>' OnClientClick="return confirm('Do you really want to delete Record?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("I_SampleID") %>'  Visible='<%# Eval("Status").ToString()==""?true:false %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="update" runat="server" CommandName="UpdateRecord" CommandArgument='<%# Eval("I_SampleID") %>' OnClientClick="return confirm('Do you really want to Update Record?');">Update</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
   <script src="js/buttons.colVis.min.js"></script>
   
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100],
            iDisplayLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
            "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('Canes QC Entry Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Canes QC Entry Details').bold().fontsize(3).toUpperCase(),
                    filename: 'CanesQCEntryDetails',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
</script>

</asp:Content>
