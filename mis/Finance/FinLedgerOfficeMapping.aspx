<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinLedgerOfficeMapping.aspx.cs" Inherits="mis_Finance_FinLedgerOfficeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }




        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-header">
                            <h3 class="box-title">Ledger Delete Permanently</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                 <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Ledger Name <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLedgerName" class="form-control select2" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlLedgerName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            <asp:Panel ID="panel1" Visible="false" runat="server">                            
       
                            <div class="row">

                        <div class="col-md-12">

                            <fieldset>
                                <legend>
                                    Mapped Office</legend>

                                <div class="row" style="margin-bottom: 10px;">
                                    <div class="col-md-3">
                                        <legend>
                                            <asp:CheckBox ID="chkHeadOffice" runat="server" Text="Apex Federation" onclick="CheckuncheckAll();" />
                                        </legend>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>
                                                <asp:CheckBox ID="chkDistrict" runat="server" Text="ALL Dugdh Sangh" onclick="CheckOfficeAllDistrict();" /></legend>
                                            <div class="table-responsive">
                                                <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table District customCSS cbl_all_Office" RepeatColumns="3" RepeatDirection="Horizontal" onclick="CheckUncheckOfficeAllDistrict();">
                                                </asp:CheckBoxList>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row hidden">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>

                                                <asp:CheckBox ID="chkAllOtherOffice" runat="server" Text="ALL Other Office" onclick="CheckOfficeAllOther();" /></legend>
                                            <div class="table-responsive">
                                                <asp:CheckBoxList ID="chkOtherOffice" runat="server" ClientIDMode="Static" CssClass="table Other customCSS cbl_all_Office" RepeatColumns="5" RepeatDirection="Horizontal" onclick="CheckuncheckOfficeAllOther();">
                                                </asp:CheckBoxList>
                                            </div>
                                        </fieldset>
                                    </div>

                                </div>
                                <small><span id="valchkOffice" class="text-danger"></span></small>
                            </fieldset>

                        </div>

                    </div>
                            <%--<div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table customCSS cbl_all_Office" RepeatColumns="5" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>--%>
                                </asp:Panel> 
                            <div class="row">
                              
                                 <div class="col-md-2">
                                    <asp:Button ID="btnDel" runat="server" CssClass="btn btn-danger btn-block" ClientIDMode="Static" Text="Delete"  OnClick="btnDel_Click"/>
                                </div>
                                
                            </div>
                                
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    
</asp:Content>

