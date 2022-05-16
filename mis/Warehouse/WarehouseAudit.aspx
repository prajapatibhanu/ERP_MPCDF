<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WarehouseAudit.aspx.cs" Inherits="mis_Warehouse_WarehouseAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <style type="text/css">
        .hideGridColumn {
            display: none;
        }
    </style>
    <div class="content-wrapper">

        
        <section class="content">
            <div class="box box-success">
                <div class="box-header with-border">
                    <h3 class="box-title" id="Label1">Warehouse Audit / गोदाम का ऑडिट</h3>
                    <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnback" CausesValidation="false" ValidationGroup="back" runat="server" Text="<< Back" CssClass="btn btn-default btn-sm" OnClick="btnback_Click" />
                            <br />
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div id="DvAuditDate" runat="server" class="col-md-3">
                            <div class="form-group">
                                <label>Audit Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtAuditDate" ValidationGroup="date" CausesValidation="true" AutoPostBack="true" date-provide="datepicker" runat="server" placeholder="DD/MM/YYYY" CssClass="form-control" data-date-end-date="0d" ClientIDMode="Static" autocomplete="off" OnTextChanged="txtAuditDate_TextChanged" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                                <asp:RegularExpressionValidator runat="server" ValidationGroup="date" ControlToValidate="txtAuditDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." SetFocusOnError="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:GridView ID="GVDivAuditProcess" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="Item Not Found!" EmptyDataRowStyle-ForeColor="Red" class="table table-striped no-border no-margin pagination-ys set-table-border" AutoGenerateColumns="False" DataKeyNames="Item_id" runat="server" OnLoad="GVDivAuditProcess_Load">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Item_id" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" HeaderText="Stock Id" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="Cr" HeaderText="Quantity" />
                                    <%--<asp:BoundField DataField="RemainAuditQty" HeaderText="Remain Audit Quantity" />--%>
                                    <asp:TemplateField HeaderText="Audit Quantity<span style='color: red'>*</span>">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAuditQty" AutoPostBack="true" runat="server" Style="width: 90%;" CssClass="form-control" onkeypress="return validateDec(this,event);" OnTextChanged="txtAuditQty_TextChanged" MaxLength="10"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" Enabled="False" ValidationGroup="date" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--Confirmation Modal Start --%>
                    <div id="myModelNew" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-sm" role="document">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 10px 15px 5px;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title">Confirmation Box</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p>Are you sure you want to Update this record?</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer" style="padding: 0px; text-align: left; padding-bottom: 10px;">
                                    <div class="col-md-12">
                                        <asp:Button runat="server" CssClass="btn action-button" Text="Yes" ID="BtnSubmit" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />&nbsp;&nbsp;<asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn action-button" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--ConfirmationModal End --%>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        $("#txtAuditDate").datepicker({
            format: 'dd/mm/yyyy',
            endDate: '0d',
            autoclose: true
        });

        function ShowModel() {
            $('#myModelNew').modal('show');
            return false;
        }

        function HideModel() {
            $('#myModelNew').modal('hide');
            return false;
        }

    </script>
</asp:Content>

