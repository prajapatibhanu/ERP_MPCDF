<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EntryformforHO.aspx.cs" Inherits="mis_DCSInformation_EntryformforHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">DCS Wise Information</h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date<span style="color: red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Union<span style="color: red">*</span></label>
                                        <asp:DropDownList ID="ddlUnion" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlUnion_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Total no of Member's in Union<span style="color: red">*</span></label>
                                        <asp:TextBox ID="txtTotalNoofmembersinunion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>District<span style="color: red">*</span></label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
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
                                        <label>DCS/BMC Name<span style="color: red">*</span></label>
                                        <asp:DropDownList ID="ddlDCS" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDCS_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
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
                                        <label>Total no of Memeber's in DCS<span style="color: red">*</span></label>
                                        <asp:TextBox ID="txtTotalNoofmembersinDCS" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Bank Details</legend>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Bank Name<span style="color: red">*</span></label>
                                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Branch Name<span style="color: red">*</span></label>
                                                        <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>IFSC Code<span style="color: red">*</span></label>
                                                        <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control">                                                      
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>No of Forms in Bank<span style="color: red">*</span></label>
                                                        <asp:TextBox ID="txtNoofForm" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return validateNum(event)">                                                      
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" CssClass="btn btn-success" runat="server" ClientIDMode="Static" Text="Add" OnClick="btnAdd_Click" OnClientClick="return validateBank();" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvBankDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvBankDetails_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bank">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                                                        <asp:Label ID="lblBankID" CssClass="hidden" runat="server" Text='<%# Eval("BankID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Branch">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                                        <asp:Label ID="lblBranchID" CssClass="hidden" runat="server" Text='<%# Eval("BranchID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IFSC Code">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIFSCCode" runat="server" Text='<%# Eval("IFSCCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No of Forms">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoofform" runat="server" Text='<%# Eval("Noofforms") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkbtnDelete" Text="Delete" CssClass="label label-danger" runat="server" CommandName="Delet" CommandArgument='<%# Eval("BranchID") %>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <table>
                                        <tr style="margin-top: 10px;">
                                            <td>No of form's Submitted by DCS<br />
                                                (डीसीएस द्वारा  जमा किये गए फॉर्म)<span style="color: red">*</span></td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtNoofMalesformsSubmittedbyDCS" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalDCSForm()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtNoofFemalesformsSubmittedbyDCS" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalDCSForm()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtTotalNoofformsSubmittedbyDCS" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Out of which No of forms Submitted for increase in limit(Already having KCC)<br />
                                                (जिनमें से कोई भी सीमा में वृद्धि के लिए प्रस्तुत नहीं की गई है (पहले से ही KCC वाले))<span style="color: red">*</span></td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtOutofwhichNoofMalesformsSubmittedforincreaseinlimit" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormAlreadyHavingKCC()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtOutofwhichNoofFemalesformsSubmittedforincreaseinlimit" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormAlreadyHavingKCC()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtOutofwhichTotalNoofformsSubmittedforincreaseinlimit" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="margin-top: 10px;">Application for New KCC having Land(having no KCC)<br />
                                                ( न्यू केसीसी के लिए आवेदन जिनके पास भूमि है (having no KCC))<span style="color: red">*</span></td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtMaleApplicationforNewKCChavingLand" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormHavingNoKCC()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtFemaleApplicationforNewKCChavingLand" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormHavingNoKCC()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtTotalApplicationforNewKCChavingLand" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="margin-top: 10px;">Application for New KCC having no Land(पूर्व KCC ना हो)<br />
                                                (न्यू केसीसी के लिए आवेदन जिनके पास भूमि नहीं है (पूर्व KCC ना हो))<span style="color: red">*</span></td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtMaleApplicationforNewKCChavingnoLand" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormHavingNewKCCHavingNoLand()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtFemaleApplicationforNewKCChavingnoLand" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormHavingNewKCCHavingNoLand()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtTotalApplicationforNewKCChavingnoLand" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr style="margin-top: 10px;">
                                            <td>Any Other's<br />
                                                (अन्य कोई)</td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtAnyOthersByMale" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalAnyothers()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtAnyOthersByFemale" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalAnyothers()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtAnyOthersByTotal" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr style="margin-top: 10px;">
                                            <td>Remark(for Any Other's)<br />
                                                टिप्पणी (अन्य के लिए)</td>
                                            <td style="padding: 20px;" colspan="6">
                                                <asp:TextBox ID="txtAnyOthersRemark" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr style="margin-top: 10px;">
                                            <td>No of form's Submitted by DCS to the Bank<br />
                                                (डीसीएस द्वारा बैंक को जमा किये गए फॉर्म)<span style="color: red">*</span></td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtNoofMaleformsSubmittedbyUniontotheBank" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormSubmittedbyuniontothebank()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtNoofFemaleformsSubmittedbyUniontotheBank" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalFormSubmittedbyuniontothebank()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtTotalNoofformsSubmittedbyUniontotheBank" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr style="margin-top: 10px;">
                                            <td>No of KCC Card Issued by Bank<br />
                                                (बैंक द्वारा जारी KCC कार्ड)</td>
                                            <td style="padding: 20px;">Male</td>
                                            <td>
                                                <asp:TextBox ID="txtNoofCardIssuedByMale" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalCardIssued()" autocomplete="off" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                            <td style="padding: 20px;">Female</td>
                                            <td>
                                                <asp:TextBox ID="txtNoofCardIssuedByFemale" runat="server" CssClass="form-control" ClientIDMode="Static" OnChange="TotalCardIssued()" autocomplete="off" onkeypress="return validateNum(event)">                                           
                                                </asp:TextBox></td>
                                            <td style="padding: 20px;">Total</td>
                                            <td>
                                                <asp:TextBox ID="txtTotalNoofCardIssued" runat="server" CssClass="form-control" ClientIDMode="Static">                                           
                                                </asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return validateform();" />
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
        function TotalDCSForm() {
            var Male = document.getElementById("<%= txtNoofMalesformsSubmittedbyDCS.ClientID%>").value;
            var Female = document.getElementById("<%= txtNoofFemalesformsSubmittedbyDCS.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= txtTotalNoofformsSubmittedbyDCS.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtTotalNoofformsSubmittedbyDCS.ClientID%>").value = "";
            }
        }
        function TotalFormAlreadyHavingKCC() {
            var Male = document.getElementById("<%= txtOutofwhichNoofMalesformsSubmittedforincreaseinlimit.ClientID%>").value;
            var Female = document.getElementById("<%= txtOutofwhichNoofFemalesformsSubmittedforincreaseinlimit.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= txtOutofwhichTotalNoofformsSubmittedforincreaseinlimit.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtOutofwhichTotalNoofformsSubmittedforincreaseinlimit.ClientID%>").value = "";
            }
        }
        function TotalFormHavingNoKCC() {
            var Male = document.getElementById("<%= txtMaleApplicationforNewKCChavingLand.ClientID%>").value;
            var Female = document.getElementById("<%= txtFemaleApplicationforNewKCChavingLand.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= txtTotalApplicationforNewKCChavingLand.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtTotalApplicationforNewKCChavingLand.ClientID%>").value = "";
            }
        }
        function TotalFormHavingNewKCCHavingNoLand() {
            var Male = document.getElementById("<%= txtMaleApplicationforNewKCChavingnoLand.ClientID%>").value;
            var Female = document.getElementById("<%= txtFemaleApplicationforNewKCChavingnoLand.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= txtTotalApplicationforNewKCChavingnoLand.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtTotalApplicationforNewKCChavingnoLand.ClientID%>").value = "";
            }
        }
        function TotalFormSubmittedbyuniontothebank() {
            var Male = document.getElementById("<%= txtNoofMaleformsSubmittedbyUniontotheBank.ClientID%>").value;
             var Female = document.getElementById("<%= txtNoofFemaleformsSubmittedbyUniontotheBank.ClientID%>").value;
             if (Male == "") {
                 Male = "0"
             }
             if (Female == "") {
                 Female = "0"
             }
             var Total = parseInt(Male) + parseInt(Female);
             if (Total != "0") {
                 document.getElementById("<%= txtTotalNoofformsSubmittedbyUniontotheBank.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtTotalNoofformsSubmittedbyUniontotheBank.ClientID%>").value = "";
            }
        }
        function TotalCardIssued() {
            var Male = document.getElementById("<%= txtNoofCardIssuedByMale.ClientID%>").value;
            var Female = document.getElementById("<%= txtNoofCardIssuedByFemale.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= txtTotalNoofCardIssued.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtTotalNoofCardIssued.ClientID%>").value = "";
            }
        }
        function TotalAnyothers() {
            var Male = document.getElementById("<%= txtAnyOthersByMale.ClientID%>").value;
             var Female = document.getElementById("<%= txtAnyOthersByFemale.ClientID%>").value;
             if (Male == "") {
                 Male = "0"
             }
             if (Female == "") {
                 Female = "0"
             }
             var Total = parseInt(Male) + parseInt(Female);
             if (Total != "0") {
                 document.getElementById("<%= txtAnyOthersByTotal.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= txtAnyOthersByTotal.ClientID%>").value = "";
            }
        }
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%= txtDate.ClientID%>').value == "") {
                 msg += "Enter Date.\n";
             }
             if (document.getElementById('<%= ddlUnion.ClientID%>').selectedIndex == 0) {
                msg += "Select Union.\n";
            }
            if (document.getElementById('<%= ddlDistrict.ClientID%>').selectedIndex == 0) {
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
        if (document.getElementById('<%= txtTotalNoofmembersinDCS.ClientID%>').value == "") {
                msg += "Enter Total no of Memeber's in DCS.\n";
            }
           <%-- if (document.getElementById('<%= ddlBank.ClientID%>').selectedIndex == 0) {
                msg += "Select Bank.\n";
            }
            if (document.getElementById('<%= ddlBranchName.ClientID%>').selectedIndex == 0) {
                msg += "Select Branch Name.\n";
            }--%>

            if (document.getElementById('<%= txtNoofMalesformsSubmittedbyDCS.ClientID%>').value == "") {
                msg += "Enter No of Male form's Submitted by DCS.\n";
            }
            if (document.getElementById('<%= txtNoofFemalesformsSubmittedbyDCS.ClientID%>').value == "") {
                msg += "Enter No of Female form's Submitted by DCS..\n";
            }
            if (document.getElementById('<%= txtOutofwhichNoofMalesformsSubmittedforincreaseinlimit.ClientID%>').value == "") {
                msg += "Enter No of Male forms Submitted for increase in limit(Already having KCC).\n";
            }
            if (document.getElementById('<%= txtOutofwhichNoofFemalesformsSubmittedforincreaseinlimit.ClientID%>').value == "") {
                msg += "Enter No of Female forms Submitted for increase in limit(Already having KCC).\n";
            }
            if (document.getElementById('<%= txtMaleApplicationforNewKCChavingLand.ClientID%>').value == "") {
                msg += "Enter No of Male Application for New KCC having Land(having no KCC).\n";
            }
            if (document.getElementById('<%= txtFemaleApplicationforNewKCChavingLand.ClientID%>').value == "") {
                msg += "Enter No of Female Application for New KCC having Land(having no KCC).\n";
            }
            if (document.getElementById('<%= txtMaleApplicationforNewKCChavingnoLand.ClientID%>').value == "") {
                msg += "Enter No of Male Application for New KCC having no Land(पूर्व KCC ना हो).\n";
            }
            if (document.getElementById('<%= txtFemaleApplicationforNewKCChavingnoLand.ClientID%>').value == "") {
                msg += "Enter No of Female Application for New KCC having no Land(पूर्व KCC ना हो)..\n";
            }
           <%-- if (document.getElementById('<%= txtAnyOthersByMale.ClientID%>').value == "") {
                msg += "Enter AnyOthers(Male).\n";
            }
            if (document.getElementById('<%= txtAnyOthersByFemale.ClientID%>').value == "") {
                msg += "Enter AnyOthers(Female).\n";
            }--%>
            if (document.getElementById('<%= txtNoofMaleformsSubmittedbyUniontotheBank.ClientID%>').value == "") {
                msg += "Enter No of Male form's Submitted by DCS to the Bank.\n";
            }
            if (document.getElementById('<%= txtNoofFemaleformsSubmittedbyUniontotheBank.ClientID%>').value == "") {
                msg += "Enter No of Female form's Submitted by DCS to the Bank.\n";
            }
            <%--if (document.getElementById('<%= txtNoofCardIssuedByMale.ClientID%>').value == "") {
                msg += "Enter No of Card Issued for Male\n";
            }
            if (document.getElementById('<%= txtNoofCardIssuedByFemale.ClientID%>').value == "") {
                msg += "Enter No of Card Issued for Female.\n";
            }--%>

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to save details?")) {
                    return true
                }
                else {
                    return false
                }

            }

        }
        function validateBank() {
            var msg = "";

            if (document.getElementById('<%= ddlBank.ClientID%>').selectedIndex == 0) {
                msg += "Select Bank.\n";
            }
            if (document.getElementById('<%= ddlBranchName.ClientID%>').selectedIndex == 0) {
                msg += "Select Branch Name.\n";
            }
            if (document.getElementById('<%= txtNoofForm.ClientID%>').value.trim() == "") {
                msg += "Enter No of Forms in Bank.\n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
    </script>
</asp:Content>

