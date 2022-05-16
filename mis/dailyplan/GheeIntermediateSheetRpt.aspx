<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GheeIntermediateSheetRpt.aspx.cs" Inherits="mis_dailyplan_GheeIntermediateSheetRpt" %>


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
        @media print {
              
              .noprint {
                display: none;
            }
              
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
                    <h3 class="box-title">Ghee Intermediate Sheet</h3>
                </div>
                <div class="box-body">
                    <div class="row noprint">
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
                                <asp:Textbox ID="txtDate" onkeypress="javascript: return false;" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:Textbox>
                            </div>
                        </div>
                    </div>
                     <asp:Label ID="lblMsgRecord" Text="" runat="server"></asp:Label>
                    <asp:Panel id="panel1" visible="false" runat ="server">
                    <fieldset>
                        <legend>Product Details</legend>

                       

                        <div class="row">

                            <table class="table table-bordered">
                                <tr class="text-center">
                                    <th colspan="10">
                                        <asp:Label ID="lblProductNameInner" Text="GHEE INTERMEDIATE ACCOUNT" runat="server"></asp:Label></th>
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

                                                <asp:TemplateField HeaderText="Pckt" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPckt"   runat="server" Text='<%# Eval("Pckt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Loose" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLoose" runat="server"  Text='<%# Eval("Loose") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Butter Recv. From Butter sec." HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblButterRcvdfromButterSec"  runat="server" Text='<%# Eval("ButterRcvdfromButterSec") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ghee return from FP" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGheeRetrunFromFP"  runat="server" Text='<%# Eval("GheeRetrunFromFP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtInFlowOther" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("InFlowOther") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                            
                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIntotal" Enabled="false" runat="server"  Text='<%# Eval("InTotal") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Ghee Mfg" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheeMfg" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("GheeMfg") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ghee Issue to FP" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheeIssuetoFP" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("GheeIssuetoFP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Others" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOutFlowOther" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("OutFlowOther") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Closing Balance(Packet)" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPacketClosingBalance" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("PacketClosingBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Closing Balance(Loose)" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtLooseClosingBalance" oncopy="return false" onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("LooseClosingBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ghee Issue for Packing" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGheeIssueForPacking" oncopy="return false" Enabled="false"  onpaste="return false"  autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text='<%# Eval("GheeIssueForPacking") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblouttotal" oncopy="return false" onpaste="return false" style="width : 70px;" Enabled="false"  runat="server"  Text='<%# Eval("OutTotal") %>'></asp:Label>
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
                               <table class="table dataTable table-bordered" style="width:50%">
                                            <tr style="color: black">
                                                <td><b>Variation</b></td>
                                                
                                                <td><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="txtGainLossQtyInKg" Enabled="true" runat="server"></asp:Label></td>
                                               <td><b>Kgs. Fat<br />
                                                </b>
                                                    <asp:Label ID="txtGainLossFatInKg" Enabled="true"  runat="server"></asp:Label></td>
                                                <%--<td style="width:20%"><b>Kgs. SNF<br />
                                                </b>
                                                    <asp:Label ID="txtGainLossSnfInKg" Enabled="true"  runat="server"></asp:Label></td>--%>
                                            </tr>
                                       
                                            <tr style="color: black">
                                                <td><b>Production Loss</b></td>
                                                
                                               <%-- <td><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="Label1" Enabled="true"  runat="server"></asp:Label></td>--%>
                                               <td colspan="2"><b>Fat %<br />
                                                </b>
                                                    <asp:Label ID="Label3" Enabled="true" runat="server"></asp:Label></td>
                                                <%--<td style="width:20%"><b>Snf %<br />
                                                </b>
                                                    <asp:Label ID="Label4" Enabled="true"  runat="server"></asp:Label></td>--%>
                                            </tr>
                                        
                                            <tr style="color: black">
                                                <td style="width:30%"><b>Recovery</b></td>
                                                
                                                <%--<td style="width:20%"><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="Label5" Enabled="true"  runat="server"></asp:Label></td>--%>
                                               <td colspan="2"><b>Fat %<br />
                                                </b>
                                                    <asp:Label ID="Label6" Enabled="true"  runat="server"></asp:Label></td>
                                                <%--<td style="width:20%"><b>Snf %<br />
                                                </b>
                                                    <asp:Label ID="Label7" Enabled="true" runat="server"></asp:Label></td>--%>
                                            </tr>
                                        </table>
                            </div>
                        </div>
                        

                        

                    </fieldset>
                    </asp:Panel>
                </div>
            </div>

            

        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    

</asp:Content>


