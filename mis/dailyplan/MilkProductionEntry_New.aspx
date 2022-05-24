<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkProductionEntry_New.aspx.cs" Inherits="mis_dailyplan_MilkProductionEntry_New" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
<style>
        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Daily Disposal Sheet</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server" OnTextChanged="ddlShift_TextChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="dvdetails" runat="server">
                        <div class="row">

                            <div class="col-md-12">
                                <fieldset>
                                    <legend>In Flow</legend>
                                    <div class="row">
                                        <div class="col-md-12 pull-left">
                                            <div class="form-group">
                                                <asp:Button ID="btnViewDemand" CssClass="btn btn-success" runat="server" Text="View Demand" OnClick="btnViewDemand_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvOpening" runat="server" ShowFooter="true" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvOpening_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tank/Silo" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOpeningV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                <asp:Label ID="lblOpeningI_MCID" CssClass="hidden" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOpeningQuantityInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOpeningQuantityInLtr" onkeypress="return validateDec(this,event);" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOpeningFat_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="FatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOpeningSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOpeningFATInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOpeningSNFInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnOpening" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnOpening_Click" />
                                                <asp:Button ID="btnOpeningGetTotal" runat="server" CssClass="btn btn-success" Text="GetTotal" OnClick="btnOpeningGetTotal_Click" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvProcess" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvProcess_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Variant">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                <asp:Label ID="lblProcessItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                <asp:TextBox ID="lblProcessPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hfprocess" runat="server" Value='<%# Eval("LtrtoKgFactor") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Of Packets.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessnofpackets" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("NoOfPackets") %>' onblur="CRBPcktltr(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Packet Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessPackedInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("PackedQtyInLtr") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Packet Qty. In Kgs">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessPackedInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("PackedQtyInKg") %>' onblur="CRBLtrtoKg(this),CRBFatInKg(this),CRBSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessFat_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="CRBFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="CRBSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessFATInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProcessSNFInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnProcess" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnProcess_Click" />
                                                <asp:Button ID="btnProcessGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnProcessGetTotal_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvReturn" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvReturn_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Variant">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReturnItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                <asp:Label ID="lblReturnItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                <asp:TextBox ID="lblReturnPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hfreturn" runat="server" Value='<%# Eval("LtrtoKgFactor") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Packed Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnPackedInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" onblur="ReturnKgtoPcktltr(this),ReturnFatInKg(this),ReturnSnfInKg(this)" Text='<%# Eval("PackedQtyInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Packed Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnPackedInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("PackedQtyInLtr") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Of Packets.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnnofpackets" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("NoOfPackets") %>' onblur="Returnpktltrkg(this),ReturnFatInKg(this),ReturnSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="ReturnFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="ReturnSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnReturn_Click" />
                                                <asp:Button ID="btnReturnGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnReturnGetTotal_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvCCWiseProcurement" ClientIDMode="Static" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvCCWiseProcurement_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="MDP/CC" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblOffice_ID" Text='<%# Eval("Office_ID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCCTankerNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblCCI_TankerID" Text='<%# Eval("I_TankerID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCChallanNo" runat="server" CssClass="form-control" Text='<%# Eval("ChallanNo") %>'></asp:TextBox>
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
														<asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCCMilkQuality" runat="server" CssClass="form-control select2">
                                                                   <%-- <asp:ListItem Value="Select">Select</asp:ListItem>--%>
                                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblCCMilkQuality" Text='<%# Eval("MilkQuality") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCQuantityInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="CCProcQtyInLtr(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCQuantityInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="CCProcQtyInLtr(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCFat_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' onblur="CCProcFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' onblur="CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCFATInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCSnfInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnRemove" runat="server" Text="-" OnClick="btnRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="ButtonAdd" runat="server" Text="+" OnClick="ButtonAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnCCWiseProcurement" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnCCWiseProcurement_Click" />
                                                <asp:Button ID="btnCCWiseProcurementGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnCCWiseProcurementGetTotal_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvCCWiseGoatMilkProcurement" ClientIDMode="Static" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvCCWiseGoatMilkProcurement_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="MDP/CC" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCCGoatMilk" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblCCGoatMilkOffice_ID" Text='<%# Eval("Office_ID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCCGoatMilkTankerNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblCCGoatMilkI_TankerID" Text='<%# Eval("I_TankerID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkChallanNo" runat="server" CssClass="form-control" Text='<%# Eval("ChallanNo") %>'></asp:TextBox>
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
														<asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCCGoatMilkQuality" runat="server" CssClass="form-control select2">
                                                                   <%-- <asp:ListItem Value="Select">Select</asp:ListItem>--%>
                                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblCCGoatMilkMilkQuality" Text='<%# Eval("MilkQuality") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkQuantityInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="CCProcQtyInLtr(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkQuantityInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="CCProcQtyInLtr(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkFat_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' onblur="CCProcFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' onblur="CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkFATInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCCGoatMilkSnfInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnCCGoatMilkRemove" runat="server" Text="-"  CssClass="btn btn-warning" OnClick="btnCCGoatMilkRemove_Click"/>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnCCGoatMilkAdd" runat="server" Text="+"  CssClass="btn btn-success" OnClick="btnCCGoatMilkAdd_Click"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnCCWiseGoatMilkProcurement" runat="server" CssClass="btn btn-success" Text="Save"  OnClick="btnCCWiseGoatMilkProcurement_Click"/>
                                                <asp:Button ID="btnCCWiseGoatMilkProcurementGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnCCWiseGoatMilkProcurementGetTotal_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvrcvdfrmothrUnion" ClientIDMode="Static" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvrcvdfrmothrUnion_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Union" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlrcvdfrmothrUnion" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblOffice_ID" Text='<%# Eval("Office_ID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MilkType" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlrcvdfrmothrUnionMilkType" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblMilkTypeID" Text='<%# Eval("MilkTypeID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrcvdfrmothrUnionQuantityInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="MIPQtyInLtr(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrcvdfrmothrUnionQuantityInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="MIPQtyInKg(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrcvdfrmothrUnionFat_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' onblur="MIPFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrcvdfrmothrUnionSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' onblur="MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrcvdfrmothrUnionFATInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtrcvdfrmothrUnionSnfInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnrcvdfrmothrUnionRemove" runat="server" Text="-" OnClick="btnrcvdfrmothrUnionRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnrcvdfrmothrUnionAdd" runat="server" Text="+" OnClick="btnrcvdfrmothrUnionAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnrcvdfrmothrUnion" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnrcvdfrmothrUnion_Click" />
                                                <asp:Button ID="btnrcvdfrmothrUnionGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnrcvdfrmothrUnionGetTotal_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvForPowderConversion" ClientIDMode="Static" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvForPowderConversion_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Union" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlForPowderConversion" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblOffice_ID" Text='<%# Eval("Office_ID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MilkType" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlForPowderConversionMilkType" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblForPowderConversionMilkTypeID" Text='<%# Eval("MilkTypeID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtForPowderConversionQuantityInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="MIPQtyInLtr(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtForPowderConversionQuantityInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="MIPQtyInKg(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtForPowderConversionFat_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' onblur="MIPFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtForPowderConversionSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' onblur="MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtForPowderConversionFATInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtForPowderConversionSnfInKg" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnForPowderConversionRemove" runat="server" Text="-" OnClick="btnForPowderConversionRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnForPowderConversion" runat="server" Text="+" OnClick="btnForPowderConversion_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnSaveForPowderConversion" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSaveForPowderConversion_Click" />
                                                <asp:Button ID="btnForPowderConversionGetTotal" runat="server" CssClass="btn btn-success" Text="GetTotal" OnClick="btnForPowderConversionGetTotal_Click" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBMCDCSCollection" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvBMCDCSCollection_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tanker" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlTanker" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblI_TankerID" Text='<%# Eval("I_TankerID") %>' runat="server" CssClass="hidden"></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Route" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBMCRootName" runat="server" CssClass="form-control  capitalize ui-autocomplete-12" ClientIDMode="Static"  Text='<%# Eval("BMCTankerRootName") %>'></asp:TextBox>
                                                               <asp:HiddenField ID="hfBMCRootName" runat="server" ClientIDMode="Static" />
															   <%--<asp:DropDownList ID="ddlBMCTankerRoot" runat="server" CssClass="form-control select2"></asp:DropDownList>--%>
                                                                <%-- <asp:Label ID="lblBMCTankerRoot_Id"  Text='<%# Eval("BMCTankerRoot_Id") %>'  runat="server" CssClass="hidden"></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlBMCDCSCollectionMilkQuality" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblBMCDCSCollectionMilkQuality" Text='<%# Eval("MilkQuality") %>' runat="server" CssClass="hidden"></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBMCDCSCollectionQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="BMCCollFatInKg(this),BMCCollSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBMCDCSCollectionFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' onblur="BMCCollFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBMCDCSCollectionSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' onblur="BMCCollSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBMCDCSCollectionFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBMCDCSCollectionSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnBMCDCSCollectionRemove" runat="server" Text="-" OnClick="btnBMCDCSCollectionRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnBMCDCSCollectionAdd" runat="server" Text="+" OnClick="btnBMCDCSCollectionAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnBMCDCSCollection" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnBMCDCSCollection_Click" />
                                                <asp:Button ID="btnBMCDCSCollectionGetTotal" runat="server" CssClass="btn btn-success" Text="GetTotal" OnClick="btnBMCDCSCollectionGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvCanesCollection" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvCanesCollection_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Variant">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblCanesCollectionVariant" Text='<%# Eval("Variant") %>' runat="server"></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCanesCollectionMilkQuality" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>
                                                              
                                                                 <asp:Label ID="lblCanesCollectionMilkQuality" CssClass="hidden" Text='<%# Eval("MilkQuality") %>' runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCanesCollectionQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="CanesCollectionFatInKg,CanesCollectionSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCanesCollectionFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="CanesCollectionFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCanesCollectionSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="CanesCollectionSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                 <%--<asp:TextBox ID="txtCanesCollectionFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>' onblur="CanesCollectionFat(this)"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtCanesCollectionFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>' onblur="CanesCollectionFat(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                               <%--<asp:TextBox ID="txtCanesCollectionSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>' onblur="CanesCollectionSnf(this)"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtCanesCollectionSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>' onblur="CanesCollectionSnf(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnCansRemove" runat="server" Text="-" OnClick="btnCansRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnCansAdd" runat="server" Text="+" OnClick="btnCansAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnCanesCollection" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnCanesCollection_Click" />
                                                <asp:Button ID="btnCanesCollectionGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnCanesCollectionGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvinflowother" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvinflowother_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtinflowotherQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="InFlowFatInKg,InFlowSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtinflowotherQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtinflowotherFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="InFlowFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtinflowotherSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="InFlowSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                               <%--<asp:TextBox ID="txtinflowotherFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>' onblur="InflowothersFat(this)"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtinflowotherFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>' onblur="InflowothersFat(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtinflowotherSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>' onblur="CanesCollectionSnf(this)"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtinflowotherSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>' onblur="InflowothersSnf(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnInFlowOther" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnInFlowOther_Click" />
                                                <asp:Button ID="btnInFlowOtherGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnInFlowOtherGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
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
                            </div>


                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Out Flow</legend>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvPaticulars" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" ClientIDMode="Static" AutoGenerateColumns="false" OnRowCreated="gvPaticulars_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Variant" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartcularItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                <asp:Label ID="lblPartcularItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                <asp:TextBox ID="lblReturnPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:TextBox>
                                                            <asp:HiddenField ID="hfReturn" runat="server" Value='<%# Eval("LtrtoKgFactor") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Quantity <br/>(In Kg)" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPaticularsQuantityInKg" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="ReturnKgtoPcktltr(this),ReturnFatInKg(this),ReturnSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity <br/>(In Ltr)" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPaticularsQuantityInLtr" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInLtr") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Of Packets" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPaticularsNoOfPackets" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("NoOfPackets") %>' onblur="Returnpktltrkg(this),ReturnFatInKg(this),ReturnSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FAT %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtParticularFat_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="ReturnFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtParticularSnf_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="ReturnSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FATInKg" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtParticularFATInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNFInKg" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtParticularSnfInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnPaticulars" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnPaticulars_Click" />
                                                <asp:Button ID="btnPaticularsGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnPaticularsGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvMilkToIP" ShowFooter="true" ClientIDMode="Static" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvMilkToIP_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Variant">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMilkToIPItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                                <asp:Label ID="lblMilkToIPItemType_id" CssClass="hidden" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Milk Quanity" HeaderStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlMilkType" runat="server" CssClass="form-control select2">
                                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                                    <asp:ListItem Value="Good">Sour</asp:ListItem>
                                                                    <asp:ListItem Value="Curdle">Curdle</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblMilkType" runat="server" CssClass="hidden" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMilkToIPQuantityInKg" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="MIPQtyInLtr(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMilkToIPQuantityInLtr" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="MIPQtyInKg(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMilkToIPFat_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="MIPFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMilkToIPSnf_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMilkToIPFATInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMilkToIPSnfInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnMilkToIP" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnMilkToIP_Click" />
                                                <asp:Button ID="btnMilkToIPGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnMilkToIPGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvIssuetoMDPOrCC" ClientIDMode="Static" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIssuetoMDPOrCC_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="MDP/CC" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlMDPCC" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblOffice_ID" runat="server" CssClass="hidden" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlMDPCCTankerNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblMDPCCI_TankerID" Text='<%# Eval("I_TankerID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMDPCCChallanNo" runat="server" CssClass="form-control" Text='<%# Eval("ChallanNo") %>'></asp:TextBox>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
														<asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlMDPCCMilkQuality" runat="server" CssClass="form-control select2">
                                                                   <%-- <asp:ListItem Value="Select">Select</asp:ListItem>--%>
                                                                    <asp:ListItem Value="Skimmed Milk">Skimmed Milk</asp:ListItem>
                                                                    <asp:ListItem Value="Whole Milk">Whole Milk</asp:ListItem>
                                                                   
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblMDPCCMilkQuality" Text='<%# Eval("MilkType") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoMDPOrCCQuantityInKg" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcQtyInLtr(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoMDPOrCCQuantityInLtr" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcQtyInKg(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoMDPOrCCFat_Per" onkeypress="return validateDec(this,event)" Text='<%# Eval("FAT") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoMDPOrCCSnf_Per" onkeypress="return validateDec(this,event)" Text='<%# Eval("SNF") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoMDPOrCCFATInKg" ClientIDMode="Static" Text='<%# Eval("FatInKg") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoMDPOrCCSnfInKg" ClientIDMode="Static" Text='<%# Eval("SnfInKg") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnIssuetoMDPOrCCRemove" runat="server" Text="-" OnClick="btnIssuetoMDPOrCCRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnIssuetoMDPOrCCAdd" runat="server" Text="+" OnClick="btnIssuetoMDPOrCCAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIssuetoMDPOrCC" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIssuetoMDPOrCC_Click" />
                                                <asp:Button ID="btnIssuetoMDPOrCCGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnIssuetoMDPOrCCGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvIsuuetoOtherParty" ClientIDMode="Static" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIsuuetoOtherParty_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Third Party" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlThirdUnion" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblOffice_ID" runat="server" CssClass="hidden" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlThirdUnionTankerNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                <asp:Label ID="lblThirdUnionI_TankerID" Text='<%# Eval("I_TankerID") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtThirdUnionChallanNo" runat="server" CssClass="form-control" Text='<%# Eval("ChallanNo") %>'></asp:TextBox>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
														<asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlThirdUnionMilkQuality" runat="server" CssClass="form-control select2">
                                                                  <%--  <asp:ListItem Value="Select">Select</asp:ListItem>--%>
                                                                   <asp:ListItem Value="Skimmed Milk">Skimmed Milk</asp:ListItem>
                                                                    <asp:ListItem Value="Whole Milk">Whole Milk</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblThirdUnionMilkQuality" Text='<%# Eval("MilkType") %>' runat="server" CssClass="hidden"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIsuuetoOtherPartyQuantityInKg" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInKg") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcQtyInLtr(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIsuuetoOtherPartyQuantityInLtr" onkeypress="return validateDec(this,event)" Text='<%# Eval("QtyInLtr") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcQtyInKg(this),CCProcFatInKg(this),CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIsuuetoOtherPartyFat_Per" onkeypress="return validateDec(this,event)" Text='<%# Eval("FAT") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIsuuetoOtherPartySnf_Per" onkeypress="return validateDec(this,event)" Text='<%# Eval("SNF") %>' ClientIDMode="Static" CssClass="form-control" runat="server" onblur="CCProcSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIsuuetoOtherPartyFATInKg" ClientIDMode="Static" Text='<%# Eval("FatInKg") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIsuuetoOtherPartySnfInKg" ClientIDMode="Static" Text='<%# Eval("SnfInKg") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnIsuuetoOtherPartyRemove" runat="server" Text="-" OnClick="btnIsuuetoOtherPartyRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnIsuuetoOtherPartyAdd" runat="server" Text="+" OnClick="btnIsuuetoOtherPartyAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIsuuetoOtherParty" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIsuuetoOtherParty_Click" />
                                                <asp:Button ID="btnIsuuetoOtherPartyGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnIsuuetoOtherPartyGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="GVIssuetoPowderPlant" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="GVIssuetoPowderPlant_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlVariant" runat="server" CssClass="form-control select2">

                                                                    <asp:ListItem Value="For Skimmed Milk">For Skimmed Milk</asp:ListItem>
                                                                    <asp:ListItem Value="For Whole Milk(HPWM)">For Whole Milk(HPWM)</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblVariant" runat="server" CssClass="hidden" Text='<%# Eval("Variant") %>'>
                                                              
                                                                </asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Container">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlContainer" runat="server" CssClass="form-control select2">
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblContainer" runat="server" CssClass="hidden" Text='<%# Eval("I_MCID") %>'>
                                                              
                                                                </asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoPowderPlantQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="MIPQtyInLtr(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoPowderPlantQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="MIPQtyInLtr(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoPowderPlantFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' onblur="MIPFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoPowderPlantSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' onblur="MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoPowderPlantFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("FatInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoPowderPlantSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnIssuetoPowderPlantRemove" runat="server" Text="-" OnClick="btnIssuetoPowderPlantRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnIssuetoPowderPlantAdd" runat="server" Text="+" OnClick="btnIssuetoPowderPlantAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIssuetoPowderPlant" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIssuetoPowderPlant_Click" />
                                                <asp:Button ID="btnIssuetoPowderPlantGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnIssuetoPowderPlantGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvIssuetoCream" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIssuetoCream_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoCreamQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoCreamQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoCreamFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="FatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoCreamSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoCreamFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>' onblur="IssuetocreamFat(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoCreamSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>' onblur="IssuetocreamSnf(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIssuetoCream" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIssuetoCream_Click" />
                                                <asp:Button ID="btnIssuetoCreamGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnIssuetoCreamGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvIssuetoIceCream" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIssuetoIceCream_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Milk">Milk</asp:ListItem>
                                                                    <asp:ListItem Value="Cream">Cream</asp:ListItem>
                                                                    <asp:ListItem Value="SMP">SMP</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblType" runat="server" CssClass="hidden" Text='<%# Eval("Type") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoIceCreamQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="MIPQtyInLtr(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoIceCreamQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="MIPQtyInKg(this),MIPFatInKg(this),MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoIceCreamFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="MIPFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoIceCreamSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="MIPSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoIceCreamFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetoIceCreamSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnIssuetoIceCreamRemove" runat="server" Text="-" OnClick="btnIssuetoIceCreamRemove_Click" CssClass="btn btn-warning" />
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnIssuetoIceCreamAdd" runat="server" Text="+" OnClick="btnIssuetoIceCreamAdd_Click" CssClass="btn btn-success" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIssuetoIceCream" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIssuetoIceCream_Click" />
                                                <asp:Button ID="btnIssuetoIceCreamGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total"  OnClick="btnIssuetoIceCreamGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvIssuetoother" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIssuetoother_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetootherQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetootherQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetootherFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="FatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetootherSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetootherFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssuetootherSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIssuetoother" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIssuetoother_Click" />
                                                <asp:Button ID="btnIssuetootherGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnIssuetootherGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvOther" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvOther_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotherQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotherQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotherFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="FatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotherSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotherFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtotherSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnOutflowOther" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnOutflowOther_Click" />
                                                <asp:Button ID="btnOutflowOtherGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnOutflowOtherGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="GVIssueofgoatmilk" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="GVIssueofgoatmilk_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIssueofgoatmilkVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssueofgoatmilkQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssueofgoatmilkQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssueofgoatmilkFat_per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="FatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssueofgoatmilkSnf_Per" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssueofgoatmilkFatInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIssueofgoatmilkSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnIssueofgoatmilkSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnIssueofgoatmilkSave_Click"/>
                                                <asp:Button ID="btnIssueofgoatmilk_gettotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnIssueofgoatmilk_gettotal_Click"/>
                                            </div>

                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvClosingBalances" ShowFooter="true" ClientIDMode="Static" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvClosingBalances_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tanker" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClosingV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                                <asp:Label ID="lblClosingI_MCID" CssClass="hidden" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClosingBalancesQuantityInKg" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClosingBalancesQuantityInLtr" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClosingBalancesFat_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="FatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClosingBalancesSnf_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="SnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClosingBalancesFATInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClosingBalancesSnfInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnClosingBalances" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnClosingBalances_Click" />
                                                <asp:Button ID="btnClosingBalancesGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnClosingBalancesGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvColdRoomBalances" ClientIDMode="Static" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvColRoomBalances_RowCreated">
                                                    <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Variant" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCRBItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                <asp:Label ID="lblCRBItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                                <asp:TextBox ID="lblReturnPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:TextBox>
                                                            <asp:HiddenField ID="hfCRB" runat="server" Value='<%# Eval("LtrtoKgFactor") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No Of Packets" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesPackets" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("NoOfPackets") %>' onblur="CRBPcktltr(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Qty. In Ltr." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesQuantityInLtr" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="CRBLtrtoKg(this),CRBFatInKg(this),CRBSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesQuantityInKg" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="CRBFatInKg(this),CRBSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesFat_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Fat_Per") %>' onblur="CRBFatInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesSnf_Per" onkeypress="return validateDec(this,event)" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("Snf_Per") %>' onblur="CRBSnfInKg(this)"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesFATInKg" ClientIDMode="Static" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgFat") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtColdRoomBalancesSnfInKg" Enabled="true" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnColdRoomBalances" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnColdRoomBalances_Click" />
                                                <asp:Button ID="btnColdRoomBalancesGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnColdRoomBalancesGetTotal_Click"/>
                                            </div>

                                        </div>
                                    </div>
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
                                </fieldset>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GVRRSheet" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemCat_id" Visible="false" runat="server" Text='<%# Eval("ItemCat_id") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Balance <br/>B.F." HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtBalance_BFRR" Enabled="false" Text='<%# Eval("RR_OpeningBalance_New") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Obtained" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_Obtained") %>' ID="txtRRObtained" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_Total") %>' ID="txtRRTotal" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Toning" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_Toning") %>' ID="txtRRToning" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Maintaining <br />S.N.F." HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_MaintainingSNF") %>' ID="txtRRMaintainingSNF" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Issued For<br />Product Section" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_IssuedForProductSection") %>' ID="txtRRIssueforproductionsection" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Issued" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_TotalIssued") %>' ID="txtRRTotalIssued" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Closing <br />Balance" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox Text='<%# Eval("RR_ClosingBalance") %>' ID="txtRRClosingBalance" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="Submit" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnGetTotal" runat="server" CssClass="btn btn-success" Text="Get Total" OnClick="btnGetTotal_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <div class="modal fade" id="ModalViewDemand" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">View Demand</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group table-responsive">
                                <asp:GridView ID="gvmttos" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Variant Name">
                                            <ItemTemplate>

                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                Total
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Prev. Demand </br>500 ML (In Pkt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrev_Demand_500InPkt" Text='<%# Eval("500MLPPkt") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="Prev_Demand_500InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev. Demand </br>200 ML (In Pkt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrev_Demand_200InPkt" Text='<%# Eval("200MLPPkt") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="Prev_Demand_200InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prev. Demand </br>1 Ltr(In Pkt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrev_Demand_1InPkt" Text='<%# Eval("1LPPkt") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="Prev_Demand_1InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Prev. Demand </br>500 ML (In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand500InLtr" Text='<%# Eval("500MLPLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_Demand500InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Prev. Demand </br>200 ML (In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand200InLtr" Text='<%# Eval("200MLPLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_Demand200InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Prev. Demand </br>Total in Ltrs">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrev_DemandInLtr" Text='<%# Eval("PrevDemInLtr") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblPrev_Demand1InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Demand </br>500 ML (In Pkt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrent_Demand_500InPkt" runat="server" Text='<%# Eval("500MLCPkt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblCurrent_Demand_500InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Demand </br>200 ML (In Pkt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrent_Demand_200InPkt" runat="server" Text='<%# Eval("200MLCPkt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblCurrent_Demand_200InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Demand </br>1 Ltr (In Pkt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrent_Demand_1InPkt" runat="server" Text='<%# Eval("1LCPkt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblCurrent_Demand_1InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Demand </br>Total in Ltrs">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrent_Demand_1InLtr" runat="server" Text='<%# Eval("CurDemInLtr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblCurrent_Demand_1InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ViewDemand() {
            $('#ModalViewDemand').modal('show');
        }



        function GetQtyInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInLtr = $(row).children("td").eq(2).find('input[type="text"]').val();
            var Fat = $(row).children("td").eq(3).find('input[type="text"]');
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                $(row).children("td").eq(1).find('input[type="text"]').val(QtyInKg);

            }

            CalculateTotalInFlow();
            CalculateTotalOutFlow();

        }
        function GetQtyInLtr(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var QtyInLtr = 0;
            if (QtyInKg == "")
                QtyInKg = 0;
            if (QtyInKg != "") {
                QtyInLtr = parseFloat(QtyInKg / 1.030).toFixed(2);
                $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();

        }
        function FatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(3).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(5).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function SnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(6).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();

        }
        function ReturnKgtoPcktltr(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var PackagingSize = $(row).children("td").eq(0).find('input[type="text"]').val();
            var Value = PackagingSize.split(",");
            var Unit_id = Value[1];
            PackagingSize = Value[0];
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var QtyInLtr = $(row).children("td").eq(2).find('input[type="text"]').val();
            var NoOfPackets = $(row).children("td").eq(3).find('input[type="text"]').val();
            var LtrtoKgFactor = $(row).children("td").eq(0).find('input[type="hidden"]').val();
            var QtyInLtr = 0;
            if (PackagingSize == "")
                PackagingSize = 0;

            if (QtyInKg != "") {
                QtyInLtr = parseFloat(QtyInKg / LtrtoKgFactor).toFixed(2);
                $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);
            }
            if (Unit_id == 6) {
                NoOfPackets = parseFloat(QtyInLtr / PackagingSize).toFixed(2);
                NoOfPackets = Math.round(NoOfPackets);
                $(row).children("td").eq(3).find('input[type="text"]').val(NoOfPackets);
            }
            if (Unit_id == 20) {
                NoOfPackets = parseFloat(QtyInLtr / (PackagingSize / 1000)).toFixed(2);
                NoOfPackets = Math.round(NoOfPackets);
                $(row).children("td").eq(3).find('input[type="text"]').val(NoOfPackets);
            }

            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function Returnpktltrkg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var PackagingSize = $(row).children("td").eq(0).find('input[type="text"]').val();
            var Value = PackagingSize.split(",");
            var Unit_id = Value[1];
            PackagingSize = Value[0];
            //var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            //var QtyInLtr = $(row).children("td").eq(2).find('input[type="text"]').val();
            var NoOfPackets = $(row).children("td").eq(3).find('input[type="text"]').val();
            var LtrtoKgFactor = $(row).children("td").eq(0).find('input[type="hidden"]').val();
            var QtyInLtr = 0;
            var QtyInKg = 0;
            if (PackagingSize == "")
                PackagingSize = 0;

            if (NoOfPackets == "")
                NoOfPackets = 0;
            if (Unit_id == 6) {
                QtyInLtr = parseFloat(NoOfPackets * PackagingSize).toFixed(2);
            }
            if (Unit_id == 20) {
                QtyInLtr = parseFloat(NoOfPackets * (PackagingSize / 1000)).toFixed(2);
            }

            QtyInKg = parseFloat(QtyInLtr * LtrtoKgFactor).toFixed(2);
            $(row).children("td").eq(1).find('input[type="text"]').val(QtyInKg);
            $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);
            //QtyInLtr = NoOfPackets *
            //if (QtyInKg != "") {
            //    QtyInLtr = parseFloat(QtyInKg / LtrtoKgFactor).toFixed(2);
            //    $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);
            //}
            //if (Unit_id == 6) {
            //    NoOfPackets = parseFloat(QtyInLtr / PackagingSize).toFixed(2);
            //    NoOfPackets = Math.round(NoOfPackets);
            //    $(row).children("td").eq(3).find('input[type="text"]').val(NoOfPackets);
            //}
            //if (Unit_id == 20) {
            //    NoOfPackets = parseFloat(QtyInLtr / (PackagingSize / 1000)).toFixed(2);
            //    NoOfPackets = Math.round(NoOfPackets);
            //    $(row).children("td").eq(3).find('input[type="text"]').val(NoOfPackets);
            //}

            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function GetReturnQtyInLtr(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInLtr = $(row).children("td").eq(2).find('input[type="text"]').val();
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                $(row).children("td").eq(3).find('input[type="text"]').val(QtyInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function ReturnFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(6).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function ReturnSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(7).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();


        }

        function CRBPcktltr(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var PackagingSize = $(row).children("td").eq(0).find('input[type="text"]').val();
            var LtrtoKgFactor = $(row).children("td").eq(0).find('input[type="hidden"]').val();
            var Value = PackagingSize.split(",");
            var Unit_id = Value[1];
            PackagingSize = Value[0];
            var QtyInKg = $(row).children("td").eq(3).find('input[type="text"]').val();
            var QtyInLtr = $(row).children("td").eq(2).find('input[type="text"]').val();
            var NoOfPackets = $(row).children("td").eq(1).find('input[type="text"]').val();
            var QtyInLtr = 0;
            if (PackagingSize == "")
                PackagingSize = 0;
            if (NoOfPackets == "")
                NoOfPackets = 0;


            if (Unit_id == 6) {
                QtyInLtr = parseFloat(NoOfPackets * PackagingSize).toFixed(2);
                $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);
            }
            if (Unit_id == 20) {
                QtyInLtr = parseFloat((NoOfPackets * PackagingSize) / 1000).toFixed(2);
                $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);

            }
            //if (QtyInKg != "") {
            //    QtyInLtr = parseFloat(QtyInKg / 1.030).toFixed(2);
            //    $(row).children("td").eq(2).find('input[type="text"]').val(QtyInLtr);
            //}
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CRBLtrtoKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInLtr = $(row).children("td").eq(2).find('input[type="text"]').val();
            var LtrtoKgFactor = $(row).children("td").eq(0).find('input[type="hidden"]').val();
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * LtrtoKgFactor).toFixed(2);
                $(row).children("td").eq(3).find('input[type="text"]').val(QtyInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CRBFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(3).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(6).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CRBSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(3).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(7).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();


        }

        function BMCCollFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(3).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(6).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function BMCCollSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(3).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(7).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }

        function InFlowFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(2).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(4).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function InFlowSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(3).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(5).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
		 function CCProcQtyInLtr(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInKg = $(row).children("td").eq(4).find('input[type="text"]').val();
            var QtyInLtr = 0;
            if (QtyInKg == "")
                QtyInKg = 0;
            if (QtyInKg != "") {
                QtyInLtr = parseFloat(QtyInKg / 1.030).toFixed(2);
                $(row).children("td").eq(5).find('input[type="text"]').val(QtyInLtr);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CCProcQtyInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInLtr = $(row).children("td").eq(5).find('input[type="text"]').val();
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                $(row).children("td").eq(4).find('input[type="text"]').val(QtyInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CCProcFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(4).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(6).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(8).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CCProcSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(4).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(7).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(9).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function MIPQtyInLtr(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var QtyInLtr = 0;
            if (QtyInKg == "")
                QtyInKg = 0;
            if (QtyInKg != "") {
                QtyInLtr = parseFloat(QtyInKg / 1.030).toFixed(2);
                $(row).children("td").eq(3).find('input[type="text"]').val(QtyInLtr);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function MIPQtyInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInLtr = $(row).children("td").eq(3).find('input[type="text"]').val();
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                $(row).children("td").eq(2).find('input[type="text"]').val(QtyInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function MIPFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(6).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function MIPSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(7).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function ProcessLosseQtyInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInLtr = $(row).children("td").eq(1).find('input[type="text"]').val();
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                $(row).children("td").eq(2).find('input[type="text"]').val(QtyInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function ProcessPackedQtyInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;

            var QtyInLtr = $(row).children("td").eq(3).find('input[type="text"]').val();
            var QtyInKg = 0;
            if (QtyInLtr == "")
                QtyInLtr = 0;
            if (QtyInLtr != "") {
                QtyInKg = parseFloat(QtyInLtr * 1.030).toFixed(2);
                $(row).children("td").eq(4).find('input[type="text"]').val(QtyInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function ProcessFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var TotalQtyinKg = 0;
            var LQtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var PQtyInKg = $(row).children("td").eq(4).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (LQtyInKg == "")
                LQtyInKg = 0;
            if (PQtyInKg == "")
                PQtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            TotalQtyinKg = parseFloat(LQtyInKg) + parseFloat(PQtyInKg);
            FatInKg = parseFloat((TotalQtyinKg / 100) * FatPer).toFixed(2);
            $(row).children("td").eq(7).find('input[type="text"]').val(FatInKg);

            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function ProcessSnfInKg(currentrow) {
            debugger;
            var TotalQtyinKg = 0;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var LQtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var PQtyInKg = $(row).children("td").eq(4).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(6).find('input[type="text"]').val();
            if (LQtyInKg == "")
                LQtyInKg = 0;
            if (PQtyInKg == "")
                PQtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            TotalQtyinKg = parseFloat(LQtyInKg) + parseFloat(PQtyInKg);
            SnfInKg = parseFloat((TotalQtyinKg / 100) * SnfPer).toFixed(2);
            $(row).children("td").eq(8).find('input[type="text"]').val(SnfInKg);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();

        }
		function InflowothersFat(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var FatInKg = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatInKg == "")
                FatInKg = 0;
            var Fat = parseFloat((FatInKg / QtyInKg) * 100).toFixed(2);
            $(row).children("td").eq(2).find('input[type="text"]').val(Fat);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function InflowothersSnf(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var SnfInKg = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfInKg == "")
                SnfInKg = 0;
            var Fat = parseFloat((SnfInKg / QtyInKg) * 100).toFixed(2);
            $(row).children("td").eq(3).find('input[type="text"]').val(Fat);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
		function CanesCollectionFat(currentrow)
        {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var FatInKg = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatInKg == "")
                FatInKg = 0;
            var Fat = parseFloat((FatInKg / QtyInKg) * 100).toFixed(2);
            $(row).children("td").eq(3).find('input[type="text"]').val(Fat);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CanesCollectionSnf(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var SnfInKg = $(row).children("td").eq(6).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfInKg == "")
                SnfInKg = 0;
            var Fat = parseFloat((SnfInKg / QtyInKg) * 100).toFixed(2);
            $(row).children("td").eq(4).find('input[type="text"]').val(Fat);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CanesCollectionFatInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var FatInKg = 0;
            var FatPer = $(row).children("td").eq(3).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatPer == "")
                FatPer = 0;
            if (QtyInKg != "") {
                FatInKg = parseFloat((QtyInKg / 100) * FatPer).toFixed(2);
                $(row).children("td").eq(5).find('input[type="text"]').val(FatInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function CanesCollectionSnfInKg(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(2).find('input[type="text"]').val();
            var SnfInKg = 0;
            var SnfPer = $(row).children("td").eq(4).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfPer == "")
                SnfPer = 0;
            if (QtyInKg != "") {
                SnfInKg = parseFloat((QtyInKg / 100) * SnfPer).toFixed(2);
                $(row).children("td").eq(6).find('input[type="text"]').val(SnfInKg);
            }
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function IssuetocreamFat(currentrow) {
            debugger;
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var FatInKg = $(row).children("td").eq(5).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (FatInKg == "")
                FatInKg = 0;
            var Fat = parseFloat((FatInKg / QtyInKg) * 100).toFixed(2);
            $(row).children("td").eq(3).find('input[type="text"]').val(Fat);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        function IssuetocreamSnf(currentrow) {
            var row = currentrow.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var QtyInKg = $(row).children("td").eq(1).find('input[type="text"]').val();
            var SnfInKg = $(row).children("td").eq(6).find('input[type="text"]').val();
            if (QtyInKg == "")
                QtyInKg = 0;
            if (SnfInKg == "")
                SnfInKg = 0;
            var Fat = parseFloat((SnfInKg / QtyInKg) * 100).toFixed(2);
            $(row).children("td").eq(4).find('input[type="text"]').val(Fat);
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        }
        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });

        $(document).ready(function () {
            CalculateTotalInFlow();
            CalculateTotalOutFlow();
        });

        function CalculateTotalInFlow() {
            debugger
            var i = 0;
            var j = 0;
            var k = 0;
            var l = 0;
            var m = 0;
            var n = 0;
            var o = 0;
            var p = 0;
            var q = 0;
            var r = 0;
            var TQtyInLtr = 0;
            var TQtyInKg = 0;
            var TFatInkg = 0;
            var TSnfInkg = 0;

            var rowcount = $('#gvOpening tr').length;
            $('#gvOpening tr').each(function (index) {
                if (i > 1 && i < rowcount - 1) {

                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                i++;
            });
            var rowcountprocess = $('#gvProcess tr').length;
            $('#gvProcess tr').each(function (index) {
                if (p > 1 && p < rowcountprocess - 1) {

                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                p++;
            });
            var rowcount1 = $('#gvReturn tr').length;
            $('#gvReturn tr').each(function (index) {
                if (j > 1 && j < rowcount1 - 1) {

                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                j++;
            });
            var rowcount3 = $('#gvCCWiseProcurement tr').length;

            $('#gvCCWiseProcurement tr').each(function (index) {

                if (k > 1 && k < rowcount3 - 1) {


                    var QtyInLtr = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(8).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(9).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                k++;
            });

            var rowcount10 = $('#gvCCWiseGoatMilkProcurement tr').length;

            $('#gvCCWiseGoatMilkProcurement tr').each(function (index) {

                if (r > 1 && r < rowcount10 - 1) {


                    var QtyInLtr = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(8).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(9).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                r++;
            });
            var rowcount4 = $('#gvinflowother tr').length;

            $('#gvinflowother tr').each(function (index) {
                debugger;
                if (l > 1 && l < rowcount4 - 1) {

                    debugger;

                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(5).find('input[type="text"]').val();

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;


                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                l++;
            });

            var rowcount5 = $('#gvBMCDCSCollection tr').length;

            $('#gvBMCDCSCollection tr').each(function (index) {

                if (m > 1 && m < rowcount5 - 1) {

                    debugger;
                    //var QtyInLtr = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    //if (QtyInLtr == "")
                    //    QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;


                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                m++;
            });

            var rowcount6 = $('#gvrcvdfrmothrUnion tr').length;

            $('#gvrcvdfrmothrUnion tr').each(function (index) {

                if (n > 1 && n < rowcount6 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                n++;
            });


            var rowcount8 = $('#gvForPowderConversion tr').length;

            $('#gvForPowderConversion tr').each(function (index) {

                if (o > 1 && o < rowcount8 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                o++;
            });
            var rowcount9 = $('#gvCanesCollection tr').length;

            $('#gvCanesCollection tr').each(function (index) {
                debugger;
                if (q > 1 && q < rowcount9 - 1) {

                    debugger;

                    var QtyInKg = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(6).find('input[type="text"]').val();

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;


                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                q++;
            });


            document.getElementById('<%=txtInFlowTQtyInKg.ClientID%>').value = TQtyInKg;
            document.getElementById('<%=txtInFlowTFatInKg.ClientID%>').value = TFatInkg;
            document.getElementById('<%=txtInFlowTSnfInKg.ClientID%>').value = TSnfInkg;

        }
        function CalculateTotalOutFlow() {
            debugger;
            var i = 0;
            var j = 0;
            var k = 0;
            var l = 0;
            var m = 0;
            var n = 0;
            var o = 0;
            var p = 0;
            var q = 0;
            var r = 0;
            var s = 0;
            var TQtyInLtr = 0;
            var TQtyInKg = 0;
            var TFatInkg = 0;
            var TSnfInkg = 0;
            var rowcount = $('#gvPaticulars tr').length;
            $('#gvPaticulars tr').each(function (index) {
                if (i > 1 && i < rowcount - 1) {
                    debugger;
                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                i++;
            });
            var rowcount1 = $('#gvMilkToIP tr').length;
            $('#gvMilkToIP tr').each(function (index) {
                if (j > 1 && j < rowcount1 - 1) {

                    var QtyInLtr = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                j++;
            });
            var rowcount3 = $('#gvIssuetoMDPOrCC tr').length;

            $('#gvIssuetoMDPOrCC tr').each(function (index) {
                debugger;
                if (k > 1 && k < rowcount3 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(8).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(9).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                k++;
            });
            var rowcount4 = $('#gvIsuuetoOtherParty tr').length;

            $('#gvIsuuetoOtherParty tr').each(function (index) {
                debugger;
                if (l > 1 && l < rowcount4 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(8).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(9).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                l++;
            });




            var rowcount5 = $('#gvIssuetoother tr').length;

            $('#gvIssuetoother tr').each(function (index) {
                debugger;
                if (m > 1 && m < rowcount5 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                m++;
            });

            var rowcount6 = $('#gvOther tr').length;

            $('#gvOther tr').each(function (index) {
                debugger;
                if (n > 1 && n < rowcount6 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;


                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                n++;
            });

            var rowcount7 = $('#gvClosingBalances tr').length;

            $('#gvClosingBalances tr').each(function (index) {
                debugger;
                if (o > 1 && o < rowcount7 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                o++;
            });


            var rowcount8 = $('#GVIssuetoPowderPlant tr').length;
            $('#GVIssuetoPowderPlant tr').each(function (index) {
                debugger;
                if (p > 1 && p < rowcount8 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                p++;
            });

            var rowcount9 = $('#gvIssuetoCream tr').length;
            $('#gvIssuetoCream tr').each(function (index) {
                debugger;
                if (q > 1 && q < rowcount9 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                q++;
            });


            var rowcount10 = $('#gvColdRoomBalances tr').length;
            $('#gvColdRoomBalances tr').each(function (index) {
                debugger;
                if (r > 1 && r < rowcount10 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                r++;
            });
            var rowcount11 = $('#gvIssuetoIceCream tr').length;
            $('#gvIssuetoIceCream tr').each(function (index) {
                debugger;
                if (s > 1 && s < rowcount11 - 1) {

                    debugger;
                    var QtyInLtr = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var QtyInKg = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var FatInkg = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var SnfInkg = $(this).children("td").eq(7).find('input[type="text"]').val();
                    if (QtyInLtr == "")
                        QtyInLtr = 0;

                    if (QtyInKg == "")
                        QtyInKg = 0;

                    if (FatInkg == "")
                        FatInkg = 0;

                    if (SnfInkg == "")
                        SnfInkg = 0;

                    TQtyInLtr = parseFloat(parseFloat(TQtyInLtr) + parseFloat(QtyInLtr)).toFixed(2)
                    TQtyInKg = parseFloat(parseFloat(TQtyInKg) + parseFloat(QtyInKg)).toFixed(2)
                    TFatInkg = parseFloat(parseFloat(TFatInkg) + parseFloat(FatInkg)).toFixed(2)
                    TSnfInkg = parseFloat(parseFloat(TSnfInkg) + parseFloat(SnfInkg)).toFixed(2)
                }
                s++;
            });



            document.getElementById('<%=txtOutFlowTQtyInKg.ClientID%>').value = TQtyInKg;
            document.getElementById('<%=txtOutFlowTFatInKg.ClientID%>').value = TFatInkg;
            document.getElementById('<%=txtOutFlowTSnfInKg.ClientID%>').value = TSnfInkg;

        }

    </script>
	 <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            debugger;
            $("[id$=txtBMCRootName]").autocomplete({

                source: function (request, response) {
                    $.ajax({

                        url: '<%=ResolveUrl("MilkProductionEntry_New.aspx/SearchBMCRootName") %>',
                        data: "{ 'BMCRootName': '" + request.term + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",

                        contentType: "application/json; charset=utf-8",
                        success: function (data) {

                            response($.map(data.d, function (item) {
                                return {
                                    label: item
                                    //val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfBMCRootName]").val(i.item.val);
                },
                minLength: 1

            });

        });
    </script>
</asp:Content>

