<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GenrateDutyChart.aspx.cs" Inherits="mis_DutyChart_GenrateDutyChart" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish no-print" style="min-height: 250px;">
                <div class="box-header">
                    <h3 class="box-title">Generate Duty Master</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row no-print">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                        InitialValue="0" ErrorMessage="Select Year" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                        ControlToValidate="ddlYear" ForeColor="Red" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" ClientIDMode="Static">
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
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" ClientIDMode="Static">
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" Style="margin-top: 21px;" ValidationGroup="a" ID="btnSubmit" Text="Generate Chart" OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row no-print">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Year</label>
                            <asp:DropDownList ID="ddlSearchYear" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="2021">2021</asp:ListItem>
                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                <asp:ListItem Value="2023">2023</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Month</label>
                            <asp:DropDownList ID="ddlSearchMonth" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchMonth_SelectedIndexChanged">
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
                                            2021 दिनांक - <label id="lblDate" runat="server"></label> से आगामी आदेश तक प्रभावशील</td>
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
                                            <asp:GridView ID="GvDetail" ShowHeader="true" AutoGenerateColumns="false" CssClass="datatable table table-bordered" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="BMCTankerRootName" HeaderText="Root Name" />
                                                    <asp:BoundField DataField="V_VehicleNo" HeaderText=" Vehicle No" />
                                                    <asp:BoundField DataField="D_VehicleCapacity" HeaderText="Vehicle Capacity" />
                                                    <asp:BoundField DataField="Driver_Name" HeaderText="Driver Name" />
                                                    <asp:BoundField DataField="Tester_Name" HeaderText="Tester Name" />
                                                    <asp:BoundField DataField="Cleaner_Name" HeaderText="Attendant" />
                                                    <%--<asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" />--%>
                                                </Columns>
                                            </asp:GridView>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../Finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../Finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="../Finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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
    </script>
</asp:Content>

