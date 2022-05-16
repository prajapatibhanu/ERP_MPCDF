<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptAvailableItemStock.aspx.cs" Inherits="mis_MilkCollection_RptAvailableItemStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        @media print {
             
              .noprint {
                display: none;
            }
               .
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Available Item Stock</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body ">
                            <fieldset class="noprint">
                                <legend>Filter</legend>
                                <div class="row noprint">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFarmer" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>


                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date  </label>
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
                                </div>--%>

                                <div class="col-md-3">
                                    <label>Item Category<span style="color: red;">*</span></label>
                                   <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Category!'></i>" ErrorMessage="Select Item Category" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemcategory" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged" OnInit="ddlitemcategory_Init" AutoPostBack="true" CssClass="form-control select2" runat="server" ValidationGroup="a"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>Item Type <span style="color: red;">*</span></label>
                                   <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Type!'></i>" ErrorMessage="Select Item Type" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemtype" OnInit="ddlitemtype_Init"  CssClass="form-control select2" runat="server" ValidationGroup="a"></asp:DropDownList>
                                    </div>

                                </div>

                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary"  ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" OnClick="btnSubmit_Click"/>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                            </fieldset>
                            <fieldset>
                                <legend>Report</legend>
                                <div class="row noprint">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        <asp:Button ID="btnExport" Visible="false" runat="server" CssClass="btn btn-default" Text="Export" OnClick="btnExport_Click"/>
                                        <asp:Button ID="btnPrint" Visible="false" runat="server" CssClass="btn btn-default" Text="Print" OnClientClick="window.print()"/>
                                    </div>
                                        </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvReport" runat="server" EmptyDataText="No Record Found" CssClass="table table-bordered table-responsive" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Item Category" DataField="ItemCatName"/>
                                                <asp:BoundField HeaderText="Item Type" DataField="ItemTypeName"/>
                                                <asp:BoundField HeaderText="Item Name" DataField="ItemName"/>
                                                <asp:TemplateField HeaderText="Available Stock">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAvailableStock" runat="server" Text='<%# string.Concat(Eval("Available") + " " + Eval("UQCCode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
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

