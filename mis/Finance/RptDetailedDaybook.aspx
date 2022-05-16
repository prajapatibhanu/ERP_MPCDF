<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDetailedDaybook.aspx.cs" Inherits="mis_Finance_RptDetailedDaybook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {
            .Hiderow, .main-footer {
                display: none;
            }

            .box {
                border: none;
            }

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            .lblheadingFirst p {
                text-align: center !important;
                font-size: 10px !important;
            }
        }

        .align-right {
            text-align: right !important;
        }

        .UnderLine {
            style ="text-decoration:underline";
        }

        .item {
            background-color: wheat;
        }

        .billbybill {
            background-color: gray;
            color: white;
        }

        .ledger {
            background-color: #e6e6e6;
        }

        .narration {
            background-color: #ffffe6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Detailed Day Book</h3>
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
                    <p class="hide_print">
                        <span runat="server" id="spnAltW">[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>
                    </p>

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>
                <div class="box-body">
                    <div class="row Hiderow">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row pull-right Hiderow">
                        <div class="col-md-12">
                            <div class="form-group" style="margin-top: 28px;">
                                <label>Filter: &nbsp;&nbsp;</label>
                                <asp:CheckBox ID="ChkLedger" ClientIDMode="Static" runat="server" Checked="true" Text="Opposite Ledger.&nbsp;&nbsp;" onchange="HideOppositeLedger();" />
                                <asp:CheckBox ID="chkBillByBill" ClientIDMode="Static" runat="server" Checked="true" Text="BillWiseDetail.&nbsp;&nbsp;" onchange="HideBillByBill();" />
                                <asp:CheckBox ID="chkItemDetail" ClientIDMode="Static" runat="server" Checked="true" Text="Inventory.&nbsp;" onchange="HideItem();" />
                                <asp:CheckBox ID="chkNarration" ClientIDMode="Static" runat="server" Checked="true" Text="Narration.&nbsp;&nbsp;" onchange="HideNarration();" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:Button ID="btnExportExcel" CssClass="Hiderow" runat="server" Text="Export Excel" OnClick="btnExportExcel_Click" />
                                  <asp:TextBox ID="txtSearch" placeholder="Search" onkeyup="Search_Gridview(this, 'GridView1')" runat="server" />
                                <hr />
								<asp:GridView ID="GridView1" ClientIDMode="Static"  DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date." ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                <%-- <div id="div" runat="server">                                              
                                            </div>--%>
                                                <asp:Panel ID="pnlLedger" runat="server" Style="display: block" Width="100%">
                                                    <span class="HideRecord">(As Per Details)</span>
                                                    <asp:GridView ID="GVLBillbyBill" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid billbybill BillByBill HideRecord" GridLines="None" ShowHeader="false" Width="80%">
                                                        <Columns>
                                                            <asp:BoundField DataField="BillByBillTx_RefType" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="BillByBillTx_Ref" ItemStyle-Width="20%" ItemStyle-Font-Size="X-Small" />
                                                            <asp:BoundField DataField="BillByBillTx_Date" ItemStyle-Width="10%" ItemStyle-Font-Size="X-Small" />
                                                            <asp:TemplateField ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("BillByBillTx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Ledger ledger HideRecord" GridLines="None" ShowHeader="false" OnRowDataBound="gvLedger_RowDataBound" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="90%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLName" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                                    <asp:Label ID="lblBVID" CssClass="hidden" Text='<%# Eval("VoucherTx_ID").ToString() %>' runat="server" />
                                                                    <asp:Label ID="lblBLID" CssClass="hidden" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                                                    <asp:Panel ID="pnlBillByBill" runat="server" Style="display: block">
                                                                        <asp:GridView ID="GVMLBillbyBill" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid billbybill BillByBill HideRecord" GridLines="None" ShowHeader="false" Width="80%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="BillByBillTx_RefType" ItemStyle-Width="10%" />
                                                                                <asp:BoundField DataField="BillByBillTx_Ref" ItemStyle-Width="20%" ItemStyle-Font-Size="X-Small" />
                                                                                <asp:BoundField DataField="BillByBillTx_Date" ItemStyle-Width="10%" ItemStyle-Font-Size="X-Small" />
                                                                                <asp:TemplateField ItemStyle-Width="30%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl" runat="server" Text='<%#Eval("BillByBillTx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Tx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GvItem" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Item item HideRecord" GridLines="None" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="ItemName" ItemStyle-Width="50%" />
                                                            <asp:BoundField DataField="Quantity" ItemStyle-Width="20%" />
                                                            <asp:TemplateField ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Rate")+ " /" + Eval("UQCCode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Amount" ItemStyle-Width="20%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvSubLedger" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Ledger HideRecord" GridLines="None" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="Ledger_Name" ItemStyle-Font-Bold="true" ItemStyle-Width="50%" />
                                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Tx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblNarration" class="Narration narration HideRecord" Text="" runat="server" />
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Name." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" HeaderStyle-CssClass="Hiderow" ItemStyle-CssClass="Hiderow">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" Visible='<%# Eval("Status").ToString() == "0" ? true :false  %>' OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                               <asp:LinkButton ID="bankreceiptprint" CssClass="label label-primary" runat="server" Text="Receiving" CommandName="ReceiptPrint" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                 <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
												 <asp:Label ID="lblV_Editright" CssClass="hidden" Text='<%# Eval("V_Editright").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="." ItemStyle-Width="12%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVID" Text='<%# Eval("VoucherTx_ID").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="." ItemStyle-Width="12%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLID" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblLedgerTx_ID" Text='<%# Eval("LedgerTx_ID").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

	<script type="text/javascript">
        function Search_Gridview(strKey, strGV) {
            debugger;
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    <script>
        $(window).on('load', function () {
            $(".HideRecord").css("display", "none");

        })
        function handleKeyDown(e) {
            var ctrlPressed = 0;
            var altPressed = 0;
            var shiftPressed = 0;
            var evt = (e == null ? event : e);

            shiftPressed = evt.shiftKey;
            altPressed = evt.altKey;
            ctrlPressed = evt.ctrlKey;
            self.status = ""
               + "shiftKey=" + shiftPressed
               + ", altKey=" + altPressed
               + ", ctrlKey=" + ctrlPressed

            if ((altPressed) && (evt.keyCode == 87)) {
                if ($('.HideRecord').is(':visible')) {
                    $(".HideRecord").css("display", "none");
                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "block");
                    document.getElementById('<%= chkBillByBill.ClientID%>').checked = true;
                    document.getElementById('<%= chkItemDetail.ClientID%>').checked = true;
                    document.getElementById('<%= chkNarration.ClientID%>').checked = true;
                    document.getElementById('<%= ChkLedger.ClientID%>').checked = true;

                    //$("p.subledger").css("border-top", "1px solid #ccc");
                }
            }
            //alert("You pressed the " + fromKeyCode(evt.keyCode)
            // + " key (keyCode " + evt.keyCode + ")\n"
            // + "together with the following keys:\n"
            // + (shiftPressed ? "Shift " : "")
            // + (altPressed ? "Alt " : "")
            // + (ctrlPressed ? "Ctrl " : "")
            //)            

            return true;
        }

        document.onkeydown = handleKeyDown;
        function HideBillByBill() {
            debugger;
            if (document.getElementById('<%= chkBillByBill.ClientID%>').checked) {


                $(".BillByBill").css("display", "block");
            }
            else {
                $(".BillByBill").css("display", "none");
            }
        }
        function HideItem() {
            debugger;
            if (document.getElementById('<%= chkItemDetail.ClientID%>').checked) {


                $(".Item").css("display", "block");
            }
            else {
                $(".Item").css("display", "none");
            }
        }
        function HideNarration() {
            debugger;
            if (document.getElementById('<%= chkNarration.ClientID%>').checked) {


                $(".Narration").css("display", "block");
            }
            else {
                $(".Narration").css("display", "none");
            }
        } HideOppositeLedger
        function HideOppositeLedger() {
            debugger;
            if (document.getElementById('<%= ChkLedger.ClientID%>').checked) {


                $(".Ledger").css("display", "block");
            }
            else {
                $(".Ledger").css("display", "none");
            }
        }
    </script>
</asp:Content>







