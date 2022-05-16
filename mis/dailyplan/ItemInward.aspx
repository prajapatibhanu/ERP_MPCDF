<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemInward.aspx.cs" Inherits="mis_dailyplan_ItemInward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }

        .capitalize {
            text-transform: capitalize;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Opening Stock</h3>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Opening as on Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Category<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ItemGroup" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ItemGroup_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valItemGroup" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Group<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlItemCategory" runat="server" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged1" AutoPostBack="true" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlItemCategory" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Name<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlItem" class="text-danger"></span></small>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Item Unit<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Quantity</label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" onkeypress="return validateNum(event)" placeholder="Enter Quantity" MaxLength="50">                                   
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Batch No</label>
                                <asp:TextBox ID="txtBatchNo" runat="server" CssClass="form-control" placeholder="Enter Batch No" MaxLength="50">                                   
                                </asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Lot No</label>
                                <asp:TextBox ID="txtLotNo" ToolTip="Stock Lot No" autocomplete="off" runat="server"
                                    placeholder="Lot No" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Remark</label>
                                <asp:TextBox ID="txtReceivingRemark" autocomplete="off" runat="server" placeholder="Remark" CssClass="form-control capitalize" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="ItemInward.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GVItemDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" CssClass="datatable table table-hover table-bordered" AutoGenerateColumns="False" DataKeyNames="ItmStock_id" runat="server" OnRowDeleting="GVItemDetail_RowDeleting" OnSelectedIndexChanged="GVItemDetail_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("ItemName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblID" Text='<%# Eval("ItmStock_id").ToString() %>' CssClass="hidden" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TranDt" HeaderText="Opening As On" />
                                    <%--                                    <asp:BoundField DataField="UnitName" HeaderText="Item Unit" />--%>
                                    <asp:BoundField DataField="BatchNo" HeaderText="Batch No" />
                                    <asp:BoundField DataField="LotNo" HeaderText="Lot No" />
                                    <asp:BoundField DataField="Inward" HeaderText="Quantity" />
                                    <asp:BoundField DataField="ReceivingRemark" HeaderText="Remark" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="label Aselect1 label-info" CausesValidation="False" CommandName="Select" Text="View / Edit"></asp:LinkButton>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false


            ,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('.box-title').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.box-title').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });




        function callalert() {
            debugger;
            $("#OfficeModal").modal('show');
        }
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtItemName").html("");
            $("#valItemGroup").html("");
            $("#valddlItemCategory").html("");
            $("#valddlUnit").html("");
            $("#valtxtitemaliscode").html("");
            $("#valddlHsnCode").html("");
            $("#valchkOffice").html("");
            $("#valddlpurchaseledger").html("");
            $("#valddlsalesledger").html("");

            if (document.getElementById('<%=ItemGroup.ClientID%>').selectedIndex == 0) {
                msg += "Select Category. \n"
                $("#valItemGroup").html("Select Category");
            }
            if (document.getElementById('<%=ddlItemCategory.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Group. \n"
                $("#valddlItemCategory").html("Select Item Group");
            }
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg += "Select Item \n"
                $("#valddlItem").html("Select Item");
            }

            if (msg != "") {
                alert(msg)
                return false;

            }
            else {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
            }
        }
        function getConfirmation() {
            debugger;
            var retVal = confirm("Record will be deleted. Are you sure want to continue?");
            if (retVal == true) {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
            else {

                return false;
            }
        }

    </script>
</asp:Content>
