<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="EditMilkProductionEntry.aspx.cs" Inherits="mis_dailyplan_EditMilkProductionEntry" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Edit Milk Issue For Production According to Variant In Particular Section</h3>
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
                               <%-- <asp:TextBox ID="txtDate" onkeypress="javascript: return false;"  AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                 <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" data-date-end-date="0d" data-date-start-date="-1d" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" AutoPostBack="true" OnTextChanged="ddlShift_TextChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                    </div>


                    <fieldset>
                        <legend>Milk Transfer To Section</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvmttos" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." ShowFooter="true">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Variant Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnVN" CssClass="btn btn-block btn-secondary" OnClick="lnkbtnVN_Click" Text='<%#Eval("ItemTypeName") %>' CommandArgument='<%#Eval("ItemType_id") %>' runat="server"></asp:LinkButton>
                                                    <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    Total
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Prev. Demand </br>500 ML (In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand_500InPkt" Text='<%# Eval("500MLPPkt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="Prev_Demand_500InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prev. Demand </br>200 ML (In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand_200InPkt" Text='<%# Eval("200MLPPkt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="Prev_Demand_200InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prev. Demand </br>1 Ltr(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand_1InPkt" Text='<%# Eval("1LPPkt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="Prev_Demand_1InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                           <%-- <asp:TemplateField HeaderText="Prev. Demand </br>500 ML (In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand500InLtr" Text='<%# Eval("500MLPLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_Demand500InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Prev. Demand </br>200 ML (In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand200InLtr" Text='<%# Eval("200MLPLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_Demand200InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                              <asp:TemplateField HeaderText="Prev. Demand </br>Total in Ltrs">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr" Text='<%# Eval("PrevDemInLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_Demand1InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Demand </br>500 ML (In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_500InPkt" runat="server" Text='<%# Eval("500MLCPkt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_500InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Current Demand </br>200 ML (In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_200InPkt" runat="server" Text='<%# Eval("200MLCPkt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_200InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Current Demand </br>1 Ltr (In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_1InPkt" runat="server" Text='<%# Eval("1LCPkt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_1InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Current Demand </br>500 ML (In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_500InLtr" runat="server" Text='<%# Eval("500MLCLtr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_500InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Current Demand </br>200 ML (In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_200InLtr" runat="server" Text='<%# Eval("200MLCLtr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_200InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                             <asp:TemplateField HeaderText="Current Demand </br>Total in Ltrs">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_1InLtr" runat="server" Text='<%# Eval("CurDemInLtr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_1InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                          <%--  <asp:TemplateField HeaderText="Milk Qty </br>(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgv_mqty" Width="80%" Text='<%# Eval("TMQty") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT </br>(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvV_Fat" Width="80%" Text='<%# Eval("TFQty") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF</br>(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvV_Snf" Width="80%" Text='<%# Eval("TSQty") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbFInalSheet" OnClick="lbFInalSheet_Click" CssClass="btn btn-block btn-secondary" runat="server">Update Whole Milk Sheet</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnRecombinationRecombination" OnClick="btnRecombinationRecombination_Click" CssClass="btn btn-block btn-secondary" runat="server">Recombination & Reconstitution Sheet</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 hidden">
                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lblCowMilkSheet" OnClick="lblCowMilkSheet_Click" CssClass="btn btn-block btn-secondary" runat="server">Cow Milk Sheet</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </fieldset>

                    <hr />
                    <h5 runat="server" style="padding-left: 10px;" visible="false" id="FTitle" class="box-title">DAILY DISPOSAL SHEET</h5>

                    <div class="col-md-12" runat="server" id="divfinal" visible="false">
                        <div class="form-group table-responsive">
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
                                        <asp:Label ID="lblReceiptOpeningBalance" runat="server"></asp:Label>
                                    </td>
                                    <td>Issued For Sale To Product</td>
                                    <td>
                                        <asp:Label ID="lblIssuedsaletoproduct" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Receipt:</td>
                                    <td>
                                        <asp:Label ID="lblReceiptqty" runat="server"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>


                                <tr>
                                    <td>Good</td>
                                    <td>
                                        <asp:Label ID="lblReceiptgood" runat="server"></asp:Label>
                                    </td>
                                    <td>Good</td>
                                    <td>
                                        <asp:Label ID="lblIssuedGood" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Sour</td>
                                    <td>
                                        <asp:Label ID="lblReceiptSour" runat="server"></asp:Label>
                                    </td>
                                    <td>Sour</td>
                                    <td>
                                        <asp:Label ID="lblIssuedSour" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Curdled</td>
                                    <td>
                                        <asp:Label ID="lblReceiptCurdled" runat="server"></asp:Label>
                                    </td>
                                    <td>Curdled</td>
                                    <td>
                                        <asp:Label ID="lblIssuedCurdled" runat="server"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td>CR</td>
                                    <td>
                                        <asp:Label ID="lblReceiptCR" runat="server"></asp:Label>
                                    </td>
                                    <td>Issued To Product Section</td>
                                    <td>
                                        <asp:Label ID="lblIssuedToProductSection" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>W/B</td>
                                    <td>
                                        <asp:Label ID="lblReceiptWB" runat="server"></asp:Label>
                                    </td>
                                    <td>Losses</td>
                                    <td>
                                        <asp:Label ID="lblIssuedLosses" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Flushing</td>
                                    <td>
                                        <asp:Label ID="lblReceiptFlushing" runat="server"></asp:Label>
                                    </td>
                                    <td>Closing Balance</td>
                                    <td>
                                        <asp:Label ID="lblIssuedClosingBalance" runat="server"></asp:Label>
                                    </td>
                                </tr>


                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GVIssueMilkFromVarient" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="72%" HeaderStyle-Height="1%">
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
                                                <asp:TemplateField HeaderStyle-Width="77%">
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


                                </tr>

                                <tr>
                                    <td>Total</td>
                                    <td>
                                        <asp:Label Font-Bold="true" ID="lblReceiptFinalTotal" runat="server"></asp:Label>
                                    </td>
                                    <td>Total</td>
                                    <td>
                                        <asp:Label Font-Bold="true" ID="lblIssuedFinalTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>

                    <hr />

                    <div class="col-md-12" runat="server" id="divRRfinal" visible="false">
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

                    <div class="col-md-12" runat="server" id="divCWfinal" visible="false">
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

                    <div class="col-md-12" runat="server" id="divDDfinal" visible="false">
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
                                                    <th>Qty.(In Kg)</th>
                                                    <th>Issued</th>
                                                    <th>Qty.(In Kg)</th>
                                                </tr>
                                                <tr>
                                                    <td>Opening Balance</td>
                                                    <td>
                                                        <asp:Label ID="lblopeningBalance" Width="50%" Text='<%# Eval("OpeningMilkQty_PrevShift") %>' runat="server"></asp:Label></td>

                                                    <td>Issued For Sale</td>
                                                    <td>
                                                        <asp:Label ID="txtIssuedforstes" Width="50%" Text='<%# Eval("IssuedForSates") %>' runat="server"></asp:Label>

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Received</td>
                                                    <td>
                                                        <asp:Label ID="lblReceived" Width="50%" Text='<%# Eval("ReceivedMilk") %>' runat="server"></asp:Label></td>

                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>


                                                <tr>
                                                    <td>Prepared</td>
                                                    <td>
                                                        <asp:Label ID="txtPrepared" Width="50%" Text='<%# Eval("PreparedMilk") %>' runat="server"></asp:Label>

                                                    </td>

                                                    <td>Issued For Whole Milk</td>
                                                    <td>
                                                        <asp:Label ID="txtIssuedforWH" Width="50%" Text='<%# Eval("IssuedForWholeMilk") %>' runat="server"></asp:Label>

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
                                                    <td>Losses</td>
                                                    <td>
                                                        <asp:Label ID="txtLosses" Width="50%" Text='<%# Eval("Losses") %>' runat="server"></asp:Label>

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
                                                    <td colspan="2"></td>

                                                    <td colspan="2">
                                                        <asp:GridView ID="GVVariantDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                            EmptyDataText="No Record Found.">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Variant Name" HeaderStyle-Width="69%">
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
                                                            </Columns>
                                                        </asp:GridView>


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

            </div>


            <div class="modal" id="VarientModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 580px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                            <h4 class="modal-title">Name :
                                <asp:Label ID="lblVarientname" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Date :
                                <asp:Label ID="lbldate" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp; 
                               Shift : 
                                <asp:Label ID="lblshift" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Section :
                                <asp:Label ID="lblsection" Font-Bold="true" runat="server"></asp:Label>

                            </h4>
                        </div>
                        <div class="modal-body">

                            <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>

                            <div class="row">

                                <asp:UpdatePanel ID="updatepnl" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-12">
                                            <div class="row" style="height: 450px; overflow: scroll;">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <section class="content">

                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <table class="table table-bordered">
                                                                    <%-- <tr class="text-center">
                                                    <th colspan="4">
                                                        </th>
                                                </tr>--%>
                                                                    <tr>
                                                                        <th>Receipt</th>
                                                                        <th>Qty.</th>
                                                                        <th>Issued</th>
                                                                        <th>Qty.</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Opening Balance</td>
                                                                        <td>
                                                                            <asp:Label ID="lblopeningBalance" Width="50%" Text="0" runat="server"></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="txtIssuedforstes_Text1" Text="Issued For Sale 1" runat="server"></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIssuedforstes" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </td>

                                                                    </tr>

                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                        <td>
                                                                            <asp:Label ID="txtIssuedforstes_Text2" Text="Issued For Sale 2" runat="server"></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIssuedforstes2" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                        <td>
                                                                            <asp:Label ID="txtIssuedforstes_Text3" Text="Issued For Sale 3" runat="server"></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIssuedforstes3" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                        <td>
                                                                            <asp:Label ID="txtIssuedforstes_Text4" Text="Issued For Sale 4" runat="server"></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIssuedforstes4" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>



                                                                    <tr>


                                                                        <td colspan="2">

                                                                            <asp:GridView ID="gvMilkinContainer_opening" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                                EmptyDataText="No Record Found.">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="37%">
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

                                                                            <asp:GridView ID="gv_SMTDetails_Opening" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                                EmptyDataText="No Record Found.">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="37%">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="txtQtyInLtr" CssClass="form-control" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:Label>
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
                                                                            <asp:Label ID="lblReceived" Width="50%" Text="0" runat="server"></asp:Label>
                                                                        </td>

                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>


                                                                    <tr>
                                                                        <td>Prepared</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPrepared" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="save"
                                                                        ErrorMessage="Enter Prepared" Text="<i class='fa fa-exclamation-circle' title='Enter Prepared!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                        <td>Issued For Whole Milk</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIssuedforWH" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="save"
                                                                        ErrorMessage="Enter Issued For Whole Milk" Text="<i class='fa fa-exclamation-circle' title='Enter Issued For Whole Milk!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Return</td>
                                                                        <td>
                                                                            <asp:TextBox ID="lblReturn" runat="server" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%"></asp:TextBox>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="save"
                                                                        ErrorMessage="Enter Return" Text="<i class='fa fa-exclamation-circle' title='Enter Return!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                        <td>Issued For Product Section</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIssuedforProductions" Enabled="false" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="save"
                                                                        ErrorMessage="Enter Issued For Product Section" Text="<i class='fa fa-exclamation-circle' title='Enter Issued For Product Section!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td>Issued For QC</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtLosses" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="save"
                                                                        ErrorMessage="Enter Losses" Text="<i class='fa fa-exclamation-circle' title='Enter Losses!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                    </tr>


                                                                    <tr>
                                                                        <td colspan="2"></td>

                                                                        <td colspan="2">



                                                                            <asp:GridView ID="gv_SMTDetails" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                                EmptyDataText="No Record Found.">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                            <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQtyInLtr" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <hr />

                                                                            <asp:GridView ID="gvMilkinContainer" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                                EmptyDataText="No Record Found.">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="50%">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                            <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQtyInKg" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>

                                                                            <hr />

                                                                            <asp:GridView ID="GVVariantDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                                EmptyDataText="No Record Found.">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Variant Name" HeaderStyle-Width="50%">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                            <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                                            <asp:Label ID="lblPackagingSize" Visible="false" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>
                                                                                            <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Qty. (In Pkt)">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQtyInPkt" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInPkt") %>' runat="server"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQtyInLtr" Text="0" runat="server"></asp:Label>
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
                                                                            <asp:TextBox ID="txtClosingBalance" Width="50%" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="save"
                                                                        ErrorMessage="Enter Closing Balance" Text="<i class='fa fa-exclamation-circle' title='Enter Closing Balance!'></i>"
                                                                        ControlToValidate="txtClosingBalance" ForeColor="Red" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Total</td>
                                                                        <td>
                                                                            <asp:Label ID="lblReceiptTotal" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>Total</td>
                                                                        <td>&nbsp; 
                                                                         <asp:Label ID="lblIssuedtotal" Font-Bold="true" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>

                                                        </section>
                                                        <!-- /.content -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btngettotalvarient" OnClick="btngettotalvarient_Click" runat="server" CausesValidation="false" CssClass="btn btn-primary" Text="Get Total" />
                            <asp:Button ID="btnPopupSave" Enabled="false" OnClientClick="return ValidateT()" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>


            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

            <div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT" OnClick="btnYesT_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>



            <div class="modal" id="VarientModel_FinalSheet">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 580px; width: 1024px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton1" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Name :
                                <asp:Label ID="lblName_WM" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Date :
                                <asp:Label ID="lblDate_WM" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp; 
                               Shift : 
                                <asp:Label ID="lblShift_WM" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Section :
                                <asp:Label ID="lblSection_WM" Font-Bold="true" runat="server"></asp:Label>

                            </h4>

                        </div>
                        <div class="modal-body">

                            <asp:Label ID="lblhmMSG" runat="server" Text=""></asp:Label>


                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 450px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row">

                                                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>--%>

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
                                                                    <asp:Label ID="lblWMSOpeningBalance" runat="server"></asp:Label>
                                                                </td>
                                                                <td>Issued For Sale To Product</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSIssuedForSaleToProduct" CssClass="form-control" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <%-- <td>Receipt:</td>--%>

                                                                <td colspan="2">

                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="e" ShowMessageBox="true" ShowSummary="false" />
                                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="f" ShowMessageBox="true" ShowSummary="false" />


                                                                    <fieldset>
                                                                        <legend>Add Tanker</legend>
                                                                        <asp:Label ID="lblmsgTanker" runat="server" Text=""></asp:Label>
                                                                        <table class="datatable table table-striped table-bordered table-hover"  runat="server"  id="FSTanker_R">
                                                                            <tr>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Tanker No <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="f" runat="server" Display="Dynamic" ControlToValidate="ddlTankerNo" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker No!'></i>" ErrorMessage="Enter Tanker No." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                            <asp:DropDownList ID="ddlTankerNo" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlTankerNo_SelectedIndexChanged" AutoPostBack="true">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>



                                                                                </td>
                                                                                 <td>
                                                                                    <div class="col-md-12" id="divTankerNameReceipt" runat="server" visible="false">
                                                                                        <div class="form-group">
                                                                                            <label>Tanker No<span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="e"
                                                                                                ErrorMessage="Enter Tanker No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker No !'></i>"
                                                                                                ControlToValidate="txtTankerNo_Receipt" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtTankerNo_Receipt" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="e"></asp:RegularExpressionValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="13" placeholder="XX-00-XX-0000"  ID="txtTankerNo_Receipt"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="f"
                                                                                                ErrorMessage="Enter Qty. (In Ltr)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Qty. (In Ltr) !'></i>"
                                                                                                ControlToValidate="txtQtyInLtrTanker" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" ID="txtQtyInLtrTanker" MaxLength="20" placeholder="Enter Qty. (In Ltr)"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-md-2">
                                                                                        <div class="form-group">
                                                                                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="btnLtrTanker_Click" ValidationGroup="f" ID="btnLtrTanker" Text="Add Tanker" />
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <hr />
                                                                        <asp:GridView ID="GV_TDetail_WM_Receipt" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInLtr" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="DeleteSeal" OnClick="lnkDelete_Click" Style="color: red; margin-top: 20px;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                         
                                                                        <hr />
                                                                        <asp:GridView ID="GV_TDetail_WM_Receipt1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Enabled="false" ID="txtQtyInLtr" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField> 
                                                                            </Columns>
                                                                        </asp:GridView>



                                                                    </fieldset>

                                                                    <asp:Label ID="lblWMSReceipt" Visible="false" runat="server"></asp:Label>
                                                                </td>

                                                                <td colspan="2">

                                                                    <fieldset>
                                                                        <legend>Add Tanker</legend>
                                                                        <asp:Label ID="lblmsg_issue" runat="server" Text=""></asp:Label>
                                                                        <table class="datatable table table-striped table-bordered table-hover"  runat="server" id="FSTanker_I">
                                                                            <tr>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Tanker No <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="e" runat="server" Display="Dynamic" ControlToValidate="ddlTankerNo_Issue" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker No!'></i>" ErrorMessage="Enter Tanker No." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                            <asp:DropDownList ID="ddlTankerNo_Issue" CssClass="form-control select2" OnSelectedIndexChanged="ddlTankerNo_Issue_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>



                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-md-12" id="divTankerName" runat="server" visible="false">
                                                                                        <div class="form-group">
                                                                                            <label>Tanker No<span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="e"
                                                                                                ErrorMessage="Enter Tanker No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Tanker No !'></i>"
                                                                                                ControlToValidate="txtTankerNo_Issue" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtTankerNo_Issue" Display="Dynamic" ValidationExpression="^[A-Z|a-z]{2}-\d{2}-[A-Z|a-z]{1,2}-\d{4}$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid vehicle no. format (XX-00-XX-0000)!'></i>" ErrorMessage="Invalid vehicle no. format (XX-00-XX-0000)" SetFocusOnError="true" ForeColor="Red" ValidationGroup="e"></asp:RegularExpressionValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control"  ID="txtTankerNo_Issue" ClientIDMode="Static" MaxLength="13" placeholder="XX-00-XX-0000"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="e"
                                                                                                ErrorMessage="Enter Qty. (In Ltr)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Qty. (In Ltr) !'></i>"
                                                                                                ControlToValidate="txtQtyInLtrTanker_Issue" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" ID="txtQtyInLtrTanker_Issue" MaxLength="20" placeholder="Enter Qty. (In Ltr)"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-md-2">
                                                                                        <div class="form-group">
                                                                                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="btn_Add_Issue_Click" ValidationGroup="e" ID="btn_Add_Issue" Text="Add Tanker" />
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <hr />
                                                                        <asp:GridView ID="GV_TDetail_WM_Issued" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInLtr" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete_Issue" runat="server" ToolTip="DeleteSeal" OnClick="lnkDelete_Issue_Click" Style="color: red; margin-top: 20px;" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>

                                                                        <hr />
                                                                        <asp:GridView ID="GV_TDetail_WM_Issued1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Enabled="false" ID="txtQtyInLtr" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField> 

                                                                            </Columns>
                                                                        </asp:GridView>


                                                                    </fieldset>

                                                                </td>


                                                            </tr>


                                                            <tr>
                                                                <td>Good</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSR_Good" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>Good</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSI_Good" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Sour</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSR_Sour" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>Sour</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSI_Sour" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>Curdled</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSR_Curdled" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>Curdled</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSI_Curdled" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td>CR</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSCR" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>Issued To Product Section</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSIssuedToProductSection" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td>W/B</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSWB" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>Losses</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSLosses" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Flushing</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSFlushing" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Width="50%" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>


                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                        EmptyDataText="No Record Found.">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Issue To WM" HeaderStyle-Width="39%">
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
                                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                        EmptyDataText="No Record Found.">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Prepared" HeaderStyle-Width="50%">
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

                                                                    <fieldset>
                                                                        <legend>Add Container</legend>
                                                                        <asp:Label ID="lblmsgContainer" runat="server" Text=""></asp:Label>
                                                                        <table class="datatable table table-striped table-bordered table-hover"  runat="server"  id="FSContainer">
                                                                            <tr>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Container Name <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="rfvtxtQtyInLtr" ValidationGroup="c" runat="server" Display="Dynamic" ControlToValidate="ddlContainer" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Enter Container Name!'></i>" ErrorMessage="Enter Container Name." SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                            <asp:DropDownList ID="ddlContainer" CssClass="form-control select2" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>

                                                                                    <div class="col-md-12">
                                                                                        <div class="form-group">
                                                                                            <label>Qty. (In Ltr) <span style="color: red;">*</span></label>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="c"
                                                                                                ErrorMessage="Enter Qty. (In Ltr)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Qty. (In Ltr) !'></i>"
                                                                                                ControlToValidate="txtQtyInLtr" Display="Dynamic" runat="server">
                                                                                            </asp:RequiredFieldValidator>
                                                                                            <asp:TextBox autocomplete="off" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" ID="txtQtyInLtr" MaxLength="20" placeholder="Enter Qty. (In Ltr)"></asp:TextBox>

                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                                <td>
                                                                                    <div class="col-md-2">
                                                                                        <div class="form-group">
                                                                                            <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="btnaddContainer_Click" ValidationGroup="c" ID="btnaddContainer" Text="Add Container" />
                                                                                        </div>
                                                                                    </div>

                                                                                </td>
                                                                            </tr>
                                                                        </table>

                                                                        <hr />

                                                                        <asp:GridView ID="GV_CD_WM" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQtyInKg" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" Style="margin-top: 20px; color: red;" ToolTip="DeleteContainer" OnClick="lnkDelete_Click1" OnClientClick="return confirm('Are you sure to Delete this record?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>


                                                                         <hr />

                                                                        <asp:GridView ID="GV_CD_WM1" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="50%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox  Enabled="false" ID="txtQtyInKg" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </fieldset>

                                                                    <%--<asp:GridView ID="GV_CD_WM" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                        EmptyDataText="No Record Found.">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Container Name" HeaderStyle-Width="50%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblI_MCID" Visible="false" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty. (In Ltr)">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtQtyInKgCM" AutoPostBack="true" OnTextChanged="txtQtyInKgCM_TextChanged" autocomplete="off" CssClass="form-control" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' runat="server"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>--%>

                                                                </td>



                                                            </tr>


                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td>Closing Balance</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWMSClosingBalance" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>



                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblWMSR_Total" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="lblWMSI_Total" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </table>

                                                    </div>

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnFinalPopup" OnClientClick="return ValidateT_Final()" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Update" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>



            <div class="modal fade" id="VarientModelF_Final" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT_Final">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblFinalpopupsheetmsg" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Save" ID="btnYes_Final" OnClick="btnYes_Final_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="btnclose_Final" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

            <%--For Recombination Reconstitution Sheet--%>

            <div class="modal" id="RecombinationReconstitution_FinalSheet">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 500px; width: 1024px">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton2" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Name :
                                <asp:Label ID="lblName_RAR" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Date :
                                <asp:Label ID="lblDate_RAR" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp; 
                               Shift : 
                                <asp:Label ID="lblShift_RAR" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Section :
                                <asp:Label ID="lblsection_RAR" Font-Bold="true" runat="server"></asp:Label>

                            </h4>

                        </div>
                        <div class="modal-body">

                            <asp:Label ID="lblRandRSheet" runat="server" Text=""></asp:Label>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 360px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblerrorPopR" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <span style="color: red;">Step 1:- Click On Get Total Button</span>
                                                            <br />
                                                            <span style="color: red;">Step 2:- Click On Save Button</span>
                                                        </div>
                                                        <hr />
                                                    </div>
                                                    <div class="row">

                                                        <!-------------->
                                                        <div class="col-md-12">

                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GVRRSheet" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                                                        <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Balance <br/>B.F." HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBalance_BFRR" Enabled="false" Text='<%# Eval("RR_OpeningBalance_New") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                        <asp:Label ID="lblMilkProductionProcessRRS_Id" runat="server"  Text='<%# Eval("MilkProductionProcessRRS_Id") %>'  CssClass="hidden"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Obtained" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_Obtained") %>' ID="txtRRObtained" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" OnTextChanged="txtRRObtained_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_Total") %>' ID="txtRRTotal" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Toning" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_Toning") %>' ID="txtRRToning" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" OnTextChanged="txtRRToning_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Maintaining <br />S.N.F." HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_MaintainingSNF") %>' ID="txtRRMaintainingSNF" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" OnTextChanged="txtRRMaintainingSNF_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Issued For<br />Product Section" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_IssuedForProductSection") %>' ID="txtRRIssueforproductionsection" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" OnTextChanged="txtRRIssueforproductionsection_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total Issued" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_TotalIssued") %>' ID="txtRRTotalIssued" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Closing <br />Balance" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox Text='<%# Eval("RR_ClosingBalance") %>' ID="txtRRClosingBalance" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                            <%--<table class="table table-bordered">

                                                                <tr>
                                                                    <th>Particular</th>
                                                                    <th>B.F.</th>
                                                                    <th>Obtained</th>
                                                                    <th>Total</th>
                                                                    <th>Toning</th>
                                                                    <th>Maintaining S.N.F.</th>
                                                                    <th>Issued For
                                                                        <br />
                                                                        Product Section</th>
                                                                    <th>Total Issued</th>
                                                                    <th>Closing Balance</th>
                                                                </tr>
                                                                <tr>
                                                                    <th>S.M.P.</th>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox2" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox3" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox4" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox5" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox6" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox7" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox8" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Ghee</th>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox9" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox10" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox11" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox12" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox13" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox14" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox15" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox16" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Cream</th>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox17" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox18" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox19" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox20" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox21" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox22" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox23" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox24" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>White Butter</th>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox25" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox26" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox27" Enabled="false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox28" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox29" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox30" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox31" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox32" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox></td>
                                                                </tr>

                                                            </table>--%>
                                                        </div>

                                                        <!-------------->

                                                    </div>

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnGetTotal" OnClick="btnGetTotal_Click" Visible="false" runat="server" CssClass="btn btn-primary" Text="Get Total" />
                            <asp:Button ID="btnNextRRsheet" Enabled="true"  Visible="false" runat="server" ValidationGroup="B" CssClass="btn btn-primary" Text="Next" OnClick="btnNextRRsheet_Click"/>
                            <asp:Button ID="btnrorrsheet" Enabled="true" OnClientClick="return ValidateT_Final_RR()" runat="server" ValidationGroup="b" CssClass="btn btn-primary" Text="Save" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal fade" id="VarientModelF_Final_RANDR" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT_Final_RR">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblFinalpopupsheetmsgRR" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Save" ID="btnsaveRR" OnClick="btnsaveRR_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button3" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>



            <%--For Cow Milk Sheet--%>

            <div class="modal" id="CowMilk_FinalSheet">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 360px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton3" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Name :
                                <asp:Label ID="lbltitle_cwm" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Date :
                                <asp:Label ID="lbldate_cwm" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp; 
                               Shift : 
                                <asp:Label ID="lblshift_cwm" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                               Section :
                                <asp:Label ID="lblsection_cwm" Font-Bold="true" runat="server"></asp:Label>

                            </h4>

                        </div>
                        <div class="modal-body">

                            <asp:Label ID="lblerrorPopCW11" runat="server" Text=""></asp:Label>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 280px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblerrorPopCW" runat="server"></asp:Label>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <span style="color: red;">Step 1:- Click On Get Total Button</span>
                                                            <br />
                                                            <span style="color: red;">Step 2:- Click On Save Button</span>
                                                        </div>
                                                        <hr />
                                                    </div>

                                                    <div class="row">

                                                        <!-------------->
                                                        <div class="col-md-12">

                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GVCW_Sheet" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
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
                                                                                        <asp:TextBox ID="txtBalance_BFCw" Enabled="false" Text='<%# Eval("New_OpeningBalance") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Prepard" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWPrepard" Text='<%# Eval("CM_Prepard") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWTotal" Text='<%# Eval("CM_Total") %>' Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Sale" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWSale" Text='<%# Eval("CM_Sale") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Issued Towm" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWIssuedTowm" Text='<%# Eval("CM_IssuedTowm") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Loss" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWLoss" Text='<%# Eval("CM_Loss") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Cl. B." HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWClosingBalance" Text='<%# Eval("CM_ClosingBalance") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total Issued" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCWTotalIssued" Text='<%# Eval("CM_TotalIssued") %>' Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                            <%--<table class="table table-bordered">

                                                                    <tr>
                                                                        <th>Particular</th>
                                                                        <th>O/B</th>
                                                                        <th>Prepard</th>
                                                                        <th>Total</th>
                                                                        <th>Sale</th>
                                                                        <th>Issued Towm</th>
                                                                        <th>Loss</th>
                                                                        <th>Cl. B.</th>
                                                                        <th>Total</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th>Cow Milk</th>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>

                                                                </table>--%>
                                                        </div>
                                                        <!-------------->

                                                    </div>

                                                    <div class="modal-footer">
                                                        <asp:Button ID="btnCWGetTotal" OnClick="btnCWGetTotal_Click" runat="server" CssClass="btn btn-primary" Text="Get Total" />
                                                        <asp:Button ID="btnrocwsheet" Enabled="false" OnClientClick="return CowM_Final_RR()" runat="server" ValidationGroup="b" CssClass="btn btn-primary" Text="Save" />
                                                    </div>

                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal fade" id="CowMilk_FinalSheet_Submit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT_Final_CW">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblFcowmsg" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Save" ID="btnCwS" OnClick="btnCwS_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button5" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>





        </section>

    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function VarientModelF() {
            $("#VarientModel").modal('show');
        }

        function ValidateT() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnPopupSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnPopupSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }

    </script>

    <script>

        function VarientModelF_Final() {
            $("#VarientModel_FinalSheet").modal('show');
        }

        function RecombinationReconstitution_Final() {
            $("#RecombinationReconstitution_FinalSheet").modal('show');
        }

        function CowMilk_Final() {
            $("#CowMilk_FinalSheet").modal('show');
        }

        function ValidateT_Final() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnYes_Final.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblFinalpopupsheetmsg.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#VarientModelF_Final').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnYes_Final.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblFinalpopupsheetmsg.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#VarientModelF_Final').modal('show');
                    return false;
                }
            }
        }


        function ValidateT_Final_RR() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnsaveRR.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblFinalpopupsheetmsgRR.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#VarientModelF_Final_RANDR').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnsaveRR.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblFinalpopupsheetmsgRR.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#VarientModelF_Final_RANDR').modal('show');
                    return false;
                }
            }
        }



        function CowM_Final_RR() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnCwS.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblFcowmsg.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#CowMilk_FinalSheet_Submit').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnCwS.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblFcowmsg.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#CowMilk_FinalSheet_Submit').modal('show');
                    return false;
                }
            }
        }





    </script>

</asp:Content>
