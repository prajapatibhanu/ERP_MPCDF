<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkTestingRequestToQCHourlyTesting.aspx.cs" Inherits="mis_dailyplan_MilkTestingRequestToQCHourlyTesting"  MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
                  display: none;
              }
          @media print {
             
              .noprint {
                display: none;
            }
              .NonPrintable {
                  display: block;
              }  
          }

    </style>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <%--ConfirmationModal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>

    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success noprint">

                <div class="box-header">
                    <h3 class="box-title">Hourly Milk/Product Testing</h3>
                </div>
                <div class="box-body">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                    <fieldset runat="server">
                        <legend>Hourly Milk/Product Testing</legend>
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

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlShift" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
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

                                    <div class="col-md-3">
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

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Test Request For<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlmilktestrequestfor" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Test Request For!'></i>"
                                                    ErrorMessage="Select Test Request For." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlmilktestrequestfor" AutoPostBack="true" OnSelectedIndexChanged="ddlmilktestrequestfor_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3" id="divVariant" runat="server">
                                        <div class="form-group">
                                            <label>Variant<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvVariant" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlVariant" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Variant!'></i>"
                                                    ErrorMessage="Select Test Request For." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlVariant"  runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlVariant_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label>Sample Name/No<span style="color: red;"> *</span></label>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    ErrorMessage="Enter Sample Name/No" Text="<i class='fa fa-exclamation-circle' title='Enter Sample Name/No !'></i>"
                                                    ControlToValidate="txtSampleName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSampleName" placeholder="Enter Sample Name/No"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label>Sample Quantity<span style="color: red;"> *</span></label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                    ErrorMessage="Enter Sample Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Sample Quantity !'></i>"
                                                    ControlToValidate="txtSampleQuantity" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev1" ControlToValidate="txtSampleQuantity" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                            </span>--%>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSampleQuantity" placeholder="Enter Sample Quantity"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-md-3 hidden">
                                        <div class="form-group">
                                            <label>Unit<span style="color: red;">*</span></label>
                                           <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlUnit" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Section!'></i>"
                                                    ErrorMessage="Select Unit." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>--%>
                                            <asp:DropDownList ID="ddlUnit" OnInit="ddlUnit_Init" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sample Batch No</label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Sample Batch No" Text="<i class='fa fa-exclamation-circle' title='Enter Sample Batch No !'></i>"
                                                    ControlToValidate="txtSampleBatch_No" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSampleBatch_No" placeholder="Enter Sample Batch No."></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sample Lot No</label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a"
                                                    ErrorMessage="Enter Sample Lot No" Text="<i class='fa fa-exclamation-circle' title='Enter Sample Lot No !'></i>"
                                                    ControlToValidate="txtSampleLot_No" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSampleLot_No" placeholder="Enter Sample Lot No."></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            

                        </div>
                        <asp:Panel ID="panelMachineHead" runat="server" Visible="false">
                            <fieldset>
                                <legend>Machine Head Details</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Machine Name<span style="color: red;"> *</span></label>
                                           <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvdMachineList" runat="server" Display="Dynamic"  ControlToValidate="ddlMachineList" Text="<i class='fa fa-exclamation-circle' title='Select Machine Name!'></i>" ErrorMessage="Select Machine Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </span>
                                           <%-- <asp:DropDownList ID="ddlMachine" runat="server" OnSelectedIndexChanged="ddlMachine_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2"></asp:DropDownList>--%>
                                            <asp:ListBox runat="server" ID="ddlMachineList" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-2 hidden">
                                        <div class="form-group">
                                            <label>Machine Head<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvddlMachineHead" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlMachineHead" Text="<i class='fa fa-exclamation-circle' title='Select Head Name!'></i>" ErrorMessage="Select Head Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </span>
                                            <asp:DropDownList ID="ddlMachineHead" runat="server" CssClass="form-control select2" ></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 hidden">
                                        <div class="form-group">
                                            <label>Quantity<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvtxtQuantity" runat="server" Display="Dynamic"  ControlToValidate="txtQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </span>
                                            <asp:TextBox ID="txtQuantity" autocomplete="off" Maxlength="20" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnAdd" runat="server" style="margin-top:22px;" OnClick="btnAdd_Click" ValidationGroup="Add" CssClass="btn btn-primary" Text="Add" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvMachineHeadDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvMachineHeadDetails_RowCommand">
                                           <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                            </ItemTemplate>   
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Machine Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMachineName"  runat="server" Text='<%# Eval("Machine_Name") %>' ></asp:Label>
                                                <asp:Label ID="lblMachine_ID" CssClass="hidden" runat="server" Text='<%# Eval("Machine_ID") %>' ></asp:Label>
                                            </ItemTemplate>   
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Head Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeadName" runat="server" Text='<%# Eval("Head_Name") %>' ></asp:Label>
                                                <asp:Label ID="lblHead_ID" CssClass="hidden" runat="server" Text='<%# Eval("Head_ID") %>' ></asp:Label>
                                            </ItemTemplate>   
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvtxtQuantity" runat="server" Display="Dynamic"  ControlToValidate="txtQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                    </span>
                                                <asp:TextBox ID="txtQuantity" autocomplete="off" MaxLength="12" onkeypress="return validateDec(this,event)" runat="server" Text=""></asp:TextBox>
                                                
                                            </ItemTemplate>   
                                        </asp:TemplateField>
                                      <%--  <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server"  CommandName="DeleteRow" CommandArgument='<%# Eval("Head_ID") %>' OnClientClick="return confirm('Do you really want To delete Record?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>   
                                        </asp:TemplateField>--%>
                                    </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                        <fieldset>
                            <legend>Test Parameter Details</legend>
                            <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                  
                                    <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" ShowFooter="true"
                                        EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                        AutoGenerateColumns="False" AllowPaging="False" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="QCParameterID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("QCParameterID").ToString()%>' runat="server" />
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

                                            <asp:TemplateField HeaderText="Value/Option">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinValue" Visible="false" Text='<%# Eval("MinValueF").ToString() %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblMaxValue" Visible="false" Text='<%# Eval("MaxValueF").ToString() %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblOptionDetails" Visible="false" Text='<%# Eval("OptionDetails").ToString()%>' runat="server"></asp:Label>

                                                    <asp:Label ID="lblValueRang" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Test Result" HeaderStyle-Width="20%">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtvalue" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtvalue_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:DropDownList ID="ddloption" Visible="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        </fieldset>
                        
                       
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="txtRemarks_R" TextMode="MultiLine" runat="server" placeholder="Enter Remarks..." class="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                           
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4"></div>
                                <div class="col-md-1">
                                    <asp:LinkButton ID="lbGCheckResult" CssClass="btn btn-success" OnClick="lbGCheckResult_Click" runat="server">Check Result</asp:LinkButton>
                                </div>
                                
                                <div class="col-md-1">
                                    <div class="form-group" style="padding-left: 20px;">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-primary" Text="Save Result" />
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                                <div class="col-md-2"></div>
                            </div>
                        </div>


                    </fieldset>


                </div>

            </div>

            <fieldset runat="server">
                <legend>Testing Request Details</legend>

                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-3 noprint">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFilterOffice" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 noprint">
                            <div class="input-group">
                                <label>Filter Date<span class="text-danger">*</span></label>
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <i class="far fa-calendar-alt"></i>
                                    </span>
                                </div>
                                <asp:TextBox ID="txtFilterDT" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtFilterDT_TextChanged"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                      
                        <div class="row">
                            <div class="col-md-12 pull-right noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                <asp:Button ID="btnPrint" runat="server"  Visible="false" CssClass="btn btn-default" Text="Print" OnClientClick="window.print();"/>
                                <asp:Button ID="btnShowntoplant" runat="server"  Visible="false" CssClass="btn btn-primary" Text="Shown To Plant" OnClick="btnShowntoplant_Click"/>
                            </div>      
                            </div>
                            <div class="col-md-12">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys GridView3"
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

                                           <%-- <asp:TemplateField HeaderText="Sample Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSampleQuantity" Text='<%# Eval("SampleQuantity").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUQCCode" Text='<%# Eval("UQCCode").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

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

                                           <%-- <asp:TemplateField HeaderText="Request Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestStatus" CssClass="noprint" Text='<%# Eval("RequestStatus").ToString()%>' runat="server"></asp:Label>
                                                    <asp:Label ID="Label6"  CssClass="NonPrintable" Text='<%# Eval("TestRequest_Status").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Result Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" CssClass="noprint" Text='<%# Eval("ResultStatus").ToString()%>' runat="server"></asp:Label>
                                                    <asp:Label ID="Label7" CssClass="NonPrintable" Text='<%# Eval("TestRequest_Result_Status").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbFilterReq"  OnClick="lbFilterReq_Click" CommandArgument='<%# Eval("TestRequest_ID").ToString()%>' Visible='<%# Eval("TestRequest_Result").ToString() == "Declared" ? true : false %>' runat="server" CssClass="label label-default" CausesValidation="False" Text="View Result"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Head Details"  HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblHeadFilterReq" OnClick="lblHeadFilterReq_Click"  CommandArgument='<%# Eval("TestRequest_ID").ToString()%>' Visible='<%# Eval("ProductSection_ID").ToString() == "9" ? true : false %>' runat="server" CssClass="label label-default" CausesValidation="False" Text="View Head Details"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <HeaderTemplate>
                                                   <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static"/>
                                                    Shown To Plant
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("showntoplant").ToString()=="1"?true:false %>' Enabled='<%# Eval("showntoplant").ToString()=="0"?true:false %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GridView5" Visible="false" runat="server" ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                        AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="TestRequest_ID" OnRowDataBound="GridView5_RowDataBound">
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

                                           <%-- <asp:TemplateField HeaderText="Request Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestStatus" CssClass="noprint" Text='<%# Eval("RequestStatus").ToString()%>' runat="server"></asp:Label>
                                                    <asp:Label ID="Label6"  CssClass="NonPrintable" Text='<%# Eval("TestRequest_Status").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Result Status">
                                                <ItemTemplate>
                                                  
                                                    <asp:Label ID="Label7" CssClass="NonPrintable" Text='<%# Eval("TestRequest_Result_Status").ToString()%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Result Detail"  HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <ItemTemplate>
                                                   <asp:Panel ID="pnlResult" runat="server" Style="display: block" Width="100%"><span class="HideRecord">(As Per Details)</span>
                                                    <asp:GridView ID="GvResultDetail"  runat="server" ShowHeaderWhenEmpty="true"
                                                EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                                AutoGenerateColumns="False" AllowPaging="False" OnRowDataBound="GvResultDetail_RowDataBound">
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
                                                    
                                                </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Heads Detail"  HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                                <ItemTemplate>
                                                   <asp:Panel ID="pnlHeadDetail" runat="server" Style="display: block" Width="100%"><span class="HideRecord">(As Per Details)</span>
                                                    <asp:GridView ID="GvHeadDetail" runat="server" ShowHeaderWhenEmpty="true" 
                                                EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                                AutoGenerateColumns="False" AllowPaging="False" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRNo" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Machine Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMachineName" Text='<%# Eval("Machine_Name").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Head Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHeadName" Text='<%# Eval("Head_Name").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" Text='<%# Eval("Quantity").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   

                                                </Columns>
                                            </asp:GridView>
                                                    
                                                </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </fieldset>


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
                            </div>

                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>

            <div class="modal" id="ViewHeadDetail">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="height: 500px;">
                            <div class="modal-header">
                                <asp:LinkButton runat="server" class="close" ID="LinkButton2" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                                <h4 class="modal-title">Request No :
                                <asp:Label ID="Label8" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp;
                               Request For :
                                <asp:Label ID="Label9" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp; 
                               Batch No : 
                                <asp:Label ID="Label10" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp;
                               Lot No :
                                <asp:Label ID="Label11" Font-Bold="true" runat="server"></asp:Label>

                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="height: 350px; overflow: scroll;">
                                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="true" ShowFooter="true"
                                                EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                                AutoGenerateColumns="False" AllowPaging="False" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Machine Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMachineName" Text='<%# Eval("Machine_Name").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Head Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHeadName" Text='<%# Eval("Head_Name").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" Text='<%# Eval("Quantity").ToString()%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    
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

    <script>

        function ViewResultParameterModal() {
            $("#ViewResultModalDetail").modal('show');

        }
        function ViewHeadDetailModal() {
            $("#ViewHeadDetail").modal('show');

        }

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            debugger;
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save Result") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }

            }
        }

    </script>
    <script>

        $(function () {
            $('[id*=ddlMachineList]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    
   <%-- <script src="../../../mis/js/jquery.js" type="text/javascript"></script>--%>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
<script type="text/javascript">
       $('#checkAll').click(function () {

            var inputList = document.querySelectorAll('.GridView3 tbody input[type="checkbox"]:not(:disabled)');

            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;

                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


    </script>
</asp:Content>

