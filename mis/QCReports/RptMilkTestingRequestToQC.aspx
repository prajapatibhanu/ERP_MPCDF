<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptMilkTestingRequestToQC.aspx.cs" Inherits="mis_QCReports_RptMilkTestingRequestToQC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">

                <div class="box-header">
                    <h3 class="box-title">Milk Testing Request To QC FeedBack Report</h3>
                </div>
                <div class="box-body">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset runat="server">
                                <legend>Milk Testing Request To QC</legend>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group table-responsive">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                            ControlToValidate="ddlDS" InitialValue="0"
                                                            Text="<i class='fa fa-exclamation-circle' title='Select DS!'></i>"
                                                            ErrorMessage="Select DS." SetFocusOnError="true" ForeColor="Red"
                                                            ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Date</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Shift</label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlShift" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Production Section<span class="text-danger">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                            ControlToValidate="ddlPSection" InitialValue="0"
                                                            Text="<i class='fa fa-exclamation-circle' title='Select Section!'></i>"
                                                            ErrorMessage="Select Section." SetFocusOnError="true" ForeColor="Red"
                                                            ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged"
                                                        runat="server" CssClass="form-control select2">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Test Request Type<span style="color: red;">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                            ControlToValidate="ddlTestRequestType" InitialValue="0"
                                                            Text="<i class='fa fa-exclamation-circle' title='Select Test Request Type!'></i>"
                                                            ErrorMessage="Select Test Request Type." SetFocusOnError="true" ForeColor="Red"
                                                            ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlTestRequestType" AutoPostBack="true" OnSelectedIndexChanged="ddlTestRequestType_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <hr />

                                <div class="table table-responsive">
                                    <asp:GridView ID="gvtestingrequestdetail" runat="server" ShowHeaderWhenEmpty="true"
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

                                            <%-- <asp:TemplateField HeaderText="Section Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductSection_Name" Text='<%# Eval("ProductSection_Name").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

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
                                                     <asp:Label ID="lblItemType_id"  CssClass="hidden" Text='<%# Eval("ItemType_id").ToString()%>' runat="server"></asp:Label>
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

                                            

                                            <asp:TemplateField HeaderText="Submit Test Result">
                                                <ItemTemplate>                                                 
                                                    <asp:LinkButton ID="lblviewresult" OnClick="lblviewresult_Click" CommandArgument='<%# Eval("TestRequest_ID").ToString()%>' Visible='<%# Eval("TestRequest_Result").ToString() == "Declared" ? true : false %>' runat="server" CssClass="label label-default" CausesValidation="False" Text="View Result"></asp:LinkButton>
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

            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

           
            <div class="modal" id="ViewResultModalDetail">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="height: 500px;">
                            <div class="modal-header">
                                <asp:LinkButton runat="server" class="close" ID="LinkButton1" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
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
                                                AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="TestRequestResult_ID" OnRowDataBound="GridView2_RowDataBound">
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
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtRR"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Remarks!'></i>"
                                                        ErrorMessage="Enter Test Remarks." SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save"></asp:RequiredFieldValidator>
                                                </span>
                                                <label>Remarks<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtRR" Enabled="false" TextMode="MultiLine" runat="server" placeholder="Enter Remarks..." class="form-control" MaxLength="200"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- /.modal-content -->
                    </div>
                </div>
                <!-- /.modal-dialog -->
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function ViewResultParameterModal() {
            $("#ViewResultModalDetail").modal('show');

        }
    </script>
</asp:Content>

