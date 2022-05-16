<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductCreamSheetEntry.aspx.cs" Inherits="mis_dailyplan_ProductCreamSheetEntry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        .customCSS label {
            padding-left: 10px;
        }

        /*table {
            white-space: nowrap;
        }*/

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
    <asp:ScriptManager runat="server" ID="SM1">
    </asp:ScriptManager>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vs2" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Cream Sheet Entry</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" visible="false">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" AutoPostBack="true" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Product Details</legend>

                        <div class="row">
                            <div class="col-md-12">
                                <h9 style="color: red;">Note:- First Of All Please Click On Get Total After That Click On Save Button</h9>
                            </div>
                            <hr />
                        </div>

                        <div class="row">

                            <table class="table table-bordered">
                                <tr class="text-center">
                                    <th colspan="10">
                                        <asp:Label ID="lblProductNameInner" Text="CREAM ACCOUNT" runat="server"></asp:Label></th>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:GridView ID="GVVariantDetail_In" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." OnRowCreated="GVVariantDetail_In_RowCreated">
                                             <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CST 1" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCST1" runat="server" Text='<%# Eval("OpeningCST1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CST 2" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCST2" runat="server" Text='<%# Eval("OpeningCST2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="CST 3" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClosingBalanceGood" runat="server" Text='<%# Eval("OpeningBalanceGood") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CST 4" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClosingBalanceSour" runat="server" Text='<%# Eval("OpeningBalanceSour") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cream Obtained From Good Milk" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGoodMilk" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CreamObtFrom_GoodMilk") %>' onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cream Obtained From Sour Milk" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSourMilk" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CreamObtFrom_SourMilk") %>' onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cream Received From Processing Section" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRcvdfromProcSctn" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("CreamRcvdFromProcessing") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblIntotal" Enabled="false" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <asp:GridView ID="GVVariantDetail_Out" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." OnRowCreated="GVVariantDetail_Out_RowCreated">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id_Out" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue For White Butter Good" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_WhiteButterGood" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("IssueFor_WhiteButterGood") %>' onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue For White Butter Sour" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_WhiteButterSour" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("IssueFor_WhiteButterSour") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue For Table Butter" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_TableButter" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("IssueFor_TableButter") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Issue For Cooking Butter" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_CookingButter" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("IssueFor_CookingButter") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return to Processing Section" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReturntoProcessingSection" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("CreamReturntoProcessing") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               


                                                <%--<asp:TemplateField HeaderText="CST 1" HeaderStyle-Width="10%">--%>
                                                    <asp:TemplateField HeaderText="Issue For Sale" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCreamIssueforSale" oncopy="return false" onpaste="return false" Text='<%# Eval("IssueFor_Sale") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderText="CST 2" HeaderStyle-Width="10%">--%>
                                                 <asp:TemplateField HeaderText="Issue For Others" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCreamIssueforothers" oncopy="return false" Text='<%# Eval("IssueFor_Others") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CST 1" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCST1" oncopy="return false" onpaste="return false" Text='<%# Eval("CST1") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CST 2" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCST2" oncopy="return false" onpaste="return false" Text='<%# Eval("CST2") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="CST 3" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCLClosing" oncopy="return false" onpaste="return false" Text='<%# Eval("Closing") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CST 4" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSourQty" oncopy="return false" onpaste="return false" Text='<%# Eval("SourQty") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblouttotal" oncopy="return false" onpaste="return false" style="width : 70px;" Enabled="false" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblGainLoss" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <table class="table dataTable table-bordered">
                                            <tr style="background-color: #ff874c; color: black">
                                                <td><b>Gain/Loss</b></td>
                                                
                                                <td><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="txtGainLossQtyInKg" Enabled="true" CssClass="form-control" runat="server"></asp:Label></td>
                                               <td><b>Kgs. Fat<br />
                                                </b>
                                                    <asp:Label ID="txtGainLossFatInKg" Enabled="true" CssClass="form-control" runat="server"></asp:Label></td>
                                                <td><b>Kgs. SNF<br />
                                                </b>
                                                    <asp:Label ID="txtGainLossSnfInKg" Enabled="true" CssClass="form-control" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txtReceived_From" TextMode="MultiLine" placeholder="Remark.........." oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="text-align: center;">
                                    <asp:Button ID="btnGetTotal" OnClick="btnGetTotal_Click" runat="server" CssClass="btn btn-primary" Text="Get Total" />
                                    <asp:Button ID="btnPopupSave_Product" Enabled="false" OnClientClick="return ValidateT_Product()" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" />
                                </div>
                            </div>
                        </div>

                    </fieldset>

                </div>
            </div>

            <div class="modal fade" id="myModalT_P" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT_P">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT_P" OnClick="btnYesT_P_Click1" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button3" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
		function validateDecUnit(el, evt) {
            var digit = 3;
             var charCode = (evt.which) ? evt.which : event.keyCode;
             if (digit == 0 && charCode == 46) {
                 return false;
             }

             var number = el.value.split('.');
             if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             //just one dot (thanks ddlab)
             if (number.length > 1 && charCode == 46) {
                 return false;
             }
             //get the carat position
             var caratPos = getSelectionStart(el);
             var dotPos = el.value.indexOf(".");
             if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                 return false;
             }
             return true;
         }
        function ValidateT_Product() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnPopupSave_Product.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT_P').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnPopupSave_Product.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT_P').modal('show');
                    return false;
                }
            }
        }

    </script>

</asp:Content>


