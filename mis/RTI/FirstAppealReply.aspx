<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FirstAppealReply.aspx.cs" Inherits="mis_RTI_FirstAppealReply" %>

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
                            <h3 class="box-title" id="Label1">First Appeal Request Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">First Appeal Detail</legend>
                                        <div class="form-group">
                                            <asp:DetailsView ID="DetailsView3" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                <Fields>
                                                    <asp:TemplateField HeaderText="Status"  HeaderStyle-Width="30%">
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
                                                                <asp:TemplateField HeaderText="Status"  HeaderStyle-Width="30%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_Status").ToString()%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="RTI_RegistrationNo" HeaderText="Registration No" />
                                                                <asp:BoundField DataField="RTI_UpdatedOn" HeaderText="RTI Filed Date" />
                                                                <asp:BoundField DataField="RTI_Subject" HeaderText="Subject Of RTI" />
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
                            <div class="row" id="divPaymentDetail" runat="server">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Payment Detail</legend>

                                        <div class="form-group">
                                            <div class="row" id="div1" runat="server">
                                                <div class="col-md-12">

                                                    <div class="form-group">
                                                        <label style="text-decoration:underline">For RTI</label>
                                                        <asp:DetailsView ID="DetailsView4" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                            <Fields>
                                                                 <asp:TemplateField HeaderText="PO / Receipt No."  HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_POReceiptNo").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                               <%-- <asp:BoundField DataField="RTI_POReceiptNo" HeaderText="PO / Receipt No." />--%>
                                                                <asp:BoundField DataField="RTI_PaymentMode" HeaderText="Payment Mode" />
                                                                <asp:BoundField DataField="RTI_Amount" HeaderText="Fees" />
                                                            </Fields>
                                                        </asp:DetailsView>
                                                        </div>
                                                     <div class="form-group">
                                                         <label style="text-decoration:underline">For First Appeal</label>
                                                        <asp:DetailsView ID="DetailsView5" runat="server" DataKeyNames="RTI_ID" AutoGenerateRows="false" CssClass="table table-responsive table-striped table-bordered table-hover">
                                                            <Fields>
                                                                 <asp:TemplateField HeaderText="PO / Receipt No."  HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRTI_Status" Text='<%# Eval("RTI_FAPOReceiptNo").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                               <%-- <asp:BoundField DataField="RTI_FAPOReceiptNo" HeaderText="PO / Receipt No." />--%>
                                                                <asp:BoundField DataField="RTI_FAPaymentMode" HeaderText="Payment Mode" />
                                                                <asp:BoundField DataField="RTI_FAAmount" HeaderText="Fees" />
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
                                                    </asp:TemplateField>--%>
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
                                                    <asp:TemplateField HeaderText="BPL Card No"  HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApp_BPLCardNo" runat="server" Text='<% #Eval("App_BPLCardNo").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year Of Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_YearOfIssue" runat="server" Text='<% #Eval("App_YearOfIssue").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issuing Authority">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_IssuingAuthority" runat="server" Text='<% #Eval("App_IssuingAuthority").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="BPL Card No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApp_BPLCardNo" runat="server" Text='<% #Eval("ShowHideBPL").ToString() == "Yes" ? Eval("App_BPLCardNo") : "-" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year Of Issue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_YearOfIssue" runat="server" Text='<% #Eval("ShowHideBPL") == "Yes" ? Eval("App_YearOfIssue") : "-"%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issuing Authority">
                                                        <ItemTemplate>
                                                            <asp:Label ID="App_IssuingAuthority" runat="server" Text='<% #Eval("ShowHideBPL") == "Yes" ? Eval("App_IssuingAuthority") : "-"%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
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
                            <h3 class="box-title" id="Label2">Officer Reply Section</h3>
                            <div class="pull-right">
                                <asp:LinkButton ID="lnkBack" runat="server" Text="Back" CssClass="label label-info" Font-Size="Small"  OnClick="lnkBack_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                                    <div class="pull-left">
                                        <asp:Label ID="lblShowNoComOnHide" runat="server" Text="Officer is not replied yet." ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="pull-right">
                                        <asp:LinkButton ID="lnkViewIntrlDis" Visible="true" runat="server" Text="View Internal Discussion" OnClick="lnkViewIntrlDis_Click" OnClientClick="ForFocus()" ClientIDMode="Static" ForeColor="#0623f3" Font-Underline="true"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <div class="direct-chat-messages" style="height: 100%;">
                                        <div id="dvChat" runat="server"></div>
                                        <!--For Chat-->
                                    </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
                                </div>
                            </div>
                            <div id="divRply" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Reply</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Remark<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" ID="txtRply_RTIRemark" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                        <small><span id="valtxtRply_RTIRemark" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Upload Document</label>
                                                        <asp:FileUpload ID="fuRply_RTIDoc1" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Upload Document</label>
                                                        <asp:FileUpload ID="fuRply_RTIDoc2" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                    </div>
                                                </div>
                                            </div>
                                            <small><span id="valfuRply_RTIDoc1" class="text-danger"></span></small>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Status<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRply_Status" ClientIDMode="Static">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Value="Open">Still Open</asp:ListItem>
                                                            <asp:ListItem Value="Close">Close</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlRply_Status" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnReply" runat="server" class="btn btn-success btn-block" Text="Reply For Citizen" OnClick="btnReply_Click" OnClientClick="return validate()" />
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
            <!-- Internal Discussion-->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Internal Discussion </h3>
                            <asp:Label ID="Label3" runat="server" Text="" Visible="true"></asp:Label>
                            <div class="pull-right">
                                <asp:LinkButton ID="lnkAddOfficer" runat="server" CssClass="label label-info text-bold" CausesValidation="False" Text="Add Officer For Internal Discussion" CommandName="select" OnClick="lnkAddOfficer_Click" Font-Size="Medium"></asp:LinkButton>
                                <%-- <a class="btn btn-default btn-block" data-toggle="modal" data-target="#AddOfficerModal">Add Officer For Internal Discussion</a>--%>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div id="divIntlRply" runat="server">
                                    <div class="col-md-6">
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
                                            <small><span id="valfuChat_Doc1" class="text-danger"></span></small>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnInternalDiscussion" runat="server" class="btn btn-success btn-block" Text="Internal Discussion" OnClick="btnInternalDiscussion_Click" OnClientClick="return validateInternalDiss()" />
                                                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <asp:Label ID="lblCommentRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div class="direct-chat-messages" style="height: 100%;">
                                        <div id="divchat" runat="server"></div>
                                        <!--For Chat-->
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="pull-right">
                                        <asp:LinkButton ID="lnkViewRply" Visible="true" runat="server" Text="View Reply Section" ClientIDMode="Static" OnClick="lnkViewRply_Click" ForeColor="#0623f3" Font-Underline="true"></asp:LinkButton>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <!-- /.box-footer-->
                                <!-- form start -->

                            </div>

                        </div>
                        <!-- /.box-body -->
                    </div>

                    <div id="AddOfficerModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title"><b>Forward To</b></h4>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-body">
                                            <div class="row">
                                                <asp:Label ID="lblModal" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Deapartment Name<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlAdd_Department" runat="server" class="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlAdd_Department_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <small><span id="valddlAdd_Department" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Officer Name<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlFrwd_OfficerName" runat="server" class="form-control" ClientIDMode="Static">
                                                        </asp:DropDownList>
                                                        <small><span id="valddlFrwd_OfficerName" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-9"></div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" runat="server" class="btn btn-success btn-block" Text="Add Officer" OnClick="btnAdd_Click" OnClientClick="return validateAdd()" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h4>Officer List</h4>
                                                    <asp:Label ID="lblGridRecord" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Add_ID" CssClass="table table-responsive table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Emp_Name" HeaderText="Officer Name" />
                                                            <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No." />
                                                            <%--<asp:CommandField SelectText="Delete" HeaderText="Delete" ShowDeleteButton="true" />--%>
                                                            <asp:TemplateField HeaderText="Remove">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Remove" OnClientClick="return confirm('Do you really want to Remove Officer Name?');"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
        function openModal() {
            debugger
            $('#AddOfficerModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            })
        };

        function validate() {
            $("#valtxtRply_RTIRemark").html("");
            $("#valfuRply_RTIDoc1").html("");
            $("#valfuRply_RTIDoc2").html("");
            $("#valddlRply_Status").html("");
            debugger;
            var msg = "";
            if (document.getElementById("txtRply_RTIRemark").value == "") {
                msg += "Enter Remark\n";
                $("#valtxtRply_RTIRemark").html("Enter Remark");
            }
            if (document.getElementById("ddlRply_Status").selectedIndex == 0) {
                msg += "Select Status\n";
                $("#valddlRply_Status").html("Select Status");
            }

            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnReply.ClientID%>').value.trim() == "Reply For Citizen") {
                    if (confirm("Do you really want to Reply ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

        }

        function validateInternalDiss() {
            $("#valtxtRply_RTIRemark").html("");
            $("#valfuRply_RTIDoc1").html("");
            $("#valddlRply_Status").html("");
            $("#valtxtChat_Remark").html("");
            $("#valfuChat_Doc1").html("");
            $("#valddlAdd_Department").html("");
            $("#valddlFrwd_OfficerName").html("");
            debugger;
            var msg = "";
            if (document.getElementById("txtChat_Remark").value == "") {
                msg += "Enter Remark\n";
                $("#valtxtChat_Remark").html("Enter Remark");
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
        function validateAdd() {
            $("#valtxtRply_RTIRemark").html("");
            $("#valfuRply_RTIDoc1").html("");
            $("#valddlRply_Status").html("");
            $("#valtxtChat_Remark").html("");
            $("#valfuChat_Doc1").html("");
            $("#valddlAdd_Department").html("");
            $("#valddlFrwd_OfficerName").html("");
            debugger;
            var msg = "";
            if (document.getElementById("ddlAdd_Department").selectedIndex == 0) {
                msg += "Select Department\n";
                $("#valddlAdd_Department").html("Select Department");
            }
            if (document.getElementById("ddlFrwd_OfficerName").selectedIndex == 0 || document.getElementById("ddlFrwd_OfficerName").selectedIndex < 0) {
                msg += "Select Officer Name\n";
                $("#valddlFrwd_OfficerName").html("Select Officer Name");
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnAdd.ClientID%>').value.trim() == "Add") {
                    if (confirm("Do you really want to Add Officer ?")) {
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
            $("#valtxtRply_RTIRemark").html("");
            $("#valfuRply_RTIDoc1").html("");
            $("#valfuRply_RTIDoc2").html("");
            $("#valddlRply_Status").html("");
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
                        $("#valfuRply_RTIDoc1").html("File Name Should be less than " + maxLengthFileName + " characters. \n");
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            $("#valfuRply_RTIDoc1").html("Special character not allowed in file name. \n");
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
                            $("#valfuRply_RTIDoc1").html("File Format Is Not Correct (Only " + strValidFormates + ").\n");
                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
                    $("#valfuRply_RTIDoc1").html("File Name is incorrect");
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

