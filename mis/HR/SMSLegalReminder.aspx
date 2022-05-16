<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SMSLegalReminder.aspx.cs" Inherits="mis_SMSLegalReminder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .para-title {
            color: tomato;
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Corporation's Upcoming Legal Hearings in Next 3 Days - Send Reminder SMS</h3>
                </div>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12 para-title">
                            <p>अगले तीन दिवस में कोर्ट की सुनवाई|</p>
                        </div>
                    </div>


                    <div class="row first-block">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtmessage" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Text="Eg :  Hearing Reminder: Your Case No. (2/Civil Case/Consumer Court) Next Hearing on 30/01/2019
" ReadOnly="true"></asp:TextBox>
                            <p>
                                <i><small>
                                    <ul>
                                        <li>SMS 140 शब्दों से छोटा लिखें  |</li>
                                        <li>SMS अंग्रेजी में ही लिखें |</li>
                                    </ul>
                                </small></i>
                            </p>
                        </div>
                        <div class="col-md-8">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="30">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                         </asp:TemplateField>
                                         <asp:BoundField DataField="OIC_Name" HeaderText="OIC NAME" />
                                         <asp:BoundField DataField="Emp_MobileNo" HeaderText="OIC MOBILE NO" />
                                         <asp:BoundField DataField="Hearing_Date" HeaderText="HEARING DATE" />
                                         <asp:BoundField DataField="Case_No" HeaderText="CASE NO" />
                                         <asp:BoundField DataField="Case_Type" HeaderText="CASE TYPE" />
                                         <asp:BoundField DataField="Case_CourtType" HeaderText="COURT TYPE" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="form-group pull-left">
                                <asp:Button ID="btnSendSMS" runat="server" Text="Send Reminder SMS" CssClass="btn btn-success" OnClick="btnSendSMS_Click" OnClientClick="return SendSMS();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>

                    </hr>
                    <div class="row">
                        <div class="col-md-12 para-title">
                            <p>वो OIC जिनको आज कोर्ट की सुनवाई का अनुस्मारक SMS भेजा जा चुका है|</p>
                        </div>
                    </div>
                    <div class="row second-block">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpName" HeaderText="EMPLOYEE NAME" />
                                        <asp:BoundField DataField="SmsMobile" HeaderText="MOBILE NUMBER" />
                                        <asp:BoundField DataField="SmsDate" HeaderText="SMS DATE" />
                                        <asp:BoundField DataField="SmsContent" HeaderText="SMS" />
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
    <script type="text/javascript">
        function checkAll(objRef) {
            debugger;
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


        function SendSMS() {
            if (document.getElementById('<%=btnSendSMS.ClientID%>').value.trim() == "Send SMS") {
                if (confirm("Do you really want to Send SMS ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

