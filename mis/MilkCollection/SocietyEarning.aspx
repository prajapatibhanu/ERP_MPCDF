<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyEarning.aspx.cs" Inherits="mis_MilkCollection_SocietyEarning" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Producer Earning</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Earning Detail</legend>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtsocietyName" Text="<i class='fa fa-exclamation-circle' title='Enter society Name!'></i>" ErrorMessage="Enter society Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtsocietyName" Enabled="false" autocomplete="off" placeholder="Enter society Name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                                            
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date (दिनांक)</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtDate"  onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <label>Producer<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvProducer" runat="server" Display="Dynamic" ControlToValidate="ddlFarmer" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Producer'></i>" ErrorMessage="Select Producer" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlFarmer" AutoPostBack="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Total Amount<span style="color: red;">*</span>
                                        </label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ControlToValidate="txttotalamount" Text="<i class='fa fa-exclamation-circle' title='Enter Total Amount!'></i>" ErrorMessage="Enter Total Amount" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txttotalamount" MaxLength="10" onkeypress="return validateDec(this,event);"   CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Remark
                                        </label>
                                       
                                        <asp:TextBox ID="txtRemark"   CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" CssClass="btn btn-success" OnClientClick="return ValidatePage();" Text="Save" style="margin-top:30px;" OnClick="btnSave_Click"/>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>
                            
                        </div>
                    </div>
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Earning Details</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDateF" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtDateF" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDateF_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="gv_EarningDetail"  ShowHeader="true" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowCommand="gv_EarningDetail_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryDt" runat="server" Text='<%# Eval("EntryDt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProducerName" runat="server" Text='<%# Eval("ProducerName") %>'></asp:Label>
                                                     <asp:Label ID="lblProducerId" runat="server" CssClass="hidden" Text='<%# Eval("ProducerId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NetAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Edit/Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord"  Visible='<%# Eval("Count").ToString()=="0"?true:false %>' CommandArgument='<%# Eval("DcsLocalEarning_Id") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord"  Visible='<%# Eval("Count").ToString()=="0"?true:false %>' CommandArgument='<%# Eval("DcsLocalEarning_Id") %>' OnClientClick="return confirm('Do you really want to delete record?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script type="text/javascript">
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
        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });
    </script>
</asp:Content>

