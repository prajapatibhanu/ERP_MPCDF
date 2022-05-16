<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="WarehouseBranchReport.aspx.cs" Inherits="mis_Warehouse_WarehouseReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                <div class="box box-success">

                    <%--<div class="col-md-12">

                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Branch Wise Stock Report</h3>
                        </div>
                    </div>--%>

                    <%--<div class="row">
                        <div class="col-md-12">
                            <hr />
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <br />
                            <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportPath="mis\Warehouse\NewReport.rdlc">
                                </LocalReport>
                            </rsweb:ReportViewer>
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

