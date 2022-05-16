<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalarySlip.aspx.cs" Inherits="mis_Payroll_SalarySlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <style>
        .box {
            position: relative;
            border-radius: 3px;
            background: #ffffff;
            border-top: 3px solid #d2d6de;
            margin-bottom: 20px;
            width: 100%;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            box-shadow: none;
            border-top: none;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e1e1e1;
        }

        .text-center h3 {
            font-size: 15px;
            font-family: verdana;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 0px 2px;
        }

        #subheading-salary {
            font-size: 13px;
        }

        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 100px;
        }

        .printbutton {
            border-top: 1px dashed #838383;
            margin-top: 5px;
            padding-top: 5px;
        }

        table h4 {
            font-size: 15px;
        }

        .table {
            margin-bottom: 5px;
        }

        th, td, h3 {
            text-transform: uppercase !important;
        }

        /*.watermark {
            width: 300px;
            height: 100px;
            display: block;
            position: relative;
        }

            .watermark::after {
                content: "";
                background: url('../../mis/image/sanchi_logo_blue.png');
                opacity: 0.2;
                top: 0;
                left: 0;
                bottom: 0;
                right: 0;
                position: absolute;
                z-index: -1;
            }*/

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            
            border: 1px solid #999 !important;
        }

        .table-bordered th {
            border-bottom-width: 2px !important;
            background-color: none !important;
            color: #333 !important;
        }

        th {
            background: white !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="content-wrapper">
                <section class="content watermark" id="DivSlip" runat="server" style="padding-top: 0px; height: 60px;">
                    <!-- Default box -->
                    <div class="box box-default">
                        <div class="box-header text-center">
<%--                            <h3 class="">
                                <img src="../../mis/image/sanchi_logo_blue.png" class="salary-logo">
                                <br />
                                MP STATE CO OPERATIVE DAIRY FEDERATION<br />
                                <span id="subheading-salary" class="subheading-salary">PAY SLIP FOR THE MONTH OF <span id="lblMonth" runat="server"></span>&nbsp; <span id="lblFinancialYear" runat="server"></span>&nbsp; <span style="color: red;" id="lblGenStatus" runat="server"></span></span></h3>--%>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="text-align: center;" class="printbutton">
                                        <input type="button" class="btn btn-primary" value="Print" onclick="window.print()">
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </form>
</body>
</html>
