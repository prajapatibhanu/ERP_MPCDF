<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollArrearsDAOnly.aspx.cs" Inherits="mis_Payroll_PayrollArrearsDAOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Generate DA Arrears </h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lbldarate" runat="server" Text="DA %"></asp:Label><span style="color: red;">*</span>
                                <asp:TextBox ID="txtdarate" runat="server" CssClass="form-control" placeholder="Enter DA Percentage" onblur="return checkvalue(this);" onkeypress="return validateNum(event)" MaxLength="3"></asp:TextBox>
                                <small><span id="valtxtdarate" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <fieldset>
                                <legend>From Year & Month</legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <small><span id="valddlFYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Month<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFMonth" runat="server" class="form-control">
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
                                        <small><span id="valddlFMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                        <div class="col-md-5">
                            <fieldset>
                                <legend>To Year & Month</legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlTYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <small><span id="valddlTYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Month<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlTMonth" runat="server" class="form-control">
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
                                        <small><span id="valddlTMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <fieldset>
                                <legend>Order No & Date</legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Order No.</label>
                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" placeholder="Order No."></asp:TextBox>
                                        <small><span id="valtxtOrderNo" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Order Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control DateAdd" placeholder="Select Order Date" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            <small><span id="valtxtOrderDate" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-5">
                            <fieldset>
                                <legend>Year & Month</legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <small><span id="valddlYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Month<span style="color: red;">*</span></label>
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
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("Emp_ID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="ArrearMonth" runat="server" Text='<%# Eval("Emp_Name") %>' ToolTip='<%# Eval("Emp_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid BasicSalary">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBasicSalary" runat="server" Text='<%# Eval("BasicSalary") %>' CssClass="form-control" placeholder="Basic Salary" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Basic Salary In Arrear">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtArrearBasicSalary" runat="server" Text='<%# Eval("BasicPaidInArrear") %>' CssClass="form-control" placeholder="Arrears Basic Salary" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Basic Salary Paid">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalBasicSalary" runat="server" Text='<%# Eval("TotalBasicPaid") %>' CssClass="form-control" placeholder="Total Basic Paid" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid DA">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPaidDa" runat="server" Text='<%# Eval("PaidDa") %>' CssClass="form-control" placeholder="Paid Da" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DA To Be Paid(As Per New Rate)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDaToBePaid" runat="server" Text='<%# Eval("DaToBePaid") %>' CssClass="form-control" placeholder="DA To Be Paid" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DA Remaining Arrear Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDaRemainingArrearAmount" runat="server" Text='<%# Eval("DaRemainingArrearAmount") %>' CssClass="form-control txtDaRemainingArrearAmount" onkeypress="return validateNum(event)" placeholder="DaRemainingArrearAmount" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remaining EPF Amount">
                                            <ItemTemplate>
                                                <%--                                                <asp:TextBox ID="txtRemainingEpfAmount" runat="server" Text='<%# Eval("RemainingEpfAmount") %>' CssClass="form-control txtRemainingEpfAmount" placeholder="RemainingEpfAmount" onkeypress="return validateNum(event)" ReadOnly="true"></asp:TextBox>--%>
                                                <asp:Label ID="lblRemainingEpfAmount" runat="server" Text='<%# Eval("RemainingEpfAmount") %>' CssClass="form-control txtRemainingEpfAmount"></asp:Label>
                                                <asp:HiddenField ID="hfRemainingEpfAmount" runat="server" Value='<%# Eval("RemainingEpfAmount") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Payment">
                                            <ItemTemplate>
                                                <%--                                                <asp:TextBox ID="txtNetPayment" runat="server" Text='<%# Eval("NetPayment") %>' CssClass="form-control txtNetPayment" placeholder="NetPayment" onkeypress="return validateNum(event)" ReadOnly="true"></asp:TextBox>--%>
                                                <asp:Label ID="lblNetPayment" runat="server" Text='<%# Eval("NetPayment") %>' CssClass="form-control txtNetPayment"></asp:Label>

                                                <asp:HiddenField ID="hfNetPayment" runat="server" Value='<%# Eval("NetPayment") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Visible="false" Text="Save" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        $('#txtPolicy_StartDate').datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'MM yy',
            onClose: function (dateText, inst) {
                $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
            }
        });


        $('.txtDaRemainingArrearAmount').on('focusout', function () {
            var RemainingArrearAmount = $(this).val();
            //$(this).val(RemainingArrearAmount);
            //alert(RemainingArrearAmount);
            //var RemainingEpfAmount = $(this).parent().parent().find('.txtRemainingEpfAmount').text();
            //var NetPayment = $(this).parent().parent().find('.txtNetPayment').text();

            //RemainingEpfAmount = Math.round((RemainingArrearAmount * 0.12));
            //NetPayment = Math.round(RemainingArrearAmount) - Math.round(RemainingEpfAmount);
            var RemainingEpfAmount = parseInt((RemainingArrearAmount * 0.12));
            var NetPayment = (RemainingArrearAmount) - (RemainingEpfAmount);

            $(this).parent().parent().find('.txtRemainingEpfAmount').text(RemainingEpfAmount);
            $(this).parent().parent().find('#hfRemainingEpfAmount').val(RemainingEpfAmount);
            $(this).parent().parent().find('.txtNetPayment').text(NetPayment);
            $(this).parent().parent().find('#hfNetPayment').val(NetPayment);


            calculateTotal();

            //console.log($(this).val());
            //console.log($(this).parent().parent().find('.txtRemainingEpfAmount').val());
            //console.log($(this).parent().parent().find('.txtNetPayment').val());
        });
        function calculateTotal() {
            var dasum = 0;
            var epfsum = 0;
            var netsum = 0;

            $('.txtDaRemainingArrearAmount').each(function () {
                dasum += Number($(this).val());
            });

            $('.txtRemainingEpfAmount').each(function () {
                epfsum += Number($(this).text());
            });
            $('.txtNetPayment').each(function () {
                netsum += Number($(this).text());
            });


            $('.TotalDa').html(dasum);
            $('.TotalEpf').html(epfsum);
            $('.TotalNet').html(netsum);


        }


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

            if (document.getElementById('<%=ddlFYear.ClientID%>').selectedIndex == 0) {
                msg += "Select From Year. \n"
                $("#valddlFYear").html("Select From Year");
            }
            if (document.getElementById('<%=ddlTYear.ClientID%>').selectedIndex == 0) {
                msg += "Select To Year. \n"
                $("#valddlTYear").html("Select To Year");
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg += "Select Year Of Payment. \n"
                $("#valddlYear").html("Select Year Of Payment");
            }

            if (document.getElementById('<%=ddlFMonth.ClientID%>').selectedIndex == 0) {
                msg += "Select From Month. \n"
                $("#valddlFMonth").html("Select From Month");
            }
            if (document.getElementById('<%=ddlTMonth.ClientID%>').selectedIndex == 0) {
                msg += "Select To Month. \n"
                $("#valddlTMonth").html("Select To Month");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg += "Select Month Of Payment. \n"
                $("#valddlMonth").html("Select Month Of Payment");
            }


            if (document.getElementById('<%=txtdarate.ClientID%>').value.trim() == "") {
                msg += "Select Date \n"
                $("#valtxtdarate").html("Enter Rate Of DA To Be Paid");
            }

            if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg += "Select Date \n"
                $("#valtxtOrderNo").html("Enter Order No");
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "Select Date \n"
                $("#valtxtOrderDate").html("Select Date");
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
</asp:Content>

