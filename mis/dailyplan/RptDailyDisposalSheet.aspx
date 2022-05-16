<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDailyDisposalSheet.aspx.cs" Inherits="mis_dailyplan_RptDailyDisposalSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        .customCSS label {
            padding-left: 10px;
        }

        table {
            white-space: nowrap;
        }

        .capitalize {
            text-transform: capitalize;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">DAILY DISPOSAL SHEET</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                            </div>
                        </div>


                    </div>

                    <fieldset runat="server" id="divfinal" visible="false">
                        <legend>Milk Sheet</legend>
                        <div class="col-md-12">
                            <div class="form-group table-responsive">

                                <h1 class="text-center" style="font-weight: 800; font-size: 20px;"><span id="spnofcname" runat="server"></span></h1>
                                <h3 class="text-center" style="font-weight: 500; font-size: 13px;">Dairy Plant</h3>
                                <h2 class="text-center" style="font-weight: 800; font-size: 20px;">DAILY DISPOSAL SHEET</h2>


                                <table class="table table-bordered">
                                    <tr class="text-center">
                                        <th colspan="4">WHOLE MILK</th>
                                    </tr>
                                    <tr>
                                        <th>Receipt</th>
                                        <th>Qty.</th>
                                        <th>Issued</th>
                                        <th>Qty.</th>
                                    </tr>
                                    <tr>
                                        <td>Opening Balance</td>
                                        <td>
                                            <asp:Label ID="lblReceiptOpeningBalance" runat="server"></asp:Label></td>
                                        <td>Issued For Sale To Product</td>
                                        <td>
                                            <asp:Label ID="lblIssuedsaletoproduct" runat="server"></asp:Label></td>
                                    </tr>


                                    <tr>
                                        <%-- <td>Receipt:</td>--%>
                                        <td colspan="2">
                                            <asp:GridView ID="GV_TDetail_WM_Receipt" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="62%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                            <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQtyInLtr" Enabled="false" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <asp:Label ID="lblReceiptqty" Visible="false" runat="server"></asp:Label>
                                        </td>

                                        <td colspan="2">

                                            <asp:GridView ID="GV_TDetail_WM_Issued" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="62%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                            <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQtyInLtr" Enabled="false" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </td>


                                    </tr>

                                    <%--<tr>
                                        <td>Receipt:</td>
                                        <td>
                                            <asp:Label ID="lblReceiptqty" runat="server"></asp:Label></td>
                                        <td></td>
                                        <td></td>
                                    </tr>--%>


                                    <tr>
                                        <td>Good</td>
                                        <td>
                                            <asp:Label ID="lblReceiptgood" runat="server"></asp:Label></td>
                                        <td>Good</td>
                                        <td>
                                            <asp:Label ID="lblIssuedGood" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td>Sour</td>
                                        <td>
                                            <asp:Label ID="lblReceiptSour" runat="server"></asp:Label></td>
                                        <td>Sour</td>
                                        <td>
                                            <asp:Label ID="lblIssuedSour" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td>Curdled</td>
                                        <td>
                                            <asp:Label ID="lblReceiptCurdled" runat="server"></asp:Label></td>
                                        <td>Curdled</td>
                                        <td>
                                            <asp:Label ID="lblIssuedCurdled" runat="server"></asp:Label></td>
                                    </tr>


                                    <tr>
                                        <td>CR</td>
                                        <td>
                                            <asp:Label ID="lblReceiptCR" runat="server"></asp:Label></td>
                                        <td>Issued To Product Section</td>
                                        <td>
                                            <asp:Label ID="lblIssuedToProductSection" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td>W/B</td>
                                        <td>
                                            <asp:Label ID="lblReceiptWB" runat="server"></asp:Label></td>
                                        <td>Losses</td>
                                        <td>
                                            <asp:Label ID="lblIssuedLosses" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Flushing</td>
                                        <td>
                                            <asp:Label ID="lblReceiptFlushing" runat="server"></asp:Label></td>
                                        <td>Closing Balance</td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GVIssueMilkFromVarient" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="62%" HeaderStyle-Height="1%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                            <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQtyInPkt" Text='<%# Eval("TMQty") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>

                                        <td colspan="2">
                                            <asp:GridView ID="GVIssuedFromWholeMilk" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="70%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                            <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQtyInPkt" Text='<%# Eval("TMQty") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <hr />

                                            <asp:GridView ID="GV_CD_WM" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found.">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="70%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                            <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQtyInKgCM" Enabled="false" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </td>



                                    </tr>

                                    <tr>
                                        <td>Flushing</td>
                                        <td></td>
                                        <td>Closing Balance</td>
                                        <td>
                                            <asp:Label ID="lblIssuedClosingBalance" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr runat="server" visible="false">
                                        <td>Total</td>
                                        <td>
                                            <asp:Label ID="lblReceiptFinalTotal" runat="server"></asp:Label></td>
                                        <td>Total</td>
                                        <td>
                                            <asp:Label ID="lblIssuedFinalTotal" runat="server"></asp:Label></td>
                                    </tr>

                                </table>
                            </div>
                        </div>
                        <hr />
                        <div class="col-md-12">
                            <div class="form-group table-responsive">
                                <span style="border: 1px solid #d56a12 !important; background-color: #ff874c !important; color: #000; padding: 1px; line-height: 1.42857143; vertical-align: top; font-size: 16px;">RR Sheet </span>
                                <asp:GridView ID="GV_RRFinalSheet" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Particulars" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance <br/>B.F." HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtBalance_BFRR" Text='<%# Eval("RR_OpeningBalance_New") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Obtained" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRObtained" Text='<%# Eval("RR_Obtained") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRTotal" Text='<%# Eval("RR_Total") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Toning" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRToning" Text='<%# Eval("RR_Toning") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maintaining <br />S.N.F." HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRMaintainingSNF" Text='<%# Eval("RR_MaintainingSNF") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issued For<br />Product Section" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRIssueforproductionsection" Text='<%# Eval("RR_IssuedForProductSection") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Issued" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRTotalIssued" Text='<%# Eval("RR_TotalIssued") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing <br />Balance" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRRClosingBalance" Text='<%# Eval("RR_ClosingBalance") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <hr />
                        <div class="col-md-12">
                            <div class="form-group table-responsive">
                                <span style="border: 1px solid #d56a12 !important; background-color: #ff874c !important; color: #000; padding: 1px; line-height: 1.42857143; vertical-align: top; font-size: 16px;">Cow Milk </span>
                                <asp:GridView ID="GV_CowMilkSheetFinal" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="O/B" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtBalance_BFCw" Text='<%# Eval("New_OpeningBalance") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Prepard" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWPrepard" Text='<%# Eval("CM_Prepard") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWTotal" Text='<%# Eval("CM_Total") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sale" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWSale" Text='<%# Eval("CM_Sale") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issued Towm" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWIssuedTowm" Text='<%# Eval("CM_IssuedTowm") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Loss" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWLoss" Text='<%# Eval("CM_Loss") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cl. B." HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWClosingBalance" Text='<%# Eval("CM_ClosingBalance") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Issued" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCWTotalIssued" Text='<%# Eval("CM_TotalIssued") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">

                                    <asp:GridView ID="gvDDSheet" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." ShowHeader="false" ShowFooter="true">
                                        <Columns>

                                            <asp:TemplateField>

                                                <ItemTemplate>
                                                    <table class="table table-bordered">
                                                        <tr class="text-center">
                                                            <th colspan="4">
                                                                <asp:Label ID="lblItemTypeName" Width="50%" Text='<%# Eval("ItemTypeName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblItemType_id" Visible="false" Width="50%" Text='<%# Eval("ItemType_id") %>' runat="server"></asp:Label>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <th>Receipt</th>
                                                            <th>Qty.</th>
                                                            <th>Issued</th>
                                                            <th>Qty.</th>
                                                        </tr>
                                                         <tr>
                                                            <td>Opening Balance</td>
                                                            <td>
                                                                <asp:Label ID="lblopeningBalance" Width="50%" Text='<%# Eval("OpeningMilk") %>' runat="server"></asp:Label></td>

                                                            <td>Issued For Sale1</td>
                                                            <td>
                                                                <asp:Label ID="txtIssuedforstes" Width="50%" Text='<%# Eval("IssuedForSates") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="2"></td>
                                                            <td>Issued For Sale2</td>
                                                            <td>
                                                                <asp:Label ID="Label2" Width="50%" Text='<%# Eval("IssuedForSale2") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="2">
                                                                <td>Issued For Sale3</td>
                                                                <td>
                                                                    <asp:Label ID="Label1" Width="50%" Text='<%# Eval("IssuedForSale3") %>' runat="server"></asp:Label>

                                                                </td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2"></td>
                                                            <td>Issued For Sale4</td>
                                                            <td>
                                                                <asp:Label ID="Label3" Width="50%" Text='<%# Eval("IssuedForSale4") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>


                                                        <tr>
                                                             
                                                            <td>Issued For Sale4</td>
                                                            <td>
                                                                <asp:Label ID="Label4" Width="50%" Text='<%# Eval("IssuedForSale4") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>




                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gvMilkinContainer_opening" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="60%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qty.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtQtyInKg" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                            <td colspan="2"></td>
                                                        </tr>



                                                        <tr runat="server" visible="false">
                                                            <td>Received</td>
                                                            <td>
                                                                <asp:Label ID="lblReceived" Width="50%" Text='<%# Eval("ReceivedMilk") %>' runat="server"></asp:Label></td>

                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>


                                                        <tr>
                                                            <td><span id="spnPreparedText" runat="server"><%# Eval("ItemType_id").ToString() =="87"?"Prepared for SMP":"Prepared" %></span></td>
                                                            <td>
                                                                <asp:Label ID="txtPrepared" Width="50%" Text='<%# Eval("PreparedMilk") %>' runat="server"></asp:Label>

                                                            </td>

                                                            <td>Issued For Whole Milk</td>
                                                            <td>
                                                                <asp:Label ID="txtIssuedforWH" Width="50%" Text='<%# Eval("IssuedForWholeMilk") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>
														<tr id="trPrepared" runat="server" visible='<%# Eval("ItemType_id").ToString() =="87"?true:false %>'>
                                                                <td>Prepared for Separation</td>
                                                                <td>
                                                                    <asp:Label ID="txtPreparedforSeparation" Width="50%" Text='<%# Eval("PreparedMilkforSeparation").ToString()==""?"0.00":Eval("PreparedMilkforSeparation").ToString() %>' runat="server"></asp:Label>
                                                                </td>

                                                                
                                                            </tr>
                                                        <tr>
                                                            <td>Return</td>
                                                            <td>
                                                                <asp:Label ID="lblReturn" runat="server" Text='<%# Eval("ReturnMilk") %>' Width="50%"></asp:Label>

                                                            </td>

                                                            <td>Issued For Product Section</td>
                                                            <td>
                                                                <asp:Label ID="txtIssuedforProductions" Text='<%# Eval("IssuedForProductSection") %>' Width="50%" runat="server"></asp:Label>

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td>Issued For QC</td>
                                                            <td>
                                                                <asp:Label ID="txtLosses" Width="50%" Text='<%# Eval("Losses") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>


                                                        <tr>

                                                            <td colspan="2"></td>

                                                            <td colspan="2">
                                                                <asp:GridView ID="GVVariantDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Variant Name" HeaderStyle-Width="70%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty. (In Pkt)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtQtyInPkt" Text='<%# Eval("QtyInPkt") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--<asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtQtyInKg" CssClass="form-control" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                    </Columns>
                                                                </asp:GridView>


                                                                <asp:GridView ID="gvMilkinContainer" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found.">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="70%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty. (In Kg)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtQtyInKg" CssClass="form-control" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>


                                                            </td>


                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td>Closing Balance</td>
                                                            <td>
                                                                <asp:Label ID="txtClosingBalance" Width="50%" Text='<%# Eval("ClosingBalance") %>' runat="server"></asp:Label>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Total</td>
                                                            <td>
                                                                <asp:Label ID="lblReceiptTotal" Font-Bold="true" Text='<%# Eval("ReceiptMilkNetQty") %>' runat="server"></asp:Label></td>
                                                            <td>Total</td>
                                                            <td>
                                                                <asp:Label ID="lblIssuedtotal" Font-Bold="true" Text='<%# Eval("IssuedMilkNetQty") %>' runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>
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


        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
