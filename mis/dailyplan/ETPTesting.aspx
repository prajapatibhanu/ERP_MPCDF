<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ETPTesting.aspx.cs" Inherits="mis_dailyplan_ETPTesting" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">ETP Testing</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                        <div class="row">
                             <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label><span class="text-danger">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtEntryDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Source</label><span class="text-danger">*</span>
                                     <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtSource" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtSource"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Source!'></i>"
                                                        ErrorMessage="Enter Source" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtSource" runat="server" CssClass="form-control" Placeholder="Enter Source"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Ph</label><span class="text-danger">*</span>
                                     <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtPh" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtPh"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Ph!'></i>"
                                                        ErrorMessage="Enter Ph" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtPh" runat="server" CssClass="form-control" Placeholder="Enter Ph"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>SUSPENDED SOLID mg/ltr</label><span class="text-danger">*</span>
                                     <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtSuspendedSolid" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtSuspendedSolid"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter SUSPENDED SOLID mg/ltr!'></i>"
                                                        ErrorMessage="Enter SUSPENDED SOLID mg/ltr" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtSuspendedSolid" runat="server" CssClass="form-control" Placeholder="Enter SUSPENDED SOLID mg/ltr"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Oil & Grease mg/ltr</label><span class="text-danger">*</span>
                                     <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtOilandGrease" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtOilandGrease"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Oil & Grease mg/ltr!'></i>"
                                                        ErrorMessage="Enter Oil & Grease mg/ltr" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" ></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtOilandGrease" runat="server" CssClass="form-control" Placeholder="Enter Oil & Grease mg/ltr"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>COD mg/ltr</label><span class="text-danger">*</span>
                                     <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtCOD" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtCOD"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter COD mg/ltr!'></i>"
                                                        ErrorMessage="Enter COD mg/ltr" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtCOD" runat="server" CssClass="form-control" Placeholder="Enter COD mg/ltr"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Initial D.O mg/ltr</label>
                                    <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvinitialDO" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtinitialDO"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Initial D.O!'></i>"
                                                        ErrorMessage="Enter Initial D.O" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtinitialDO" Enabled="false" runat="server" CssClass="form-control" Placeholder="Enter Initial D.O mg/ltr"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Final D.O mg/ltr</label>
                                    <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvFinalDO" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtFinalDO"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Final D.O!'></i>"
                                                        ErrorMessage="Enter Final D.O" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtFinalDO" Enabled="false" runat="server" CssClass="form-control" Placeholder="Enter Final D.O mg/ltr"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>B.O.D mg/ltr</label>
                                    <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvBOD" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtBOD"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter B.O.D!'></i>"
                                                        ErrorMessage="Enter B.O.D" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>        
                                                </span>
                                    <asp:TextBox ID="txtBOD" Enabled="false" runat="server" CssClass="form-control" Placeholder="Enter B.O.D mg/ltr"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Placeholder="Enter Remark"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" ValidationGroup="save" style="margin-top:20px;" Text="Save" OnClientClick="return ValidatePage();" OnClick="btnSave_Click"/>
                                     <a href="ETPTesting.aspx" class="btn btn-default" style="margin-top:20px;">Clear</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">ETP Testing Report</h3>
                        </div>
                        <asp:Label ID="lblRecordMsg" runat="server" Text="">
                        </asp:Label>
                        <div class="box-body">
                            <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExcel_Click" />                            
                                    </div>
                                </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDetails" runat="server" EmptyDataText="No Record Found" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblDateDiff" Visible="false" runat="server" Text='<%# Eval("DateDiff") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Source">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSource" runat="server" Text='<%# Eval("Source") %>'></asp:Label>                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ph">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPh" runat="server" Text='<%# Eval("Ph") %>'></asp:Label> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="SUSPENDED SOLID mg/ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSuspendedSolid" runat="server" Text='<%# Eval("SuspendedSolid") %>'></asp:Label>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Oil & Grease mg/ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOilandGrease" runat="server" Text='<%# Eval("OilandGrease") %>'></asp:Label>                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="COD mg/ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCOD" runat="server" Text='<%# Eval("COD") %>'></asp:Label>                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Initial D.O mg/ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInitialDO" runat="server" Text='<%# Eval("InitialDO") %>'></asp:Label> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Final D.O mg/ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinalDO" runat="server" Text='<%# Eval("FinalDO") %>'></asp:Label>  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="B.O.D mg/ltr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOD" runat="server" Text='<%# Eval("BOD") %>'></asp:Label>   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" Enabled='<%# Eval("InitialDO").ToString()==""?true:false %>' CommandArgument='<%# Eval("ETPTestingID") %>'><i class="fa fa-edit"></i></asp:LinkButton> 
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
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

