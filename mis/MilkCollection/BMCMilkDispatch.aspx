<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="BMCMilkDispatch.aspx.cs" Inherits="mis_MilkCollection_BMCMilkDispatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="content-wrapper">

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

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">BMC Milk Dispatch</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Office Detail</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Society Name </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Block <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtBlock" CssClass="form-control" MaxLength="20" placeholder="Block" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Dispatch</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shift</label>

                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlShift" OnInit="ddlShift_Init" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
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
                                <div class="col-md-3">
                                                <label>Tanker Type<span style="color: red;">*</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvTankerType" runat="server" Display="Dynamic" ControlToValidate="ddlTankerType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Tanker Type!'></i>" ErrorMessage="Select Tanker Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddQuality"></asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlTankerType" AutoPostBack="true" Enabled="false" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlTankerType_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Single Chamber" Value="S"></asp:ListItem>
                                                        <asp:ListItem Text="Dual Chamber" Value="D"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                ErrorMessage="Enter Vehicle No" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No !'></i>"
                                                ControlToValidate="txtV_VehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtV_VehicleNo" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VehicleNo" Enabled="false" ClientIDMode="Static" MaxLength="13" placeholder="XX-00-XX-0000"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Name <span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                ErrorMessage="Enter Driver Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Name !'></i>"
                                                ControlToValidate="txtV_DriverName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ValidationExpression="^[^'@%#$&=^!~?]+$" ValidationGroup="a" runat="server" ControlToValidate="txtV_DriverName" ErrorMessage="Enter Valid Driver Name." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Driver Name. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_DriverName" Enabled="false" MaxLength="40" placeholder="Enter Driver Name"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Mobile No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                ErrorMessage="Enter Driver Mobile No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile !'></i>"
                                                ControlToValidate="txtV_DriverMobileNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" ValidationGroup="a" runat="server" ControlToValidate="txtV_DriverMobileNo" ErrorMessage="Enter Driver Mobile Number." Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_DriverMobileNo" Enabled="false" MaxLength="10" placeholder="Enter Driver Mobile"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Tester Name <span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                                ErrorMessage="Enter Tester Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tester Name !'></i>"
                                                ControlToValidate="txtV_TesterName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationExpression="^[^'@%#$&=^!~?]+$" ValidationGroup="a" runat="server" ControlToValidate="txtV_TesterName" ErrorMessage="Enter Valid Tester Name." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Driver Name. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_TesterName" Enabled="false" MaxLength="40" placeholder="Enter Driver Name"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Tester Mobile No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                                ErrorMessage="Enter Tester Mobile No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tester Mobile !'></i>"
                                                ControlToValidate="txtV_TesterMobileNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" ValidationExpression="^[6789]\d{9}$" ValidationGroup="a" runat="server" ControlToValidate="txtV_TesterMobileNo" ErrorMessage="Enter Tester Mobile Number." Text="<i class='fa fa-exclamation-circle' title='Enter Driver Mobile Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_TesterMobileNo" Enabled="false" MaxLength="10" placeholder="Enter Driver Mobile"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
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
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Dispatch Time<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDispatchTime" runat="server" Display="Dynamic" ControlToValidate="txtDispatchTime" Text="<i class='fa fa-exclamation-circle' title='Enter Dispatch Time!'></i>" ErrorMessage="Enter Dispatch Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDispatchTime" ReadOnly="true" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Adulteration Test<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select Adulteration Test" Text="<i class='fa fa-exclamation-circle' title='Select Adulteration Test !'></i>"
                                                ControlToValidate="ddlAdulterationTest" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlAdulterationTest" AutoPostBack="true" OnSelectedIndexChanged="ddlAdulterationTest_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>



                            </fieldset>
                        </div>
                    </div>
                    <div class="row" runat="server" visible="false" id="divmilkdispatch">
                        <div class="col-md-12">

                            <fieldset>
                                <legend>Net Milk Dispatch</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Milk Supply To
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlDCS" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select BMC!'></i>" ErrorMessage="Select BMC" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlDCS" ValidationGroup="a" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Milk Dispatch Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Milk Dispatch Type" Text="<i class='fa fa-exclamation-circle' title='Select Milk Dispatch Type !'></i>"
                                                ControlToValidate="ddlMilkDispatchtype" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlMilkDispatchtype" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkDispatchtype_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem Value="Tanker">Tanker</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                            <label>Chamber Type<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvCompartmentType" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                                <asp:RequiredFieldValidator ID="rfvCompartmentType_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlCompartmentType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Chamber Type!'></i>" ErrorMessage="Select Chamber Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCompartmentType" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlCompartmentType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Front" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Rear" Value="R"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                <div class="col-md-3">
                                    <label>Milk Quality<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlMilkQuality_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                            <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>Milk Quantity (In KG)<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuantity_S" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity (In KG)!'></i>" ErrorMessage="Enter Milk Quantity (In KG)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revMilkQuantity_S" ControlToValidate="txtMilkQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtMilkQuantity" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtMilkQuantity_TextChanged" autocomplete="off" CssClass="form-control" Width="100%" placeholder="Milk Quantity (In KG)" runat="server" MaxLength="7"></asp:TextBox>
                                        <asp:Label ID="lblD_VehicleCapacity" Visible="false" runat="server"></asp:Label>
                                    </div>
                                </div>




                                


                                <div class="col-md-3">
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

                                <div class="col-md-3">
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

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>SNF %<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtnetsnf" Text="<i class='fa fa-exclamation-circle' title='Enter SNF %!'></i>" ErrorMessage="Enter SNF %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revSNF_S" ControlToValidate="txtnetsnf" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtnetsnf" Width="100%" Enabled="false" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="SNF %" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Temperature (°C)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txttemperature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txttemperature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txttemperature" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" MaxLength="3" CssClass="form-control" runat="server" OnTextChanged="txttemperature_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Scale Reading<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ControlToValidate="txtScaleReading" Text="<i class='fa fa-exclamation-circle' title='Enter Scale Reading!'></i>" ErrorMessage="Enter Scale Reading %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtScaleReading" Width="100%" onkeypress="return validateDec(this,event)" CssClass="form-control" placeholder="Scale Reading" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sample No<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtSampalNo" Text="<i class='fa fa-exclamation-circle' title='Enter sample No!'></i>" ErrorMessage="Enter sample No" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtSampalNo" Width="100%" CssClass="form-control" placeholder="sample No" runat="server" MaxLength="6"></asp:TextBox>

                                    </div>
                                </div>


                            </fieldset>

                        </div>
                        <div class="row" runat="server" visible="false" id="divCollectionDetail">

                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Self Milk Collection</legend>

                                    <div class="table-responsive">
                                        <asp:GridView ID="gv_ViewSelfMilkCollection" OnRowDataBound="gv_ViewSelfMilkCollection_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                            ShowHeader="True" ShowFooter="true">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name") %>' runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblOffice_Id" Text='<%# Eval("Office_Id") %>' Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblFatInKg" Text='<%# Eval("FatInKg") %>' Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblSnfInKg" Text='<%# Eval("SnfInKg") %>' Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblV_Date" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblI_MilkSupplyQtyTotal" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fat %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAT_IN_Per" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CLR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SNF %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNF_IN_Per" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Temp">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <hr />


                                </fieldset>
                            </div>


                            <div class="col-md-6">
                                <fieldset>
                                    <legend>DCS Milk Receive</legend>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gv_dcsmilkreceive" OnRowDataBound="gv_dcsmilkreceive_RowDataBound" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                            ShowHeader="True" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name") %>' runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblOffice_Id" Text='<%# Eval("Office_Id") %>' Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblFatInKg" Text='<%# Eval("FatInKg") %>' Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblSnfInKg" Text='<%# Eval("SnfInKg") %>' Visible="false" runat="server" Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblV_Date" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMilkQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Milk Quantity (In Ltr)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblI_MilkSupplyQtyTotal" runat="server" Font-Bold="true"></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fat %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAT_IN_Per" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CLR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SNF %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNF_IN_Per" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Temp">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTemp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>





                        </div>
                        <div class="col-md-6" runat="server" id="milktestdetail" visible="false">
                            <fieldset>
                                <legend>Adulteration Test Details
                                </legend>

                                <%--  <i style="color: red; font-size: 12px;">Note:- All Adulteration Tests are mandatory.</i>
                                <hr />--%>
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












                    <div class="row" runat="server" visible="false" id="divsealdetail">
                        <div class="col-lg-12">
                            <fieldset>
                                <legend>Tanker Seal Details</legend>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal Type </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="ddlSealtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Sale Type!'></i>" ErrorMessage="Select Sale Type." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSealtype" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="New">New</asp:ListItem>
                                            <asp:ListItem Value="Broken">Broken</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Chamber Type </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="ddlChamberType" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlChamberType" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Chamber">Chamber</asp:ListItem>
                                            <asp:ListItem Value="ValveBox">ValveBox</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal Color </label>
                                        <asp:RequiredFieldValidator ID="rfvSealColor" runat="server" Display="Dynamic" ControlToValidate="ddlSealColor" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Seal Color!'></i>" ErrorMessage="Select Seal Color." SetFocusOnError="true" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSealColor" OnInit="ddlSealColor_Init" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal No.</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="c"
                                                ErrorMessage="Enter Seal No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Seal No !'></i>"
                                                ControlToValidate="txtV_SealNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9a-z-A-Z]+$" ValidationGroup="c" runat="server" ControlToValidate="txtV_SealNo" ErrorMessage="Enter Valid Seal Number." Text="<i class='fa fa-exclamation-circle' title='Enter Valid Seal Number. !'></i>"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealNo" MaxLength="20" placeholder="Enter Seal No"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Seal Remark </label>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_SealRemark" MaxLength="200" placeholder="Enter Seal Remark"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnTankerSealDetails_Click" Style="margin-top: 20px;" ValidationGroup="c" ID="btnTankerSealDetails" Text="Add Seal" />
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

                                        <asp:TemplateField HeaderText="Seal Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSealtype" runat="server" Text='<%# Eval("Sealtype") %>'></asp:Label>
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
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Action</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" Enabled="false" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Submit" OnClientClick="return ValidatePage();" AccessKey="S" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>


                </div>
            </div>


            <div class="box box-success" runat="server" visible="false" id="div_milkdetails">
                <div class="box-header">
                    <h3 class="box-title">Milk Dispatch Challan Details</h3>
                </div>
                <div class="box-body">

                    <div class="col-md-12">
                        <asp:GridView ID="GrdDispatchDetails" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                            EmptyDataText="No Record Found." DataKeyNames="I_EntryID">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvI_CollectionID" runat="server" Visible="false" Text='<%# Eval("I_EntryID") %>'></asp:Label>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Challan No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_ChallanNo" runat="server" Text='<%# Eval("V_ChallanNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Milk Quality">
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_MilkQuality" runat="server" Text='<%# Eval("D_MilkQuality") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Quantity (In KG)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("NetMilkQtyInKG") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FAT %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CLR">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SNF %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Dispatch Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lvlV_MilkDispatchType" runat="server" Text='<%# Eval("V_MilkDispatchType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <a href='../MilkCollection/BMC_ChallanDetails.aspx?BMCCH_ID=<%# new APIProcedure().Encrypt(Eval("I_EntryID").ToString()) %>' target="_blank" title="Print Challan"><i class="fa fa-print"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap-timepicker.js"></script>
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

        $('#txtArrivalTime').timepicker();

    </script>
</asp:Content>

