<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_AddSpecifications.aspx.cs" Inherits="mis_CattleFeed_CFP_AddSpecifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Add Product Specification (प्रोडक्ट विशिष्टता जोड़ें)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Add Product Specification (प्रोडक्ट विशिष्टता प्रविष्टि)
                                </legend>
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Production Unit Name<span class="hindi">(उत्पादन इकाई नाम)</span><span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rq1" runat="server" ControlToValidate="ddlProdUnit" InitialValue="0" ValidationGroup="a" ErrorMessage="Please select Production Unit." Text="<i class='fa fa-exclamation-circle' title='Please select Production Unit !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProdUnit" runat="server" AutoPostBack="true" CssClass="form-control select2" OnSelectedIndexChanged="ddlProdUnit_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Production Unit --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group"> 
                                            <label>Product Name(प्रोडक्ट का नाम)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rq2" runat="server" ControlToValidate="ddlProdName" InitialValue="0" ValidationGroup="a" ErrorMessage="Please select Product Name." Text="<i class='fa fa-exclamation-circle' title='Please select Product Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlProdName" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Product --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Category (वस्तु की श्रेणी)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="ddlitemCat" InitialValue="0" ValidationGroup="a" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitemCat" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemCat_SelectedIndexChanged"> 
                                                <asp:ListItem Text="--Select Category --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group"> 
                                            <label>Item Type (वस्तु की प्रकार)<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlitemtype" InitialValue="0" ValidationGroup="a" ErrorMessage="Select Item Sub-Group." Text="<i class='fa fa-exclamation-circle' title='Select Item Sub-Group !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitemtype" runat="server" Width="100%" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlitemtype_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Select Type --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Name (वस्तु का नाम)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="ddlitems" InitialValue="0" ValidationGroup="a" ErrorMessage="Select Items Name." Text="<i class='fa fa-exclamation-circle' title='Select Items Name !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlitems" runat="server" Width="100%"  CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Item--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Item Ratio(वस्तु अनुपात)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv6" runat="server" Display="Dynamic" ControlToValidate="ddlItemRatio" InitialValue="0" ValidationGroup="a" ErrorMessage="Select Item Ratio." Text="<i class='fa fa-exclamation-circle' title='Select Item Ratio !'></i>"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItemRatio" runat="server" Width="100%" CssClass="form-control select2">
                                                <asp:ListItem Text="-- Select Ratio --" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="text-align: center;">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Save" ID="btnSubmit" ValidationGroup="a" CssClass="btn btn-block btn-success" runat="server" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button Text="Clear" ID="btnReset" CssClass="btn btn-block btn-default" runat="server" OnClick="btnReset_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                            <legend> Product Specification  Detail(प्रोडक्ट विशिष्टता विवरण)
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                        <asp:GridView ID="gvOpeningStock" DataKeyNames="CFPProductSpecifcationID" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover" OnRowCommand="gvOpeningStock_RowCommand" PageSize="20" AllowPaging="true" OnPageIndexChanging="gvOpeningStock_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.<br />(क्रं.)">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product (प्रोडक्ट का नाम)" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name (वस्तु का नाम)" />
                                                <asp:BoundField DataField="ItemRation" HeaderText="Item Ratio(वस्तु अनुपात)" />
                                                <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("CFPProductSpecifcationID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("CFPProductSpecifcationID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

