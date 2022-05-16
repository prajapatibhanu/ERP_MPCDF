<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ViewPendingGrievance_Details.aspx.cs" Inherits="mis_Grievance_ViewGrievance_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Grievance/Feedback List</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                            <div class="box-body">
                                <asp:GridView ID="GridView1" DataKeyNames="GrvApplication_ID" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grievance Status">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("App_GrvStatus").ToString()%>' runat="server" ID="App_GrvStatus"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Application_RefNo" HeaderText="Reference No" />
                                        <asp:BoundField DataField="App_Name" HeaderText="Applicant Name" />
                                        <asp:BoundField DataField="Application_Subject" HeaderText="Subject" />
                                        <asp:BoundField DataField="Application_UpdatedOn" HeaderText="Apply Date" />
                                        <%--<asp:TemplateField HeaderText="View More Detail">
                                            <ItemTemplate>
                                               <asp:LinkButton ID="LinkButton1" CssClass="label label-default" runat="server" CommandName="select">View More</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

