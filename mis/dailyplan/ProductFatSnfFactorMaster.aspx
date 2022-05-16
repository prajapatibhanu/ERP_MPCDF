<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductFatSnfFactorMaster.aspx.cs" Inherits="mis_dailyplan_ProductFatSnfFactorMaster" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
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
                    <div class="box box-success">
                        <div class="box-header">
                                <h3 class="box-title">PRODUCT FAT SNF FATCOR MASTER</h3>
                            </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-md-3">
                                    <label>Section<span style="color: red;">*</span></label>
                                  <%--  <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProductSection" OnInit="ddlProductSection_Init" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <label>Product<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ControlToValidate="ddlProduct" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Product!'></i>" ErrorMessage="Select Product" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProduct"  OnInit="ddlProduct_Init" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Fat %</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtxtFat" ValidationGroup="Save"
                                                 ErrorMessage="Enter Fat" Text="<i class='fa fa-exclamation-circle' title='Enter Fat !'></i>"
                                                ControlToValidate="txtFat" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txtFat" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" MaxLength="5" ValidationGroup="Save"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Snf %</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvtxtSnf" ValidationGroup="Save"
                                                 ErrorMessage="Enter Distance" Text="<i class='fa fa-exclamation-circle' title='Enter Snf !'></i>"
                                                ControlToValidate="txtSnf" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txtSnf" runat="server" CssClass="form-control" onkeypress="return validateDec(this,event)" MaxLength="5" ValidationGroup="Save"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Save" ValidationGroup="Save" style="margin-top:20px;"/>
                                    </div>
                                </div>
                                                   </div>
                                               </div>
                        <div class="box-body">
                            <asp:GridView ID="gvDetail" runat="server" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="gvDetail_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductSection_Name" runat="server" Text='<%# Eval("ProductSection_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemTypeName" runat="server" Text='<%# Eval("ItemTypeName") %>'></asp:Label>
                                             <asp:Label ID="lblItemType_id" CssClass="hidden" runat="server" Text='<%# Eval("ItemType_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fat">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Snf">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSnf" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ProdFatSnfFactID") %>' CommandName="EditRecord" ><i class="fa fa-edit"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
  <script type="text/javascript">
      function ValidatePage() {

          if (typeof (Page_ClientValidate) == 'function') {
              Page_ClientValidate('Save');
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

