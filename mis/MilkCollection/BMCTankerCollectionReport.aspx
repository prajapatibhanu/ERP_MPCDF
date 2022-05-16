<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BMCTankerCollectionReport.aspx.cs" Inherits="mis_MilkCollection_BMCTankerCollectionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        @media print {
             
              .noprint {
                display: none;
            }
               
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
           
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">BMC Tanker Collection Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row noprint">                               
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDate" Enabled="true" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                              <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 pull-left noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                <asp:Button ID="btnPrint" runat="server" Visible="false" CssClass="btn btn-default" Text="Print" OnClientClick="window.print();"/>
                            </div>
                                
                            </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                                <asp:GridView ID="gvBMCTankerCollectionDetails" ShowFooter="true" EmptyDataText="No Record Found" CssClass="table table-bordered" AutoGenerateColumns="false" runat="server" OnRowCreated="gvBMCTankerCollectionDetails_RowCreated">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vendor Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_VenderName" runat="server" Text='<%# Eval("V_VenderName") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanker No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="V_VehicleNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Chamber Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblChamberType" runat="server" Text='<%# Eval("ChamberType") %>'></asp:Label>                                                            
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>                                                            
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>                                                         
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kg Fat">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKgFat" runat="server" Text='<%# Eval("FatKg") %>'></asp:Label>
                                                             
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kg SNF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKgSNF" runat="server" Text='<%# Eval("SnfKg") %>'></asp:Label>
                                                              
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

