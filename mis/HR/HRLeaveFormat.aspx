<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRLeaveFormat.aspx.cs" Inherits="mis_HR_HRLeaveFormat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .mainbody {
            width: 700px;
            margin: 10px auto;
            font-size: 15px;
            font-family: verdana;
        }

        span.Head1 {
            font-size: 20px;
        }

        span {
            display: block;
        }

        .date_cl {
            width: 46%;
            display: inline-block;
        }

        .sign_cl {
            width: 50%;
            display: inline-block;
            float: right;
            text-align: right;
        }

        .main-heading {
            text-align: center;
        }

        span.Head2 {
            display: block;
            margin-top: -15px;
            font-size: 16px;
        }

        span.confirmation {
            font-size: 15px;
            margin-bottom: 11px;
            line-height: 23px;
        }

        table, th, td {
            border: 1px solid #d2d2d2;
            border-collapse: collapse;
        }

        th, td {
            padding: 5px;
        }

        span#lblEmpName, span#lblReason {
            display: inline-block;
        }

        span#lblOffice {
            font-size: 18px;
        }

        span.AdrCSS {
            font-size: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="mainbody2" id="mainbody2" runat="server">
                <p style="color: red; margin: 20px auto; text-align: center; font-size: 20px;">Print is not available for this leave.. !!</p>
            </div>
            <div class="mainbody" id="mainbody" runat="server">
                <div>
                    <div>
                        <div class="main-heading">
                            <%-- <span class="Head1">M.P. STATE CO-OPERATIVE DAIRY FEDERATION LTD. </span>
                            <br />
                            <span class="Head2">DUGDH BHAWAN, DUGDH MARG, HABIBGANJ, BHOPAL</span>--%>
                            <span>
                                <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label></span>
                        </div>
                    </div>
                    <div class="table" style="margin: 10px auto;">
                        <table class="table" style="width: 100%">
                            <tr style="text-align: left;">
                                <th colspan="5">Name of the employee:  
                                    <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>
                                </th>
                            </tr>
                            <tr style="text-align: left;">
                                <th>Designation</th>
                                <td colspan="2">
                                    <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td>
                                <th>Division</th>
                                <td>
                                    <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="text-align: left;">
                                <th>Employee No</th>
                                <td colspan="2">
                                    <asp:Label ID="lblEmpNo" runat="server" Text=""></asp:Label></td>
                                <th>Date</th>
                                <td>
                                    <asp:Label ID="lblDateLeave" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="text-align: left;">
                                <th colspan="5">Leave Particulars</th>
                            </tr>
                            <tr style="text-align: left;">
                                <th>Leave Category</th>
                                <th style="width: 100px">From</th>
                                <th style="width: 100px">To</th>
                                <th>No. Of Days</th>
                                <th>Balance</th>
                            </tr>
                            <tr style="text-align: left;">
                                <td>
                                    <asp:Label ID="lblLeaveType" runat="server" Text=""></asp:Label>
                                    <%--Casual Leave (CL)--%>

                                </td>
                                <td>
                                    <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblTo" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblDays" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblBalance" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <%--<tr style="text-align: left;">
                                <th>Restrcted Holiday (EH)</th>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
                            </tr>--%>
                            <tr style="text-align: left;">
                                <th colspan="5">Reason:
                                    <asp:Label ID="lblReason" runat="server" Text=""></asp:Label></th>
                            </tr>
                            <tr style="text-align: left;">
                                <th>Sign Of Employee: </th>
                                <td colspan="2">

                                    <td>Date</td>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <span class="confirmation">In the absence of the above employee, of the leave is granted, the charge would be hold by Shri/Smt ...............................................</span>
                    </div>
                    <div>
                        <div class="date_cl">
                            Date:	
                        </div>
                        <div class="sign_cl">
                            Sign. Of Section Head	
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
