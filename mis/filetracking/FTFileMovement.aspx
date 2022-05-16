<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FTFileMovement.aspx.cs" Inherits="mis_filetracking_SearchFile" %>

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
                            <h3 class="box-title">File Movement Detail</h3>
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
                            <div id="divfiledetail" runat="server">
                                <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                <legend>File Detail</legend>
                                        <asp:HyperLink ID="hpview" CssClass="label label-primary" runat="server"></asp:HyperLink>
                                <asp:DetailsView ID="Gvfiledetail" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                    <Fields>
                                        <asp:TemplateField HeaderText="प्राथमिकता">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("File_Priority".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="File_No" HeaderText="फ़ाइल / नोट शीट/ पत्र संख्या" />
                                        <asp:BoundField DataField="File_Type" HeaderText="फाइल का प्रकार" />
                                        <asp:BoundField DataField="Department_Name" HeaderText="विभाग" />
                                        <asp:BoundField DataField="File_Title" HeaderText="दस्तावेज़ का शीर्षक" />
                                        <asp:BoundField DataField="QRCode" HeaderText="बार कोड" />   
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpview" Target="_blank" CssClass="btn btn-sm btn-primary" runat="server" NavigateUrl='<%# string.Format("ViewFileStatus.aspx?FileID="+ APIProcedure.Client_Encrypt(Eval("File_ID").ToString())+"") %>'>विवरण देखें</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                                                                
                                    </Fields>
                                </asp:DetailsView>  
                            </fieldset>
                                </div>
                            </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg2" runat="server" Text="" Visible="true" Style="color: red; font-size: 17px;"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.no" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:BoundField DataField="FileOnDesk" HeaderText="File Status" />
                                            <asp:BoundField DataField="Forwarded_Officer" HeaderText="Officers" />
                                            <asp:BoundField DataField="Forwarded_UpdatedOn" HeaderText="Releasing Date" />
                                           <%-- <asp:BoundField DataField="Emp_Name" HeaderText="Forwarded By" /> --%>
                                             <asp:BoundField DataField="Forwarded_Description" HeaderText="Description" />                                     
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
    <link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
<%--    <link href="../finance/css/buttons.dataTables.min.css" rel="stylesheet" />--%>
    <link href="../finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../finance/js/jquery.dataTables.min.js"></script>
<%--    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>
    <script src="../finance/js/fromkeycode.js"></script>--%>
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
        $('.datatable').DataTable({
            paging: false,
            sorting:false
        });
    </script>
</asp:Content>
