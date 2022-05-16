﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpIncreamentPending.aspx.cs" Inherits="mis_HR_HREmpIncreamentPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employee Increment In Process ( Required Final Confirmation)</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" DataKeyNames="Increament_ID" class="table table-bordered table-striped table-hover" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("Increament_ID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Order_No" HeaderText="Order No" />
                                        <asp:BoundField DataField="Order_Date" HeaderText="Order Date" />
                                        <asp:BoundField DataField="OldLevel_Name" HeaderText="Existing Level" />
                                        <asp:BoundField DataField="Old_BasicSalary" HeaderText="Existing Basic Salary" />
                                        <asp:BoundField DataField="NewLevel_Name" HeaderText="New Level" />
                                        <asp:BoundField DataField="New_BasicSalary" HeaderText="New Basic Salary" />
                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CssClass="label label-info" runat="server" CommandName="Select">Confirm Increment</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Visible="false" Text="Confirm Increment" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Increment Approval</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Increment Date<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtIncreamentDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remark<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnApprove" class="btn btn-success" runat="server" Text="Approve" OnClick="btnApprove_Click" OnClientClick="return validateform();" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>


                <div id="myModalAll" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Increment Approval</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Increment Date<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtIncreamentDateAll" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remark<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtRemarkAll" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnApprovalAll" class="btn btn-success" runat="server" Text="Confirm Increment of Selected Employee" OnClick="btnApprovalAll_Click" OnClientClick="return validateform2();" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
        function callalert() {
            $("#myModal").modal({ backdrop: 'static', keyboard: false })
        }
        function callalertall() {
            $("#myModalAll").modal({ backdrop: 'static', keyboard: false })
        }
        
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtIncreamentDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Increment Date.\n";
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg += "Enter Remark. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnApprove.ClientID%>').value.trim() == "Approve") {
                    if (confirm("Do you really want to Approve increment ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

        }

        function validateform2() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtIncreamentDateAll.ClientID%>').value.trim() == "") {
                msg = msg + "Select Increment Date.\n";
            }
            if (document.getElementById('<%=txtRemarkAll.ClientID%>').value.trim() == "") {
                msg += "Enter Remark. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnApprovalAll.ClientID%>').value.trim() == "Confirm Increment of Selected Employee") {
                    if (confirm("Do you really want to Approve increment ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

        }



        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });
        $(document).ready(function () {
            var checkbox = $('table tbody input[type="checkbox"]:disabled');
            for (var i = 0; i < checkbox.length; i++) {
                $(checkbox[i]).parents('tr').css('background-color', '#f7efe2');
            }

        });
    </script>

</asp:Content>

