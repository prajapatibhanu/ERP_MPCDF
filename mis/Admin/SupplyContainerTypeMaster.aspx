<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SupplyContainerTypeMaster.aspx.cs" Inherits="mis_Admin_SupplyContainerTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Supply Container Type Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Container type<span style="color: red;"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlState_Name" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static">
                                    <asp:ListItem>Select</asp:ListItem>
                                     <asp:ListItem>Crate</asp:ListItem>
                                     <asp:ListItem>Carton</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Container code<span style="color: red;"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlDivision_Name" CssClass="form-control" AutoPostBack="true" ClientIDMode="Static">
                                    <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>CR01</asp:ListItem>
                                        <asp:ListItem>CT01</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>Container Capacity<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtDistrict_Name" runat="server" placeholder="Enter Container Capacity.." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                           <div class="col-md-4">
                            <div class="form-group">
                                <label>Item Name<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Item Name.." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                            </div>
                        </div>
                       <div class="col-md-4">
                            <div class="form-group">
                                <label>Item Packet Size<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Item Packet Size.." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                            </div>
                        </div>
                 <div class="col-md-4">
                            <div class="form-group">
                                <label>Item Qty<span style="color: red;">*</span></label>
                                <asp:TextBox ID="TextBox3" runat="server" placeholder="Enter Item Qty.." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-primary" runat="server" Text="Save" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="AdminDistrict.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" PageSize="150" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="District_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("District_ID").ToString()%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="State_Name" HeaderText="State Name" />
                                    <asp:BoundField DataField="Division_Name" HeaderText="Division Name" />
                                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("District_ID").ToString()%>' Checked='<%# Eval("District_IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

