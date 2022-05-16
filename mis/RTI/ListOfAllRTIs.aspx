<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ListOfAllRTIs.aspx.cs" Inherits="mis_RTI_ListOfAllRTIs" %>

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

        .dt-buttons {
            margin-bottom: 20px;
        }

        .dataTables_length {
            margin-left: 170px;
            margin-top: -50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%--<section class="content-header">
            <div class="row">
                <!-- left column -->
                <div class="col-md-10 col-md-offset-1">
                    
                </div>
            </div>
        </section>--%>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12 ">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">RTI Request</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlOfficeID" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date (दिनांक से)<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date (दिनांक तक)<span style="color: red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" placeholder="Select From Date.." CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnsearch" runat="server" ValidationGroup="Save" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 26px;" OnClick="btnsearch_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                        <asp:GridView ID="GridView1" DataKeyNames="RTI_ID" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RTI Status" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_Status").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="App_Name" HeaderText="Applicant Name" />
                                                <%--<asp:BoundField DataField="RTI_RequestType" HeaderText="RTI RequestType" />--%>
                                                <asp:BoundField DataField="RTI_RegistrationNo" HeaderText="Registration No" />
                                                <asp:BoundField DataField="RTI_Subject" HeaderText="RTI Subject" />
                                                <asp:BoundField DataField="RTI_UpdatedOn" HeaderText="File Date" />
                                                <asp:BoundField DataField="RTI_ClosingDate" HeaderText="Closing Date" />
                                                <asp:TemplateField HeaderText="Edit Detail" ShowHeader="False" ItemStyle-CssClass="text-center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCntBeEdit" Text="Can't Be Edit" runat="server" CssClass="label label-warning"></asp:Label>
                                                        <asp:LinkButton ID="lnkEditModal" runat="server" CssClass="label label-info" Text="Edit Detail" OnClick="lnkEditModal_Click" ToolTip='<%# Eval("RTI_ID").ToString()%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RTI Detail" ShowHeader="False" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkViewDetail" runat="server" CssClass="label label-default" Text="View Detail" NavigateUrl='<%# "RTIReply.aspx?RTI_ID=" + APIProcedure.Client_Encrypt(Eval("RTI_ID").ToString())+"&ShowHide=" + APIProcedure.Client_Encrypt("Show")%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div id="ModalEditRTIDetail" class="modal fade" role="dialog">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header  bg-gray-light">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Forward To</h4>
                                        </div>
                                        <div class="modal-body bg-gray-light">
                                            <asp:Label ID="lblModal" runat="server"></asp:Label>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Subject for RTI (आरटीआई के लिए विषय)<span style="color: red;"> *</span></label><br />
                                                        <asp:TextBox ID="txtRTI_Subject" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                        </asp:TextBox>
                                                        <small><span id="valRTI_Subject" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Text for RTI Request Application (आरटीआई अनुरोध आवेदन)<span style="color: red;"> *</span></label>
                                                        <asp:TextBox ID="txtRTI_Request" runat="server" class="form-control " ClientIDMode="Static" TextMode="MultiLine" Width="100%" Rows="6">
                                                        </asp:TextBox>
                                                        <small><span id="valRTI_Request" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Supporting Document (सहायक दस्तावेज)</label>
                                                        <div class="pull-right">
                                                            <asp:HyperLink ID="hyprRTI_RequestDoc" runat="server" Text="View Document" Target="_blank"></asp:HyperLink>
                                                        </div>
                                                        <asp:FileUpload ID="fuRTI_RequestDoc" runat="server" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" ClientIDMode="Static" />
                                                        <small><span id="valRTI_RequestDoc" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-8"></div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnEditRTI" runat="server" class="btn btn-success btn-block" Text="Update" OnClientClick="return validate()" OnClick="btnEditRTI_Click" ClientIDMode="Static" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>

    <script>
        $('#txtFromDate').change(function () {
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if ($('#txtToDate').val() != "") {
                if (start > end) {
                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if (start > end) {
                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });

        function validateform() {
            var msg = "";
           
            $("#valtxtDate").html("");
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Please Select To Date. \n";
                $("#valtxtDate").html("Please Select Date.");
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Please Select From Date. \n";
                $("#valtxtDate").html("Please Select Date.");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
        }
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: true,

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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
        function openModal1() {
            debugger
            //$('#AddOfficerModal').modal('show');
            $('#ModalEditRTIDetail').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            })
        };

        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------
            $("#valRTI_RequestDoc").html("");
            var msg = '';
            if (document.getElementById(that.id).value != '') {
                var size = document.getElementById(that.id);

                var fileName = document.getElementById(that.id).value;
                var lengthFileName = parseInt(document.getElementById(that.id).value.length)

                var fileExtacntionArray = new Array();
                fileExtacntionArray = fileName.split('.');

                if (fileExtacntionArray.length == 2) {

                    var fileExtacntion = fileExtacntionArray[fileExtacntionArray.length - 1];


                    if (lengthFileName >= parseInt(maxLengthFileName) + parseInt(1)) {
                        msg += '- File Name Should be less than ' + maxLengthFileName + ' characters. \n';
                        $("#valRTI_RequestDoc").html("File Name Should be less than " + maxLengthFileName + " characters.");
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            $("#valRTI_RequestDoc").html("Special character not allowed in file name");
                            break;
                        }

                    }
                    var isFileFormatCorrect = false;
                    var strValidFormates = '';

                    if (validFileFormaString != "") {

                        var fileFormatArray = new Array();
                        fileFormatArray = validFileFormaString.split('*');

                        for (var j = 0; j < fileFormatArray.length; j++) {
                            if (fileFormatArray[j].toUpperCase() == fileExtacntion.toUpperCase()) {
                                isFileFormatCorrect = true;
                            }

                            if (j == fileFormatArray.length - 1) {
                                strValidFormates += '.' + fileFormatArray[j].toLowerCase();

                            }
                            else {
                                strValidFormates += '.' + fileFormatArray[j].toLowerCase() + '/';

                            }
                        }

                        if (isFileFormatCorrect == false) {
                            msg += 'File Format Is Not Correct (Only ' + strValidFormates + ').\n';
                            $("#valRTI_RequestDoc").html("File Format Is Not Correct (Only " + strValidFormates + ")");
                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
                    // $("#valfuRTI_RequestDoc").html("");
                }
                if (msg != '') {
                    document.getElementById(that.id).value = "";
                    alert(msg);
                    return false;
                }
                else {
                    return true;
                }

            }
        }
        function ValidateFileSize(a) {

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 20971520) {
                alert('File size should not greater than 5 mb.');
                $("#valRTI_RequestDoc").html("फ़ाइल 5 MB से अधिक नहीं होनी चाहिए।");
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
        function validate() {
            var msg = "";

            if (document.getElementById('txtRTI_Subject').value == "") {
                msg += "Enter Subject for RTI (आरटीआई के लिए विषय)\n";
                $("#valtxtRTI_Subject").html("Enter Subject for RTI (आरटीआई के लिए विषय)");
            }
            if (document.getElementById('txtRTI_Request').value == "") {
                msg += "Enter Text for RTI Request Application (आरटीआई अनुरोध आवेदन)\n";
                $("#valtxtRTI_Request").html("Enter Text for RTI Request Application (आरटीआई अनुरोध आवेदन)");
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnEditRTI.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
    </script>
</asp:Content>

