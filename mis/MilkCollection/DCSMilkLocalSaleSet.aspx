<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DCSMilkLocalSaleSet.aspx.cs" Inherits="mis_MilkCollection_DCSMilkLocalSaleSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
     
    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=gvItemDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>

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
                    <h3 class="box-title">Item / Milk Rate Master </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Detail</legend>

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
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtTransactionDt" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtTransactionDt" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

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


                            </fieldset>
                        </div>
                    </div>


                    <div class="row" runat="server" visible="false" id="divIteminfo">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Items Detail</legend>

                                <asp:GridView ID="gvItemDetails" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                All
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <%# Eval("ItemName") %>(<%# Eval("UnitName") %>)
                                                <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                <asp:Label ID="lblUnit_id" Visible="false" runat="server" Text='<%# Eval("Unit_id") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="DCS Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDistributorDCSPrice" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtDistributorDCSPrice_TextChanged" MaxLength="10" placeholder="Enter DCS Price" ValidationGroup="a" Text='<%# Eval("DistributorDCSPrice") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator101A" ValidationGroup="a"
                                                        ErrorMessage="Enter DCS Price" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Price!'></i>"
                                                        ControlToValidate="txtDistributorDCSPrice" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51B" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDistributorDCSPrice" ErrorMessage="Enter Valid DCS Price" Text="<i class='fa fa-exclamation-circle' title='Enter Valid DCS Price!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DCS Margine">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDCSMargine" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtDCSMargine_TextChanged" MaxLength="10" placeholder="Enter DCS Margine" ValidationGroup="a" Text='<%# Eval("DCSMargine") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator102C" ValidationGroup="a"
                                                        ErrorMessage="Enter DCS Margine" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net DCS Margine!'></i>"
                                                        ControlToValidate="txtDCSMargine" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52D" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtDCSMargine" ErrorMessage="Enter Valid DCS Margine" Text="<i class='fa fa-exclamation-circle' title='Enter Valid DCS Margine!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Secretary Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSecretaryPrice" Enabled="false" onkeypress="return validateDec(this,event)" AutoPostBack="true" MaxLength="10" placeholder="Enter Secretary Price" ValidationGroup="a" Text='<%# Eval("SecretaryPrice") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator101E" ValidationGroup="a"
                                                        ErrorMessage="Enter Secretary Price" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Secretary Price!'></i>"
                                                        ControlToValidate="txtSecretaryPrice" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator51F" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtSecretaryPrice" ErrorMessage="Enter Valid Secretary Price" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Secretary Price!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Secretary Margine">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSecretaryMargine" onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="txtSecretaryMargine_TextChanged" MaxLength="10" placeholder="Enter Secretary Margine" ValidationGroup="a" Text='<%# Eval("SecretaryMargine") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator102G" ValidationGroup="a"
                                                        ErrorMessage="Enter Secretary Margine" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Net Secretary Margine!'></i>"
                                                        ControlToValidate="txtSecretaryMargine" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator52H" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtSecretaryMargine" ErrorMessage="Enter Valid Secretary Margine" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Secretary Margine!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRP">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMRP" Enabled="false" onkeypress="return validateDec(this,event)" MaxLength="10" placeholder="Enter MRP" ValidationGroup="a" Text='<%# Eval("MRP") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator103I" ValidationGroup="a"
                                                        ErrorMessage="Enter MRP" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter MRP!'></i>"
                                                        ControlToValidate="txtMRP" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator53J" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtMRP" ErrorMessage="Enter Valid MRP" Text="<i class='fa fa-exclamation-circle' title='Enter Valid MRP!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Effective from Date">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEffectiveDate" data-date-start-date="0d" Text='<%# Eval("EffectiveDate") %>' onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Effective from Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>





                            </fieldset>
                        </div>

                    </div>

                    <div class="row" runat="server" visible="false" id="divpageAction">
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
                            <h3 class="box-title">All Milk Rate Details [Office Wise]</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="gvItemSaleRate" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="lblItemSaleRate_Id" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <%# Eval("ItemName") %> (<%# Eval("UnitName") %>)
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DCS Price">
                                                <ItemTemplate>
                                                    <%# Eval("DistributorDCSPrice") %> &nbsp;Rs
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DCS Margine">
                                                <ItemTemplate>
                                                    <%# Eval("DCSMargine") %> &nbsp;Rs
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Secretary Price">
                                                <ItemTemplate>
                                                    <%# Eval("SecretaryPrice") %> &nbsp;Rs
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Secretary Margine">
                                                <ItemTemplate>
                                                    <%# Eval("SecretaryMargine") %> &nbsp;Rs
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <%# Eval("MRP") %> &nbsp;Rs
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="Effective Date">
                                                <ItemTemplate>
                                                    <%# Eval("EffectiveDate") %>
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


        //$(function () {
        //    $("#txtTransactionDt").datepicker({
        //        endDate: "0d",
        //        startDate: "-365d",
        //        showDate: "-365d",
        //        autoclose: true
        //    });

        //});

    </script>
</asp:Content>
