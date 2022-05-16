<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_GenerateRecipie.aspx.cs" Inherits="mis_Masters_Mst_GenerateRecipie" %>


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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes"  Style="margin-top: 20px; width: 50px;" />
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
                        <h3 class="box-title">Generate Recipe / विधि तैयार करें</h3>
                    </div>
                          <!-- /.box-header -->


                      <div class="box-body">
                        <fieldset>
                            <legend>Enter Data</legend>
                               <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                       <div class="row">

                                    <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="b"
                                                ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="b" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off"  AutoPostBack="true" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Enter Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" data-date-start-date="1d" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                             <div class="col-md-3" id="pnlItem" runat="server">
                                    <div class="form-group">
                                        <label>Product Name/ प्रोडक्ट का नाम<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                InitialValue="0" ErrorMessage="Select Item" Text="<i class='fa fa-exclamation-circle' title='Select Item !'></i>"
                                                ControlToValidate="ddlProductName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlProductName" AutoPostBack="true"  OnInit="ddlProductName_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>

                              <div class="col-md-3">
                                       <div class="form-group">
                                        <label>Quantity/ मात्रा<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                                InitialValue="0" ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>"
                                                ControlToValidate="txtQty" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:TextBox ID="txtQty"  runat="server" autocomplete="off" CssClass="form-control" MaxLength="20" ></asp:TextBox>
                                                 
                                    </div>

                                </div>


                           </div>


                            </fieldset>


                               <div class="row">
                                <div class="col-md-2" id="pnlSubmit" runat="server" >
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary"  ID="btnSubmit" OnClick="btnSubmit_Click" Text="Generate" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                           <%--     <div class="col-md-2" id="pnlClear" runat="server" >
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-primary" />
                                    </div>
                                </div>--%>
                            </div>
                          </div>
                    </div>
                    </div>


                <div class="row" id="pnlProduct" runat="server" visible="false">
                         
                                <div class="col-md-12">
                                      <fieldset>
                            <legend><asp:Label ID="lblCartMsg" Text="Recipie List" runat="server"></asp:Label>
                            </legend>
                                    <div class="table-responsive" >
                                        <table class="table table-hover table-bordered pagination-ys">
                                            <tr>   
                                                <th>SNo. / क्र.</th> 
                                                 <th>Raw Materials (Items)</th> 
                                                 <th>Lassi (2000 Packets)</th> 
                                                 <%--  <th>Item Category / वस्तु वर्ग</th> 
                                                <th>Item Name/ वस्तु नाम </th>    <th>Retailer Type/ विक्रेता प्रकार </th> 
                                                <th>Retailer Name/ विक्रेता नाम </th>    <th>Rate/ दाम</th> 
                                                <th>GST Rate/ GST दाम</th>--%>   
                                                 <%--<th>Date / दिनांक </th> --%>
                                                
                                            </tr> 
                                           

                                              
                                             <tr>  
                                                  <td>1</td>    <td>Curd</td> 
                                                 <td>150 KG</td> 
                                                  <%--  <td>2/27/2020</td> 
                                                <td>Process Done</td>--%>

                                                   </tr> 
                                              <tr>  
                                                  <td>2</td>    <td>Flavour</td> 
                                                 <td>50 KG</td> 
                                                  <%--  <td>2/27/2020</td> 
                                                <td>Process Done</td>--%>

                                                   </tr>    
                                              <tr>  
                                                  <td>3</td>    <td>Sugar</td> 
                                                 <td>200 KG</td> 
                                                  <%--  <td>2/27/2020</td> 
                                                <td>Process Done</td>--%>

                                                   </tr>  
                                            
                                              <tr>  
                                                  <td></td>    <td style="font-weight:bold" >Total</td> 
                                                 <td style="font-weight:bold">750KG</td> 
                                                  <%--  <td>2/27/2020</td> 
                                                <td>Process Done</td>--%>

                                                   </tr>                                                           
                                        </table>

                                        <%--<asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered pagination-ys"
                                            ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo. / क्र." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='1' runat="server" />
                                               
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Category / वस्तु वर्ग ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Category" runat="server" Text='Milk' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name/ वस्तु नाम ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="ItemName" runat="server" Text='FCM 500 ML' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer Type/ विक्रेता प्रकार ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="Type" runat="server" Text='Institution' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retailer Name/ विक्रेता नाम ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='Health' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Rate/ दाम ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="Rate" runat="server" Text='24' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GST(Y/N) ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="GST" runat="server" Text='Yes' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="GST Rate/ GST दाम ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="GSTRate" runat="server" Text='1.50' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Effective Date / प्रभावी दिनांक ">
                                                   <ItemTemplate>
                                                        <asp:Label ID="Effective" runat="server" Text='2/27/2020' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Remarks" runat="server" Text='Process Done' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>--%>
                                    </div>
                                           </fieldset>
                                </div>
                            </div>
                </div>

        </section>
         </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

