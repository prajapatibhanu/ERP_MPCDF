<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_ProductRejectedDemandDetails.aspx.cs" Inherits="mis_DemandSupply_Rpt_ProductRejectedDemandDetails" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }

        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }


        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid black !important;
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
            border: 1px solid black !important;
        }

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }
    </style>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
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

        function chkedit(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < 1; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        // inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
         </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <section class="content noprint">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Rejected Product DM</h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <fieldset>
                                    <legend>View Rejected Product DM 
                                    </legend>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date /दिनांक से<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Enter FromDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter FromDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server"  autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date / दिनांक तक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" ControlToValidate="txtToDate"
                                                    ErrorMessage="Enter ToDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter ToDate !'></i>"
                                                    Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server"  autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>
                                            <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location / स्थान<%--<span style="color: red;"> *</span>--%></label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route No / रूट</label>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                             <label>DM Type</label>
                                           <%-- <asp:DropDownList ID="ddlDMType" runat="server" TabIndex="4" CssClass="form-control select2">
                                               
                                            </asp:DropDownList>--%>
                                              <asp:ListBox runat="server" ID="ddlDMType" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">
                                                   <asp:ListItem Text="Regular Demand" Selected="True" Value="0"></asp:ListItem> 
                                                <asp:ListItem Text="Current Demand" Selected="True" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="Current Ghee Demand" Selected="True" Value="2"></asp:ListItem>

                                              </asp:ListBox>
                                            </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" OnClick="btnSearch_Click" ID="btnSearch" Text="Search" AccessKey="S" />
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                    
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" id="pnldata" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Rejected Product DM Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" DataKeyNames="ProductDispDeliveryChallanId" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Challan No. / DM NO." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVDChallanNo" Text='<%#Eval("DMChallanNo") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDelivary_Date" Text='<%#Eval("Delivary_Date") %>' runat="server" />
                                                        <asp:Label ID="lblDelivaryShiftid" Visible="false" Text='<%#Eval("DelivaryShift_id") %>' runat="server" />
                                                        <asp:Label ID="lblAreaId" Visible="false" Text='<%#Eval("AreaId") %>' runat="server" />
                                                        <asp:Label ID="lblRouteId" Visible="false" Text='<%#Eval("RouteId") %>' runat="server" />
                                                        <asp:Label ID="lblProductDMStatus" Visible="false" Text='<%#Eval("ProductDMStatus") %>' runat="server" />
                                                        <asp:Label ID="lblMilkOrProductDemandId" Visible="false" Text='<%#Eval("MilkOrProductDemandId") %>' runat="server" />
                                                        <asp:Label ID="lblTotalIssueCrate" Visible="false" Text='<%#Eval("TotalIssueCrate") %>' runat="server" />
                                                        <asp:Label ID="lblDistributorId" Visible="false" Text='<%#Eval("DistributorId") %>' runat="server" />
                                                        <asp:Label ID="lblDMEditStatus" Visible="false" Text='<%#Eval("DMEditStatus") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShiftName" Text='<%#Eval("ShiftName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRName" Text='<%#Eval("RName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDName" Text='<%#Eval("DName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vehicle No." HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVehicleNo" Text='<%#Eval("VehicleNo") %>' runat="server" />
                                                        <asp:Label ID="lblVehicleMilkOrProduct_ID" Visible="false" Text='<%#Eval("VehicleMilkOrProduct_ID") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DM Type" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDMStatus" Text='<%#Eval("PDMStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DM Status" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGPStatus" Text='<%#Eval("GPStatus") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprint" CssClass="buton button-mini button-blue" CommandName="RecordPrint" Visible='<%# Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : Eval("IsActive").ToString()=="False" ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="DM Print"><i class="btn btn-info fa fa-print"> DM</i>  </asp:LinkButton>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkEdit" CssClass="button button-mini button-green" CommandName="RecordEdit" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="" && Eval("IsActive").ToString()=="True")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Edit DM"><i class="btn btn-danger fa fa-pencil"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkbtn" CssClass="button button-mini button-orange" CommandName="RecordRedirected" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? true :false %>' runat="server" ToolTip="Generate DM"><i class="btn btn-warning fa fa-external-link"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkInvoice" CssClass="button button-mini button-green" CommandName="RecordPrintInvoice" Visible='<%# Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : Eval("IsActive").ToString()=="False" ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Invoice Print"><i class="btn btn-secondary fa fa-print"> Invoice </i></asp:LinkButton>
                                                        &nbsp;&nbsp;<br />
                                                        <asp:LinkButton ID="lnkUpdateCrate" CssClass="button button-mini button-green" CommandName="UpdateCrate" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="True" && Eval("IsActive").ToString()=="True" && Eval("LastCrateEditBy").ToString()=="")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Update Crate"><i class="btn btn-success fa fa-pencil"> Crate</i></asp:LinkButton>
                                                        &nbsp;&nbsp;<br />
                                                        <asp:LinkButton ID="lnkReturn" CssClass="button button-mini button-red" CommandName="ItemReturn" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="True" && Eval("IsActive").ToString()=="True" && Eval("LastReturnEditBy").ToString()=="")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Item Return"><i class="btn btn-danger fa fa-exchange"> Return</i></asp:LinkButton>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkReplace" CssClass="button button-mini button-red" CommandName="ItemReplace" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="True" && Eval("IsActive").ToString()=="True" && Eval("LastReplaceEditBy").ToString()=="")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Item Replace"><i class="btn btn-warning fa fa-exchange"> Replace</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fs" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationReject" runat="server" ValidationGroup="rej" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="crateupdate" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ItemReturn" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="ItemReplace" ShowMessageBox="true" ShowSummary="false" />
            <div class="modal" id="ItemDetailsModal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="modalChallan" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Date :<span id="modaldate" style="color: red" runat="server"></span>
                                &nbsp;&nbsp;Route :<span id="modalroute" style="color: red" runat="server"></span>&nbsp;&nbsp;Vehicle No :<span id="modalVehicle" style="color: red" runat="server"></span></h4>
                        </div>
                        <div class="modal-body">
                            <div id="divitem" runat="server">
                                <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details</legend>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Total Crate<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="fs" ControlToValidate="txtTotalCrate"
                                                                ErrorMessage="Enter Total Crate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Total Crate !'></i>"
                                                                Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtTotalCrate" MaxLength="5" ClientIDMode="Static"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Vehicle No.</label>

                                                        <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="form-control select2">
                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        <label>Remark (If Reject DM)<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="rej" ControlToValidate="txtRejectRemark"
                                                                ErrorMessage="Enter Remark" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Remark !'></i>"
                                                                Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ValidationGroup="rej"
                                                                ErrorMessage="Enter Remark. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Remark !'></i>" ControlToValidate="txtRejectRemark"
                                                                ValidationExpression="^[a-zA-Z0-9\s,./-]+$">
                                                            </asp:RegularExpressionValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtRejectRemark" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row" style="height: 250px; overflow: scroll;">

                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView4" runat="server" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="ProductDispDeliveryChallanChildId">
                                                            <Columns>
                                                                 <asp:TemplateField HeaderText="Select">
                                                                     <HeaderTemplate>
                                                                                 <asp:CheckBox ID="checkAll" Checked="false" runat="server" onclick="checkAll(this);" AutoPostBack="true" OnCheckedChanged="chkedit_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkedit"  AutoPostBack="true" runat="server" onclick="chkedit(this);" Checked="false" OnCheckedChanged="chkedit_CheckedChanged" />
                                                                       </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemCatName" Visible="false" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                        <asp:Label ID="lblIName" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                        <asp:Label ID="lblItem_id" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                         <asp:Label ID="lblProductDispDeliveryChallanChildId" Visible="false" Text='<%# Eval("ProductDispDeliveryChallanChildId")%>' runat="server" />
                                                                        <asp:Label ID="lblissuecaret" Visible="false"  runat="server" />
                                                                        <asp:Label ID="lblissueBox" Visible="false"  runat="server" />
                                                                        <asp:Label ID="lblFiItemQtyByCarriageMode" Visible="false"  runat="server" />
                                                                        <asp:Label ID="lblFiNotIssueQty" Visible="false"  runat="server" />
                                                                         <asp:Label ID="lblCarriageModeID" Visible="false"  Text='<%# Eval("CarriageModeID")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Qty.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQty" Text='<%# Eval("TotalQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Supply Qty.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplyTotalQty" Text='<%# Eval("SupplyQty")%>' runat="server" />
                                                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                            ErrorMessage="Enter Total Supply Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Total Supply Qty. !'></i>"
                                                                            ControlToValidate="txtSupplyTotalQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                            ErrorMessage="Enter Valid Number In Quantity Field !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Number In Quantity Field !'></i>" ControlToValidate="txtSupplyTotalQty"
                                                                            ValidationExpression="^[0-9]*$">
                                                                        </asp:RegularExpressionValidator>
                                                                        </span>
                                                                          <asp:TextBox runat="server" onfocusout="FetchData(this)" autocomplete="off" Visible="false" CausesValidation="true" Text='<%# Eval("SupplyQty")%>' CssClass="form-control" ID="txtSupplyTotalQty" MaxLength="5" onpaste="return false;" onkeypress="return validateNum(event);" placeholder="Enter Total Supply Qty." ClientIDMode="Static"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Crate Qty.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="IssueCrate" Text='<%# Eval("IssueCrate")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate (Including GST)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRateIncludingGST" Enabled="false" Width="70px" CssClass="form-control" Text='<%# Eval("RateincludingGST")%>' runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmount" Enabled="false" CssClass="form-control" Width="80px" Text='<%# Eval("Amount")%>' runat="server"></asp:TextBox>
                                                                        <asp:HiddenField ID="HFTotalAmt" Value='<%# Eval("Amount")%>' runat="server"></asp:HiddenField>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" Visible="false" CommandName="RecordEdit" CommandArgument='<%#Eval("ProductDispDeliveryChallanChildId") %>' runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" ValidationGroup="c" CausesValidation="true" CommandName="RecordUpdate" CommandArgument='<%#Eval("ProductDispDeliveryChallanChildId") %>' runat="server" ToolTip="Update" Style="color: darkgreen;" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-check"></i></asp:LinkButton>
                                                                        &nbsp;&nbsp;&nbsp; 
                                                                        <asp:LinkButton ID="lnkReset" Visible="false" CommandName="RecordReset" CommandArgument='<%#Eval("ProductDispDeliveryChallanChildId") %>' runat="server" ToolTip="Reset" Style="color: gray;"><i class="fa fa-times"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-md-2 pull-left">

                                <asp:LinkButton ID="lnkFinalSubmit" OnClick="lnkFinalSubmit_Click" ValidationGroup="fs" OnClientClick="return confirm('Are you sure to Submit?')" CssClass="btn btn-success" Text="Final Update" runat="server"></asp:LinkButton>
                            </div>
                            
                            <div class="col-md-1 pull-right">

                               
                                <asp:LinkButton ID="lnkReject" OnClick="lnkReject_Click" ValidationGroup="rej" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Reject?')" Text="Reject" runat="server"></asp:LinkButton>
                            </div>
                             <div class="col-md-1 pull-right">

                                   <asp:LinkButton ID="LinkButton2" OnClick="lnkupdate_Click"  CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to Update?')" Text="Update" runat="server"></asp:LinkButton>
                               
                            </div>

                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="ItemDetailsModalForCrate">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="Crate_modalChallan" style="color: red" runat="server"></span>&nbsp;&nbsp;
                             Date :<span id="Crate_modaldate" style="color: red" runat="server"></span>
                                &nbsp;&nbsp;Route :<span id="Crate_modalroute" style="color: red" runat="server"></span>&nbsp;&nbsp;Vehicle No :<span id="Crate_modalVehicle" style="color: red" runat="server"></span></h4>
                        </div>
                        <div class="modal-body">
                            <div id="div1" runat="server">
                                <asp:Label ID="lblCrateModalMsg" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details</legend>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Total Crate<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="crateupdate" ControlToValidate="txtUpdateTotalIssueCrate"
                                                                ErrorMessage="Enter Total Crate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Total Crate !'></i>"
                                                                Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtUpdateTotalIssueCrate" MaxLength="5" ClientIDMode="Static"></asp:TextBox>
                                                    </div>

                                                </div>

                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">



                            <asp:LinkButton ID="LinkButton3" OnClick="lnkUpdateCrate_Click" ValidationGroup="crateupdate" CssClass="btn btn-success" Text="Update Crate" runat="server"></asp:LinkButton>


                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>--%>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal" id="ItemDetailsModalReturn">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="modalDistName" style="color: red" runat="server"></span>&nbsp;&nbsp;
                                       &nbsp;&nbsp;Route :<span id="modalroutename" style="color: red" runat="server"></span>
                                &nbsp;&nbsp;Challan No : <span id="modalChallanNo_Return" style="color: red" runat="server"></span>
                                &nbsp;&nbsp; Date :<span id="modalreturndelivarydate" style="color: red" runat="server"></span>
                                &nbsp;&nbsp; Shift :<span id="modalshift" style="color: red" runat="server"></span></h4>
                        </div>
                        <div class="modal-body">
                            <div id="divitemReturn" runat="server">
                                <asp:Label ID="lblModalReturnMsg" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details for Sales Return</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Sales Return Date<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="ItemReturn"
                                                                ErrorMessage="Enter Sales Return Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sales Return Date !'></i>"
                                                                ControlToValidate="txtSalesReturnDate" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>

                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="ItemReturn" runat="server" Display="Dynamic" ControlToValidate="txtSalesReturnDate"
                                                                ErrorMessage="Invalid Sales Return Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Sales Return Date !'></i>" SetFocusOnError="true"
                                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSalesReturnDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="height: 280px; overflow: scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView2" runat="server" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="ProductDispDeliveryChallanChildId">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="checkAll(this);" />
                                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="ItemReturn" ErrorMessage="Please select at least one record."
                                                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="3px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                        <asp:Label ID="Label3" Visible="false" Text='<%# Eval("ProductDispDeliveryChallanChildId")%>' runat="server" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Supply Qty" HeaderStyle-Width="15px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" ID="txtSupplyQty" Width="70px" CssClass="form-control" Enabled="false" Text='<%# Eval("SupplyQty")%>'></asp:TextBox>
                                                                         <asp:Label ID="lblLastSupplyQtyBeforeReturn" Visible="false" Text='<%# Eval("LastSupplyQtyBeforeReturn")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="25px">
                                                                    <HeaderTemplate>
                                                                        Return Qty 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" autocomplete="off" Width="70px" onblur="FetchData1(this)" Text='<%# Eval("ReturnQty").ToString() %>' CausesValidation="true" onpaste="return false;" CssClass="form-control" ID="txtTotalReturnQty" MaxLength="8" onkeypress="return validateNum(event);" ClientIDMode="Static"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Remark
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" autocomplete="off" CausesValidation="true" CssClass="form-control" ID="txtReturnRemark" MaxLength="200" Text='<%# Eval("ReturnRemark")%>' placeholder='<%# Eval("ReturnBy").ToString()=="" ? "Enter Remark" : Eval("ReturnRemark").ToString() %>' ClientIDMode="Static"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-md-2" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ItemReturn" OnClientClick="return ValidatePage();" ID="btnReturnSubmit" Text="Save" />
                                    </div>
                                </div>
                                <div class="col-md-2 pull-right" style="margin-top: 20px;">

                                    <asp:LinkButton ID="lnkFinalSubmitReturn" OnClientClick="return confirm('Are you sure to Final Save?')" OnClick="lnkFinalSubmitReturn_Click" CssClass="btn btn-success" Text="Final Save" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>

            <div class="modal" id="ItemDetailsModalReplace">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">Item Details for <span id="modalDistNameReplace" style="color: red" runat="server"></span>&nbsp;&nbsp;
                                       &nbsp;&nbsp;Route :<span id="modalroutenameReplace" style="color: red" runat="server"></span>
                                &nbsp;&nbsp;Challan No : <span id="modalChallanNoReplace" style="color: red" runat="server"></span>
                                &nbsp;&nbsp; Date :<span id="modalreturndelivarydateReplace" style="color: red" runat="server"></span>
                                &nbsp;&nbsp; Shift :<span id="modalshiftReplace" style="color: red" runat="server"></span></h4>
                        </div>
                        <div class="modal-body">
                            <div id="div2" runat="server">
                                <asp:Label ID="lblModalMsgReplace" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Item Details for Sales Replace</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Sales Replace Date<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="ItemReplace"
                                                                ErrorMessage="Enter Sales Replace Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Sales Return Date !'></i>"
                                                                ControlToValidate="txtSalesReplaceDate" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>

                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="ItemReplace" runat="server" Display="Dynamic" ControlToValidate="txtSalesReplaceDate"
                                                                ErrorMessage="Invalid Sales Return Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Sales Return Date !'></i>" SetFocusOnError="true"
                                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSalesReplaceDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="height: 280px; overflow: scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView3" runat="server" EmptyDataText="No Record Found" class="table table-striped table-bordered" AllowPaging="false"
                                                            AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="ProductDispDeliveryChallanChildId">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="CheckBox2" runat="server" onclick="checkAll(this);" />
                                                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="ItemReplace" ErrorMessage="Please select at least one record."
                                                                            ClientValidationFunction="Validate1" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="3px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" Text='<%# Eval("ItemName")%>' runat="server" />
                                                                        <asp:Label ID="Label6" Visible="false" Text='<%# Eval("Item_id")%>' runat="server" />
                                                                        <asp:Label ID="Label7" Visible="false" Text='<%# Eval("ProductDispDeliveryChallanChildId")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Supply Qty" HeaderStyle-Width="15px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" ID="TextBox1" Width="70px" CssClass="form-control" Enabled="false" Text='<%# Eval("SupplyQty")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Width="25px">
                                                                    <HeaderTemplate>
                                                                        Replace Qty 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" autocomplete="off" Width="70px" onblur="FetchData2(this)" Text='<%# Eval("ReplaceQty").ToString() %>' CausesValidation="true" onpaste="return false;" CssClass="form-control" ID="txtReplaceQty" MaxLength="8" onkeypress="return validateNum(event);" ClientIDMode="Static"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Remark
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" autocomplete="off" CausesValidation="true" CssClass="form-control" ID="txtReplaceRemark" MaxLength="200" Text='<%# Eval("ReplaceRemark")%>' placeholder='<%# Eval("ReplaceBy").ToString()=="" ? "Enter Remark" : Eval("ReplaceRemark").ToString() %>' ClientIDMode="Static"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-md-2" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="ItemReplace" OnClientClick="return ValidatePage1();" ID="btnReplaceSubmit" Text="Save" />
                                    </div>
                                </div>
                                <div class="col-md-2 pull-right" style="margin-top: 20px;">

                                    <asp:LinkButton ID="LinkButton4" OnClientClick="return confirm('Are you sure to Final Save?')" OnClick="lnkFinalSubmitReplace_Click" CssClass="btn btn-success" Text="Final Save" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
        </section>
        <%--Confirmation Modal Start --%>
        <div class="modal fade" id="myModalReturn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                            <asp:Label ID="lblPopupAlertReturn" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnReturnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>

            </div>
        </div>
        <%--ConfirmationModal End --%>

        <%--Confirmation Modal Start --%>
        <div class="modal fade" id="myModalReplace" tabindex="-1" role="dialog" aria-labelledby="myModalLabel1" aria-hidden="true">
            <div style="display: table; height: 100%; width: 100%;">
                <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                    <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabel1">Confirmation</h4>

                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlertReplace" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="Button1" OnClick="btnReplaceSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>

            </div>
        </div>
        <%--ConfirmationModal End --%>
        <!-- /.content -->
        <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <asp:Label runat="server" Visible="false" ID="lblcheckcount"></asp:Label>
      <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('.loader').fadeOut();
            $('.loader').fadeOut();
            $("#<%=btnSearch.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));

        });
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
        function myItemDetailsModalForCrate() {
            $("#ItemDetailsModalForCrate").modal('show');

        }
        // for product

        function FetchData(button) {
            var row = button.parentNode.parentNode;
            var SQty = GetChildControl(row, "txtSupplyTotalQty").value;
            var RIGST = GetChildControl(row, "txtRateIncludingGST").value;


            if (SQty == '') {
                SQty = 0;

            }
            if (RIGST == '') {

                DSC2 = 0.00;
            }


            if (SQty == '' && RIGST == '') {
                alert('Total Supply Qty can not be empty');
                GetChildControl(row, "txtSupplyTotalQty").value = '';
                GetChildControl(row, "txtSupplyTotalQty").focus();

            }
            else {
                var ItemAmount = (parseInt(SQty) * parseFloat(RIGST));
                GetChildControl(row, "txtAmount").value = parseFloat(ItemAmount).toFixed(2);
                GetChildControl(row, "HFTotalAmt").value = parseFloat(ItemAmount).toFixed(2);


            }


            return false;
        };

        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
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
            var gridView = document.getElementById("<%=GridView2.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function FetchData1(button) {
            var row = button.parentNode.parentNode;
            var SQty = GetChildControl1(row, "txtSupplyQty").value;
            var RQty = GetChildControl1(row, "txtTotalReturnQty").value;


            if (parseInt(RQty) <= parseInt(SQty)) {

            }
            else {
                if (RQty == "") {

                }
                else {
                    GetChildControl1(row, "txtTotalReturnQty").value = '';
                    GetChildControl1(row, "txtTotalReturnQty").focus();
                }

            }


            return false;
        };
        function GetChildControl1(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };

        // for replace qty
        function Validate1(sender, args) {
            var gridView = document.getElementById("<%=GridView3.ClientID %>");
             var checkBoxes = gridView.getElementsByTagName("input");
             for (var i = 0; i < checkBoxes.length; i++) {
                 if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }
         function FetchData2(button) {
             var row = button.parentNode.parentNode;
             var SQty = GetChildControl1(row, "txtSupplyQty").value;
             var ReplaceQty = GetChildControl1(row, "txtReplaceQty").value;

             if (ReplaceQty == '') {
                 ReplaceQty = 0;

             }

             if (parseInt(ReplaceQty) > parseInt(SQty)) {
                 GetChildControl2(row, "txtReplaceQty").value = '';
                 GetChildControl2(row, "txtReplaceQty").focus();
             }


             return false;
         };
         function GetChildControl2(element, id) {
             var child_elements = element.getElementsByTagName("*");
             for (var i = 0; i < child_elements.length; i++) {
                 if (child_elements[i].id.indexOf(id) != -1) {
                     return child_elements[i];
                 }
             }
         };
        <%--function ValidatePage() {

            //if (typeof (Page_ClientValidate) == 'function') {
            //    Page_ClientValidate('fs');
            //}

            if (Page_IsValid) {
                debugger;
                if (document.getElementById('<%=lnkFinalSubmit.ClientID%>').value.trim() == "Final Update") {
                    document.getElementById('<%=lblPopupAlert1.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }--%>
        function myReturnItemModal() {
            $("#ItemDetailsModalReturn").modal('show');

        }
        function myReplaceItemModal() {
            $("#ItemDetailsModalReplace").modal('show');

        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('ItemReturn');
            }

            if (Page_IsValid) {
                if (document.getElementById('<%=btnReturnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlertReturn.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalReturn').modal('show');
                    return false;
                }
            }
        }
        function ValidatePage1() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('ItemReplace');
            }

            if (Page_IsValid) {
                if (document.getElementById('<%=btnReplaceSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlertReplace.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalReplace').modal('show');
                    return false;
                }
            }
        }


        $(function () {
            $('[id*=ddlDMType]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });

        });
    </script>
</asp:Content>


