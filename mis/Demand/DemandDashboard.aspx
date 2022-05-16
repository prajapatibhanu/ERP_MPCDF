<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DemandDashboard.aspx.cs" Inherits="mis_Demand_DemandDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function ShowReport() {
            $('#ReportModal').modal('show');
        }
        //function ShowPayReport() {
        //    $('#paymentModal').modal('show');
        //}
        
        function Closeeport() {
            $('#ReportModal').modal('hide');
        }
        function Closepayreport() {
            $('#paymentModal').modal('hide');
        }
        function printDiv() {
            var divName = 'printableArea';
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Dashboard
        <small></small>
            </h1>

        </section>
        <section class="content">
            <fieldset>
                <legend>Order Status</legend>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>From Date</label>
                            <span class="pull-right">

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                                    ErrorMessage="Invalid FromDate" Text="<i class='fa fa-exclamation-circle' title='Invalid FromDate !'></i>" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                            </span>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>To Date</label>
                            <span class="pull-right">

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                            </span>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtToDate" MaxLength="10" placeholder="Enter ToDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group" style="margin-top: 20px;">
                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label></h3>

                                <p>Total Orders</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-bag"></i>
                            </div>
                            <asp:LinkButton ID="lnkbtnTotal" runat="server" OnClick="lnkbtnTotal_Click" CssClass="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>

                        </div>
                    </div>
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblApproved" runat="server"></asp:Label></h3>

                                <p>Approved Orders</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-bag"></i>
                            </div>
                            <asp:LinkButton ID="lnkbtnApproved" runat="server" OnClick="lnkbtnApproved_Click" CssClass="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblPending" runat="server"></asp:Label></h3>

                                <p>Pending Orders</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-bag"></i>
                            </div>
                            <asp:LinkButton ID="lnkbtnPending" runat="server" OnClick="lnkbtnPending_Click" CssClass="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-red">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblRejected" runat="server"></asp:Label></h3>

                                <p>Rejected Orders</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-bag"></i>
                            </div>
                            <asp:LinkButton ID="lnkbtnRejected" runat="server" OnClick="lnkbtnRejected_Click" CssClass="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend>Supply Status of Milk</legend>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>From Date</label>
                            <span class="pull-right">

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtDemandFrom"
                                    ErrorMessage="Invalid FromDate" Text="<i class='fa fa-exclamation-circle' title='Invalid FromDate !'></i>" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                            </span>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDemandFrom" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>To Date</label>
                            <span class="pull-right">

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtDemandto"
                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                            </span>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDemandto" MaxLength="10" placeholder="Enter ToDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Shift</label>
                            <%-- <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvpro" Display="Dynamic" ValidationGroup="b" runat="server" ControlToValidate="ddlShift" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator>
                            </span>--%>
                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                <asp:ListItem Text="-- Select Shift --" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <div class="form-group" style="margin-top: 20px;">
                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btndemand_Click" ValidationGroup="b" ID="btndemand" Text="Search" />
                        </div>
                    </div>
                </div>
                <div class="row">

                    <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                    <asp:GridView ID="grdDemand" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                        AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True" OnRowDataBound="grdDemand_RowDataBound" OnRowCommand="grdDemand_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnName" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Name") %>' CommandName="RecordUpdate" CommandArgument='<%#Eval("Office_ID") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
           <%-- <fieldset>
                <legend>Payment Detail of Milk</legend>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>From Date</label>
                            <span class="pull-right">

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="vsgissue" runat="server" Display="Dynamic" ControlToValidate="txtDemandFrom"
                                    ErrorMessage="Invalid FromDate" Text="<i class='fa fa-exclamation-circle' title='Invalid FromDate !'></i>" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                            </span>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtpayfromdt" MaxLength="10" placeholder="Enter FromDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>To Date</label>
                            <span class="pull-right">

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="vsgissue" runat="server" Display="Dynamic" ControlToValidate="txtDemandto"
                                    ErrorMessage="Invalid ToDate" Text="<i class='fa fa-exclamation-circle' title='Invalid ToDate !'></i>" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>

                            </span>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtpaytodt" MaxLength="10" placeholder="Enter ToDate" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group" style="margin-top: 20px;">
                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnpaydetail_Click" ValidationGroup="vsgissue" ID="btnpaydetail" Text="Search" />
                        </div>

                    </div>
                    <div class="col-md-12" style="text-align: center;">

                        <asp:HiddenField ID="hdnofficeid" runat="server" Value="0" />
                        <asp:GridView ID="grdPaydetail" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                            AutoGenerateColumns="false" EmptyDataText="No Record Found." EnableModelValidation="True" OnRowCommand="grdPaydetail_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnName" CssClass="btn btn-block btn-secondary" Text='<%#Eval("OfficeName") %>' CommandName="RecordUpdate" CommandArgument='<%#Eval("OfficeID") %>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Milk Amount(RS.)">
                                    <ItemTemplate>
                                        <%#Eval("MilkAmount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amount(RS.)">
                                    <ItemTemplate>
                                        <%#Eval("PaidAmount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remaining Amount(RS.)">
                                    <ItemTemplate>
                                        <%#Eval("RemainingBalance") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>--%>
        </section>

        <div class="modal" id="ItemDetailsModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body">
                        <div id="divitem" runat="server">
                            <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Order Details</legend>
                                        <div class="row" style="height: 300px; overflow: scroll;">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                        AutoGenerateColumns="False" EmptyDataText="No Record Found." EnableModelValidation="True" DataKeyNames="MilkOrProductDemandId">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderId" Text='<%#Eval("OrderId") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Route No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRName" Text='<%# Eval("RName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Retailer/Institution Name ">
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lblPAI" Text='<%# Eval("RetailerTypeID").ToString() == "3" ? Eval("Organization_Name") : Eval("BoothName") %>' runat="server" />--%>
                                                                    <asp:Label ID="lblPAI" Text='<%# Eval("BoothName") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Shift">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblShiftName" Text='<%# Eval("ShiftName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDemandDate" Text='<%# Eval("Demand_Date")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemCatName" Text='<%# Eval("ItemCatName")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDemandStatus" Text='<%# Eval("DStatus")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" Text='<%# Eval("Delivary_Date")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryShift" Text='<%# Eval("DelivaryShift")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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

                        <button type="button" class="btn btn-default" data-dismiss="modal">CLOSE </button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <div class="modal fade" id="ReportModal" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="ReportModal" aria-hidden="true">
            <div class="modal-dialog" style="width: 60%;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <span style="color: white">Route Wise Details</span>
                    </div>
                    <div class="modal-body" id="printableArea">

                        <div class="row" style="height: 250px; overflow: scroll;">

                            <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" class="table table-striped table-bordered" AllowPaging="false"
                                AutoGenerateColumns="true" ShowFooter="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkbtnRoute" Text='<%#Eval("Route") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-default" ID="btnclose" OnClientClick="Closeeport();" CausesValidation="false" Text="Close" />
                          <asp:Button runat="server" CssClass="btn btn-primary" ID="btnprint" OnClick="btnprint_Click" CausesValidation="false" Text="Export" />
                    </div>
                </div>

            </div>
        </div>
        <div class="modal fade" id="paymentModal" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="ReportModal" aria-hidden="true">
            <div class="modal-dialog" style="width: 60%;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <span style="color: white">Route Wise Details</span>
                    </div>
                    <div class="modal-body">

                        <div class="row" style="height: 250px; overflow: scroll;">

                            <asp:GridView ID="GridView3" runat="server" class="table table-striped table-bordered" AllowPaging="false"
                                AutoGenerateColumns="false" ShowFooter="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkbtnRoute" Text='<%#Eval("RName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkDistributor" Text='<%#Eval("DTName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivary Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkDelivaryDate" Text='<%#Eval("Delivary_Date") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkPaymentMode" Text='<%#Eval("PaymentMode") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkPaymentDate" Text='<%#Eval("PaymentDate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkChequeNo" Text='<%#Eval("ChequeNo") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkAmount" Text='<%#Eval("Amount") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PaidAmt">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkPaidAmt" Text='<%#Eval("PaidAmt") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkBalance" Text='<%#Eval("Balance") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-default" ID="Button1" OnClientClick="Closepayreport();" CausesValidation="false" Text="Close" />
                        <%--  <asp:Button runat="server" CssClass="btn btn-primary" ID="btnprint" OnClientClick="printDiv();" CausesValidation="false" Text="Print" />--%>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
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
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'Order List',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    footer: false
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
        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }
    </script>
</asp:Content>

