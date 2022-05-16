<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DateWiseRDReport.aspx.cs" Inherits="mis_ResearchandDev_DateWiseRDReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">DateWise R & D Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" Style="margin-top: 25px;" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                            <div class="col-md-12">
                                <div id="div1" runat="server">
                                    <table class="table table-bordered">
                                        <tr>
                                            <th>S.no</th>
                                            <th>Start Date</th>
                                            <th>Project Name</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>25/05/2020</td>
                                            <td>Testing</td>
                                            <td>Pending</td>
                                            <td><asp:LinkButton ID="lnkbtn" runat="server" data-toggle="modal" data-target="#ProjectDetailModal" CssClass="label label-info">View</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>25/05/2020</td>
                                            <td>Testing</td>
                                            <td>Completed</td>
                                            <td><asp:LinkButton ID="LinkButton1" runat="server" data-toggle="modal" data-target="#ProjectDetailModal1" CssClass="label label-info">View</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
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
                                            <td>Status:</td>
                                            <td>Pending</td>
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
                                            <td>Supporting Docs:</td>
                                            <td><a href="">View</a></td>
                                        </tr>
                                        <tr>
                                            <td>Expected Outcomes:</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Actual Outcomes:</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Survey</td>
                                            <td>InComplete</td>
                                        </tr>

                                        <tr>
                                            <td>Implement In:</td>
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
        <div class="modal fade" id="ProjectDetailModal1" data-backdrop="false" data-keyboard="false" role="dialog">
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
                                            <td>Testing1</td>
                                        </tr>
                                        <tr>
                                            <td>Research Details:</td>
                                            <td>Testing Details</td>
                                        </tr>
                                        <tr>
                                            <td>Status:</td>
                                            <td>Completed</td>
                                        </tr>
                                         <tr>
                                            <td>Start Date:</td>
                                            <td>25/05/2020</td>
                                        </tr>
                                         <tr>
                                            <td>End Date:</td>
                                            <td>27/05/2020</td>
                                        </tr>                                        
                                         <tr>
                                            <td>Supporting Docs:</td>
                                            <td><a href="">View</a></td>
                                        </tr>
                                        <tr>
                                            <td>Expected Outcomes:</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Actual Outcomes:</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Survey</td>
                                            <td>Completed</td>
                                        </tr>

                                        <tr>
                                            <td>Implement In:</td>
                                            <td> All Dugdh Sangh</td>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

