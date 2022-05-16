<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollGenerate_LPCertificate.aspx.cs" Inherits="mis_Payroll_PayrollGenerate_LPCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            .pagebreak {
                page-break-before: always;
            }
        }

        .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }

        .thead {
            display: table-header-group;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Generate Last Pay Certificate </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office <span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Employee Name<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Order Details</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Designation<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtDesignation" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Level<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtLevel" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Order No.<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtOrderNo" autocomplete="off" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Order Date<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtOrderDate" runat="server" autocomplete="off" class="form-control DateAdd" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtTransferOffice" runat="server" class="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Proceeding To<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtProceedingTo" runat="server" class="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Relieving Order No.<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtReliveingOrderNo" runat="server" class="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Relieving Date<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtRelievingDate" runat="server" autocomplete="off" placeholder="Select Date..." class="form-control DateAdd" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Salary Paid Upto<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtSalaryPaidUpto" runat="server" autocomplete="off" placeholder="Select Date..." class="form-control DateAdd"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <asp:TextBox ID="txtCurrentDate" autocomplete="off" runat="server" placeholder="Select Date..." class="form-control DateAdd"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Salary Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Earning Head<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtEarningHead" autocomplete="off" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Earning Amount<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtEarningAmount" autocomplete="off" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Deduction Head<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtDeductionHead" autocomplete="off" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Deduction Amount<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtDeductionAmount" autocomplete="off" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-1" style="margin-top:20px;">
                                        <div class="form-group">
                                            <label></label>
                                          <asp:LinkButton ID="btnAdd" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" runat="server"></asp:LinkButton>
                                        </div>
                                    </div>
                                        </div>
                                     <div class="col-md-12">
                                         <div class="table-responsive">
                                              <asp:GridView ID="GridView1"  EmptyDataText="No Record Found." runat="server" 
                                                  class="table table-hover table-bordered pagination-ys" OnRowCommand="GridView1_RowCommand"
                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" >
                                <Columns>

                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Earning">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEarningHead" runat="server" Text='<%# Eval("EarningHead") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEarningAmount" runat="server" Text='<%# Eval("EarningAmount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduction">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeductionHead" runat="server" Text='<%# Eval("DeductionHead") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeductionAmount" runat="server" Text='<%# Eval("DeductionAmount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                       <asp:LinkButton ID="lnkDelete" CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                         </div>
                                         </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>

                    <div class="row">
                            <fieldset>
                                <legend>Details</legend>
                                <div class="row">
                                 
                                    <div class="col-md-12">
                                        <div class="form-group">
                                          
                                          1  &nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                         
                                           2 &nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                  
                                  <div class="col-md-12">
                                        <div class="form-group">
                                          
                                          3 &nbsp;&nbsp; <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-12">
                                        <div class="form-group">
                                         
                                          4 &nbsp;&nbsp; <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-12">
                                        <div class="form-group">
                                          
                                           5&nbsp;&nbsp; <asp:TextBox ID="TextBox5" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                         <div class="col-md-12">
                                        <div class="form-group">
                                          
                                         6 &nbsp;&nbsp;  <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-12">
                                        <div class="form-group">
                                         
                                         7 &nbsp;&nbsp;   <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     
                                </div>
                            </fieldset>
                      
                    </div>

                    <div class="row">
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" CssClass="btn btn-primary" Text="Print" />
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
       <div id="Print" runat="server" class="NonPrintable"></div>
   </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

