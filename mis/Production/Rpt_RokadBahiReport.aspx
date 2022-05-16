<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_RokadBahiReport.aspx.cs" Inherits="mis_Production_Rpt_RokadBahiReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Rokad Bahi Report</h3>
                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <div class="row">
                                <div class="col-md-3 no-print">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control no-print" data-date-end-date="0d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 no-print">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" onkeypress="return false;" runat="server" placeholder="dd/mm/yyyy" class="form-control no-print" data-date-end-date="0d" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-provide="datepicker" data-date-start-date="-365d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2 no-print">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" runat="server" Style="margin-top: 22px;" CssClass="btn btn-primary btn-block no-print" Text="Search" OnClientClick="return validateform();" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div id="DivReport" runat="server"></div>
                                        <%-- <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="4" style="text-align: center">जमा विवरण</th>
                                                    <th colspan="4" style="text-align: center">नामे विवरण</th>
                                                </tr>
                                                <tr>
                                                    <th style="text-align: center">दिनांक</th>
                                                    <th style="text-align: center">खाता</th>
                                                    <th style="text-align: center">विवरण</th>
                                                    <th style="text-align: center">राशि</th>
                                                    <th style="text-align: center">दिनांक</th>
                                                    <th style="text-align: center">खाता</th>
                                                    <th style="text-align: center">विवरण</th>
                                                    <th style="text-align: center">राशि</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>19/11/2020</td>
                                                    <td>-</td>
                                                    <td>Prarambhik Rokad</td>
                                                    <td>2000</td>
                                                    <td>19/11/2020</td>
                                                    <td>Dugdh ka kray</td>
                                                    <td>ABC</td>
                                                    <td>3300</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <th>Kul Yog</th>
                                                    <th>2000</th>
                                                    <td></td>
                                                    <td></td>
                                                    <th>Vyay Yog</th>
                                                    <th>3300</th>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <th>Kul Yog</th>
                                                    <th>2000</th>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <th>Antim Rokad</th>
                                                    <th>1300</th>
                                                </tr>
                                            </tbody>
                                        </table>--%>
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
    <script type="text/javascript">
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtTodate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter To Date. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

