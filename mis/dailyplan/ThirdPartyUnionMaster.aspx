<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ThirdPartyUnionMaster.aspx.cs" Inherits="mis_dailyplan_ThirdPartyUnionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
   <style>
        .customCSS td {
            padding: 0px !important;
        }

        .customCSS label {
            padding-left: 10px;
        }

        table {
            white-space: nowrap;
        }

        .capitalize {
            text-transform: capitalize;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click"  Style="margin-top: 20px; width: 50px;" />
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
                   <h3 class="box-title">THIRD PARTY UNION MASTER</h3>
               </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>THIRD PARTY UNION DETAIL</legend>
                        <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Third Party Union Name<span class="text-danger">*</span></label>
                               <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvThirdPartyUnion_Name" runat="server" Display="Dynamic" ControlToValidate="txtThirdPartyUnion_Name" Text="<i class='fa fa-exclamation-circle' title='Enter Third Party Union Name!'></i>" ErrorMessage="Enter Third Party Union Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                        <asp:TextBox ID="txtThirdPartyUnion_Name" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-group">
                                <label>Third Party Union Address</label>
                              <%-- <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvThirdPartyUnion_Address" runat="server" Display="Dynamic" ControlToValidate="txtThirdPartyUnion_Address" Text="<i class='fa fa-exclamation-circle' title='Enter Third Party Union Address!'></i>" ErrorMessage="Enter Third Party Union Address" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>--%>
                                        <asp:TextBox ID="txtThirdPartyUnion_Address" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"  MaxLength="200"></asp:TextBox>
                            </div>
                        </div>
                            <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Third Party GST No.</label>
                              
                                        <asp:TextBox ID="txtGST" runat="server" autocomplete="off" ClientIDMode="Static" CssClass="form-control"  MaxLength="16"></asp:TextBox>
                                    </div>
                                </div>
                        <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" style="margin-top:20px;" ValidationGroup="Save" OnClientClick="return ValidatePage();"  Text="Save"/>
                                    </div>
                                </div>
                    </div>
                    </fieldset>
                    
                    <fieldset>
                        <legend>THIRD PARTY UNION LIST</legend>
                        <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="Gridview1" PageSize="50" AllowPaging="True" runat="server" DataKeyNames="ThirdPartyUnion_Id" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="Gridview1_RowCommand" OnPageIndexChanging="Gridview1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Third Party Union Name" DataField="ThirdPartyUnion_Name" />
                                                <asp:BoundField HeaderText="Third Party Union Address" DataField="ThirdPartyUnion_Address" /> 
                                                  <asp:BoundField HeaderText="Third Party GST No." DataField="TPGSTno" />                                                                                               
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" Text="Edit" CommandName="EditDetail" CommandArgument='<%# Eval("ThirdPartyUnion_Id") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                          
                                            </Columns>
                                        </asp:GridView>
                            </div>
                        </div>
                    </div>
                    </fieldset>
                    
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

