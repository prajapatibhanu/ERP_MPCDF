<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MDM_SFSMP_Testing.aspx.cs" Inherits="mis_dailyplan_MDM_SFSMP_Testing" %>

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
                            <h3 class="box-title">MDM-SFSMP Testing</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Mfg Date<span style="color: red"> *</span></label>
                                        <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Entry Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>--%>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtEntryDate" autocomplete="off" placeholder="Entry Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Used By Date<span style="color: red"> *</span></label>
                                        <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Entry Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>--%>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtUsedByDate" autocomplete="off" placeholder="Entry Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Batch No<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtBatchNo" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateNum(event);" MaxLength="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBatchNo" ErrorMessage="Enter Batch No" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Flavour<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtFlavour" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validatename(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFlavour" ErrorMessage="Enter Flavour" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>OT<span style="color: red"> *</span></label>
                                        <asp:DropDownList ID="ddlOT" class="form-control" runat="server" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Sour">Sour</asp:ListItem>
                                            <asp:ListItem Value="Curd">Curd</asp:ListItem>
                                            <asp:ListItem Value="Satisfactory">Satisfactory</asp:ListItem>
                                            <asp:ListItem Value="Off Taste">Off Taste</asp:ListItem>
                                            <asp:ListItem Value="Slightly Off Taste">Slightly Off Taste</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlOT" InitialValue="0" ErrorMessage="Select OT" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Appearance<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtAppearance" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateName(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAppearance" ErrorMessage="Enter Appearance" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Color<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtColor" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateName(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtColor" ErrorMessage="Enter Color" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Sweetness<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtSweetness" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateName(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSweetness" ErrorMessage="Enter Sweetness" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Fat %<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtFat" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFat" ErrorMessage="Enter FAT %" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Acidity<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtAcidity" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAcidity" ErrorMessage="Enter Acidity" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Neutralizer<span style="color: red"> *</span></label>
                                        <asp:DropDownList ID="ddlNeutralizer" class="form-control" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                            <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlNeutralizer" InitialValue="0" ErrorMessage="Select Neutralizer" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Insolubility<span style="color: red"> *</span></label>
                                        <asp:TextBox ID="txtInsolubiLity" class="form-control" runat="server" ClientIDMode="Static" autocomplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtInsolubiLity" ErrorMessage="Enter InsolubiLity" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
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
                            <h3 class="box-title">MDM-SFSMP Testing Report</h3>
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
                                        <asp:Button ID="btnSearch" runat="server" style="margin-top : 20px;" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                                <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered gvDetail" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mfg Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("MfgDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Used By Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsedByDate" runat="server" Text='<%# Eval("UsedByDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="B. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBNo" runat="server" Text='<%# Eval("BNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flavour">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlavour" runat="server" Text='<%# Eval("Flavour") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOT" runat="server" Text='<%# Eval("OT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apperance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApperance" runat="server" Text='<%# Eval("Apperance") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblColor" runat="server" Text='<%# Eval("Color") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sweetness">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSweetness" runat="server" Text='<%# Eval("Sweetness") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat_Per" runat="server" Text='<%# (Convert.ToDecimal(Eval("Fat_Per"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acidity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcidity" runat="server" Text='<%# (Convert.ToDecimal(Eval("Acidity"))).ToString("0.00") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Neutralizer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNeutralizer" runat="server" Text='<%# Eval("Neutralizer") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Insolubility">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInsolubility" runat="server" Text='<%# (Convert.ToDecimal(Eval("Insolubility"))).ToString("0.00") %>'></asp:Label>
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

