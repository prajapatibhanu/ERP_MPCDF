﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTForwardedByOperator.aspx.cs" Inherits="mis_filetracking_FTForwardedByOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 0px;">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success" style="min-height: 0px;">
                        <div class="box-header with-border">
                            <h3 class="box-title">फ़ाइल / नोट शीट/ पत्र विवरण</h3>
                            <asp:Label ID="Label3" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>File No./ BAR Code</label>
                                        <asp:TextBox runat="server" ID="txtFileNo" CssClass="form-control" placeholder="Enter File No. / BAR Code"></asp:TextBox>
                                        <small><span id="valtxtFileNo" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateFileNo();"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="FTForwardedByOperator.aspx" runat="server" class="btn btn-default btn-block">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblgridMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="divDetail" runat="server">
                    <div class="col-md-6">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h3 class="box-title" id="Label1">फ़ाइल / नोट शीट/ पत्र विवरण</h3>
                                <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <fieldset>
                                    <legend>NoteSheet</legend>
                                    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                        <Fields>
                                            <asp:TemplateField HeaderText="प्राथमिकता">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="File_No" HeaderText="फ़ाइल / नोट शीट/ पत्र संख्या" />
                                            <asp:BoundField DataField="File_Type" HeaderText="फाइल का प्रकार" />
                                            <asp:BoundField DataField="File_UpdatedOn" HeaderText="फाइल बनने की दिनांक" />
                                            <asp:BoundField DataField="Department_Name" HeaderText="विभाग" />
                                            <asp:BoundField DataField="File_Title" HeaderText="दस्तावेज़ का शीर्षक" />
                                            <asp:BoundField DataField="StatusOfFile" HeaderText="दस्तावेज़ का तरीका" />
                                            <asp:BoundField DataField="QRCode" HeaderText="बार कोड" />
                                            <asp:BoundField DataField="AddressTo" HeaderText="दस्तावेज़ संबोधित" />
                                            <asp:TemplateField HeaderText="सम्बंधित दस्तावेज़">
                                                <ItemTemplate>
                                                    <a href='<%# Eval("Document_Upload") %>' target="_blank" class="label label-info"><%# Eval("Document_Upload").ToString() != "" ? "View" : "" %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Fields>
                                    </asp:DetailsView>
                                    <div id="FileDescription" runat="server" style="background-color: #bff2d3;">
                                        <label id="lblDescription1" runat="server">फ़ाइल / नोट शीट/ पत्र विवरण</label><br />
                                        <span id="FileDescription1" runat="server"></span>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h3 class="box-title" id="Label2">अधिकारी से टिप्पणियां</h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12" id="divComments" runat="server">
                                        <div id="DivOfficerChat" runat="server">
                                            <%--<fieldset>
                                                <legend>Comments from Officers</legend>
                                                <div class="direct-chat-messages" style="height: 100%;">
                                                    <!-- Message. Default to the left -->
                                                    <div class="direct-chat-msg">
                                                        <div class="direct-chat-info clearfix">
                                                            <span class="direct-chat-name pull-left" id="DepartmentOfficer" runat="server"></span>
                                                            <span class="direct-chat-timestamp pull-right" id="ForwardDatetime" runat="server"></span>
                                                        </div>
                                                        <!-- /.direct-chat-info -->
                                                        <img class="direct-chat-img" src="../image/User1.png" alt="message user image" />
                                                        <!-- /.direct-chat-img -->
                                                        <div class="direct-chat-text form-group" style="background-color: #bff2d3;" id="CommentonFile" runat="server">
                                                            <div class="attachment text-right">
                                                                <a href="/Uploads/" id="Attachment1" target='blank'>Attachment 1</a>
                                                                <a href="/Uploads/" id="Attachment2" target='blank'>Attachment 2</a>
                                                            </div>
                                                        </div>
                                                        <span class="direct-chat-timestamp pull-right" id="Forwardedby" runat="server">himani</span>
                                                    </div>
                                                </div>
                                            </fieldset>--%>
                                        </div>
                                    </div>
                                    <div id="DivForward" runat="server">
                                        <fieldset>
                                            <legend>Forward</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label id="lblDescription" runat="server">नोट शीट / पत्र पर टिप्पणी</label>
                                                        <asp:TextBox runat="server" placeholder="टिप्पणी..." ID="txtForwarded_Description" Rows="10" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                                                        <small><span id="valtxtForwarded_Description" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>अधिकारी का नाम <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="ddlForwarded_Officer" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlForwarded_Officer_SelectedIndexChanged">
                                                            <asp:ListItem>चुने</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlForwarded_Officer" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>विभाग <span style="color: red;">*</span></label>
                                                        <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control"></asp:TextBox>
                                                        <small><span id="valtxtDepartment" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Forward Date (फाइल आगे बढाने की दिनांक)<span style="color: red;">*</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtForwardDate" runat="server" placeholder="Select Forward Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                        <small><span id="valtxtForwardDate" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Attachment 1</label>
                                                        <asp:FileUpload ID="Document_Upload1" runat="server" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Attachment 2</label>
                                                        <asp:FileUpload ID="Document_Upload2" runat="server" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnForward" runat="server" Text="Forward" CssClass="btn btn-success btn-block" OnClick="btnConfirm_Click" OnClientClick="return validateform();" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <%--<div id="myModal" class="modal fade" role="dialog">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                    <h4 class="modal-title">Confirm Forward</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>आपके मोबाइल पर भेजा गया OTP नंबर प्रविष्ट करे<span style="color: red;">*</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblModal" runat="server" Text="" Style="color: red;"></asp:Label></label>
                                                                <asp:TextBox ID="txtOTP" autocomplete="off" runat="server" placeholder="Enter OTP..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button ID="btnConfirm" class="btn btn-success pull-left" runat="server" Text="Confirm Forward" OnClick="btnConfirm_Click" OnClientClick="return validateConfirmform();" />
                                                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function callalert() {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false
            })
        }

        function validateFileNo() {
            var msg = "";
            $("#valtxtFileNo").html("");


            if (document.getElementById('<%=txtFileNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter File No./ BAR Code. \n";
                $("#valtxtFileNo").html("Enter File No./ BAR Code.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }

        function validateform() {
            var msg = "";
            $("#valtxtForwarded_Description").html("");
            $("#valtxtDepartment").html("");
            $("#valddlForwarded_Officer").html("");
            $("#valtxtForwardDate").html("");


           <%-- if (document.getElementById('<%=txtForwarded_Description.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Description. \n";
                $("#valtxtForwarded_Description").html("Enter Description.");
            }--%>
            if (document.getElementById('<%=ddlForwarded_Officer.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Officer. \n";
                $("#valddlForwarded_Officer").html("Select Officer.");
            }
            if (document.getElementById('<%=txtDepartment.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Forward Department. \n";
                $("#valtxtDepartment").html("Enter Forward Department.");
            }
            if (document.getElementById('<%=txtForwardDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Forward Date. \n";
                $("#valtxtForwardDate").html("Select Forward Date.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                //callalert();
                return true;
            }

        }

        <%--function validateConfirmform() {
            var msg = "";
            if (document.getElementById('<%=txtOTP.ClientID%>').value.trim() == "") {
                msg = msg + "Enter OTP. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnConfirm.ClientID%>').value.trim() == "Confirm Forward") {
                    if (confirm("Do you really want to Confirm Forward File ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }
        }--%>
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

