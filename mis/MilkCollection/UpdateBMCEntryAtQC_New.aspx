<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateBMCEntryAtQC_New.aspx.cs" Inherits="mis_MilkCollection_UpdateBMCEntryAtQC_New" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnUpdate_Click" Style="margin-top: 20px; width: 50px;" />
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
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Update BMC/DCS Milk Collection Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row noprint">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Reference No.<span style="color: red;"> *</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select Reference No" Text="<i class='fa fa-exclamation-circle' title='Select Reference No !'></i>"
                                                ControlToValidate="ddlReferenceNo" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlReferenceNo" Width="100%" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlReferenceNo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <%-- <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" style="margin-top:22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click"/>
                            </div>
                        </div>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="updatepnl" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvBMCDetails" CssClass="table table-bordered" AutoGenerateColumns="false" runat="server" OnRowDataBound="gvBMCDetails_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%-- <asp:CheckBox ID="chkSelect" Visible="false" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"/>--%>
                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Eval("RowVisible").ToString()=="Yes"?(Container.DataItemIndex +1).ToString():"" %>'></asp:Label>
                                                                <%-- <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID") %>'></asp:Label>--%>
                                                                <asp:Label ID="lblType" CssClass="hidden" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Name_E" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Temp">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Temp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvTemp" runat="server" Display="Dynamic" ControlToValidate="txtV_Temp" Text="<i class='fa fa-exclamation-circle' title='Enter Temp!'></i>" ErrorMessage="Enter Temp" Enabled="true" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtV_Temp" Visible="false" Text='<%# Eval("Temp") %>' runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                                <%-- <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQty" runat="server" Display="Dynamic" Enabled="true" ControlToValidate="txtD_MilkQuantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity!'></i>" ErrorMessage="Enter Quantity" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>--%>
                                                                <asp:TextBox ID="txtD_MilkQuantity" Text='<%# Eval("Quantity") %>' onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtD_MilkQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fat %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT") %>'></asp:Label>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvFAT" runat="server" Display="Dynamic" Enabled="true" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Enter FAT!'></i>" ErrorMessage="Enter FAT" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 3.2 and maximum 10." Display="Dynamic" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 3.2 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="3.2" MaximumValue="10"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtFAT" Text='<%# Eval("FAT") %>' onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtFAT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CLR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                                                <span class="pull-right">
                                                                    <asp:RequiredFieldValidator ID="rfvCLR" runat="server" Display="Dynamic" Enabled="false" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>" ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Minimum CLR required 20 and maximum 30." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum CLR required 20 and maximum 30.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a" Type="Double" MinimumValue="20" MaximumValue="30"></asp:RangeValidator>
                                                                </span>
                                                                <asp:TextBox ID="txtCLR" Text='<%# Eval("CLR") %>' onkeypress="return validateDec(this,event)" Visible="false" runat="server" CssClass="form-control" OnTextChanged="txtCLR_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SNF %">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF") %>'></asp:Label>
                                                                <asp:TextBox ID="txtSNF" Text='<%# Eval("SNF") %>' Enabled="true" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kg Fat">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKgFat" runat="server" Text='<%# Eval("FatKg") %>'></asp:Label>
                                                                <asp:TextBox ID="txtKgFat" Enabled="false" Text='<%# Eval("FatKg") %>' Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Kg SNF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblKgSNF" runat="server" Text='<%# Eval("SnfKg") %>'></asp:Label>
                                                                <asp:TextBox ID="txtKgSNF" Enabled="false" Text='<%# Eval("SnfKg") %>' Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function tabFocus(e) {
            document.getElementById("HiddenField1").value = e.id;
        }
    </script>
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtTime').timepicker();

    </script>


</asp:Content>

