<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GrievanceRequest.aspx.cs" Inherits="Grievance_GrievanceRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Grievance/Feedback Detail</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Grievance Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="false" class="table table-striped table-bordered">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Grievance Status" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Application_GrvStatus").ToString()%>' runat="server" ID="Application_GrvStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ApplicationApply_Date" HeaderText="Apply Date" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Application_RefNo" HeaderText="Reference No / Complaint No" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="District_Name" HeaderText="District Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Location" HeaderText="Address" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Application_GrievanceType" HeaderText="Grievance Type" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Complaint_Name" HeaderText="Complaint Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Font-Bold="true" />
                                            </Fields>
                                        </asp:DetailsView>
                                        <div class="form-group">
                                            <label>Grievance Description</label>
                                            <asp:TextBox ID="Application_Descritption" runat="server" TextMode="MultiLine" Rows="10" Width="100%" Enabled="false"></asp:TextBox>
                                            <%--<div id="Application_Descritption" runat="server" style="word-wrap: break-word; text-align: justify">--%>
                                        </div>
                                        <div class="pull-right">
                                            <div class="form-group">
                                                <asp:HyperLink ID="hyprApplication_Doc1" Visible="true" runat="server" CommandName="View" Text="Attachment 1 " Target="_blank"></asp:HyperLink>
                                                |
                                                    <asp:HyperLink ID="hyprApplication_Doc2" Visible="true" runat="server" CommandName="View" Text="Attachment 2" Target="_blank"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Reply to Citizen</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="direct-chat-messages" style="height: 100%;">
                                        <div id="dvChat" runat="server"></div>
                                        <!--For Chat-->
                                    </div>
                                </div>
                            </div>
                            <div id="DivReply" runat="server">
                                <fieldset>
                                    <legend>Reply</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Status<span style="color: red;"> *</span></label>
                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Open</asp:ListItem>
                                                    <asp:ListItem>Close</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlStatus" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Document(JPEG*PNG*JPG*PDF*DOC)</label>
                                                <asp:Label ID="lblFUReply_SuppDoc" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                                <asp:FileUpload ID="FUReply_SuppDoc" CssClass="form-control" runat="server" ClientIDMode="Static" onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                                <small><span id="valFUReply_SuppDoc" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Remark(टिप्पणी)</label><span style="color: red">*</span>
                                                <asp:TextBox runat="server" placeholder="Remark(टिप्पणी)" ID="txtRemark" MaxLength="25" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                                                <small><span id="valtxtRemark" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button ID="BtnReply" CssClass="btn btn-block btn-success" runat="server" Text="Reply" OnClientClick="return validateform();" OnClick="BtnReply_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div id="DivInternal" runat="server">
                            <div class="box-header with-border">
                                <h3 class="box-title">Internal Discussion </h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="BtnAddOfficer" runat="server" CssClass="btn btn-info btn-block" CausesValidation="False" Text="आंतरिक चर्चा के लिए अधिकारी जोड़ें" CommandName="select" OnClick="BtnAddOfficer_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div id="DivDepartmentReply" runat="server">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label3" runat="server" Text="" Visible="true"></asp:Label>
                                        <fieldset>
                                            <legend>Reply</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Remark<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" ID="txtChat_Remark" CssClass="form-control" ClientIDMode="Static" placeholder="Remark" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                        <small><span id="valtxtChat_Remark" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Document(JPEG*PNG*JPG*PDF*DOC)</label>
                                                        <asp:Label ID="lblfuChat_Doc1" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                                        <asp:FileUpload ID="fuChat_Doc1" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="uploadDocinternal(),ValidateFileSize(this,1024*1024)" />
                                                        <small><span id="valfuChat_Doc1" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Document(JPEG*PNG*JPG*PDF*DOC)</label>
                                                        <asp:Label ID="lblfuChat_Doc2" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                                        <asp:FileUpload ID="fuChat_Doc2" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="uploadDocinternal(),ValidateFileSize(this,1024*1024)" />
                                                        <small><span id="valfuChat_Doc2" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnInternalDiscussion" runat="server" class="btn btn-success btn-block" Text="Internal Discussion" OnClick="btnInternalDiscussion_Click" OnClientClick="return validateInternalDiss()" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <fieldset>
                                        <legend>Comments</legend>
                                        <asp:Label ID="lblCommentRecord" runat="server" Text=""></asp:Label>
                                        <div class="direct-chat-messages" style="height: 100%;">
                                            <div id="divchat" runat="server"></div>
                                            <!--For Chat-->
                                        </div>
                                    </fieldset>
                                </div>
                            </div>

                        </div>
                        <!-- /.box-body -->
                    </div>

                    <div id="AddOfficerModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header  bg-gray-light">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Forward To</h4>
                                </div>
                                <div class="modal-body bg-gray-light">
                                    <%-- <asp:Label ID="lblModal" runat="server" Text="" ForeColor="Red"></asp:Label>--%>

                                    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>--%>
                                    <asp:Label ID="Label4" runat="server" Text="" Visible="true"></asp:Label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>अधिकारी का नाम<span style="color: red;"> *</span></label>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlFrwd_OfficerName" Style='width: 300px;' runat="server" class="form-control select2" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlFrwd_OfficerName" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6 hidden">
                                            <div class="form-group">
                                                <label>विभाग का नाम <span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlFrwd_Department" runat="server" class="form-control" OnSelectedIndexChanged="ddlFrwd_Department_SelectedIndexChanged1" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <small><span id="valddlFrwd_Department" class="text-danger"></span></small>
                                            </div>
                                        </div>

                                    </div>
                                    <%--  </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                    <div class="row">
                                        <div class="col-md-8"></div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" runat="server" class="btn btn-success btn-block" Text="Add" OnClick="btnAdd_Click" OnClientClick="return validateAdd()" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>Officer List</label><br />
                                            <asp:Label ID="lblGridRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" DataKeyNames="Forward_ID" CssClass="table table-responsive table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Emp_Name" HeaderText="Officer Name" />
                                                        <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No." />
                                                        <%-- <asp:CommandField SelectText="Delete" HeaderText="Delete" ShowDeleteButton="true" />--%>
                                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Department will be deleted. Are you sure want to continue?');"></asp:LinkButton>
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
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function callalert() {
            debugger;
            $("#myModal").modal('show');

        }
        function openModal() {
            debugger
            $('#AddOfficerModal').modal('show');
        };

        //Reply to Citizen
        function validateform() {
            var msg = "";
            $("#valddlStatus").html("");
            $("#valtxtRemark").html("");
            if (document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Status Type. \n";
                $("#valddlStatus").html("Select Status Type.");
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "please Enter Remark. \n";
                $("#valtxtRemark").html("please Enter Remark.");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=BtnReply.ClientID%>').value.trim() == "Reply") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function uploadDoc() {
            if (document.getElementById('<%=FUReply_SuppDoc.ClientID%>').files.length != 0) {
                var ext = $('#FUReply_SuppDoc').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'doc', 'docx']) == -1) {
                    $("#valFUReply_SuppDoc").html("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                    document.getElementById('FUReply_SuppDoc').value = "";
                }
                else {
                    $('#lblFUReply_SuppDoc').text("");
                }
            }
            else {
                $('#lblFUReply_SuppDoc').text("");
            }

        }
        //END


        // Start Internal Discussion
        function validateInternalDiss() {
            $("#valtxtChat_Remark").html("");
            $("#valfuChat_Doc1").html("");
            $("#valfuChat_Doc2").html("");
            var msg = "";

            if (document.getElementById('<%=txtChat_Remark.ClientID%>').value.trim() == "") {
                msg = msg + "please Enter Remark. \n";
                $("#valtxtChat_Remark").html("please Enter Remark.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnInternalDiscussion.ClientID%>').value.trim() == "Internal Discussion") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function uploadDocinternal() {
            if (document.getElementById('<%=fuChat_Doc1.ClientID%>').files.length != 0) {
                var ext = $('#fuChat_Doc1').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'doc', 'docx']) == -1) {
                    $("#valfuChat_Doc1").html("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                    document.getElementById('fuChat_Doc1').value = "";
                }
                else {
                    $('#lblfuChat_Doc1').text("");
                }
            }
            else {
                $('#lblfuChat_Doc1').text("");
            }

            if (document.getElementById('<%=fuChat_Doc2.ClientID%>').files.length != 0) {
                var ext = $('#fuChat_Doc2').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'doc', 'docx']) == -1) {
                    $("#valfuChat_Doc2").html("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                    document.getElementById('fuChat_Doc2').value = "";
                }
                else {
                    $('#lblfuChat_Doc2').text("");
                }
            }
            else {
                $('#lblfuChat_Doc2').text("");
            }
        }
        function ValidateFileSize(a, size) {
            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > size) {
                alert('File size should not greater than' + size / 1024 + ' kb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
        function OnlyNumbers(event) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        //INTERNALEND


        function validateAdd() {
            debugger;
            //$("#valtxtRply_RTIRemark").html("");
            //$("#valfuRply_RTIDoc1").html("");
            //$("#valddlRply_Status").html("");
            //$("#valtxtChat_Remark").html("");
            //$("#valfuChat_Doc1").html("");
            $("#valddlAdd_Department").html("");
            $("#valddlFrwd_OfficerName").html("");
            var msg = "";
            // if (document.getElementById('<%=ddlFrwd_Department.ClientID%>').selectedIndex == 0) {
            //msg += "Select Department\n";
            // $("#valddlFrwd_Department").html("Select Department");
            // }
            if (document.getElementById('<%=ddlFrwd_OfficerName.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlFrwd_OfficerName.ClientID%>').selectedIndex < 0) {
                msg += "Select Officer Name\n";
                $("#valddlFrwd_OfficerName").html("Select Officer Name");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnAdd.ClientID%>').value.trim() == "Add") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                <%--if (document.getElementById('<%=btnAdd.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }--%>
            }
        }
        function HideLabel() {
            var seconds = 10;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function ValidateFileSize(a, size) {
            // 1 kb =(size=1024) 
            // 1 mb =(size=1024 * 1024 * 1) 

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > size) {
                alert('File size should not greater than' + size / 1024 + ' kb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
        function HideLabel() {
            var seconds = 10;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };


    </script>

</asp:Content>

