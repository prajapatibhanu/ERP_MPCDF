<%@ Page Title="" Language="C#" MasterPageFile="~/mis/RTIMaster.master" AutoEventWireup="true" CodeFile="RTIDetails.aspx.cs" Inherits="RTI_RTIApplicantsForms_RTIDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                <div class="col-md-6 ">
                    <!-- general form elements -->
                    <div class="box box-success " id="DetailDiv" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Request Details</h3>
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row" id="divFirstAppeal" runat="server">
                              <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">First Appeal Detail</legend>
                                        <div class="form-group">
                                            <asp:DetailsView ID="DetailsView2" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                               <Fields>
                                                   <asp:TemplateField HeaderText="Status">
                                                       <ItemTemplate>
                                                        <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_FAStatus").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                   </asp:TemplateField>  
                                                   <asp:BoundField DataField="RTI_RegistrationNo"  HeaderText="Registration No" />
                                                   <asp:BoundField DataField="RTI_FAUpdatedOn"  HeaderText="First Appeal Filed Date" />
                                                   <asp:BoundField DataField="RTI_FAGroundFor"  HeaderText="Ground for First Appeal" /> 
                                               </Fields>
                                            </asp:DetailsView>
                                        </div>
                                         <div class="form-group">
                                             <label>Comment :</label>
                                            <div id="RTIFADetails" runat="server" style="word-wrap: break-word; text-align: justify"></div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hyprRTI_FARequestDoc" Visible="true" runat="server" Text="Attachment" Target="_blank" ></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                </div>
                                <!--Start RTI Request Detail-->
                             <div class="row" id="divRTIRequest"  runat="server">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">RTI Detail</legend>
                                        <div class="form-group">
                                            <asp:DetailsView ID="DetailsView1" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                               <Fields>
                                                   <asp:TemplateField HeaderText="Status">
                                                       <ItemTemplate>
                                                        <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_Status").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                   </asp:TemplateField>  
                                                   <asp:BoundField DataField="RTI_RegistrationNo"  HeaderText="Registration No" />
                                                   <asp:BoundField DataField="RTI_UpdatedOn"  HeaderText="RTI Filed Date" />
                                                   <asp:BoundField DataField="RTI_Subject"  HeaderText="RTI Subject" />
                                               </Fields>
                                            </asp:DetailsView>
                                        </div>
                                        <div class="form-group">
                                            <label>Request :</label>
                                            <div id="RTIDetails" runat="server" style="word-wrap: break-word; text-align: justify"></div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hyprRTI_RequestDoc" Visible="true" runat="server" Text="Attachment" Target="_blank" ></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                             </div>
                              <div class="row" id="divRequestFirstAppeal"  runat="server">
                                <div class="bg-gray-light">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="form-group  pull-right">
                                                <label style="margin-right: 10px;">Apply For First Appeal (पहली अपील के लिए आवेदन करें)</label>
                                                <asp:CheckBox ID="chkForFirstAppeal" runat="server" OnCheckedChanged="chkForFirstAppeal_CheckedChanged" AutoPostBack="true" onchange="FirstAppealRequest()" ClientIDMode="Static" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- For First Appeal Request-->
                      
                                <div class="col-md-12 ReasonForFirstAppeal" id="ReasonForFirstAppeal" runat="server">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Reason For First Appeal</legend>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>GROUND FOR FIRST APPEAL (पहली अपील के लिए कारण):<span style="color: red;">*</span></label>
                                                            <asp:DropDownList ID="ddlRTI_FAGroundFor" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                <asp:ListItem Value="Refused access to information Requested">Refused access to information Requested</asp:ListItem>
                                                                <asp:ListItem Value="No Response Within the Time Limit">No Response Within the Time Limit</asp:ListItem>
                                                                <asp:ListItem Value="Unreasonable amount of Fee required to Pay">Unreasonable amount of Fee required to Pay</asp:ListItem>
                                                                <asp:ListItem Value="Provided Incomplete, Mislesading or False Information">Provided Incomplete, Mislesading or False Information</asp:ListItem>
                                                                <asp:ListItem Value="Any other ground">Any other ground</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <small><span id="valddlRTI_FAGroundFor" class="text-danger"></span></small>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Comment (टिप्पणी):</label>
                                                            <asp:TextBox ID="txtRTI_FAComment" runat="server" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine"  onkeyDown="checkTextAreaMaxLength(this,event,'199');" MaxLength="199"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Upload Document (दस्तावेज अपलोड करें):</label>
                                                            <asp:FileUpload ID="fuRTI_FARequestDoc" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                       <small><span id="valfuRTI_FARequestDoc" class="text-danger"></span></small>
                                                             </div>
                                                    </div>

                                                    <div class="row pull-right">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" Text="Send Request" CssClass="btn btn-success form-control" ID="btnSendRequest" ClientIDMode="Static" OnClick="btnSendRequest_Click" OnClientClick="return validate()" />
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
                                            <asp:DetailsView ID="DetailsView3" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                <Fields>
                                                    <%--<asp:TemplateField HeaderText="Status">
                                                       <ItemTemplate>
                                                        <asp:Label ID="Label1" Text='<%# Eval("RTI_Status").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                   </asp:TemplateField> --%>
                                                    <asp:BoundField DataField="App_Name" HeaderText="Applicant Name" />
                                                    <asp:BoundField DataField="App_UserType" HeaderText="Applicant Type" />
                                                    <asp:BoundField DataField="App_MobileNo" HeaderText="Mobile No" />
                                                    <asp:BoundField DataField="App_Email" HeaderText="Email" />
                                                    <asp:BoundField DataField="App_Address" HeaderText="Address" />
                                                    <asp:BoundField DataField="District_Name" HeaderText="District" />
                                                </Fields>
                                            </asp:DetailsView>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                              </div>
                        </div>

                    </div>
               

                <div class="col-md-6 ">
                    <div class="box box-success direct-chat">
                        <div class="box-header with-border">
                            <h3 class="box-title">Departmental Reply</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- Conversations are loaded here -->
                            <div class="row">
                                <div class="col-md-12">
                                    
                                    
                                    <!-- DIRECT CHAT -->
                                        <!-- Conversations are loaded here -->
                                        <div class="direct-chat-messages" style="height: 100%;">
                                            <div class="form-group">
                                         <asp:Label ID="lblDepartmentRecord" runat="server" ClientIDMode="Static"></asp:Label>
                                        </div>
                                           <%-- <fieldset>
                                                <legend>RTI REQUEST REPLY</legend>
                                            <br />  </fieldset>--%>
                                                <div id="dvChat" runat="server"></div>  <!--For Chat-->
                                              
                                        </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <!-- /.box-footer-->
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
      //  $("txtRTI_FAComment").MaxLength({ MaxLength: 10 });
        function FirstAppealRequest()
        {
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
                }
           
        }
        function validate() {
            $("#valddlRTI_FAGroundFor").html("");
            $("#valfuRTI_FARequestDoc").html("");
            debugger;
            var msg = "";
            if (document.getElementById("ddlRTI_FAGroundFor").selectedIndex == 0) {
                msg += "GROUND FOR FIRST APPEAL (पहली अपील के लिए कारण)\n";
                $("#valddlRTI_FAGroundFor").html("GROUND FOR FIRST APPEAL (पहली अपील के लिए कारण)");
            }
            
            if (msg != "") {
                alert(msg);
                return false
            }
            if (msg == "") {
                return true
            }

        }

        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------
            $("#valddlRTI_FAGroundFor").html("");
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
                        $("#valfuRTI_FARequestDoc").html("File Name Should be less than " + maxLengthFileName + " characters. \n");
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            $("#valfuRTI_FARequestDoc").html("Special character not allowed in file name. \n");
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
                            $("#valfuRTI_FARequestDoc").html("File Format Is Not Correct (Only " + strValidFormates + ").\n");
                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
                    $("#valfuRTI_FARequestDoc").html("File Name is incorrect");
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
            $("#valddlRTI_FAGroundFor").html("");
            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 20971520) {
                alert('File size should not greater than 5 mb.');
                $("#valfuRTI_FARequestDoc").html("");
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>

