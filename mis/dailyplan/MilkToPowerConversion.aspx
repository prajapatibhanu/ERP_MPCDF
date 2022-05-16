<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkToPowerConversion.aspx.cs" Inherits="mis_dailyplan_MilkToPowerConversion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Milk To Power Conversion</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">


                    <fieldset>
                        <legend>Milk To Power Conversion Office</legend>
                        <div class="row">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDugdhSangh" runat="server" Display="Dynamic" ControlToValidate="ddlDS" Text="<i class='fa fa-exclamation-circle' title='Select Dugdh Sangh!'></i>" ErrorMessage="Select Dugdh Sangh" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlDS" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Date<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="rbtnTransferType" runat="server" RepeatDirection="Horizontal" Style="margin-top: 20px;" OnSelectedIndexChanged="rbtnsaleto_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Union To Union&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">Union To Third Party </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="col-md-2" id="union" runat="server">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblUnionType" runat="server"></asp:Label><span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvddlUnion" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlUnion" Text="<i class='fa fa-exclamation-circle' title='Select Union!'></i>" ErrorMessage="Select Union" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlUnion" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlUnion" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>Dispatch</legend>
                        <div class="row">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Milk Type<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlmilktype" Text="<i class='fa fa-exclamation-circle' title='Select Milk Type!'></i>" ErrorMessage="Select Milk Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlmilktype" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">FCM</asp:ListItem>
                                            <asp:ListItem Value="2">Whole</asp:ListItem>
                                            <asp:ListItem Value="3">STD</asp:ListItem>
                                            <asp:ListItem Value="4">Skimmed Milk</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMt1" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Quantity In KG<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" Display="Dynamic" ControlToValidate="txtMilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtMilkQuantity" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>FAT In KG<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFAT" runat="server" Display="Dynamic" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Enter FAT!'></i>" ErrorMessage="Enter FAT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtFAT" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>SNF In KG<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvSNF" runat="server" Display="Dynamic" ControlToValidate="txtSNF" Text="<i class='fa fa-exclamation-circle' title='Enter SNF!'></i>" ErrorMessage="Enter SNF" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtSNF" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Milk Value<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtMilkValue" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Value!'></i>" ErrorMessage="Enter Value" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtMilkValue" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </fieldset>



                    <fieldset>
                        <legend>To Be SMP Recovered</legend>
                        <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Recovery (in %)<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtrecoveryPer" Text="<i class='fa fa-exclamation-circle' title='Enter Recovery %!'></i>" ErrorMessage="Enter Recovery %" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtrecoveryPer" MaxLength="3" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Recovered(Approx Quantity In M.T.)<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtRecoveredApproxQty" Text="<i class='fa fa-exclamation-circle' title='Enter Recovered Approx Quantity!'></i>" ErrorMessage="Enter SMP Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtRecoveredApproxQty" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>SMP Value<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtsmpvalue" Text="<i class='fa fa-exclamation-circle' title='Enter SMP Value!'></i>" ErrorMessage="Enter SMP Value" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtsmpvalue" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="Save" OnClientClick="return ValidatePage();" Text="Save" />
                                </div>
                            </div>
                            <div class="col-md-5"></div>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>Milk To Power Conversion
                        </legend>
                        <div class="table table-responsive">
                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static" AutoGenerateColumns="False">
                                <Columns>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" CssClass="label label-default" Text="Receive" CommandArgument='<%# Eval("MilkToPowerConversion_ID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Style="padding: 3px; border-radius: 3px;" CssClass='<%# Eval("Receive_Status").ToString() != "0" ? "label label-success" : "label label-warning" %>' Text='<%# Eval("Receive_Status").ToString() == "0" ? "Pending" : "Received" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("Remark") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Conversion <br/> Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkToPowerConversionType" runat="server" Text='<%# Eval("MilkTrasferType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Conversion <br/>  Union">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConversionOfficeTo" runat="server" Text='<%# Eval("ConversionOfficeTo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk <br/>  Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk Quantity <br/>  In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FAT <br/>In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkFat" runat="server" Text='<%# Eval("MilkFat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SNF <br/>  In KG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkSnf" runat="server" Text='<%# Eval("MilkSnf") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Milk <br/>  Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMilkValue" runat="server" Text='<%# Eval("MilkValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Recovery <br/> %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecoveryPer" runat="server" Text='<%# Eval("RecoveryPer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approx  <br/> QuantityIn M.T.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecoveredSMPQuantityMT" runat="server" Text='<%# Eval("RecoveredSMPQuantityMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SMP  <br/> Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecoveredSMPValue" runat="server" Text='<%# Eval("RecoveredSMPValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>



                </div>

            </div>




            <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

            <div class="modal" id="ParameterModalDetail">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="height: 460px;">
                            <div class="modal-header">
                                <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>
                                <h4 class="modal-title">Date :
                                <asp:Label ID="lbldate" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp;
                               Conversion Type:
                                <asp:Label ID="lblConversionType" Font-Bold="true" runat="server"></asp:Label>
                                    &nbsp; 
                               Conversion Union : 
                                <asp:Label ID="lblConversionUnion" Font-Bold="true" runat="server"></asp:Label>

                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="row" style="height: 300px; overflow: scroll;">
                                    <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>

                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Date<span class="text-danger">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ControlToValidate="txtreceivedate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtreceivedate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Recovered Quantity(In M.T.)<span class="text-danger">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtsmpreceivedqty" Text="<i class='fa fa-exclamation-circle' title='Enter Recovered Approx Quantity!'></i>" ErrorMessage="Enter SMP Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtsmpreceivedqty" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>SMP Value<span class="text-danger">*</span></label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="txtsmpreceivedvalue" Text="<i class='fa fa-exclamation-circle' title='Enter SMP Value!'></i>" ErrorMessage="Enter SMP Value" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox ID="txtsmpreceivedvalue" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Remarks</label>
                                                    <asp:TextBox ID="txtRemarks_R" TextMode="MultiLine" runat="server" placeholder="Enter Remarks..." class="form-control" MaxLength="200"></asp:TextBox>
                                                </div>
                                            </div>



                                           

                                            <div class="col-md-12">
                                                 <hr />
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="true"
                                                        EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys"
                                                        AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="SMPRecieve_ID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("SMPRecieve_ID").ToString()%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="SMPRecieve_Date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" />
                                                             

                                                            <asp:TemplateField HeaderText="QuantityIn M.T.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSMPValueQty" runat="server" Text='<%# Eval("SMPValueQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SMP Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSMPValue" runat="server" Text='<%# Eval("SMPValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                        </div>
                                    </div>




                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="save" ID="btnSavePMDetails" Text="Save Result" OnClientClick="return ValidateTPM()" />
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>


            <div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT" OnClick="btnYesT_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function RecieveModal() {
            $("#ParameterModalDetail").modal('show');
        }
    </script>

    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }


        function ValidateTPM() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSavePMDetails.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSavePMDetails.ClientID%>').value.trim() == "Save Result") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }

    </script>
</asp:Content>
