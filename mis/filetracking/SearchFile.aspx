<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SearchFile.aspx.cs" Inherits="mis_filetracking_SearchFile" %>

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
                            <h3 class="box-title">फ़ाइल / पत्र / क्यूआर कोड खोजें</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>File/ Letter No. / QR Code<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtFileNumber" class="form-control" runat="server" placeholder="File/ Letter No. / QR Code" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtFileNumber" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-block btn-success" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg2" runat="server" Text="" Visible="true" Style="color: red; font-size: 17px;"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="File_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("File_Status")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="प्राथमिकता">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="File_No" HeaderText="फ़ाइल / नोट शीट/ पत्र संख्या" />
                                            <asp:BoundField DataField="File_Title" HeaderText="दस्तावेज़ का शीर्षक" />
                                            <asp:BoundField DataField="File_UpdatedOn" HeaderText="फाइल बनने की दिनांक" />
                                             <asp:BoundField DataField="FileReceivingDate" HeaderText="फाइल प्राप्त करने की दिनांक" />
                                            <asp:TemplateField HeaderText="फाइल का विवरण">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" CssClass="label label-default" runat="server" CommandName="select">View More </asp:LinkButton>
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

            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform() {
            var msg = "";
            $("#valtxtFileNumber").html("");
            if (document.getElementById('<%=txtFileNumber.ClientID%>').value.trim() == "") {
                msg = msg + "Enter File/ Letter No. / QR Code. \n";
                $("#valtxtFileNumber").html("Enter File/ Letter No. / QR Code.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
        }
    </script>
</asp:Content>
