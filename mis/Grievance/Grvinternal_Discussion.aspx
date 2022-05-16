<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Grvinternal_Discussion.aspx.cs" Inherits="mis_Grievance_Grvinternal_Discussion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">

                <div class="col-md-6">

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Internal Discusssion Detail</h3>
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
                                                <asp:BoundField DataField="Application_RefNo" HeaderText="Complaint No" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="District_Name" HeaderText="District Name" HeaderStyle-Font-Bold="true" />                                              
                                                <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Font-Bold="true" />                                             
                                                <asp:BoundField DataField="Application_GrievanceType" HeaderText="Grievance Type" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Complaint_Name" HeaderText="Complaint Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Font-Bold="true" />
                                            </Fields>
                                        </asp:DetailsView>
                                        <div class="form-group">
                                            <label>Grievance Description</label>
                                            <div id="Application_Descritption" runat="server" style="word-wrap: break-word; text-align: justify">
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
                                </div>
                            </fieldset>
                           

                        </div>

                    </div>

                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Internal Discussion</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Comments</legend>
                                        <asp:Label ID="lblCommentRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <div class="direct-chat-messages" style="height: 100%;">
                                            <div id="divChat" runat="server"></div>
                                            <!--For Chat-->
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
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
                                                    <label>Document(JPEG*PNG*JPG*PDF*DOC)</label>
                                                    <asp:Label ID="lblfuChat_Doc1" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                                    <asp:FileUpload ID="fuChat_Doc1" runat="server" CssClass="form-control" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document(JPEG*PNG*JPG*PDF*DOC)</label>
                                                    <asp:Label ID="lblfuChat_Doc2" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                                    <asp:FileUpload ID="fuChat_Doc2" runat="server" CssClass="form-control" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)"/>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<small><span id="valfuRply_RTIDoc3" class="text-danger"></span></small>--%>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Button ID="btnInternalDiscussion" runat="server" class="btn btn-success btn-block" Text="Internal Discussion" OnClientClick="return validateform();" OnClick="btnInternalDiscussion_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
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
        function validateform() {
            var msg = "";
            $("#valddlStatus").html("");
            $("#valtxtRemark").html("");

            if (document.getElementById('<%=txtChat_Remark.ClientID%>').value.trim() == "") {
            msg = msg + "please Enter Remark. \n";
            $("#valtChat_Remark").html("please Enter Remark.");
        }

        if (msg != "") {
            alert(msg);
            return false;
        }
        else {
            if (document.getElementById('<%=btnInternalDiscussion.ClientID%>').value.trim() == "Reply") {
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
            if (document.getElementById('<%=fuChat_Doc1.ClientID%>').files.length != 0) {
                var ext = $('#fuChat_Doc1').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'doc', 'docx']) == -1) {
                    $('#lblfuChat_Doc1').text("केवल 'PNG*JPG*PDF*JPEG*DOC' दस्तावेज अपलोड करें।");
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
                    $('#lblfuChat_Doc2').text("केवल 'PNG*JPG*PDF*JPEG*DOC' दस्तावेज अपलोड करें।");
                    document.getElementById('lblfuChat_Doc2').value = "";
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
    </script>
</asp:Content>

