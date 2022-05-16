<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_VariantWiseData.aspx.cs" Inherits="mis_dailyplan_Rpt_VariantWiseData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Milk Sheet Varient Wise Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlPSection" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2 no-print">
                            <div class="form-group">
                                <label>Item Type<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlItemType" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2 no-print">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Select From Date!'></i>" ErrorMessage="Select From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtFromDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 no-print">
                            <div class="form-group">
                                <label>To Date<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Select To Date!'></i>" ErrorMessage="Select To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtToDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 no-print">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Style="margin-top: 19px;" CssClass="btn btn-success" ValidationGroup="Submit" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default no-print" Text="Print" OnClientClick="window.print();" />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-info no-print" Text="Export to Excel" OnClick="btnExcel_Click" />
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvDetail" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="QtyInKg" HeaderText="QTY (In Kg)" />
                                        <asp:BoundField DataField="QtyInLtr" HeaderText="QTY (In Ltr)" />
                                        <asp:BoundField DataField="Fat_Per" HeaderText="FAT Per" />
                                        <asp:BoundField DataField="Snf_Per" HeaderText="SNF Per" />
                                        <asp:BoundField DataField="KgFat" HeaderText="Fat (In Kg)" />
                                        <asp:BoundField DataField="KgSnf" HeaderText="SNF (In Kg)" />
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
</asp:Content>

