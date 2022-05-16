<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MCU.aspx.cs" Inherits="mis_MilkCollection_ChillingCentre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Milk Collection Unit</h3>
                            <span id="ctl00_ContentBody_lblmsg"></span>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <span id="ctl00_ContentBody_rfv1" style="color: Red; display: none;"><i class='fa fa-exclamation-circle' title='Please Enter Date !'></i></span>
                                            <span id="ctl00_ContentBody_revdate" style="color: Red; display: none;"><i class='fa fa-exclamation-circle' title='Invalid Date !'></i></span>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input name="ctl00$ContentBody$txtTransactionDt" type="text" maxlength="10" id="txtTransactionDt" class="form-control" onkeypress="javascript: return false;" onpaste="return false;" placeholder="Select Date" autocomplete="off" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" style="width: 100%;" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Shift<span class="text-danger">*</span></label>
                                        <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                            <option value="0">Select</option>
                                            <option value="1">Morning</option>
                                            <option value="2">Evening</option>
                                            <option value="3">Special</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Route No.<span class="text-danger">*</span></label>
                                        <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                            <option value="0">Select</option>
                                            <option value="1">R1</option>
                                            <option value="2">R2</option>
                                            <option value="3">R3</option>
                                        </select>
                                        <small><span id="valddlstate" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Milk Details
                                </legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>DCS Name<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1" selected>DCS1</option>
                                                <option value="2">DCS2</option>
                                                <option value="3">DCS3</option>
                                                <option value="4">DCS4</option>
                                            </select>
                                            <small><span id="valddlstate" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>DCS Code<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtemail" type="text" value="84B" disabled maxlength="50" id="ctl00_ContentBody_txtemail" class="form-control" placeholder="DCS Code" />

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Milk Qty. (KG)<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtmobno" type="text" maxlength="10" id="ctl00_ContentBody_txtmobno" class="form-control" placeholder="Milk Qt (KG)" onkeypress="javascript:tbx_fnNumeric(event, this);" />
                                            <small><span id="valtxtmobno" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Fat %<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtemail" type="text" maxlength="50" id="ctl00_ContentBody_txtemail" class="form-control" placeholder="Fat %" />
                                            <small><span id="valtxtemail" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>SNF %<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtemail" type="text" maxlength="50" id="ctl00_ContentBody_txtemail" class="form-control" placeholder="SNF %" />
                                            <small><span id="valtxtemail" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Value(Rs.)<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtemail" type="text" maxlength="50" id="ctl00_ContentBody_txtemail" class="form-control" placeholder="Value(INR)" />
                                            <small><span id="valtxtemail" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <input type="submit" style="margin-top: 25px" name="ctl00$ContentBody$btnSave" value="Add" id="btnAdd" class="btn btn-success btn-block" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dugdh Sangh Name<span class="text-danger">*</span></label>
                                        <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                            <option value="0">Select</option>
                                            <option value="1">DS1</option>
                                            <option value="2">DS2</option>
                                        </select>
                                        <small><span id="valddlstate" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Truck No.<span class="text-danger">*</span></label>
                                        <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                            <option value="0">Select</option>
                                            <option value="1">MP 04 T 1848</option>
                                            <option value="2">MP 04 T 8941</option>
                                            <option value="3">MP 04 R 8419</option>
                                            <option value="4">MP 04 K 9999</option>
                                        </select>
                                        <small><span id="valddlstate" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <input type="submit" name="ctl00$ContentBody$btnSave" value="Save" id="btnSave" class="btn btn-primary btn-block" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="#" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box-header">
                                <h3 class="box-title">Detail of Milk Collection Unit </h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <div>
                                                <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse: collapse;">
                                                    <thead>
                                                        <tr>
                                                            <th scope="col">S.No.<br />
                                                            </th>
                                                            <th scope="col">Date
                                                                    <br />
                                                            </th>
                                                            <th scope="col">Shift<br />
                                                            </th>
                                                            <th scope="col">Route No.<br />
                                                            </th>
                                                            <th scope="col">DCS Name </th>
                                                            <th scope="col">Milk Qty. (KG)
                                                                    <br />
                                                            </th>
                                                            <th scope="col">Fat %
                                                                    <br />
                                                            </th>
                                                            <th scope="col">SNF %
                                                                    <br />
                                                            </th>
                                                            <th scope="col">Value(Rs.)
                                                                    <br />
                                                            </th>
                                                            <th scope="col">Dugdh Sangh Name<br />
                                                            </th>
                                                            <th scope="col">Truck No.
                                                                    <br />
                                                            </th>
                                                            <th scope="col">Action
                                                                    <br />
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>1
                                                            </td>
                                                            <td>28/01/2019
                                                            </td>
                                                            <td>Morning
                                                            </td>
                                                            <td>R1
                                                            </td>
                                                            <td>DCS1
                                                            </td>

                                                            <td>50
                                                            </td>
                                                            <td>12
                                                            </td>
                                                            <td>56
                                                            </td>
                                                            <td>45
                                                            </td>
                                                            <td>DS1
                                                            </td>
                                                            <td>MP 04 K 9999
                                                            </td>
                                                            <td>
                                                                <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                                &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2
                                                            </td>
                                                            <td>28/01/2019
                                                            </td>
                                                            <td>Morning
                                                            </td>
                                                            <td>R3
                                                            </td>
                                                            <td>DCS2
                                                            </td>
                                                            <td>40
                                                            </td>
                                                            <td>28
                                                            </td>
                                                            <td>26
                                                            </td>
                                                            <td>46
                                                            </td>
                                                            <td>DS1
                                                            </td>
                                                            <td>MP 04 R 8419
                                                            </td>
                                                            <td>
                                                                <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                                &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                            </td>
                                                        </tr>


                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--ConfirmationModal End --%>

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="ViewModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog" style="width: 60%; display: table-cell; vertical-align: middle;">
                        <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                            <div class="modal-header" style="background-color: #d9d9d9;">
                                <button type="button" class="close" data-dismiss="modal">
                                    <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel2">Milk Collection Detail's</h4>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label1" Text="Member ID [Name] : " runat="server"></asp:Label>
                                        <asp:Label ID="lblMemberId" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvPopup_ViewMilkCollectionDetails" ShowHeader="true" ShowFooter="true" FooterStyle-BackColor="#eaeaea" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkType_id" runat="server" Visible="false" Text='<%# Eval("MilkType_id") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FAT %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFooter" runat="server" Font-Bold="true" Text="Grand Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkQty" runat="server" Text='<%# Eval("MilkQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMilkQty" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnit_id" runat="server" Visible="false" Text='<%# Eval("Unit_id") %>'></asp:Label>
                                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate/Ltr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RatePerLtr", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value (In Rs.)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("TotalValue", "{0:0.00}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalValue" runat="server" Font-Bold="true" Text="0.00"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-default pull-right" data-dismiss="modal">CLOSE </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

