<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollTaxAssesmentForm.aspx.cs" Inherits="mis_Payroll_PayrollTaxAssesmentForm" %>

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
                    <h3 class="box-title print_hidden">Income Tax Assessment Form</h3>
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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform1();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>

                    <div class="row">
                        <div class="col-md-12 table-responsive" style="max-height: 400px;">
                            <asp:Label ID="lblEmpDetail" class="lblEmpDetail" runat="server" Text="" Visible="true" Style="color: black; font-size: 17px;"></asp:Label>
                            <p class="hidden-print" style="color: blue"><b>नोट: </b>प्रिंट करने के लिए कृपया Ctrl+P का उपयोग करें |</p>
                            <asp:GridView ID="GridView1" DataKeyNames="EarnDeduction_ID" runat="server" AutoGenerateColumns="False" class="datatable table table-hover table-bordered pagination-ys">
                                <Columns>
                                    <%--<asp:BoundField DataField="EarnDeduction_Name" HeaderText="Head" />--%>
                                    <asp:TemplateField HeaderText="Head" ItemStyle-CssClass="alignR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetsalary" CssClass='<%# Eval("EarnDeduction_Type")%>' runat="server" Text='<%# Eval("EarnDeduction_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EarnDeduction_Type" HeaderText="Earning & Deduction" />

                                    <asp:BoundField DataField="Total" HeaderText="Total In FY" />

                                    <asp:BoundField DataField="April" HeaderText="April" />
                                    <asp:BoundField DataField="AprilArr" HeaderText="April Arrear" />

                                    <asp:BoundField DataField="May" HeaderText="May" />
                                    <asp:BoundField DataField="MayArr" HeaderText="May Arrear" />

                                    <asp:BoundField DataField="June" HeaderText="June" />
                                    <asp:BoundField DataField="JuneArr" HeaderText="June Arrear" />

                                    <asp:BoundField DataField="July" HeaderText="July" />
                                    <asp:BoundField DataField="JulyArr" HeaderText="July Arrear" />

                                    <asp:BoundField DataField="August" HeaderText="August" />
                                    <asp:BoundField DataField="AugustArr" HeaderText="August Arrear" />

                                    <asp:BoundField DataField="September" HeaderText="September" />
                                    <asp:BoundField DataField="SeptemberArr" HeaderText="September Arrear" />

                                    <asp:BoundField DataField="October" HeaderText="October" />
                                    <asp:BoundField DataField="OctoberArr" HeaderText="October Arrear" />

                                    <asp:BoundField DataField="November" HeaderText="November" />
                                    <asp:BoundField DataField="NovemberArr" HeaderText="November Arrear" />

                                    <asp:BoundField DataField="December" HeaderText="December" />
                                    <asp:BoundField DataField="DecemberArr" HeaderText="December Arrear" />

                                    <asp:BoundField DataField="January" HeaderText="January" />
                                    <asp:BoundField DataField="JanuaryArr" HeaderText="January Arrear" />

                                    <asp:BoundField DataField="February" HeaderText="February" />
                                    <asp:BoundField DataField="FebruaryArr" HeaderText="February Arrear" />

                                    <asp:BoundField DataField="March" HeaderText="March" />
                                    <asp:BoundField DataField="MarchArr" HeaderText="March Arrear" />


                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="row" id="dvAssesmentForm" runat="server" visible="false">
                        <div class="col-md-12">
                            <table class="table">
                                <tbody>
                                    <tr>
<%--                                        <th>
                                            <img src="../image/mpagro-logo.png" class="salary-logo" /></th>
                                        <th colspan="5">
                                            <p style="text-align: center">
                                                THE MADHYA PRADESH STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LIMITED<br>
                                                "PANCHANAN" 3rd FLOOR,MALAVIYA NAGAR BHOPAL<br>
                                                Phone(0755) - 2552652,2551756,2551807 Fax: 0755-2557305   
                                            </p>
                                        </th>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                             Tax Assesment Form
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Name</th>
                                        <td>
                                            <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label></td>
                                        <th>Place Of Posting</th>
                                        <td>
                                            <asp:Label ID="lblPostingPlace" runat="server" Text=""></asp:Label></td>
                                        <th>Designation</th>
                                        <td>
                                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>Pan No</th>
                                        <td>
                                            <asp:Label ID="lblPanNo" runat="server" Text=""></asp:Label></td>
                                        <th>Accounting Year</th>
                                        <td>
                                            <asp:Label ID="lblAccountingYear" runat="server" Text=""></asp:Label></td>
                                        <th>Assessment Year</th>
                                        <td>
                                            <asp:Label ID="lblAssessmentYear" runat="server" Text=""></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td colspan="6">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="6">COMPUTATION OF TAXABLE INCOME  <small style="color: teal">&nbsp;&nbsp; (
                                            <asp:Label ID="lblEmployeeType" runat="server" Text=""></asp:Label><asp:HiddenField ID="hfEmployeeType" runat="server" />
                                            )</small></th>
                                    </tr>
                                    <tr>
                                        <td colspan="6"></td>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td colspan="4">Total gross pay(Including arrear)</td>
                                        <td>
                                            <asp:Label ID="lblGrossSalary" CssClass="lblGrossSalary" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td colspan="4">Allowable deduction 150000 or Less (Under section section 80C</td>
                                        <td>
                                            <asp:TextBox ID="txtbox80c" CssClass="textarea txtbox80c" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td colspan="4">Standard deduction (50000)</td>
                                        <td>
                                            <asp:Label ID="lblStandardDeduction" CssClass="lblStandardDeduction" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td colspan="4">Any other dedcution as per IT Act(If applicable  80D/Under Section 24/80DD</td>
                                        <td>
                                           <%-- <asp:TextBox ID="txtbox80g" CssClass="textarea txtbox80g" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:TextBox>--%>
                                            <asp:Label ID="lblbox80g" CssClass="lblbox80g" runat="server" Text=""></asp:Label>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">a) Interest on home loan (Section 24)</td>
                                        <td>
                                            <asp:TextBox ID="txtbox24" CssClass="textarea txtbox24" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:TextBox></td>
                                    </tr>
                                    
                                    <tr>
                                        <td></td>
                                        <td colspan="3">b) Mediclaim (Section 80D)</td>
                                        <td>
                                            <asp:TextBox ID="txtbox80d" CssClass="textarea txtbox80d" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">c) Disabled dependant (Section 80DD)</td>
                                        <td>
                                            <asp:TextBox ID="txtbox80dd" CssClass="textarea txtbox80dd" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:TextBox></td>
                                    </tr>


                                    <%--                                    <tr>
                                        <td></td>
                                        <td>Remark:</td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtbox80greason" CssClass="textarea" TextMode="multiline" Columns="100" Rows="6" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>5</td>
                                        <td colspan="4">Professional tax</td>
                                        <td>
                                            <asp:Label ID="lblProfessionalTax" CssClass="lblProfessionalTax" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>6</td>
                                        <td colspan="4">Taxable income</td>
                                        <td>
                                            <asp:Label ID="lblTaxableIncome" CssClass="lblTaxableIncome" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>7</td>
                                        <td colspan="4">Total payable tax</td>
                                        <td>
                                            <asp:Label ID="lblTotalPayableTax" CssClass="lblTotalPayableTax" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"><b>Note:  </b>(i) Employees claiming exemption for H.R.A. have to enclose original receipt of rent paid to the owner for evidence.</td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"><b>DECLARATION:-  </b>I declare that the information given as above is based on my best possible estimate and proposed investments payments/ deposits will be made by end of financial year.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            Dated: </td>
                                        <td colspan="4">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                        <td>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <b>SIGNATURE</b></td>
                                    </tr>

                                </tbody>
                            </table>
                            <div class="col-md-4 print_hidden"></div>
                            <div class="col-md-2 print_hidden">
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-success btn-block Aselect1" runat="server" Text="Calculate Payable Tax" />
                            </div>
                            <div class="col-md-2 print_hidden">
                                <asp:Button ID="btnPrint" CssClass="btn btn-primary btn-block" runat="server" Text="Print" OnClientClick="printpage();" />
                            </div>
                            <div class="col-md-4 print_hidden"></div>
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
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to see details ?")) {
                    return true;
                }
                else {
                    return false;
                }

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


