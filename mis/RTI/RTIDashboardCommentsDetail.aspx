<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RTIDashboardCommentsDetail.aspx.cs" Inherits="mis_RTI_RTIDashboardCommentsDetail" %>

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
                                                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="30%">
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
                                                        <asp:TextBox ID="RTIDetails" runat="server" Enabled="false" Rows="15" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                        <%--<div id="RTIDetails" runat="server" style="word-wrap: break-word; text-align: justify"></div>--%>
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

                            <div class="row" id="divPaymentDetail" runat="server">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Payment Detail</legend>

                                        <div class="form-group">
                                            <div class="row" id="div1" runat="server">
                                                <div class="col-md-12">

                                                    <div class="form-group">
                                                        <asp:DetailsView ID="DetailsView3" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                            <Fields>
                                                                <asp:TemplateField HeaderText="PO / Receipt No." HeaderStyle-Width="30%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_POReceiptNo").ToString()%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:BoundField DataField="RTI_POReceiptNo" HeaderText="PO / Receipt No." />--%>
                                                                <asp:BoundField DataField="RTI_PaymentMode" HeaderText="Payment Mode" />
                                                                <asp:BoundField DataField="RTI_Amount" HeaderText="Fees" />
                                                            </Fields>
                                                        </asp:DetailsView>
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
                                                    <asp:TemplateField HeaderText="BPL Card No" HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApp_BPLCardNo" runat="server" Text='<% #Eval("App_BPLCardNo").ToString() == "" ? "-" : Eval("App_BPLCardNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year Of Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_YearOfIssue" runat="server" Text='<% #Eval("App_YearOfIssue") != "" ? Eval("App_YearOfIssue") : "-"%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issuing Authority">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_IssuingAuthority" runat="server" Text='<% #Eval("App_IssuingAuthority") != "" ? Eval("App_IssuingAuthority") : "-"%>'></asp:Label>
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
                            <h3 class="box-title" id="Label2">Comments From Officers</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <asp:Label ID="lblRTIReply" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="dvRTIRplyComment" runat="server">
                                        <fieldset>
                                            <legend>Comments</legend>
                                            <div class="direct-chat-messages" style="height: 100%;">
                                                <div id="dvChat" runat="server"></div>
                                                <!--For Chat-->
                                            </div>
                                        </fieldset>
                                    </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label3">Comments of Internal Discussion</h3>
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- Internal Discussion-->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <asp:Label ID="lblCommentRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div id="divInternalDiscussion" runat="server">
                                        <div class="direct-chat-messages" style="height: 100%;">
                                            <div id="divchat" runat="server"></div>
                                        </div>
                                    </div>
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
        function openModal() {
            debugger
            //$('#AddOfficerModal').modal('show');
            $('#AddOfficerModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            })
        };
        function openModal1() {
            debugger
            //$('#AddOfficerModal').modal('show');
            $('#ApplyForFAModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            })
        };
        function HideLabel() {
            var seconds = 10;
            setTimeout(function () {
                debugger
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
                document.getElementById("<%=lblCommentRecord.ClientID %>").style.display = "none";

            }, seconds * 1000);
        };

        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------
            $("#valtxtRply_RTIRemark").html("");

            $("#valfuRply_RTIDoc1").html("");
            $("#valfuChat_Doc1").html("");
            $("#valddlRply_Status").html("");
            debugger
            var msg = '';
            var uploadfield = "";
            var fileplace = that.id;
            if (fileplace == 'fuRTI_FARequestDoc') {
                uploadfield = 'valfuRTI_FARequestDoc';
            }
            else {
                uploadfield = 'valfuRply_RTIDoc1';
            }
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
                        $("#" + uploadfield).html("File Name Should be less than " + maxLengthFileName + " characters. \n");
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            $("#" + uploadfield).html("Special character not allowed in file name. \n");
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
                            var id = that.id;
                            if (id == "fuChat_Doc1" || id == "fuChat_Doc2" || id == 'fuRTI_FARequestDoc') {
                                $("#" + uploadfield).html("File Format Is Not Correct (Only " + strValidFormates + ").\n");
                            }
                            else {
                                $("#" + uploadfield).html("File Format Is Not Correct (Only " + strValidFormates + ").\n");
                            }

                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
                    var id = that.id;
                    if (id == "fuChat_Doc1" || id == "fuChat_Doc2" || id == 'fuRTI_FARequestDoc') {
                        $("#" + uploadfield).html("File Name is incorrect.\n");
                    }
                    else {
                        $("#" + uploadfield).html("File Name is incorrect.\n");
                    }
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

        // For First Appeal Request

        function FirstAppealRequest() {
            debugger
            var chkFirstAppeal = document.getElementById("chkForFirstAppeal");
            // if (chkFirstAppeal.checked)
            // {
            var aa = document.getElementsByClassName('ReasonForFirstAppeal');
            //aa.style.display = chkFirstAppeal.checked ? "block" : "none";
            //$("#ReasonForFirstAppeal").show();
            //ReasonForFirstAppeal.style.display = none;
            //  }
            // else {
            // $('#ReasonForFirstAppeal').fadeOut('slow');
            //ReasonForFirstAppeal.style.visibility = true;
            // }
            //  ReasonForFirstAppeal.style.visibility = chkFirstAppeal.checked ? "visible" : "hidden";

        }

        function checkTextAreaMaxLength(textBox, e, length) {
            debugger
            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);

            if (textBox.value.length > maxLength - 1) {
                alert("Comment should be completed within 200 characters.");
                return false;
            }

        }
    </script>
</asp:Content>

