<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemReceipeMaster.aspx.cs" Inherits="mis_dailyplan_ItemReceipeMaster" %>

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
                    <h3 class="box-title">Item Receipe Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>   
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>                            
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Product Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlProductSection" class="text-danger"></span></small>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>Product/Item<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlProduct" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Item/Ingredients<span class="text-danger">*</span></label>   
                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlItem" class="text-danger"></span></small>                            
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Quantity<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtQuantity" class="text-danger"></span></small>
                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>Quantity Unit</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control">                       
                                </asp:DropDownList>
                                <small><span id="valddlUnit" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>                  
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="ItemReceipeMaster.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" ShowHeader="true" CssClass="datatable table table-bordered table-condensed" AutoGenerateColumns="false" DataKeyNames="Recepie_ID" EmptyDataText="No Record Found" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:BoundField DataField="Office_Name" HeaderText="Dugdh Sangh" />
                                   <asp:BoundField DataField="ProductSection_Name" HeaderText="Product Section" />
                                   <asp:BoundField DataField="Product" HeaderText="Product/Item" />
                                   <asp:BoundField DataField="Item" HeaderText="Item" />
                                   <asp:BoundField DataField="Item_Quantity" HeaderText="Quantity" />
                                   <asp:BoundField DataField="UnitName" HeaderText="Quantity Unit" />
                                   <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandName="Select">Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="label label-danger" CommandName="Delete" OnClientClick="return confirm('Record will be deleted. Are you sure want to continue?');">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
  
        function validateform() {
            debugger;
            var msg = "";
            $("#valddlDS").html("");
            $("#valddlProductSection").html("");
            $("#valddlProduct").html("");
            $("#valddlItem").html("");
            $("#valtxtQuantity").html("");          
            if (document.getElementById('<%=ddlDS.ClientID%>').selectedIndex == 0) {
                msg += "Select Dugdh Sangh. \n"
                $("#valddlDS").html("Select Dugdh Sangh");
            }
            if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
                $("#valddlProductSection").html("Select Product Section");
            }
            if (document.getElementById('<%=ddlProduct.ClientID%>').selectedIndex == 0) {
                msg += "Select Product/Item. \n"
                $("#valddlProduct").html("Select Product/Item");
            }
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg += "Select Item. \n"
                $("#valddlItem").html("Select Item");
            }
            if (document.getElementById('<%=txtQuantity.ClientID%>').value.trim() == "") {
                msg += "Enter Quantity. \n"
                $("#valtxtQuantity").html("Enter Quantity");
            }
            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }
        }

    </script>
</asp:Content>

