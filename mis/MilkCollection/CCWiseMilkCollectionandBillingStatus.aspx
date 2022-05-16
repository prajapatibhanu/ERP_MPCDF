<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CCWiseMilkCollectionandBillingStatus.aspx.cs" Inherits="mis_MilkCollection_CCWiseMilkCollectionandBillingStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {
            .no-print {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">CC Wise Society Milk Collection/Billing Status</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BMC Root</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlBMCTankerRootName" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                    ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                     <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                                   
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Report</legend>
                                <div class="row">
                                    <div class="col-md-3 pull-left noprint">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 pull-left">
                                        <div class="form-group">
                                            <asp:Label ID="lblCount" Font-Bold="true" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvReports" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                              
                                                    <asp:TemplateField HeaderText="Society">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBMC" Text='<%# Eval("Office_Name") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Collection Entry Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMilkCollectionStatus" CssClass="label" style="font-size:16px;" Text='<%# Eval("MilkCollectionStatus") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Entry Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastEntryDate" Text='<%# Eval("EntryDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Billing Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBillingStatus"  CssClass="label" style="font-size:16px;" Text='<%# Eval("BillingStatus") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Billing Period">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastEntryDate" Text='<%# Eval("BillingPeriod") %>' runat="server"></asp:Label>
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
                </div>
            </div>
        </section>
        
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
       
    </script>
</asp:Content>

