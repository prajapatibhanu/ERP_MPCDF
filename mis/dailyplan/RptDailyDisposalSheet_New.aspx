<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDailyDisposalSheet_New.aspx.cs" Inherits="mis_dailyplan_RptDailyDisposalSheet_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }
         @media print {
              .NonPrintable {
                  display: block;
              }
              .noprint {
                display: none;
            }
               .pagebreak { page-break-before: always; }
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


    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">DAILY DISPOSAL SHEET</h3>
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

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                            </div>
                        </div>


                    </div>

                    <fieldset runat="server" id="divfinal" visible="false">
                        <legend>Milk Sheet</legend>
                        <div class="col-md-12">
                            <div class="form-group table-responsive">

                                <%--<h1 class="text-center" style="font-weight: 800; font-size: 20px;">INDORE SAHKARI DUGDHA SANGH MARYADIT</h1>--%>
                                <h1 class="text-center" style="font-weight: 800; font-size: 20px;"><span id="spnOfficeName" runat="server"></span></h1>
								
                              <%--  <h3 class="text-center" style="font-weight: 500; font-size: 13px;">Indore Dairy Plant</h3>--%>
                                <h3 class="text-center" style="font-weight: 500; font-size: 13px;">Dairy Plant&nbsp;&nbsp;&nbsp;<span id="spndate" runat="server"></span>&nbsp;&nbsp;&nbsp;<span id="spnshift" runat="server"></span></h3>
                                <h2 class="text-center" style="font-weight: 800; font-size: 20px;">DAILY DISPOSAL SHEET</h2>


                                
                            </div>
                        </div>
                        <div class="col-md-12">
                            <fieldset>
                                <legend>In Flow</legend>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvOpening" runat="server" ShowFooter="true" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvOpening_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tank/Silo" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOpeningV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtOpeningQuantityInKg" runat="server"  Text='<%# Eval("QtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtOpeningQuantityInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtOpeningFat_Per"  runat="server" Text='<%# Eval("Fat_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtOpeningSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtOpeningFATInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtOpeningSNFInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvProcess" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvProcess_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Variant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcessItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                            <asp:Label ID="lblProcessItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    
                                                    <asp:TemplateField HeaderText="No Of Packets.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessnofpackets"  runat="server" onchange="Packetoltr(this)" Text='<%# Eval("NoOfPackets") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													<asp:TemplateField HeaderText="Packet Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessPackedInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													 <asp:TemplateField HeaderText="Packet Qty. In Kgs">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessPackedInKg"  runat="server" Text='<%# Eval("QtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessFat_Per"  runat="server" Text='<%# Eval("Fat_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessFATInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtProcessSNFInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvReturn" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvReturn_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Variant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReturnItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                             <asp:Label ID="lblReturnItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                            <asp:Label ID="lblReturnPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Packed Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnPackedInKg"  runat="server" Text='<%# Eval("PackedQtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Packed Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnPackedInLtr"  runat="server" Text='<%# Eval("PackedQtyInLtr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Of Packets.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnnofpackets"  runat="server" onchange="Packetoltr(this)" Text='<%# Eval("NoOfPackets") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnFat_per"  runat="server" Text='<%# Eval("Fat_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnSnf_Per" runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnFatInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtReturnSnfInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                                                           
                                                             <asp:Label ID="lblOffice_ID"  Text='<%# Eval("Office_Name") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanker" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                             <asp:Label ID="lblV_VehicleNo"  Text='<%# Eval("V_VehicleNo") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                             <asp:Label ID="lblChallanNo"  Text='<%# Eval("ChallanNo") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													<asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCCMilkQuality" Text='<%# Eval("MilkQuality") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCCQuantityInKg" runat="server" Text='<%# Eval("QtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCCQuantityInLtr" runat="server" Text='<%# Eval("QtyInLtr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCCFat_Per"  runat="server" Text='<%# Eval("FAT") %>' onchange="FatInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCCSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' onchange="SnfInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCCFATInKg"  runat="server" Text='<%# Eval("FatInKg") %>' onchange="FatInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCCSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                            </asp:GridView>
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
                                                               
                                                                <asp:Label ID="lblCCGoatMilkOffice_ID" Text='<%# Eval("Office_Name") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                               <asp:Label ID="lblCCGoatMilkV_VehicleNo"  Text='<%# Eval("V_VehicleNo") %>'  runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                             <asp:Label ID="lblChallanNo"  Text='<%# Eval("ChallanNo") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
														<asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="12%">
                                                            <ItemTemplate>                                  
                                                                <asp:Label ID="lblCCGoatMilkMilkQuality" Text='<%# Eval("MilkQuality") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCCGoatMilkQuantityInKg"  ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCCGoatMilkQuantityInLtr"  ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCCGoatMilkFat_Per"  ClientIDMode="Static" runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCCGoatMilkSnf_Per"  ClientIDMode="Static" runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. Fat">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCCGoatMilkFATInKg" ClientIDMode="Static" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kgs. SNF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCCGoatMilkSnfInKg" ClientIDMode="Static"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        
                                    </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvrcvdfrmothrUnion" ClientIDMode="Static" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvrcvdfrmothrUnion_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Union" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                            
                                                             <asp:Label ID="lblOffice_ID"  Text='<%# Eval("Office_Name") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="MilkType" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                          <asp:Label ID="lblMilkTypeID"  Text='<%# Eval("MilkTypeName") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtrcvdfrmothrUnionQuantityInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtrcvdfrmothrUnionQuantityInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtrcvdfrmothrUnionFat_Per"  runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtrcvdfrmothrUnionSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtrcvdfrmothrUnionFATInKg"  runat="server" Text='<%# Eval("FatInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtrcvdfrmothrUnionSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvForPowderConversion" ClientIDMode="Static" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvForPowderConversion_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Union" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                          
                                                             <asp:Label ID="lblOffice_ID"  Text='<%# Eval("Office_Name") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="MilkType" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                         <asp:Label ID="lblForPowderConversionMilkTypeID"  Text='<%# Eval("MilkTypeName") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtForPowderConversionQuantityInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtForPowderConversionQuantityInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtForPowderConversionFat_Per"  runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtForPowderConversionSnf_Per"   runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtForPowderConversionFATInKg"  runat="server" Text='<%# Eval("FatInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtForPowderConversionSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvBMCDCSCollection" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvBMCDCSCollection_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tanker">
                                                        <ItemTemplate>
                                                            
                                                             <asp:Label ID="lblI_TankerID"  Text='<%# Eval("V_VehicleNo") %>'  runat="server" ></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Route">
                                                        <ItemTemplate>
                                                            
                                                             <asp:Label ID="lblBMCTankerRootName"  Text='<%# Eval("BMCTankerRootName") %>'  runat="server" ></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Milk Quality">
                                                        <ItemTemplate>
                                                          
                                                            <asp:Label ID="lblBMCDCSCollectionMilkQuality"  Text='<%# Eval("MilkQuality") %>'  runat="server"></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBMCDCSCollectionQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  <%--    <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBMCDCSCollectionQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetReturnQtyInKg(this),ReturnFatInKg(this),ReturnSnfInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBMCDCSCollectionFat_per"  runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBMCDCSCollectionSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBMCDCSCollectionFatInKg" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBMCDCSCollectionSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     
                                                </Columns>
                                            </asp:GridView>
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
                                                            
                                                             <asp:Label ID="lblCanesCollectionVariant"  Text='<%# Eval("Variant") %>'  runat="server" ></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Milk Quality" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                              
                                                              
                                                                 <asp:Label ID="lblCanesCollectionMilkQuality"  Text='<%# Eval("MilkQuality") %>' runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                 
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCanesCollectionQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCanesCollectionFat_per"  runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCanesCollectionSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCanesCollectionFatInKg"  runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtCanesCollectionSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     
                                                </Columns>
                                            </asp:GridView>
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
                                                            <asp:Label ID="txtinflowotherQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInKg") %>' onblur="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtinflowotherQtyInLtr" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server" Text='<%# Eval("QtyInLtr") %>' onblur="GetQtyInKg(this),FatInKg(this),SnfInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtinflowotherFat_per"  runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtinflowotherSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtinflowotherFatInKg"  runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtinflowotherSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table dataTable table-bordered">
                                            <tr style="background-color: #ff874c; color: black">
                                                <td><b>RECEIPT TOTAL</b></td>
                                                <%--<td><b>Qty. In Ltr.</b><br />
                                                    <asp:Textbox ID="txtInFlowTQtyInLtr" runat="server"></asp:Textbox></td>--%>
                                                <td><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="txtInFlowTQtyInKg" runat="server"></asp:Label></td>
                                                <td><b>Kgs. Fat<br />
                                                </b>
                                                    <asp:Label ID="txtInFlowTFatInKg"  runat="server"></asp:Label></td>
                                                <td><b>Kgs. SNF<br />
                                                </b>
                                                    <asp:Label ID="txtInFlowTSnfInKg"  runat="server"></asp:Label></td>
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
                                            <asp:GridView ID="gvPaticulars" runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" ClientIDMode="Static" AutoGenerateColumns="false" OnRowCreated="gvPaticulars_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Variant" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartcularItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                            <asp:Label ID="lblPartcularItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                            <asp:Label ID="lblReturnPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 
                                                 
                                                    <asp:TemplateField HeaderText="Quantity <br/>(In Kg)" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPaticularsQuantityInKg"  runat="server" Text='<%# Eval("QtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Quantity <br/>(In Ltr)" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPaticularsQuantityInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="No Of Packets" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPaticularsNoOfPackets"  runat="server" Text='<%# Eval("NoOfPackets") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FAT %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtParticularFat_Per"  runat="server" Text='<%# Eval("Fat_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtParticularSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FATInKg" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtParticularFATInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNFInKg" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtParticularSnfInKg" runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvMilkToIP" ShowFooter="true"  ClientIDMode="Static" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvMilkToIP_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Variant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMilkToIPItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                            <asp:Label ID="lblMilkToIPItemType_id" CssClass="hidden" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                           
                                                            <asp:Label ID="lblMilkType" runat="server"  Text='<%# Eval("MilkType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMilkToIPQuantityInKg"  runat="server" Text='<%# Eval("QtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMilkToIPQuantityInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMilkToIPFat_Per"  runat="server" Text='<%# Eval("Fat_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMilkToIPSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMilkToIPFATInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMilkToIPSnfInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvIssuetoMDPOrCC"  ClientIDMode="Static" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIssuetoMDPOrCC_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MDP/CC" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                            <asp:Label ID="lblOffice_ID" runat="server"  Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                             
                                                                <asp:Label ID="lblMDPCCV_VehicleNo" Text='<%# Eval("V_VehicleNo") %>' runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                             <asp:Label ID="lblChallanNo"  Text='<%# Eval("ChallanNo") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
														<asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lblMDPCCMilkQuality" Text='<%# Eval("MilkType") %>' runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoMDPOrCCQuantityInKg"  Text='<%# Eval("QtyInKg") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoMDPOrCCQuantityInLtr"  Text='<%# Eval("QtyInLtr") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoMDPOrCCFat_Per"  Text='<%# Eval("FAT") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoMDPOrCCSnf_Per"  Text='<%# Eval("SNF") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoMDPOrCCFATInKg"  Text='<%# Eval("FatInKg") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoMDPOrCCSnfInKg" Text='<%# Eval("SnfInKg") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvIsuuetoOther"  ClientIDMode="Static" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvIsuuetoOther_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Third Party" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                          
                                                            <asp:Label ID="lblOffice_ID" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Tanker No" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                               
                                                                <asp:Label ID="lblThirdUnionV_VehicleNo" Text='<%# Eval("V_VehicleNo") %>' runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Challan No" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                           
                                                             <asp:Label ID="lblChallanNo"  Text='<%# Eval("ChallanNo") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

														<asp:TemplateField HeaderText="Milk Type" HeaderStyle-Width="12%">
                                                            <ItemTemplate>
                                                              
                                                                <asp:Label ID="lblThirdUnionMilkQuality" Text='<%# Eval("MilkType") %>' runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIsuuetoOtherQuantityInKg"  Text='<%# Eval("QtyInKg") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIsuuetoOtherQuantityInLtr"  Text='<%# Eval("QtyInLtr") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIsuuetoOtherFat_Per"  Text='<%# Eval("FAT") %>'  runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIsuuetoOtherSnf_Per" Text='<%# Eval("SNF") %>' ClientIDMode="Static" runat="server" onblur="SnfInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIsuuetoOtherFATInKg"  Text='<%# Eval("FatInKg") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIsuuetoOtherSnfInKg" Text='<%# Eval("SnfInKg") %>'  runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
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
                                                            <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Container">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoPowderPlantQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoPowderPlantQtyInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoPowderPlantFat_per"  runat="server" Text='<%# Eval("Fat_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoPowderPlantSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoPowderPlantFatInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoPowderPlantSnfInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                                                            <asp:Label ID="txtIssuetoCreamQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoCreamQtyInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoCreamFat_per"  runat="server" Text='<%# Eval("Fat_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoCreamSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoCreamFatInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoCreamSnfInKg" Enabled="true" ClientIDMode="Static" CssClass="form-control" runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                                                               
                                                                <asp:Label ID="lblType" runat="server"  Text='<%# Eval("Type") %>'></asp:Label>

                                                            </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoIceCreamQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoIceCreamQtyInLtr" runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoIceCreamFat_per"  runat="server" Text='<%# Eval("Fat_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoIceCreamSnf_Per"   runat="server" Text='<%# Eval("Snf_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoIceCreamFatInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtIssuetoIceCreamSnfInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvoutflowother" ShowFooter="true" runat="server" ClientIDMode="Static" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvoutflowother_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Particular">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Qty. In Kgs.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtoutflowotherQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtoutflowotherQtyInLtr"   runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtoutflowotherFat_per"   runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtoutflowotherSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtoutflowotherFatInKg"  runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtoutflowotherSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                                                            <asp:Label ID="txtotherQtyInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Qty. In Ltr.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtotherQtyInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtotherFat_per"  runat="server" Text='<%# Eval("FAT") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtotherSnf_Per"  runat="server" Text='<%# Eval("SNF") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtotherFatInKg"  runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtotherSnfInKg"  runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvClosingBalances"  ShowFooter="true" ClientIDMode="Static" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvClosingBalances_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Tanker" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClosingV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 
                                                    <asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtClosingBalancesQuantityInKg"  runat="server" Text='<%# Eval("QtyInKg") %>' onchange="GetQtyInLtr(this),FatInKg(this),SnfInKg(this)"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Qty. In Ltr." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtClosingBalancesQuantityInLtr"  runat="server" Text='<%# Eval("QtyInLtr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtClosingBalancesFat_Per"  runat="server" Text='<%# Eval("Fat_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtClosingBalancesSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtClosingBalancesFATInKg"  runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtClosingBalancesSnfInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvColdRoomBalances" ShowFooter="true" runat="server" CssClass="datatable table table-striped table-bordered table-hover" AutoGenerateColumns="false" OnRowCreated="gvColRoomBalances_RowCreated">
                                                <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Variant" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCRBItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                            <asp:Label ID="lblCRBItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                            <asp:Label ID="lblReturnPackagingSize" CssClass="hidden" runat="server" Text='<%# Eval("PackagingSize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 
                                                  
                                                    
                                                     
                                                       <asp:TemplateField HeaderText="No Of Packets" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesPackets"  runat="server" Text='<%# Eval("NoOfPackets") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													 <asp:TemplateField HeaderText="Qty. In Ltr." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesQuantityInLtr" runat="server" Text='<%# Eval("QtyInLtr") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													<asp:TemplateField HeaderText="Qty. In Kgs." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesQuantityInKg"  runat="server" Text='<%# Eval("QtyInKg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fat %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesFat_Per"  runat="server" Text='<%# Eval("Fat_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesSnf_Per"  runat="server" Text='<%# Eval("Snf_Per") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. Fat" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesFATInKg" runat="server" Text='<%# Eval("KgFat") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Kgs. SNF" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtColdRoomBalancesSnfInKg"  runat="server" Text='<%# Eval("KgSnf") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                    </div>           
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table dataTable table-bordered">
                                            <tr style="background-color: #ff874c; color: black">
                                                <td><b>TOTAL OUT FLOW</b></td>
                                               <%-- <td><b>Qty. In Ltr.</b><br />
                                                    <asp:Label ID="txtOutFlowTQtyInLtr" runat="server"></asp:Label></td>--%>
                                                <td><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="txtOutFlowTQtyInKg"  runat="server"></asp:Label></td>
                                                <td><b>Kgs. Fat<br />
                                                </b>
                                                    <asp:Label ID="txtOutFlowTFatInKg"  runat="server"></asp:Label></td>
                                                <td><b>Kgs. SNF<br />
                                                </b>
                                                    <asp:Label ID="txtOutFlowTSnfInKg" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
								<div class="row">
                                    <div class="col-md-12">
                                        <table class="table dataTable table-bordered">
                                            <tr style="background-color: #ff874c; color: black">
                                                <td><b>VARIATION</b></td>
                                                
                                                <td><b>Qty. In Kgs.</b><br />
                                                    <asp:Label ID="txtVariationTQtyInKg"  runat="server"></asp:Label></td>
                                                <td><b>Kgs. Fat<br />
                                                </b>
                                                    <asp:Label ID="txtVariationTFatInKg"  runat="server"></asp:Label></td>
                                                <td><b>Kgs. SNF<br />
                                                </b>
                                                    <asp:Label ID="txtVariationTSnfInKg"  runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
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
                                                                                        <asp:Label ID="txtBalance_BFRR" Enabled="false" Text='<%# Eval("RR_OpeningBalance_New") %>' oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Obtained" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_Obtained") %>' ID="txtRRObtained" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_Total") %>' ID="txtRRTotal" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Toning" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_Toning") %>' ID="txtRRToning" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Maintaining <br />S.N.F." HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_MaintainingSNF") %>' ID="txtRRMaintainingSNF" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Issued For<br />Product Section" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_IssuedForProductSection") %>' ID="txtRRIssueforproductionsection" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Total Issued" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_TotalIssued") %>' ID="txtRRTotalIssued" Enabled="false" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Closing <br />Balance" HeaderStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label Text='<%# Eval("RR_ClosingBalance") %>' ID="txtRRClosingBalance" oncopy="return false" onpaste="return false" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                            </div>
                        </div>
                            </fieldset>
                        </div>


                    </fieldset>
                </div>

            </div>


        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
