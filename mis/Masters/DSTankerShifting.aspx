<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DSTankerShifting.aspx.cs" Inherits="mis_Masters_DSTankerShifting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">TRANSFER TANKER</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>FILTER</legend>
                                <div class="row">
                                   <div class="col-md-2">
                                <div class="form-group">
                                    <label>From DS<span style="color: red">*</span></label>
                                     <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFromds" ValidationGroup="Save"
                                                        InitialValue="0" ErrorMessage="Select From DS" Text="<i class='fa fa-exclamation-circle' title='Select From DS !'></i>"
                                                        ControlToValidate="ddlFromds" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlFromds" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                                    <div class="col-md-2">
                                <div class="form-group">
                                    <label>Tanker No<span style="color: red">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                        InitialValue="0" ErrorMessage="Select Tanker No" Text="<i class='fa fa-exclamation-circle' title='Select Tanker No !'></i>"
                                                        ControlToValidate="ddlV_VehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlV_VehicleNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                                    <div class="col-md-2">
                                <div class="form-group">
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                        InitialValue="0" ErrorMessage="Select To DS" Text="<i class='fa fa-exclamation-circle' title='Select To DS !'></i>"
                                                        ControlToValidate="ddlTods" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></span>
                                    <label>To DS<span style="color: red">*</span></label>
                                    <asp:DropDownList ID="ddlTods" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                                     <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="Save" ID="btnSubmit" style="margin-top:20px;" OnClick="btnSubmit_Click" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                        <a href="DSTankerShifting.aspx" style="margin-top:20px;" class="btn btn-default" >Clear</a>
                                    </div>
                                </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="box-body">

                            <fieldset>
                                <legend>TRANSFER TANKER HISTORY</legend>
                                <div class="col-md-12">
                                <div class="form-group">
                                     <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnExcel_Click" />
                                </div>
                            </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvTankerHistory" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRow" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Shift Date" DataField="ShiftDate" />
                                                <asp:BoundField HeaderText="Tanker No" DataField="V_VehicleNo" />
                                                <asp:BoundField HeaderText="Tanker From" DataField="FROMDS" />
                                                <asp:BoundField HeaderText="Tanker To" DataField="TODS" />                             
                                            </Columns>
                                        </asp:GridView>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
     <script type="text/javascript">
         function ValidatePage() {

             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
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

