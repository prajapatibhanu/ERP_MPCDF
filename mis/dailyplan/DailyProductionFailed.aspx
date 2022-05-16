<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyProductionFailed.aspx.cs" Inherits="mis_dailyplan_DailyProductionFailed" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Failed Sample</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <span id="ctl00_ContentBody_lblMsg"></span>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                    <small><span id="valOrderDate" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                <small><span id="valShift" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Product Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlProductSection" class="text-danger"></span></small>
                            </div>
                        </div>

                        <%--                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Batch No<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtBatchNo" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valBatchNo" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Serial/Lot No<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtLotNo" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valLotNo" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Remark </label>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>--%>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Search" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <h4 class="text-bold text-center"><u>Sample Failed On  <span style="color: orange; font-weight: bold">
                                    <asp:Label ID="lblSelectedDate" runat="server" Text=""></asp:Label></span></u></h4>


                                <div class="table table-responsive" id="table" runat="server">
                                    <div>
                                        <table cellspacing="0" rules="all" class="table table-hover table-bordered table-striped pagination-ys " border="1" id="GridView2" style="border-collapse: collapse;">
                                            <tbody>
                                                <tr>
                                                    <th scope="col">SNo.</th>
                                                    <th scope="col">Production Date</th>
                                                    <th scope="col">Shift</th>
                                                    <th scope="col">Particular</th>
                                                    <th scope="col">Production Quantity (Packet)</th>
                                                    <th scope="col">Batch No.</th>
                                                    <th scope="col">Lot No.</th>
                                                    <th scope="col">Total FAT (Gram)</th>
                                                    <th scope="col">Total SNF (Gram)</th>
                                                    <th scope="col">QC Sample Quantity(Packet)</th>
                                                    <th scope="col">QC Sample No</th>
                                                    <th scope="col">Result</th>
                                                    <th scope="col">Test Remark (Test No.)</th>
                                                    <th scope="col">Action</th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 5%;">
                                                        <span id="lblRowNumber">1</span>
                                                    </td>
                                                    <td>28/05/2020</td>
                                                    <td>Morning</td>
                                                    <td>
                                                        <span id="ItemName" title="45">Sanchi Gold 500 Ml</span>
                                                    </td>
                                                    <td>150</td>
                                                    <td>ABCD</td>
                                                    <td>l-233</td>
                                                    <td>4500</td>
                                                    <td>6750</td>
                                                    <td>1</td>
                                                    <td>333eere</td>
                                                    <td>Fail</td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <a onclick="return getConfirmation();" id="Supplement" class="label label-warning" href="DailyProductionReprocess.aspx">Reprocess</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>


                                <div class="table table-responsive">
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

        function getConfirmation() {
            debugger;
            var retVal = confirm("Are you sure want to continue?");
            if (retVal == true) {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
            else {

                return false;
            }
        }

        function validateform() {
            debugger;
            var msg = "";
            $("#valddlDS").html("");
            $("#valddlProductSection").html("");
            if (document.getElementById('<%=ddlDS.ClientID%>').selectedIndex == 0) {
                msg += "Select Dugdh Sangh. \n"
                $("#valddlDS").html("Select Dugdh Sangh");
            }
            if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
                $("#valddlProductSection").html("Select Product Section");
            }
            if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Select Shift. \n"
                $("#valShift").html("Select Shift");
            }
<%--            if (document.getElementById('<%=txtBatchNo.ClientID%>').value.trim() == "") {
                msg += "Enter Batch No. \n"
                $("#valBatchNo").html("Enter Batch No.");
            }
            if (document.getElementById('<%=txtLotNo.ClientID%>').value.trim() == "") {
                msg += "Enter Lot No. \n"
                $("#valLotNo").html("Enter Lot No.");
            }--%>
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "Select Date \n"
                $("#valOrderDate").html("Select Date");
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

