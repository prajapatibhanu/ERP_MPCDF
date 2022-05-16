<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DSWiseProgressiveReport.aspx.cs" Inherits="mis_ResearchandDev_DSWiseProgressiveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-body">
                        <div class="box-header">
                            <h3 class="box-title">Dugdh Sangh Wise Progressive Report of R & D</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Dugdh Sangh</label>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">                                              
                                                <asp:ListItem>भोपाल सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>ग्वालियर सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>इंदौर सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>जबलपुर सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>उज्जैन सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>बुंदेलखंड सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                            </asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Project Name</label>
                                    <asp:ListBox runat="server" ID="ddlProjectName" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">                                              
                                                <asp:ListItem>Testing</asp:ListItem>                                                                                         
                                            </asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" style="margin-top:21px;" CssClass="btn btn-primary btn-block" OnClick="btnSearch_Click"/>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div id="divtable" runat="server">
                                        <table class="table table-bordered">
                                        <tr>
                                            <th>S.no</th>
                                            <th>Dugdh Sangh</th>
                                            <th>Project Name</th>
                                            <th>Received Date</th>
                                            <th>Action Plan</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>भोपाल सहकारी दुग्ध संघ मर्यादित</td>
                                            <td>Testing</td>
                                            <td>30/05/2020</td>
                                            <td>Under Trial</td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>ग्वालियर सहकारी दुग्ध संघ मर्यादित</td>
                                            <td>Testing</td>
                                            <td>30/05/2020</td>
                                            <td>Will use After some time</td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>इंदौर सहकारी दुग्ध संघ मर्यादित</td>
                                            <td>Testing</td>
                                            <td>30/05/2020</td>
                                            <td>Other's</td>
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>जबलपुर सहकारी दुग्ध संघ मर्यादित</td>
                                            <td>Testing</td>
                                            <td>30/05/2020</td>
                                            <td>Under Trial</td>
                                        </tr>
                                    </table>
                                    </div>
                                    <div id="div1" runat="server">
                                        <table class="table table-bordered">
                                        <tr>
                                            <th>S.no</th>
                                            <th>Dugdh Sangh</th>
                                            <th>Project Name</th>
                                            <th>Received Date</th>
                                            <th>Action Plan</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>भोपाल सहकारी दुग्ध संघ मर्यादित</td>
                                            <td>Testing</td>
                                            <td>30/05/2020</td>
                                            <td>Under Trial</td>
                                        </tr>
                                       
                                    </table>
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
    <script src="../../../mis/js/jquery.js" type="text/javascript"></script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        $(function () {
            $('[id*=ddlProjectName]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        </script>
</asp:Content>

