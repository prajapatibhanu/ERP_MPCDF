<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TableButterShelfLife.aspx.cs" Inherits="mis_QCReports_TableButterShelfLife" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">TABLE BUTTER SHELF LIFE</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>DATE OF PROCCESSING</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDate" autocomplete="off" placeholder="DATE" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BATCH NO</label>
                                        <asp:TextBox ID="txBatchNo" CssClass="form-control" runat="server" placeholder="BATCH NO"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>OT</label>
                                        <asp:TextBox ID="TextBox14" CssClass="form-control" runat="server" placeholder="OT"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>VIRTUAL TEMPRETURE</label>
                                        <asp:TextBox ID="txtVirtualTemp" CssClass="form-control" runat="server" placeholder="VIRTUAL TEMPRETURE"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>OT</label>
                                        <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" placeholder="OT"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>ACIDITY</label>
                                        <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server" placeholder="ACIDITY"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>VIRTUAL TEMPRETURE</label>
                                        <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" placeholder="VIRTUAL TEMPRETURE"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>DAT g.m.</label>
                                        <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" placeholder="VIRTUAL TEMPRETURE"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>REMARK</label>
                                        <asp:TextBox ID="TextBox1" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server" placeholder="REMARK"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn btn-primary" />
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

