<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VehicleType.aspx.cs" Inherits="mis_Common_VehicleType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" OnClick="btnSubmit_Click" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
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
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Vehicle Type Master</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label>Vehicle Type Name<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                ErrorMessage="Enter Vehicle Type Name" Text="<i class='fa fa-exclamation-circle' title='Enter Vehicle Type Name !'></i>"
                                                ControlToValidate="txtVehicleTypeName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtVehicleTypeName"
                                                ErrorMessage="Only alphabet allow" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allow. !'></i>"
                                                SetFocusOnError="true" ValidationExpression="^[0-9a-zA-Z\s]+$">
                                            </asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox ID="txtVehicleTypeName" CssClass="form-control" MaxLength="60" runat="server" placeholder="Enter Vehicle Type Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label></label>
                                        <span class="pull-right">
                                 <asp:RequiredFieldValidator ID="rfvSContainerTypeName" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="RblContainerTypeName" ErrorMessage="Select Container Type" Text="<i class='fa fa-exclamation-circle' title='Select Container Type!'></i>">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <fieldset>
                                            <legend>Container Type</legend>
                                            <asp:RadioButtonList runat="server" ID="RblContainerTypeName" RepeatDirection="Horizontal" CssClass="pull-left" AutoPostBack="true" ClientIDMode="Static"> 
                                                <asp:ListItem Value="1">Referigrated &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Non Referigrated</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <hr />
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-block btn-primary" ValidationGroup="a" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" href="VehicleType.aspx">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Vehicle Type Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" CssClass="datatable table table-striped table-bordered table-hover"
                                    EmptyDataText="No Record Found." DataKeyNames="VehicleType_ID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VehicleType Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVehicleTypeID" Visible="false" runat="server" Text='<%# Eval("VehicleType_ID") %>' />
                                                <asp:Label ID="lblVehicleTypeName" runat="server" Text='<%# Eval("VehicleType_Name") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Container Type Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContainerTypeName" runat="server" Text='<%# Eval("ContainerType_Name") %>' />
                                                <asp:Label ID="lblContainerTypeId" runat="server" Visible="false" Text='<%# Eval("ContainerType_Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("VehicleType_ID") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("VehicleType_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- /.box-body -->
        </section>
        <!-- /.content -->
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

