<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GenerateMilkOrProductGatePassAtDock.aspx.cs" Inherits="mis_Demand_GenerateMilkOrProductGatePassAtDock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            @page {
                size: portrait;
            }

            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }


        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }

        .thead {
            display: table-header-group;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 5px;
            font-size: 15px;
            text-align: center;
        }

        .CapitalClass {
            text-transform: uppercase;
        }

        .capitalize {
            text-transform: capitalize;
        }

        .columngreen {
            background-color: #aee6a3 !important;
        }
    </style>
    <script>
        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=GridViewOrderDetails.ClientID %>");
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridViewOrderDetails.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="ModalSave" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Generate Gate Pass </h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Generate Gate Pass
                                </legend>
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" TabIndex="1" autocomplete="off" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" CssClass="form-control" ID="txtDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlShift" TabIndex="2" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvic" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Select Item Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" TabIndex="3" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Vehicle No.<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                    ErrorMessage="Select Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                    ControlToValidate="ddlVehicleNoList" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:DropDownList ID="ddlVehicleNoList" TabIndex="4" ClientIDMode="Static" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" TabIndex="5" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" TabIndex="5" CssClass="btn btn-success" OnClick="btnShowPendingGatePass_Click" ID="btnShowPendingGatePass" Text="Show Pending" />

                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnloderdetails" runat="server" Visible="false">
                                    <asp:GridView ID="GridViewOrderDetails" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                        AutoGenerateColumns="false"  EmptyDataText="No Record Found." EnableModelValidation="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="b" ErrorMessage="Please select at least one record."
                                                        ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                     <%-- <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />--%>
                                                  <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Id"  ItemStyle-Width="200">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderId" Text='<%#Eval("OrderId") %>' runat="server" />
                                                    <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%# Eval("MilkOrProductDemandId") %>' runat="server" />
                                                    <asp:Label ID="lblpariyojastatus" Visible="false" Text='<%# Eval("Priyojna_status") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Retailer Name" HeaderStyle-Width="5px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBName" Text='<%#Eval("BName") %>' runat="server" />
                                                    <asp:Label ID="lblBoothId" Visible="false" Text='<%#Eval("BoothId") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-Width="5px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleNo" Text='<%#Eval("VehicleNo") %>' runat="server" />

                                                    <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server" />
                                                    <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server" />
                                                    <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server" ForeColor="Red"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DM Type" HeaderStyle-Width="5px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                    <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%#Eval("ProductDMStatus") %>' runat="server" />
                                                    <asp:Label ID="lblmsg1" Visible="false"  runat="server" ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                    <div class="row">


                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Vehicle No.</label>
                                                <span class="pull-right">
                                                    <asp:LinkButton ID="lnkVehicle" CausesValidation="false" OnClick="lnkVehicle_Click" ToolTip="Add New Vehicle Details" runat="server"><b>[Add]</b></asp:LinkButton>
                                                </span>
                                                <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>In Time</label>
                                                <div class="input-group bootstrap-timepicker timepicker">
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control timepicker" Text="00:00 AM" ID="txtInTime" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Out Time</label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="b"
                                                        ErrorMessage="Enter Oute Time" Text="<i class='fa fa-exclamation-circle' title='Enter Oute Time !'></i>"
                                                        ControlToValidate="txtOutTime" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="input-group bootstrap-timepicker timepicker">
                                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control timepicker" ID="txtOutTime" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                                                    <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Supervisor Name</label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSupervisorName" MaxLength="80" placeholder="Enter Supervisor Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Crate Status</label>
                                                <asp:DropDownList ID="ddlCrateStatus" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Text="Crate Issued" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Crate Not Issued" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Driver Name</label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDriver_name" MaxLength="25" placeholder="Enter Supervisor Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Driver Mobile No.</label>
                                                <asp:TextBox runat="server" autocomplete="off" onkeypress="return validateNum(event)" CssClass="form-control" ID="Driver_Mobile_No" MaxLength="10" placeholder="Enter Supervisor Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Remark</label>
                                                <asp:TextBox runat="server" TextMode="MultiLine" autocomplete="off" CssClass="form-control" ID="txtRemark"  placeholder="Enter Supervisor Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-1" style="margin-top: 20px;">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="b" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                            </div>
                                        </div>
                                        <div class="col-md-1" style="margin-top: 20px;">
                                            <div class="form-group">
                                                <asp:Button ID="btnClear" OnClick="btnClear_Click" CssClass="btn btn-default" Text="Clear" runat="server" />
                                            </div>
                                        </div>


                                    </div>
                                </asp:Panel>
                            </fieldset>
                        </div>

                    </div>

                </div>
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Pending List of Gate Pass</h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Pending List of Gate Pass
                                </legend>
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Id" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderId" Text='<%#Eval("OrderId") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBName" Text='<%#Eval("BName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivary_Date" Text='<%#Eval("Delivary_Date") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Category" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCatName" Text='<%#Eval("ItemCatName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" Text='<%#Eval("VehicleNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCStatus" Text='<%#Eval("CStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal" id="VehicleDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 340px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Vehicle Details</h4>
                        </div>
                        <div class="modal-body">


                            <div class="row" style="height: 200px; overflow: scroll;">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ModalSave"
                                                InitialValue="0" ErrorMessage="Select Type" Text="<i class='fa fa-exclamation-circle' title='Select Type !'></i>"
                                                ControlToValidate="ddlVendorType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlVendorType" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorType_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="ModalSave"
                                                InitialValue="0" ErrorMessage="Select Name" Text="<i class='fa fa-exclamation-circle' title='Select Name !'></i>"
                                                ControlToValidate="ddlVendorName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlVendorName" Width="200px" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No.<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ModalSave"
                                                ErrorMessage="Enter Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle No. !'></i>"
                                                ControlToValidate="txtMVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMVehicleNo" Display="Dynamic"
                                                ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server"
                                                Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>"
                                                ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="ModalSave">
                                            </asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" MaxLength="13" ID="txtMVehicleNo" placeholder="XX-00-XX-0000" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Type<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="ModalSave"
                                                ErrorMessage="Enter Vehicle Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Type. !'></i>"
                                                ControlToValidate="txtVehicleType" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="ModalSave"
                                                ErrorMessage="Invalid Vehicle Type. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid Vehicle Type !'></i>" ControlToValidate="txtVehicleType"
                                                ValidationExpression="^[a-zA-Z0-9\s]+$">
                                            </asp:RegularExpressionValidator>

                                        </span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtVehicleType" MaxLength="10" placeholder="Enter Vehicle Type"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>IsActive</label>

                                        <asp:CheckBox ID="chkIsActive" CssClass="form-control" Checked="true" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-11">
                                    <asp:Label ID="lblModalMsg1" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ModalSave" ID="btnSaveVehicleDetails" Text="Submit" OnClick="btnSaveVehicleDetails_Click" />

                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </section>
        <section class="content">

            <div id="Print" runat="server" class="NonPrintable"></div>

            <div id="Print1" runat="server" class="NonPrintable"></div>

        </section>


    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
      <asp:Label ID="lblFinalAmount" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblFinalPaybleAmount" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblTcsTax" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblTcsTaxAmt" runat="server" Visible="false"></asp:Label>
    <script>
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;

        }
        function myVehicleDetailsModal() {
            $("#VehicleDetailsModal").modal('show');

        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            if (Page_IsValid) {
                debugger;
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

