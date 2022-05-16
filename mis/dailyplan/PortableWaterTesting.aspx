<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PortableWaterTesting.aspx.cs" Inherits="mis_dailyplan_PortableWaterTesting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media Print {
            .no-print {
                display: none;
            }
        }
    </style>
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
            <div class="row no-print">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Portable Water Testing</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row no-print">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date<span style="color: red"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Entry Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtEntryDate" autocomplete="off" placeholder="Entry Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtEntryDate" runat="server" ControlToValidate="txtEntryDate" ErrorMessage="Select Entry Date" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row no-print">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Row Water</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>PH<span style="color: red"> *</span></label>
                                                    <asp:TextBox ID="txtR_PH" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtR_PH" runat="server" ControlToValidate="txtR_PH" ErrorMessage="Enter Row Water PH" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>TDS PPM<span style="color: red"> *</span></label>
                                                    <asp:TextBox ID="txtR_TDSPPM" MaxLength="20" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtR_TDSPPM" runat="server" ControlToValidate="txtR_TDSPPM" ErrorMessage="Enter Row Water TDS PPM" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Hardness PPM<span style="color: red"> *</span></label>
                                                    <asp:TextBox ID="txtR_HardnessPPM" MaxLength="20" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtR_HardnessPPM" runat="server" ControlToValidate="txtR_HardnessPPM" ErrorMessage="Enter Row Water Hardness PPM" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Soft Water</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>PH<span style="color: red"> *</span></label>
                                                    <asp:TextBox ID="txtS_PH" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtS_PH" runat="server" ControlToValidate="txtS_PH" ErrorMessage="Enter Soft Water PH" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>TDS PPM<span style="color: red"> *</span></label>
                                                    <asp:TextBox ID="txtS_TDSPPM" MaxLength="20" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtS_TDSPPM" runat="server" ControlToValidate="txtS_TDSPPM" ErrorMessage="Enter Soft Water TDS PPM" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Hardness PPM<span style="color: red"> *</span></label>
                                                    <asp:TextBox ID="txtS_HardnessPPM" MaxLength="20" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtR_HardnessPPM" ErrorMessage="Enter Soft Water Hardness PPM" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row no-print">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Portable Water Testing Report</h3>
                        </div>
                        <asp:Label ID="gridlblmsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row no-print">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>From Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Select From Date!'></i>" ErrorMessage="Select From Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" autocomplete="off" placeholder="From Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txttoDate" Text="<i class='fa fa-exclamation-circle' title='Select To Date!'></i>" ErrorMessage="Select To Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txttoDate" autocomplete="off" placeholder="To Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                  <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" style="margin-top:20px;" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row no-print">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export To Excel" CssClass="btn btn-primary" OnClick="btnExcel_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered gvDetail" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCreated="gvDetail_RowCreated">
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EnrtyDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PH">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRPH" runat="server" Text='<%# (Convert.ToDecimal(Eval("Row_PH"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS PPM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRTDS_PPM" runat="server" Text='<%# (Convert.ToDecimal(Eval("Row_TDSPPM"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hardness PPM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRHardnessPPM" runat="server" Text='<%# (Convert.ToDecimal(Eval("Row_HardnessPPM"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PH">
                                            <ItemTemplate>
                                                <asp:Label ID="lblS_PH" runat="server" Text='<%# (Convert.ToDecimal(Eval("Soft_PH"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS PPM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblS_TDSPPM" runat="server" Text='<%# (Convert.ToDecimal(Eval("Soft_TDSPPM"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hardness PPM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblR_HardnessPPM" runat="server" Text='<%# (Convert.ToDecimal(Eval("Soft_HardnessPPM"))).ToString("0.00") %>'></asp:Label>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
</asp:Content>

