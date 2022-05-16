<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GrgVehicleMaster.aspx.cs" Inherits="mis_Garage_GrgVehicleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        /*.customCSS td {
            padding: 0px !important;
        }*/

        /*.paddingLR {
            padding: 0px 5px;
        }*/
        .AlignR {
            text-align: right !important;
        }

        #GridViewLedger td {
            padding: 3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Vehicle Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtDate" data-date-end-date="0d" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Vehicle Category<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleCategory" placeholder="Enter Vehicle Category" MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Vehicle Type<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleType" placeholder="Enter Vehicle Type" MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Vehicle Make<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleMake" placeholder="Enter Vehicle Make" MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Vehicle Model<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleModel" placeholder="Enter Vehicle Model" MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Fuel Type<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtFuelType" runat="server" RepeatColumns="3" CssClass="form-control">
                                    <asp:ListItem Value="Diesel" Selected="True">&nbsp;Diesel&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="Petrol">&nbsp;Petrol&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="CNG">&nbsp;CNG</asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Vehicle Average<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleAverage" placeholder="Enter Vehicle Average"  onkeypress="return validateDec(this,event);"  MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Vehicle Owned Type<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtVehicleOwnedType" runat="server" RepeatColumns="2" CssClass="form-control" OnSelectedIndexChanged="rbtVehicleOwnedType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Hired">&nbsp;Hired&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="Owned" Selected="True">&nbsp;Owned</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Monthly Rent<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtMonthlyRent" placeholder="Enter Monthly Rent"  onkeypress="return validateDec(this,event);"  MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Agency  (for hired Vehicle)<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAgency" placeholder="Enter Agency" MaxLength="200" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Allot /incharge<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAllot_Incharge" placeholder="Enter Allot / incharge" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Vehicle No<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleNo" placeholder="Enter Vehicle No" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Vehicle Chechis No<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleChechisNo" placeholder="Enter Vehicle Chechis No" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Vehicle Registration No<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleRegistrationNo" placeholder="Enter Vehicle Registration No" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Vehicle Insurance No<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleInsuranceNo" placeholder="Enter Vehicle Insurance No" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <label>Insurance Value<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtInsuranceValue" placeholder="Enter Insurance Value"  onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <label>Insurance Valid Till<span style="color: red;"> *</span></label>

                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtInsuranceValidTill" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Insurance Company<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtInsuranceCompany" placeholder="Enter Insurance Company" MaxLength="200" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <label>Vehicle Summary<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVehicleSummary" placeholder="Enter Vehicle Summary" MaxLength="500" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Incharge (section/officer)<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtInCharge" placeholder="Enter Incharge" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <label>Driver Name<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDriverName" placeholder="Enter Driver Name" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <label>Driver License No<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDriverLicenseNo" placeholder="Enter Driver License No" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="GrgVehicleMaster.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>


                </div>
            </div>


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

