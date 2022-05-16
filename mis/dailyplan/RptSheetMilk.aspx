<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptSheetMilk.aspx.cs" Inherits="mis_dailyplan_RptSheetMilk" %>

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

            .footerprint h5 {
                font-weight: 600;
            }

            table {
                border: 1px solid #dcdcdc !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header print_hidden">
                    <h3 class="box-title">Milk Sheet</h3>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static">

                                    <asp:ListItem Value="">Morning</asp:ListItem>
                                    <asp:ListItem>Evening</asp:ListItem>
                                    <asp:ListItem>Night</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">



                        <div class="row">
                            <div class="col-md-12 text-center">
                                <div class="col-md-3 col-sm-3 col-xs-3">
                                </div>
                                <div class="col-md-6  col-sm-6 col-xs-6">
                                    <h4>BHOPAL SAHAKARI DUGDH SANGH MARYADIT</h4>
                                    <h5>BHOPAL DAIRY PLANT HABIBGANJ - BHOPAL</h5>
                                    <h5>DAILY DISPOSAL SHEET</h5>
                                </div>
                                <div class="col-md-3  col-sm-3 col-xs-3">
                                    <h5>Date:..................Shift:................</h5>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <table class="table table-bordered" style="font-size: 12px">
                                    <tr>
                                        <td rowspan="2">DM No.</td>
                                        <td>FCM 1/2</td>
                                        <td>CHAH 1</td>
                                        <td>STD 1/2</td>
                                        <td>HTM 1/2</td>
                                        <td>HDTM 1/2</td>
                                        <td>HDTM 1/5</td>
                                        <td>LITE</td>
                                        <td>COW</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">WHOLE MILK</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>To Prod:</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>HTM</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>HDTM</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Receipt:</td>
                                            <td></td>
                                            <td>FCM</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Good</td>
                                            <td></td>
                                            <td>CHAH</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Sour</td>
                                            <td></td>
                                            <td>STD Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Curdled</td>
                                            <td></td>
                                            <td>Separation</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>HDTM</td>
                                            <td></td>
                                            <td>Good</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Skim Milk</td>
                                            <td></td>
                                            <td>Sour</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Chaha</td>
                                            <td></td>
                                            <td>Curdled</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>FCM</td>
                                            <td></td>
                                            <td>Issued To Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>HTM</td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>COW</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>CR</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>W/B</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Flushing</td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">FCM</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">HDTM</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">STD</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">HTM</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">SKIMMED MILK</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">CHAHA MILK</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">LITE</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->
                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">
                                        <tr class="text-center">
                                            <th colspan="4">COW</th>
                                        </tr>
                                        <tr>
                                            <th>Receipt</th>
                                            <th>Qty.</th>
                                            <th>Issued</th>
                                            <th>Qty.</th>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td></td>
                                            <td>Issued For Sale</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Prepared</td>
                                            <td></td>
                                            <td>Issued For Whole Milk</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Return</td>
                                            <td></td>
                                            <td>Issued For Product Section</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Losses</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>Closing Balance</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td>Total</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-------------->

                            <div class="col-md-12">
                                <div>
                                    <table class="table table-bordered">

                                        <tr>
                                            <th>Particular</th>
                                            <th>B.F.</th>
                                            <th>Obtained</th>
                                            <th>Total</th>
                                            <th>Toning</th>
                                            <th>Maintaining S.N.F.</th>
                                            <th>Issued For Product Section</th>
                                            <th>Total Issued</th>
                                            <th>Closing Balance</th>
                                        </tr>
                                        <tr>
                                            <th>S.M.P.</th>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <th>Ghee</th>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <th>Cream</th>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <th>White Butter</th>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                            <!-------------->
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

    <script src="../HR/js/jquery.dataTables.min.js"></script>
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
    </script>
</asp:Content>

