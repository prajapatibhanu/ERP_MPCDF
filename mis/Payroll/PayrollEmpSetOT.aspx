<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEmpSetOT.aspx.cs" Inherits="mis_Payroll_PayrollEmpSetOT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title print_hidden">Employee Overtime </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" CssClass="print_hidden" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row print_hidden">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" Enabled="false">
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
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>


                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <div>
                                    <table cellspacing="0" rules="all" class="table table-hover table-bordered table-striped pagination-ys " border="1" id="GridView1" style="border-collapse: collapse;">
                                        <tbody>
                                            <tr>
                                                <th>
                                                    <input id="chkSelect" type="checkbox">
                                                </th>
                                                <th scope="col">SNo.</th>
                                                <th scope="col">Employee</th>
                                                <th scope="col">BasicSalary</th>
                                                <th scope="col">1 Hr OT Salary</th>
                                                <th scope="col">Total OT in Hr</th>
                                                <th scope="col">Total OT Amount</th>
                                                <th scope="col">Net Payment</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="chkSelect" type="checkbox">
                                                </td>
                                                <td style="width: 5%;">
                                                    <span id="lblRowNumber" title="1">1</span>
                                                </td>
                                                <td>
                                                    <span id="ArrearMonth" title="2019">RAVINDRA PRATAP SINGH TIWARI</span>
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl02$txtBasicSalary" type="text" value="0.00" id="txtBasicSalary" class="form-control" placeholder="Basic Salary">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl02$txtArrearBasicSalary" type="text" value="0.00" id="txtArrearBasicSalary" class="form-control" placeholder="Arrears Basic Salary">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl02$txtTotalBasicSalary" type="text" value="0.00"  id="txtTotalBasicSalary" class="form-control" placeholder="Total Basic Paid">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl02$txtPaidDa" type="text" value="0.00" id="txtPaidDa" class="form-control" placeholder="Paid Da">
                                                </td>
                                                <td>
                                                    <span id="lblNetPayment" class="form-control txtNetPayment">0.00</span>
                                                    <input type="hidden" name="ctl00$ContentBody$GridView1$ctl02$hfNetPayment" id="hfNetPayment" value="0.00">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="chkSelect" type="checkbox">
                                                </td>
                                                <td style="width: 5%;">
                                                    <span id="lblRowNumber" title="2">2</span>
                                                </td>
                                                <td>
                                                    <span id="ArrearMonth" title="2019">BHAGYASHRI BAJAJ</span>
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl03$txtBasicSalary" type="text" value="0.00"  id="txtBasicSalary" class="form-control" placeholder="Basic Salary">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl03$txtArrearBasicSalary" type="text" value="0.00"  id="txtArrearBasicSalary" class="form-control" placeholder="Arrears Basic Salary">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl03$txtTotalBasicSalary" type="text" value="0.00"  id="txtTotalBasicSalary" class="form-control" placeholder="Total Basic Paid">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl03$txtPaidDa" type="text" value="0.00"  id="txtPaidDa" class="form-control" placeholder="Paid Da">
                                                </td>
          
                                                <td>
                                                    <span id="lblNetPayment" class="form-control txtNetPayment">0.00</span>
                                                    <input type="hidden" name="ctl00$ContentBody$GridView1$ctl03$hfNetPayment" id="hfNetPayment" value="0.00">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="chkSelect" type="checkbox">
                                                </td>
                                                <td style="width: 5%;">
                                                    <span id="lblRowNumber" title="3">3</span>
                                                </td>
                                                <td>
                                                    <span id="ArrearMonth" title="2019">SANDEEP SHARNAGAT</span>
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl04$txtBasicSalary" type="text" value="0.00" id="txtBasicSalary" class="form-control" placeholder="Basic Salary">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl04$txtArrearBasicSalary" type="text" value="0.00" id="txtArrearBasicSalary" class="form-control" placeholder="Arrears Basic Salary">
                                                </td>
                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl04$txtTotalBasicSalary" type="text" value="0.00" id="txtTotalBasicSalary" class="form-control" placeholder="Total Basic Paid">
                                                </td>

                                                <td>
                                                    <input name="ctl00$ContentBody$GridView1$ctl04$txtDaToBePaid" type="text" value="0.00" id="txtDaToBePaid" class="form-control" placeholder="DA To Be Paid">
                                                </td>
                                                <td>
                                                    <span id="lblNetPayment" class="form-control txtNetPayment">0.00</span>
                                                    <input type="hidden" name="ctl00$ContentBody$GridView1$ctl04$hfNetPayment" id="hfNetPayment" value="0.00">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td align="right">Total</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td class="TotalNet">0.00</td>
                                                <td class="TotalNet">0.00</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <input type="submit" name="ctl00$ContentBody$btnSave" value="Save" id="ctl00_ContentBody_btnSave" class="btn btn-success btn-block">
                            </div>
                        </div>
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



        



        //$(document).ready(function () {
        //    $('<thead></thead>').prependTo('#GridView1').append($('#GridView1 tr:first'));
        //    $('#GridView1 thead tr th:first').css('width', '36px !important');
        //});


        window.onload =
          $('select').each(function () {
              var str = $(this).val();
              if (str == 'A') {
                  $(this).css('color', 'red');
              }
              else if (str == 'H') {
                  $(this).css('color', 'blue');
                  $(this).css('border', '1px solid green;');
              }


          });
        <%--var d = new Date();
        var month = $('#<%= ddlMonth.ClientID %>').find("option:selected").val();
        var mm = month;
        month = month - 1;
        var year = $('#<%= ddlYear.ClientID %>').find("option:selected").val();
        var getTot = daysInMonth(mm, year);
        var getLastMonthTotDays = 0;
        var LastMonth = 0;
        var LastYear = 0
        //debugger;
        if (month == 0) {
            LastMonth = 12;
            LastYear = year - 1;
        }
        else {
            LastMonth = month - 1;
            LastYear = year;
        }
        getLastMonthTotDays = daysInMonth(LastMonth, LastYear);
        if (LastMonth == 12)
            LastMonth = 11;
        // console.log(getLastMonthTotDays);
        //  console.log(daysInMonth(d.getMonth(), d.getFullYear()))
        var sat = new Array();
        var sun = new Array();

        // Current Month
        for (var i = 1; i <= getLastMonthTotDays; i++) {
            if (i <= 20) {
                //debugger;
                var newDate = new Date(year, month, i)
                console.log(i + "-" + newDate.getDay());
                if (newDate.getDay() == 0) {
                    sat.push(i)
                }
                if (newDate.getDay() == 6) {
                    sun.push(i)
                }
            }
            else {
                //debugger;

                var newDateLastMonth = new Date(LastYear, LastMonth, i)
                console.log(i + "-" + newDateLastMonth.getDay());
                if (newDateLastMonth.getDay() == 0) {
                    sat.push(i)
                }
                if (newDateLastMonth.getDay() == 6) {
                    sun.push(i)
                }
            }

        }--%>
        // Last Month
        //for (var i = 21; i <= getLastMonthTotDays; i++) {

        //}
        //var max = sat.length;
        //var x = 0;
        //for (x = 0; x < max; x++) {
        //    var t = sat[x];
        //    t = '.' + t;
        //    $(t).each(function () {
        //        $(this).parent().html('<div style="width:8px; text-align:center;"><label style="text-align:center;color:red;font-weight:bold;font-size:12px;margin-bottom:0px;">S<label></div>');

        //    });
        //}
        //function daysInMonth(month, year) {

        //    return new Date(year, month, 0).getDate();
        //}

        function change(ddl) {
            //debugger;
            var str = ddl.selectedIndex;
            if (str == 1) {
                ddl.style.color = "red";
                ddl.style.border = "1px solid red;";
            }
            else if (str == 2) {
                ddl.style.color = "blue";
                ddl.style.border = "1px solid blue;";
            }
            else {
                ddl.style.color = "black";
                ddl.style.border = "1px solid black;";
                // border: 1px solid green;
            }

        }

        function validateform() {
            var msg = "";

            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
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
    <script>
        function myFunction() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("GridView1");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</asp:Content>



