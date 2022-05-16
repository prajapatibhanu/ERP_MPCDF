﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherItemCreditNote.aspx.cs" Inherits="mis_Finance_VoucherItemCreditNote" %>

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

        .select2 {
            width: 100% !important;
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
                            <div class="row">
                                <div class="col-md-4">
                                    <h3 class="box-title">Item Credit Note Voucher</h3>
                                </div>
                                <div class="col-md-8">
                                    <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right hidden" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>

                                     <a target="_blank" href="LedgerMasterB.aspx" class="btn btn-primary pull-right">Add Ledger</a>
                                    <asp:LinkButton ID="btnRefreshLedgerList" class="btn btn-primary pull-right Aselect1" Style="margin-right: 10px;" runat="server" OnClick="btnRefreshLedgerList_Click">Refresh Ledger & Item</asp:LinkButton>

                                    <asp:LinkButton ID="lnkPreviousVoucher" class="btn btn-primary pull-right" Style="margin-right: 10px; display:none;" runat="server" OnClick="lnkPreviousVoucher_Click">Copy Previous Voucher</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblPreviousVoucherNo" runat="server" Text="" Font-Bold="true" Style="color: blue"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panel1" runat="server">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Voucher No.<span style="color: red;"> *</span></label>
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <asp:Label ID="lblVoucherNo" runat="server" Visible="false" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                                <asp:Label ID="lblVoucherTx_No" runat="server" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                            </div>
                                            <div class="col-md-6" style="margin-left: -32px;">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher/Bill No." ClientIDMode="Static" MaxLength="6" onkeypress="return validateNum(event)" autocomplete="off"></asp:TextBox>
                                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label>Supplier's Invoice No.<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtInvoice" placeholder="Enter Supplier's Invoice No." MaxLength="16" autocomplete="off" onchange="FillNarration();"></asp:TextBox>
                                            <small><span id="valtxtInvoice" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2 hidden">
                                        <div class="form-group">
                                            <label>Supplier's Invoice Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtSupplierInvoiceDate" runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" onchange="FillNarration(),CompareSupplierInvocieDate();" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtVoucherTx_Date" runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" onchange="CompareSupplierInvocieDate();" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Party A/c Name (CR)<span style="color: red;"> *</span></label>&nbsp;&nbsp; (<asp:Label ID="lblPAN_Status" runat="server" Style="color: red" Text=""></asp:Label>)
                                            <asp:DropDownList ID="ddlPartyName" CssClass="form-control select1 select2" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" onchange="FillNarration();">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                            <small><span id="valddlPartyName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Current Balance<span style="color: red;"> *</span></label>
                                            &nbsp;&nbsp; (<asp:Label ID="lblTurnOver" runat="server" Style="color: red" Text=""></asp:Label>)
                                            <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkitem" ClientIDMode="Static" runat="server" OnCheckedChanged="chkitem_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;<label>Add Item</label>
                                    </div>
                                </div>
                                <asp:Panel ID="itemdetail" runat="server">
                                    <fieldset>
                                        <legend>Item Detail</legend>
                                        <div id="divitem" runat="server">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Item Name<span style="color: red;"> *</span></label><br />
                                                        <asp:DropDownList ID="ddlItem" CssClass="form-control select1 select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Warehouse<span style="color: red;"> *</span></label><br />
                                                        <asp:DropDownList ID="ddlWarehouse" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Quantity<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantity" onkeypress="return validateDecUnit(this,event)" placeholder="Enter Quantity..." onchange="CalculateAmount();" MaxLength="8" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblUnit" CssClass="hidden" runat="server" Text="2"></asp:Label>
                                                </div>
                                                <div class="col-md-2 hidden">
                                                    <div class="form-group">
                                                        <label>Unit<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUnitName" placeholder="Enter Unit..."></asp:TextBox>
                                                        <asp:Label ID="lblUnitName" CssClass="hidden" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Rate Per<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" onkeypress="return validateDec(this,event);" ID="txtRate" placeholder="Enter Rate..." onchange="CalculateAmount();" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Amount<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAmount" placeholder="Enter Amount..." MaxLength="12" autocomplete="off" onchange="CalculateRate();" onkeypress="return validateDec(this,event);" onblur="return validateAmount(this);"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" class="btn btn-success btn-block" Style="margin-top: 25px;" runat="server" OnClick="btnAdd_Click" Text="Add" OnClientClick="return ValidateItemAdd();"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- ItemDetail GridView-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting" ShowFooter="true">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblItemRowNo" Text='<%# Eval("ID").ToString()%>' CssClass="hidden" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HSN Code">
                                                                <HeaderStyle />
                                                                <%-- <ItemStyle HorizontalAlign="Right" />--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHSNCode" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                                    <%-- <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WareHouse">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWarehouse_Name" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate Per">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount ">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CGST">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="CGST" CssClass="hidden" runat="server" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGST" CssClass="paddingLR" runat="server" Text='<%# string.Concat(Eval("CGSTAmt").ToString(),"(", Eval("CGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SGST">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="SGST" CssClass="hidden" runat="server" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGST" CssClass="paddingLR" runat="server" Text='<%# string.Concat(Eval("SGSTAmt").ToString(),"(",Eval("SGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IGST">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="IGST" CssClass="hidden" runat="server" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGST" CssClass="paddingLR" runat="server" Text='<%# string.Concat(Eval("IGSTAmt").ToString(),"(",Eval("IGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cess Tax">
                                                                <HeaderStyle />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="CessTax" CssClass="hidden" runat="server" Text='<%# Eval("CessTaxAmount").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCessTax" CssClass="paddingLR" runat="server" Text='<%# Eval("CessTaxCalType").ToString() == "On Value"?string.Concat(Eval("CessTaxAmount").ToString(),"  (",Eval("CessTaxRate").ToString(),"%",")"):string.Concat(Eval("CessTaxAmount").ToString(),"(",Eval("CessTaxRate").ToString(),"/ Unit",")")  %>'></asp:Label>
                                                                    <asp:Label ID="lblCessTaxRate" CssClass="hidden" runat="server" Text='<%# Eval("CessTaxRate").ToString()%>'>'></asp:Label>
                                                                    <asp:Label ID="lblCessTaxApplicable" CssClass="hidden" runat="server" Text='<%# Eval("IsCessTaxApplicable").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCessTaxCalType" CssClass="hidden" runat="server" Text='<%# Eval("CessTaxCalType").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sale Ledger">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLedgerName" runat="server" Text='<%# Eval("Ledger_Name").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblLedgerID" CssClass="hidden" runat="server" Text='<%# Eval("Ledger_ID").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- End-->
                                    </fieldset>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-6"></div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>Amount Detail (Dr)</legend>
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
                                                                    <asp:TextBox ID="txtLedgerAmt" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off" onblur="return validateAmount(this);" onkeypress="return allowNegativeNumber(event);"></asp:TextBox>
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
                                                    <!-- Ledger/Amount Detail GridView-->
                                                    <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="GridViewLedger_RowDeleting">
                                                        <Columns>
                                                            <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Style="color: red;" Text="" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblHSNCode" CssClass="hidden" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblGSTApplicable" CssClass="hidden" runat="server" Text='<%# Eval("GSTApplicable").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" onblur="return validateAmount(this);" onkeypress="return allowNegativeNumber(event);" CssClass="form-control AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>' MaxLength="12" onfocusout="return validateAmount(this);"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridViewCessLedger" runat="server" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false">
                                                        <Columns>
                                                            <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCessAmt" CssClass="hidden" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCessAmount" runat="server" onblur="return validateAmount(this);" onkeypress="return allowNegativeNumber(event);" CssClass="form-control AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>' MaxLength="12" onfocusout="return validateAmount(this);" ReadOnly="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                    <!-- End-->
                                                    <hr />
                                                    <span style="color: red">Suggested Round Off:  </span>
                                                    <asp:Label ID="lblroundsuggestion" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                                    <asp:HiddenField ID="HF_Turnover" runat="server" ClientIDMode="Static" />
                                                    &nbsp;&nbsp;|&nbsp;&nbsp;
                                                     <span style="color: red">TCS:  </span>
                                                    <asp:Label ID="lblTCSSuggestion" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                                    <asp:HiddenField ID="HF_TurnoverAmt" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="HF_BeforeRate" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="HF_AfterRate" runat="server" ClientIDMode="Static" />
                                                     <asp:HiddenField ID="HF_TCSType" runat="server" ClientIDMode="Static" />
                                                    &nbsp;&nbsp;|&nbsp;&nbsp;
                                                     <span style="color: red">TDS:  </span>
                                                    <asp:Label ID="lblTDSSuggestion" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                                    <asp:HiddenField ID="HF_TDS_Rate" runat="server" ClientIDMode="Static" />
                                                     <asp:HiddenField ID="HF_TDSType" runat="server" ClientIDMode="Static" />
                                                    <br />
                                                    <table class="table table-bordered customCSS">
                                                        <tr>
                                                            <th>GRAND TOTAL :
                                                            </th>
                                                            <th style="width: 50%; padding: 3px;">
                                                                <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0" autocomplete="off"></asp:TextBox>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <asp:Panel ID="BillByBillDetail" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewBillByBillDetail" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="RID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" OnRowDeleting="GridViewBillByBillDetail_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type of Ref" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeOfRef" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_RefType").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblRefNo" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_Ref").ToString()%>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cr/Dr" SortExpression="leftBonus" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("BillByBillTxType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Action" SortExpression="leftBonus" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="label label-danger" CommandName="Delete" CausesValidation="false">Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlChequeDetail" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewChequeDetail" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/ DD No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChequeTx_No" runat="server" Text='<%# Eval("ChequeTx_No") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/ DD Date.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChequeTx_Date" runat="server" Text='<%# Eval("ChequeTx_Date") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ChequeTx_Amount." HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChequeTx_Amount" runat="server" Text='<%# Eval("ChequeTx_Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Narration<span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtVoucherTx_Narration" runat="server" TextMode="MultiLine" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:Button runat="server" CssClass="hidden" ID="btnNarration" OnClick="btnNarration_Click" AccessKey="R" />
                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Print Receipt</label>
                                            <asp:RadioButtonList ID="rbtPrint" runat="server" CssClass="form-control" RepeatColumns="2">
                                                <asp:ListItem Value="No" Selected="True">&nbsp;No &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="Yes">&nbsp;Yes</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 21px;"  ID="btnAccept" Text="Accept" OnClick="btnAccept_Click" OnClientClick="return validateAccept();" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a href="VoucherPurchaseGoods.aspx" id="btn_Clear" runat="server" class="btn btn-default btn-block" style="margin-top: 21px;" >Clear</a>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
                <!-- Start Add BillByBillDetail Modal-->
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
                                            <asp:DropDownList runat="server" CssClass="form-control select2" Visible="false" ID="ddlBillByBillTx_Ref" onchange="ChangeRef()">
                                            </asp:DropDownList>
                                            <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Amount<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event);" MaxLength="12" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                            <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cr/Dr<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlBillByBillTx_crdr" CssClass="form-control" ClientIDMode="Static" runat="server">
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
                                        <asp:GridView ID="GridViewRef" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="RID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type of Ref" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfRef" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_RefType").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblRefNo" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_Ref").ToString()%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr/Dr" SortExpression="leftBonus" HeaderStyle-Width="10%" ItemStyle-Width="10%">
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
                                            <%--<asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSubmit" Text="Final Submit" OnClick="btnSubmit_Click" Visible="false" OnClientClick="return validateSubmit()" />--%>
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
                <!-- End-->

            </div>
            <%--Start ViewAgstRefDetail Modal--%>
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
            <!-- End-->

            <!-- Start Add ChequeDetail Modal-->
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
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeTx_Date" placeholder="DD/MM/YYYY" data-date-start-date="-89d" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtChequeTx_Date" style="color: red;"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Amount<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtChequeTx_Amount" placeholder="Enter Amount" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
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
                                        <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="RID" ShowHeaderWhenEmpty="true" ID="GVFinChequeTx" AutoGenerateColumns="false">
                                            <Columns>

                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque/ DD No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_No" runat="server" Text='<%# Eval("ChequeTx_No") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque/ DD Date.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_Date" runat="server" Text='<%# Eval("ChequeTx_Date") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChequeTx_Amount." HeaderStyle-Width="15%" ItemStyle-Width="15%">
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
            <!-- End-->

            <!-- Start View ChequeDetail Modal-->
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
            <!-- End-->
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowModal() {
            $("#ModalItemDetail").modal();
        }
        function ShowReferanceModal() {
            $("#ModalReferance").modal();
        }
        function ShowModalChequeDetail() {
            $('#ModalChequeDetail').modal('show');
        }
        function ShowModalChequeDetailView() {
            $('#ModalChequeDetailView').modal('show');
        }
        function CalculateAmount() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            // CalculateGrandTotal();
        }
        function CalculateRate() {
            debugger;
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var TotalAmount = document.getElementById('<%=txtAmount.ClientID%>').value.trim();
            //var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "") {
                document.getElementById('<%=txtQuantity.ClientID%>').value = "1";
                Quantity = "1";
            }
            if (TotalAmount == "")
                TotalAmount = "0";

            document.getElementById('<%=txtRate.ClientID%>').value = (TotalAmount / Quantity).toFixed(2);
        }

        $(document).ready(function () {
            CalculateGrandTotal();
        });

        function CalculateGrandTotal() {
            //  debugger;
            var i = 0;
            var Tval = 0;
            var TotalBeforRountoff = 0;
            var rowcount = $('#GridViewItem tr').length;
            $('#GridViewItem tr').each(function (index) {
                if (i > 0 && i < rowcount - 1) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(6).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                    TotalBeforRountoff = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
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



            $('#GridViewCessLedger tr').each(function (index) {
                if (i > 0) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)

                    TotalBeforRountoff = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                }
                i++;
            });

            document.getElementById('<%=lblGrandTotal.ClientID%>').value = Tval;
            document.getElementById('<%=lblroundsuggestion.ClientID%>').innerText = (Math.round(Tval) - Tval).toFixed(2);


            var rc = $('#GridViewLedger tr').length;
            rc = parseInt(rc) - 3;

            var kr = 0;
            kr = parseInt(0);
            $('#GridViewLedger tr').each(function (index) {

                if (kr < rc) {
                    var temp = TotalBeforRountoff;
                    var val = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;


                    TotalBeforRountoff = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                }
                kr = kr + 1;
            });

            debugger;
            var TurnoverAmt = $('#HF_TurnoverAmt').val();
            var BeforeRate = $('#HF_BeforeRate').val();
            var AfterRate = $('#HF_AfterRate').val();
            var TDS_Rate = $('#HF_TDS_Rate').val();
            var Turnover = $('#HF_Turnover').val();

            if (TurnoverAmt == "")
                TurnoverAmt = 0;
            if (BeforeRate == "")
                BeforeRate = 0;
            if (AfterRate == "")
                AfterRate = 0;
            if (TDS_Rate == "")
                TDS_Rate = 0;
            if (Turnover == "")
                Turnover = 0;


            Turnover = parseFloat(Turnover) + parseFloat(TotalBeforRountoff);
            var TCSAmt = 0;
            var TDSAmt = 0;

            if (parseFloat(TurnoverAmt) < parseFloat(Turnover)) {
                TCSAmt = ((parseFloat(TotalBeforRountoff).toFixed(3) * parseFloat(AfterRate).toFixed(3)) / 100)
            }
            else {
                TCSAmt = ((parseFloat(TotalBeforRountoff).toFixed(3) * parseFloat(BeforeRate).toFixed(3)) / 100)
            }

            TDSAmt = ((parseFloat(TotalBeforRountoff).toFixed(3) * parseFloat(TDS_Rate).toFixed(3)) / 100)

            if ($('#HF_TCSType').val() == "Yes") {
                document.getElementById('<%=lblTCSSuggestion.ClientID%>').innerText = parseFloat(TCSAmt).toFixed(2);
            }
            else {
                document.getElementById('<%=lblTCSSuggestion.ClientID%>').innerText = parseFloat(0.00).toFixed(2);
            }
            if ($('#HF_TDSType').val() == "Yes") {
                document.getElementById('<%=lblTDSSuggestion.ClientID%>').innerText = parseFloat(TDSAmt).toFixed(2);
            }
            else {
                document.getElementById('<%=lblTDSSuggestion.ClientID%>').innerText = parseFloat(0.00).toFixed(2);
            }


            // document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML = Tval;
        }

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
            if (document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount Cannot Be zero.\n";
                    $("#valtxtBillByBillTx_Amount").html("Amount Cannot Be zero.");
                }

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
                msg = msg + "Select Party Name. \n";
            }
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Name. \n";
            }
            if (document.getElementById('<%=txtQuantity.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Quantity. \n";
            }
            if (document.getElementById('<%=txtRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Rate. \n";
            }
            if (document.getElementById('<%=txtAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount. \n";
            }
            if (document.getElementById('<%=txtAmount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtAmount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";

                }
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
            if (document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtLedgerAmt").html("Amount cannot be Zero.");
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
                var amt = document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtChequeTx_Amount").html("Amount cannot be Zero.");
                }
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
            //     var VoucherGrandTotal = document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML;
            //   var RefTotal = document.getElementById('<%=lblRefTotal.ClientID%>').innerHTML;

            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher No. \n";
            }
           <%-- if (document.getElementById('<%=txtInvoice.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Supplier's Invoice No. \n";
            }
            if (document.getElementById('<%=txtSupplierInvoiceDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter  Supplier's Invoice Date. \n";
            }--%>
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date. \n";
            }

            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party A/c Name. \n";
            }
            var chk = document.getElementById('<%=chkitem.ClientID%>').checked;
            if (chk == true) {
                var rowscount = $("#<%=GridViewItem.ClientID %> tr").length;
                if (rowscount == "1") {
                    msg += "Enter Item Detail. \n";
                    $("#valGridViewItem").html("Enter Item Detail");
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

                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;

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

                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
        }
        $('.GSTNo').blur(function () {

            //var reg = /^(d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1})$/;
            //var reg = /^(\d{2})([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})(\d{1})([a-zA-Z]{1})(\d{1})$/;
            var reg = /^(\d{2})([A-Z]{5})(\d{4})([A-Z]{1})(\d{1})([Z]{1})([0-9A-Z]{1})$/;
            if (document.getElementById('txtGSTNo').value != "") {
                if (reg.test(document.getElementById('txtGSTNo').value) == false) {
                    alert("Invalid GST Number.");
                    document.getElementById('txtGSTNo').value = "";
                }
            }

        });
        function validateDecUnit(el, evt) {
            var digit = document.getElementById('<%=lblUnit.ClientID%>').innerText;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }
        function getSelectionStart(o) {
            if (o.createTextRange) {
                var r = document.selection.createRange().duplicate()
                r.moveEnd('character', o.value.length)
                if (r.text == '') return o.value.length
                return o.value.lastIndexOf(r.text)
            } else return o.selectionStart
        }
        function FillNarration() {
            debugger;
            var partyselect = document.getElementById('<%= ddlPartyName.ClientID%>');
            var PartyName = partyselect.options[partyselect.selectedIndex].text;
            var SupplierInvoice = document.getElementById('<%= txtInvoice.ClientID%>').value.trim();
            var SupplierInvoiceDate = document.getElementById('<%= txtSupplierInvoiceDate.ClientID%>').value.trim();
            if (PartyName == "Select") {
                var PartyName = "";
            }
            var Narration = PartyName;
            //var Narration = PartyName + ' ' + 'Supplier’s Invoice No - ' + SupplierInvoice + ' ' + 'Supplier’s Invoice Date -' + SupplierInvoiceDate;
            document.getElementById('<%= txtVoucherTx_Narration.ClientID%>').value = Narration;
        }
        function CompareSupplierInvocieDate() {
            var VoucherDate = document.getElementById('<%= txtVoucherTx_Date.ClientID%>').value.trim();
            if (VoucherDate != "") {
                var dateParts = VoucherDate.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var xd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {

                var xd = "";
            }
            var SupplierInvoiceDate = document.getElementById('<%= txtSupplierInvoiceDate.ClientID%>').value.trim();
            if (SupplierInvoiceDate != "") {
                var dateParts = SupplierInvoiceDate.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {

                var yd = "";
            }
            if (yd > xd) {
                alert("Supplier Invoice Date should be smaller than Voucher Date");
                document.getElementById('<%= txtSupplierInvoiceDate.ClientID%>').value = "";
            }


        }
        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }
        function validateAmount(sender) {

            var pattern = /^-?[0-9]+(.[0-9]{1,5})?$/;
            var text = sender.value;
            if (text != "") {
                if (text.match(pattern) == null) {
                    alert('the format is wrong');
                    sender.value = "0";
                    CalculateGrandTotal();
                }
                else {
                    CalculateGrandTotal();
                }
            }
            else {
                sender.value = "0";
            }


        }
        //ON Selecting AgstRef
        function ChangeRef() {
            debugger;
            var billbybillrefSelect = document.getElementById('<%=ddlBillByBillTx_Ref.ClientID%>');
            var selectedText = billbybillrefSelect.options[billbybillrefSelect.selectedIndex].text;
            var fields = selectedText.split('[');
            var value = fields[1].split(' ');
            var BillByBillAmount = value[1]
            $("#txtBillByBillTx_Amount").val(BillByBillAmount);
            <%--document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value() = BillByBillAmount;--%>
            if (value[2] == "Dr") {

                $("#ddlBillByBillTx_crdr").val("Cr");

            }
            else {

                $("#ddlBillByBillTx_crdr").val("Dr");
            }

        }
    </script>
</asp:Content>


