<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SkimMilkPowderAnalysis.aspx.cs" Inherits="mis_QCReports_SkimMilk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">SKIM MILK POWDER ANALYSIS</h3>
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
                                        <label>PROD. CODE</label>
                                        <asp:TextBox ID="txBatchNo" CssClass="form-control" runat="server" placeholder="PROD. CODE"></asp:TextBox>
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
                                        <label>TIME</label>
                                        <asp:TextBox ID="TextBox14" CssClass="form-control" runat="server" placeholder="TIME"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BAG NO.</label>
                                        <asp:TextBox ID="txtBagNo" CssClass="form-control" runat="server" placeholder="BAG NO."></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>TEMPRATURE</label>
                                        <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server" placeholder="TEMPRATURE"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FLAVOUR</label>
                                        <asp:TextBox ID="txtFlavour" CssClass="form-control" runat="server" placeholder="FLAVOUR"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>SCORCHED PARTICULERS</label>
                                        <asp:TextBox ID="txtColour" CssClass="form-control" runat="server" placeholder="SCORCHED PARTICULERS"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>LOCTOS EXTRA</label>
                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="LOCTOS EXTRA"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FAT %</label>
                                        <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" placeholder="FAT %"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>MOISTURE %</label>
                                        <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" placeholder="MOISTURE %"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>ACIDITY</label>
                                        <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" placeholder="ACIDITY"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>INSOL. INDEX M.L.</label>
                                        <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" placeholder="INSOL. INDEX M.L."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>B. D. g/ml</label>
                                        <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server" placeholder="B. D. g/ml"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>PROTEIN %</label>
                                        <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server" placeholder="PROTEIN %"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>REMARK</label>
                                        <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server" placeholder="REMARK"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>QUALITY OF RAW MATERIAL</legend>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>
                                                        SOURCE</label>
                                                    <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server" placeholder="SOURCE"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>
                                                        TEMP.</label>
                                                    <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server" placeholder="TEMP."></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>
                                                        FAT %</label>
                                                    <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server" placeholder="FAT %"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>SNF %</label>
                                                    <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server" placeholder="SNF %"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>
                                                        ACIDITY</label>
                                                    <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server" placeholder="ACIDITY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>
                                                        MBRT</label>
                                                    <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server" placeholder="MBRT"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>NEUTRALIZER</label>
                                                    <asp:TextBox ID="TextBox17" CssClass="form-control" runat="server" placeholder="NEUTRALIZER"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>PRESERVATIVE</label>
                                                    <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server" placeholder="PRESERVATIVE"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>TOTAL PRODUCTION</legend>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>QTY</label>
                                                <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server" placeholder="QTY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>BEG</label>
                                                <asp:TextBox ID="TextBox20" CssClass="form-control" runat="server" placeholder="BAG"></asp:TextBox>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>PASSED BAG</legend>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>QTY</label>
                                                <asp:TextBox ID="TextBox21" CssClass="form-control" runat="server" placeholder="QTY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>BAG</label>
                                                <asp:TextBox ID="TextBox22" CssClass="form-control" runat="server" placeholder="BAG"></asp:TextBox>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>PASSED BAG</legend>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>QTY</label>
                                                <asp:TextBox ID="TextBox23" CssClass="form-control" runat="server" placeholder="QTY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>BAG</label>
                                                <asp:TextBox ID="TextBox24" CssClass="form-control" runat="server" placeholder="BAG"></asp:TextBox>
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

