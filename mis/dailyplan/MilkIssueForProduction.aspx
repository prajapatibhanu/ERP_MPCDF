<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkIssueForProduction.aspx.cs" Inherits="mis_dailyplan_MilkIssueForProduction" %>

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
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Milk Issue For Production According to Variant In Particular Section</h3>
                </div>
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
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                    </div>

                    <fieldset>
                        <legend>Opening Stock</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvReceivedMilkDetail" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found.">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Milk Qty In KG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCr" runat="server" Text='<%# Eval("AvailableMilkInStock") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT In Kg">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFat" runat="server" Text='<%# Eval("AvailableFATInStock") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF In KG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("AvailableSNFInStock") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </fieldset>


                    <fieldset>
                        <legend>Milk Transfer To Section</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group table-responsive">
                                    <asp:GridView ID="gvmttos" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." ShowFooter="true">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Variant Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                                    <asp:Label ID="lblItem_FAT_RatioPer" Visible="false" runat="server" Text='<%# Eval("Item_FAT_RatioPer") %>'></asp:Label>
                                                    <asp:Label ID="lblItem_SNF_RatioPer" Visible="false" runat="server" Text='<%# Eval("Item_SNF_RatioPer") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbgettotal" CssClass="btn btn-success" OnClick="lbgettotal_Click" runat="server">Get Total</asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Prev. Demand </br>(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_Demand_InPkt" Text='<%# Eval("Prev_Demand_InPkt") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="Prev_Demand_InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Prev. Demand </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr" Text='<%# Eval("Prev_DemandInLtr") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPrev_DemandInLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Current Demand </br>(In Pkt)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InPkt" runat="server" Text='<%# Eval("Current_Demand_InPkt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InPkt_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Corrent Demand </br>(In Ltr)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InLtr" runat="server" Text='<%# Eval("Current_Demand_InLtr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCurrent_Demand_InLtr_F" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Milk Qty </br>(In Kg)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgv_mqty" Width="80%" OnTextChanged="txtgv_mqty_TextChanged" AutoPostBack="true" Text='<%# Eval("TMQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtgv_mqty" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Milk Qty In KG Ex- 0.00" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT </br>(In Kg)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Fat" Enabled="false" Width="80%" Text='<%# Eval("TFQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtgvV_Fat" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Fat Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF</br>(In Kg)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Snf" Enabled="false" Width="80%" Text='<%# Eval("TSQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtgvV_Snf" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid SNF Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <%-- <asp:TemplateField HeaderText="Milk Qty (In KG)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgv_mqty" Width="80%" Text='<%# Eval("TMQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" OnTextChanged="txtgv_mqty_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtgv_mqty" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Milk Qty In KG Ex- 0.00" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT (In Kg)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Fat" Width="80%" Text='<%# Eval("TFQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" OnTextChanged="txtgvV_Fat_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtgvV_Fat" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Fat Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF In KG">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Snf" Width="80%" Text='<%# Eval("TSQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" OnTextChanged="txtgvV_Snf_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtgvV_Snf" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid SNF Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>

                                    <div runat="server" class="pull-right" style="padding-top: 2px;" id="div2">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-success" Text="Save" />
                                    </div>

                                </div>
                            </div>
                        </div>

                    </fieldset>
                     
                </div>

            </div>
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Milk Issue For Product Variant History</h3>
                </div>
                <div class="box-body">
                    <div class="row">

                      
                            
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Filter Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtdateFilter" AutoPostBack="true" OnTextChanged="txtdateFilter_TextChanged" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                
                        
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Filter Shift</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlshiftFilter" OnSelectedIndexChanged="ddlshiftFilter_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                             <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPS" AutoPostBack="true"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPS_SelectedIndexChanged"></asp:DropDownList>
                                <small><span id="valddlPS" class="text-danger"></span></small>
                            </div>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group table-responsive">
                                <asp:GridView ID="GVMTHistory" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." ShowFooter="true">
                                    <Columns>


                                        <asp:TemplateField HeaderText="Milk Issue Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDate" Width="50%" Text='<%# Eval("Date") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblDateF" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Milk Issue Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="txtName" Width="50%" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" /> 
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Section Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductSection_Name" runat="server" Text='<%# Eval("ProductSection_Name") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Variant Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDr" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                <asp:Label ID="lblItemType_id" Visible="false" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Qty In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgv_mqty" Width="50%" Text='<%# Eval("TMQty") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FAT In Kg">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvV_Fat" Width="50%" Text='<%# Eval("TFQty") %>' runat="server"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF In KG">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvV_Snf" Width="50%" Text='<%# Eval("TSQty") %>' runat="server"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            debugger;
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

    </script>
</asp:Content>
