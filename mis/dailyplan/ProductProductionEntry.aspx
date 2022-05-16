<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductProductionEntry.aspx.cs" Inherits="mis_dailyplan_ProductProductionEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        .customCSS label {
            padding-left: 10px;
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
                    <h3 class="box-title">Product Sheet</h3>
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
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvmttos" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Variant Name" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%--Enabled='<%# Eval("PPSM_S").ToString() == "" ? true : false %>'--%>
                                                    <asp:LinkButton Enabled='<%# Eval("PPSM_S").ToString() == "" ? true : false %>' ID="lnkbtnVN" CssClass="btn btn-block btn-secondary" OnClick="lnkbtnVN_Click" Text='<%#Eval("ItemTypeName") %>' CommandArgument='<%#Eval("ItemType_id") %>' runat="server"></asp:LinkButton>
                                                    <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    Total
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prev. Demand </br>(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand_InPkt" Text='<%# Eval("Prev_Demand_InPkt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="Prev_Demand_InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prev. Demand </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr" Text='<%# Eval("Prev_DemandInLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Demand </br>(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InPkt" runat="server" Text='<%# Eval("Current_Demand_InPkt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Demand </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InLtr" runat="server" Text='<%# Eval("Current_Demand_InLtr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="status" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPPSM_StatusMSG" runat="server" Text='<%# Eval("PPSM_StatusMSG") %>'></asp:Label>
                                                    <asp:Label ID="lblPPSM_S" Visible="false" runat="server" Text='<%# Eval("PPSM_S") %>'></asp:Label>
                                                    <asp:Label ID="lblPPSM_Id" Visible="false" runat="server" Text='<%# Eval("PPSM_Id") %>'></asp:Label>
                                                    <br />
                                                    <asp:LinkButton ID="lbviewsheet" OnClick="lbviewsheet_Click" Visible='<%# Eval("PPSM_Id").ToString() == "" ? false : true %>'
                                                        CssClass="label label-info" Text="View Sheet" CommandArgument='<%#Eval("PPSM_Id") %>' runat="server"></asp:LinkButton>
                                                    <%-- <br />
                                                    <asp:LinkButton ID="lbdelete" Visible='<%# Eval("PPSM_Id").ToString() == "" ? false : true %>'
                                                        CssClass="label label-danger" OnClientClick="return confirm('Are you sure you want to delete this event?');" Text="Delete" CommandArgument='<%#Eval("PPSM_Id") %>' runat="server"></asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>

                    </fieldset>

                    <h5 runat="server" style="padding-left: 10px;" visible="false" id="FTitle" class="box-title">Product Sheet</h5>


                </div>
            </div>


            <div class="modal" id="VarientModel_Product">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg" style="width: 80%;">
                        <div class="modal-content" style="height: 450px;">
                            <div class="modal-header">
                                <asp:LinkButton runat="server" class="close" ID="LinkButton2" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                                <h4 class="modal-title">Name:
                                 
								<asp:Label ID="lblProductName" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp;
                                Date :
                                <asp:Label ID="lbldate" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp; 
								 Section :
                                <asp:Label ID="lblsection" Font-Bold="true" runat="server"></asp:Label>
                                </h4>




                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row" style="height: 300px; overflow: scroll;">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <section class="content">
                                                        <div class="row">
                                                            <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                        </div>
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
                                                                        <asp:Label ID="lblProductNameInner" runat="server"></asp:Label></th>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 50%;">

                                                                        <asp:GridView ID="GVVariantDetail_In" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Balance-B/F" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBalance_BF" runat="server" Text='<%# Eval("ClosingBalance") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Prepared" HeaderStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtPrepared" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Return/Loss" HeaderStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtReturn" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="lblIntotal" Enabled="false" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </td>
                                                                    <th>&nbsp;</th>
                                                                    <td style="width: 50%;">
                                                                        <asp:GridView ID="GVVariantDetail_Out" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Sale" HeaderStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtSale" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Testing" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtTesting" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Issue for Kheer" HeaderStyle-Width="10%" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtIssueforkheer" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Discarded" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtDiscarded" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="CL.Closing" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCLClosing" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="lblouttotal" oncopy="return false" onpaste="return false" Enabled="false" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                            </Columns>
                                                                        </asp:GridView>

                                                                    </td>
                                                                </tr>
                                                            </table>


                                                        </div>
                                                    </section>
                                                    <!-- /.content -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnGetTotal" OnClick="btnGetTotal_Click" runat="server" CssClass="btn btn-primary" Text="Get Total" />
                                <asp:Button ID="btnPopupSave_Product" Enabled="false" OnClientClick="return ValidateT_Product()" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
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
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT_P" OnClick="btnYesT_P_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button3" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />





            <div class="modal" id="VarientModel_Product_Rpt">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 400px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton1" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                            <h4 class="modal-title">
                                <asp:Label ID="lblTopTitle" Width="55%" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 300px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="Label6" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div id="DivTable" runat="server">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </section>
                                                <!-- /.content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>


        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>


        function VarientModelF_Product_Rpt() {
            $("#VarientModel_Product_Rpt").modal('show');
        }

        function VarientModelF_Product() {
            $("#VarientModel_Product").modal('show');
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
