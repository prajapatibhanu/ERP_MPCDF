<%@ page title="" language="C#" masterpagefile="~/mis/MainMaster.master" autoeventwireup="true" codefile="MilkCollection_Home.aspx.cs" inherits="mis_mcms_reports_MilkCollection_Home" %>

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
                        <h4>
                            <asp:Label ID="lblHeading" runat="server"></asp:Label></h4>
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

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift</label>

                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">All</asp:ListItem>
                                        <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                        <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                    </asp:DropDownList>
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
                        <div class="col-md-12">
                            <asp:GridView runat="server" ID="gvDispatchEntry" OnRowCommand="gvDispatchEntry_RowCommand" Visible="false" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found.">
                                <Columns>
                                    <asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                    <asp:BoundField DataField="V_ChallanNo" HeaderText="Challan No." />
                                    <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />
                                    <asp:BoundField DataField="DT_DispatchDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Dispatch Time" />
                                    <asp:BoundField DataField="D_MilkQuality" HeaderText="Milk Type" />
                                    <asp:BoundField DataField="milkinkg" HeaderText="Milk Quantity (In KG)" />

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("Status").ToString() == "Received" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Adulterated Milk">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdulteratedMilk" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("AdulteratedMilkStatus").ToString() == "Yes" ? "label label-danger" : "label label-success" %>' Text='<%# Eval("AdulteratedMilkStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="More Info">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewMore" Style="padding: 3px; border-radius: 3px;" CommandArgument='<%# Eval("V_ChallanNo") %>' CommandName="ViewEntry" runat="server" Visible='<%# Eval("Status").ToString() == "Received" ? true : false %>' Text="View" CssClass="label label-warning"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <asp:GridView runat="server" ID="gvReceivedEntry" OnRowCommand="gvDispatchEntry_RowCommand" Visible="false" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found.">
                                <Columns>
                                    <asp:BoundField DataField="DT_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                    <asp:BoundField DataField="V_ChallanNo" HeaderText="Challan No." />
                                    <asp:BoundField DataField="V_VehicleNo" HeaderText="Vehicle No." />
                                    <asp:BoundField DataField="DT_ArrivalDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Arrival Time" />
                                    <asp:BoundField DataField="DT_DispatchDateTime" DataFormatString="{0:hh:mm tt}" HeaderText="Dispatch Time" />
                                    <asp:BoundField DataField="D_MilkQuality" HeaderText="Milk Type" />
                                    <asp:BoundField DataField="milkinkg" HeaderText="Milk Quantity (In KG)" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("Status").ToString() == "Received" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Adulterated Milk">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdulteratedMilk" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("AdulteratedMilk").ToString() == "Yes" ? "label label-danger" : "label label-success" %>' Text='<%# Eval("AdulteratedMilk") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="More Info">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" Style="padding: 3px; border-radius: 3px;" CommandArgument='<%# Eval("V_ChallanNo") %>' CommandName="ViewEntry" runat="server" Visible='<%# Eval("Status").ToString() == "Received" ? true : false %>' Text="View" CssClass="label label-warning"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>



                </div>
            </div>
            <div class="modal fade" id="pu_QCDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel"><span style="font-size: medium;">Challan No. :</span>
                                <asp:Label ID="lblChallanNo" ForeColor="White" Font-Size="Medium" Font-Bold="true" runat="server"></asp:Label>
                            </h5>
                            <button type="button" class="close pull-right" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                            </button>

                        </div>
                        <div class="clearfix"></div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <h5>Dispatched from DCS -
                                        <asp:Label runat="server" ID="lbldcsname"></asp:Label>
                                    </h5>
                                </div>
                                <div class="col-md-12 table-responsive-md">
                                    <asp:GridView ID="gvQCDetailsForCC" runat="server" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" CssClass="table table-bordered" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="NetMilkQtyInKG" HeaderText="Milk Quantity" />
                                            <asp:BoundField DataField="FAT" HeaderText="FAT %" />
                                            <asp:BoundField DataField="SNF" HeaderText="SNF %" />
                                            <asp:BoundField DataField="CLR" HeaderText="CLR" />
                                            <asp:BoundField DataField="V_Temp" HeaderText="Temp" />
                                            <asp:BoundField DataField="I_TotalCans" HeaderText="Total Can" />
                                            <asp:BoundField DataField="NetFATInKG" HeaderText="FAT (In Kg)" />
                                            <asp:BoundField DataField="NetSNFInKG" HeaderText="SNF (In Kg)" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-12 text-center">
                                    <h5 style="margin-top: 10px;">Received at CC / BMC -
                                        <asp:Label runat="server" ID="lblccbmcname"></asp:Label></h5>
                                </div>
                                <div class="col-md-12 table-responsive-md">
                                    <asp:GridView ID="gvQCDetailsForDS" runat="server" CssClass="table table-bordered" Width="100%" AutoGenerateColumns="false" EmptyDataRowStyle-ForeColor="Red" EmptyDataText="No Data Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NetMilkQtyInKG" HeaderText="Milk Quantity" />
                                            <asp:BoundField DataField="FAT" HeaderText="FAT %" />
                                            <asp:BoundField DataField="SNF" HeaderText="SNF %" />
                                            <asp:BoundField DataField="CLR" HeaderText="CLR" />
                                            <asp:BoundField DataField="V_Temp" HeaderText="Temp" />
                                            <asp:BoundField DataField="I_TotalCans" HeaderText="Total Can" />
                                            <asp:BoundField DataField="NetFATInKG" HeaderText="FAT (In Kg)" />
                                            <asp:BoundField DataField="NetSNFInKG" HeaderText="SNF (In Kg)" />
                                        </Columns>
                                    </asp:GridView>


                                    <div class="col-md-12" runat="server" id="milktestdetail" visible="false">
                                        <fieldset>
                                            <legend>Adulteration Test Details
                                            </legend>
                                            <i style="color: red; font-size: 12px;"><asp:Label ID="lblAdulterationMilkRemarks" runat="server"></asp:Label></i>
                                            <hr />
                                            <div class="form-group table-responsive">
                                                <asp:GridView ID="gvmilkAdulterationtestdetail" ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Chamber Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSealLocation" runat="server" Text='<%# Eval("V_SealLocation").ToString() == "F" ? "Front" : Eval("V_SealLocation").ToString() == "S" ? "Single" : "Rear" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Adulteration Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdulterationType" runat="server" Text='<%# Eval("V_AdulterationType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Adulteration Value">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_AdulterationValue" runat="server" Text='<%# Eval("V_AdulterationValue") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                       
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

