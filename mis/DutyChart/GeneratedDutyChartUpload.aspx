<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GeneratedDutyChartUpload.aspx.cs" Inherits="mis_DutyChart_GeneratedDutyChartUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .StringDiv {
            font-size: 13px !important;
            font-family: monospace !important;
        }

        table td, table th {
            border-bottom: 1px dotted #c1c0c0 !important;
            padding: 5px !important;
            border-right: 1px dotted #c1c0c0 !important;
            margin: 0px !important;
        }

        .right_align {
            text-align: right;
        }

        .left_align {
            text-align: left;
        }

        .center_align {
            text-align: left;
        }

        table th {
            background: #9e9e9e !important;
            padding: 2px 5px !important;
            word-break: break-all !important;
            border-bottom: 1px dotted #ddd !important;
        }

        .btnsubmit {
            margin-top: 18px;
        }

        /* loading dots */

        .one {
            opacity: 0;
            -webkit-animation: dot 1.3s infinite;
            -webkit-animation-delay: 0.0s;
            animation: dot 1.3s infinite;
            animation-delay: 0.0s;
        }

        .two {
            opacity: 0;
            -webkit-animation: dot 1.3s infinite;
            -webkit-animation-delay: 0.2s;
            animation: dot 1.3s infinite;
            animation-delay: 0.2s;
        }

        .three {
            opacity: 0;
            -webkit-animation: dot 1.3s infinite;
            -webkit-animation-delay: 0.3s;
            animation: dot 1.3s infinite;
            animation-delay: 0.3s;
        }

        @-webkit-keyframes dot {
            0% {
                opacity: 0;
            }

            50% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @keyframes dot {
            0% {
                opacity: 0;
            }

            50% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        p.loading {
            font-weight: 900;
            font-size: 22px;
            text-align: center;
            color: #e91e63;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnUpload_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">


                <div class="box-header">
                    <h3 class="box-title">Upload Duty Chart</h3>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row no-print">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Year<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select Year" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                            ControlToValidate="ddlYear" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                        <asp:ListItem Value="2022">2022</asp:ListItem>
                                        <asp:ListItem Value="2023">2023</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Month<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" InitialValue="0"
                                            ErrorMessage="Select Month" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="ddlMonth" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
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
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Choose File<span class="text-danger">*</span></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" required="required" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnUpload" ValidationGroup="a" runat="server" CssClass="btn btn-success btnsubmit" Text="Upload" OnClick="btnUpload_Click" OnClientClick="return ValidatePage();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive" id="DivData" runat="server" visible="false">
                                <table style="width: 100%;">
                                    <thead>
                                        <tr>
                                             <td colspan="3"><asp:Label ID="lblHeading" runat="server"></asp:Label>&nbsp;ड्यूटी चार्ट</td>
                                        </tr>
                                        <tr>
                                            <td>क्रमांक/ ......................... /गैरेज 2021</td>
                                            <td>दिनांक ...............</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>माह -
                                                <label id="lblMonth" runat="server"></label>
                                                2021 दिनांक -
                                            <label id="lblDate" runat="server"></label>
                                                से आगामी आदेश तक प्रभावशील</td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Duty Chart</td>
                                            <td>Month -
                                            <label id="lblMonth2" runat="server"></label>
                                            </td>
                                            <td>ERP Generated</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="3">
                                                <div id="divExport" runat="server">
                                                    <asp:GridView ID="GvDetail" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="RouteName" HeaderText="Root Name" />
                                                            <asp:BoundField DataField="TankerNo" HeaderText="Vehicle No" />
                                                            <asp:BoundField DataField="VehicleCapacity" HeaderText="Vehicle Capacity" />
                                                            <asp:BoundField DataField="DriverName" HeaderText="Driver Name" />
                                                            <asp:BoundField DataField="TesterName" HeaderText="Tester Name" />
                                                            <asp:BoundField DataField="CleanerName" HeaderText="Attendant" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="3">
                                                <ul>
                                                    विवरण :
                                                    <li>वाहन चालक वाहन के रख रखाव हेतु स्वयं जवाबदार रहेंगे |</li>
                                                    <li>वाहन की साफ सफाई उपरांत ही वाहन डेरी परिसर से बहार निकले |</li>
                                                    <li>वाहन का मेलो मीटर चालू होना आवश्यक है बंद पाए जाने पर वाहन चालक पर कार्यवाही की जावेगी |</li>
                                                    <li>वाहन चालक द्वारा लॉग बुक को नियमित भरी जावे जाँच दौरान अधूरी लॉगबुक पाए जाने पर कार्यवाही की जाएगी साथ ही टी.ए. निरस्त कर दिए जायेंगे |</li>
                                                </ul>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <ul>
                                                    नोट :
                                                    <li>वाहनों में लगे जी. पी. एस सिस्टम में किसी भी तरह की छेड़छाड़ की जिम्मेदारी वाहन चालक की होगी |</li>
                                                    <li>वाहन चालक बिना अनुमति से टैंकर पर अय चालक को नहीं भेजेंगे | वाहन की साफ़ सफाई की जवाबदारी वाहन चालक की होगी |</li>
                                                    <li>आवश्यक रन कि. मी. दूरी चलने का ध्यान रखे एवं समय पर सर्विसिंग का कार्य कराये जाने हेतु मैकेनिक के बताये |</li>
                                                </ul>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>प्रभारी (परिवहन </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <ol>
                                                    <li>महाप्रबंधक (सयंत्र सञ्चालन) की ओर सूचनार्थ |</li>
                                                    <li>टाइम ऑफिस की ओर सूचनार्थ |</li>
                                                    <li>नोटिस बोर्ड चस्पा |</li>
                                                </ol>
                                            </td>
                                            <td></td>
                                            <td>प्रभारी (परिवहन)</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnUpload.ClientID%>').value.trim() == "Upload") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

