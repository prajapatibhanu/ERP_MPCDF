<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSInformationForm.aspx.cs" Inherits="mis_DCSInformation_DCSInformationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">District Wise DCS Information</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of memebers already having KCC</label>
                                    <asp:TextBox runat="server" ID="textbox1" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Blank form received by DCS</label>
                                    <asp:TextBox runat="server" ID="textbox2" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of form Fillup by Male's<br />&nbsp;</label>
                                    <asp:TextBox runat="server" ID="textbox3" CssClass="form-control" ClientIDMode="Static" onchange="TotalFormFillUp();"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of form Fillup by Female's</label>
                                    <asp:TextBox runat="server" ID="textbox12" CssClass="form-control" ClientIDMode="Static" onchange="TotalFormFillUp();"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total No of form Fillup by Member's</label>
                                    <asp:TextBox runat="server" ID="textbox13" CssClass="form-control" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Male's form certified by DCS</label>
                                    <asp:TextBox runat="server" ID="textbox4" CssClass="form-control" ClientIDMode="Static" onchange="TotalFormCertified();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Female's form certified by DCS</label>
                                    <asp:TextBox runat="server" ID="textbox14" CssClass="form-control" ClientIDMode="Static" onchange="TotalFormCertified();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total No of form certified by DCS</label>
                                    <asp:TextBox runat="server" ID="textbox15" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Male's form Submitted to Bank</label>
                                    <asp:TextBox runat="server" ID="textbox5" CssClass="form-control" ClientIDMode="Static" onchange="TotalFormSubmittedtoBank();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Female's form Submitted to Bank</label>
                                    <asp:TextBox runat="server" ID="textbox16" CssClass="form-control" ClientIDMode="Static" onchange="TotalFormSubmittedtoBank();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total No of form Submitted to Bank</label>
                                    <asp:TextBox runat="server" ID="textbox17" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Male's Apply for New Card</label>
                                    <asp:TextBox runat="server" ID="textbox6" CssClass="form-control" ClientIDMode="Static" onchange="TotalApplyforNewCard();"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Female's Apply for New Card</label>
                                    <asp:TextBox runat="server" ID="textbox7" CssClass="form-control" ClientIDMode="Static" onchange="TotalApplyforNewCard();"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total Member's Apply for New Card</label>
                                    <asp:TextBox runat="server" ID="textbox11" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of card for limit enhancement</label>
                                    <asp:TextBox runat="server" ID="textbox8" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Bank Wise Detail</legend>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Bank Name</label><br />&nbsp;                                             
                                                <asp:DropDownList ID="ddlBankname" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>State Bank of India</asp:ListItem>
                                                    <asp:ListItem>Union Bank of India</asp:ListItem>
                                                    <asp:ListItem>Canara Bank</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Branch</label><br />&nbsp; 
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>TT Nagar</asp:ListItem>
                                                    <asp:ListItem>Saket Nagar</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>No of Acknowledgement received from Banks</label>

                                                <asp:TextBox runat="server" ID="textbox9" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Total Cards Issued/Limit Extended</label>
                                                <asp:TextBox runat="server" ID="textbox10" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">                                                
                                                <asp:Button runat="server" ID="btnAdd" Style="margin-top:35px;" CssClass="btn btn-success"  Text="Add"></asp:Button>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <th>S.No</th>
                                                    <th>Bank Name</th>
                                                    <th>Branch</th>
                                                    <th>No of Acknowledgement received form Banks</th>
                                                    <th>Total Cards Issued/Limit Extended</th>
                                                </tr>
                                                <tr>
                                                    <td>1</td>
                                                    <td>State Bank of India</td>
                                                    <td>TT Nagar</td>
                                                    <td>1000</td>
                                                     <td>100</td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td>State Bank of India</td>
                                                    <td>Saket Nagar</td>
                                                    <td>5000</td>
                                                     <td>300</td>
                                                </tr>
                                            </table>
                                        </div>                                   
                                        
                                    </fieldset>
                                    <div class="col-md-2">
                                             <div class="form-group">
                                                <label>Total No of Acknowledgement received form Banks</label>
                                                <asp:TextBox runat="server" ID="textbox18" CssClass="form-control" Text="6000" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="col-md-2">
                                             <div class="form-group">
                                                <label>Total Cards Issued/Limit Extended<br />&nbsp;</label>
                                                <asp:TextBox runat="server" ID="textbox19" CssClass="form-control" Text="400" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="col-md-2">
                                            <div class="form-group">                                                
                                                <asp:Button runat="server" ID="Button1" Style="margin-top:55px;" CssClass="btn btn-success"  Text="Submit"></asp:Button>
                                            </div>
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
    <script>
        function TotalFormFillUp()
        {
            var Male = document.getElementById("<%= textbox3.ClientID%>").value;
            var Female = document.getElementById("<%= textbox12.ClientID%>").value;
            if(Male == "")
            {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0")
            {
                document.getElementById("<%= textbox13.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= textbox13.ClientID%>").value = "";
            }
        }
        function TotalFormCertified() {
            var Male = document.getElementById("<%= textbox4.ClientID%>").value;
            var Female = document.getElementById("<%= textbox14.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= textbox15.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= textbox15.ClientID%>").value = "";
            }
        }
        function TotalFormSubmittedtoBank() {
            var Male = document.getElementById("<%= textbox5.ClientID%>").value;
            var Female = document.getElementById("<%= textbox16.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= textbox17.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= textbox17.ClientID%>").value = "";
            }
        }
        function TotalApplyforNewCard() {
            var Male = document.getElementById("<%= textbox6.ClientID%>").value;
            var Female = document.getElementById("<%= textbox7.ClientID%>").value;
            if (Male == "") {
                Male = "0"
            }
            if (Female == "") {
                Female = "0"
            }
            var Total = parseInt(Male) + parseInt(Female);
            if (Total != "0") {
                document.getElementById("<%= textbox11.ClientID%>").value = parseInt(Total);
            }
            else {
                document.getElementById("<%= textbox11.ClientID%>").value = "";
            }
        }
    </script>
</asp:Content>

