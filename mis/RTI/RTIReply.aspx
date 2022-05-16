<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RTIReply.aspx.cs" Inherits="mis_RTI_Rply" %>

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
                            <div class="pull-right">
                                <asp:LinkButton ID="lnkApplyForFirstAppeal" runat="server" Text="Apply For First Appeal" Font-Underline="true" Font-Bold="true" ForeColor="#0b7ff7" OnClick="lnkApplyForFirstAppeal_Click" Font-Size="Medium"></asp:LinkButton>
                            </div>
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
                                                                        <asp:Label ID="lbl1" Text='<%# Eval("RTI_POReceiptNo").ToString()%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <%-- <asp:BoundField DataField="RTI_POReceiptNo" HeaderText="PO / Receipt No." />--%>
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

                            <!--Request For First Appeal-->
                            <%--<div class="row" id="divRequestFirstAppeal"  runat="server">
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

                                        
                                    </fieldset>
                                </div>
                            </div>--%>
                            <!--End First Appeal-->
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
                                <asp:LinkButton ID="lnkBack" runat="server" Text="Back" CssClass="label label-info" Font-Size="Small" OnClick="lnkBack_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
                            <div id="divRply" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Reply</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Remark<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" ID="txtRply_RTIRemark" CssClass="form-control" ClientIDMode="Static" placeholder="Remark" TextMode="MultiLine" Rows="5"></asp:TextBox>
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
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRply_Status" ClientIDMode="Static" OnSelectedIndexChanged="ddlRply_Status_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                                            <asp:ListItem Value="Open">Still Open</asp:ListItem>
                                                            <asp:ListItem Value="Close">Close</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlRply_Status" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div id="rtidate" runat="server">
                                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>RTI Close Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtRTICloseDate" runat="server" placeholder="Select RTI File Date.." class="form-control DateAdd" autocomplete="off"  data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtRTICloseDate" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnReply" runat="server" class="btn btn-success btn-block" Text="Reply For Citizen" OnClientClick="return validate()" OnClick="btnReply_Click" />
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
            <div class="row" id="Heading1">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Heading">Internal Discussion </h3>
                            <asp:Label ID="Label3" runat="server" Text="" Visible="true"></asp:Label>
                            <div class="pull-right">
                                <asp:LinkButton ID="lnkAddOfficer" runat="server" CssClass="btn btn-info btn-block" CausesValidation="False" Text="Add Officer For Internal Discussion" CommandName="select" OnClick="lnkAddOfficer_Click"></asp:LinkButton>
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
                                                                <label>Deapartment Name<span style="color: red;"> *</span></label><br />
                                                                <asp:DropDownList ID="ddlAdd_Department" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlAdd_Department_SelectedIndexChanged" AutoPostBack="true" Width="100%">
                                                                </asp:DropDownList>
                                                                <small><span id="valddlAdd_Department" class="text-danger"></span></small>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Officer Name<span style="color: red;"> *</span></label>
                                                                <asp:DropDownList ID="ddlFrwd_OfficerName" runat="server" class="form-control" ClientIDMode="Static" Width="100%">
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
                                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Officer Name" />
                                                                    <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No." />
                                                                    <%-- <asp:CommandField SelectText="Delete" HeaderText="Delete" ShowDeleteButton="true" />--%>
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

                    <div id="ApplyForFAModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title"><b>Apply For First Appeal</b></h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblFA" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Reason For First Appeal</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <asp:HiddenField ID="hdnApp_Status" runat="server" ClientIDMode="Static" />
                                                        <label>GROUND FOR FIRST APPEAL <small>(पहली अपील के लिए कारण)</small>:<span style="color: red;">*</span></label>
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
                                                    <div class="form-group">
                                                        <label>Comment <small>(टिप्पणी)</small>:</label>
                                                        <asp:TextBox ID="txtRTI_FAComment" runat="server" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" onkeyDown="return checkTextAreaMaxLength(this,event,'199');" MaxLength="199"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Upload Document <small>(दस्तावेज अपलोड करें)</small>:</label>
                                                        <asp:FileUpload ID="fuRTI_FARequestDoc" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                                        <small><span id="valfuRTI_FARequestDoc" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="row" id="divFill_FirstPaymentDetail" runat="server">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend style="margin-bottom: 12px;">Fee Detail</legend>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Payment Mode <small>(भुगतान का प्रकार)</small> <span style="color: red;">*</span></label>
                                                            <asp:DropDownList ID="ddlRTI_PaymentMode" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Cash</asp:ListItem>
                                                                <asp:ListItem>Postal Order</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <small><span id="valddlRTI_PaymentMode" class="text-danger"></span></small>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Postal Order/ Receipt No <small>(रसीद क्रं)</small><span style="color: red;"> *</span></label>
                                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Postal Order/ Receipt No (रसीद क्रं)" ID="txtRTI_POReceiptNo" ClientIDMode="Static" MaxLength="15" />
                                                            <small><span id="valtxtRTI_POReceiptNo" class="text-danger"></span></small>
                                                        </div>
                                                    </div>
                                                    <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date (दिनांक)</label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtDate" runat="server" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Amount <small>(राशि)(Rs.)</small><span style="color: red;"> *</span></label>
                                                            <asp:TextBox runat="server" CssClass="form-control Amount" placeholder="Amount (राशि)(Rs.)" ID="txtRTI_Amount" ClientIDMode="Static" MaxLength="8" onkeypress="return validateDec(this,event);" />
                                                            <small><span id="valtxtRTI_Amount" class="text-danger"></span></small>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                            <asp:Button runat="server" Text="Send Request" CssClass="btn btn-success form-control" ID="btnSendRequest" ClientIDMode="Static" OnClick="btnSendRequest_Click" OnClientClick="return validateReqFirstApp()" Width="20%" />
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
        function validate() {
            $("#valtxtRply_RTIRemark").html("");
            $("#valfuRply_RTIDoc1").html("");
            $("#valddlRply_Status").html("");
            $("#valtxtChat_Remark").html("");
            $("#valfuChat_Doc1").html("");
            $("#valddlAdd_Department").html("");
            $("#valddlFrwd_OfficerName").html("");
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
            if (document.getElementById("txtRTICloseDate").value == "") {
                msg += "Enter RTI Close Date\n";
                $("#valtxtRTICloseDate").html("Enter RTI Close Date");
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
            //if (msg != "") {
            //    alert(msg);
            //    return false
            //}
            //if (msg == "") {
            //    return true
            //}
        }
        function HideLabel() {
            var seconds = 10;
            setTimeout(function () {
                debugger
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
                document.getElementById("<%=lblCommentRecord.ClientID %>").style.display = "none";
                document.getElementById("<%=lblGridRecord.ClientID %>").style.display = "none";
                document.getElementById("<%=lblModal.ClientID %>").style.display = "none";

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

        function validateReqFirstApp() {
            $("#valddlRTI_FAGroundFor").html("");
            $("#valfuRTI_FARequestDoc").html("");
            $("#valddlRTI_PaymentMode").html("");
            $("#valtxtRTI_POReceiptNo").html("");
            $("#valtxtRTI_Amount").html("");
            debugger;
            var msg = "";
            if (document.getElementById("ddlRTI_FAGroundFor").selectedIndex == 0) {
                msg += "GROUND FOR FIRST APPEAL (पहली अपील के लिए कारण)\n";
                $("#valddlRTI_FAGroundFor").html("GROUND FOR FIRST APPEAL (पहली अपील के लिए कारण)");
            }
            if (document.getElementById("hdnApp_Status").value == "No") {
                if (document.getElementById("ddlRTI_PaymentMode").selectedIndex == 0) {
                    msg += "Payment Mode (भुगतान का प्रकार)\n";
                    $("#valddlRTI_PaymentMode").html("Payment Mode (भुगतान का प्रकार)");
                }
                if (document.getElementById("txtRTI_POReceiptNo").value == "") {
                    msg += "Postal Order/ Receipt No (रसीद क्रं) \n";
                    $("#valtxtRTI_POReceiptNo").html("Postal Order/ Receipt No (रसीद क्रं)");
                }
                if (document.getElementById("txtRTI_Amount").value == "") {
                    msg += "Amount (राशि)\n";
                    $("#valtxtRTI_Amount").html("Amount (राशि)");
                }
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnSendRequest.ClientID%>').value.trim() == "Send Request") {
                if (confirm("Do you really want to Send Request?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

            //if (msg != "") {
            //    alert(msg);
            //    return false
            //}
            //if (msg == "") {
            //    return true
            //}

    }
    function ForFocus() {
        debugger;
        document.getElementById("Heading").focus();
    }
    </script>
</asp:Content>

