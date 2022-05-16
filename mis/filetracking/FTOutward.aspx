<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTOutward.aspx.cs" Inherits="mis_filetracking_FTOutward" %>

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
                            <h3 class="box-title">Outward Letter</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Dispatch Date (प्रेषित दिनांक)<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDispatchDate" runat="server" placeholder="Select Dispatch Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtDispatchDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Letter No (पत्र संख्या)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtLetterNo" runat="server" placeholder="Enter Letter No." class="form-control" ClientIDMode="Static" Style="text-transform: uppercase" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtLetter_No" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Letter Subject (पत्र विषय)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtLetterSubject" runat="server" placeholder="Enter Letter Subject" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtLetterSubject" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>CC Number (कार्बन कॉपी नंबर)</label>
                                        <asp:TextBox ID="txtEndorsementNumber" runat="server" placeholder="Enter CC Number" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtEndorsementNumber" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Letter Receive From (से प्राप्त करें)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtLetterReceiveFrom" runat="server" placeholder="Enter Letter Receive From" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtLetterReceiveFrom" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address to (को संबोधित)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtAddressTo" runat="server" placeholder="Enter Address to" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtAddressTo" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Remark (टिप्पणी)</label><span style="color: red;">*</span>
                                                <asp:TextBox ID="txtRemark" runat="server" class="form-control" Rows="6" TextMode="MultiLine" MaxLength="200" placeholder="Enter Remark..." ClientIDMode="Static"></asp:TextBox>
                                                <small><span id="valtxtRemark" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Forward to Department (विभाग)<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtForwardDepartment" runat="server" Rows="2" TextMode="MultiLine" class="form-control" placeholder="Forward to Department..." autocomplete="off"></asp:TextBox>
                                                <small><span id="valtxtForwardDepartment" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Forward to Officer (अफ़सर)<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtForwardOfficer" runat="server" class="form-control" Rows="2" TextMode="MultiLine" placeholder="Forward to Officer..." autocomplete="off"></asp:TextBox>
                                                <small><span id="valtxtForwardOfficer" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Upload File (दस्तावेज अपलोड करें)</label>
                                                <asp:HyperLink ID="hyprDoc" Target="_blank" runat="server" Text="VIEW"></asp:HyperLink><br />
                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div id="DivCopyTo" runat="server">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Letter Copy To</label>
                                                <asp:TextBox ID="txtCopyTo" CssClass="form-control" runat="server" placeholder="Enter Copy To" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" Style="margin-top: 22px;" runat="server" CssClass="btn btn-success" Text="ADD" OnClick="btnAdd_Click" OnClientClick="return validate();" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="15">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Letter Copy To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCopyTo" runat="server" Text='<%# Eval("CopyTo").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="25">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" CssClass="label label-default" Text="Delete" runat="server" OnClick="OnDelete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            <div class="form-group"></div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnForward" runat="server" class="btn btn-block btn-success" Text="Forward" OnClientClick="return validateform();" OnClick="btnForward_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FTOutward.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                                <div class="col-md-4">
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
    <script type="text/javascript">
        function validateform() {
            debugger;
            var msg = "";
            var FileType = "";
            $("#valtxtDispatchDate").html("");
            $("#valtxtLetter_No").html("");
            $("#valtxtLetterSubject").html("");
            $("#valtxtLetterReceiveFrom").html("");
            $("#valtxtAddressTo").html("");
            $("#valtxtRemark").html("");
            $("#valtxtForwardDepartment").html("");
            $("#valtxtForwardOfficer").html("");

            if (document.getElementById('<%=txtDispatchDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Dispatch Date. \n";
                $("#valtxtDispatchDate").html("Select Dispatch Date.");
            }
            if (document.getElementById('<%=txtLetterNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter No. \n";
                $("#valtxtLetter_No").html("Enter Letter No.");
            }
            if (document.getElementById('<%=txtLetterSubject.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter Subject. \n";
                $("#valtxtLetterSubject").html("Enter Letter Subject.");
            }
            if (document.getElementById('<%=txtLetterReceiveFrom.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter Receive From. \n";
                $("#valtxtLetterReceiveFrom").html("Enter Letter Receive From..");
            }
            if (document.getElementById('<%=txtAddressTo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Address To. \n";
                $("#valtxtAddressTo").html("Enter Address To.");
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Remark. \n";
                $("#valtxtRemark").html("Enter Remark.");
            }
            if (document.getElementById('<%=txtForwardDepartment.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Forward To Department. \n";
                $("#valtxtForwardDepartment").html("Enter Forward To Department.");
            }
            if (document.getElementById('<%=txtForwardOfficer.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Forward To Officer. \n";
                $("#valtxtForwardOfficer").html("Enter Forward To Officer.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnForward.ClientID%>').value.trim() == "Forward") {
                    if (confirm("Do you really want to Forward Letter ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function validate() {
            debugger;
            var msg = "";
            var FileType = "";
            if (document.getElementById('<%=txtCopyTo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter Copy To. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            <%--else {
                if (document.getElementById('<%=btnForward.ClientID%>').value.trim() == "Forward") {
                    if (confirm("Do you really want to Forward Letter ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }--%>
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
