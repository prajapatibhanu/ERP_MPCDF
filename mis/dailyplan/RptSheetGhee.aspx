<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptSheetGhee.aspx.cs" Inherits="mis_dailyplan_RptSheetGhee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        li.header {
            font-size: 14px !important;
            color: #F44336 !important;
        }

        span#ctl00_spnUsername {
            text-transform: uppercase;
            font-weight: 600;
            font-size: 16px;
        }

        li.dropdown.tasks-menu.classhide a {
            padding: 4px 10px 0px 0px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        .navbar {
            background: #ebf4ff !important;
            box-shadow: 0px 0px 8px #0058a6;
            color: #0058a6;
        }

        .skin-green-light .main-header .logo {
            background: #fff !important;
        }


        a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
            box-shadow: 1px 1px 1px #808080;
        }

        a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
            box-shadow: 1px 1px 1px #808080;
        }

        .btn-success {
            background-color: #1d7ce0;
            border-color: #1d7ce0;
        }

            .btn-success:hover, .btn-success:active, .btn-success.hover, .btn-success:focus, .btn-success.focus, .btn-success:active:focus {
                background-color: #1d7ce0;
                border-color: #1d7ce0;
            }

            .btn-success:hover {
                color: #fff;
                background-color: #1d7ce0;
                border-color: #1d7ce0;
            }

        fieldset {
            border: 1px solid #ff7836;
            padding: 15px;
            margin-bottom: 15px;
        }

        legend {
            width: initial;
            padding: 4px 15px;
            margin: 0;
            font-size: 12px;
            font-weight: bold;
            color: #00427b;
            text-transform: uppercase;
            border: 1px solid #ff7836;
        }

        table .select2 {
            width: 100px !important;
        }

        .btnmargin {
            margin-top: 18px;
        }

        @media print {
            .print_hidden {
                display: none !important;
            }

            .box {
                border: none !important;
            }
            .footerprint h5{
                font-weight:600;
            }
            table 
            {
                border:1px solid #dcdcdc !important;
            }
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header print_hidden">
                    <h3 class="box-title">Daily Ghee Account Sheet</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <span id="ctl00_ContentBody_lblMsg"></span>
                <div class="box-body">
                    <div class="row print_hidden">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>तारीख / Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                            <div class="form-group">
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="mainsheet" runat="server" id="mainsheet">
                        </div>
                    </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


        $(document).ready(function () {


            var checkbox = $('table tbody input[type="checkbox"]:disabled');
            for (var i = 0; i < checkbox.length; i++) {
                $(checkbox[i]).parents('tr').css('background-color', 'rgba(255, 24, 0, 0.55)');
                //    $(checkbox[i]).parents('tr').css('color', '#FFFFFF');

                //$('table tbody input[type="checkbox"]').css('width', '25px');
            }



        });
        function validateform() {
            debugger;
            var msg = "";
            $("#valddlDS").html("");
            $("#valddlProductSection").html("");
            if (document.getElementById('<%=ddlDS.ClientID%>').selectedIndex == 0) {
                msg += "Select Dugdh Sangh. \n"
                $("#valddlDS").html("Select Dugdh Sangh");
            }
            <%--if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
                $("#valddlProductSection").html("Select Product Section");
            }--%>
            <%--if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Please Select Shift. \n"
            }--%>
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg = "Please Select Date."
            }

            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }
        }
    </script>

    <%--<script src="../HR/js/jquery.dataTables.min.js"></script>
    <script src="../HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../HR/js/dataTables.buttons.min.js"></script>
    <script src="../HR/js/jszip.min.js"></script>
    <script src="../HR/js/buttons.html5.min.js"></script>
    <script src="../HR/js/buttons.print.min.js"></script>


    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],

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
                        title: $('.lblSeletedInfo').text(),
                        exportOptions: {
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h1').text(),
                        exportOptions: {
                            columns: ':not(.no-print)'
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
    </script>--%>
</asp:Content>

