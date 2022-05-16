<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptSMPTesting.aspx.cs" Inherits="mis_QCReports_RptSMPTesting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">SMP Testing</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>DATE</label>
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
                                        <label>SHIFT</label>
                                        <asp:DropDownList ID="ddlshift" CssClass="form-control" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>SHIFT 1</asp:ListItem>
                                            <asp:ListItem>SHIFT 2</asp:ListItem>
                                            <asp:ListItem>SHIFT 3</asp:ListItem>
                                        </asp:DropDownList>
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
                                        <label>CONTROL UNIT</label>
                                        <asp:TextBox ID="txtControlUnit" CssClass="form-control" runat="server" placeholder="CONTROL UNIT"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BAG NO.</label>
                                        <asp:TextBox ID="txtBagNo" CssClass="form-control" runat="server" placeholder="BAG NO."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FLAVOUR</label>
                                        <asp:TextBox ID="txtFlavour" CssClass="form-control" runat="server" placeholder="FLAVOUR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>COLOUR</label>
                                        <asp:TextBox ID="txtColour" CssClass="form-control" runat="server" placeholder="COLOUR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>MOISTURE DETAILS</legend>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>EMPTY DISH<br />
                                                    &nbsp;</label>
                                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="EMPTY DISH"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>DISH WASH SMP<br />
                                                    &nbsp;</label>
                                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" placeholder="DISH WASH SMP"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>SMP WEIGHT<br />
                                                    &nbsp;</label>
                                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" placeholder="SMP WEIGHT"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>WEIGHT OF DISH AFTER DRYING</label>
                                                <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" placeholder="WEIGHT OF DISH AFTER DRYING"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>DIFF. IN NO. 2-4<br />
                                                    &nbsp;</label>
                                                <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" placeholder="DIFF. IN NO. 2-4"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>MOISTURE %<br />
                                                    &nbsp;</label>
                                                <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server" placeholder="MOISTURE %"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>FAT %</label>
                                                <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server" placeholder="FAT %"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>ACIDITY</label>
                                                <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server" placeholder="DISH WASH SMP"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>INSOLUBILITY INDEX</label>
                                                <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server" placeholder="INSOLUBILITY INDEX"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>BULK DESNSITY</label>
                                                <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server" placeholder="BULK DESNSITY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>PROTEIN</label>
                                                <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server" placeholder="PROTEIN"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>TOTAL SOLIDS</label>
                                                <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server" placeholder="TOTAL SOLIDS"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>ASH %</label>
                                                <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server" placeholder="ASH %"></asp:TextBox>
                                            </div>
                                        </div>
                                    </fieldset>
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

