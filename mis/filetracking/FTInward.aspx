<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTInward.aspx.cs" Inherits="mis_filetracking_FTInvert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/datepicker3.css" rel="stylesheet" />
    <style>
        .inline-rb label {
            margin-left: 5px;
        }

        /*table#ContentBody_rbtType, table#ContentBody_rbtType td {
            border: 0 !important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-header">
                            <div class="col-md-10">
                                <h3 class="box-title">Inward Letter</h3>
                            </div>
                            <%--<div class="col-md-2">
                                <asp:TextBox ID="txtCurrentDate" runat="server" placeholder="Select Date" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                            </div>--%>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Letter Date (लैटर बनने दिनांक)<span style="color: red;">*</span></label>
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
                                        <label>Receiving Date (प्राप्ति दिनांक)<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtInwardDate" runat="server" placeholder="Select Receiving Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static" onchange="CompareReceiveDate()"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtInwardDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Letter No (पत्र संख्या) <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtFile_No" runat="server" class="form-control" MaxLength="50" placeholder="Enter Letter No." autocomplete="off" Style="text-transform: uppercase"></asp:TextBox>
                                        <small><span id="valtxtFile_No" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
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
                                        <label>Letter Subject (पत्र विषय)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtSubject" runat="server" placeholder="Enter Letter Subject" autocomplete="off" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <small><span id="valtxtSubject" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Letter Receive From (से प्राप्त किया)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtReceivingFrom" runat="server" placeholder="Enter Letter Receive From" autocomplete="off" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <small><span id="valtxtReceivingFrom" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Address to (को संबोधित)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtAddressTo" runat="server" placeholder="Enter Address To" class="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        <small><span id="valtxtAddressTo" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>BAR Code<%--<span style="color: red;">*</span>--%></label>
                                        <asp:TextBox ID="txtQRCode" runat="server" placeholder="Enter BAR Code" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtQRCode" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Upload File (दस्तावेज अपलोड करें)</label>
                                        <asp:HyperLink ID="hyprDoc" Target="_blank" runat="server" Text="VIEW"></asp:HyperLink><br />
                                        <asp:FileUpload ID="UploadFile" CssClass="form-control" runat="server" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark (टिप्पणी)</label><span style="color: red;">*</span>
                                        <asp:TextBox ID="txtRemark" runat="server" class="form-control" Rows="4" TextMode="MultiLine" MaxLength="200" placeholder="Enter Remark" ClientIDMode="Static"></asp:TextBox>
                                        <small><span id="valtxtRemark" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group"></div>
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="BtnSave" runat="server" class="btn btn-block btn-success" Text="Save" OnClientClick="return validateform();" OnClick="BtnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FTInward.aspx" runat="server" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script type="text/javascript">
        function validateform() {
            debugger;
            var msg = "";
            var FileType = "";
            $("#valtxtCreateDate").html("");
            $("#valtxtInwardDate").html("");
            $("#valtxtFile_No").html("");
            $("#valddlFile_Priority").html("");
            $("#valtxtSubject").html("");
            $("#valtxtReceivingFrom").html("");
            $("#valtxtAddressTo").html("");
            //$("#valtxtQRCode").html("");
            $("#valtxtRemark").html("");

            if (document.getElementById('<%=txtCreateDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Letter Date. \n";
                $("#valtxtCreateDate").html("Select Letter Date.");
            }
            if (document.getElementById('<%=txtInwardDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Receiving Date. \n";
                $("#valtxtInwardDate").html("Select Receiving Date.");
            }
            if (document.getElementById('<%=txtFile_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter No. \n";
                $("#valtxtFile_No").html("Enter Letter No.");
            }
            if (document.getElementById('<%=ddlFile_Priority.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Priority. \n";
                $("#valddlFile_Priority").html("Select Priority.");
            }
            if (document.getElementById('<%=txtSubject.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter Subject. \n";
                $("#valtxtSubject").html("Enter Letter Subject.");
            }
            if (document.getElementById('<%=txtReceivingFrom.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter Receive From . \n";
                $("#valtxtReceivingFrom").html("Enter Letter Receive From .");
            }
            if (document.getElementById('<%=txtAddressTo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Address To. \n";
                $("#valtxtAddressTo").html("Enter Address To.");
            }
           <%-- if (document.getElementById('<%=txtQRCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter QR Code. \n";
                $("#valtxtQRCode").html("Enter QR Code");
            }--%>
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Remark. \n";
                $("#valtxtRemark").html("Enter Remark.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=BtnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Detail ?")) {
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
        function CompareReceiveDate() {
            var LetterDate = document.getElementById('<%= txtCreateDate.ClientID%>').value.trim();
            if (LetterDate != "") {
                var dateParts = LetterDate.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var xd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {

                var xd = "";
            }
            var ReceivingDate = document.getElementById('<%= txtInwardDate.ClientID%>').value.trim();
            if (ReceivingDate != "") {
                var dateParts = ReceivingDate.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {

                var yd = "";
            }
            if (yd < xd) {
                alert("Receiving date should be greater than or equal to Letter date");
                document.getElementById('<%= txtInwardDate.ClientID%>').value = "";
            }


        }
    </script>
</asp:Content>
