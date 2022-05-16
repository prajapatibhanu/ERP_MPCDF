<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UnionwiseLitretoKgVariantFactorMaster.aspx.cs" Inherits="mis_dailyplan_UnionwiseLitretoKgVariantFactorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Variant Wise Litre to Kg Conversion Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                           <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvsocietyName" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            
                            <div class="col-md-3">
                                    <label>Variant Type<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvVariant" runat="server" Display="Dynamic" ControlToValidate="ddlVariant" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Variant!'></i>" ErrorMessage="Select Variant" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlVariant"  CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Litre to Kg Conversion Factor<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvLitretoKgConversionFactor" runat="server" Display="Dynamic" ControlToValidate="txtLitretoKgConversionFactor" Text="<i class='fa fa-exclamation-circle' title='Enter Litre to Kg Conversion Factor!'></i>" ErrorMessage="Enter Litre to Kg Conversion Factor" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtLitretoKgConversionFactor" onkeypress="return validateDec(this,event)" autocomplete="off" placeholder="Enter Litre to Kg Conversion Factor" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Fat Factor<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvFatFactor" runat="server" Display="Dynamic" ControlToValidate="txtFatFactor" Text="<i class='fa fa-exclamation-circle' title='Enter Fat Factor!'></i>" ErrorMessage="Enter Fat Factor" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtFatFactor" onkeypress="return validateDec(this,event)" autocomplete="off" placeholder="Enter Fat Factor" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Snf Factor<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvSnffactor" runat="server" Display="Dynamic" ControlToValidate="txtSnffactor" Text="<i class='fa fa-exclamation-circle' title='Enter Snf Factor!'></i>" ErrorMessage="Enter Fat Factor" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtSnffactor" onkeypress="return validateDec(this,event)" autocomplete="off" placeholder="Enter Snf Factor" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                           </div>
                       
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" ValidationGroup="a" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();"/>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Variant Type">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Litre to Kg Conversion Factor">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblLtrtoKgFactor" runat="server" Text='<%# Eval("LtrtoKgFactor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Fat Factor">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblFatFactor" runat="server" Text='<%# Eval("FatFactor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Snf Factor">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblSnfFactor" runat="server" Text='<%# Eval("SnfFactor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="lnkedit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("LtrtoKgFactor_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

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

