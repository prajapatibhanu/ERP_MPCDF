<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EditOutwardDetail.aspx.cs" Inherits="mis_filetracking_EditOutwardDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/datepicker3.css" rel="stylesheet" />
    <style>
        .inline-rb label {
            margin-left: 5px;
        }

        /*table#ContentBody_rbtType, table#ContentBody_rbtType td {
            border: 0 !important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-header">
                            <h3 class="box-title">Edit Outward Letter Detail</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Letter No (पत्र संख्या)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtLetterNo" runat="server" placeholder="Enter Letter No." class="form-control" ClientIDMode="Static" Style="text-transform: uppercase" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtLetter_No" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSearch" runat="server" class="btn btn-block btn-success" Text="Search" OnClientClick="return validateform();" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="EditOutwardDetail.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div id="InsertDiv" runat="server">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Letter Copy To</label>
                                                <asp:TextBox ID="txtCopyTo" CssClass="form-control" runat="server" placeholder="Enter Copy To" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" Style="margin-top: 22px;" runat="server" CssClass="btn btn-success" Text="ADD" OnClientClick="return validate();" OnClick="btnAdd_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridView2" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Letter Copy To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCopyTo" runat="server" Text='<%# Eval("CopyTo").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="25">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" CssClass="label label-default" Text="Delete" runat="server" OnClick="OnDelete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button ID="btnSaveData" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSaveData_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblGridMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    <div id="UpdateGrid" runat="server">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="30">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCopyTo" runat="server" Text='<%# Eval("CopyTo") %>'></asp:Label>
                                                        <asp:Label ID="lblCopyTo_ID" runat="server" Visible="false" Text='<%# Eval("CopyTo_ID") %>'></asp:Label>
                                                        <asp:TextBox ID="txtCopyTo" Visible="false" MaxLength="100" runat="server" Text='<%# Eval("CopyTo") %>' CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Visible="false" ID="gvtxtCopyTo" ValidationGroup="b" runat="server" ControlToValidate="txtCopyTo" ErrorMessage="Please Enter Copy To." Text=" Enter Copy To."></asp:RequiredFieldValidator>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" ToolTip="Edit" CausesValidation="false" OnClick="lnkEdit_Click" OnClientClick="return confirm('Are you sure to Edit?')"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdate" Visible="false" runat="server" ValidationGroup="b" ToolTip="Update" OnClick="lnkUpdate_Click" OnClientClick="return confirm('Are you sure to Update?')"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkCancel" Visible="false" runat="server" CausesValidation="false" ToolTip="Cancel" Style="color: red;" OnClick="lnkCancel_Click" OnClientClick="return confirm('Are you sure to Cancel?')"><i class="fa fa-remove"></i></asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
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
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform() {
            var msg = "";
            $("#valtxtLetter_No").html('');
            if (document.getElementById('<%=txtLetterNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Letter No. \n";
                $("#valtxtLetter_No").html("Enter Letter No.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function validate() {
            var msg = "";
            if (document.getElementById('<%=txtCopyTo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Copy To. \n";
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

