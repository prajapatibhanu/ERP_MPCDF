<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Product_Generated_Recipe.aspx.cs" Inherits="mis_CattleFeed_CFP_Product_Generated_Recipe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script type="text/javascript">
        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }
    </script>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">View Product Recipe(प्रोडक्ट की विधि)</h3>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Product Recipe(प्रोडक्ट की विधि)
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
                                            <asp:DropDownList ID="ddlProdUnit" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group"  style="margin-top: 17px;">
                                        <asp:Button ID="btnview" runat="server" Text="Generate Recipe" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="a" OnClick="btnview_Click" />
                                            </div>
                                    </div>
                                   
                                </div>
                                
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12" style="text-align: center;">
                                    <asp:HiddenField ID="hdnvalue" runat="server" Value="0" />
                                    <asp:GridView ID="gvProductItems" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvProductItems_RowCommand"
                                        EmptyDataText="No Record Found.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.(क्रं)">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Product(प्रोडक्ट )" ItemStyle-Width="30%" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Product Unit(प्रोडक्ट की इकाई)" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="ProductQuantity" HeaderText="Quanity(in MT)(मात्रा)" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="TranactionAT" HeaderText="Generated Date(बनने की दिनांक)" ItemStyle-Width="30%" />
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                      <asp:LinkButton ID="btnview" runat="server" Text="View" ToolTip="View Recipe for selected Product" CssClass="btn btn-info" CommandArgument='<%#Eval("CFPProductRecipeID")+"|"+Eval("ItemName")+"|"+Eval("ProductQuantity") %>' CommandName="Detail" ><i class="fa  fa-camera"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Recipe Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Recipe Details for <asp:Label ID="lblpro" CssClass="Autoclr" runat="server"></asp:Label></span>
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 100%" colspan="2">
                                        <asp:GridView ID="grd" CssClass="table table-bordered" runat="server" EmptyDataText="No Data Available" Width="100%" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ItemName" HeaderText="वस्तु का नाम " />
                                               
                                                <%--<asp:BoundField DataField="ItemRatio" HeaderText="अनुपात" />--%>
                                                <asp:BoundField DataField="ProductPercentage" HeaderText="अनुपात की मात्रा" />
                                                <asp:BoundField DataField="ItemQuantity" HeaderText="आवश्यक मात्रा (in MT)" />
                                                 <asp:BoundField DataField="AvailableQuantity" HeaderText="उपलब्ध मात्रा (in MT)" />

                                            </Columns>

                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>


                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            
                             <asp:Button runat="server" ID="btnExport" class="btn btn-success" Text="Export to Excel" OnClick="btnExport_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

