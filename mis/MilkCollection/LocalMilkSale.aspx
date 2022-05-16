<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LocalMilkSale.aspx.cs" Inherits="mis_MilkCollection_LocalMilkSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Local Milk Sale</h3>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" autocomplete="off" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" CssClass="form-control DateAdd" placeholder="Select Date" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvShift" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlShift" CssClass="form-control" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" AutoPostBack="true" CausesValidation="true" ValidationGroup="Test" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Remainig Quantity<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRemainingQty" runat="server" Display="Dynamic" ControlToValidate="txtMilkQty" Text="<i class='fa fa-exclamation-circle' title='Enter Code!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtRemainingQty" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Milk Qty. (In Ltr)<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvMilkQty" runat="server" Display="Dynamic" ControlToValidate="txtMilkQty" Text="<i class='fa fa-exclamation-circle' title='Enter Code!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtMilkQty" CssClass="form-control" MaxLength="13" placeholder="Enter Quantity" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Rate<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RFVtxtRate" runat="server" Display="Dynamic" ControlToValidate="txtRate" Text="<i class='fa fa-exclamation-circle' title='Enter Code!'></i>" ErrorMessage="Enter Rate" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtRate" CssClass="form-control" MaxLength="13" Text="100.00" placeholder="Enter Rate" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Style="margin-top:23px;" CssClass="btn btn-success btn-block" Text="Save" ValidationGroup="Save" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <a href="LocalMilkSale.aspx" Style="margin-top:23px;" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="box-title">Remaining Milk Detail</h4>
                                    <div class="form-group table-responsive">
                                        <asp:GridView ID="gvRemainingMilkDetail" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvV_Code" runat="server" Text='<%# Eval("DT_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shift" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvV_Shift" runat="server" Text='<%# Eval("V_Shift") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Quantity" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvI_TotalQty" runat="server" Text='<%# Eval("D_TotalMilk") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sale Quantity" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvI_SaleQty" runat="server" Text='<%# Eval("D_SaleMilkQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remaining Quantity" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvI_RemainingQty" runat="server" Text='<%# Eval("D_RemainingMilkQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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

