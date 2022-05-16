<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductButterSheetEntryNew.aspx.cs" Inherits="mis_dailyplan_ProductButterSheetEntryNew" %>


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
                    <h3 class="box-title">Butter Sheet Entry</h3>
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
                        <legend>In Flow</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <h9 style="color: red;">Note:- First Of All Please Click On Get Total After That Click On Save Button</h9>
                            </div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GVVariantDetail_In" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                <asp:Label ID="lblButterSheet_ID" Visible="false" runat="server" Text='<%# Eval("ButterSheet_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OB of CC (a)" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblOB_CC" runat="server" Text='<%# Eval("OB_CC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OB of Loose" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblOB_Loose" runat="server" Text='<%# Eval("OB_Loose") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OB in Cold Room (1)" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblOB_Cold_Room1" runat="server" Text='<%# Eval("OB_Cold_Room1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OB in Cold Room (2)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOB_Cold_Room2" runat="server" Text='<%# Eval("OB_Cold_Room2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WB mfd. Good" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWB_Mfg_Good" CssClass="form-control" runat="server" onkeypress="return validateDecUnit(this,event)" Text='<%# Eval("WB_Mfg_Good") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WB mfd. Sour" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWB_Mfg_Sour" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("WB_Mfg_Sour") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received from CC 1">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecieved_from_CC_1" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("Recieved_from_CC_1") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Received from CC 2" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecieved_from_CC_2" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("Recieved_from_CC_2") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Received from CC 3" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecieved_from_CC_3" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("Recieved_from_CC_3") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received from FP" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecieved_from_FP" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("Recieved_from_FP") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Received from Others" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecieved_from_others" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("Recieved_from_others") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Total" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblintotal" oncopy="return false" onpaste="return false" Enabled="false" CssClass="form-control" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                                </div>
                                
                                <hr />
                            </div>
                            



                        </div>

                    </fieldset>

                    <fieldset>
                        <legend>Out Flow</legend>
                        <div class="row">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVVariantDetail_Out" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id_Out" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue to Processing" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssue_to_Processing" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("Issue_to_Processing") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue for Ghee" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssue_for_Ghee" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("Issue_for_Ghee") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Butter Milk" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtButter_Milk" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("Butter_Milk") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue to Others" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssue_to_other" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("Issue_to_other") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue for FP">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssue_for_FP" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("Issue_for_FP") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue for Sale">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssue_for_Sale" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("Issue_for_Sale") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CB of CC">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCB_CC" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CB_CC") %>' onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CB Loose">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCB_Loose" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("CB_Loose") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CB Cold Room 1">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCB_Cold_Room1" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CB_Cold_Room1") %>' onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CB Cold Room 2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCB_Cold_Room2" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CB_Cold_Room2") %>' onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Sample" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSample" CssClass="form-control" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("Sample") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblouttotal" oncopy="return false" Style="width: 70px;" onpaste="return false" Enabled="false" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                        

                                    </td>
                                </tr>


                            </table>
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
