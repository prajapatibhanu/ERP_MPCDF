﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDailyAttendenceReport.aspx.cs" Inherits="HRDailyAttendenceReport" %>

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
                            <h3 class="box-title" id="Label1">Attendence Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>FROM DATE<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtStartDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>TO DATE<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtEndDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">
                                        <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered"
                                                AutoGenerateColumns="false"  ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" Text='<%# Eval("Status").ToString()%>' runat="server"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DATE" HeaderText="Date" />
                                                    <asp:BoundField DataField="Day" HeaderText="Day" />
                                                    <asp:BoundField DataField="LoginTime" HeaderText="LoginTime" />
                                                    <asp:BoundField DataField="LogoutTime" HeaderText="LogoutTime" />
                                                    <%-- <asp:BoundField DataField="Login_LogoutTime" HeaderText="O'Clock" />
                                                 <asp:BoundField DataField="DiffTime" HeaderText="Difference(In min)" />--%>
                                                    <asp:BoundField DataField="WorkingHours" HeaderText="Working Hours" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
    
   <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
   <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
   <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
   <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
			pageLength:100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });

        $('#txtStartDate').datepicker({
            autoclose: true
        });
        $('#txtEndDate').datepicker({
            autoclose: true
        });

        function validateform() {
            debugger
            var msg = "";
            var fromdate = document.getElementById('<%=txtStartDate.ClientID%>').value.trim();
            var Todate = document.getElementById('<%=txtEndDate.ClientID%>').value.trim();
            if (document.getElementById('<%=txtStartDate.ClientID%>').value.trim() == "") {
                msg += "Select From Date. \n";
            }
            if (document.getElementById('<%=txtEndDate.ClientID%>').value.trim() == "") {
                msg += "Select To Date. \n";
            }
            if (fromdate != "" && Todate != "") {
                var Fdate = new Date(fromdate);
                var Tdate = new Date(Todate);

                if (Fdate > Tdate) {
                    alert("Invalid Date Range!\nStart Date cannot be after End Date!")
                    document.getElementById("lblMsg").innerHTML = "";
                }
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

