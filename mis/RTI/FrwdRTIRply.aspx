<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FrwdRTIRply.aspx.cs" Inherits="mis_RTI_FrwdRTIRply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) --
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">RTI Request Details</h3>

                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div id="divFirstAppeal" runat="server">
                                <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">First Appeal Detail</legend>
                                        <div class="form-group">
                                            <asp:DetailsView ID="DetailsView3" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                <Fields>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_FAStatus").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RTI_RegistrationNo" HeaderText="Registration No" />
                                                    <asp:BoundField DataField="RTI_FAUpdatedOn" HeaderText="First Appeal Filed Date" />
                                                    <asp:BoundField DataField="RTI_FAGroundFor" HeaderText="Ground for First Appeal" />
                                                </Fields>
                                            </asp:DetailsView>
                                        </div>
                                        <div class="form-group">
                                            <label>Comment</label>
                                            <div id="RTIFADetails" runat="server" style="word-wrap: break-word; text-align: justify"></div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hyprRTI_FARequestDoc" Visible="true" runat="server" Text="Attachment" Target="_blank"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">RTI Detail</legend>

                                        <div class="form-group">
                                            <div class="row" id="divRTIRequest" runat="server">
                                                <div class="col-md-12">

                                                    <div class="form-group">
                                                        <asp:DetailsView ID="DetailsView1" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                            <Fields>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_Status").ToString()%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="RTI_RegistrationNo" HeaderText="Registration No" />
                                                                <asp:BoundField DataField="RTI_UpdatedOn" HeaderText="RTI Filed Date" />
                                                                <asp:BoundField DataField="RTI_Subject" HeaderText="RTI Subject" />
                                                            </Fields>
                                                        </asp:DetailsView>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Request</label>
                                                        <div id="RTIDetails" runat="server" style="word-wrap: break-word; text-align: justify"></div>
                                                        <div class="pull-right">
                                                            <div class="form-group">
                                                                <asp:HyperLink ID="hyprRTI_RequestDoc" Visible="true" runat="server" Text="Attachment" Target="_blank"></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Applicant Detail</legend>
                                        <div class="form-group">
                                            <asp:DetailsView ID="DetailsView2" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                <Fields>
                                                    <%--<asp:TemplateField HeaderText="Status">
                                                       <ItemTemplate>
                                                        <asp:Label ID="Label1" Text='<%# Eval("RTI_Status").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                   </asp:TemplateField> --%>
                                                    <asp:BoundField DataField="App_Name" HeaderText="Applicant Name" />
                                                    <asp:BoundField DataField="RTI_RegistrationNo" HeaderText="Registration No" />
                                                    <asp:BoundField DataField="App_UserType" HeaderText="Applicant Type" />
                                                    <asp:BoundField DataField="App_MobileNo" HeaderText="Mobile No" />
                                                    <asp:BoundField DataField="App_Email" HeaderText="Email" />
                                                    <asp:BoundField DataField="App_Gender" HeaderText="Gender" />
                                                    <asp:BoundField DataField="App_Address" HeaderText="Address" />
                                                    <asp:BoundField DataField="App_Pincode" HeaderText="Pincode" />
                                                    <asp:BoundField DataField="Block_Name" HeaderText="Block" />
                                                    <asp:BoundField DataField="District_Name" HeaderText="District" />
                                                    <asp:BoundField DataField="State_Name" HeaderText="State" />
                                                    <asp:BoundField DataField="App_PLStatus" HeaderText="BPL Status" />
                                                    <asp:TemplateField HeaderText="BPL Card No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApp_BPLCardNo" runat="server" Text='<%# Eval("App_BPLCardNo").ToString()%>' Visible='<% #Eval("App_BPLCardNo").ToString() != "" ? true : false%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year Of Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_YearOfIssue" runat="server" Text='<% #Eval("App_YearOfIssue").ToString()%>' Visible='<% #Eval("App_YearOfIssue") != "" ? true : false%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issuing Authority">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_IssuingAuthority" runat="server" Text='<% #Eval("App_IssuingAuthority").ToString()%>' Visible='<% #Eval("App_IssuingAuthority") != "" ? true : false%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Fields>
                                            </asp:DetailsView>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                
                    <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Internal Discussion </h3>
                           
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                        <asp:Label ID="lblCommentRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <div class="direct-chat-messages" style="height: 100%;">
                                            <div id="divChat" runat="server"></div>
                                            <!--For Chat-->
                                        </div>
                                     </div>
                            </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
                                        <!-- form start -->
                            <div id="divIntlRply" runat="server">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset>
                                                        <legend>Reply</legend>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Remark<span style="color: red;"> *</span></label>
                                                                    <asp:TextBox runat="server" ID="txtChat_Remark" CssClass="form-control" ClientIDMode="Static" placeholder="Remark" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                                    <small><span id="valtChat_Remark" class="text-danger"></span></small>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Upload Document</label>
                                                                    <asp:FileUpload ID="fuChat_Doc1" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Upload Document</label>
                                                                    <asp:FileUpload ID="fuChat_Doc2" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <small><span id="valfuRply_RTIDoc3" class="text-danger"></span></small>

                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnInternalDiscussion" runat="server" class="btn btn-success btn-block" Text="Internal Discussion" OnClientClick="return validateInternalDiss()" OnClick="btnInternalDiscussion_Click" />
                                                                    <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                               </div>
                        </div>
                        <!-- /.box-body -->
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
    <script>
        function validateInternalDiss() {
            $("#valtChat_Remark").html("");
            $("#valfuRply_RTIDoc3").html("");
            debugger;
            var msg = "";
            if (document.getElementById("txtChat_Remark").value == "") {
                msg += "Enter Remark\n";
                $("#valtChat_Remark").html("Enter Remark");
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnInternalDiscussion.ClientID%>').value.trim() == "Internal Discussion") {
                    if (confirm("Do you really want to Save Remark ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function HideLabel() {
            var seconds = 10;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
            };

            function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
                //ex---------------
                //maxLengthFileName=50;
                //validFileFormaString=JPG*JPEG*PDF*DOCX
                //uploadControlId=upSaveBill
                //ex---------------
               
                if (document.getElementById("txtChat_Remark").value == "") {
                    msg += "Enter Remark\n";
                    $("#valtChat_Remark").html("Enter Remark");
                } else {
                    $("#valtChat_Remark").html("");
                }
                $("#valfuRply_RTIDoc3").html("");
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
                            $("#valfuRply_RTIDoc3").html("File Name Should be less than " + maxLengthFileName + " characters. \n");
                        }
                        for (i = 0; i <= (fileName.length - 1) ; i++) {
                            var charFileName = '';

                            charFileName = fileName.substring(i, i + 1);

                            if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                                msg += '- Special character not allowed in file name. \n';
                                $("#valfuRply_RTIDoc3").html("Special character not allowed in file name. \n");
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
                                $("#valfuRply_RTIDoc3").html("File Format Is Not Correct (Only " + strValidFormates + ").\n");
                            }
                        }

                    }
                    else {
                        msg += '- File Name is incorrect';
                        $("#valfuRply_RTIDoc3").html("File Name is incorrect");
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
                    $("#valfuRply_RTIDoc1").html("File size should not greater than 5 mb.");
                    document.getElementById(a.id).value = '';
                    return false;
                }
                else {
                    return true;
                }

            }
    </script>
</asp:Content>

