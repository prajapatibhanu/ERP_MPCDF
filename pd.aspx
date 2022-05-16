<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pd.aspx.cs" Inherits="pd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Producer Detail</title>
    <link href="mis/Masters/print.css" rel="stylesheet" />
    <script type="text/javascript">
        function printDiv() {
            var divToPrint = document.getElementById('DivIdToPrint');
            var htmlToPrint = '';
            htmlToPrint += divToPrint.outerHTML;
            var newWin = window.open('', 'Print-Window', 'letf=0,top=0,width=800%,height=600,toolbar=0,scrollbars=0,status=0');
            // alert('1');
            newWin.document.open();
            newWin.document.write('<html><head><link rel=\"stylesheet\" href=\"print.css\" type=\"text/css\" media=\"print\"/></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            // mywindow.document.write("");
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 1000);
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="content-wrapper">
            <section class="content">
                <div class="box box-success">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>

                    <div class="box-body">
                        <div class="row">
                        </div>

                    </div>
                </div>
            </section>
        </div>

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <%--<div class="modal-header">
                        <h5 style="float: left;">दुग्ध उत्पादक पहचान पत्र</h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>--%>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <div class="row">
                            <div id="DivIdToPrint">
                                <div class="pagebreak">
                                    <table width="100%" style="background: url(mis/image/card_wm.png) no-repeat center center;">
                                        <tr>
                                            <td align="center" colspan="2" style="line-height: 18px;">
                                                <div style="float: left; width: 15%; text-align: left;">
                                                    <img src="mis/image/bds_logo.png" style="width: 36px;" />
                                                </div>
                                                <div style="float: left; text-align: center; width: 85%;">
                                                    <p style="text-align: center; text-transform: uppercase; font-size: 11pt; margin: 0; font-weight: bold;">
                                                        <asp:Label ID="lblds" runat="server"></asp:Label>
                                                    </p>
                                                    <p style="text-align: center; text-transform: uppercase; font-size: 9pt; margin: 0; font-weight: bold; color: #d72f31; padding: 0;">(दुग्ध उत्पादक पहचान पत्र)</p>
                                                    <p style="text-align: center; border: 1px solid #000; background: #000; width: 40px; margin: 5px auto 10px;"></p>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" width="35%" style="font-size: 12px;">दुग्ध समिति का नाम :</td>
                                            <td valign="top" width="65%">
                                                <asp:Label ID="lblSociety" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 12px;">विकासखण्ड :</td>
                                            <td valign="top">
                                                <asp:Label ID="lblBlock" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 12px;">सदस्यता क्र. :</td>
                                            <td valign="top">
                                                <asp:Label ID="lblCode" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 12px;">सदस्य का नाम :</td>
                                            <td valign="top">
                                                <asp:Label ID="lblProducer" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 12px;">मोबाइल :</td>
                                            <td valign="top">
                                                <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="position: absolute; bottom: 40px; right: 10px;">
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                                <div class="pagebreak">
                                    <table width="100%" style="background: url(mis/image/card_wm.png) no-repeat center center;">
                                        <tr>
                                            <td align="center" colspan="2" style="line-height: 18px;">
                                                <div style="float: left; width: 15%; text-align: left;">
                                                  <img src="mis/image/bds_logo.png" style="width: 36px;" />
                                                </div>
                                                <div style="float: left; text-align: center; width: 85%;">
                                                    <p style="text-align: center; text-transform: uppercase; font-size: 11pt; margin: 0; font-weight: bold;">
                                                        <asp:Label ID="lblDS1" runat="server"></asp:Label>
                                                    </p>
                                                    <p style="text-align: center; text-transform: uppercase; font-size: 9pt; margin: 0; font-weight: bold; color: #d72f31; padding: 0;">(दुग्ध उत्पादक पहचान पत्र)</p>
                                                    <p style="text-align: center; border: 1px solid #000; background: #000; width: 40px; margin: 5px auto 10px;"></p>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <ol style="margin: 5px 10px 0px; padding: 0px; line-height: 18px;">
                                                    <li>यह कार्ड सर्वथा अहस्तांतरणीय है ।</li>
                                                    <li>इस कार्ड को सुरक्षित रखने की पूर्ण जिम्मेदारी सदस्य की होगी ।</li>
                                                    <li>सदस्य को समिति में दुग्ध प्रदाय करते समय कार्ड प्रस्तुत करना होगा ।</li>
                                                    <li>कार्ड खोने पर समिति में आवेदन देकर तथा रु. 100 का शुल्क देकर नया कार्ड प्राप्त किया जा सकेगा । </li>
                                                </ol>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" valign="baseline" style="padding: 20px 20px 0; font-size: 12px;">
                                                <div style="width: 50%; float: left;">सचिव का नाम</div>
                                                <div style="width: 50%; float: left; text-align: right;">हस्ताक्षर सचिव</div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                   <%-- <div class="modal-footer">
                        <input type='button' id='btn' class="btn mt-2 mb-2 btn-sm btn-primary" value='PRINT' onclick="printDiv();" />
                    </div>--%>
                </div>
            </div>

            <div class="clearfix"></div>
        </div>

    </form>
     
    <script>
        function ShowModal() {
            $('#myModal').modal('toggle');
        }
        function HideModal() {
            $('#myModal').hide(t);
        }
        function printData() {
            var divToPrint = document.getElementById("printTable");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }
    </script>
</body>
</html>
