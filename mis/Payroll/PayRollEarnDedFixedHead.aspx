<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollEarnDedFixedHead.aspx.cs" Inherits="mis_Payroll_PayRollEarnDedFixedHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
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

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements  -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label1">Fixed Master Data in Payroll Heads </h3>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice_Name" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlOffice_Name_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlOffice_Name" class="text-danger"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Earning & Deduction Head<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEarnDeducHead" runat="server" class="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlEarnDeducHead" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClientClick="return validateform();" OnClick="btnShow_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <p style="color: #123456; font-size: 15px;" runat="server">
                                        <asp:Label ID="lblDeductionDetails" CssClass="lblDeductionDetails" runat="server" Text=""></asp:Label>
                                    </p>
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
                                                <asp:TemplateField HeaderText="Emploee Name" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EmpName" runat="server" Text='<%# Eval("Emp_Name") %>' ToolTip='<%# Eval("Emp_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation" ControlStyle-Font-Size="Smaller">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EmpDesignation" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount") %>' CssClass="form-control txtDaRemainingArrearAmount" onkeypress="return validateNum(event)" placeholder="DaRemainingArrearAmount" onpaste="return false;"></asp:TextBox>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


    <script>
        $("#txtmonth").datepicker({
            format: "MM",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true
        });
        function validateform() {
            var msg = "";
            $("#valddlOffice_Name").html("");
            $("#valddlFinancialYear").html("");
            $("#valtxtmonth").html("");
            if (document.getElementById('<%=ddlEarnDeducHead.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Earning Deduction Head \n";
                $("#valtxtmonth").html("Select Earning Deduction Head.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
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
                $(checkbox[i]).parents('tr').css('background-color', '#f7efe2');
                //    $(checkbox[i]).parents('tr').css('color', '#FFFFFF');

                //$('table tbody input[type="checkbox"]').css('width', '25px');
            }



        });



        $('.txtDaRemainingArrearAmount').on('focusout', function () {
            var RemainingArrearAmount = $(this).val();
            calculateTotal();
        });
        function calculateTotal() {
            var dasum = 0;
            $('.txtDaRemainingArrearAmount').each(function () {
                dasum += Number($(this).val());
            });

            $('.TotalDa').html(dasum);
        }


        //$('.txtDaRemainingArrearAmount').each(function () {
        //    var val1 = parseInt($(this).val());
        //    var val2 = parseInt($(this).parent().parent().find('.calculatedHead').text());

        //    if (val1 != val2) {
        //        $(this).parent().parent().css({
        //            'color': 'blue',
        //            'font-weight':'600'
        //        });
        //        console.log(val1 + " ~ " + val2);
        //    }
            
        //});
        
        //txtDaRemainingArrearAmount
        //calculatedHead

    </script>
</asp:Content>

