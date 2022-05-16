<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GrgVehicleReportDateWise.aspx.cs" Inherits="mis_Garage_GrgVehicleReportDateWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {
            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }

            .Dtime {
                display: block;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow hide_print">
                    <h3 class="box-title">Date Range Wise Vehicle Report</h3>

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>

                <div class="box-body">
                    <div class="hide_print">

                        <div class="row">
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
                                    <label>To Date</label><span style="color: red">*</span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vehicle Owned Type</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlVehicleOwnedType" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicleOwnedType_SelectedIndexChanged">
                                        <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                        <asp:ListItem Value="Hired">Hired</asp:ListItem>
                                        <asp:ListItem Value="Owned">Owned</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vehicle No</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlVehicleNo" runat="server" class="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div class="row">
                        <div class="col-md-12 table-responsive">

                            <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" DataKeyNames="VehicleID" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Date" HeaderText="Date" />
                                    <asp:BoundField DataField="VehicleOwnedType" HeaderText="Vehicle Owned Type" />
                                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                    <asp:BoundField DataField="VehicleModel" HeaderText="Vehicle Model" />
                                    <asp:BoundField DataField="FuelType" HeaderText="Fuel Type" />
                                    <asp:BoundField DataField="VehicleAverage" HeaderText="Vehicle Average" />
                                    <asp:BoundField DataField="MonthlyRent" HeaderText="Monthly Rent" />
                                    <asp:BoundField DataField="Agency" HeaderText="Agency" />
                                    <asp:BoundField DataField="Allot_Incharge" HeaderText="Allot/Incharge" />
                                    <asp:BoundField DataField="VehicleRegistrationNo" HeaderText="Vehicle Registration No" />
                                    <asp:BoundField DataField="InsuranceValue" HeaderText="InsuranceValue" />
                                    <asp:BoundField DataField="InsuranceValidTill" HeaderText="Insurance Valid Till" />
                                    <asp:BoundField DataField="DriverName" HeaderText="Driver Name" />
                                    <asp:BoundField DataField="DriverLicenseNo" HeaderText="DriverLicense No" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" Visible='<%# Eval("Status").ToString() == "0" ?  true: false %>' CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Item Name will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                            <asp:HyperLink ID="HL_Edit" CssClass="label label-info" Target="_blank" NavigateUrl='<%# "~/mis/Garage/GrgVehicleMaster.aspx?Action=" + APIProcedure.Client_Encrypt(Eval("VehicleID").ToString())%>' runat="server">Edit</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>

    <script>






        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "bSort": false,
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
                        title: $('h3').text(),
                        exportOptions: {
                            //columns: ':not(.no-print)'
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        //title: 'Date Range Wise Vehicle Report',
                        exportOptions: {
                            orthogonal: 'sort',
                            format: {
                                body: function (data, row, column, node) {
                                    data = data.trim();
                                    data = column === 4 ? "\0" + data : data;
                                    return data.replace(/(&nbsp;|<([^>]+)>)/ig, "");
                                    //var data = data.find("span").text();
                                    //return data;
                                }
                            },
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
                        },
                        //customizeData: function (data) {
                        //    for (var i = 0; i < data.body.length; i++) {
                        //        for (var j = 0; j < data.body[i].length; j++) {
                        //            if (j ==4) {
                        //                data.body[i][j] = '\u200C' + data.body[i][j];
                        //            }

                        //        }
                        //    }
                        //},
                        // footer: true
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
            //t.on('order.dt search.dt', function () {
            //    t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            //        cell.innerHTML = i + 1;
            //    });
            //}).draw();
        });
    </script>



</asp:Content>







