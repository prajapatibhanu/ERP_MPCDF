<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollGenerationCheckList.aspx.cs" Inherits="mis_Payroll_PayrollGenerationCheckList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 46px;
            float: right;
        }

        @media print {
            .print_hidden {
                display: none !important;
            }

            .box-body, .box, tbody {
                height: auto !important;
            }

                .box.box-success {
                    border: none;
                }


            .headingOfDept {
                display: block !important;
            }

            .salary-logo {
                -webkit-filter: grayscale(100%);
                filter: grayscale(100%);
                width: 40px;
            }

            .lblEmpDetail {
                font-size: 14px !important;
            }

            .main-footer {
                display: none;
            }

            .textarea {
                border: none;
            }
        }

        .textarea {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title print_hidden">Payroll Generation (Check List)   { <span style="color: #ffe81f; font-size: 13px;">Data to be captured from HR Department. </span>}</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row print_hidden">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Payroll Will Be Created For Year<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Payroll Will Be Created For Month.<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform1();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>


                    <div class="row" id="dvAssesmentForm" runat="server" visible="false">
                        <div class="col-md-12">
                            <table class="table table-bordered" style="font-size: 14px;">
                                <tbody>
                                    <tr>
                                        <th style="width: 30px;">SNo.</th>
                                        <th style="width: 400px;">Milestone</th>
                                        <th style="width: 100px;">Details(No's)</th>
                                        <th style="width: 500px;">Remarks</th>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td>पिछले माह कुल कितने अधिकारीयों / कर्मचारियों का वेतन पत्रक जनरेट किया गया |</td>
                                        <td>
                                            <asp:TextBox ID="txtLastMonthEmpCount" runat="server" CssClass="form-control" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLastMonthEmpRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>इस माह कुल कितने अधिकारीयों / कर्मचारियों का वेतन पत्रक जनरेट किया जा रहा है |</td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentMonthEmpCount" onkeypress="return validateNum(event)" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentMonthEmpRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="font-weight: bold;">कुल अंतर </td>
                                        <td>
                                            <asp:Label ID="lblEmpDifference" runat="server" Text=""></asp:Label></td>
                                        <td></td>
                                    </tr>


                                    <tr>
                                        <th colspan="4">यदि अंतर है तो  </th>
                                    </tr>

                                    <tr>
                                        <td>3</td>
                                        <td>इस माह अन्य स्थान से ट्रान्सफर होकर आए अधिकारी / कर्मचारी  </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpTransferIn" runat="server" onkeypress="return validateNum(event)" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpTransferInRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>4</td>
                                        <td>इस माह इस कार्यालय से अन्य स्थान पर ट्रान्सफर होकर गए अधिकारी / कर्मचारी  </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpTransferOut" runat="server" onkeypress="return validateNum(event)" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpTransferOutRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>5</td>
                                        <td>इस माह रिटायर हुए अधिकारी / कर्मचारी </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpRetire" runat="server" onkeypress="return validateNum(event)" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpRetireRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <th colspan="4">अन्य जानकारी  </th>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td>किसी अधिकारी / कर्मचारी का बेसिक वेतन में बदलाव </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpChangedBasic" onkeypress="return validateNum(event)" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpChangedBasicRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>7</td>
                                        <td>किसी अधिकारी / कर्मचारी का अन्य कोई कतौत्रा ? ( हेड वार जानकारी देवें )</td>
                                        <td>
                                            <asp:TextBox ID="txtEmpDeduction" onkeypress="return validateNum(event)" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpDeductionRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>8</td>
                                        <td>किसी अधिकारी / कर्मचारी का अन्य कोई भुगतान ? ( हेड वार जानकारी देवें )</td>
                                        <td>
                                            <asp:TextBox ID="txtEmpEarning" onkeypress="return validateNum(event)" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtEmpEarningRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>

                                </tbody>
                            </table>
                            <div class="col-md-2 print_hidden">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-success btn-block" runat="server" Text="Save Checklist" />
                            </div>
                            <div class="col-md-8 print_hidden"></div>

<%--                            <div class="col-md-2 print_hidden">
                                <asp:Button ID="btnPrint" CssClass="btn btn-primary btn-block" runat="server" Text="Print" OnClientClick="printpage();" />
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">


        function validateform1() {
            var msg = "";
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                //if (confirm("Do you really want to see details ?")) {
                //    return true;
                //}
                //else {
                //    return false;
                //}
                return true;

            }
        }

        function printpage() {
            print();
        }


        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }



        $('.txtbox80c,.txtbox80dd,.txtbox80d,.txtbox24').on('focusout', function () {
            calculateTotal();
        });

        function calculateTotal() {
            var lblTaxableIncome = 0;
            var lblbox80g = 0;
            //var lblTotalPayableTax = 0;

            lblTaxableIncome = (Number($(".lblGrossSalary").text()) - Number(Number($(".lblProfessionalTax").text()) + (Number($(".lblStandardDeduction").text()) + (Number($(".txtbox80c").val()) + (Number($(".txtbox80g").text()))))));

            lblbox80g = ((Number($(".txtbox80dd").val()) + (Number($(".txtbox80d").val()) + (Number($(".txtbox24").val())))));

            /****************/
            $('.lblTaxableIncome').text(lblTaxableIncome.toFixed(2));
            $('.lblbox80g').text(lblbox80g.toFixed(2));
            //$('.lblTotalPayableTax').val(lblTotalPayableTax.toFixed(2));

        }


    </script>
    <link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            ordering: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }
            ],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<""tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
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


        //$(".Earning").parent().parent().css("color", "green");
        //$(".Deduction").parent().parent().css("color", "red");
        $(".Deduction,.Earning").css("color", "black");
        $(".Deduction,.Earning").parent().css("background", "#eaeaea");
    </script>
</asp:Content>


