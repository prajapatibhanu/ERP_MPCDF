<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EditApplicantProfile.aspx.cs" Inherits="mis_RTI_EditApplicantProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Applicant Profile (आवेदक प्रोफाइल)</h3>
                     <div class="pull-right" style="margin-right:20px; padding:2px;">
                         <asp:ImageButton ID="img1" runat="server" ImageUrl="~/mis/RTI/RTI_Docs/ZBack.jpg" OnClick="img1_Click"  />
                    
                </div>
                </div>
                <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>

                <!-- Start RTI Details-->
                <div class="box-body">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="row" style="margin-top:-20px;">
                        <div class="col-md-12">
                            <fieldset>
                                <legend style="margin-bottom: 12px;">Applicant Personal Detail</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Applicant Name (आवेदक का नाम)<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Applicant Name (आवेदक का नाम)"  ID="txtApp_Name" ClientIDMode="Static" autocomplete="off" onkeypress="return validatename(event)" MaxLength="100" />
                                        <small><span id="valtxtApp_Name" class="text-danger"></span></small>
                                             </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Mobile No. (मोबाईल नंबर)<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtApp_MobileNo" ClientIDMode="Static" autocomplete="off" onkeypress="return OnlyNumbers(event)" MaxLength="10" />
                                             <small><span id="valtxtApp_MobileNo" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <label>User Type (आवेदक का प्रकार)<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlApp_UserType" runat="server" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Individual">Individual</asp:ListItem>
                                            <asp:ListItem Value="Other Organization">Other Organization</asp:ListItem>
                                            <asp:ListItem Value="NGO">NGO</asp:ListItem>
                                            <asp:ListItem Value="Common Service Center">Common Service Center</asp:ListItem>
                                        </asp:DropDownList>
                                         <small><span id="valddlApp_UserType" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Email (ईमेल)<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" placeholder="Email (ईमेल)"  ID="txtApp_Email" ClientIDMode="Static" autocomplete="off" MaxLength="50" onkeypress="return AvoidSpace(event)" />
                                             <small><span id="valtxtApp_Email" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Gender (लिंग)<span style="color: red;"> *</span></label>
                                            <asp:RadioButtonList ID="rbtnlApp_Gender" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static">
                                                <asp:ListItem Selected="True">Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <!-- Start Address Details-->


                                <fieldset>
                                    <legend style="margin-bottom: 12px;">Address Section</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row">
                                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>State (राज्य)<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlApp_State" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlApp_State_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                                          
                                                        </asp:DropDownList>
                                                        <small><span id="valddlApp_State" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Districts (जिला)<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlApp_District" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlApp_District_SelectedIndexChanged" AutoPostBack="true">
                                                           
                                                        </asp:DropDownList>
                                                        <small><span id="valddlApp_District" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Block (खंड)<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlApp_Block" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                           
                                                        </asp:DropDownList>
                                                        <small><span id="valddlApp_Block" class="text-danger"></span></small>
                                                       <%-- <label>Tehsil(तालुका)<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control"   ID="txtApp_Tehsil" ClientIDMode="Static" autocomplete="off" onkeypress="return validatename(event)" />--%>
                                                    </div>
                                                </div>
                                                         </ContentTemplate>
                                                </asp:UpdatePanel>
                                                
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Pin code (पिनकोड)<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Pin code (पिनकोड)"  ID="txtApp_Pincode" onkeypress="return OnlyNumbers(event)" ClientIDMode="Static" autocomplete="off" MaxLength="6" />
                                                    <small><span id="valtxtApp_Pincode" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Address (पता)<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Address (पता)"  ID="txtApp_Address" ClientIDMode="Static" Rows="5" autocomplete="off" />
                                                <%-- <asp:TextBox runat="server" CssClass="form-control" ID="txtMeetingDate" ClientIDMode="Static"  data-date-end-date="0d" placeholder="DD/MM/YYYY" />--%>
                                            <small><span id="valtxtApp_Address" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <!-- Start BPL Details-->
                                <fieldset>
                                    <legend style="margin-bottom: 12px;">Poverty Line Status</legend>
                                    <div class="row">
                                        <div class="col-md-12" style="display: inline-block">
                                            <label style="margin-right: 20px;">Is The Applicant Below Poverty Line ? (गरीबी रेखा से नीचे आवेदक है?)<span style="color: red;"> *</span></label>
                                            <asp:RadioButtonList ID="rbtnlApp_PLStatus" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" ClientIDMode="Static" onChange="abc()">
                                                <asp:ListItem Value="Yes (हाँ)" Selected="True">Yes (हाँ)</asp:ListItem>
                                                <asp:ListItem Value="No (नहीं)">No (नहीं)</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                       </div>
                                    <div class="row aaa ">
                                        <div class="col-md-4" id="BPLCardNo" runat="server">
                                            <div class="form-group">
                                                <label id="lblBPL">BPL Card No. (बीपील कार्ड क्रं)<span style="color: red;">*</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control"  placeholder="BPL Card No. (बीपील कार्ड क्रं)" ID="txtApp_BPLCardNo" ClientIDMode="Static" autocomplete="off" MaxLength="10" />
                                            <small><span id="valtxtApp_BPLCardNo" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="YearOfIssue" runat="server">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label id="lblYear">Year of Issue (जारी करने का वर्ष)<span style="color: red;">(YYYY) *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Year of Issue (जारी करने का वर्ष)"  ID="txtApp_YearOfIssue" onkeypress="return OnlyNumbers(event)" ClientIDMode="Static" autocomplete="off" MaxLength="4" />
                                                <small><span id="valtxtApp_YearOfIssue" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="IssuingAuthority" runat="server">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label id="lblIssue">Issuing Authority (जारी करने वाला प्राधिकरण)<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Issuing Authority (जारी करने वाला प्राधिकरण)"  ID="txtApp_IssuingAuthority" ClientIDMode="Static" autocomplete="off" onkeypress="return validatename(event)" MaxLength="50" />
                                                 <small><span id="valtxtApp_IssuingAuthority" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5"></div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" Text="Update" CssClass="btn btn-success btn-block" ID="btnUpdate" ClientIDMode="Static" OnClientClick="return validate()" OnClick="btnUpdate_Click" /><!--OnClientClick="return validate()" -->
                            </div>
                        </div>
                        <div class="col-md-5"></div>
                    </div>
                    </asp:Panel>
                    
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
        <script>

            window.onload = function () {
                abc();
            };
            function abc() {
                debugger
                var rb = document.getElementById("rbtnlApp_PLStatus");
                var inputs = rb.getElementsByTagName('input');
            
                for (var i = 0; i < inputs.length; i++) {
                    if (i == 0 && inputs[0].checked) {
                        document.getElementById('txtApp_BPLCardNo').style.display = 'block';
                        document.getElementById('txtApp_YearOfIssue').style.display = 'block';
                        document.getElementById('txtApp_IssuingAuthority').style.display = 'block';
                        document.getElementById('lblBPL').style.display = 'block';
                        document.getElementById('lblYear').style.display = 'block';
                        document.getElementById('lblIssue').style.display = 'block';
                    }
                    if (i == 1 && inputs[1].checked) {
                        document.getElementById('txtApp_BPLCardNo').style.display = "none";
                        document.getElementById('txtApp_YearOfIssue').style.display = "none";
                        document.getElementById('txtApp_IssuingAuthority').style.display = "none";
                        document.getElementById('lblBPL').style.display = "none";
                        document.getElementById('lblYear').style.display = "none";
                        document.getElementById('lblIssue').style.display = "none";
                        $("#valtxtApp_BPLCardNo").html("");
                        $("#valtxtApp_YearOfIssue").html("");
                        $("#valtxtApp_IssuingAuthority").html("");
                    }
                }
            }
            function OnlyNumbers(event) {
                var e = event || evt; // for trans-browser compatibility
                var charCode = e.which || e.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }

            function validatename(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                    return false;
                }
                return true;
            }

            function AvoidSpace(event) {
                var k = event ? event.which : window.event.keyCode;
                if (k == 32) return false;
            }

            function validate() {
                $("#valtxtApp_MobileNo").html("");
                $("#valtxtApp_Name").html("");
                $("#valtxtApp_Email").html("");
                $("#valddlApp_UserType").html("");
                $("#valtxtApp_Address").html("");
                $("#valtxtApp_Pincode").html("");
                $("#valddlApp_State").html("");
                $("#valddlApp_District").html("");
                $("#valddlApp_Block").html("");
                $("#valtxtApp_BPLCardNo").html("");
                $("#valtxtApp_YearOfIssue").html("");
                $("#valtxtApp_IssuingAuthority").html("");
                $("#valfuRTI_RequestDoc").html("");
                var msg = "";
                if (document.getElementById('txtApp_Name').value == "") {
                    msg += "Enter Applicant Name (आवेदक का नाम)\n";
                    $("#valtxtApp_Name").html("Enter Applicant Name (आवेदक का नाम)");
                }

                debugger;
                var mobileno = document.getElementById('txtApp_MobileNo').value;
                var digit = mobileno.toString()[0];
                if (mobileno == "") {
                    msg += "Enter Mobile Number\n";
                    $("#valtxtApp_MobileNo").html("Enter Mobile Number(मोबाईल नंबर)");
                }
                else {
                    if (mobileno.length != 10 || (digit < 6)) {
                        msg += "Enter Valid Mobile Number\n";
                        $("#valtxtApp_MobileNo").html("Enter Valid Mobile Number(मोबाईल नंबर)");
                    }
                }
                if (document.getElementById('txtApp_Email').value == "") {
                    msg += "Enter Email (ईमेल) \n";
                    $("#valtxtApp_Email").html("Enter Email (ईमेल)");
                }
                if (document.getElementById('txtApp_Email').value != "") {

                    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;
                    if (reg.test(document.getElementById('txtApp_Email').value) == false) {
                        msg += "Enter Valid Email Address. \n";
                        $("#valtxtApp_Email").html("Enter Valid Email (ईमेल)");
                    }
                }
                if (document.getElementById("ddlApp_UserType").selectedIndex == 0) {
                    msg += "Select User Type (आवेदक का प्रकार)\n";
                    $("#valddlApp_UserType").html("Select User Type (आवेदक का प्रकार)");
                }
                if (document.getElementById('txtApp_Address').value == "") {
                    msg += "Enter Address (पता)\n";
                    $("#valtxtApp_Address").html("Enter Address (पता)");
                }
                if (document.getElementById('txtApp_Pincode').value == "") {
                    msg += "Enter Pin code (पिनकोड)\n";
                    $("#valtxtApp_Pincode").html("Enter Pin code (पिनकोड)");
                }
                if (document.getElementById("ddlApp_State").selectedIndex == 0) {
                    msg += "Select State (राज्य) \n";
                    $("#valddlApp_State").html("Select State (राज्य)");
                }
                if ((document.getElementById("ddlApp_District").selectedIndex == 0) || (document.getElementById("ddlApp_District").selectedIndex == -1)) {
                    msg += "Select Districts (जिला)\n";
                    $("#valddlApp_District").html("Select Districts (जिला)");
                }
                if ((document.getElementById("ddlApp_Block").selectedIndex == 0) || (document.getElementById("ddlApp_District").selectedIndex == -1)) {
                    msg += "Select Block (खंड) \n";
                    $("#valddlApp_Block").html("Select Block (खंड)");
                }
                //if (document.getElementById("rbtnlApp_PLStatus").selectedIndex == 0) {
                //    msg += "Select Name\n";
                //}
                debugger
                var rb = document.getElementById("rbtnlApp_PLStatus");
                var inputs = rb.getElementsByTagName('input');
            
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].checked) {
                        if (i == 0) {
                            if (document.getElementById('txtApp_BPLCardNo').value == "") {
                                msg += "Enter BPL Card No. (बीपील कार्ड क्रं)\n";
                                $("#valtxtApp_BPLCardNo").html("Enter BPL Card No. (बीपील कार्ड क्रं)");
                            }
                            if (document.getElementById('txtApp_YearOfIssue').value == "") {
                                msg += "Enter Year of Issue (जारी करने का वर्ष)\n";
                                $("#valtxtApp_YearOfIssue").html("Enter Year of Issue (जारी करने का वर्ष)");
                            }
                            if (document.getElementById('txtApp_YearOfIssue').value != "") {
                                if (document.getElementById('txtApp_YearOfIssue').value.length != 4) {
                                    msg += "Enter Valid Year of Issue (जारी करने का वर्ष)(YYYY)\n";
                                    $("#valtxtApp_YearOfIssue").html("Enter Valid Year of Issue (जारी करने का वर्ष)(YYYY)");
                                }
                            }
                            if (document.getElementById('txtApp_IssuingAuthority').value == "") {
                                msg += "Enter Issuing Authority (जारी करने वाला प्राधिकरण)\n";
                                $("#valtxtApp_IssuingAuthority").html("Enter Issuing Authority (जारी करने वाला प्राधिकरण)");
                            }
                        }
                        else {}
                    }
                }
                if (msg != "") {
                    alert(msg);
                    return false
                }
                if (msg == "") {
                    if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                <%--if (document.getElementById('<%=btnSaveAsDraft.ClientID%>').value.trim() == "Save As Draft") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }--%>
            }
            }

            </script>
</asp:Content>

