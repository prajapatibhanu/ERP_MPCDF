<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WarehouseAuditHistory.aspx.cs" Inherits="mis_Warehouse_WarehouseAuditHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function ShowModel() {
            $('#myModal').modal('show');
            return false;
        }

        function HideModel() {
            $('#myModal').modal('hide');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="row">
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box-header with-border">

                            <h3 class="box-title" id="Label2">Warehouse Audit History</h3>
                            <br />
                            <br />
                            <asp:Button ID="btnback" CausesValidation="false" ValidationGroup="back" runat="server" Text="<< Back" CssClass="btn btn-default btn-sm" OnClick="btnback_Click" />

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblError" runat="server"></asp:Label>
                                    <hr />
                                </div>
                            </div>
                            <div id="DivAuditHistory" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="GVDivAuditProcess" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="Audit history not found!" EmptyDataRowStyle-ForeColor="Red" class="table table-striped no-border no-margin pagination-ys set-table-border" AutoGenerateColumns="False" DataKeyNames="AuditDate" runat="server" OnSelectedIndexChanged="GVDivAuditProcess_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AuditDate" HeaderText="Audit Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Emp_Name" HeaderText="Audit By" />
                                            <asp:CommandField ButtonType="Link" ControlStyle-CssClass="label label-default" ShowSelectButton="true" HeaderText="View More" SelectText="View More" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Warehouse Audit History</h4>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive">
                            <asp:GridView ID="GVAuditHistory" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" class="table table-striped no-border no-margin pagination-ys set-table-border" AutoGenerateColumns="False" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemName" HeaderText="ITEM NAME" />
                                    <asp:BoundField DataField="Cr" HeaderText="QUANTITY" DataFormatString="{0:00}" />
                                    <asp:BoundField DataField="AuditQuantity" HeaderText="AUDIT QUANTITY" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

