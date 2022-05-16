<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkorProductDemand_DetailUnderOffice.aspx.cs" Inherits="mis_Dashboard_MilkorProductDemand_DetailUnderOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">File Tracking</h3>
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">

                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                        <fieldset>
                            <legend>Milk Demand for:
                                    <asp:Label ID="lblmilksearchdate" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                            </legend>
                            <asp:GridView ID="grdMilk" runat="server" class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdMilk_PageIndexChanging" AllowPaging="true" PageSize="100">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Delivary_Date" HeaderText="Delivary Date" />
                                    <asp:BoundField DataField="OrderId" HeaderText="Order NO" />
                                    <asp:BoundField DataField="DName" HeaderText="Distributor" />
                                    <asp:BoundField DataField="RouteName" HeaderText="Route" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                    <asp:BoundField DataField="SupplyTotalQty" HeaderText="Qty" />
                                    <asp:BoundField DataField="SupplyTotalQtyLtr" HeaderText="Qty(In Ltr)" />
                                </Columns>
                            </asp:GridView>
                        </fieldset>
                        <fieldset>
                            <legend>Product Demand for:
                                    <asp:Label ID="lblProductsearchdate" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                            </legend>
                            <asp:GridView ID="grdProduct" runat="server" class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdProduct_PageIndexChanging" AllowPaging="true" PageSize="100">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Delivary_Date" HeaderText="Delivary Date" />
                                    <asp:BoundField DataField="OrderId" HeaderText="Order NO" />
                                    <asp:BoundField DataField="DName" HeaderText="Distributor" />
                                    <asp:BoundField DataField="RouteName" HeaderText="Route" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                    <asp:BoundField DataField="TotalQty" HeaderText="Qty(Demand)" />
                                    <asp:BoundField DataField="SupplyTotalQty" HeaderText="Qty(Supply)" />
                                </Columns>
                            </asp:GridView>
                        </fieldset>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

