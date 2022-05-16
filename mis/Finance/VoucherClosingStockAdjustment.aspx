<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherClosingStockAdjustment.aspx.cs" Inherits="mis_Finance_VoucherClosingStockAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
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
                    <div class="box box-success" style="background-color: #fffed9">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-4">
                                    <h3 class="box-title">Closing Stock Adjustment</h3>

                                </div>
                                <div class="col-md-8">
                                    <a target="_blank" href="LedgerMasterB.aspx" class="btn btn-primary pull-right">Add Ledger</a>
                                    <asp:LinkButton ID="btnRefreshLedgerList" class="btn btn-primary pull-right Aselect1" Style="margin-right: 10px;" runat="server" OnClick="btnRefreshLedgerList_Click">Refresh Ledger</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">

                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" data-date-end-date="0d" placeholder="DD/MM/YYYY" MaxLength="50" autocomplete="off" AutoPostBack="true" OnTextChanged="txtVoucherTx_Date_TextChanged"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Particulars<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlLedger_ID" CssClass="form-control select1 select2" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddlLedger_ID_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Current Balance<span style="color: red;"> *</span></label>
                                        <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Cr/Dr<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlcreditdebit" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="Dr">Debit</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Amount</label><span style="color: red;"> *</span>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtLedgerTx_Amount" onblur="return validateAmount(this);" placeholder="Enter Amount..." MaxLength="12" ClientIDMode="Static" onkeypress="return validateDec(this,event);" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" ID="btnAddLedger" CssClass="btn btn-block btn-success" Text="Save" OnClick="btnAddLedger_Click" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="VoucherClosingStockAdjustment.aspx" id="btnClear" runat="server" class="btn btn-block btn-default" style="margin-top: 21px;">Clear</a>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" DataKeyNames="LedgerTx_ID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VoucherTx_Date" HeaderText="Voucher Date" />
                                            <asp:BoundField DataField="Ledger_Name" HeaderText="Ledger Name" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                           
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
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
        </section>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>


