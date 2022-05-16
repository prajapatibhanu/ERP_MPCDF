﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DebitNote.aspx.cs" Inherits="mis_Finance_DebitNote" %>

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
        .control1 {           
            height: 27px !important;
            padding: 2px 3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success" style="background-color: #faf4c7">
                        <div class="box-header">
                            <h3 class="box-title">Purchase Return (Debit Note)</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Panel ID="panel1" runat="server">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Voucher/Ref No.<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_Ref" placeholder="Enter Voucher No..." ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtVoucherTx_Ref" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 pull-left">
                                        <div class="form-group">
                                            <label>Voucher Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtVoucherTxDate" runat="server" CssClass="form-control DateAdd" placeholder="Enter Voucher No..." autocomplete="off"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblPreviousVoucherNo" runat="server" Text="" Font-Bold="true" style="color:blue"></asp:Label>
                            </div>
                        </div>
                    </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Voucher/Bill No.<span style="color: red;"> *</span></label>
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <asp:Label ID="lblVoucherTx_No" runat="server" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                            </div>
                                            <div class="col-md-6" style="margin-left: -32px;">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." ClientIDMode="Static" MaxLength="7" onkeypress="return validateNum(event);" autocomplete="off"></asp:TextBox>
                                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Party A/c Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlPartyName" CssClass="form-control select2" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlPartyName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Current Balance<span style="color: red;"> *</span></label>
                                            <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtVoucherTx_Date" runat="server" CssClass="form-control DateAdd" placeholder="Enter Voucher No..." autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="itemdetail" runat="server">
                                    <fieldset>
                                        <legend>Item Detail</legend>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ItemID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="GridViewItem_SelectedIndexChanged">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                                <ItemTemplate>

                                                                    <asp:CheckBox ID="chkboxid" onchange="CalculateGridAmount()" runat="server" OnCheckedChanged="chkboxid_CheckedChanged" AutoPostBack="true" />
                                                                    <asp:TextBox ID="txtIGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:TextBox>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblHSNCode" CssClass="hidden" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WareHouse">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:TextBox>
                                                                    <asp:Label ID="lblWarehouse_Name" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" CssClass="hidden" Text='<%# Eval("Quantity").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control control1" Text='<%# Eval("Quantity").ToString()%>' ToolTip='<%# Eval("ItemID").ToString()%>' MaxLength="12" onkeypress="return validateDec(this,event);" onfocusout="CalculateGridAmount()" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate Per">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="txtRate" runat="server" CssClass="hidden" Text='<%# Eval("Rate").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount ">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="txtAmountH" runat="server" CssClass="form-control control1" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CGST">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control control1" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:TextBox>
                                                                    <asp:Label ID="CGST" CssClass="hidden" runat="server" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTPer" runat="server" Text='<%# string.Concat("(",Eval("CGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                                    <asp:Label ID="lblCGST_Per" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SGST">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control control1" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:TextBox>
                                                                    <asp:Label ID="SGST" CssClass="hidden" runat="server" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTPer" runat="server" Text='<%# string.Concat("(",Eval("SGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                                    <asp:Label ID="lblSGST_Per" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IGST">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control control1" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:TextBox>
                                                                    <asp:Label ID="IGST" CssClass="hidden" runat="server" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTPer" runat="server" Text='<%# string.Concat("(",Eval("IGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                                    <asp:Label ID="lblIGST_Per" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Sales Ledger">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLedgerID" CssClass="hidden" runat="server" Text='<%# Eval("Ledger_ID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblLedgerName" runat="server" Text='<%# Eval("Ledger_Name").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-6"></div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>Amount Detail</legend>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div id="divamount" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <label>Ledger</label>
                                                                    <asp:DropDownList ID="ddlLedger" runat="server" CssClass="form-control select2">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <label>Amount</label>
                                                                    <asp:TextBox ID="txtLedgerAmt" runat="server" CssClass="form-control" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>&nbsp;</label>
                                                                    <asp:Button ID="btnAddLedgerAmt" runat="server" CssClass="btn btn-block" Text="Add" OnClick="btnAddLedgerAmt_Click" OnClientClick="return ValidateAddLedger()" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="GridViewLedger_RowDeleting">
                                                        <Columns>
                                                            <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"><img src="../image/Del.png" /></asp:LinkButton>
                                                                    <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" onblur="CalculateGrandTotal();" CssClass="form-control AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>' MaxLength="12"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>

                                                    <hr />
                                                    <span style="color: red">Suggested Round Off:  </span>
                                                    <asp:Label ID="lblroundsuggestion" runat="server" ClientIDMode="Static" Text=""></asp:Label><br />
                                                    <table class="table table-bordered customCSS">
                                                        <tr>
                                                            <th>GRAND TOTAL :
                                                            </th>
                                                            <th style="width: 50%; padding: 3px;">
                                                                <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0"></asp:TextBox>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Narration<span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtVoucherTx_Narration" runat="server" TextMode="MultiLine" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnAccept" Text="Accept" OnClick="btnAccept_Click" OnClientClick="return validateAccept();" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a href="DebitNote.aspx" id="btn_Clear" runat="server" class="btn btn-block btn-default">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <div id="ModalReferance" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Bill-wise Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Type of Ref<span style="color: red;"> *</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRefType" OnSelectedIndexChanged="ddlRefType_SelectedIndexChanged" AutoPostBack="true">
                                                <%--<asp:ListItem Value="0">Advance</asp:ListItem>--%>

                                                <asp:ListItem Value="2">New Ref</asp:ListItem>
                                                <asp:ListItem Value="1">Agst Ref</asp:ListItem>
                                                <%--<asp:ListItem Value="3">On Account</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <small><span id="valddlRefType" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Name<span style="color: red;">*</span></label>
                                            <asp:HyperLink ID="lnkView" onclick="ShowRefDetailModal();" Visible="false" runat="server">View AgstRef</asp:HyperLink>
                                            <asp:TextBox runat="server" ID="txtBillByBillTx_Ref" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="false" ID="ddlBillByBillTx_Ref">
                                            </asp:DropDownList>
                                            <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Amount<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event);" MaxLength="12" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cr/Dr<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlBillByBillTx_crdr" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                                <asp:ListItem Value="Dr">Debit</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlBillByBillTx_crdr" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:Button runat="server" Text="Add" ID="btnAddBillByBill" ClientIDMode="Static" CssClass="btn btn-block btn-default" OnClick="btnAddBillByBill_Click" OnClientClick="return validateBillByBill();"></asp:Button>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridViewRef" runat="server" DataKeyNames="RID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="true" OnRowDeleting="GridViewRef_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type of Ref">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" OnClientClick="return confirm('The Type of Ref will be deleted. Are you sure want to continue?');"><img src="../image/Del.png" /></asp:LinkButton>
                                                        <asp:Label ID="lblTypeOfRef" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_RefType").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblRefNo" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_Ref").ToString()%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr/Dr" SortExpression="leftBonus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%# Bind("BillByBillTxType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <table class="table table-bordered hidden">
                                            <tr>
                                                <th style="width: 50%;">VOUCHER GRAND TOTAL :&nbsp;
                                                   <%-- <asp:Label ID="lblGrandTotal" ClientIDMode="Static" runat="server" Text="0"></asp:Label>--%>
                                                </th>

                                                <th>REF. TOTAL :&nbsp;
                                                    <asp:Label ID="lblRefTotal" ClientIDMode="Static" runat="server" Text="0"></asp:Label>
                                                </th>
                                            </tr>

                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSubmit" Text="Final Submit" OnClick="btnSubmit_Click" Visible="false" OnClientClick="return validateSubmit()" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
                <div id="ParticularDetailViewModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Accounting Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:GridView ID="GridViewParticularDetailViewModal" runat="server" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="true">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Particulars ">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblParticularsName" CssClass="paddingLR" runat="server" Text='<%# Eval("ParticularName").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="150">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" CssClass="form-control paddingLR AlignR" runat="server" Text='<%# Eval("ParticularAmt").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                </div>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <%--AgstRef Detail--%>
            <div class="modal fade" id="AgstRefModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Against Ref Details </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewRefDetail" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VoucherTx_Date" HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20%" />

                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button runat="server" Text="Add" ID="btnBillByBillSave" OnClick="btnBillByBillSave_Click" ClientIDMode="Static" CssClass="btn btn-success"></asp:Button>--%>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ModalChequeDetail" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Cheque-wise Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cheque/ DD No.</label>
                                        <asp:TextBox ID="txtChequeTx_No" onkeypress="return validateNum(event);" runat="server" placeholder="Enter Cheque/ DD No." MaxLength="6" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtChequeTx_No" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cheque/ DD Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeTx_Date" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtChequeTx_Date" style="color: red;"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Amount<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtChequeTx_Amount" placeholder="Enter Amount" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtChequeTx_Amount" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" Text="Add" CssClass="btn btn-block btn-default" ID="btnAddCheque" OnClick="btnAddCheque_Click" OnClientClick="return validateCheque();"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="RID" ShowHeaderWhenEmpty="true" ID="GVFinChequeTx" AutoGenerateColumns="false" OnRowDeleting="GVFinChequeTx_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" OnClientClick="return confirm('cheque detail will be deleted. Are you sure want to continue?');"><img src="../image/Del.png" /></asp:LinkButton>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_No" runat="server" Text='<%# Eval("ChequeTx_No") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_Date" runat="server" Text='<%# Eval("ChequeTx_Date") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_Amount" runat="server" Text='<%# Eval("ChequeTx_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <%-- <asp:Button runat="server" Text="Add" ID="btnAddChequeDetail" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnAddChequeDetail_Click"></asp:Button>--%>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="ModalChequeDetailView" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Cheque Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVViewFinChequeTx" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No." />
                                                <asp:BoundField DataField="ChequeTx_Date" HeaderText="Cheque/ DD Date" />
                                                <asp:BoundField DataField="ChequeTx_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function CalculateGST() {
            debugger;
            var SumCGST = 0;
            var SumSGST = 0;
            var SumIGST = 0;
            var i = 0;
            $('#GridViewItem tr').each(function (index) {


                debugger;
                if (i > 0) {
                    if ($(this).children("td").eq(0).find('input[type="checkbox"]').is(':checked')) {
                        var CGST = $(this).children("td").eq(6).find('input[type="text"]').val();
                        var SGST = $(this).children("td").eq(7).find('input[type="text"]').val();
                        var IGST = $(this).children("td").eq(8).find('input[type="text"]').val();

                        if (CGST == "")
                            CGST = 0;

                        if (SGST == "")
                            SGST = 0;

                        if (IGST == "")
                            IGST = 0;

                        SumCGST = parseFloat(SumCGST) + parseFloat(CGST);
                        SumSGST = parseFloat(SumSGST) + parseFloat(SGST);
                        SumIGST = parseFloat(SumIGST) + parseFloat(IGST);
                    }
                }
                i++;
            });

            i = 0;
            $('#GridViewLedger tr').each(function (index) {

                if (i == 0) {
                    $(this).children("td").eq(1).find('input[type="text"]').val(parseFloat(SumCGST).toFixed(2));
                }
                else if (i == 1) {
                    $(this).children("td").eq(1).find('input[type="text"]').val(parseFloat(SumSGST).toFixed(2));
                }
                else if (i == 2) {
                    $(this).children("td").eq(1).find('input[type="text"]').val(parseFloat(SumIGST).toFixed(2));
                }
                i++;

            });

        }
        function CalculateGridAmount() {

            var i = 0;
            var Amount = 0;
            var Tval = 0;
            //var TotalRemaining = 0;
            var trCount = $('#GridViewItem tr').length - 1;

            $('#GridViewItem tr').each(function (index) {
                debugger;


                var Quantity = $(this).children("td").eq(3).find('input[type="text"]').val();
                var Rate = $(this).children("td").eq(4).find('input[type="text"]').val();
                var CGST = $(this).children("td").eq(6).find('input[type="text"]').val();
                var CGSTPer = $(this).children("td").eq(1).find('input[type="text"]').val();
                var SGST = $(this).children("td").eq(7).find('input[type="text"]').val();
                var IGST = $(this).children("td").eq(8).find('input[type="text"]').val();
                var SGSTPer = $(this).children("td").eq(2).find('input[type="text"]').val();
                var IGSTPer = $(this).children("td").eq(0).find('input[type="text"]').val();
                var Amount = Amount;
                Amount = parseFloat(parseFloat(Quantity) * parseFloat(Rate)).toFixed(2);
                CGST = parseFloat(Amount * (CGSTPer / 100)).toFixed(2);
                SGST = parseFloat(Amount * (SGSTPer / 100)).toFixed(2);
                IGST = parseFloat(Amount * (IGSTPer / 100)).toFixed(2);
                $(this).children("td").eq(5).find('input[type="text"]').val(Amount);
                $(this).children("td").eq(6).find('input[type="text"]').val(CGST);
                $(this).children("td").eq(7).find('input[type="text"]').val(SGST);
                $(this).children("td").eq(8).find('input[type="text"]').val(IGST);


            });

            //$('#GridViewLedger tr').each(function (index) {


            //    $('#GridViewItem tr').each(function (index) {
            //        debugger;
            //        if (i > 0) {
            //            var temp = Tval;
            //            var val = $(this).children("td").eq(6).find('input[type="text"]').val();
            //            Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
            //        }
            //        i++;

            //    });
            //    $(this).children("td").eq(1).find('input[type="text"]').val(Tval);
            //});
            CalculateGST();
            CalculateGrandTotal();
            //document.getElementById("txtTotalEarning").value = TotalPaidEarning;
            //document.getElementById("txtETotalRemaining").value = TotalPaidEarning;

            //var TotalDeduction = document.getElementById("txtTotalDeduction").value.trim();
            //if (TotalDeduction.trim() == "")
            //    TotalDeduction = 0;

            //document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalPaidEarning) - parseFloat(TotalDeduction)).toFixed(2);

            //return true;
        }

        function ShowModal() {
            $("#ModalItemDetail").modal();
        }
        function ShowReferanceModal() {
            $("#ModalReferance").modal();
        }
        function ParticularDetailViewModal() {
            $('#ParticularDetailViewModal').modal('show');
        }
        function ShowRefDetailModal() {
            $('#AgstRefModal').modal('show');

        }
        function ShowModalChequeDetail() {
            $('#ModalChequeDetail').modal('show');
        }
        function ShowModalChequeDetailView() {
            $('#ModalChequeDetailView').modal('show');
        }

        $(document).ready(function () {
            CalculateGrandTotal();
        });

        function CalculateGrandTotal() {
            debugger;
            var i = 0;
            var Tval = 0;

            $('#GridViewItem tr').each(function (index) {
                debugger;
                if (i > 0) {
                    if ($(this).children("td").eq(0).find('input[type="checkbox"]').is(':checked')) {
                        var temp = Tval;
                        var val = $(this).children("td").eq(5).find('input[type="text"]').val();

                        if (val == "")
                            val = 0;

                        Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                    }

                }
                i++;
            });
            $('#GridViewLedger tr').each(function (index) {
                if (i > 0) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                }
                i++;
            });

            document.getElementById('<%=lblGrandTotal.ClientID%>').value = Tval;
            document.getElementById('<%=lblroundsuggestion.ClientID%>').innerText = (Math.round(Tval) - Tval).toFixed(2);
            // document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML = Tval;




        }

        <%--function ChangeRef() {
            $("#lblRefNo").hide();
            $("#txtRefNo").hide();
            $("#ddlRefNo").hide();
            var RefType = document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex;
            if (RefType == 0 || RefType == 2) {
                $("#txtRefNo").show();
            }
            else if (RefType == 3) {
                $("#lblRefNo").show();
            }
            else {
                $("#ddlRefNo").show();
            }
        }--%>
        function validateBillByBill() {
            var msg = "";
            $("#valddlBillByBillTx_Ref").html("");
            $("#valtxtBillByBillTx_Amount").html("");
            if (document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex == 1) {
                if (document.getElementById('<%=ddlBillByBillTx_Ref.ClientID%>').selectedIndex == 0) {
                    msg += "Select Name \n";
                    $("#valddlBillByBillTx_Ref").html("Select Name");
                }
            }
            else if (document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex == 0) {
                if (document.getElementById('<%=txtBillByBillTx_Ref.ClientID%>').value.trim() == "") {
                    msg += "Enter Name \n";
                    $("#valddlBillByBillTx_Ref").html("Enter Name");
                }

            }
            else {
                $("#valddlBillByBillTx_Ref").html("");
            }


        if (document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount \n";
                $("#valtxtBillByBillTx_Amount").html("Enter Amount");
            }
            if (document.getElementById('<%=ddlBillByBillTx_crdr.ClientID%>').selectedIndex == 0) {
                msg += "Select Cr/Dr \n";
                $("#valddlBillByBillTx_crdr").html("Select Cr/Dr");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                return true;

            }
        }
        function ValidateItemAdd() {
            var msg = "";
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party Name.";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
                // ShowModal();
            }
        }
        function ValidateAddLedger() {
            var msg = "";
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Ledger.\n";
            }
            if (document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount.";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
        function validateCheque() {
            debugger;
            var msg = "";
            $("#valtxtChequeTx_No").html("");
            //$("#valtxtChequeTx_Date").html("");
            $("#valtxtChequeTx_Amount").html("");
            var CheckNo = document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim();
            if (document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim() != "") {
                if (CheckNo.length != 6) {
                    msg += "Enter 6 Digit Cheque/ DD No.  \n";
                    $("#valtxtChequeTx_No").html("Enter  6 Digit Cheque/ DD No");
                }
            }

            if (document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount. \n";
                $("#valtxtChequeTx_Amount").html("Enter Amount");
            }
            if (document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim() != "") {

                var LedgerAmount = parseFloat(document.getElementById('<%=lblGrandTotal.ClientID%>').value);
                var ChequeAmount = parseFloat(document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value);

                var rowscount = $("#<%=GVFinChequeTx.ClientID %> tr").length;
                if (rowscount > "1") {
                    <%--var grid = document.getElementById('<%=GVFinChequeTx.ClientID %>');
                    var gridinput = grid.getElementsByTagName('input');
                    var sum = 0 * 1;

                    for (i = 0; i < gridinput.length; i++) {
                        var value1 = gridinput[i].value;
                        sum = parseFloat(sum) + parseFloat(value1);
                    }
                    LedgerAmount = parseFloat(LedgerAmount) - parseFloat(sum);--%>
                    if (ChequeAmount >= LedgerAmount) {

                        msg += "Enter Valid Amount. \n";
                        $("#valtxtChequeTx_Amount").html("Enter Valid Amount");
                    }

                }
                else {
                    if (ChequeAmount > LedgerAmount) {
                        msg += "Enter Valid Amount. \n";
                        $("#valtxtChequeTx_Amount").html("Enter Valid Amount");
                    }

                }


            }
            if (msg != "") {
                alert(msg);
                return false;

            }
            else {

                return true;
            }
        }

        function validateAccept() {
            var msg = "";
            debugger;

            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher No. \n";
            }

            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
               msg = msg + "Enter Date. \n";
           }
           if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
               msg = msg + "Select Party A/c Name. \n";
           }
           var gridView = document.getElementById('<%= GridViewItem.ClientID %>');
           for (var i = 1; i < gridView.rows.length; i++) {
               var inputs = gridView.rows[i].getElementsByTagName('input');
               if (inputs != null) {
                   if (inputs[i].type == "checkbox") {
                       if (inputs[i].checked) {
                           isValid = true;
                           break;
                       }
                       else {
                           msg = msg + "select atleast one Item. \n";
                           break;
                       }
                   }
               }
           }

           if (document.getElementById('<%=txtVoucherTx_Narration.ClientID%>').value.trim() == "") {
               msg = msg + "Enter Narration. \n";
           }

           if (msg != "") {
               alert(msg);
               return false;
           }
           else {
               if (confirm("Do you really want to Accept Details ?")) {
                   return true;
               }
               else {
                   return false;
               }

           }
       }

       function validateSubmit() {
           var msg = "";
           debugger;
           var VoucherGrandTotal = document.getElementById('<%=lblGrandTotal.ClientID%>').value.trim();
            var RefTotal = document.getElementById('<%=lblRefTotal.ClientID%>').innerHTML;

            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher No. \n";
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date. \n";
            }
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party A/c Name. \n";
            }
            //if (parseFloat(VoucherGrandTotal) != parseFloat(RefTotal)) {
            //    msg += "Amount Not Clear. \n";
            //}
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Submit Details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

    </script>
</asp:Content>
