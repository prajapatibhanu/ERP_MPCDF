<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_FinanceRightsToEmployee.aspx.cs" Inherits="mis_Finance_Mst_FinanceRightsToEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employee Rights for Finance</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Emp Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlEmpName" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" style="margin-top:20px;" Text="Save" OnClick="btnSave_Click"  CssClass="btn btn-primary"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNo" Text='<%# Container.DataItemIndex +1 %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Office Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblOffice" Text='<%# Eval("Office_Name")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployee" Text='<%# Eval("Emp_Name")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActive" Checked='<%# Eval("IsActive").ToString()=="1"?true:false %>' ToolTip='<%# Eval("FinanceEditRights_ID")%>' runat="server" OnCheckedChanged="chkActive_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

