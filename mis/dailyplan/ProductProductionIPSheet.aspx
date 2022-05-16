<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductProductionIPSheet.aspx.cs" Inherits="mis_dailyplan_ProductProductionIPSheet" %>

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
                    <h3 class="box-title">Final Product Manufactured & Balance Details</h3>
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
                            <div class="col-md-6">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvmttos" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Variant Name" HeaderStyle-Width="10%">
                                                 <ItemTemplate>
                                                       <asp:LinkButton  ID="lnkbtnVN" CssClass="btn btn-block btn-secondary" Enabled='<%# Eval("PPIPSM").ToString() == "" ? true : false %>' OnClick="lnkbtnVN_Click" Text='<%#Eval("ItemTypeName") %>' CommandArgument='<%#Eval("ItemType_id") %>' runat="server"></asp:LinkButton>
                                                    <asp:Label ID="Label1" CssClass="hidden" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" CssClass="hidden" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                    <asp:Label ID="lblFat" CssClass="hidden" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                    <asp:Label ID="lblSnf" CssClass="hidden" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                      <asp:Label ID="Label2" CssClass="hidden" runat="server" Text='<%# Eval("WMFat") %>'></asp:Label>
                                                      <asp:Label ID="lblWMFat" CssClass="hidden" runat="server" Text='<%# Eval("WMFat") %>'></asp:Label>
                                                    <asp:Label ID="lblWMSnf" CssClass="hidden" runat="server" Text='<%# Eval("WMSnf") %>'></asp:Label>
                                                    <asp:Label ID="lblItemRatioPer" CssClass="hidden" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                     <asp:Label ID="lblCalculationMethod" CssClass="hidden" runat="server" Text='<%# Eval("CalculationMethod") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="status" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                   
                                                    <asp:Label ID="lblPPSM_StatusMSG" runat="server" Text='<%# Eval("PPSM_StatusMSG") %>'></asp:Label>
                                                    
                                                    <asp:Label ID="lblPPIPSM" Visible="false" runat="server" Text='<%# Eval("PPIPSM") %>'></asp:Label>
                                                    <br />
                                                    <asp:LinkButton ID="lbviewsheet" OnClick="lbviewsheet_Click" Visible='<%# Eval("PPIPSM").ToString() == "" ? false : true %>'
                                                        CssClass="label label-info" Text="View Sheet" CommandArgument='<%#Eval("PPIPSM") %>' runat="server"></asp:LinkButton>
                                                    <%--Enabled='<%# Eval("PPSM_S").ToString() == "" ? true : false %>'--%>
                                                  
                                                     </ItemTemplate>
                                                
                                            </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Packing status" HeaderStyle-Width="10%">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblPPIPPackMat_StatusMSG" runat="server" Text='<%# Eval("PPIPPackMat_StatusMSG") %>'></asp:Label>

                                                    <%-- <asp:Label ID="lblPPIPPackMat_ID" Visible="false" runat="server" Text='<%# Eval("PPIPPackMat_ID") %>'></asp:Label>
                                                    <br />--%>
                                                    <asp:LinkButton ID="lbviewsheet1" OnClick="lbviewsheet1_Click" Visible='<%# Eval("PPIPSM").ToString() == "" ? false : true %>'
                                                        CssClass="label label-info" Text="Packing Material Account" CommandArgument='<%#Eval("PPIPSM") %>' runat="server"></asp:LinkButton>
                                                    <%--Enabled='<%# Eval("PPSM_S").ToString() == "" ? true : false %>'--%>
                                                </ItemTemplate>

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

                                                           
                                                                        <asp:Label ID="lblProductNameInner" runat="server"></asp:Label>
                                                               

                                                                        <asp:GridView ID="GVVariantDetail_In" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                            EmptyDataText="No Record Found.">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                         <asp:Label ID="lblUQCCode" CssClass="hidden" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
                                                                                         <asp:Label ID="lblPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>
                                                                                        <asp:Label ID="lblItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtOpeningBalance" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Closing") %>' onblur="CalcClosingBal(this)"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Return" HeaderStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtReturn" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("Return") %>' onblur="CalcClosingBal(this)" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Manufactured" HeaderStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtManufactured" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("Manufactured") %>' onblur="CalcClosingBal(this)" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
																				<asp:TemplateField HeaderText="Purchased From" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtPurchasedFrom" ClientIDMode="Static"  runat="server" CssClass="form-control" onblur="CalcClosingBal(this)" Text='<%# Eval("PurchasedFrom") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sample" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtSample" ClientIDMode="Static"  runat="server" CssClass="form-control" onblur="CalcClosingBal(this)" Text='<%# Eval("Sample") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Dispatch" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtDispatch" ClientIDMode="Static"  runat="server" CssClass="form-control" onblur="CalcClosingBal(this)" Text='<%# Eval("Dispatch") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="10%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtClosingBalance" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("ClosingBalance") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>

                                                                 


                                                        </div>
														<div class="row">
                                                            <div class="col-md-3">
                                                               
                                                                    <label>Select DS(for Purchased from)</label>
                                                                    <asp:DropDownList ID="ddlDSPurchasedfrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                                               
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
                            <div class="modal-footer">
                                
                                <asp:Button ID="btnPopupSave_Product"  OnClientClick="return ValidateT_Product()" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
			<div class="modal" id="VarientModel_PackingAccount">
                <%--<div style="display: table; height: 100%; width: 100%;">--%>
                <div class="modal-dialog modal-lg" style="width: 100%;">
                    <div class="modal-content" style="height: 450px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton3" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">Name:
                                 
								<asp:Label ID="Label4" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp;
                                Date :
                                <asp:Label ID="Label7" Font-Bold="true" runat="server"></asp:Label>
                                &nbsp; 
								 Section :
                                <asp:Label ID="Label8" Font-Bold="true" runat="server"></asp:Label>
                            </h4>




                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 300px;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <div class="row">
                                                        <asp:Label ID="Label10" runat="server"></asp:Label>
                                                    </div>

                                                    <div class="row">


                                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                                        <asp:GridView ID="gvPackingMatAccount" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                            EmptyDataText="No Record Found." OnRowCreated="gvPackingMatAccount_RowCreated">
                                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="center" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                        <asp:Label ID="lblUQCCode" CssClass="hidden" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
                                                                        <asp:Label ID="lblPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>
                                                                        <asp:Label ID="lblItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="CUP">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOB_Cup_Cone_Duplex" Style="width: 60px;" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("OB_Cup_Cone_Duplex") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Lead" HeaderStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOB_Lead" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("OB_Lead") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Stick" HeaderStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOB_Stick" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("OB_Stick") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C Box" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOB_CBox" Style="width: 60px;" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("OB_CBox") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CUP" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRFS_Cup_Cone_Duplex" Style="width: 60px;" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("RFS_Cup_Cone_Duplex") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Lead" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRFS_Lead" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("RFS_Lead") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Stick" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRFS_Stick" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("RFS_Stick") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C Box" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRFS_CBox" Style="width: 60px;" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("RFS_CBox") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CUP" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUFP_Cup_Cone_Duplex" Style="width: 60px;" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("UFP_Cup_Cone_Duplex") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Lead" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUFP_Lead" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("UFP_Lead") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Stick" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUFP_Stick" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("UFP_Stick") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C Box" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUFP_CBox" Style="width: 60px;" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("UFP_CBox") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CUP" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtDM_Cup_Cone_Duplex" Style="width: 60px;" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("DM_Cup_Cone_Duplex") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Lead" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtDM_Lead" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("DM_Lead") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Stick" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtDM_Stick" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("DM_Stick") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C Box" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtDM_CBox" Style="width: 60px;" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("DM_CBox") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CUP" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtReturn_Cup_Cone_Duplex" Style="width: 60px;" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Return_Cup_Cone_Duplex") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Lead" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtReturn_Lead" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("Return_Lead") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Stick" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtReturn_Stick" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("Return_Stick") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C Box" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtReturn_CBox" Style="width: 60px;" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("Return_CBox") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CUP" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCB_Cup_Cone_Duplex" Style="width: 60px;" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("CB_Cup_Cone_Duplex") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Lead" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCB_Lead" ClientIDMode="Static" oncopy="return false" onpaste="return false" Text='<%# Eval("CB_Lead") %>' CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Stick" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCB_Stick" Style="width: 60px;" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("CB_Stick") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="C Box" HeaderStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCB_CBox" ClientIDMode="Static" runat="server" CssClass="form-control" Text='<%# Eval("CB_CBox") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>






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

                            <asp:Button ID="btnPackingMatAccount" OnClientClick="return ValidatePackingMatAcc()" runat="server" OnClick="btnPackingMatAccount_Click" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" />
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
                <%--</div>--%>
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

			<div class="modal fade" id="myModalT_P1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT_P1">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT1" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="Button1" OnClick="btnPackingMatAccount_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>



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
		function VarientModel_PackingAccount() {
            $("#VarientModel_PackingAccount").modal('show');
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
		 function ValidatePackingMatAcc() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnPackingMatAccount.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT1.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT_P1').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnPackingMatAccount.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT1.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT_P1').modal('show');
                    return false;
                }
            }
        }
        function CalcClosingBal(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var OpeningBal = $(row).children("td").eq(1).find('input[type="text"]').val();
            var Return = $(row).children("td").eq(2).find('input[type="text"]').val();
            var Manufactured = $(row).children("td").eq(3).find('input[type="text"]').val();
			var PurchasedFrom = $(row).children("td").eq(4).find('input[type="text"]').val();
            var Sample = $(row).children("td").eq(5).find('input[type="text"]').val();
            var Dispatch = $(row).children("td").eq(6).find('input[type="text"]').val();
            if (OpeningBal == "")
                OpeningBal = 0;
            if (Return == "")
                Return = 0;
            if (Manufactured == "")
                Manufactured = 0;
            if (Sample == "")
                Sample = 0;
            if (Dispatch == "")
                Dispatch = 0;
            var ClosingBalnce = (parseFloat(OpeningBal) + parseFloat(Return) + parseFloat(Manufactured) + parseFloat(PurchasedFrom) - parseFloat(Sample) - parseFloat(Dispatch)).toFixed(2);
            $(row).children("td").eq(7).find('input[type="text"]').val(ClosingBalnce);
            
        }

    </script>


</asp:Content>
