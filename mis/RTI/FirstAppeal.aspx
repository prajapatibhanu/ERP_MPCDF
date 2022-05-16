<%@ Page Title="" Language="C#" MasterPageFile="~/mis/RTIMaster.master" AutoEventWireup="true" CodeFile="FirstAppeal.aspx.cs" Inherits="RTI_FirstAppeal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%--<section class="content-header">
            <div class="row">
                <!-- left column -->
                <div class="col-md-10 col-md-offset-1">
                    
                </div>
            </div>
        </section>--%>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12 ">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">RTI Details</h3>
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>

                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">
                                        <table class="table table-responsive table-striped table-bordered table-hover ">
                                            <tbody>
                                                <tr>
                                                    <th>S. No. </th>
                                                    <th>RTI Status </th>
                                                    <th>Registartion No. </th>
                                                    <th>RTI Subject </th>
                                                    <th>RTI File Date </th>
                                                    <th>First Appeal Date </th>
                                                    <th>RTI Detail </th>
                                                </tr>
                                                <tr>
                                                    <td>1 </td>
                                                    <td><span class='label label-warning' style="font-size: 10px">Open</span></td>

                                                    <td>1234/AAS/9958 </td>
                                                    <td>For Product Information</td>
                                                    <td>09/08/2018  </td>
                                                    <td>25/07/2018  </td>
                                                    <td>
                                                        <asp:LinkButton ID="btn1" runat="server" Text="View" OnClick="btn1_Click" CssClass="label label-default" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>2 </td>
                                                    <td><span class='label label-success' style="font-size: 10px">Close</span></td>
                                                    <td>1234/AAS/9468 </td>
                                                    <td>For Product Information</td>
                                                    <td>25/07/2018  </td>
                                                    <td>25/07/2018  </td>
                                                    <td>
                                                        <asp:LinkButton ID="btn2" runat="server" Text="View" CssClass="label label-default" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>3 </td>
                                                    <td><span class='label label-success' style="font-size: 10px">Close</span></td>
                                                    <td>1234/AAS/9018 </td>
                                                    <td>For Product Information</td>
                                                    <td>18/07/2018  </td>
                                                    <td>25/07/2018  </td>
                                                    <td>
                                                        <asp:LinkButton ID="btn3" runat="server" Text="View" CssClass="label label-default" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>4 </td>
                                                    <td><span class='label label-success' style="font-size: 10px">Close</span></td>
                                                    <td>1234/AAS/9418 </td>
                                                    <td>For Product Information</td>
                                                    <td>15/07/2018  </td>
                                                    <td>25/07/2018  </td>
                                                    <td>
                                                        <asp:LinkButton ID="btn4" runat="server" Text="View" CssClass="label label-default" />
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- /.box -->

                </div>

            </div>
            <!-- /.row -->

        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

