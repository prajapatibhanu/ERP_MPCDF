<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRCRReport.aspx.cs" Inherits="mis_HRCRReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }

        #myInput {
            background-image: url('images/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">CR Report</h3>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOffice" runat="server" class="form-control select2" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>From Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-3" style="margin-top: 19px;">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-block btn-success" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblmsgbox" runat="server" Text=""></asp:Label>

                                <asp:GridView ID="gvDetails" runat="server" class="datatable table table-hover table-bordered pagination-ys" OnRowDataBound="gvDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--                                        <asp:TemplateField ItemStyle-Width="30">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("Emp_ID").ToString()%>' Checked='<%# Eval("Form_IsActive").ToString()=="0" ? false : true %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmp_Name" Text='<%# Eval("Emp_Name").ToString() %>' runat="server" />
                                                <asp:HiddenField ID="HF_Emp_ID" Value='<%# Eval("Emp_ID").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:BoundField HeaderText="Employee Name" DataField="Emp_Name" />  --%>
                                        <%--<asp:BoundField HeaderText="Employee Code" DataField="UserName" />--%>
                                        <%--                                        <asp:BoundField HeaderText="Mobile No" DataField="Emp_MobileNo" />
                                        <asp:BoundField HeaderText="Office/Branch" DataField="Office_Name" />--%>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
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
            pageLength: 100,
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
                    title: $('.lblheadingFirst').html(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6]
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




        $('#ddlFromYear,#ddlToYear').change(function () {
            
            var startyear = parseInt($('#ddlFromYear').val(),10);
            var endtyear = parseInt($('#ddlToYear').val(),10);
            //alert(startyear);
            //alert(endtyear);
            if (startyear > endtyear) {
                alert("From Year should not be greater than To Year.");
                $('#ddlToYear').val($('#ddlFromYear').val());
                //$('#ddlFromYear').prop('selectedIndex', 0);
                //$('#ddlToYear').prop('selectedIndex', 0);
            }
        });


        $('.DropdownList1').change(function () {
            alert("Handler for .change() called.");
        });

        //$('#ddlToYear').change(function () {
        //    var startyear = $('#ddlFromYear').val();
        //    var endtyear = $('#ddlToYear').val();
        //    if (startyear > endtyear) {
        //        alert("From Year should not be greater than To Year.");
        //    }
        //});

        //function checkYearValidation() {

        //    if (start > end) {

        //        if ($('#txtToDate').val() != "") {
        //            alert("To Date can not be less than From Date.");
        //            $('#txtToDate').val("");
        //        }
        //    }
        //    if (start > end) {

        //        if ($('#txtFromDate').val() != "") {
        //            alert("From date should not be greater than To Date.");
        //            $('#txtFromDate').val("");
        //        }
        //    }

        //}
    </script>

</asp:Content>

