<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RetailerLedgerMapping.aspx.cs" Inherits="mis_Finance_RetailerLedgerMapping" %>

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
                    <h3 class="box-title">Retailer Ledger Mapping</h3>
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
                                            <label>Ledger<span style="color: red;"> *</span></label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlLedger" Text="<i class='fa fa-exclamation-circle' title='Select Ledger!'></i>" ErrorMessage="Select Ledger" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                            <asp:ListBox ID="ddlLedger" CssClass="form-control" runat="server" ClientIDMode="Static" SelectionMode="Multiple">
                                            </asp:ListBox>                                         
                                            
                                        </div>
                                    </div>
                        
                    </div>                 
                                    
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
                    <h3 class="box-title">Retailer Ledger Mapping Details</h3>
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

                                    <asp:GridView ID="gvRetailerLedgerMappingDetail" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="RetailerLedgerMapping_ID" Text='<%# Eval("RetailerLedgerMapping_ID") %> ' runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Retailer Type">
                                                <ItemTemplate>
                                                    <%# Eval("RetailerTypeName") %> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ledger Name">
                                                <ItemTemplate>
                                                    <%# Eval("Ledger_Name") %> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active/InActive">
                                                <ItemTemplate>
                                                   <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true" Checked='<%# Eval("IsActive").ToString()=="True"?true:false %>' OnCheckedChanged="chkActive_CheckedChanged"/>
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
        <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlLedger]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
</asp:Content>

