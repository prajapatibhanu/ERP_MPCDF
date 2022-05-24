<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductButterSheetEntry_New.aspx.cs" Inherits="mis_dailyplan_ProductButterSheetEntry" %>


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
                        <legend>BUTTER MFD. ENTRY In Flow</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <h9 style="color: red;">Note:- First Of All Please Click On Get Total After That Click On Save Button</h9>
                            </div>
                            <hr />
                        </div>
                        <fieldset>
                            <legend>Opening Balance</legend>
                            <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="Gv_Opening" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="White Butter Opening" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWhiteButterOpening" runat="server" Text='<%# Eval("WhiteButterOpening") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                        <asp:TemplateField HeaderText="Table Butter Opening" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTableButterOpening" runat="server" Text='<%# Eval("TableButterOpening") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cooking Butter Opening" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCookingButterOpening" runat="server" Text='<%# Eval("CookingButterOpening") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Total" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblintotal" oncopy="return false" onpaste="return false" Enabled="false" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>

                                </asp:GridView>
                                <hr />
                            </div>
							


                        </div>
                        </fieldset>
                        <fieldset>
                            <legend>Cream And Butter Received</legend>
                            <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="Gv_Cream_ButterReceived" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found.">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     
                                        <asp:TemplateField HeaderText="Cream Recvd For White Butter Good" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreamrcvdforwhitebuttergood" runat="server" Text='<%# Eval("IssueFor_WhiteButterGood") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cream Recvd For White Butter Sour" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreamrcvdforwhitebuttersur" runat="server" Text='<%# Eval("IssueFor_WhiteButterSour") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cream Recvd For Table Butter" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreamrcvdfortablebutter" runat="server" Text='<%# Eval("IssueFor_TableButter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cream Recvd For Cooking Butter" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreamrcvdforcookingbutter" runat="server" Text='<%# Eval("IssueFor_CookingButter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Butter Recvd From CC" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblbutterrcvdfromcc" CssClass="form-control" Text='<%# Eval("ButterRcvdFromCC") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Butter Recvd From Finished Products" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblbutterrcvdfromFinprod" CssClass="form-control" Text='<%# Eval("ButterRcvdFromFinishedProducts") %>' runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>

                                </asp:GridView>
                                <hr />
                            </div>
							


                        </div>
                        </fieldset>
                        
                        <div class="row">
                                        <div class="col-md-12">
                                            <table class="table dataTable table-bordered">
                                                <tr style="background-color: #ff874c; color: black">
                                                    <td><b>TOTAL IN FLOW</b></td>
                                                    <%--<td><b>Qty. In Ltr.</b><br />
                                                    <asp:TextBox ID="txtInFlowTQtyInLtr" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>--%>
                                                    <td><b>Qty. In Kgs.</b><br />
                                                        <asp:TextBox ID="txtInFlowTQtyInKg" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <td><b>Kgs. Fat<br />
                                                    </b>
                                                        <asp:TextBox ID="txtInFlowTFatInKg" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <td><b>Kgs. SNF<br />
                                                    </b>
                                                        <asp:TextBox ID="txtInFlowTSnfInKg" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                    </fieldset>

                    <fieldset>
                        <legend>Butter Disposal Entry(Out Flow)</legend>
                        <fieldset>
                            <legend>Butter Mfg</legend>
                            <div class="row">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GV_ButterMfg" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id_Out" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="White Butter Mfg" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtwhitebuttermfg" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("WhiteButterMfg") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Table Butter Mfg" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txttablebuttermfg" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("TableButterMfg") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cooking Butter Mfg" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtcookingbuttermfg" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CookingButterMfg") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>


                            </table>
                        </div>
                        </fieldset>
                        <fieldset>
                            <legend>Butter Issue To FP</legend>
                            <div class="row">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <asp:GridView ID="Gv_ButterIssueToFP" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id_Out" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="White Butter Issue To FP" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtWhiteButterIssuetoFP" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("WhiteButterIssuetoFP") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Table Butter Issue To FP" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTableButterIssuetoFP" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("TableButterIssuetoFP") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cooking Butter Issue To FP" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCookingButterIssuetoFP" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CookingButterIssuetoFP") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>


                            </table>
                        </div>
                        </fieldset>
                        <fieldset>
                            <legend>Butter Issue To Other Section</legend>
                            <div class="row">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <asp:GridView ID="Gv_IssuetoOther" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id_Out" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Issue To Processing..." HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueTo_Processing" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("ButterIssueToProcessing") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Butter Issue For Ghee" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_Ghee" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("ButterIssueForGhee") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Butter Issue For Other" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtButterIssueFor_other" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("ButterIssueForOther") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue For Store" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_Store" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("ButterIssueForStore") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue For Butter Milk" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueFor_SweetButterMilk" oncopy="return false" onpaste="return false" CssClass="form-control" Text='<%# Eval("ButterIssueForSweetButterMilk") %>' autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>


                            </table>
                        </div>
                        </fieldset>
                        <fieldset>
                            <legend>Butter Closing</legend>
                            <div class="row">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        <asp:GridView ID="Gv_Closing" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        <asp:Label ID="lblItem_id_Out" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="White Butter Closing" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtWhiteButterClosing" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("WhiteButterClosing") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Table Butter Closing" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTableButterClosing" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("TableButterClosing") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cooking Butter Closing" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCookingButterClosing" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" Text='<%# Eval("CookingButterClosing") %>'  onkeypress="return validateDecUnit(this,event)" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                                                              
                                            </Columns>
                                        </asp:GridView>

                                    </td>
                                </tr>


                            </table>
                        </div>
                        </fieldset>
                        <div class="row">
                                        <div class="col-md-12">
                                            <table class="table dataTable table-bordered">
                                                <tr style="background-color: #ff874c; color: black">
                                                    <td><b>TOTAL OUT FLOW</b></td>
                                                    <%--<td><b>Qty. In Ltr.</b><br />
                                                    <asp:TextBox ID="txtOutFlowTQtyInLtr" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>--%>
                                                    <td><b>Qty. In Kgs.</b><br />
                                                        <asp:TextBox ID="txtOutFlowTQtyInKg" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <td><b>Kgs. Fat<br />
                                                    </b>
                                                        <asp:TextBox ID="txtOutFlowTFatInKg" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                    <td><b>Kgs. SNF<br />
                                                    </b>
                                                        <asp:TextBox ID="txtOutFlowTSnfInKg" Enabled="true" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                        <div class="row">

                            <div class="col-md-12">
                                <asp:Label ID="lblVariation" runat="server" ForeColor="Red" Text=""></asp:Label>
                                <table class="table dataTable table-bordered">
                                    <tr style="background-color: #ff874c; color: black">
                                        <td><b>Variation</b></td>

                                        <td><b>Qty. In Kgs.</b><br />
                                            <asp:Label ID="txtVariationQtyInKg" Enabled="true" CssClass="form-control" runat="server"></asp:Label></td>
                                        <td><b>Kgs. Fat<br />
                                        </b>
                                            <asp:Label ID="txtVariationFatInKg" Enabled="true" CssClass="form-control" runat="server"></asp:Label>
                                            <%-- <asp:Label ID="lblFATMsg" runat="server" ForeColor="Red"></asp:Label>--%>
                                        </td>
                                        <td><b>FAT %</b><br />
                                            <asp:Label ID="txtFATPer" Enabled="false" CssClass="form-control" runat="server"></asp:Label></td>
                                        <td><b>Kgs. SNF<br />
                                        </b>
                                            <asp:Label ID="txtVariationSnfInKg" Enabled="true" CssClass="form-control" runat="server"></asp:Label></td>
                                        <td><b>SNF %</b><br />
                                            <asp:Label ID="txtSNFPer" Enabled="false" CssClass="form-control" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                                <span id="spn" style="color : red;" runat="server" visible="false"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txtReceived_From" TextMode="MultiLine" placeholder="Received From Remark.........." oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
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
