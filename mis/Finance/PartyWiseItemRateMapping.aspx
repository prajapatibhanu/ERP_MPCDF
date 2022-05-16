<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PartyWiseItemRateMapping.aspx.cs" Inherits="mis_Finance_PartyWiseItemRateMapping" %>

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
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Party Wise Item Rate Mapping</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                                        <div class="form-group">
                                            <label>RetailerType<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlRetailerType" Text="<i class='fa fa-exclamation-circle' title='Select RetailerType!'></i>" ErrorMessage="Select RetailerType" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                            <asp:DropDownList ID="ddlRetailerType" CssClass="form-control select2" runat="server" ClientIDMode="Static">
                                            </asp:DropDownList>                                         
                                            
                                        </div>
                                    </div>
                        <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                              <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                               <%-- <asp:TextBox ID="txtVoucherTx_Date" runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" onchange="CompareSupplierInvocieDate();" AutoPostBack="true"></asp:TextBox>--%>
                                                 <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                    </div>                 
                                    <fieldset>
                                        <legend>Item Detail</legend>
                                      
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Item Name<span style="color: red;"> *</span></label><br />
                                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlItem" Text="<i class='fa fa-exclamation-circle' title='Select Item!'></i>" ErrorMessage="Select Item" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                        </span>
                                                        <asp:DropDownList ID="ddlItem" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                              
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Rate Per<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtRate" Text="<i class='fa fa-exclamation-circle' title='Enter Rate'></i>" ErrorMessage="Enter Rate" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
                                        </span>
                                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" onkeypress="return validateDec(this,event);" ID="txtRate" placeholder="Enter Rate..."  autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" class="btn btn-success" Style="margin-top: 25px;" ValidationGroup="b" runat="server" OnClick="btnAdd_Click" Text="Add"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                       
                                        <!-- ItemDetail GridView-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblItemRowNo" Text='<%# Eval("ID").ToString()%>' CssClass="hidden" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>                                                                 
                                                                </ItemTemplate>

                                                            </asp:TemplateField>                                                          
                                                            <asp:TemplateField HeaderText="Rate Per">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                                   
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- End-->
                                    </fieldset>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" ValidationGroup="a" Text="Save" OnClientClick="return ValidatePage();" OnClick="btnSave_Click"/>
                                        </div>
                                    </div>
                                </div>
                </div>
                
            </div> 
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Party Wise Item Rate Mapping Details</h3>
                </div>
                <div class="box-body">

                            <div class="row">
                                <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Retailer Type<span style="color: red;"> *</span></label>                                         
                                            <asp:DropDownList ID="ddlRetailerTypeflt" CssClass="form-control select2" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlRetailerTypeflt_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>                                         
                                            
                                        </div>
                                    </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                    <asp:GridView ID="gvItemRateDetail" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="ItemRateMapping_ID" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="RetailerType">
                                                <ItemTemplate>
                                                    <%# Eval("RetailerTypeName") %> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <%# Eval("ItemName") %> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <%# Eval("Rate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    
                                            <asp:TemplateField HeaderText="Effective Date">
                                                <ItemTemplate>
                                                    <%# Eval("EffectiveDate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

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

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

