<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptFinBalanceSheetEng20_21.aspx.cs" Inherits="mis_Finance_RptFinBalanceSheetEng20_21" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .tdtext {
            font-weight: 800 !important;
            /*color: #e82319 !important;*/
        }

        .align-right {
            text-align: right !important;
        }

        .align-rightB {
            text-align: right !important;
            font-weight: 700 !important;
            /*color: #e82319 !important;*/
        }

        td {
            padding: 2px !important;
        }

        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }

            .box, .box-body {
                border: none;
                padding: 0px;
            }

            td {
                padding: 2px;
            }
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header hide_print">
                    <h3 class="box-title">Balance Sheet (2020-21)</h3>

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>

                <div class="box-body">

                    <h4 style="text-align: center">BHOPAL SAHAKARI DUGDH SANGH MARYADIT<br />
                        Habibganj, Bhopal (M.P) PIN - 462024<br />
                        Balance Sheet<br />
                        01-04-2020 To 31-03-2021<br /></h4>
                    <table class="table table-bordered">
                        <tr>
                            <th>Liabilities</th>
                            <th colspan="2">as at 31-March-2021</th>
                            <th>Assets</th>
                            <th colspan="2">as at 31-March-2021</th>
                        </tr>
                        <tr>
                            <td class="tdtext">Capital Account</td>
                            <td></td>
                            <td class="align-rightB">61,03,22,173.99</td>
                            <td class="tdtext">Fixed Assets</td>
                            <td></td>
                            <td class="align-rightB">23,99,26,967.93</td>
                        </tr>
                        <tr>
                            <td>Share Capital</td>
                            <td class="align-right">10,76,76,648.00</td>
                            <td></td>
                            <td>AI EQUIPMENT</td>
                            <td class="align-right">4,88,850.48</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>GRANT & SUBSIDY</td>
                            <td class="align-right">6,65,49,628.24</td>
                            <td></td>
                            <td>Air Conditioner</td>
                            <td class="align-right">1,14,177.39</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Reserves & Surplus</td>
                            <td class="align-right">43,60,95,897.75</td>
                            <td></td>
                            <td>BMC</td>
                            <td class="align-right">7,67,318.86</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Building Main</td>
                            <td class="align-right">23,93,626.88</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdtext">Loans (Liability)</td>
                            <td></td>
                            <td class="align-rightB">13,59,072.06</td>
                            <td>Computer & Assesiries</td>
                            <td class="align-right">39,08,215.45</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Short Term Loan</td>
                            <td class="align-right">13,59,072.06</td>
                            <td></td>
                            <td>Cooler</td>
                            <td class="align-right">7,23,999.30</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>ELECTRIC EQUIPMENT</td>
                            <td class="align-right">5,21,036.79</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdtext">Current Liabilities</td>
                            <td></td>
                            <td class="align-rightB">94,97,48,995.76</td>
                            <td>FIXED ASSET HATA, RAJNAGAR, CHHATARPUR</td>
                            <td class="align-right">18,10,132.99</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Other Liabilities</td>
                            <td class="align-right">31,98,60,263.10</td>
                            <td></td>
                            <td>FIXED ASSET OF CATTLE FEED</td>
                            <td class="align-right">4,72,89,780.15</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>SECURITY DEPOSIT CONTRACTOR</td>
                            <td class="align-right">13,74,43,181.56</td>
                            <td></td>
                            <td>FIXED ASSETS</td>
                            <td class="align-right">1,78,891.38</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Duties & Taxes</td>
                            <td class="align-right">(-)1,42,04,967.99</td>
                            <td></td>
                            <td>Fixed Assets Gms</td>
                            <td class="align-right">61,559.32</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Provisions</td>
                            <td class="align-right">29,07,94,589.37</td>
                            <td></td>
                            <td>Fixed Assets of CC Guna</td>
                            <td class="align-right">46,407.02</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Sundry Creditors</td>
                            <td class="align-right">21,58,55,929.72</td>
                            <td></td>
                            <td>FURNITURE & FIXTURES</td>
                            <td class="align-right">54,53,919.58</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>LAB EQUIPMENT</td>
                            <td class="align-right">11,61,849.19</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdtext">Profit & Loss A/c</td>
                            <td></td>
                            <td></td>
                            <td>Land</td>
                            <td class="align-right">1,76,295.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>LCD Projector A/c</td>
                            <td class="align-right">20,076.55</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdtext">Profit & Loss Appr A/c Bds</td>
                            <td></td>
                            <td class="align-rightB">76,33,39,951.45</td>
                            <td>Milk Can Main</td>
                            <td class="align-right">76,35,318.31</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Opening Balance</td>
                            <td class="align-right">69,81,09,686.05</td>
                            <td></td>
                            <td>Milk Parlour/ Booth</td>
                            <td class="align-right">52,95,914.79</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Current Period</td>
                            <td class="align-right">23,94,80,818.00</td>
                            <td></td>
                            <td>NEW PLANT (NDDB)</td>
                            <td class="align-right">6,68,51,185.56</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Less: Transferred</td>
                            <td class="align-right">17,42,50,552.60</td>
                            <td></td>
                            <td>OFFICE EQUIPMENT</td>
                            <td class="align-right">1,60,773.47</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>PLANT & MACHINERY</td>
                            <td class="align-right">8,29,17,932.89</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Plant & Machinery CC</td>
                            <td class="align-right">52,59,236.79</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Plant & Machinery CFF</td>
                            <td class="align-right">52,39,714.50</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Publicity & Equipment A</td>
                            <td class="align-right">57,456.49</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>PURCHASE OF VEHICLE</td>
                            <td class="align-right">8,98,698.88</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>TUBE WELL</td>
                            <td class="align-right">48,199.83</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Vety Equipment Main</td>
                            <td class="align-right">1.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Weighing Scale</td>
                            <td class="align-right">4,46,399.09</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="tdtext">Investments</td>
                            <td></td>
                            <td class="align-rightB">57,59,400.00</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>BCCB (Investment)</td>
                            <td class="align-right">1,00,000.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Gramm Vidhut Investment</td>
                            <td class="align-right">500.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Investment on Share</td>
                            <td class="align-right">15,000.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>MPCDF SHARE INVESTMENT</td>
                            <td class="align-right">50,01,000.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>MPDDC (Investment)</td>
                            <td class="align-right">29,000.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>MPDDC INVESTMENT (B)</td>
                            <td class="align-right">100.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>MPDMS (Investment)</td>
                            <td class="align-right">5,40,000.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>MPRSB (Investment)</td>
                            <td class="align-right">73,800.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="tdtext">Current Assets</td>
                            <td></td>
                            <td class="align-rightB">2,07,90,83,825.33</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Advance To DCS</td>
                            <td class="align-right">20,49,870.46</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Other Current Assets</td>
                            <td class="align-right">20,00,99,940.66</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Closing Stock</td>
                            <td class="align-right">41,42,02,846.83</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Deposits (Asset)</td>
                            <td class="align-right">11,37,27,188.61</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Advance To Staff</td>
                            <td class="align-right">14,45,724.29</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Other Advances</td>
                            <td class="align-right">3,08,53,602.56</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Sundry Debtors</td>
                            <td class="align-right">16,96,57,276.87</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Cash-in-hand</td>
                            <td class="align-right">59,252.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Bank Accounts</td>
                            <td class="align-right">22,09,81,754.05</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>FDR</td>
                            <td class="align-right">92,60,06,369.00</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="tdtext">Work Under Construction</td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdtext">Total</td>
                            <td></td>
                            <td class="align-rightB">2,32,47,70,193.26</td>
                            <td class="tdtext">Total</td>
                            <td></td>
                            <td class="align-rightB">2,32,47,70,193.26</td>
                        </tr>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>






