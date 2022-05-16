<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_SetItemRatioForProducts.aspx.cs" Inherits="mis_CattleFeed_CFP_SetItemRatioForProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Production Set Ratio(उत्पादन अनुपात रखना)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Set Item Ratio for Product (प्रोडक्ट के लिए अनुपात प्रविष्टि करें)
                                </legend>
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Production Unit<span class="hindi">(उत्पादन इकाई नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rvf1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProdUnit" InitialValue="0" ErrorMessage="Please Select Production Unit." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlProdUnit" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProdUnit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Product Name<span class="hindi">(उत्पाद नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv2" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProd" InitialValue="0" ErrorMessage="Please Select Product." Text="<i class='fa fa-exclamation-circle' title='Please Select Product !'></i>"></asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlProd" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <asp:Button ID="btnview" runat="server" Text="View Item to Set Ratio" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="a" OnClick="btnview_Click" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12" style="text-align: center;">
                                    <asp:GridView ID="gvProductItems" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.(क्रं)">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:HiddenField ID="hdnUnitid" runat="server" Value='<%#Eval("Unitid") %>' />
                                                    <asp:HiddenField ID="hdnItemCatID" runat="server" Value='<%#Eval("ItemCatID") %>' />
                                                    <asp:HiddenField ID="hdnItemTypeID" runat="server" Value='<%#Eval("ItemTypeID") %>' />
                                                    <asp:HiddenField ID="hdnItemID" runat="server" Value='<%#Eval("ItemID") %>' />
                                                    <asp:HiddenField ID="hdnCFPProductSpecifcationID" runat="server" Value='<%#Eval("CFPProductSpecifcationID") %>' />
                                                    <asp:HiddenField ID="hdnItemRatioID" runat="server" Value='<%#Eval("TI_Item_Ratio_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Items(वस्तु)">
                                                <ItemTemplate>

                                                    <%# Eval("ItemName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit(इकाई )">
                                                <ItemTemplate>

                                                    <%# Eval("UnitName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Ratio">
                                                <ItemTemplate>
                                                    <%#Eval("ItemRation") %>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Total(कुल) :
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage/Metric Tonne <span style='color: red;'> *</span>">
                                                <ItemTemplate>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="txtPercentage" autocomplete="off" runat="server" CssClass="form-control Amount" MaxLength="20" OnTextChanged="txtPercentage_TextChanged" AutoPostBack="true" Text='<%#Eval("ProductPercentage") %>'></asp:TextBox>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfvper" Display="Dynamic" ValidationGroup="b" InitialValue="0" runat="server" ControlToValidate="txtPercentage" ErrorMessage="Please Enter Percentage/Metric Tonne." Text="<i class='fa fa-exclamation-circle' title='Please Enter Percentage/Metric Tonne !'></i>">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,3})?$" ValidationGroup="b" runat="server" ControlToValidate="txtPercentage" ErrorMessage="Please Enter Valid Number or two decimal value." Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Number or two decimal value. !'></i>"></asp:RegularExpressionValidator>
                                                        </span>
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterTotal" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-12" id="pnlsave" runat="server" visible="false" style="text-align: center;">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Save" ValidationGroup="b" ID="btnSubmit" CssClass="btn btn-block btn-success" Style="margin-top: 25px;" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" />

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Clear" ID="btnReset" CssClass="btn btn-block btn-default" Style="margin-top: 25px;" runat="server" OnClick="btnReset_Click" />
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                            <fieldset id="filledgrd" runat="server" visible="false">
                                <legend>Item Ratio for Product :
                                    <asp:Label ID="lblproname" runat="server" ForeColor="Maroon"></asp:Label>
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                            <asp:GridView ID="gvOpeningStock" DataKeyNames="ProductItemSetRatioID" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                    <asp:BoundField DataField="ItemRation" HeaderText="Ratio" />
                                                    <asp:BoundField DataField="ProductPercentage" HeaderText="Percentage/Metric Tonne" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

