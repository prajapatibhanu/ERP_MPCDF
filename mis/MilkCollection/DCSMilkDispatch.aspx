<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" UICulture="en-IN" AutoEventWireup="true" CodeFile="DCSMilkDispatch.aspx.cs" Inherits="mis_MilkCollection_DCSMilkDispatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                    <h3 class="box-title">DCS Milk Dispatch</h3>
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
                                        <label>Dispatch DateTime</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate" CssClass="form-control" runat="server"></asp:TextBox>
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
                            </fieldset>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <fieldset>
                                <legend>Milk Collection</legend>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Milk Quantity In Ltr
                                        </label>
                                        <asp:TextBox ID="txtQuantity" Enabled="false" MaxLength="5" placeholder="Milk Quantity" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            FAT %
                                        </label>
                                        <asp:TextBox ID="txtFAT" Enabled="false" placeholder="FAT" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>S.N.F. %</label>
                                        <asp:TextBox ID="txtSNF" Enabled="false" placeholder="CLR" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            CLR
                                        </label>
                                        <asp:TextBox ID="txtCLR" Enabled="false" placeholder="CLR" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            FAT (In KG)
                                        </label>
                                        <asp:TextBox ID="MC_txtfatinkg" Enabled="false" placeholder="FAT" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>S.N.F.(In KG)</label>
                                        <asp:TextBox ID="MC_snfinkg" Enabled="false" placeholder="CLR" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                            </fieldset>
                        </div>
                        <div class="col-md-6">
                            <fieldset>
                                <legend>Local Milk Sale</legend>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Sale Milk Quantity (In Ltr)</label>
                                        <asp:TextBox ID="txtSaleMilkQuantity" OnTextChanged="txtSaleMilkQuantity_TextChanged" Enabled="false" MaxLength="5" placeholder="Sale Milk Quantity" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Rate Per Ltr
                                        </label>
                                        <asp:TextBox ID="txtMRPRate" Enabled="false" placeholder="Rate Per Ltr" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Total Amount</label>
                                        <asp:TextBox ID="txtNetAmount" Enabled="false" placeholder="Rate Per Ltr" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">

                            <fieldset>
                                <legend>Net Milk Dispatch</legend>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Milk Qty (In Ltr)</label>
                                        <asp:TextBox ID="txtNetMilkQty" MaxLength="5" Enabled="false" placeholder="Milk Quantity" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            FAT %
                                        </label>
                                        <asp:TextBox ID="txtNetFat" Enabled="false" placeholder="FAT" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>S.N.F. % </label>
                                        <asp:TextBox ID="txtnetsnf" Enabled="false" placeholder="CLR" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            CLR
                                        </label>
                                        <asp:TextBox ID="txtNetCLR" Enabled="false" placeholder="CLR" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Milk Qty (In KG)</label>
                                        <asp:TextBox ID="txtNetMilkQtyInKG" MaxLength="5" Enabled="false" placeholder="Milk Quantity IN KG" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtrate" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtamount" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            FAT (In KG)
                                        </label>
                                        <asp:TextBox ID="txtfatinkg" Enabled="false" placeholder="FAT" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>S.N.F. (In KG) </label>
                                        <asp:TextBox ID="txtsnfinkg" Enabled="false" placeholder="CLR" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-6">
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

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Milk Dispatch Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Milk Dispatch Type" Text="<i class='fa fa-exclamation-circle' title='Select Milk Dispatch Type !'></i>"
                                                ControlToValidate="ddlMilkDispatchtype" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlMilkDispatchtype" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkDispatchtype_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Tanker">Tanker</asp:ListItem>
                                            <asp:ListItem Value="Cans">Cans</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <label>Milk Quality<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvMilkQuality_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                            <asp:ListItem Value="Sour">Curd</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Temperature (°C)<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvTemprature" runat="server" Display="Dynamic" ControlToValidate="txttemperature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="rfvTemprature_S" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="txttemperature" Text="<i class='fa fa-exclamation-circle' title='Enter Temperature (°C)!'></i>" ErrorMessage="Enter Temperature (°C)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txttemperature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                            <asp:RegularExpressionValidator ID="revTemprature_S" Enabled="false" ControlToValidate="txttemperature" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txttemperature" placeholder="Enter Temperature" onkeypress="return validateDec(this,event)" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6" runat="server" id="divcandetail" visible="false">
                                    <div class="form-group">
                                        <label>Total Cans <span style="color: red;">*</span></label>
                                        <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txttotalcans" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Total Cans!'></i>" ErrorMessage="Enter Milk Total Cans" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Enabled="false" ID="RegularExpressionValidator7" ValidationGroup="a" runat="server" ControlToValidate="txttotalcans" ForeColor="Red" Display="Dynamic"
                                            ValidationExpression="^\d+" ErrorMessage="*Invalid Milk Total Cans." Text="<i class='fa fa-exclamation-circle' title='Invalid Total Cans. !'></i>" />
                                        <asp:TextBox ID="txttotalcans" placeholder="Enter Total Cans" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Adulteration Test<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="a"
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


                    <div class="row" runat="server" visible="false" id="divMilkDype">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Milk Dispatch Details</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                ErrorMessage="Enter Vehicle No" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No !'></i>"
                                                ControlToValidate="txtV_VehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtV_VehicleNo" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtV_VehicleNo" ClientIDMode="Static" MaxLength="13" placeholder="XX-00-XX-0000"></asp:TextBox>
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
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_DriverName" MaxLength="40" placeholder="Enter Driver Name"></asp:TextBox>
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
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ID="txtV_DriverMobileNo" MaxLength="10" placeholder="Enter Driver Mobile"></asp:TextBox>
                                    </div>
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
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Submit" OnClientClick="return ValidatePage();" AccessKey="S" />
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
                                        <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("NetMilkQtyInKG_KG") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FAT %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("Fat_Per") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CLR">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR_Avg") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SNF %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Milk Dispatch Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lvlV_MilkDispatchType" runat="server" Text='<%# Eval("V_MilkDispatchType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Cans">
                                    <ItemTemplate>
                                        <asp:Label ID="lblI_TotalCans" runat="server" Text='<%# Eval("I_TotalCans") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <a href='../MilkCollection/Dcs_ChallanDetails.aspx?DCSCH_ID=<%# new APIProcedure().Encrypt(Eval("I_EntryID").ToString()) %>' target="_blank" title="Print Challan"><i class="fa fa-print"></i></a>
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

