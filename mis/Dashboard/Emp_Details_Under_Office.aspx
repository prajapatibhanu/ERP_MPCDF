<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Emp_Details_Under_Office.aspx.cs" Inherits="mis_Dashboard_Emp_Details_Under_Office" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Employee's Information</h3>
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <fieldset>
                        <legend>Employee Details</legend>

                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" Text="" Style="font-size: 17px;"></asp:Label>
                            <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true" PageSize="70"  class="datatable table table-striped table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UserName" HeaderText="Emp Code" />
                                    <asp:BoundField DataField="Emp_Name" HeaderText="Emp Name" />
                                    <asp:BoundField DataField="Emp_Gender" HeaderText="Gender" />
                                    <asp:BoundField DataField="Emp_Dob" HeaderText="DOB" />
                                    <asp:BoundField DataField="Emp_MaritalStatus" HeaderText="Marital Status" />
                                    <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

