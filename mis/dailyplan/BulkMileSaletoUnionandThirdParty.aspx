<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BulkMileSaletoUnionandThirdParty.aspx.cs" Inherits="mis_dailyplan_BulkMileSaletoUnionandThirdParty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click"  Style="margin-top: 20px; width: 50px;" />
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
                    <h3 class="box-title">BULK MILK SALE TO UNION/MDP & THIRD PARTY</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>BULK MILK SALE TO UNION/MDP & THIRD PARTY DETAIL</legend>
                        <div class="row">
                           
                            <div class="col-md-7">
                            <div class="form-group">
                               <asp:RadioButtonList ID="rbtnTransferType" runat="server" RepeatDirection="Horizontal" style="margin-top:20px;" OnSelectedIndexChanged="rbtnsaleto_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Union To Union&nbsp;&nbsp;</asp:ListItem>
                                         <asp:ListItem Value="2">&nbsp;&nbsp;Union To Third Party&nbsp;&nbsp;</asp:ListItem>
                                   <asp:ListItem Value="3">&nbsp;&nbsp;Union To MDP</asp:ListItem>
                                    </asp:RadioButtonList>
                            </div>
                        </div>
                            </div>
                        <div class="row" >
                             <div class="col-md-2">
                            <div class="form-group">
                                <label>Date<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
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
                            <div class="col-md-2" id="union" runat="server">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Union<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlUnion" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlUnion" Text="<i class='fa fa-exclamation-circle' title='Select Union!'></i>" ErrorMessage="Select Union" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlUnion" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    <small><span id="valddlUnion" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-2" id="thirdparty" runat="server">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Third Party<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlThirdparty" runat="server" Display="Dynamic" InitialValue="0"  ControlToValidate="ddlThirdparty" Text="<i class='fa fa-exclamation-circle' title='Select Third Party!'></i>" ErrorMessage="Select Third Party" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlThirdparty" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    <small><span id="valddlThirdparty" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-2" id="MDP" runat="server">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Mini Dairy Plant<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvddlMDP" runat="server" InitialValue="0" Display="Dynamic" ControlToValidate="ddlMDP" Text="<i class='fa fa-exclamation-circle' title='Select Mini Dairy Plant!'></i>" ErrorMessage="Select Mini Dairy Plant" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlMDP" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    <small><span id="valddlMDP" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-2">
                            <div class="form-group">
                                <label>Quantity In KG<span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" Display="Dynamic" ControlToValidate="txtQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtQuantity" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event)"></asp:TextBox>
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
                            <div class="col-md-12">
                            <div class="form-group">
                                <label>Remark</label>
                                
                                <asp:TextBox ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" autocomplete="off" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary"  ValidationGroup="Save" OnClientClick="return ValidatePage();"  Text="Save"/>
                                    </div>
                                </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>
                            BULK MILK SALE DETAIL
                        </legend>
                        <div class="table table-responsive">
                                <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("Remark") %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>' ToolTip ='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransferfrom" runat="server" Text='<%# Eval("TransferFrom") %>'></asp:Label>                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Transfer To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransferTo" runat="server" Text='<%# Eval("TransferTo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMilkTrasferType" runat="server" Text='<%# Eval("MilkTrasferType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAT In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" Text="Edit" CommandName="EditRecord" CommandArgument='<%# Eval("BilkMilkSale_Id") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                    </fieldset>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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
        </script>
</asp:Content>

