<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTComposeFileByOperator.aspx.cs" Inherits="mis_filetracking_FTComposeFileByOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/datepicker3.css" rel="stylesheet" />
    <style>
        .inline-rb label {
            margin-left: 5px;
        }



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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-header">
                            <h3 class="box-title">फ़ाइल / पत्र / नोट शीट (Inward File)</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Type (प्रकार) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFile_Type" class="form-control" runat="server" onchange="ChangeColor();" ClientIDMode="Static">
                                            <asp:ListItem Value="0">चुनें </asp:ListItem>
                                            <asp:ListItem Value="1">फ़ाइल / नोट शीट </asp:ListItem>
                                            <asp:ListItem Value="3">पत्र </asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFile_Type" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Priority (प्राथमिकता चुने) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFile_Priority" class="form-control" runat="server">
                                            <asp:ListItem Text="चुने"></asp:ListItem>
                                            <asp:ListItem>Low</asp:ListItem>
                                            <asp:ListItem>Medium</asp:ListItem>
                                            <asp:ListItem>High</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFile_Priority" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>File No (फ़ाइल / नोट शीट/ पत्र संख्या) <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtFile_No" runat="server" class="form-control" MaxLength="50" placeholder="फ़ाइल / नोट शीट/ पत्र संख्या भरें..." Style="text-transform: uppercase" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtFile_No" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Title (दस्तावेज़ का शीर्षक) <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtFile_Title" runat="server" class="form-control" placeholder="दस्तावेज़ का शीर्षक भरें..." autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtFile_Title" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>BAR Code<%--<span style="color: red;">*</span>--%></label>
                                        <asp:TextBox ID="txtQRCode" runat="server" placeholder="Enter BAR Code..." class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtQRCode" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Created by (के द्वारा बनाई गई) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEmployeeList" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlEmployeeList" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Create Date (बनने की दिनांक)<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtCreateDate" runat="server" placeholder="Select Letter Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtCreateDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Attachment (दस्तावेज संलग्न करें) </label>
                                        <asp:HyperLink ID="hyprDoc" Target="_blank" runat="server" Visible="false" Text="VIEW"></asp:HyperLink><br />
                                        <asp:FileUpload ID="Document_Upload" class="form-control" runat="server" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label id="lblDescription">Description (विवरण)</label>
                                        <asp:TextBox ID="txtFile_Description" runat="server" class="form-control" Rows="13" TextMode="MultiLine" MaxLength="200" placeholder="विवरण भरें..." ClientIDMode="Static"></asp:TextBox>
                                        <small><span id="valtxtFile_Description" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" class="btn btn-block btn-success" Text="Create" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FTComposeFileByOperator.aspx" runat="server" class="btn btn-default btn-block">Clear</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" DataKeyNames="File_ID" class="datatable table table-hover table-bordered table-striped" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="प्राथमिकता">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ToolTip='<%# Eval("File_ID")%>' ID="FilePriority"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="File_No" HeaderText="फ़ाइल / नोट शीट/ पत्र संख्या" />
                                                <asp:BoundField DataField="File_Title" HeaderText="दस्तावेज़ का शीर्षक" />
                                                <asp:BoundField DataField="StatusOfFile" HeaderText="दस्तावेज़ का तरीका" />
                                                <asp:BoundField DataField="QRCode" HeaderText="बार कोड" />
                                                <asp:BoundField DataField="FileCreatedOnOpt" HeaderText="फाइल / पत्र बनने की दिनांक" />
                                                <asp:BoundField DataField="Emp_Name" HeaderText="फाइल / पत्र बनाया गया" />
                                                <asp:TemplateField HeaderText="फाइल / पत्र का विवरण">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="label label-danger" runat="server" CommandName="delete" OnClientClick="return confirm('File will be deleted, Are you sure want to continue?')">Delete</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkInfo" CssClass="label label-info" runat="server" Text="File has been forwarded"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkEdit" CssClass="label label-default" runat="server" CommandName="RecordEdit" CommandArgument='<%# Eval("File_ID")%>' OnClientClick="return confirm('Are you sure want ro edit this file?')">Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
        $('.datatable').DataTable({
            paging: true,
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
                        columns: [0, 1, 2, 3, 4, 5, 6,7]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6,7]
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
    <script type="text/javascript">

        function ChangeColor() {
            //फ़ाइल / नोट शीट/ पत्र  विवरण
            if (document.getElementById('<%=ddlFile_Type.ClientID%>').selectedIndex == 1) {
                $("#txtFile_Description").css({ "background-color": "#bff2d3" });
                $("#lblDescription").text("नोट शीट विवरण");
            }
            else if (document.getElementById('<%=ddlFile_Type.ClientID%>').selectedIndex == 2) {
                $("#txtFile_Description").css({ "background-color": "white" });
                $("#lblDescription").text("पत्र  विवरण");
            }
            else {
                $("#txtFile_Description").css({ "background-color": "white" });
                $("#lblDescription").text("विवरण");
            }
        }


        function validateform() {
            var msg = "";
            var FileType = "";
            $("#valddlFile_Type").html("");
            $("#valddlFile_Priority").html("");
            $("#valtxtFile_No").html("");
            $("#txtFile_Title").html("");
            $("#valtxtFile_Description").html("");
            //$("#valtxtQRCode").html("");

            if (document.getElementById('<%=ddlFile_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type. \n";
                $("#valddlFile_Type").html("Select Type.");
            }
            if (document.getElementById('<%=ddlFile_Priority.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Priority. \n";
                $("#valddlFile_Priority").html("Select Priority.");
            }
            if (document.getElementById('<%=txtFile_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter File/Note Sheet/Letter No. \n";
                $("#valtxtFile_No").html("Enter File/Note Sheet/Letter No.");
            }
            if (document.getElementById('<%=txtFile_Title.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Title. \n";
                $("#valtxtFile_Title").html("Enter Title.");
            }
            if (document.getElementById('<%=ddlEmployeeList.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Created by. \n";
                $("#valddlEmployeeList").html("Select Created by.");
            }
            if (document.getElementById('<%=txtCreateDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Create Date. \n";
                $("#valtxtFile_Title").html("Select Create Date.");
            }
            <%-- if (document.getElementById('<%=txtQRCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter QR Code. \n";
                $("#valtxtQRCode").html("Enter QR Code.");
            }--%>
            <%--if (document.getElementById('<%=txtFile_Description.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Description. \n";
                $("#valtxtFile_Description").html("Enter Description.");
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=ddlFile_Type.ClientID%>').selectedIndex == 1) {
                    FileType = "File/Note Sheet";
                }
                else if (document.getElementById('<%=ddlFile_Type.ClientID%>').selectedIndex == 2) {
                    FileType = "Letter";
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Create") {
                    if (confirm("Do you really want to Create " + FileType + " ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------

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
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
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
                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
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
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>

