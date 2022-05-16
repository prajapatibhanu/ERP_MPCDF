﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollTotalSalaryandGenrateSalary.aspx.cs" Inherits="mis_Payroll_PayrollTotalSalaryandGenrateSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employees Salary Generated List</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                        <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                        <asp:TemplateField HeaderText="NET SALARY">
                                            <ItemTemplate>
                                                <asp:Label id="lblnetsalary" runat="server" Text='<%# Eval("Salary_NetSalary").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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

