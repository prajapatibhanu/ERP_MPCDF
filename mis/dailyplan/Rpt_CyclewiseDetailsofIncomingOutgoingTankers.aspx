<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_CyclewiseDetailsofIncomingOutgoingTankers.aspx.cs" Inherits="mis_dailyplan_Rpt_CyclewiseDetailsofIncomingOutgoingTankers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">CC WISE DETAILS OF INCOMING/OUTGOING TANKER DETAILS</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlPSection" class="text-danger"></span></small>
                            </div>
                        </div>
                       <%-- <div class="col-md-2 no-print">
                            <div class="form-group">
                                <label>CC Name<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlCCName" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlCCName" class="text-danger"></span></small>
                            </div>
                        </div>--%>
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>
                                    Incoming/Outgoing<span class="text-danger">*</span>
                                </label>
                                <span class="pull-right">
                                            <asp:requiredfieldvalidator InitialValue="0" id="Requiredfieldvalidator1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="ddlIncOut" errormessage="Please Select Incoming/Outgoing." text="<i class='fa fa-exclamation-circle' title='Please Select Incoming/Outgoing !'></i>"></asp:requiredfieldvalidator>
                                           
                                        </span>
                                <asp:DropDownList ID="ddlIncOut" runat="server" CssClass="form-control select2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="Incoming">Incoming</asp:ListItem>
                                    <asp:ListItem Value="Outgoing">Outgoing</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                     <div class="col-md-3 no-print">
                                    <div class="form-group">
                                        <label>Month </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="rfv1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="txtFdt" errormessage="Please select Month." text="<i class='fa fa-exclamation-circle' title='Please select Month !'></i>"></asp:requiredfieldvalidator>
                                           
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtFdt" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2 no-print">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Style="margin-top: 19px;" CssClass="btn btn-success" ValidationGroup="a" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default no-print" Text="Print" OnClientClick="window.print();" />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-info no-print" Text="Export to Excel" OnClick="btnExcel_Click" />
                            </div>
                            <div class="table-responsive">
                                <div id="DivDetail" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $("#txtFdt").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
    </script>
</asp:Content>

