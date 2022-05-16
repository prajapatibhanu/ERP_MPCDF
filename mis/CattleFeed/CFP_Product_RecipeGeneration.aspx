<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Product_RecipeGeneration.aspx.cs" Inherits="mis_CattleFeed_CFP_Product_RecipeGeneration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Generate Recipe for Product(प्रोडक्ट बनाने की विधि)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Set Item Ratio for Product (प्रोडक्ट के लिए अनुपात प्रविष्टि करें)
                                </legend>
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Production Unit<span class="hindi">(उत्पादन इकाई नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rvf1" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProdUnit" InitialValue="0" ErrorMessage="Please Select Production Unit." Text="<i class='fa fa-exclamation-circle' title='Please Select Production Unit !'></i>"></asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlProdUnit" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlProdUnit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Product Name<span class="hindi">(उत्पाद नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv2" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlProd" InitialValue="0" ErrorMessage="Please Select Product." Text="<i class='fa fa-exclamation-circle' title='Please Select Product !'></i>"></asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlProd" runat="server" CssClass="form-control select2" >
                                                <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Quantity<span class="hindi">(मात्रा)</span><span class="hindi" > in Metric Tonne</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="a"  runat="server" ControlToValidate="txtQty" InitialValue="0" ErrorMessage="Please Enter Product Quantity in MT." Text="<i class='fa fa-exclamation-circle' title='Please Enter Product Quantity in MT !'></i>"></asp:RequiredFieldValidator></span>
                                            <asp:TextBox ID="txtQty" autocomplete="off" runat="server" CssClass="form-control Amount" MaxLength="8"  onkeypress="return validateNum(event)" Text="0" ></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <asp:Button ID="btnview" runat="server" Text="Generate Recipe" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="a" OnClick="btnview_Click" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12" style="text-align: center;">
                                    <asp:GridView ID="gvProductItems" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                        EmptyDataText="No Record Found." >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.(क्रं)">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:HiddenField ID="hdnUnitid" runat="server" Value='<%#Eval("UnitID") %>' />
                                                    <asp:HiddenField ID="hdnItemCatID" runat="server" Value='<%#Eval("ItemCatID") %>' />
                                                    <asp:HiddenField ID="hdnItemTypeID" runat="server" Value='<%#Eval("ItemTypeID") %>' />
                                                    <asp:HiddenField ID="hdnItemID" runat="server" Value='<%#Eval("ItemID") %>' />
                                                    <asp:HiddenField ID="hdnProductItemSetRatioID" runat="server" Value='<%#Eval("ProductItemSetRatioID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Items(वस्तु)">
                                                <ItemTemplate>

                                                    <%# Eval("ItemName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Ratio">
                                                <ItemTemplate>
                                                    <%#Eval("ItemRation") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Percentage/Metric Tonne">
                                                <ItemTemplate>
                                                    <%#Eval("ProductPercentage") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Quantity(In MT)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitemquantity" runat="server" Text='<%#Eval("ItemQuantity") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
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
                                            <asp:Button Text="Reset" ID="btnReset" CssClass="btn btn-block btn-default" Style="margin-top: 25px;" runat="server" OnClick="btnReset_Click" />
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

