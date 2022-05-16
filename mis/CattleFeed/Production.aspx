<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Production.aspx.cs" Inherits="mis_CattleFeed_Production" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModelNew').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Save/सुरक्षित") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModelNew').modal('show');
                    return false;
                }
            }
        }
    </script>
    <div class="content-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">

                        <h3 class="box-title">Production Entry (उत्पादन प्रविष्टि)</h3>
                    </div>
                    <div class="box-body">
                        <fieldset>
                            <legend>Item Production Entry
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Production Unit Name<span class="hindi">(उत्पादन इकाई नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvpro" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlcfp" InitialValue="0" ErrorMessage="Please Select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlcfp" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Prodution Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Date (दिनांक)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtTransactionDt" ErrorMessage="Please Enter Date" Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>

                                                <asp:TextBox ID="txtTransactionDt"  autocomplete="off" onpaste="javascript: return false;" onkeypress="javascript: return false;" placeholder="Select Date" runat="server" CssClass="form-control CurDate" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" data-date-end-date="0d" Style="float: left;"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-4" style="margin-top: 15px">
                                        <asp:Button ID="btnGenerate" runat="server" ValidationGroup="a" CausesValidation="true" CssClass="btn btn-primary" Text="Generate Production" OnClick="btnGenerate_Click" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                        <asp:GridView ID="gvOpeningStock" HeaderStyle-HorizontalAlign="Center" DataKeyNames="Item_id" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-bordered table-striped  table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="hdnCFPProductSizeID" runat="server" Value='<%#Eval("CFPProductSizeID") %>' />
                                                        <asp:HiddenField ID="hdnpackingsize" runat="server" Value='<%#Eval("Packaging_Size") %>' />
                                                        <asp:HiddenField ID="hdnCFP_ProductProductionid" runat="server" Value='<%#Eval("CFP_ProductProductionid") %>' />
                                                        <asp:HiddenField ID="hdnItem" runat="server" Value='<%#Eval("Item_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Name<br />(प्रोडक्ट  का नाम)">
                                                    <ItemTemplate><%#Eval("ItemName") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Batch No. <br />(बैच क्रमांक)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtbatchno" autocomplete="off"  CausesValidation="true" runat="server" placeholder="Batch Number" CssClass="form-control" onpaste="return false;" MaxLength="20" Style="width: 100%; float: left;" Text='<%#Eval("BatchNo") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Bags <br />(कुल बैग)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txttotalpacket" autocomplete="off" AutoPostBack="true"  runat="server" placeholder="Total Packets" CssClass="form-control Number" onpaste="return false;" MaxLength="15" Style="width: 100%; float: left;" onkeypress="return validateNum(event);" OnTextChanged="txttotalpacket_TextChanged" Text='<%#Eval("TotalBagsCreated") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rfvpackets" runat="server" Display="Dynamic" ControlToValidate="txttotalpacket" ValidationGroup="b" ErrorMessage="Accept Number only" ValidationExpression="^\d+" SetFocusOnError="true" ForeColor="Red" ></asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Production Quantity (In Metric Ton) <br />उत्पादन की मात्रा (मैट्रिक टन मे)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDailyProdCr" runat="server" autocomplete="off" CausesValidation="true" Enabled="false" placeholder="Production Qty." ValidationGroup="Mtr" CssClass="form-control Number" onpaste="return false;" MaxLength="15" Style="width: 100%; float: left;" Text='<%#Eval("ProductionQuantity") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Scrap Qty (In Metric Ton) <br />स्क्रैप मात्रा (मैट्रिक टन में)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtScrapProd" runat="server" AutoPostBack="true" CausesValidation="true" autocomplete="off" Enabled="false" placeholder="Scrap Qty." ValidationGroup="b" CssClass="form-control" onpaste="return false;" MaxLength="10" Style="width: 100%; float: left;" onkeypress="return validateDec(this,event);" Text='<%#Eval("ScrapProductionQuantity") %>'></asp:TextBox>
                                                        <asp:CompareValidator ID="cfv" ValidationGroup="b" runat="server" Display="Dynamic"  ControlToValidate="txtScrapProd" ControlToCompare="txtDailyProdCr" ErrorMessage="Scrap quantity should not be grater then Production quantity." Type="Double" Operator="LessThanEqual" SetFocusOnError="true"></asp:CompareValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <br />
                                <div class="col-md-12">

                                    <asp:Button Text="Save/सुरक्षित" ID="btnsave" ValidationGroup="Save" CssClass="btn btn-success" runat="server" OnClientClick="return ValidatePage();" Visible="false" />
                                    &nbsp;&nbsp;<asp:Button ID="btnClear" runat="server" ValidationGroup="clear" CssClass="btn btn-default" Text="Reset/रीसेट" OnClick="btnClear_Click" Visible="false" />

                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>

            </div>

        </div>
        <div class="modal fade" id="myModelNew" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                                <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp;<asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />&nbsp;
                                    <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

