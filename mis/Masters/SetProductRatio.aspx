    <%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SetProductRatio.aspx.cs" Inherits="mis_Masters_SetProductRatio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }
        .columnred {
            background-color: #f05959 !important;
        }
         .columnmilk {
            background-color: #bfc7c5 !important;
        }
        .columnproduct {
            background-color: #f5f376 !important;
        }

        
    </style>
  

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
       <%--Confirmation Modal Start --%>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
     <%--ConfirmationModal End --%>

      <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />


    <div class="content-wrapper">
        <section class="content">
            <div class="row">

                <div class="col-md-12">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title"> Product Set Ratio </h3>
                    </div>
                          <!-- /.box-header -->
                      <div class="box-body">
                        <fieldset>
                            <legend>Select Product</legend>
                               <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                               <div class="row">
                                         <div class="col-md-3" id="pnlItem" runat="server">
                                    <div class="form-group">
                                        <label>Product Name/ प्रोडक्ट का नाम<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                ControlToValidate="ddlProductName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlProductName" AutoPostBack="true"  OnInit="ddlProductName_Init" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>




                                   <div class="row" id="pnlProduct"  runat="server" visible="false">
            <div class="col-md-12">
          
        
                <!-- /.box-header -->
              
                
                    <div class="table-responsive">
                       <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="ProdSpec_id" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">  
                                               <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  

                                                    <asp:TemplateField HeaderText="Prod Name" runat="server" visible="false">
                                    <ItemTemplate>
                                         <asp:Label ID="lblProd_Id"  Text='<%# Eval("Prod_Id") %>' runat="server" Visible="false"/>
                                        <asp:Label ID="lblIngredientsId" runat="server" Text='<%# Eval("IngredientsId") %>' Visible="false" />
                                          <asp:Label ID="lblUnit_ID" runat="server" Text='<%# Eval("Unit_ID") %>'  Visible="false" />

                                    </ItemTemplate>
                                </asp:TemplateField>


                                              <%--  <asp:TemplateField HeaderText="Product Name/ प्रोडक्ट का नाम">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblProduct_Name" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Items / वस्तु">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIngredientsName" runat="server" Text='<%# Eval("IngredientsName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Item Ratio/ वस्तु अनुपात">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--   
                                                 <asp:TemplateField HeaderText="Ratio Value/ अनुपात मूल्य">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRatioValue" runat="server" Text='<%# Eval("Ratio_Value") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderText="Ratio Value/ अनुपात मूल्य">
                                                       <ItemTemplate>
                                                           <%--  <asp:Label ID="lblItemQty" Text='<%# Eval("Ratio_Value")%>' runat="server" />--%>
                                                                                <%--<asp:RequiredFieldValidator ID="rfv1" ValidationGroup="c"
                                                                                    ErrorMessage="Enter Item Qty." Enabled="false" Text="<i class='fa fa-exclamation-circle' title='Enter Item Qty. !'></i>"
                                                                                    ControlToValidate="txtItemQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                                                </asp:RequiredFieldValidator>--%>
                                                                                <asp:RegularExpressionValidator ID="rev1" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="c"
                                                                                    ErrorMessage="First digit can't be 0(Zero)!" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='First digit can't be 0(Zero)!'></i>" 
                                                                                    ControlToValidate="gv_txtRatio"
                                                                                    ValidationExpression="^[1-9][0-9]*$">
                                                                                </asp:RegularExpressionValidator>
                                                                                </span>
                                                                         <%--<asp:TextBox runat="server" autocomplete="off" Visible="false" CausesValidation="true" 
                                                                             Text='<%# Eval("Ratio_Value")%>' CssClass="form-control" ID="txtItemQty" MaxLength="5" 
                                                                             onkeypress="return validateNum(event);" placeholder="Enter Item Qty." ClientIDMode="Static"></asp:TextBox>--%>

                                                           <asp:TextBox ID="gv_txtRatio" onfocusout="FetchData(this)" onkeypress="return validateNum(event);" 
                                                               runat="server" autocomplete="off" CssClass="form-control" MaxLength="10" placeholder="Enter Value"></asp:TextBox>
                                                                            </ItemTemplate>
                                                    </asp:TemplateField>


                                            
                                            </Columns>
                                        </asp:GridView>
                    </div>
                      <div  class="col-md-12 " id="pnlSubClear" runat="server" >
                      <div class="col-md-1 " id="pnlSubmit" runat="server" >
                                  <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" 
                                           ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-1" id="pnlClear" runat="server"  >
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-primary" />
                                    </div>
                                </div>
                          </div>


                      


                   
            </div>

</div>
                                   
                                   </div>


                            </fieldset>
                          
                          
                          




                          </div>
                    </div>
                    </div>
                </div>


            




            <div class="row">
            
                <div class="col-md-12">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Production Ratio Details</h3>
                    
           </div>
                <!-- /.box-header -->

                <div class="box-body">
                 <div class="row">
                     
                               <div class="col-md-3">
                                    <div class="form-group">
                                        <label> Product Name/ प्रोडक्ट का नाम<span style="color: red;"> *</span></label>
                                        
                                           
                                        <asp:DropDownList ID="ddlFilterProduct" AutoPostBack="true" OnInit="ddlFilterProduct_Init" OnSelectedIndexChanged="ddlFilterProduct_SelectedIndexChanged"   runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>           
      <div class="col-md-12">
                    <div class="table-responsive">
                       <asp:GridView ID="GridView2" DataKeyNames="ProdSpec_id" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found.">  
                                               <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                       <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  

                                                    <asp:TemplateField HeaderText="Prod Name" runat="server" visible="false">
                                    <ItemTemplate>
                                         <asp:Label ID="lblProdId"  Text='<%# Eval("Prod_Id") %>' runat="server" Visible="false"/>
                                        <asp:Label ID="lblIngId" runat="server" Text='<%# Eval("IngredientsId") %>' Visible="false" />
                                          <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("Unit_ID") %>'  Visible="false" />

                                    </ItemTemplate>
                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Product Name/ प्रोडक्ट का नाम">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblProduct_Name" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Items / वस्तु">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIngName" runat="server" Text='<%# Eval("IngredientsName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Item Ratio/ वस्तु अनुपात">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--   
                                                 <asp:TemplateField HeaderText="Ratio Value/ अनुपात मूल्य">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRatioValue" runat="server" Text='<%# Eval("Ratio_Value") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderText="Ratio Value/ अनुपात मूल्य">
                                                       <ItemTemplate>
                                                          
                                                                             
                                                            <asp:Label ID="lblRatio" runat="server" Text='<%# Eval("Ratio_Value") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                    </asp:TemplateField>


                                            
                                            </Columns>
                                        </asp:GridView>
                    </div>
          </div></div>


                    </div>
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
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {
                
                 if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

