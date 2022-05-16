<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WhiteButterTesting.aspx.cs" Inherits="mis_QCReports_WhiteButterTesting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">WHITE BUTTER TESTING REGISTER</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>DATE OF PRODUCTION</label>
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
                                        <label>NO OF CASSES</label>
                                        <asp:TextBox ID="TextBox14" CssClass="form-control" runat="server" placeholder="NO OF CASSES"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>MOISTURE %</label>
                                        <asp:TextBox ID="txtMoisture" CssClass="form-control" runat="server" placeholder="MOISTURE %"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>CURD %</label>
                                        <asp:TextBox ID="txtBagNo" CssClass="form-control" runat="server" placeholder="CURD %"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FAT %</label>
                                        <asp:TextBox ID="txtFlavour" CssClass="form-control" runat="server" placeholder="FAT %"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>GENERAL APPEARA</label>
                                        <asp:TextBox ID="txtColour" CssClass="form-control" runat="server" placeholder="GENERAL APPEARA"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>ACIDITY</label>
                                        <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server" placeholder="ACIDITY"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>B. R. INDX</label>
                                        <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server" placeholder="B. R. INDX"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>R. M. VALUE</label>
                                        <asp:TextBox ID="TextBox17" CssClass="form-control" runat="server" placeholder="R. M. VALUE"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>F. F. A. VALUE</label>
                                        <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server" placeholder="F. F. A. VALUE"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>TESTED BY </label>
                                        <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server" placeholder="TESTED BY"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
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

