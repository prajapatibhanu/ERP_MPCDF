<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTDashboardComents.aspx.cs" Inherits="mis_filetracking_FTDashboardComents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) --
        <!-- Main content -->
        <section class="content">
            <div id="commentsDetail" runat="server">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-6">
                        <!-- general form elements -->
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
                                                </div>
                                            </div>
                                        </fieldset>--%>
                                        </div>
                                    </div>
                                </div>
                                <div id="DivForward" runat="server">
                                    <fieldset>
                                        <legend>Forward</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label id="lblDescription" runat="server">नोट शीट / पत्र पर टिप्पणी</label>
                                                    <span style="color: red">*</span>
                                                    <asp:TextBox runat="server" placeholder="टिप्पणी..." ID="txtForwarded_Description" Rows="10" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                                                    <small><span id="valtxtForwarded_Description" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>विभाग <span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlForwarded_Department" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <small><span id="valddlForwarded_Department" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>अधिकारी का नाम <span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlForwarded_Officer" class="form-control" runat="server">
                                                        <asp:ListItem>चुने</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlForwarded_Officer" class="text-danger"></span></small>
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
                                                    <asp:Button ID="btnForward" runat="server" Text="Forward" CssClass="btn btn-success btn-block" OnClientClick="return validateform();" />
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
            <div id="OutwardDetail" runat="server">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- general form elements -->
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h3 class="box-title" id="Label1">Outward Letter Detail</h3>
                                <asp:Label ID="Label3" runat="server" Text="" Visible="true"></asp:Label>
                            </div>
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:DetailsView ID="DetailsView2" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:BoundField DataField="LetterNo" HeaderText="LETTER NO" />
                                                <asp:BoundField DataField="EndorsementNo" HeaderText="CC NUMBER" />
                                                <asp:BoundField DataField="DispatchDate" HeaderText="DISPATCH DATE" />
                                                <asp:BoundField DataField="ForwardToDepartment" HeaderText="FORWARD TO DEPARTMENT" />
                                                <asp:BoundField DataField="ForwardToOfficer" HeaderText="FORWARD TO OFFICER" />
                                                <asp:BoundField DataField="LetterReceiveFrom" HeaderText="LETTER RECEIVE FROM" />
                                                <asp:BoundField DataField="AddressTo" HeaderText="ADDRESS TO" />
                                                <asp:BoundField DataField="LetterSubject" HeaderText="LETTER SUBJECT" />
                                                <asp:TemplateField HeaderText="DOCUMENT">
                                                    <ItemTemplate>
                                                        <a href='<%# Eval("Doc1") %>' target="_blank" class="label label-info"><%# Eval("Doc1").ToString() != "" ? "View" : "" %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Remark" HeaderText="REMARK" />
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="CopyTo" HeaderText="COPY TO" />
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

