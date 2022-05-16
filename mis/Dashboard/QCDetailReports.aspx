<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="QCDetailReports.aspx.cs" Inherits="mis_Dashboard_QCDetailReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script>

        function ViewResultParameterModal() {
            $("#ViewResultModalDetail").modal('show');

        }</script>
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">E-Chaalan</h3>
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <fieldset>
                        <legend>E-Chaalan Details report</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-danger" Style="margin-top: 19px;" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                        AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="TestRequest_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("TestRequest_ID").ToString()%>' runat="server" />
                                                    <asp:Label ID="lblTestRequestFor_ID" Visible="false" Text='<%# Eval("TestRequestFor_ID").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" />

                                            <asp:TemplateField HeaderText="Request DateTime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestRequest_DT" runat="server" Text='<%# (Convert.ToDateTime(Eval("TestRequest_DT"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductSection_Name" Text='<%# Eval("ProductSection_Name").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Request No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestRequest_No" Text='<%# Eval("TestRequest_No").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Request Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestRequestType" Text='<%# Eval("TestRequestType").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Request For">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestRequestFor" Text='<%# Eval("TestRequestFor").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sample Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleQuantity" Text='<%# Eval("SampleQuantity").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUQCCode" Text='<%# Eval("UQCCode").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Batch No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleBatch_No" Text='<%# Eval("SampleBatch_No").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lot No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleLot_No" Text='<%# Eval("SampleLot_No").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Request Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestStatus" Text='<%# Eval("RequestStatus").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Result Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" Text='<%# Eval("ResultStatus").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbFilterReq" OnClick="lbFilterReq_Click" CommandArgument='<%# Eval("TestRequest_ID").ToString()%>' Visible='<%# Eval("TestRequest_Result").ToString() == "Declared" ? true : false %>' runat="server" CssClass="label label-default" CausesValidation="False" Text="View Result"></asp:LinkButton>
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
            <div class="modal" id="ViewResultModalDetail">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="height: 500px;">
                            <div class="modal-header">

                                <h4 class="modal-title">Request No :
                                <asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp;
                               Request For :
                                <asp:Label ID="Label3" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp; 
                               Batch No : 
                                <asp:Label ID="Label4" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp;
                               Lot No :
                                <asp:Label ID="Label5" Font-Bold="true" runat="server"></asp:Label>

                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="height: 350px; overflow: scroll;">
                                    <asp:Label ID="lblmpR" runat="server" Text=""></asp:Label>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="true" ShowFooter="true"
                                                EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                                AutoGenerateColumns="False" AllowPaging="False" OnRowDataBound="GridView2_RowDataBound" DataKeyNames="TestRequestResult_ID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("TestRequestResult_ID").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Parameter Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCParameterName" Text='<%# Eval("QCParameterName").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Calculation Method" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCalculationMethod" Text='<%# Eval("CalculationMethod").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Expected Result">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMinValue" Visible="false" Text='<%# Eval("MinValueF").ToString() %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblMaxValue" Visible="false" Text='<%# Eval("MaxValueF").ToString() %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblOptionDetails" Visible="false" Text='<%# Eval("OptionDetails").ToString()%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblValueRang" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Outcome" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTestFinalResult" Text='<%# Eval("TestFinalResult").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Result" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTestRequest_Result" Text='<%# Eval("TestRequest_Result").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Result DateTime" HeaderStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTestRequestResult_DT" Text='<%# (Convert.ToDateTime(Eval("TestRequestResult_DT"))).ToString("dd-MM-yyyy hh:mm:ss tt") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Remarks<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtRR" Enabled="false" TextMode="MultiLine" runat="server" placeholder="Enter Remarks..." class="form-control" MaxLength="200"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                    <%-- <asp:Button runat="server" ID="Button1" class="btn btn-success" Text="Save"></asp:Button>--%>
                                </div>
                            </div>

                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

