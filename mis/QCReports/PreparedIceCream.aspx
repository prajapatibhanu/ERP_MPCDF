<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PreparedIceCream.aspx.cs" Inherits="mis_QCReports_PreparedIceCream" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link rel="stylesheet" href="../css/bootstrap-timepicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">PREPARED ICE CREAM</h3>
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
                                        <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Shift 1</asp:ListItem>
                                            <asp:ListItem>Shift 2</asp:ListItem>
                                            <asp:ListItem>Shift 3</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Time</label>
                                        <div class="input-group bootstrap-timepicker timepicker">
                                            <asp:TextBox runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtTime" Text="09:30 AM"></asp:TextBox>
                                            <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>B. NO</label>
                                        <asp:TextBox ID="txtBNo" CssClass="form-control" runat="server" placeholder="B. NO"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>QTY</label>
                                        <asp:TextBox ID="txtQTY" CssClass="form-control" runat="server" placeholder="QTY"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>TEMP</label>
                                        <asp:TextBox ID="txtTemp" CssClass="form-control" runat="server" placeholder="TEMP"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FAT</label>
                                        <asp:TextBox ID="txtFat" CssClass="form-control" runat="server" placeholder="FAT"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>ACIDITY</label>
                                        <asp:TextBox ID="txtAcidity" CssClass="form-control" runat="server" placeholder="ACIDITY"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>F.S.</label>
                                        <asp:TextBox ID="txtFS" CssClass="form-control" runat="server" placeholder="F.S."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>P. TEST</label>
                                        <asp:TextBox ID="txtPTest" CssClass="form-control" runat="server" placeholder="P. TEST"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>C.I.</label>
                                        <asp:TextBox ID="txtCI" CssClass="form-control" runat="server" placeholder="C.I."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>OT</label>
                                        <asp:TextBox ID="txtOT" CssClass="form-control" runat="server" placeholder="OT"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>VARIENT</label>
                                        <asp:TextBox ID="txtVarient" CssClass="form-control" runat="server" placeholder="VARIENT"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="chk" Style="margin-top: 15px;" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>CUP</asp:ListItem>
                                            <asp:ListItem>CONE</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <fieldset>
                                        <legend>Quantity</legend>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:CheckBoxList ID="chkQty" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="50ML">50 ML</asp:ListItem>
                                                    <asp:ListItem Value="65ML">65 ML</asp:ListItem>
                                                    <asp:ListItem Value="100ML">100 ML</asp:ListItem>
                                                    <asp:ListItem Value="750ML">750 ML</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>COLOUR</label>
                                        <asp:RadioButtonList ID="rbtCoulour" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES">YES</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="NO">NO</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FLAVOUR</label>
                                        <asp:RadioButtonList ID="rbtFlavour" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES">YES</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="NO">NO</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>FAT</label>
                                        <asp:TextBox ID="txtVFat" CssClass="form-control" runat="server" placeholder="FAT"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>T.S.</label>
                                        <asp:TextBox ID="txtTs" CssClass="form-control" runat="server" placeholder="T.S."></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>SUGAR</label>
                                        <asp:RadioButtonList ID="rbtSuger" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="YES">YES</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="NO">NO</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>OT</label>
                                        <asp:TextBox ID="txtVOot" CssClass="form-control" runat="server" placeholder="OT"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>OVER</label>
                                        <asp:TextBox ID="txtOver" CssClass="form-control" runat="server" placeholder="OVEN MAST"></asp:TextBox>
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
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtTime').timepicker();
    </script>
</asp:Content>

