<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="R_DProgressReport1.aspx.cs" Inherits="mis_ResearchandDev_R_DProgressReport1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Progress Report Of R & D Work</h3>
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">

                                <table class="table table-bordered">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Project Name</th>
                                        <th>Start Date</th>
                                        <th>Status</th>
                                        <th>Action</th>

                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton2" runat="server"  data-toggle="modal" data-target="#ProjectDetailModal">Testing</asp:LinkButton></td>
                                        <td>25/05/2020</td>
                                        <td>Pending</td>
                                        <td>
                                            <asp:LinkButton ID="lnkbtnDetails" runat="server" CssClass="label label-info" Text="View" data-toggle="modal" data-target="#myModal"></asp:LinkButton></td>

                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td><asp:LinkButton ID="LinkButton3" runat="server"  data-toggle="modal" data-target="#ProjectDetailModal">Testing</asp:LinkButton></td>
                                        <td>25/05/2020</td>
                                        <td>Completed</td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-info" Text="View" data-toggle="modal" data-target="#myModal"></asp:LinkButton>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton4" runat="server" CssClass="label label-info" Text="Survey" data-toggle="modal" data-target="#SurveyModal"></asp:LinkButton></td>

                                        <%--<td><asp:TextBox ID="Remark" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">                                              
                                                <asp:ListItem>भोपाल सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>ग्वालियर सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>इंदौर सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>जबलपुर सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>उज्जैन सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                                <asp:ListItem>बुंदेलखंड सहकारी दुग्ध संघ मर्यादित</asp:ListItem>
                                            </asp:ListBox></td>--%>
                                    </tr>
                                </table>


                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="myModal" data-backdrop="false" data-keyboard="false" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Project Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Project Name: Testing</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Start Date: 25/05/2020</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Update Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>

                                            <asp:TextBox ID="txtUpdateDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Update's</label>
                                        <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                        </asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Upload Doc1</label>
                                        <asp:FileUpload ID="fileupload1" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Upload Doc2</label>
                                        <asp:FileUpload ID="fileupload" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                            </div>
                            <%--<asp:UpdatePanel ID="updatepnl" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control Select2" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Selected="true">Pending</asp:ListItem>
                                                    <asp:ListItem>Completed</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlStatus" runat="server" CssClass="form-control Select2" onchange="Showhide()">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Selected="true">Pending</asp:ListItem>
                                            <asp:ListItem>Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div id="div1" runat="server" style="display: none;">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label>Actual Outcome</label>
                                            <asp:TextBox ID="txtActualOutcome" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3">                                      
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Impement To</label>
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
                                </div>
                            </div>




                            <%--<div class="row">
                                <div class="col-md-4">
                                    <label>Send To</label>
                                    <div class="form-group">
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
                            </div>--%>
                        </div>
                        <div class="modal-footer">

                            <asp:Button runat="server" Text="Save" ID="Button1" ClientIDMode="Static" CssClass="btn btn-primary"></asp:Button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="SurveyModal" data-backdrop="false" data-keyboard="false" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">View Survey Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Project Name: Testing</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Start Date: 25/05/2020</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Survey Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>

                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Survey Details</label>
                                        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                        </asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">

                            <asp:Button runat="server" Text="Save" ID="Button2" ClientIDMode="Static" CssClass="btn btn-primary"></asp:Button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ProjectDetailModal" data-backdrop="false" data-keyboard="false" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">View Project Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table table-bordered">
                                        <tr>
                                            <td>Research Type:</td>
                                            <td>Product Research:</td>
                                        </tr>
                                        <tr>
                                            <td>Research & Development Plan for</td>
                                            <td>New Product</td>
                                        </tr>
                                        <tr>
                                            <td>Research Title:</td>
                                            <td>Testing</td>
                                        </tr>
                                        <tr>
                                            <td>Research Details:</td>
                                            <td>Testing Details</td>
                                        </tr>
                                         <tr>
                                            <td>Start Date:</td>
                                            <td>25/05/2020</td>
                                        </tr>
                                         <tr>
                                            <td>End Date:</td>
                                            <td>30/05/2020</td>
                                        </tr>                                        
                                         <tr>
                                            <td>Upload Images:</td>
                                            <td><a href="">View</a></td>
                                        </tr>
                                        <tr>
                                            <td>Expected Outcomes:</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">

                           
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ProjectViewModal() {
            debugger;
            $('#myModal').modal('show');

        }
    </script>
    <script src="/mis/js/jquery.js" type="text/javascript"></script>
    <link href="/mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="/mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        function Showhide() {
            debugger;
            if (document.getElementById('<%= ddlStatus.ClientID%>').selectedIndex == 2) {
                document.getElementById('<%= div1.ClientID%>').style.display = "block";
            }
            else {
                document.getElementById('<%= div1.ClientID%>').style.display = "none";

            }
        }
    </script>
</asp:Content>
