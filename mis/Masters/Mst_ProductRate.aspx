<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Mst_ProductRate.aspx.cs" Inherits="mis_Masters_Mst_ProductRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
     <script type="text/javascript">
         function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
             for (i = 1; i < GridView2.rows.length; i++) {
                 GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
             }
         }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>

    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />


    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- SELECT2 EXAMPLE -->
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Product Sale Rate </h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>

                                <legend>Product Sale Rate</legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                      <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                   <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Name <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="b"
                                                    InitialValue="0" ErrorMessage="Select Item Name" Text="<i class='fa fa-exclamation-circle' title='Select Item Name !'></i>"
                                                    ControlToValidate="ddlItemName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlItemName" CssClass="form-control select2" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                  
                                    
                                  <div class="col-md-1" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="b" ID="btnSearch" Text="Search" AccessKey="S" />
                                    </div>
                                </div> 
                                     <div class="col-md-1" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-secondary" OnClick="btnClear_Click" ID="btnClear" Text="Clear" AccessKey="C" />
                                    </div>
                                </div>    
                                
                                </div>
                               
                                <div class="row" id="pnlproduct" runat="server" visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                All
                                                 <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate Type">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblProductRateTypeName" runat="server" Text='<%# Eval("ProductRateTypeName") %>'></asp:Label>                                                 
                                                <asp:Label ID="lblItem_id" Visible="false" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                 <asp:Label ID="lblProductRateTypeId" Visible="false" runat="server" Text='<%# Eval("ProductRateTypeId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField>
                                              <HeaderTemplate>
                                               Including GST Rate <asp:CustomValidator ID="CustomValidator2" runat="server" Text="<i class='fa fa-exclamation-circle' title='Please Enter at least one IncludingGST Rate. !'></i>" ValidationGroup="a" ErrorMessage="Please Enter at least one Supply Return Qty."
                                            ClientValidationFunction="IncludeingGSTValidate" ForeColor="Red"></asp:CustomValidator>
                                                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtIncludingGSTRate" autocomplete="off" onkeypress="return validateDecTwoplace(this,event)" MaxLength="10" placeholder="Enter Consumer Rate" ValidationGroup="a" Text='<%# Eval("RateIncludingGST") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="pull-right">
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator54J" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" ValidationGroup="a" runat="server" ControlToValidate="txtIncludingGSTRate" ErrorMessage="Enter Valid Consumer Rate" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Rate!'></i>"></asp:RegularExpressionValidator>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                        <asp:TemplateField HeaderText="Effective from Date">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEffectiveDate" Text='<%# Eval("EffectiveDate") %>' onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select Effective from Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                        
                                    </div>
                                </div>

                                 <div class="row" id="pnlbtn" runat="server" visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
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
     <script type="text/javascript">
         window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
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
         // function validateDecThreeplace(el, evt) {
        function validateDecTwoplace(el, evt) {
            var digit = 2;
            //var digit = 3;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }

        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
             var checkBoxes = gridView.getElementsByTagName("input");
             for (var i = 0; i < checkBoxes.length; i++) {
                 if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }
         function IncludeingGSTValidate(sender, args) {
             var gridView = document.getElementById("<%=GridView1.ClientID %>");
            var txtreturnqty = gridView.getElementsByTagName("input");
            for (var i = 0; i < txtreturnqty.length; i++) {
                if (txtreturnqty[i].value != "" && txtreturnqty[i].type == "text") {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
 </script>
</asp:Content>
