<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HeadWiseEarningDeductionsReport.aspx.cs" Inherits="mis_MilkCollection_HeadWiseEarningDeductionsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Head Wise Additions/Deductions Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Head Type" Text="<i class='fa fa-exclamation-circle' title='Select Head Type !'></i>"
                                                    ControlToValidate="ddlHeadType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlHeadType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                                <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                    ControlToValidate="ddlHeaddetails" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlHeaddetails" OnInit="ddlHeaddetails_Init" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtTdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                                            </div>
                                        </div>
                                    </div>
                            </div>
                            </fieldset>
                            
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Report</legend>
                                <div class="row">
                                    <div class="col-md-12 pull-right noprint">
                                <div class="form-group">
                                    <asp:Button ID="btnExport" runat="server"  Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                    
                                </div>
                                        </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="true" ShowFooter="True" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Record Found">
                                                <Columns>
                                                     <asp:BoundField HeaderText="Billing Date" DataField="BillingDate" />
                                                     <asp:TemplateField HeaderText="Society">
                                                         
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Head Type" DataField="HeadType" />
                                                    <asp:BoundField HeaderText="Head Name" DataField="HeadName" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

