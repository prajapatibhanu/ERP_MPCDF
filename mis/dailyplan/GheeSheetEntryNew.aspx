<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GheeSheetEntryNew.aspx.cs" Inherits="mis_dailyplan_GheeSheetEntryNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-header">
                    <h3 class="box-title">Ghee Sheet Entry</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:dropdownlist id="ddlDS" runat="server" cssclass="form-control"></asp:dropdownlist>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" visible="false">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" display="Dynamic" controltovalidate="ddlShift" initialvalue="0" text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" errormessage="Select Shift" setfocusonerror="true" forecolor="Red" validationgroup="Submit"></asp:requiredfieldvalidator>
                                </span>
                                <div class="form-group">
                                    <asp:dropdownlist id="ddlShift" autopostback="true" cssclass="form-control" runat="server">
                                    </asp:dropdownlist>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:dropdownlist id="ddlPSection" autopostback="true" onselectedindexchanged="ddlPSection_SelectedIndexChanged" runat="server" cssclass="form-control"></asp:dropdownlist>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:requiredfieldvalidator id="rfvDate" runat="server" display="Dynamic" controltovalidate="txtDate" text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" errormessage="Enter Date" setfocusonerror="true" forecolor="Red" validationgroup="Submit"></asp:requiredfieldvalidator>
                                </span>
                                <asp:textbox id="txtDate" onkeypress="javascript: return false;" autopostback="true" ontextchanged="txtDate_TextChanged" width="100%" maxlength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                            </div>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Product Details</legend>



                        <div class="row">

                            <table class="table table-bordered">
                                <tr class="text-center">
                                    <th colspan="10">
                                        <asp:label id="lblProductNameInner" text="GHEE SHEET ACCOUNT" runat="server"></asp:label>
                                    </th>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>In Flow</legend>
                                            <asp:gridview id="GVVariantDetail_In" runat="server" autogeneratecolumns="false" cssclass="GVVariantDetail_In table table-striped table-bordered table-hover"
                                            emptydatatext="No Record Found." ClientIDMode="Static" ShowFooter="true" onrowcreated="GVVariantDetail_In_RowCreated">
                                             <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Packet Size" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblInFlowItemName" Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("ItemName") %>'></asp:TextBox>
                                                        <asp:Label ID="lblInFlowItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                   <asp:TextBox ID="txtInFlowItemPackagingSize" ClientIDMode="Static" Visible="false"   CssClass="form-control" autocomplete="off" Text='<%# Eval("ItemName") %>' runat="server" ></asp:TextBox>
                                                        <asp:Label ID="lblGheeIssueForPacking" CssClass="hidden" runat="server" Text='<%# Eval("GheeIssueForPacking") %>'></asp:Label>
                                                         </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOBNo" ClientIDMode="Static"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("OBNo") %>' onblur="OpeningBalanceQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOBQtyInKg" ClientIDMode="Static"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" runat="server" Text='<%# Eval("OBQtyInKg") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGheePackNo" ClientIDMode="Static"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"  Text='<%# Eval("GheePackNo") %>' onblur="GheePackQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGheePackQtyInKg" ClientIDMode="Static"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)"  Text='<%# Eval("GheePackQtyInKg") %>'  runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReturnFromFPNo"  ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"  Text='<%# Eval("ReturnFromFPNo") %>'  onblur="ReturnFromFPQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReturnFromFPQtyInKg"  ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)"  Text='<%# Eval("ReturnFromFPQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOtherNo"  ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"  Text='<%# Eval("OtherNo") %>' onblur="OtherInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOtherQtyInKg"  ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)"  Text='<%# Eval("OtherQtyInKg") %>' runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotalNo" Enabled="false"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)"  Text='<%# Eval("TotalInFlowNo") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotalQtyInKg" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)"  Text='<%# Eval("TotalInFlowQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                             
                                        </asp:gridview>
                                        </fieldset>
                                        

                                    </td>

                                </tr>

                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Out Flow</legend>
                                            <asp:gridview id="GVVariantDetail_Out" runat="server" autogeneratecolumns="false" cssclass="datatable table table-striped table-bordered table-hover"
                                            emptydatatext="No Record Found." ShowFooter="true" onrowcreated="GVVariantDetail_Out_RowCreated">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                           <Columns>

                                                <asp:TemplateField HeaderText="Packet Size" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblOutFlowItemName"   Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("ItemName") %>'></asp:TextBox>
                                                        <asp:Label ID="lblOutFlowItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfCasesSize" runat="server" Value='<%# Eval("CasesSize") %>'/>
                                                   <%--<asp:TextBox ID="txtOutFlowItemPackagingSize" ClientIDMode="Static" Visible="false"   CssClass="form-control" autocomplete="off"  Text='<%# Eval("CasesSize") %>' runat="server" ></asp:TextBox>--%>
                                                        <asp:Label ID="lblGheeIssuetoFP" CssClass="hidden" runat="server" Text='<%# Eval("GheeIssuetoFP") %>'></asp:Label>
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="No of Cases" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIsuuetoFPNoofCases" onblur="CalculateNoofPackets(this)" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Text='<%# Eval("IsuuetoFPNoofCases") %>' runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIsuuetoFPNo" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Text='<%# Eval("IsuuetoFPNo") %>' runat="server" onblur="IssuetoFPQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIsuuetoFPQtyInKg" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" Text='<%# Eval("IsuuetoFPQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIsuuetoOtherNo" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Text='<%# Eval("IsuuetoOtherNo") %>' runat="server" onblur="IssuetoOtherQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIsuuetoOtherQtyInKg" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" Text='<%# Eval("IsuuetoOtherQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLeakagePackingNo" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Text='<%# Eval("LeakagePackingNo") %>' runat="server" onblur="LeakagePackingQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLeakagePackingQtyInKg" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" Text='<%# Eval("LeakagePackingQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCBNo" oncopy="return false" ClientIDMode="Static" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("ClosingBalanceNo") %>' onblur="ClosingBalanceQtyInKg(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCBQtyInKg" ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" Text='<%# Eval("ClosingBalanceQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOutFlowTotalNo" Enabled="false"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" Text='<%# Eval("TotalOutFlowNo") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOutFlowTotalQtyInKg" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDecUnit(this,event)" Text='<%# Eval("TotalOutFlowQtyInKg") %>' runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               

                                            </Columns>
                                        </asp:gridview>
                                        </fieldset>
                                        

                                    </td>
                                </tr>
                            </table>

                        </div>
                        
                        

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="text-align: center;">
                                    <asp:button id="btnGetTotal" runat="server" OnClick="btnGetTotal_Click" cssclass="btn btn-primary" text="Get Total" />
                                    <asp:button id="btnPopupSave_Product" OnClick="btnPopupSave_Product_Click" enabled="false"   runat="server" validationgroup="a" cssclass="btn btn-primary" text="Save" />
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
                            <asp:label id="lblPopupT" runat="server"></asp:label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:button runat="server" cssclass="btn btn-success" text="Yes" OnClick="btnPopupSave_Product_Click" id="btnYesT_P"  style="margin-top: 20px; width: 50px;" />
                            <asp:button id="Button3" validationgroup="no" runat="server" cssclass="btn btn-danger" text="No" data-dismiss="modal" style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </section>
        <asp:HiddenField ID="hfPackagingValue" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function OpeningBalanceQtyInKg(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            var No = $(row).children("td").eq(1).find('input[type="text"]').val();
            
            if (No == "")
                No = 0;
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(2).find('input[type="text"]').val(QtyInKg);
            
        }
        function GheePackQtyInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(3).find('input[type="text"]').val();

            if (No == "")
                No = 0;
            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(4).find('input[type="text"]').val(QtyInKg);
           
        }
        function ReturnFromFPQtyInKg(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(5).find('input[type="text"]').val();

            if (No == "")
                No = 0;

            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(6).find('input[type="text"]').val(QtyInKg);
            
        }
        function OtherInKg(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(7).find('input[type="text"]').val();

            if (No == "")
                No = 0;

            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(8).find('input[type="text"]').val(QtyInKg);

        }
        function IssuetoFPQtyInKg(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(2).find('input[type="text"]').val();

            if (No == "")
                No = 0;

            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(3).find('input[type="text"]').val(QtyInKg);

        }
        function IssuetoOtherQtyInKg(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(4).find('input[type="text"]').val();

            if (No == "")
                No = 0;

            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(5).find('input[type="text"]').val(QtyInKg);

        }
        function LeakagePackingQtyInKg(currentrow) {
            debugger
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(6).find('input[type="text"]').val();

            if (No == "")
                No = 0;

            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(7).find('input[type="text"]').val(QtyInKg);

        }
        function ClosingBalanceQtyInKg(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var No = $(row).children("td").eq(8).find('input[type="text"]').val();

            if (No == "")
                No = 0;

            var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            if (Variant == "100 ML") {
                var QtyInKg = parseFloat((No / 10) * 0.905).toFixed(3);
            }
            if (Variant == "200 ML") {
                var QtyInKg = parseFloat((No / 5) * 0.905).toFixed(3);
            }
            if (Variant == "500 ML") {
                var QtyInKg = parseFloat((No / 2) * 0.905).toFixed(3);
            }
            if (Variant == "1 LTR") {
                var QtyInKg = parseFloat((No) * 0.905).toFixed(3);
            }
            if (Variant == "5 LTR") {
                var QtyInKg = parseFloat((No * 5) * 0.905).toFixed(3);
            }
            if (Variant == "15 KG") {
                var QtyInKg = parseFloat((No * 15)).toFixed(3);
            }
            $(row).children("td").eq(9).find('input[type="text"]').val(QtyInKg);

        }
        function CalculateNoofPackets(currentrow)
        {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var NoofCase = $(row).children("td").eq(1).find('input[type="text"]').val();
            var CasesSize = $(row).children("td").eq(0).find('input[type="hidden"]').val();
            if (NoofCase == "")
                NoofCase = 0;

            //var Variant = $(row).children("td").eq(0).find('input[type="text"]').val();
            ////if (Variant == "100 ML") {
            ////    var NoofPacket = parseFloat(NoofCase * 60).toFixed(2);
            ////}
            //if (Variant == "200 ML") {
            //    var NoofPacket = parseFloat(NoofCase * 60).toFixed(3);
            //}
            //if (Variant == "500 ML") {
            //    var NoofPacket = parseFloat(NoofCase * 32).toFixed(3);
            //}
            //if (Variant == "1 LTR") {
            //    var NoofPacket = parseFloat(NoofCase * 16).toFixed(3);
            //}
            //if (Variant == "5 LTR") {
            //    var NoofPacket = parseFloat(NoofCase * 1).toFixed(3);
            //}
            //if (Variant == "5 KG") {
            //    var NoofPacket = parseFloat(NoofCase * 1).toFixed(3);
            //}
            //if (Variant == "15 KG") {
            //    var NoofPacket = parseFloat(NoofCase * 1).toFixed(3);
            //}
            var NoofPacket = (parseFloat(NoofCase) * parseFloat(CasesSize)).toFixed(3);
            $(row).children("td").eq(2).find('input[type="text"]').val(NoofPacket);
        }
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
        function ValidateAmount()
        {
            debugger;
           
            var i = 0;
            var TQtyInKg = 0;

            var TPackagingValue =document.getElementById('<%= hfPackagingValue.ClientID%>').value;
            var rowcount = $('#GVVariantDetail_In tr').length;
            $('#GVVariantDetail_In tr').each(function (index)
            {
                
                if (i > 1 && i < rowcount - 1)
                {
                    var QtyInKg = $(this).children("td").eq(4).find('input[type="text"]').val();
                    if (QtyInKg == "")
                        QtyInKg = 0;
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2);
                    
                }
                i++;
            });
            if (parseFloat(TPackagingValue) == parseFloat(TQtyInKg))
            {
               
               // ValidateT_Product();
            }
            else {
                alert('Total Ghee Packing Qty Should be equal to ' + TPackagingValue + '')
                return false;
            }
            
           
        }
    </script>
</asp:Content>

