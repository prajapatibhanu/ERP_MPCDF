<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GrgVehicleMaintenance_Headoffice.aspx.cs" Inherits="mis_Garage_GrgVehicleMaintenance_Headoffice" %>


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
                    <h3 class="box-title">Vehicle Maintenance</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="display:none">
                            <label>Vehicle Owned Type<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtVehicleOwnedType" runat="server" RepeatColumns="2" CssClass="form-control" OnSelectedIndexChanged="rbtVehicleOwnedType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Hired">&nbsp;Hired&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="Owned" Selected="True">&nbsp;Owned</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Vehicle No<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlVehicleNo" CssClass="form-control select2" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                                <label>Total Run-up Monthly<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedTotRunupMonthly" placeholder="Enter Total Run-up Monthly" onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                    <asp:Panel ID="pnlOwned" runat="server">
                        <div class="row">

                            

                            <div class="col-md-3">
                                <label>Fuel Consumption<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedFuelConsumption" placeholder="Enter Other Expenses On Vehicle" onkeypress="return validateDec(this,event);" onchange="CalculateAmount();" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Fuel Rate<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedFuelRate" placeholder="Enter Expenses Details" onkeypress="return validateDec(this,event);" MaxLength="10" onchange="CalculateAmount();" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Fuel Expences<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedFuelExpences" placeholder="Enter Fuel Expences" onkeypress="return validateDec(this,event);" onchange="CalculateRate();" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Other Expenses On Vehicle<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedOtherExpensesOnVehicle" placeholder="Enter Other Expenses On Vehicle" onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            
                        </div>
                        <div class="row">
                            

                            <div class="col-md-3">
                                <label>Expenses Details<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedExpensesDetails" placeholder="Enter Expenses Details" MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" style="margin-top:20px;">
                                    <asp:CheckBox ID="chkOwnedInCaseOfServicing" runat="server" AutoPostBack="true" OnCheckedChanged="chkOwnedInCaseOfServicing_CheckedChanged" />
                                    In Case Of Servicing
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlInCaseOfServicing" runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Servicing Date<span style="color: red;"> *</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtOwnedServicingDate" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Total KM<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedTotalKM" placeholder="Enter Total KM" onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Total Expenses In Servicing<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedTotalExpensesInServicing" placeholder="Enter Total Expenses In Servicing" onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Expenses Brief<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedExpensesBrief" placeholder="Enter Expenses Brief" MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Next Servicing Due Date<span style="color: red;"> *</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtOwnedNextServicingDueDate" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Other Info<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOwnedOtherInfo" placeholder="Enter Other Info " MaxLength="100" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlHired" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-4">
                                <label>Total Run-up Monthly (KM)<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtHiredTotalRunupMonthly" placeholder="Enter Total Run-up Monthly" onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Bill No<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtBillNo" placeholder="Enter Bill No" MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Bill Amount<span style="color: red;"> *</span></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtBillAmount" placeholder="Enter Bill Amount" onkeypress="return validateDec(this,event);" MaxLength="10" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="GrgVehicleMaintenance_HeadOffice.aspx">Clear</a>
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
    <script>
        function CalculateAmount() {
            var OwnedFuelConsumption = document.getElementById('<%=txtOwnedFuelConsumption.ClientID%>').value.trim();
            var OwnedFuelRate = document.getElementById('<%=txtOwnedFuelRate.ClientID%>').value.trim();
            if (OwnedFuelConsumption == "")
                OwnedFuelConsumption = "0";
            if (OwnedFuelRate == "")
                OwnedFuelRate = "0";

            document.getElementById('<%=txtOwnedFuelExpences.ClientID%>').value = (OwnedFuelConsumption * OwnedFuelRate).toFixed(2);
        }
        function CalculateRate() {
            debugger;
            var OwnedFuelConsumption = document.getElementById('<%=txtOwnedFuelConsumption.ClientID%>').value.trim();
            var OwnedFuelExpences = document.getElementById('<%=txtOwnedFuelExpences.ClientID%>').value.trim();
            //var Rate = document.getElementById('<%=txtOwnedFuelRate.ClientID%>').value.trim();
            if (OwnedFuelConsumption == "") {
                document.getElementById('<%=txtOwnedFuelConsumption.ClientID%>').value = "1";
                OwnedFuelConsumption = "1";
            }
            if (OwnedFuelExpences == "")
                OwnedFuelExpences = "0";

            document.getElementById('<%=txtOwnedFuelRate.ClientID%>').value = (OwnedFuelExpences / OwnedFuelConsumption).toFixed(2);
        }
    </script>
</asp:Content>

