<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="MilkTransferToSections.aspx.cs" Inherits="mis_dailyplan_MilkTransferToSections" %>

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
                    <h3 class="box-title">Milk Transfer To Product Section</h3>
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
                                    <asp:DropDownList ID="ddlShift" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <fieldset>
                        <legend>Opening In Stock</legend>
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

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" ToolTip="Receive" class="small-box-footer" OnClick="LinkButton3_Click" runat="server"> 
                                                   View More 
                                                    </asp:LinkButton>
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

                                            <asp:TemplateField HeaderText="Section Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCr" runat="server" Text='<%# Eval("ProductSection_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblProductSection_ID" Visible="false" runat="server" Text='<%# Eval("ProductSection_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbgettotal" CssClass="btn btn-success" OnClick="lbgettotal_Click" runat="server">Get Total</asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Milk Qty In KG">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgv_mqty" Width="50%" Text='<%# Eval("TMQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtgv_mqty" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Milk Qty In KG Ex- 0.00" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FAT In Kg">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Fat" Width="50%" Text='<%# Eval("TFQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtgvV_Fat" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Fat Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF In KG">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Snf" Width="50%" Text='<%# Eval("TSQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtgvV_Snf" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid SNF Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Milk Qty In KG">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgv_mqty" Width="50%" Text='<%# Eval("TMQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" OnTextChanged="txtgv_mqty_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtgv_mqty" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Milk Qty In KG Ex- 0.00" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate> 
                                            </asp:TemplateField>
                                              
                                            <asp:TemplateField HeaderText="FAT In Kg">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Fat" Width="50%" Text='<%# Eval("TFQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" OnTextChanged="txtgvV_Fat_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtgvV_Fat" Display="Dynamic" ValidationExpression="^(\d*\.)?\d+$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Character'></i>" ErrorMessage="Enter Valid Fat Qty In KG Ex- 0.000" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                </ItemTemplate>
                                                <FooterStyle BackColor="#a9a9a9" ForeColor="White" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SNF In KG">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvV_Snf" Width="50%" Text='<%# Eval("TSQty") %>' AutoComplete="off" onpaste="return false;" onkeypress="return validateDec(this,event)" OnTextChanged="txtgvV_Snf_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <h3 class="box-title">Milk Transfer To Product Section History</h3>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="row">
                            <div class="col-md-12">
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
                                                <asp:Label ID="lblCr" runat="server" Text='<%# Eval("ProductSection_Name") %>'></asp:Label>
                                                <asp:Label ID="lblProductSection_ID" Visible="false" runat="server" Text='<%# Eval("ProductSection_ID") %>'></asp:Label>
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



            <div class="modal" id="CCModel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="height: 530px;width:925px;">
                        <div class="modal-header">
                            <asp:LinkButton runat="server" class="close" ID="LinkButton2" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                            <h4 class="modal-title">MILK IN/OUT DETAILS </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="height: 450px; overflow: scroll;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <section class="content">
                                                    <div class="box box-Manish" style="min-height: 300px;">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Milk Detail Date Wise</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="GVCC" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                                    EmptyDataText="No Record Found." ShowFooter="true">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate><asp:Label ID="lblTotal1" runat="server" Text="Total" Font-Bold="true"></asp:Label></FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDate" runat="server" Text='<%# (Convert.ToDateTime(Eval("Date"))).ToString("dd-MM-yyyy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate></FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Milk Qty In KG(In)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCr" runat="server" Text='<%# Eval("Cr") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTMQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FAT In Kg(In)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTFQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SNF In KG(In)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTSQty" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Milk Qty In KG(Out)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDr" runat="server" CssClass='<%# Eval("Dr").ToString() == "0.00" ? "label label-defalt" : "label label-danger" %>' Text='<%# Eval("Dr") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTMQtyDr" ForeColor="Red" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FAT In Kg(Out)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFat_Dr" runat="server" CssClass='<%# Eval("Fat_Dr").ToString() == "0.000" ? "label label-defalt" : "label label-danger" %>' Text='<%# Eval("Fat_Dr") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTFat_Dr" ForeColor="Red" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SNF In KG(Out)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSnf_Dr" runat="server" CssClass='<%# Eval("Snf_Dr").ToString() == "0.000" ? "label label-defalt" : "label label-danger" %>' Text='<%# Eval("Snf_Dr") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblTSnf_Dr" ForeColor="Red" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                         
                                                                        <%--<asp:TemplateField HeaderText="Milk In/Out Flow">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" CssClass='<%# Eval("Status").ToString() == "In" ? "label label-success" : "label label-danger" %>' runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate></FooterTemplate>
                                                                        </asp:TemplateField>--%>


                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                    </div>
                                                    <!-- /.box-body -->

                                                </section>
                                                <!-- /.content -->
                                            </div>
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


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


    <script>

        function CCModelF() {
            $("#CCModel").modal('show');
        }

    </script>

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
