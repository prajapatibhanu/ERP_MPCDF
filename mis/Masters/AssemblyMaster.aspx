<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AssemblyMaster.aspx.cs" Inherits="mis_Masters_AssemblyMaster" %>

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
    <div class="col-md-12">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="row">
            <div class="col-md-6">
                <div class="card card-primary">
                    <div class="card-header">
                        <h3 class="card-title">Assembly Master Entry</h3>
                    </div>
                    <!-- /.card-header -->
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Assembly<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAssembly" ErrorMessage="Enter Assembly Name." Text="<i class='fa fa-exclamation-circle' title='Enter Assembly Name !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAssembly"
                                            ValidationExpression="^[a-zA-z\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtAssembly" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Active Status<span style="color: red;"> *</span></label><br />
                                    <asp:CheckBox runat="server" ID="chkIsActive" Checked="true" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnSave" ValidationGroup="a" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                
                    <div class="card card-primary">
                        <div class="card-header">
                            <h3 class="card-title">Assembly Details</h3>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <div>
                                    <asp:GridView ID="GridDetails" runat="server" class="table table-bordered datatable" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Assembly_Id" OnRowCommand="GridDetails_RowCommand" OnRowDataBound="GridDetails_RowDataBound" OnPageIndexChanging="GridDetails_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Assembly_Name" HeaderText="Assembly Name" />
                                            <asp:BoundField DataField="Assembly_IsActive" HeaderText="Active Status" />
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="button button-mini button-green" CommandName="EditRequest" CommandArgument='<%# Bind("Assembly_Id") %>'><i class="nav-icon fas fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No Assembly Found</EmptyDataTemplate>
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

