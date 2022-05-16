<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DateWiseSocietyMilkCollectionStatus.aspx.cs" Inherits="mis_MilkCollection_DateWiseSocietyMilkCollectionStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {
            .no-print {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">CC Wise Producer Milk Collection Status</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BMC Root</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvddlBMCTankerRootName" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                    ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>--%>
                                     <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                   <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>--%>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                                   <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift</label>

                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Report</legend>
                                <div class="row">
                                    <div class="col-md-3 pull-left noprint">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click" />
                                        </div>
                                    </div>
									<div class="col-md-12 pull-left">
                                        <div class="form-group">
                                            <asp:Label ID="lblCount" Font-Bold="true" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvReports" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="gvReports_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                              
                                                    <asp:TemplateField HeaderText="BMC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBMC" Text='<%# Eval("Office_Name") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Producers">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProducerCount" Text='<%# Eval("ProducerCount") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Producer Count(Milk Collection)">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnProducerCount" CommandName="ViewProducer" Enabled='<%# Eval("PCOUNT").ToString()=="0"?false:true %>' Text='<%# Eval("PCOUNT") %>' runat="server" CommandArgument='<%# Eval("Office_ID") %>'></asp:LinkButton>
                                                           <%-- <asp:Label ID="lblMilkCollection" Text='<%# Eval("MilkCollectionCount") %>' runat="server"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Collection Count">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnMilkCollection" CommandName="ViewRecord" Enabled='<%# Eval("MilkCollectionCount").ToString()=="0"?false:true %>' Text='<%# Eval("MilkCollectionCount") %>' runat="server" CommandArgument='<%# Eval("Office_ID") %>'></asp:LinkButton>
                                                           <%-- <asp:Label ID="lblMilkCollection" Text='<%# Eval("MilkCollectionCount") %>' runat="server"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Milk Qty(In Lt.)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQtyTotal" Text='<%# Eval("QtyTotal") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="modal fade" id="MilkCollectionFromProducerDetail" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Milk Collection Details<br /><span id="spnOfficeName" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="spnDate" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="spnShift" runat="server"></span></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ShowFooter="true" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVDetails" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Dt_Date" HeaderText="Date" />
                                        <asp:BoundField DataField="V_Shift" HeaderText="Shift" />
                                        <asp:BoundField DataField="ProducerName" HeaderText="Producer Name"  />
                                         <asp:BoundField DataField="V_MilkType" HeaderText="Milk Type" />
                                        <asp:BoundField DataField="I_MilkSupplyQty" HeaderText="Quantity(In Lt)"  />
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                     <asp:Button ID="btnModalExport" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnModalExport_Click" />
                </div>
            </div>
        </div>
    </div>
        <div class="modal fade" id="ProducerDetail" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Producer Details<br /><span id="spnOfficeName_P" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="spnDate_P" runat="server"></span></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="gvProducerDetails" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProducerName" HeaderText="Producer Name" />
                                        
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowModalDetail() {
            $('#MilkCollectionFromProducerDetail').modal('show');
        }
        function ProducerDetail() {
            $('#ProducerDetail').modal('show');
        }
    </script>
</asp:Content>

