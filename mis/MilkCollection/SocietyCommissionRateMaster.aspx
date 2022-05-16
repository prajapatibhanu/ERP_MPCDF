<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyCommissionRateMaster.aspx.cs" Inherits="mis_MilkCollection_SocietyCommissionRateMaster" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
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
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Society Commisision Rate</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>FILTER</legend>
                                 <div class="col-md-2">
                                <div class="form-group">
                                    <label>Effective Date  </label>
                                    <span style="color: red">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtEffectiveDate" ErrorMessage="Please Enter Effective Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Effective Date !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtEffectiveDate" ErrorMessage="Invalid Effective Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Effective Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    </span>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtEffectiveDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-2">
                                <div class="form-group">
                                    <label>MilkType</label>
                                    <%--<span style="color: red">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkType" runat="server" ValidationGroup="a" InitialValue="0" Display="Dynamic" ControlToValidate="ddlMilkType" ErrorMessage="Please Select Milk Type." Text="<i class='fa fa-exclamation-circle' title='Please Select Milk Type !'></i>"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <asp:DropDownList ID="ddlMilkType" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Value="Total">Total(BUF+COW)</asp:ListItem>
                                        <asp:ListItem Value="BUF">BUF</asp:ListItem>
                                        <asp:ListItem Value="COW">COW</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Rate</label>
                                    <span style="color: red">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvRate" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtRate" ErrorMessage="Please Enter Rate." Text="<i class='fa fa-exclamation-circle' title='Please Enter Rate !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">

                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 21px;" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" AccessKey="S" />

                                </div>
                            </div>
                            </fieldset>
                           
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Report</legend>
                                <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvDetail" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvDetail_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Effective Date" DataField="EffectiveDate" />
                                             <asp:BoundField HeaderText="Milk Type" DataField="MilkType" />
                                            <asp:BoundField HeaderText="Rate" DataField="Rate" />
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("SocCommissionRate_Id").ToString() %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Search") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }


        function allnumeric(inputtxt) {
            var numbers = /^[0-9]+$/;
            if (inputtxt.value.match(numbers)) {
                alert('only number has accepted....');
                document.form1.text1.focus();
                return true;
            }
            else {
                alert('Please input numeric value only');
                document.form1.text1.focus();
                return false;
            }
        }



    </script>
</asp:Content>

