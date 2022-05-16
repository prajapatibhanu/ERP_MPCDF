<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyMilkCollectionStatus.aspx.cs" Inherits="mis_MilkCollection_SocietyMilkCollectionStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Society Milk Collection Status</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvtxtDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtDate" autocomplete="off"  CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                                     <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlBMCTankerRootName" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" onclick="btnSearch_Click"/>
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
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>                            
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
                                                     <asp:TemplateField HeaderText="Date">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblDate" Text='<%# Eval("Dt_Date") %>' runat="server"></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BMC">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblBMC" Text='<%# Eval("Office_Name") %>' runat="server"></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Producer  Count">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblProducerCount" Text='<%# Eval("ProducerCount") %>' runat="server"></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Collection(From Producers)">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblMilkCollection" Text='<%# Eval("MilkCollectionCount") %>' runat="server"></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Qty(In Lt.)">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblQtyTotal" Text='<%# Eval("QtyTotal") %>' runat="server"></asp:Label>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

