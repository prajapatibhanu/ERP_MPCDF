<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WarehouseDetail.aspx.cs" Inherits="mis_Warehouse_WarehouseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">

                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Warehouse Stock as on date</h3>
                            <br />
                            <br />
                            <asp:Button ID="btnback" CausesValidation="false" ValidationGroup="back" runat="server" Text="<< Back" CssClass="btn btn-default btn-sm" OnClick="btnback_Click" /><br />
                        </div>
                        <!-- /.box-header -->

                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblError" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVWarehouseItemDetail" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="Item Not Found!" EmptyDataRowStyle-ForeColor="Red" class="table table-striped no-border no-margin pagination-ys set-table-border" AutoGenerateColumns="False" DataKeyNames="Item_id" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Item_id" Visible="false" HeaderText="Stock Id" />
                                                <asp:BoundField DataField="ItemCatName" HeaderText="Item Group (वस्तु का समूह)" />
                                                <asp:BoundField DataField="ItemTypeName" HeaderText="Item Category (वस्तु की श्रेणी)" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                <asp:BoundField DataField="UnitName" Visible="false" HeaderText="Unit Type" />
                                                <asp:BoundField DataField="Cr" DataFormatString="{0:00}" HeaderText="Item Quantity (वस्तु की मात्रा)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $("#txtAuditDate").datepicker({
            format: 'dd/mm/yyyy',
            endDate: '0d',
            autoclose: true
        });
    </script>
</asp:Content>

