<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="mis_SendMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/bootstrap3-wysihtml5.min.css" rel="stylesheet" />
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">SEND EMAIL / CIRCULAR</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Add more receipents</label>
                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Use Comma , to separate Email address " class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Subject<span style="color: red;"> *</span></label>
                                <asp:TextBox ID="txtSubject" runat="server" placeholder="Enter Subject" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Show to Main page </label>
                                <br />
                                <asp:CheckBox ID="chkbox" runat="server"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <textarea id="txtMessage" runat="server" class="form-control composetextarea" style="height: 300px"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="btn btn-default btn-file">
                                    <i class="fa fa-paperclip"></i>Attachment
                                        <asp:FileUpload ID="FU_Attachment" runat="server" name="attachment" onchange="ValidateFileSize(this)" />
                                </div>
                                <p class="help-block">Max. 24MB</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSend" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSend_Click" Text="SEND" OnClientClick="return validateform();" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-block btn-default" OnClick="btnCancel_Click" OnClientClick="return Cancel();" />
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOffice" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvDetails" DataKeyNames="" AutoGenerateColumns="false" runat="server" class="table table-hover table-bordered pagination-ys">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="30">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Employee Name" DataField="Emp_Name" />
                                        <asp:BoundField HeaderText="Employee Code" DataField="UserName" />
                                        <asp:BoundField HeaderText="Email Address" DataField="Emp_Email" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="js/bootstrap3-wysihtml5.all.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".composetextarea").wysihtml5();
        });
        function ValidateFileSize(a) {

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 1024 * 1024 * 24) {
                alert('File size should not greater than 24 mb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function validateform() {
            var msg = "";
            <%--if (document.getElementById('<%=txtEmail.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Email. \n";
            }--%>
            var txtemail = document.getElementById('<%=txtEmail.ClientID%>').value.trim();
            if (txtemail !== "") {
                var emails = txtemail.split(',');
                var index = 0;
                var k = emails.length;
                if (k != 1) {
                    for (i = 0; i < k; i++) {

                        var regMail = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/;
                        if (regMail.test(emails[i]) == false && emails[i] != "") {
                            if (index == 0) {
                                msg += "invalid Email : " + emails[i];
                                index = 1;
                            }
                            else
                                msg += "," + emails[i];
                        }

                    }
                    msg += "\n";
                }
                else {
                    var regMail = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/;
                    if (regMail.test(txtemail) == false) {
                        msg += "invalid Email : " + txtemail;
                    }
                    msg += "\n";
                }
            }
            if (document.getElementById('<%=txtSubject.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subject. \n";
            }
            if (document.getElementById('<%=txtMessage.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Message. \n";
            }

            if (msg.trim() != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Send Email ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        <%-- function CheckLength(evt) {
            if (document.getElementById('<%=txtsms.ClientID%>').value.length <= 140) {
                document.getElementById('smssize').innerHTML = 140 - document.getElementById('<%=txtsms.ClientID%>').value.length;
                return true;
            }
            else {
                if (evt.keyCode == 8) {
                    document.getElementById('smssize').innerHTML = "1";
                    return true;
                }
                else {
                    return false;
                }
            }

        }--%>
    </script>
</asp:Content>

