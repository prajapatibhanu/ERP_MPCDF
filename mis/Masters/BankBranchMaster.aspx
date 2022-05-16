<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BankBranchMaster.aspx.cs" Inherits="mis_Masters_BankBranchMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script src="../../js/datatables.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datatable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Bank Branch Master Entry</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Bank Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDivision" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlBank" InitialValue="0" ErrorMessage="Select Bank." Text="<i class='fa fa-exclamation-circle' title='Please Select Bank !'></i>"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlBank" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Branch Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                        ErrorMessage="Enter Branch Name" Text="<i class='fa fa-exclamation-circle' title='Enter Branch Name !'></i>"
                                                        ControlToValidate="txtBranchName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBranchName"
                                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtBranchName" CssClass="form-control" MaxLength="60" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Branch Code</label>
                                                <span class="pull-right">
                                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Branch Name" Text="<i class='fa fa-exclamation-circle' title='Enter Branch Code !'></i>"
                                                        ControlToValidate="txtBranchCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBranchCode"
                                                        ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[0-9]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtBranchCode" onkeypress="return validateNum();" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>IFSC Code<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter IFSC Code" Text="<i class='fa fa-exclamation-circle' title='Enter IFSC Code !'></i>"
                                                        ControlToValidate="txtIFSCCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                   <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                                        ErrorMessage="IFSC Code Formate is Not Valid" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='IFSC Code Formate is Not Valid. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[A-Z|a-z]{4}[0][\d]{6}$">
                                                    </asp:RegularExpressionValidator>--%>
                                                    
                                                </span>
                                                <asp:TextBox ID="txtIFSCCode" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Active Status<span style="color: red;"> *</span></label><br />
                                                <asp:CheckBox runat="server" ID="chkIsActive" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-block btn-primary" ValidationGroup="a" Text="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="container-fluid">
                                <div class="card card-primary">
                                    <div class="card-header">
                                        <h3 class="card-title">Bank Branch Details</h3>
                                    </div>
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-bordered datatable"
                                                EmptyDataText="No Record Found." DataKeyNames="Branch_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbankid" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                                            <asp:Label ID="lblbbankid" Visible="false" runat="server" Text='<%# Eval("Branch_id") %>' />
                                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBranchCode" runat="server" Text='<%# Eval("BranchCode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IFSC Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIFSC" runat="server" Text='<%# Eval("IFSC") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IsActive" HeaderText="Active Status" />
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpdate" CommandName="Update" runat="server" CssClass="button button-mini button-green" CommandArgument='<%#Eval("Branch_id") %>'><i class="btn btn-info fa fa-edit"></i></asp:LinkButton>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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

