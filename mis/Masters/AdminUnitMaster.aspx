<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminUnitMaster.aspx.cs" Inherits="mis_Admin_AdminUnit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css" />--%>
    <style>
        .capitalize {
            text-transform: capitalize;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-md-12">
        <div class="card card-primary">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="card-header">
                <h3 class="card-title">Unit Master</h3>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Unit<span class="text-danger"> *</span></label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtUnit" ErrorMessage="Enter Unit." Text="<i class='fa fa-exclamation-circle' title='Enter Unit !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtUnit"
                                            ValidationExpression="^[a-zA-z.\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtUnit" runat="server" placeholder="Enter Unit" class="form-control capitalize" MaxLength="20" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                    <small><span id="valtxtUnit" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Quantity Type<span class="text-danger"> *</span></label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlQuantity_Type" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlQuantity_Type" InitialValue="0" ErrorMessage="Select Quantity_Type." Text="<i class='fa fa-exclamation-circle' title='Please Select Quantity_Type !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList runat="server" ID="ddlQuantity_Type" CssClass="form-control" AutoPostBack="false">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Measure</asp:ListItem>
                                        <asp:ListItem Value="2">Volume</asp:ListItem>
                                        <asp:ListItem Value="3">Weight</asp:ListItem>
                                        <asp:ListItem Value="4">Length</asp:ListItem>
                                        <asp:ListItem Value="5">Area</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlQuantity_Type" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>UQC Code<span class="text-danger"> *</span></label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtUqc_Code" ErrorMessage="Enter Uqc_Code." Text="<i class='fa fa-exclamation-circle' title='Enter Uqc_Code !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtUqc_Code"
                                            ValidationExpression="^[a-zA-z.\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtUqc_Code" runat="server" placeholder="Enter QUC Code..." class="form-control" MaxLength="20" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                    <small><span id="valtxtUqc_Code" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Number of Decimal Places  (0 - 4)<span class="text-danger"> *</span></label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtNoOfDecimal" ErrorMessage="Enter Number of Decimal Places." Text="<i class='fa fa-exclamation-circle' title='Enter Number of Decimal Places !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtNoOfDecimal"
                                            ValidationExpression="[0-9]{1}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtNoOfDecimal" runat="server" placeholder="Enter Number of Decimal Places..." class="form-control" MaxLength="1" onkeypress="return validateNum(event);"></asp:TextBox>
                                    <small><span id="valtxtNoOfDecimal" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" ValidationGroup="a" CssClass="btn btn-block btn-success" runat="server" Text="Submit"  OnClick="btnSave_Click" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <a class="btn btn-block btn-default" href="AdminUnitMaster.aspx">Clear</a>
                                </div>
                            </div>
                            <div class="col-md-8"></div>
                        </div>
                   
                </div>
                <div class="col-md-6">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered" ClientIDMode="Static" AutoGenerateColumns="False" DataKeyNames="Unit_id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblunitname" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" CssClass="hidden" runat="server" Text='<%# Bind("Unit_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QuantityType" HeaderText="Quantity Type" />
                                        <asp:BoundField DataField="UQCCode" HeaderText="UQC Code" />
                                        <asp:BoundField DataField="NoOfDecimalPlace" HeaderText="Number of Decimal Places" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" class="nav-icon fas fa-edit" CausesValidation="False" CommandName="Select"></asp:LinkButton>
                                              <%--  <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return getConfirmation();"></asp:LinkButton>--%>
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
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>

   
    <script src="../js/ValidationJs.js"></script>
</asp:Content>

