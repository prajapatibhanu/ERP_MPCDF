<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="InwardItem.aspx.cs" Inherits="mis_MilkCollection_InwardItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

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
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Inward Item </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Office Detail</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Block <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtBlock" CssClass="form-control" MaxLength="20" placeholder="Block" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTransactionDt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Items Detail</legend>

                                <div class="col-md-3">
                                    <label>Item Category<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitemcategory" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Category!'></i>" ErrorMessage="Select Item Category" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemcategory" OnSelectedIndexChanged="ddlitemcategory_SelectedIndexChanged" OnInit="ddlitemcategory_Init" AutoPostBack="true" CssClass="form-control select2" runat="server" ValidationGroup="a"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>Item Type <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Type!'></i>" ErrorMessage="Select Item Type" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemtype" OnInit="ddlitemtype_Init" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2" runat="server" ValidationGroup="a"></asp:DropDownList>
                                    </div>

                                </div>

                                <div class="col-md-3">
                                    <label>Item Name <span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="ddlitemname" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Item Name!'></i>" ErrorMessage="Select Item Name" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlitemname" OnSelectedIndexChanged="ddlitemname_SelectedIndexChanged" OnInit="ddlitemname_Init" AutoPostBack="true" CssClass="form-control select2" runat="server" ValidationGroup="a"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Item Quantity 
                                            <asp:Label ID="lblunitnameH" runat="server"></asp:Label>
                                             <asp:HiddenField runat="server" ID="HFsalerate" />
                                             <asp:HiddenField runat="server" ID="HFFinalAmount" />
                                            <span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="a"
                                                ErrorMessage="Enter Net Quantity" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Quantity!'></i>"
                                                ControlToValidate="txtitemqty" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtitemqty" ErrorMessage="Enter Valid Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Quantity!'></i>"></asp:RegularExpressionValidator>

                                        </span>
                                        <asp:TextBox ID="txtitemqty" Enabled="false" MaxLength="10" placeholder="Enter Item Quantity" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">

                                        <label>
                                            Remarks</label>
                                        <asp:TextBox ID="txtremarks" MaxLength="100" TextMode="MultiLine" placeholder="Enter Remarks" CssClass="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>

                            </fieldset>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Action</legend>
                                <div class="col-md-1" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-1" style="margin-top: 20px;">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>


                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Inward Item Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date  </label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="TxtFdate" AutoPostBack="true" OnTextChanged="TxtFdate_TextChanged" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="gvOpeningStock" DataKeyNames="ItmStock_id" runat="server" OnRowDataBound="gvOpeningStock_RowDataBound" EmptyDataText="No records Found" OnRowCommand="gvOpeningStock_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="lblItmStock_id" runat="server" Text='<%#Eval("ItmStock_id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inward Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTranDt" runat="server" Text='<%# Eval("TranDt", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Group">
                                                <ItemTemplate>
                                                    <%# Eval("ItemCatName") %>
                                                    <asp:Label ID="lblWarehouse_id" Visible="false" runat="server" Text='<%# Eval("Warehouse_id") %>'></asp:Label>
                                                    <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                    <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    <asp:Label ID="lblTransactionFrom" Visible="false" runat="server" Text='<%# Eval("TransactionFrom") %>'></asp:Label>
                                                    <asp:Label ID="lblremark" Visible="false" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    <asp:Label ID="lblRate" Visible="false" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                    <asp:Label ID="lblAmount" Visible="false" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Sub-Group">
                                                <ItemTemplate>
                                                    <%# Eval("ItemTypeName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <%# Eval("ItemName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCr" runat="server" Text='<%# Eval("Cr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <%# Eval("UQCCode") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ItmStock_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ItmStock_id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }


        function allnumeric(inputtxt) {
            var numbers = /^[0-9]+$/;
            if (inputtxt.value.match(numbers)) {
                alert('only number has accepted....');
                document.form1.text1.focus();
                return true;
            }
            else {
                alert('Please input numeric value only');
                document.form1.text1.focus();
                return false;
            }
        }


        $(function () {
            $("#txtTransactionDt").datepicker({
                endDate: "0d",
                startDate: "-365d",
                showDate: "-365d",
                autoclose: true
            });

        });

    </script>
</asp:Content>

