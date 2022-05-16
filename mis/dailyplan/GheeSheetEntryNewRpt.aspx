<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GheeSheetEntryNewRpt.aspx.cs" Inherits="mis_dailyplan_GheeSheetEntryNewRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
          .NonPrintable {
                  display: none;
              }
        @media print
        {
            .noprint
            {
                display:none;
            }
             .NonPrintable {
                  display: block;
              }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content noprint">
            <!-- Default box -->
            <div class="box box-success">
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-header">
                    <h3 class="box-title">Ghee Sheet Entry Report</h3>
                </div>
                <div class="box-body">
                    <div class="row  noprint">
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

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFDate" runat="server" Display="Dynamic" ControlToValidate="txtFDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtFDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtTDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:Button ID="btnSearch" style="margin-top:21px;" runat="server" CssClass="btn btn-success" Text="Search" ValidationGroup="Submit" OnClick="btnSearch_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                     </div>
                </div>
                            <fieldset>
                        <legend>Product Details</legend>



                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">

                                      <asp:Button ID="btnExport" runat="server" Text="Export" Visible="false" CssClass="btn btn-primary noprint" OnClick="btnExport_Click"/>
                                      <asp:Button ID="btnprint" Visible="false" Text="Print" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                </div>
                                 <div id="tblGheeIntermediate" runat="server">

                         </div>
                            </div>
                            </div>
                           </fieldset>
                            <%--<table class="table table-bordered">
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
                                            <asp:gridview id="GVVariantDetail_In" runat="server" autogeneratecolumns="false" cssclass="datatable table table-striped table-bordered table-hover"
                                            emptydatatext="No Record Found." ClientIDMode="Static" ShowFooter="true" onrowcreated="GVVariantDetail_In_RowCreated">
                                             <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Packet Size" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblInFlowItemName" Enabled="false" CssClass="form-control" runat="server" Text='<%# Eval("ItemName") %>'></asp:TextBox>
                                                        
                                                         </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOBNo" ClientIDMode="Static"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("OBNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOBQtyInKg" ClientIDMode="Static"  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("OBQtyInKg") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheePackNo" ClientIDMode="Static" Text='<%# Eval("GheePackNo") %>'  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="GheePackQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheePackQtyInKg" ClientIDMode="Static" Text='<%# Eval("GheePackQtyInKg") %>'  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtReturnFromFPNo"  ClientIDMode="Static" Text='<%# Eval("ReturnFromFPNo") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"  onblur="ReturnFromFPQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtReturnFromFPQtyInKg"  ClientIDMode="Static" Text='<%# Eval("ReturnFromFPQtyInKg") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOtherNo"  ClientIDMode="Static" Text='<%# Eval("OtherNo") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="OtherInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOtherQtyInKg"  ClientIDMode="Static" Text='<%# Eval("OtherQtyInKg") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTotalNo" Enabled="false" Text='<%# Eval("TotalInFlowNo") %>'  oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTotalQtyInKg" Enabled="false" Text='<%# Eval("TotalInFlowQtyInKg") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
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
                                                        <asp:TextBox ID="lblOutFlowItemName" Enabled="false"  CssClass="form-control" runat="server" Text='<%# Eval("ItemName") %>'></asp:TextBox>
                                                        
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="No of Cases" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoFPNoofCases"  CssClass="form-control" autocomplete="off" Text='<%# Eval("IsuuetoFPNoofCases") %>'  onkeypress="return validateDec(this,event)" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoFPNo" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("IsuuetoFPNo") %>' onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="IssuetoFPQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoFPQtyInKg" ClientIDMode="Static" Text='<%# Eval("IsuuetoFPQtyInKg") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoOtherNo" ClientIDMode="Static" Text='<%# Eval("IsuuetoOtherNo") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="IssuetoOtherQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoOtherQtyInKg" Text='<%# Eval("IsuuetoOtherQtyInKg") %>' ClientIDMode="Static" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCBNo" oncopy="return false" Text='<%# Eval("ClosingBalanceNo") %>' ClientIDMode="Static" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="ClosingBalanceQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCBQtyInKg" ClientIDMode="Static" Text='<%# Eval("ClosingBalanceQtyInKg") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutFlowTotalNo" Enabled="false" Text='<%# Eval("TotalOutFlowNo") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutFlowTotalQtyInKg" Enabled="false" Text='<%# Eval("TotalOutFlowQtyInKg") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               

                                            </Columns>
                                        </asp:gridview>
                                        </fieldset>
                                        

                                   </td>
                                </tr>
                          </table>--%>

                      <%--  </div>
                        
                        

                       

                    </fieldset>

                </div>
            </div>

          
        </section>--%>
       <%-- <section class="content1 NonPrintable">
            <div id="header" runat="server"></div>
            <div id="divprint" runat="server">
            <asp:gridview id="gvinflow_print" runat="server" autogeneratecolumns="false" cssclass="datatable table table-striped table-bordered table-hover"
                                            emptydatatext="No Record Found." ClientIDMode="Static" ShowFooter="true" OnRowCreated="gvinflow_print_RowCreated">
                                             <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Packet Size" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInFlowItemName" Enabled="false" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        
                                                         </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOBNo" ClientIDMode="Static"  oncopy="return false" onpaste="return false" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("OBNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOBQtyInKg" ClientIDMode="Static"  oncopy="return false" onpaste="return false" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("OBQtyInKg") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheePackNo" ClientIDMode="Static" Text='<%# Eval("GheePackNo") %>'  oncopy="return false" onpaste="return false" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="GheePackQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheePackQtyInKg" ClientIDMode="Static" Text='<%# Eval("GheePackQtyInKg") %>'  oncopy="return false" onpaste="return false" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtReturnFromFPNo"  ClientIDMode="Static" Text='<%# Eval("ReturnFromFPNo") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"  onblur="ReturnFromFPQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtReturnFromFPQtyInKg"  ClientIDMode="Static" Text='<%# Eval("ReturnFromFPQtyInKg") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOtherNo"  ClientIDMode="Static" Text='<%# Eval("OtherNo") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="OtherInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOtherQtyInKg"  ClientIDMode="Static" Text='<%# Eval("OtherQtyInKg") %>' oncopy="return false" onpaste="return false" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTotalNo" Enabled="false" Text='<%# Eval("TotalInFlowNo") %>'  oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTotalQtyInKg" Enabled="false" Text='<%# Eval("TotalInFlowQtyInKg") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                             
                                        </asp:gridview>
            <asp:gridview id="gvoutflowprint" runat="server" autogeneratecolumns="false" cssclass="datatable table table-striped table-bordered table-hover"
                                            emptydatatext="No Record Found." ShowFooter="true" OnRowCreated="gvoutflowprint_RowCreated">
                                            <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                           <Columns>

                                                <asp:TemplateField HeaderText="Packet Size" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:label ID="lblOutFlowItemName" Enabled="false"   runat="server" Text='<%# Eval("ItemName") %>'></asp:label>
                                                        
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="No of Cases" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoFPNoofCases"   autocomplete="off" Text='<%# Eval("IsuuetoFPNoofCases") %>'  onkeypress="return validateDec(this,event)" runat="server" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoFPNo" ClientIDMode="Static" oncopy="return false" Text='<%# Eval("IsuuetoFPNo") %>' onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="IssuetoFPQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoFPQtyInKg" ClientIDMode="Static" Text='<%# Eval("IsuuetoFPQtyInKg") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoOtherNo" ClientIDMode="Static" Text='<%# Eval("IsuuetoOtherNo") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="IssuetoOtherQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIsuuetoOtherQtyInKg" Text='<%# Eval("IsuuetoOtherQtyInKg") %>' ClientIDMode="Static" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCBNo" oncopy="return false" Text='<%# Eval("ClosingBalanceNo") %>' ClientIDMode="Static" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" onblur="ClosingBalanceQtyInKg(this)"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCBQtyInKg" ClientIDMode="Static" Text='<%# Eval("ClosingBalanceQtyInKg") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="No's" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutFlowTotalNo" Enabled="false" Text='<%# Eval("TotalOutFlowNo") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty in Kg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutFlowTotalQtyInKg" Enabled="false" Text='<%# Eval("TotalOutFlowQtyInKg") %>' oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               

                                            </Columns>
                                        </asp:gridview>
                </div>--%>
        </section>
        <section class="content">
            <div id="divprint" class="NonPrintable" runat="server"></div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    
</asp:Content>

