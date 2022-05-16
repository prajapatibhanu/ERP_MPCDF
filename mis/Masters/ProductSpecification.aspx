<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProductSpecification.aspx.cs" Inherits="mis_Masters_ProductSpecification" %>

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

        .btnmargin{ margin-top:20px !imporatant;


        }
    </style>
  
    <%--<style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: #0f62ac !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>--%>
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
                        <h3 class="box-title">Add Product Specificaion </h3>
                    </div>
                          <!-- /.box-header -->
                      <div class="box-body">
                        <fieldset>
                             <legend>Enter Product, Ingredient, Item Ratio</legend>
                               <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-md-3" id="pnlCategory" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Item Category / वस्तु वर्ग<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>

                                   <div class="col-md-3" id="pnlItem" runat="server">
                                    <div class="form-group">
                                        <label>Product Name/ प्रोडक्ट का नाम<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Item"
                                                 Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                ControlToValidate="ddlProductName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlProductName" AutoPostBack="true"  OnInit="ddlProductName_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3" >
                                       <div class="form-group">
                                        <label>Item Name / वस्तु का नाम<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                                InitialValue="0" ErrorMessage="Select Ingredients" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Select Ingredients !'></i>"
                                                ControlToValidate="ddlNameofIngredients" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlNameofIngredients" AutoPostBack="true" OnInit="ddlNameofIngredients_Init"
                                             runat="server" CssClass="form-control select2">
                                            
                                            
                                        </asp:DropDownList>
                                    </div>

                                </div>


                                  <div class="col-md-3" >
                                       <div class="form-group">
                                        <label>Item Ratio/ वस्तु अनुपात<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                InitialValue="0" ErrorMessage="Select Ratio" Text="<i class='fa fa-exclamation-circle' title='Select Ratio !'></i>"
                                                ControlToValidate="ddlItemRatio" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlItemRatio" AutoPostBack="true" OnInit="ddlItemRatio_Init"
                                             runat="server" CssClass="form-control select2">
                                            
                                            
                                        </asp:DropDownList>
                                    </div>

                                </div>




                              

                             <div class="col-md-1 " id="pnlSubmit" runat="server" >
                                  <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" 
                                            style="margin-top:20px" OnClick="btnSubmit_Click" 
                                            ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-1" id="pnlClear" runat="server" style="margin-top:20px" >
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-primary" />
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
                    <h3 class="box-title">Product Specification  Details</h3>
                    
           </div>
                <!-- /.box-header -->

                <div class="box-body">
                  
      
                    <div class="table-responsive">
                       <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" DataKeyNames="ProdSpec_id" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
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


                                                <asp:TemplateField HeaderText="Product Name/ प्रोडक्ट का नाम">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblProduct_Name" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name / वस्तु का नाम">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIngredientsName" runat="server" Text='<%# Eval("IngredientsName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Item Ratio/ वस्तु अनुपात">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%# Eval("ProdSpec_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkDelete" CommandArgument='<%# Eval("ProdSpec_id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
     <script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('a');
             }

             if (Page_IsValid) {

                 if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                       $('#myModal').modal('show');
                       return false;
                   }
               }
           }
    </script>
</asp:Content>

