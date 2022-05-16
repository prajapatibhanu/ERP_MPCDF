<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ApplyGrievance.aspx.cs" Inherits="mis_Grievance_ApplyGrievance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title" id="Label1">उपभोक्ता द्वारा शिकायत</h3>--%>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row" style="display: none;">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Subject/Title (विषय/शीर्षक)</label><span style="color: red">*</span>
                                        <asp:TextBox runat="server" Placeholder="Enter Subject (विषय) " ID="txtSubject" MaxLength="100" CssClass="form-control" ClientIDMode="Static" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtSubject" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Grievance / Feedback Type (शिकायत / प्रतिक्रिया प्रकार)<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlGrvType" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">उत्पाद की गुणवत्ता के बारे में</asp:ListItem>
                                            <asp:ListItem Value="2">उत्पाद की उपलब्धता के बारे में</asp:ListItem>
                                            <asp:ListItem Value="3">समिति भुगतान के सम्बन्ध में</asp:ListItem>
                                            <asp:ListItem Value="4">एजेंसी / बूथ / पार्लर / डीपो सम्बन्धित शिकायत </asp:ListItem>
                                            <asp:ListItem Value="5">वितरक / परिवहनकर्ता सम्बन्धित शिकायत</asp:ListItem>
                                            <asp:ListItem Value="6">दूध उत्पादक समिति से सम्बंधित</asp:ListItem>
                                            <asp:ListItem Value="9">अन्य सुझाव</asp:ListItem>
                                            <asp:ListItem Value="10">अन्य (जानकारी प्राप्त करने से सम्बंधित)</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlGrvType" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Complaint by(शिकायतकर्ता का नाम)<span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtComplaintName" CssClass="form-control" placeholder="Enter Complaint Name(शिकायत का नाम)">                                                                              
                                        </asp:TextBox>
                                        <small><span id="valtxtComplaintName" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Contact No(संपर्क नं)<span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control" MaxLength="10" placeholder="Enter Contact No(संपर्क नं)">                                                                              
                                        </asp:TextBox>
                                        <small><span id="valtxtContactNo" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>District (जिला)<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <small><span id="valddlDistrict" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>DS Name (दुग्ध संघ)<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlDSName" CssClass="form-control"></asp:DropDownList>
                                        <small><span id="valddlDSName" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display: none;">
                                    <div class="form-group">
                                        <label>Complaint No (शिकायत क्रमांक)<span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtComplaintNo" CssClass="form-control" placeholder="Enter Complaint No(शिकायत क्रमांक)">                                                                              
                                        </asp:TextBox>
                                        <small><span id="valtxtComplaintNo" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Email ID (ईमेल) </label>
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Enter Email ID(ईमेल)">                                                                              
                                        </asp:TextBox>
                                        <small><span id="valtxtEmail" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address (पता )<span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtLocation" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Enter Address(पता )">                                                                               
                                        </asp:TextBox>
                                        <small><span id="valtxtLocation" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Description of Grievance in Detail (शिकायत का संक्षिप्त में विवरण)</label><span style="color: red">*</span>
                                        <asp:TextBox runat="server" Placeholder="Enter Description of Grievance in Detail (शिकायत का संक्षिप्त में विवरण)" ID="txtGrvDescription" TextMode="MultiLine" Rows="5" CssClass="form-control" ClientIDMode="Static" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtGrvDescription" class="text-danger"></span></small>
                                    </div>
                                </div>

                            </div>
                            <div class="row" style="display: none;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Document 1 (Only 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX')</label>
                                        <asp:Label ID="lblSuppDoc1" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                        <asp:FileUpload ID="txtSuppDoc1" CssClass="form-control" runat="server" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                        <small><span id="valtxtSuppDoc1" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Document 2 (Only 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX')</label>
                                        <asp:Label ID="lblSuppDoc2" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                        <asp:FileUpload ID="txtSuppDoc2" CssClass="form-control" runat="server" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                        <small><span id="valtxtSuppDoc2" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>

                            <div class="box-footer">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="BtnSubmit" CssClass="btn btn-block btn-success" runat="server" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="return validateform();" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a class="btn btn-block btn-default" href="ApplyGrievance.aspx">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Application_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Application_GrvStatus").ToString()%>' runat="server" ID="Application_GrvStatus"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Application_Subject" HeaderText="Subject" />
                                                <asp:BoundField DataField="Application_GrievanceType" HeaderText="Grievance / Feedback Type" />
                                                <asp:BoundField DataField="Application_Descritption" HeaderText="Grievance description " /> 
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>--%>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <ul style="color: red;">
                                    <b>Note :</b>
                                    <li><b>प्रथम स्तर अधिकारी जिसे निराकरण करना है (समय सीमा - २ दिवस )</b></li>
                                    <li><b>सीमा उपरांत द्वितीय स्तर के अधिकारी (समय सीमा - १ दिवस )</b></li>
                                    <li><b>सीमा उपरांत तृतीय स्तर के अधिकारी (समय सीमा - १ दिवस )</b></li>
                                </ul>
                            </div>
                        </div>
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
    <script type="text/javascript">

        function validateform() {
            var msg = "";
            $("#valtxtLocation").html("");
            $("#valtxtComplaintNo").html("");
            $("#valddlDistrict").html("");
            $("#valddlGrvType").html("");
            $("#valtxtGrvDescription").html("");
            $("#valtxtSuppDoc1").html("");
            $("#valtxtSuppDoc2").html("");

            if (document.getElementById('<%=ddlGrvType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Grievance Type. \n";
                $("#valddlGrvType").html("Please Select Grievance Type.");
            }
            if (document.getElementById('<%=ddlDistrict.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select District Name. \n";
                $("#valddlDistrict").html("Please Select District Name.");
            }
            if (document.getElementById('<%=txtLocation.ClientID%>').value.trim() == "") {
                msg = msg + "Please Enter Location. \n";
                $("#valtxtLocation").html("Please Enter Location.");
            }
            if (document.getElementById('<%=txtGrvDescription.ClientID%>').value.trim() == "") {
                msg = msg + "Please Enter Description. \n";
                $("#valtxtGrvDescription").html("Please Enter Description.");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=BtnSubmit.ClientID%>').value.trim() == "Submit") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function AvoidSpace(event) {
            var k = event ? event.which : window.event.keyCode;
            if (k == 32) return false;
        }

        function uploadDoc() {
            if (document.getElementById('<%=txtSuppDoc1.ClientID%>').files.length != 0) {
                var ext = $('#txtSuppDoc1').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'gif', 'doc', 'docx']) == -1) {

                    $("#valtxtSuppDoc1").html("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                    document.getElementById('txtSuppDoc1').value = "";
                }
                else {
                    $('#lblSuppDoc1').text("");
                }
            }
            else {
                $('#lblSuppDoc1').text("");
            }
            if (document.getElementById('<%=txtSuppDoc2.ClientID%>').files.length != 0) {
                var ext = $('#txtSuppDoc2').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'gif', 'doc', 'docx']) == -1) {

                    $("#valtxtSuppDoc2").html("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                    document.getElementById('txtSuppDoc2').value = "";
                }
                else {
                    $('#lblSuppDoc2').text("");
                }
            }
            else {
                $('#lblSuppDoc2').text("");
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
        function OnlyNumbers(event) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

</asp:Content>

