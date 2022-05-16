<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="E_Invoice.aspx.cs" Inherits="mis_E_Invoice_E_Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        legend {
            font-weight: 700;
        }

        .tabcss {
            width: 100%;
        }

            .tabcss td {
                padding-bottom: 3px !important;
            }

        .form-control, .btn-success {
            height: 28px;
            padding: 3px 6px;
        }

        .form-group {
            margin-bottom: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">E - Invoice</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row hidden">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Voucher No</label><span style="color: red">*</span>
                                        <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="form-control" placeholder="Enter Voucher No..." autocomplete="off" MaxLength="15"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 22px;" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Version :<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtVersion" Text="1.1" placeholder="Version..." ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset style="background-color: #d2d6de59;">
                                        <legend>Seller Details</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Seller GSTIN :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Gstin" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Legal Name :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_LglNm" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Trad Name :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_TrdNm" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Seller Address 1 :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Addr1" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Seller Address 2 :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Addr2" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Seller Location :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Loc" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>State :<span style="color: red;"> *</span></label>

                                                    <asp:DropDownList ID="ddlS_Stcd" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="37">Andhra Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="12">Arunachal Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="18">Assam</asp:ListItem>
                                                        <asp:ListItem Value="10">Bihar</asp:ListItem>
                                                        <asp:ListItem Value="30">Goa</asp:ListItem>
                                                        <asp:ListItem Value="24">Gujarat</asp:ListItem>
                                                        <asp:ListItem Value="06">Haryana</asp:ListItem>
                                                        <asp:ListItem Value="02">Himachal Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="09">Jammu and Kashmir</asp:ListItem>
                                                        <asp:ListItem Value="29">Karnataka</asp:ListItem>
                                                        <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                        <asp:ListItem Value="23" Selected="True">Madhya Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                        <asp:ListItem Value="14">Manipur</asp:ListItem>
                                                        <asp:ListItem Value="17">Meghalaya</asp:ListItem>
                                                        <asp:ListItem Value="15">Mizoram</asp:ListItem>
                                                        <asp:ListItem Value="13">Nagaland</asp:ListItem>
                                                        <asp:ListItem Value="21">Odisha</asp:ListItem>
                                                        <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                        <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                        <asp:ListItem Value="11">Sikkim</asp:ListItem>
                                                        <asp:ListItem Value="33">Tamil Nadu</asp:ListItem>
                                                        <asp:ListItem Value="16">Tripura</asp:ListItem>
                                                        <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="19">West Bengal</asp:ListItem>
                                                        <asp:ListItem Value="22">Chhattisgarh</asp:ListItem>
                                                        <asp:ListItem Value="05">Uttarakhand</asp:ListItem>
                                                        <asp:ListItem Value="20">Jharkhand</asp:ListItem>
                                                        <asp:ListItem Value="36">Telangana</asp:ListItem>
                                                        <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                        <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                        <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="07">Delhi</asp:ListItem>
                                                        <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                        <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                        <asp:ListItem Value="26">Dadra and Nagar Haveli</asp:ListItem>
                                                        <asp:ListItem Value="25">Daman and Diu</asp:ListItem>
                                                        <asp:ListItem Value="31">Lakshadweep</asp:ListItem>
                                                        <asp:ListItem Value="34">Puducherry</asp:ListItem>
                                                        <asp:ListItem Value="97">Other Territory</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Pin Code :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Pin" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Phone Number:</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Ph" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Email ID :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtS_Em" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Supply Type Code :<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlTr_SupTyp" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="B2B">B2B</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Reverse Charge :</label>
                                        <asp:DropDownList ID="ddlTr_RegRev" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>e-comm GSTIN :</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTr_EcmGstin" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Igst on Intra :</label>
                                        <asp:DropDownList ID="ddlTr_IgstOnIntra" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset style="background-color: #d4600e40;">
                                        <legend>Document Details</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Document Type :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDc_Typ" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Document Number :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDc_No" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Document Date (DD/MM/YYYY) :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtDc_Dt" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                    <fieldset style="background-color: #51b2d040;">
                                        <legend>Buyer Details</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer GSTIN :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtB_Gstin" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer Legal Name :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtB_LglNm" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer Trad Name :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtB_TrdNm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer Address 1 :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" required CssClass="form-control" ID="txtB_Addr1" placeholder="" Text="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer Address 2 :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtB_Addr2" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer Location :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" required CssClass="form-control" ID="txtB_Loc" placeholder="" Text="" ClientIDMode="Static" autocomplete="off" AutoCompleteType="Disabled" autofill="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer State :<span style="color: red;"> *</span></label>
                                                    <asp:DropDownList ID="ddlB_Stcd" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="37">Andhra Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="12">Arunachal Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="18">Assam</asp:ListItem>
                                                        <asp:ListItem Value="10">Bihar</asp:ListItem>
                                                        <asp:ListItem Value="30">Goa</asp:ListItem>
                                                        <asp:ListItem Value="24">Gujarat</asp:ListItem>
                                                        <asp:ListItem Value="06">Haryana</asp:ListItem>
                                                        <asp:ListItem Value="02">Himachal Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="09">Jammu and Kashmir</asp:ListItem>
                                                        <asp:ListItem Value="29">Karnataka</asp:ListItem>
                                                        <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                        <asp:ListItem Value="23" Selected="True">Madhya Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                        <asp:ListItem Value="14">Manipur</asp:ListItem>
                                                        <asp:ListItem Value="17">Meghalaya</asp:ListItem>
                                                        <asp:ListItem Value="15">Mizoram</asp:ListItem>
                                                        <asp:ListItem Value="13">Nagaland</asp:ListItem>
                                                        <asp:ListItem Value="21">Odisha</asp:ListItem>
                                                        <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                        <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                        <asp:ListItem Value="11">Sikkim</asp:ListItem>
                                                        <asp:ListItem Value="33">Tamil Nadu</asp:ListItem>
                                                        <asp:ListItem Value="16">Tripura</asp:ListItem>
                                                        <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="19">West Bengal</asp:ListItem>
                                                        <asp:ListItem Value="22">Chhattisgarh</asp:ListItem>
                                                        <asp:ListItem Value="05">Uttarakhand</asp:ListItem>
                                                        <asp:ListItem Value="20">Jharkhand</asp:ListItem>
                                                        <asp:ListItem Value="36">Telangana</asp:ListItem>
                                                        <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                        <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                        <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                        <asp:ListItem Value="07">Delhi</asp:ListItem>
                                                        <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                        <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                        <asp:ListItem Value="26">Dadra and Nagar Haveli</asp:ListItem>
                                                        <asp:ListItem Value="25">Daman and Diu</asp:ListItem>
                                                        <asp:ListItem Value="31">Lakshadweep</asp:ListItem>
                                                        <asp:ListItem Value="34">Puducherry</asp:ListItem>
                                                        <asp:ListItem Value="97">Other Territory</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Buyer Pin Code :<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" required CssClass="form-control" ID="txtB_Pin" Text="" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Buyer Phone Number:</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtB_Ph" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Buyer Email ID :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtB_Em" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>


                                    <!---------------Product, Batch, Attribute Details------------------>
                                    <fieldset style="background-color: #ebfff6;">
                                        <legend>Product, Batch, Attribute Details</legend>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridItemDetails" runat="server" DataKeyNames="Item_id" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl.No." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_SlNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="Itm_PrdDesc" runat="server" Text='<%# Eval("Description").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("Item_id").ToString()%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Service">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="Is_ServiceText" runat="server" Text='<%# Eval("Is_ServiceText").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="Itm_IsServc" CssClass="hidden" runat="server" Text='<%# Eval("Is_Service").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HSN code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_HsnCd" runat="server" Text='<%# Eval("HSN code").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="Itm_Barcde" CssClass="hidden" runat="server" Text='<%# Eval("Bar code").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_Qty" runat="server" Text='<%# Eval("Quantity").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="Itm_FreeQty" CssClass="hidden" runat="server" Text='<%# Eval("Free Quantity").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_Unit" runat="server" Text='<%# Eval("UnitName").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Unit Price">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_UnitPrice" runat="server" Text='<%# Eval("Unit Price").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gross Amount " ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_TotAmt" runat="server" Text='<%# Eval("Gross Amount").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="Itm_Discount" runat="server" CssClass="hidden" Text='<%# Eval("Discount").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pre Tax Value " ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_PreTaxVal" runat="server" Text='<%# Eval("Pre Tax Value").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Taxable value" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_AssAmt" runat="server" Text='<%# Eval("Taxable value").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="GST Rate (%) ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_GstRt" runat="server" Text='<%# Eval("GST Rate").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sgst Amt(Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_SgstAmt" runat="server" Text='<%# Eval("Sgst Amt").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cgst Amt (Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_CgstAmt" runat="server" Text='<%# Eval("Cgst Amt").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Igst Amt (Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_IgstAmt" runat="server" Text='<%# Eval("Igst Amt").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Cess Rate (%)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_CesRt" runat="server" Text='<%# Eval("Cess Rate").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cess Amt Adval (Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_CesAmt" runat="server" Text='<%# Eval("Cess Amt Adval").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Cess Non Adval Amt (Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_CesNonAdvlAmt" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State Cess Rate (%)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_StateCesRt" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State Cess Adval Amt (Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_StateCesAmt" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State Cess Non-Adval Amt (Rs)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_StateCesNonAdvlAmt" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Other Charges">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_OthChrg" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_TotItemVal" runat="server" Text='<%# Eval("Item Total").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order line reference">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_OrdLineRef" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Orgin country">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_OrgCntry" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unique item Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Itm_PrdSlNo" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- Batch Details --%>
                                                            <asp:TemplateField HeaderText="Batch Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Bch_Nm" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Batch Expiry Dt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Bch_Expdt" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Warranty Dt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Bch_wrDt" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- Attribute Details --%>
                                                            <asp:TemplateField HeaderText="Attribute Details of the items">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Att_Nm" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attribute Value of the Items">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Att_Val" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                    <!---------------Value Details------------------>
                                    <fieldset style="background-color: #f8ffd28c;">
                                        <legend>Value Details</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridTotal" runat="server" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Total Taxable value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_AssVal" runat="server" Text='<%# Eval("Total Taxable value").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sgst Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_SgstVal" runat="server" Text='<%# Eval("Sgst Amt").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cgst Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_CgstVal" runat="server" Text='<%# Eval("Cgst Amt").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Igst Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_IgstVal" runat="server" Text='<%# Eval("Igst Amt").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cess Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_CesVal" runat="server" Text='<%# Eval("Cess Amt").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="State Cess Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_StCesVal" runat="server" Text='0'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discount" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_Discount" runat="server" Text='<%# Eval("Discount").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Other charges" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_OthChrg" runat="server" Text='<%# Eval("Other charges").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Round off" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_RndOffAmt" runat="server" Text='<%# Eval("Round off").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Total Invoice value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_TotInvVal" runat="server" Text='<%# Eval("Total Invoice value").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Total Invoice value  in Additional Currency">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Tot_TotInvValFc" runat="server" Text='<%# Eval("Total Invoice value  in Additional Currency").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>



                                    <!---------------E- Invoice Details------------------>
                                    <fieldset style="background-color: #eaf5ffb8;">
                                        <legend>E - Invoice Details</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <table class="tabcss">
                                                        <%--<tr>
                                                            <td style="width: 40%;">
                                                                <label>1. Do you have Reverse Charges ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td style="width: 40%; padding-left: 25px;">
                                                                <label>2. Do you have e - commerce GSTIN ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td style="width: 40%;">
                                                                <label>1. Do you have Bill from and Dispatch from transaction details ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="ddlIsDispatch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlIsDispatch_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 40%; padding-left: 25px;">
                                                                <label>2. Do you have Bill to and Ship to  transaction details ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="ddlIsBillToShip" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlIsBillToShip_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr>
                                                            <td style="width: 40%;">
                                                                <label>5. Do you have Supply of Service ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td style="width: 40%; padding-left: 25px;">
                                                                <label>6. Do you have Bar code details in Items ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>--%>
                                                        <tr>
                                                            <%--<td style="width: 40%;">
                                                                <label>7. Do you have free quantity in Items ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList></td>--%>
                                                            <td style="width: 40%;">
                                                                <label>3. Do you have enter Batch details of Items ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="ddlIsBchDtls" runat="server" CssClass="form-control" Enabled="false">
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 40%; padding-left: 25px;">
                                                                <label>4. Do you have Eway-bill deatils ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="ddlIsEwayBill" runat="server" CssClass="form-control" Enabled="false">
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <%-- <td style="width: 40%;">
                                                                <label>9. Do you have Export details ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList></td>--%>

                                                            <td style="width: 40%;">
                                                                <label>5. Do you have Item's other details ?</label></td>
                                                            <td style="width: 10%;">
                                                                <asp:DropDownList ID="ddlIsOtherDtls" runat="server" CssClass="form-control" Enabled="false">
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="width: 40%; padding-left: 25px;"></td>
                                                            <td style="width: 10%;"></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </div>

                                    </fieldset>
                                    <asp:Panel ID="pnlDispatch" runat="server">
                                        <!---------------Dispatch Details------------------>
                                        <fieldset style="background-color: #f8ffd28c;">
                                            <legend>Dispatch Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Dispatch Name :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtD_Nm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Dispatch Addr1 :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtD_Addr1" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Dispatch Addr2 :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtD_Addr2" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Dispatch Location :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtD_Loc" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Dispatch Pin Code :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtD_Pin" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Dispatch State :</label>
                                                        <asp:DropDownList ID="ddlD_Stcd" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="37">Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="12">Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="18">Assam</asp:ListItem>
                                                            <asp:ListItem Value="10">Bihar</asp:ListItem>
                                                            <asp:ListItem Value="30">Goa</asp:ListItem>
                                                            <asp:ListItem Value="24">Gujarat</asp:ListItem>
                                                            <asp:ListItem Value="06">Haryana</asp:ListItem>
                                                            <asp:ListItem Value="02">Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="09">Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem Value="29">Karnataka</asp:ListItem>
                                                            <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                            <asp:ListItem Value="23">Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                            <asp:ListItem Value="14">Manipur</asp:ListItem>
                                                            <asp:ListItem Value="17">Meghalaya</asp:ListItem>
                                                            <asp:ListItem Value="15">Mizoram</asp:ListItem>
                                                            <asp:ListItem Value="13">Nagaland</asp:ListItem>
                                                            <asp:ListItem Value="21">Odisha</asp:ListItem>
                                                            <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                            <asp:ListItem Value="11">Sikkim</asp:ListItem>
                                                            <asp:ListItem Value="33">Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem Value="16">Tripura</asp:ListItem>
                                                            <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="19">West Bengal</asp:ListItem>
                                                            <asp:ListItem Value="22">Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem Value="05">Uttarakhand</asp:ListItem>
                                                            <asp:ListItem Value="20">Jharkhand</asp:ListItem>
                                                            <asp:ListItem Value="36">Telangana</asp:ListItem>
                                                            <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                            <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                            <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="07">Delhi</asp:ListItem>
                                                            <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                            <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="26">Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem Value="25">Daman and Diu</asp:ListItem>
                                                            <asp:ListItem Value="31">Lakshadweep</asp:ListItem>
                                                            <asp:ListItem Value="34">Puducherry</asp:ListItem>
                                                            <asp:ListItem Value="97">Other Territory</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlShipping" runat="server">
                                        <!---------------Shipping Details------------------>
                                        <fieldset style="background-color: #d2d6de59;">
                                            <legend>Shipping Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping GSTIN :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_Gstin" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Legal Name :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_LglNm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Trade Name :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_TrdNm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Addr1 :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_Addr1" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Addr2 :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_Addr2" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Location :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_Loc" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Pin Code :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSp_Pin" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping State :</label>
                                                        <asp:DropDownList ID="ddl_Sp_Stcd" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="37">Andhra Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="12">Arunachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="18">Assam</asp:ListItem>
                                                            <asp:ListItem Value="10">Bihar</asp:ListItem>
                                                            <asp:ListItem Value="30">Goa</asp:ListItem>
                                                            <asp:ListItem Value="24">Gujarat</asp:ListItem>
                                                            <asp:ListItem Value="06">Haryana</asp:ListItem>
                                                            <asp:ListItem Value="02">Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="09">Jammu and Kashmir</asp:ListItem>
                                                            <asp:ListItem Value="29">Karnataka</asp:ListItem>
                                                            <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                            <asp:ListItem Value="23">Madhya Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                            <asp:ListItem Value="14">Manipur</asp:ListItem>
                                                            <asp:ListItem Value="17">Meghalaya</asp:ListItem>
                                                            <asp:ListItem Value="15">Mizoram</asp:ListItem>
                                                            <asp:ListItem Value="13">Nagaland</asp:ListItem>
                                                            <asp:ListItem Value="21">Odisha</asp:ListItem>
                                                            <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                            <asp:ListItem Value="11">Sikkim</asp:ListItem>
                                                            <asp:ListItem Value="33">Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem Value="16">Tripura</asp:ListItem>
                                                            <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="19">West Bengal</asp:ListItem>
                                                            <asp:ListItem Value="22">Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem Value="05">Uttarakhand</asp:ListItem>
                                                            <asp:ListItem Value="20">Jharkhand</asp:ListItem>
                                                            <asp:ListItem Value="36">Telangana</asp:ListItem>
                                                            <asp:ListItem Value="32">Kerala</asp:ListItem>
                                                            <asp:ListItem Value="27">Maharashtra</asp:ListItem>
                                                            <asp:ListItem Value="09">Uttar Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="07">Delhi</asp:ListItem>
                                                            <asp:ListItem Value="08">Rajasthan</asp:ListItem>
                                                            <asp:ListItem Value="03">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="26">Dadra and Nagar Haveli</asp:ListItem>
                                                            <asp:ListItem Value="25">Daman and Diu</asp:ListItem>
                                                            <asp:ListItem Value="31">Lakshadweep</asp:ListItem>
                                                            <asp:ListItem Value="34">Puducherry</asp:ListItem>
                                                            <asp:ListItem Value="97">Other Territory</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                    </asp:Panel>
                                    <asp:Panel ID="pnlNotM" runat="server">
                                        <!---------------Export Details------------------>
                                        <fieldset style="background-color: #fff0ef;">
                                            <legend>Export Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Bill No :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExp_ShipBNo" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Shipping Bill Dt :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExp_ShipBDt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Port :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExp_Port" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Supplier Refund :</label>
                                                        <asp:DropDownList ID="ddlExp_RefClm" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="N">No</asp:ListItem>
                                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox runat="server" CssClass="form-control" ID="txtExp_RefClm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>--%>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Foreign Currency :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExp_ForCur" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Country Code :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtExp_CntCode" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Export Duty Amount :</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="TextBox76" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>--%>
                                        </fieldset>
                                        <!---------------E-way-bill Details------------------>
                                        <fieldset style="background-color: #eaf5ffb8;">
                                            <legend>E-way-bill Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Trans ID :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox40" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Trans Name :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox41" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Trans Mode :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox42" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Distance :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox43" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Trans Doc No. :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox44" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Trans Doc Date :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox45" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Vehicle No. :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox74" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Vehicle Type :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="TextBox75" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <!---------------Payment Details------------------>
                                        <fieldset style="background-color: #fff6e7;">
                                            <legend>Payment Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Payee Name :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Nm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Account Number :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Accdet" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Mode :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Mode" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Branch/IFSC Code :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Fininsbr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Term  of payment :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Payterm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Payment instruction :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Payinstr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Credit Transfer :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Crtrn" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>direct debit :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Dirdr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>credit days :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Crday" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Paided amount :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Paidamt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Due amount :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPay_Paymtdue" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>


                                        <!---------------Reference Details------------------>
                                        <fieldset style="background-color: #d2d6de59;">
                                            <legend>Reference Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Remarks :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRef_InvRm" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Invoice period start date :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRef_InvStDt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Invoice period end date :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRef_InvEndDt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <!---------------Pre Doc Details------------------>
                                        <fieldset style="background-color: #f8ffd28c;">
                                            <legend>Pre Doc Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Original Invoice :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPdc_InvNo" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Preceding Invoice Date :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPdc_InvDt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Other Reference :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPdc_OthRefNo" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <!---------------Contract Details------------------>
                                        <fieldset style="background-color: #ebfff6;">
                                            <legend>Contract Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Receipt Advice Number :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_RecAdvRefr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Date of Receipt Advice :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_RecAdvDt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Lot/Batch Reference Number :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_Tendrefr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Contract Reference Number :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_Contrrefr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Any other reference :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_Extrefr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Project Reference Number :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_Projrefr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Vendor PO Reference Number :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_Porefr" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Vendor PO Reference date :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCont_PoRefDt" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>

                                        <!---------------Additional Details------------------>
                                        <fieldset style="background-color: #eaf5ffb8;">
                                            <legend>Additional Details</legend>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Supporting Doc URL :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAdc_Url" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Supporting Doc in Base 64 format :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAdc_Docs" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Any additional information :</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAdc_Info" placeholder="" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="Button1" CssClass="btn btn-block btn-primary" runat="server" Text="Validate" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnSaveGenerate" CssClass="btn btn-block btn-warning" runat="server" OnClick="btnSaveGenerate_Click" Text="Save And Generate Json" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                        </div>

                                    </div>



                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

