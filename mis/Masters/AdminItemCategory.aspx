<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminItemCategory.aspx.cs" Inherits="mis_Admin_AdminItemCategory" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
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
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="row">
            <div class="col-md-6">
                 <div class="box box-Manish">
                
                    <div class="box-header">
                        <h3 class="box-title">Item Category </h3>
                    </div>
                     <!-- /.box-header -->
                         <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                        <label>Item Category<span class="text-danger"> *</span></label>
                                        <asp:TextBox ID="txtItem_Category" runat="server" placeholder="Item Category..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtItem_Category" class="text-danger"></span></small>
                                    </div>
                                </div>
                          
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" style="margin-top:20px"  ID="btnSave" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="AdminItemCategory.aspx" style="margin-top:20px" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                   </div>
            </div>
                </div>
             <div class="col-md-6">
                 <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Item Details</h3>
                    
           </div>
                            <!-- /.box-header -->
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" PageSize="20" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="ItemCat_id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemCatName" HeaderText="Item Category" />
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" class="fa fa-pencil" CausesValidation="False" CommandName="Select" ></asp:LinkButton>                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                              </div></div>
                            </div>
                        </div>
                    </div>
                    
            
        
         </div>
                </div>
            </section>
      </div>
    
           
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script type="text/javascript">
        function validateform() {
            var msg = "";
            $("#valtxtItem_Category").html("");
            if (document.getElementById('<%=txtItem_Category.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Category. \n";
                $("#valtxtItem_Category").html("Enter Item Category");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function tbx_fnAlphaOnly(e, cntrl) { if (!e) e = window.event; if (e.charCode) { if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122) { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } } } else if (e.keyCode) { if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122) { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } } } }
    </script>
    <script src="../js/ValidationJs.js"></script>
</asp:Content>

