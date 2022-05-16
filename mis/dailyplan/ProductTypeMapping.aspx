<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductTypeMapping.aspx.cs" Inherits="mis_dailyplan_ProductTypeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box-body">
                <div class="box-header">
                    <h3 class="box-title">PRODUCT TYPE MAPPING</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                        <legend>PRODUCT TYPE MAPPING</legend>
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                            <Columns>
                                 <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                                  <ItemTemplate>
                                      <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                  </ItemTemplate>                                
                                     </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Type">
                                  <ItemTemplate>
                                      <asp:Label ID="lblMsg" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                  </ItemTemplate>                                
                                     </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mapping" ItemStyle-Width="5%">
                                  <ItemTemplate>
                                      <asp:Checkbox ID="chkStatus" runat="server" Checked='<%# Eval("Productsheetstatus").ToString() == "0"?false:true %>' ToolTip='<%# Eval("ItemType_id").ToString()%>' OnCheckedChanged="chkStatus_CheckedChanged" AutoPostBack="true"></asp:Checkbox>
                                  </ItemTemplate>                                
                                     </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>


