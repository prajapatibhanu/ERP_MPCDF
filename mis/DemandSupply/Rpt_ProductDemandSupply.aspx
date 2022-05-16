<%@ Page Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_ProductDemandSupply.aspx.cs" Inherits="mis_DemandSupply_Rpt_ProductDemandSupply" %>


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
                            <h3 class="box-title">View Product DM</h3>


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
                                    <legend>View Product DM 
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
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
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
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
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
                            <h3 class="box-title">View Product DM Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="false" DataKeyNames="ProductDispDeliveryChallanId" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <%--OnRowCommand="GridView1_RowCommand"--%>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="ItemReturn" ErrorMessage="Please select at least one record."
                                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                         <asp:Label ID="lblProductDispDeliveryChallanId" Visible="false" Text='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                <%--<asp:TemplateField HeaderText="Action" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkprint" CssClass="buton button-mini button-blue" CommandName="RecordPrint" Visible='<%# Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : Eval("IsActive").ToString()=="True" ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="DM Print"><i class="btn btn-info fa fa-print"> DM</i>  </asp:LinkButton>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkEdit" CssClass="button button-mini button-green" CommandName="RecordEdit" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="" && Eval("IsActive").ToString()=="True")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Edit DM"><i class="btn btn-danger fa fa-pencil"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lnkbtn" CssClass="button button-mini button-orange" CommandName="RecordRedirected" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? true :false %>' runat="server" ToolTip="Generate DM"><i class="btn btn-warning fa fa-external-link"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkInvoice" CssClass="button button-mini button-green" CommandName="RecordPrintInvoice" Visible='<%# Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : Eval("IsActive").ToString()=="True" ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Invoice Print"><i class="btn btn-secondary fa fa-print"> Invoice </i></asp:LinkButton>
                                                        &nbsp;&nbsp;<br />
                                                        <asp:LinkButton ID="lnkUpdateCrate" CssClass="button button-mini button-green" CommandName="UpdateCrate" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="True" && Eval("IsActive").ToString()=="True" && Eval("LastCrateEditBy").ToString()=="")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Update Crate"><i class="btn btn-success fa fa-pencil"> Crate</i></asp:LinkButton>
                                                        &nbsp;&nbsp;<br />
                                                        <asp:LinkButton ID="lnkReturn" CssClass="button button-mini button-red" CommandName="ItemReturn" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="True" && Eval("IsActive").ToString()=="True" && Eval("LastReturnEditBy").ToString()=="")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Item Return"><i class="btn btn-danger fa fa-exchange"> Return</i></asp:LinkButton>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lnkReplace" CssClass="button button-mini button-red" CommandName="ItemReplace" Visible='<%#Eval("ProductDispDeliveryChallanId").ToString()=="" ? false : (Eval("DMEditStatus").ToString()=="True" && Eval("IsActive").ToString()=="True" && Eval("LastReplaceEditBy").ToString()=="")  ? true : false %>' CommandArgument='<%#Eval("ProductDispDeliveryChallanId") %>' runat="server" ToolTip="Item Replace"><i class="btn btn-warning fa fa-exchange"> Replace</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>

                      <%--  <div class="modal-footer">--%>
                        <div class="box-body" runat="server" id="Divfooter" visible="false">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Supply Date <span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="txtFromDate"
                                                ErrorMessage="Enter FromDate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter FromDate !'></i>"
                                                Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtsupplydate"
                                                ErrorMessage="Invalid Supply  Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Supply Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtsupplydate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>

                                    <%-- <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-primary" Text="Supply" ID="btnYes" OnClick="btnApp_Click" Style="margin-top: 20px; width: 100px;" />
                                </div>
                            </div>
                            </div>
                      <%--  </div>--%>
                    </div>

                </div>

            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fs" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationReject" runat="server" ValidationGroup="rej" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="crateupdate" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ItemReturn" ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="ItemReplace" ShowMessageBox="true" ShowSummary="false" />

        </section>

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
        function checkAll(objRef) {
            debugger;
            //var GridView = objRef.parentNode.parentNode.parentNode;
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
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
            debugger;
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
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

        //function checkAll(objRef) {
        //    debugger;
        //    var GridView = objRef.parentNode.parentNode.parentNode;
        //    var inputList = GridView.getElementsByTagName("input");
        //    for (var i = 0; i < inputList.length; i++) {
        //        var row = inputList[i].parentNode.parentNode;
        //        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
        //            if (objRef.checked) {
        //                inputList[i].checked = true;
        //            }
        //            else {
        //                inputList[i].checked = false;
        //            }
        //        }
        //    }
        //}

        //function FetchData1(button) {
        //    var row = button.parentNode.parentNode;
        //    var SQty = GetChildControl1(row, "txtSupplyQty").value;
        //    var RQty = GetChildControl1(row, "txtTotalReturnQty").value;


        //    if (parseInt(RQty) <= parseInt(SQty)) {

        //    }
        //    else {
        //        if (RQty == "") {

        //        }
        //        else {
        //            GetChildControl1(row, "txtTotalReturnQty").value = '';
        //            GetChildControl1(row, "txtTotalReturnQty").focus();
        //        }

        //    }


        //    return false;
        //};
        //function GetChildControl1(element, id) {
        //    var child_elements = element.getElementsByTagName("*");
        //    for (var i = 0; i < child_elements.length; i++) {
        //        if (child_elements[i].id.indexOf(id) != -1) {
        //            return child_elements[i];
        //        }
        //    }
        //};


        //function FetchData2(button) {
        //    var row = button.parentNode.parentNode;
        //    var SQty = GetChildControl1(row, "txtSupplyQty").value;
        //    var ReplaceQty = GetChildControl1(row, "txtReplaceQty").value;

        //    if (ReplaceQty == '') {
        //        ReplaceQty = 0;

        //    }

        //    if (parseInt(ReplaceQty) > parseInt(SQty)) {
        //        GetChildControl2(row, "txtReplaceQty").value = '';
        //        GetChildControl2(row, "txtReplaceQty").focus();
        //    }


        //    return false;
        //};
        //function GetChildControl2(element, id) {
        //    var child_elements = element.getElementsByTagName("*");
        //    for (var i = 0; i < child_elements.length; i++) {
        //        if (child_elements[i].id.indexOf(id) != -1) {
        //            return child_elements[i];
        //        }
        //    }
        //};

        //function myReturnItemModal() {
        //    $("#ItemDetailsModalReturn").modal('show');

        //}
        //function myReplaceItemModal() {
        //    $("#ItemDetailsModalReplace").modal('show');

        //}
        //function ValidatePage() {

        //    if (typeof (Page_ClientValidate) == 'function') {
        //        Page_ClientValidate('ItemReturn');
        //    }


        //}
        //function ValidatePage1() {

        //    if (typeof (Page_ClientValidate) == 'function') {
        //        Page_ClientValidate('ItemReplace');
        //    }


        //}


        $(function () {
            $('[id*=ddlDMType]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });

        });
    </script>
</asp:Content>
