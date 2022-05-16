<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemVariant.aspx.cs" Inherits="mis_Masters_ItemVariant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function validatename(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                return false;
            }
            return true;
        }
    </script>
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="col-md-12">
        <div class="card card-primary">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="card-header">
                <h3 class="card-title">Item Variant Master</h3>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Category<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rq1" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlItemCategory" InitialValue="0" ErrorMessage="Select Item Category" Text="<i class='fa fa-exclamation-circle' title='Please Select Item Category !'></i>"></asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList ID="ddlItemCategory" runat="server" Width="100%" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ItemCategory_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Item<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlItem" InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Please Select Item !'></i>"></asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList ID="ddlItem" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Variant Name<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="txtItemVariantName" ErrorMessage="Enter Item Variant Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="save" runat="server" ControlToValidate="txtItemVariantName" ForeColor="Red" Display="Dynamic"
                                    ValidationExpression="[a-zA-Z]*$" ErrorMessage="*Valid characters: Alphabets." Text="<i class='fa fa-exclamation-circle' title='Please Enter Item Variant Name !'></i>" />
                            </span>
                            <asp:TextBox ID="txtItemVariantName" autocomplete="off" Width="100%" runat="server" placeholder="Item Variant Name" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Unit<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Select Unit" Text="<i class='fa fa-exclamation-circle' title='Please Select Unit !'></i>"></asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" CssClass="form-control select2">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%-- <div class="col-md-3">
                            <div class="form-group">
                                <label>Item SKU Code</label>
                                <asp:TextBox ID="txtitemaliscode" ToolTip="Stock Keeping Unit" Width="100%" autocomplete="off" runat="server" MaxLength="50" placeholder="SKU Code" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>HSN Code<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlHsnCode" InitialValue="0" ErrorMessage="Select HSN Code" Text="<i class='fa fa-exclamation-circle' title='Please Select HSN Code !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlHsnCode" runat="server" Width="100%" CssClass="form-control select2">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Brand</label>
                                <asp:TextBox ID="txtItemBrand" autocomplete="off" Width="100%" runat="server" placeholder="Item Brand" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>

                      
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dimension (L x W x H)</label>
                                <asp:TextBox ID="txtItemSize" autocomplete="off" runat="server" Width="100%" placeholder="Item Dimension (L x W x H)" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dimension Class</label>
                                <asp:DropDownList ID="ddlDimensionClass" runat="server" Width="100%" CssClass="form-control select2">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Milimeter" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Centimeter" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Inch" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Feet" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Meter" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <%-- <div class="col-md-6">
                        </div>--%>

                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Item Specification</label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Display="Dynamic" runat="server" ControlToValidate="txtItemSpecification"
                                ErrorMessage="Only alphabet allow" ValidationGroup="save" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtItemSpecification" autocomplete="off" Width="100%" runat="server" placeholder="Item Specification" CssClass="form-control" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Item SKU Code</label>
                            <asp:TextBox ID="txtitemaliscode" ToolTip="Stock Keeping Unit" Width="100%" autocomplete="off" runat="server" MaxLength="50" placeholder="SKU Code" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="form-group">
                            <label>HSN Code<span class="text-danger">*</span></label>
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="save" runat="server" ControlToValidate="ddlHsnCode" InitialValue="0" ErrorMessage="Select HSN Code" Text="<i class='fa fa-exclamation-circle' title='Please Select HSN Code !'></i>"></asp:RequiredFieldValidator>
                            </span>
                            <asp:DropDownList ID="ddlHsnCode" runat="server" Width="100%" CssClass="form-control select2">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Item Brand</label>
                            <asp:TextBox ID="txtItemBrand" autocomplete="off" Width="100%" runat="server" placeholder="Item Brand" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Dimension (L x W x H)</label>
                            <asp:TextBox ID="txtItemSize" autocomplete="off" runat="server" Width="100%" placeholder="Item Dimension (L x W x H)" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Dimension Class</label>
                            <asp:DropDownList ID="ddlDimensionClass" runat="server" Width="100%" CssClass="form-control select2">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Milimeter" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Centimeter" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Inch" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Feet" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Meter" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Applicable on<span class="text-danger">*</span></legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkOfficeAll" runat="server" Text="ALL" onclick="CheckOfficeAll();" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table customCSS cbl_all_Office" RepeatColumns="5" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <small><span id="valchkOffice" class="text-danger"></span></small>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="save" CssClass="btn btn-block btn-primary" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" ValidationGroup="save" class="btn btn-block btn-default" Text="Clear" ClientIDMode="Static" OnClick="btnClear_Click" />
                                <%--<a href="ItemMaster.aspx" class="btn btn-block btn-default">Clear</a>--%>
                            </div>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GVItemDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" CssClass="datatable table table-hover table-bordered" AutoGenerateColumns="False" DataKeyNames="Item_Id" runat="server" OnRowDeleting="GVItemDetail_RowDeleting" OnSelectedIndexChanged="GVItemDetail_SelectedIndexChanged" OnRowCommand="GVItemDetail_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Item_Id" HeaderText="Item Id" Visible="false" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Item / Variant Name" />
                                        <asp:BoundField DataField="ItemCatName" HeaderText="Item Group" />
                                        <asp:BoundField DataField="ItemTypeName" HeaderText="Item Type" />
                                        <asp:BoundField DataField="UnitName" HeaderText="Item Unit" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="button button-mini button-green" CausesValidation="False" CommandArgument='<%# Bind("Item_Id") %>' CommandName="Select" Text="Edit"></asp:LinkButton>
                                                <%-- <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" Visible="false" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Item Name will be deleted. Are you sure want to continue?');"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="OfficeModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Applicable On Office</h4>
                            </div>
                            <div class="modal-body" style="height: 420px; overflow: scroll;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Item Name" DataField="ItemName" />
                                                <asp:BoundField HeaderText="Office Name" DataField="Office_Name" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <p>
                                    <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                                <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function CheckOfficeAll() {
            if (document.getElementById('<%=chkOfficeAll.ClientID%>').checked == true) {
                $('.cbl_all_Office').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                $('.cbl_all_Office').each(function () {
                    $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                });
            }
            return false;
        }
        function callalert() {
            debugger;
            $("#OfficeModal").modal('show');
        }

        function ValidatePage() {
            if (typeof (Page_ClientValidate)) {
                Page_ClientValidate('save');
            }
            if ($('#chkOffice input:checked').length > 0) {
                if (Page_IsValid) {

                    if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {
                         document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                         $('#myModal').modal('show');
                         return false;
                     }

                     if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                         document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                        $('#myModal').modal('show');
                        return false;
                    }
                }
            }
            else {
                alert('Please select atleast one Office')
                return false;
            }
        }

    </script>
</asp:Content>

