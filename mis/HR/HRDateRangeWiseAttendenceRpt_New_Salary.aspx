﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDateRangeWiseAttendenceRpt_New_Salary.aspx.cs" Inherits="mis_HR_HRDateRangeWiseAttendenceRpt_New_Salary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        input[type="radio"] {
            margin-right: 2px;
            margin-left: 6px;
        }

        .cssPedding {
            padding: 5px !important;
        }

        .dataTables_wrapper, div#ModalAttendence {
            font-size: 13px;
        }

            div#ModalAttendence .form-control {
                height: 26px;
                padding: 3px 5px;
                font-size: 12px;
            }

            .dataTables_wrapper th, .dataTables_wrapper td {
                padding: 5px !important;
            }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
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

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .dt-buttons {
            margin-bottom: 11px;
        }

        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 40px;
        }

        @media print {
            .form-group {
                display: none;
            }

            .box-header.with-border {
                display: none;
            }

            .box {
                border: none;
            }

            .main-footer {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12 ">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Attendance For Payroll</h3>
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtStartDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtEndDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control Select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee Type</label><span style="color: red">*</span>
                                        <asp:DropDownList ID="ddlEmployeeType" runat="server" class="form-control select2">
                                            <asp:ListItem Value="Permanent">Permanent Employee</asp:ListItem>
                                            <asp:ListItem Value="Fixed Employee">Fixed Employee</asp:ListItem>
                                            <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-success " Style="margin-top: 24px;" Text="Search" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="text-center">
                                                    <img src="../image/mpagro-logo.png" class="salary-logo"><br>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p style="text-align: center">
                                                    मध्य प्रदेश स्टेट एग्रो इंडस्ट्रीज डेवलपमेंट कारपोरेशन लिमिटेड
                                                    <br>
                                                    (मध्य प्रदेश शासन का उपक्रम)<br>
                                                    पंचानन भवन, तृतीय तल, मालवीय नगर भोपाल<br>
                                                    Phone(0755) - 2552652,2551756,2551807 Fax: 0755-2557305
                                                </p>
                                            </div>
                                        </div>
                                        <br />
                                        <asp:Label ID="lbltotalworkingdays" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblselectiondetail" runat="server" Text=""></asp:Label>

                                        <div class="" style="max-height: 500px; overflow: scroll">
                                            <asp:GridView ID="GridView3" runat="server" class="datatable table table-hover table-bordered"
                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Officer/Employee" />
                                                    <asp:BoundField DataField="CasualLeaveCount" HeaderText="Casual Leave" />
                                                    <asp:BoundField DataField="EarnedLeaveCount" HeaderText="Earned Leave" />
                                                    <asp:BoundField DataField="MedicalLeaveCount" HeaderText="Medical Leave" />
                                                    <asp:BoundField DataField="OptionalLeaveCount" HeaderText="Optional Leave" />
                                                    <asp:BoundField DataField="OtherLeaveCount" HeaderText="Other Leave" />
                                                    <asp:BoundField DataField="LeaveWithoutPay" HeaderText="Abset Days" />
                                                    <asp:BoundField DataField="LeaveWithoutPay" HeaderText="Present Days" />
                                                    <asp:BoundField DataField="AllLeaveCount" HeaderText="Salaried Days" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <hr />
                                        <h4 style="text-align: center">------</h4>
                                        <hr />
                                        <br />



                                        <div class="" style="max-height: 500px; overflow: scroll">
                                            <asp:GridView ID="GridView2" runat="server" class="datatable table table-hover table-bordered"
                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Officer/Employee" />
                                                    <asp:BoundField DataField="TotalWorkingDays" HeaderText="Working Days" />

                                                    <asp:BoundField DataField="CasualLeaveCount" HeaderText="Casual Leave" />
                                                    <asp:BoundField DataField="EarnedLeaveCount" HeaderText="Earned Leave" />
                                                    <asp:BoundField DataField="MedicalLeaveCount" HeaderText="Medical Leave" />
                                                    <asp:BoundField DataField="OptionalLeaveCount" HeaderText="Optional Leave" />
                                                    <asp:BoundField DataField="OtherLeaveCount" HeaderText="Other Leave" />

                                                    <asp:BoundField DataField="PendingMedicalLeaveCount" HeaderText="Medical Leave (Pending)" />
                                                    <asp:BoundField DataField="PendingEarnedLeaveCount" HeaderText="Earned Leave (Pending)" />
                                                    <asp:BoundField DataField="PendingOptionalLeaveCount" HeaderText="Optional Leave (Pending)" />
                                                    <asp:BoundField DataField="PendingCasualLeaveCount" HeaderText="Casual Leave (Pending)" />
                                                    <asp:BoundField DataField="PendingOtherLeaveCount" HeaderText="Other Leave (Pending)" />
                                                    <asp:BoundField DataField="LeaveWithoutPay" HeaderText="LWP" />

                                                    <asp:BoundField DataField="AllLeaveCount" HeaderText="Total Approved Leave" />


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <hr />
                                        <h4 style="text-align: center">------</h4>
                                        <hr />
                                        <br />

                                        <div class="" style="max-height: 500px; overflow: scroll">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered"
                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="अधिकारी/कर्मचारी का नाम">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmp_Name" Text='<%# Eval("Emp_Name").ToString() %>' runat="server" />
                                                            <asp:HiddenField ID="HF_Emp_ID" Value='<%# Eval("Emp_ID").ToString() %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TotalWorkingDays" HeaderText="कुल दिन " />
                                                    <asp:BoundField DataField="PresentDays_Att_All" HeaderText="उपस्थित दिन " />
                                                    <asp:BoundField DataField="LeaveWithoutPay" HeaderText="LWP Days By Admin" />
                                                    <asp:BoundField DataField="AbsentDays" HeaderText="अनुपस्थित दिन " />
                                                    <asp:BoundField DataField="AbsentDates" HeaderText="अनुपस्थित दिनांक " />
                                                    <asp:TemplateField HeaderText="अवकाश दिनांक">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LeaveDate1" Text='<%# Eval("LeaveDates").ToString() %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>




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
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
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
                    title: $('.modal-title').text(),
                    exportOptions: {
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.modal-title').text(),
                    exportOptions: {
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
    </script>
    <script>
        function ModalAttendenceFun() {
            $("#ModalAttendence").modal('show');
        }
        $('#txtStartDate').datepicker({
            autoclose: true
        });
        $('#txtEndDate').datepicker({
            autoclose: true
        });
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtStartDate.ClientID%>').value.trim() == "") {
                msg += "Select Start Date. \n";
            }
            if (document.getElementById('<%=txtEndDate.ClientID%>').value.trim() == "") {
                msg += "Select End Date. \n";
            }
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg += "Select Office.\n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
        <%-- function checkChecks1() {
            var elementRef = document.getElementById('<%= Rbtn_Type2.ClientID %>');
            var inputElementArray = elementRef.getElementsByTagName('input');
            for (var i = 0; i < inputElementArray.length; i++) {
                var inputElement = inputElementArray[i];
                inputElement.checked = false;
            }
            return false;
        }
        function checkChecks2() {
            var elementRef = document.getElementById('<%= Rbtn_Type1.ClientID %>');
            var inputElementArray = elementRef.getElementsByTagName('input');
            for (var i = 0; i < inputElementArray.length; i++) {
                var inputElement = inputElementArray[i];
                inputElement.checked = false;
            }
            return false;
        }--%>

        
    </script>
    <script>
        $('#txtStartDate').change(function () {
            var start = $('#txtStartDate').datepicker('getDate');
            var end = $('#txtEndDate').datepicker('getDate');

            if ($('#txtEndDate').val() != "") {
                if (start > end) {

                    if ($('#txtStartDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtStartDate').val("");
                    }
                }
            }
        });
        $('#txtEndDate').change(function () {
            var start = $('#txtStartDate').datepicker('getDate');
            var end = $('#txtEndDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtEndDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtEndDate').val("");
                }
            }
        });
    </script>
</asp:Content>


