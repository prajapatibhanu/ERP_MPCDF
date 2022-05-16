<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="mis_Grievance_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 414px;">
        <section class="content">
            <img src="../image/download.png" style="width: 75px;" />
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Accounts info</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <fieldset>
                                <legend>Company [Office] Pages</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Grievance/ViewGrievance_Details.aspx" target="_blank">Grievance / Feedback</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Grievance/Grvinternal_List.aspx" target="_blank">Internal List </a></span>
                                    </li>
                                    <%--<li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/AccountMaster.aspx" target="_blank">Account Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/ChequeBookMaster.aspx" target="_blank">Cheque Book Master</a></span>
                                    </li>--%>

                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>Transaction Pages</legend>
                                <ul class="todo-list ui-sortable">


                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank">Create Ledger</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Contra-voucher.aspx" target="_blank">Contra voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Payment-voucher.aspx" target="_blank">Payment voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Receipt-voucher.aspx" target="_blank">Receipt voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Journal-voucher.aspx" target="_blank">Journal voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Debit-Note.aspx" target="_blank">Debit Note (Purchase Return)</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Credit-Note.aspx" target="_blank">Credit Note (Sales Return)</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/EditVoucher.aspx" target="_blank">Voucher Details</a></span>
                                    </li>
                                </ul>
                            </fieldset>




                            <fieldset>
                                <legend>Reports</legend>
                                <ul class="todo-list ui-sortable">

                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/HeadReport.aspx" target="_blank">All Heads</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/LedgerReport.aspx" target="_blank">Ledger Summary</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Profit-and-Loss-Report.aspx" target="_blank">Profit &amp; Loss</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/TrialBalance.aspx" target="_blank">Trial Balance</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Balance-Sheet-Report.aspx" target="_blank">Balance Sheet</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/GST1.aspx" target="_blank">GST 1</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/GST3.aspx" target="_blank">GST 3</a></span>
                                    </li>
                                </ul>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Inventory Info</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->





                            <fieldset>
                                <legend>Inventory vouchers</legend>


                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/PurchaseMaster.aspx" target="_blank">Purchase Voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/CreditSale.aspx" target="_blank">Credit Sale Voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/CashSale.aspx" target="_blank">Cash Sale Voucher</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Transfer Purchase voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Transfer Sale voucher</a></span>
                                    </li>


                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>Inward/Outward Summary</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Inward Summary</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Outward Summary</a></span>
                                    </li>

                                    <li>

                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Opening_Balance.aspx" target="_blank">Opening Balance for Inventory</a></span>
                                    </li>
                                </ul>
                            </fieldset>
                        </div>
                    </div>
                </div>

            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

