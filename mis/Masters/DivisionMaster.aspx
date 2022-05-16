<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DivisionMaster.aspx.cs" Inherits="mis_Masters_DivisionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script src="../../js/datatables.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datatable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card-header">
                    <h3 class="card-title">Division Master Entry</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Division<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtdivision" ErrorMessage="Enter Division Name." Text="<i class='fa fa-exclamation-circle' title='Enter Division Name !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtdivision"
                                        ValidationExpression="^[a-zA-z\s]+$"
                                        Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                </span>
                                <asp:TextBox ID="txtdivision" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Active Status<span style="color: red;"> *</span></label><br />
                                <asp:CheckBox runat="server" ID="chkIsActive" Checked="true" />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" ValidationGroup="a" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="container-fluid">
                            <div class="box box-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Division Details</h3>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridDetails" runat="server" class="table table-bordered datatable" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Division_Id" OnRowDataBound="GridDetails_RowDataBound" OnRowCommand="GridDetails_RowCommand" OnPageIndexChanging="GridDetails_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Division_Name" HeaderText="Division Name" />
                                                <asp:BoundField DataField="Division_IsActive" HeaderText="Active Status" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="fa fa-edit" CommandName="EditRequest" CommandArgument='<%# Bind("Division_Id") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>No Division Found</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

