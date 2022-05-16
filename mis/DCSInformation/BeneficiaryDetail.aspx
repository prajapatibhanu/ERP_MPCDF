<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BeneficiaryDetail.aspx.cs" Inherits="mis_DCSInformation_BeneficiaryDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Beneficiary Details</h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>District<span style="color:red">*</span></label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>CC<span style="color: red">*</span></label>
                                        <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCC_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="colccname" runat="server">
                                    <div class="form-group">
                                        <label>CC Name<span style="color: red">*</span></label>
                                        <asp:TextBox ID="txtCCName" runat="server" CssClass="form-control" placeholder="Enter CC Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>DCS Name<span style="color:red">*</span></label>
                                        <asp:DropDownList ID="ddlDCS" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlDCS_SelectedIndexChanged" AutoPostBack="true">                                          
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="coldcsname" runat="server">
                                    <div class="form-group">
                                        <label>DCS/BMC Name<span style="color: red">*</span></label>
                                        <asp:TextBox ID="txtDCSName" runat="server" CssClass="form-control" placeholder="Enter DCS/BMC Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Beneficiary Name<span style="color:red">*</span></label>
                                        <asp:TextBox ID="txtBeneficiaryName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Gender<span style="color:red">*</span></label>
                                        <asp:RadioButtonList ID="rbtnGender" CssClass="form-control" ClientIDMode="Static" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Male">&nbsp;&nbsp;&nbsp;Male&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="Female">&nbsp;&nbsp;&nbsp;Female</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Bank Details</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Bank Name<span style="color:red">*</span></label>
                                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem>Select</asp:ListItem>                                                  
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Branch Name<span style="color:red">*</span></label>
                                                    <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" AutoPostBack="true">                                                        
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>IFSC Code<span style="color:red">*</span></label>
                                                    <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" ClientIDMode="Static" MaxLength="15">                                                      
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Account No<span style="color:red">*</span></label>
                                                    <asp:TextBox ID="txtBankAccountNo" runat="server" MaxLength="20" CssClass="form-control" ClientIDMode="Static">                                                      
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Mobile No<span style="color:red">*</span></label>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control MobileNo" ClientIDMode="Static" MaxLength="10" onkeypress="return validateNum(event)">                                                      
                                                    </asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Submit for Increase in Limit<span style="color:red">*</span></label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtnsubmitforincreaseinlimit" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Yes">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="No" Selected="True">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Application for New KCC having land(having no KCC)<span style="color:red">*</span></label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtnAppfornewkcchavingland" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Yes">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="No"  Selected="True">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Application for New KCC having no land(पूर्व KCC ना हो).<span style="color:red">*</span></label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtnAppfornewkcchavingnoland" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Yes">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="No"  Selected="True">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Card Issued<span style="color:red">*</span></label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtnCardissued" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Yes">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="No"  Selected="True">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return validateform();"/>
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
        function validateform()
        {
            var msg = "";
            if(document.getElementById('<%=ddlDistrict.ClientID%>').selectedIndex == 0)
            {
                msg += "Select District.\n";
            }
             if (document.getElementById('<%= ddlCC.ClientID%>').selectedIndex == 0) {
                msg += "Select CC.\n";
            }
            else if (document.getElementById('<%= ddlCC.ClientID%>').selectedIndex != 0) {
                 var CC = document.getElementById('<%= ddlCC.ClientID%>').value;
                 if (CC == "-1") {
                     if (document.getElementById('<%= txtCCName.ClientID%>').value == "") {
                         msg += "Enter CC Name.\n";

                     }
                 }
             }
         if (document.getElementById('<%= ddlDCS.ClientID%>').selectedIndex == 0) {
                msg += "Select DCS.\n";
            }
            else if (document.getElementById('<%= ddlDCS.ClientID%>').selectedIndex != 0) {
                var DCS = document.getElementById('<%= ddlDCS.ClientID%>').value;
                if (DCS == "-1") {
                    if (document.getElementById('<%= txtDCSName.ClientID%>').value == "") {
                        msg += "Enter DCS Name.\n";

                    }
                }
            }
            if (document.getElementById('<%=txtBeneficiaryName.ClientID%>').value.trim() == "")
            {
                msg += "Enter Benificiary Name.\n";
            }
            if (document.getElementById('<%=rbtnGender.ClientID%>').selectedIndex == -1) {
                msg += "Select Gender.\n";
            }
            if (document.getElementById('<%=ddlBank.ClientID%>').selectedIndex == 0) {
                msg += "Select Bank.\n";
            }
            if (document.getElementById('<%=ddlBranchName.ClientID%>').selectedIndex == 0) {
                msg += "Select Branch.\n";
            }
            if (document.getElementById('<%=txtIFSC.ClientID%>').value.trim() == "") {
                msg += "Enter IFSC.\n";
            }
            if (document.getElementById('<%=txtBankAccountNo.ClientID%>').value.trim() == "") {
                msg += "Enter Account No.\n";
            }
            if (document.getElementById('<%=txtMobileNo.ClientID%>').value.trim() == "") {
                msg += "Enter Mobile No.\n";
            }
            if(msg != "")
            {
                alert(msg)
                return false;
            }
            else
            {
                if (confirm("Do you really want to save details?"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        $('.IFSC').blur(function () {
            debugger;
            var Obj = $('.IFSC').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                //var IFSC = /^[A-Za-z]{4}[0]{1}[0-9A-Za-z]{6}$/;
                var IFSC = /^[A-Z]{4}[0]{1}[0-9A-Z]{6}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(IFSC) == -1) {
                    alert("Invalid IFSC Code");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.IFSC').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild IFSC Code.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.IFSC').val('');
                    return false;
                }

                if ($('.IFSC').val.length != 11) {
                    alert("Invaild IFSC Code.");
                    return false;
                }
            }
        });
        $('.MobileNo').blur(function () {
            debugger;
            var Obj = $('.MobileNo').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo').val('');
                    return false;
                }
            }
        });
    </script>
</asp:Content>

