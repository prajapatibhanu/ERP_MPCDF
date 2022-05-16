<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UMEmpRoleMap.aspx.cs" Inherits="mis_Admin_UMEmpRoleMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">User Role Mapping</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>User Type</label>
                                <asp:DropDownList ID="ddlUserType" runat="server" OnInit="ddlUserType_Init" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label><asp:Label ID="lblUserName" runat="server" Text="Employee"></asp:Label> Name</label>
                                <asp:DropDownList ID="ddlEmployye_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployye_Name_SelectedIndexChanged" >
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div id="divGrid" runat="server">
                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Role_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Role_Name" HeaderText="Roles" />
                                <asp:BoundField DataField="Role_ID" HeaderText="Role_ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField ItemStyle-Width="30" HeaderText="Exist Roles">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("Role_ID").ToString()%>' Checked='<%# Eval("ExistID").ToString() != "" ? true : false %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

