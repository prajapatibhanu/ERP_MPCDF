<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="mcms-home_FilledTanker.aspx.cs" Inherits="mis_MilkCollection_mcms_home_FilledTanker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-Manish">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Reports</h3>
                    <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                </div>
                <!-- /.card-header -->
                <div class="box-body">
                    <div class="row">
                        <h4>Dispatch Tanker Details</h4>
                        <br />
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date</label><span style="color: red;"> *</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="glyphicon glyphicon-calendar"></i>
                                    </span>
                                    <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date</label><span style="color: red;"> *</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <i class="glyphicon glyphicon-calendar"></i>
                                    </span>
                                    <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" Style="margin-top: 20px;" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="Save" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 pull-right noprint">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click" />
                                        </div>
                                    </div>
                        <div class="col-md-12">
                            <asp:GridView runat="server" ID="gvDispatchEntry" Visible="false" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found." OnRowCommand="gvDispatchEntry_RowCommand" OnRowDataBound="gvDispatchEntry_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                    <asp:BoundField DataField="V_ReferenceCode" HeaderText="Reference No." />
                                    <asp:BoundField DataField="ChallanNo" HeaderText="Challan No." />
                                    <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />
                                    <asp:BoundField DataField="DT_ArrivalDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Arrival Time" />
                                    <asp:BoundField DataField="DT_DispatchDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Dispatch Time" />
                                    <asp:BoundField DataField="V_MilkType" HeaderText="Milk Type" />
                                    <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity (In KG)" />
									<asp:BoundField DataField="V_SealLocation" HeaderText="Chamber Type" />
                                    <%--<asp:BoundField DataField="TotalSeal" HeaderText="Total Seals" />--%>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("Status").ToString() == "Received" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="More Info">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewMore" Style="padding: 3px; border-radius: 3px;" CommandArgument='<%# Eval("ChallanNo") %>' CommandName="ViewEntry" runat="server" Visible='<%# Eval("Status").ToString() == "Received" ? true : false %>' Text="View" CssClass="label label-warning"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									 <asp:TemplateField HeaderText="More Info" Visible="false">
                                        <ItemTemplate>
                                            <asp:Panel ID="panel1" runat="server">
                                                <label>Dispatched From Dugdh Sangh</label>
                                                <asp:GridView ID="gridview2" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seal Location">
                                                <ItemTemplate>
                                                    <%# Eval("V_SealLocation").ToString() == "S" ? "Single" : Eval("V_SealLocation").ToString() == "F" ? "Front" : "Rear" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity" />
                                            <asp:BoundField DataField="D_FAT" HeaderText="FAT %" />
                                            <asp:BoundField DataField="D_SNF" HeaderText="SNF %" />
                                            <asp:BoundField DataField="D_CLR" HeaderText="CLR" />
                                            <asp:BoundField DataField="V_Temp" HeaderText="Temp" />
                                            <asp:BoundField DataField="V_Acidity" HeaderText="Acidity" />
                                            <asp:BoundField DataField="V_COB" HeaderText="COB" />
                                            <asp:BoundField DataField="V_Alcohol" HeaderText="Alcohol %" />
                                            <asp:BoundField DataField="V_MBRT" HeaderText="MBRT" />
                                            <asp:BoundField DataField="V_MilkQuality" HeaderText="Milk Quality" />
                                           
                                        </Columns>
                                    </asp:GridView>
                                                
                                                <label>Recieved at Chilling Centre</label>
                                                <asp:GridView ID="gridview1" runat="server" CssClass="table table-bordered" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seal Location">
                                                <ItemTemplate>
                                                    <%# Eval("V_SealLocation").ToString() == "S" ? "Single" : Eval("V_SealLocation").ToString() == "F" ? "Front" : "Rear" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity" />
                                            <asp:BoundField DataField="D_FAT" HeaderText="FAT %" />
                                            <asp:BoundField DataField="D_SNF" HeaderText="SNF %" />
                                            <asp:BoundField DataField="D_CLR" HeaderText="CLR" />
                                            <asp:BoundField DataField="V_Temp" HeaderText="Temp" />
                                            <asp:BoundField DataField="V_Acidity" HeaderText="Acidity" />
                                            <asp:BoundField DataField="V_COB" HeaderText="COB" />
                                            <asp:BoundField DataField="V_Alcohol" HeaderText="Alcohol %" />
                                            <asp:BoundField DataField="V_MBRT" HeaderText="MBRT" />
                                            <asp:BoundField DataField="V_MilkQuality" HeaderText="Milk Quality" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									 <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <a href='../MilkCollection/MilkFilledTankerChallanDetails.aspx?Cid=<%# new APIProcedure().Encrypt(Eval("I_EntryID").ToString()) %>' target="_blank" title="Print Gate Pass"><i class="fa fa-print"></i></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									
									
                                </Columns>
                            </asp:GridView>
                            <asp:GridView runat="server" ID="gvReceivedEntry" Visible="false" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found." OnRowCommand="gvDispatchEntry_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                    <%--<asp:BoundField DataField="Office_Name" HeaderText="Office Name" />--%>
                                   
                                    <asp:BoundField DataField="V_ReferenceCode" HeaderText="Reference No." />
                                    <asp:BoundField DataField="ChallanNo" HeaderText="Challan No." />
                                     <asp:BoundField DataField="CC_Name" HeaderText="CC Name" />
                                    <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />

                                    <asp:BoundField DataField="DT_ArrivalDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Arrival Time" />
                                    <asp:BoundField DataField="V_MilkType" HeaderText="Milk Type" />
                                    <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity (In KG)" />
									<asp:BoundField DataField="V_SealLocation" HeaderText="Chamber Type" />
                                    <%--<asp:BoundField DataField="TotalSeal" HeaderText="Total Seals" />--%>
                                    <asp:TemplateField HeaderText="More Info">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" Style="padding: 3px; border-radius: 3px;" CommandArgument='<%# Eval("ChallanNo") %>' CommandName="ViewEntry" runat="server"  Text="View" CssClass="label label-warning"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <a href='../MilkCollection/MilkFilledTankerChallanDetails.aspx?Cid=<%# new APIProcedure().Encrypt(Eval("I_EntryID").ToString()) %>' target="_blank" title="Print Gate Pass"><i class="fa fa-print"></i></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    
                </div>
            </div>

            <%--Confirmation Modal Start --%>
            <div class="modal fade" id="pu_QCDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel"><span style="font-size: medium;">Challan No. :</span>
                                <asp:Label ID="lblVehicleNo" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>
                            </h5>
                            <button type="button" class="close pull-right" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                            </button>

                        </div>
                        <div class="clearfix"></div>
                        <div class="card-body">
                            <div class="row">
                               
                                <div class="col-md-12 text-center">
                                    <h5 style="margin-top: 10px;">Dispatched From Dugdh Sangh</h5>
                                </div>
                                <div class="col-md-12 table-responsive-md">
                                    <asp:GridView ID="gvQCDetailsForDS" runat="server" CssClass="table table-bordered" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seal Location">
                                                <ItemTemplate>
                                                    <%# Eval("V_SealLocation").ToString() == "S" ? "Single" : Eval("V_SealLocation").ToString() == "F" ? "Front" : "Rear" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity" />
                                            <asp:BoundField DataField="D_FAT" HeaderText="FAT %" />
                                            <asp:BoundField DataField="D_SNF" HeaderText="SNF %" />
                                            <asp:BoundField DataField="D_CLR" HeaderText="CLR" />
                                            <asp:BoundField DataField="V_Temp" HeaderText="Temp" />
                                            <asp:BoundField DataField="V_Acidity" HeaderText="Acidity" />
                                            <asp:BoundField DataField="V_COB" HeaderText="COB" />
                                            <asp:BoundField DataField="V_Alcohol" HeaderText="Alcohol %" />
                                            <asp:BoundField DataField="V_MBRT" HeaderText="MBRT" />
                                            <asp:BoundField DataField="V_MilkQuality" HeaderText="Milk Quality" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                 <div class="col-md-12 text-center">
                                    <h5>Recieved at Chilling Centre</h5>
                                </div>
                                <div class="col-md-12 table-responsive-md">
                                    <asp:GridView ID="gvQCDetailsForCC" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seal Location">
                                                <ItemTemplate>
                                                    <%# Eval("V_SealLocation").ToString() == "S" ? "Single" : Eval("V_SealLocation").ToString() == "F" ? "Front" : "Rear" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="I_MilkQuantity" HeaderText="Milk Quantity" />
                                            <asp:BoundField DataField="D_FAT" HeaderText="FAT %" />
                                            <asp:BoundField DataField="D_SNF" HeaderText="SNF %" />
                                            <asp:BoundField DataField="D_CLR" HeaderText="CLR" />
                                            <asp:BoundField DataField="V_Temp" HeaderText="Temp" />
                                            <asp:BoundField DataField="V_Acidity" HeaderText="Acidity" />
                                            <asp:BoundField DataField="V_COB" HeaderText="COB" />
                                            <asp:BoundField DataField="V_Alcohol" HeaderText="Alcohol %" />
                                            <asp:BoundField DataField="V_MBRT" HeaderText="MBRT" />
                                            <asp:BoundField DataField="V_MilkQuality" HeaderText="Milk Quality" />
                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <%--ConfirmationModal End --%>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowQCDetails() {
            $('#pu_QCDetails').modal('show');
            return false;
        }
    </script>
</asp:Content>

