<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="NPDWMShiftingMaster.aspx.cs" Inherits="mis_MilkCollection_NPDWMShiftingMaster" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" onclick="btnUpdate_Click"  Style="margin-top: 20px; width: 50px;" />
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
                    <div class="box box-success">
                        <div class="box-body">
                            <div class="box-header">
                                <h3 class="box-title">NP/DWM Shifting Master</h3>
                            </div>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="box-body">
                                <fieldset>
                                    <legend>Filter</legend>
                                    <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Transfer/Update CC<span style="color: red">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlTransferfromCC" Text="<i class='fa fa-exclamation-circle' title='Select Transfer/Updtae CC!'></i>" ErrorMessage="Select Transfer/Updtae CC" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                             <asp:DropDownList ID="ddlTransferfromCC"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlTransferfromCC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    <div class="form-group">
                                        <label>From Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFdt" Enabled ="false" onkeypress="javascript: return false;" Width="100%" MaxLength="10" data-date-end-date="0d" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date  </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTdt" Enabled ="false" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" data-date-end-date="0d" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Update/Transfer<span style="color: red">*</span></label>
                                                 <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlType" Text="<i class='fa fa-exclamation-circle' title='Select Transfer/Updtae Type!'></i>" ErrorMessage="Select Transfer/Updtae Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem value="0">Select</asp:ListItem>
                                                    <asp:ListItem value="1" Selected="True">Update</asp:ListItem>
                                                    <asp:ListItem value="2">Transfer/Update</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="col-md-2" id="divTransfertto" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Transfer To CC<span style="color: red">*</span></label>
                                              <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlTransfertoCC" Text="<i class='fa fa-exclamation-circle' title='Select Transfer to CC!'></i>" ErrorMessage="Select Transfer to CC" SetFocusOnError="true" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                             <asp:DropDownList ID="ddlTransfertoCC"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" ValidationGroup="a"  style="margin-top:22px;" OnClick="btnSearch_Click"/>
                                        </div>
                                    </div>
                                </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Details</legend>
                                     <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField ControlStyle-CssClass="noprint">
                                                <HeaderTemplate>
                                                    <input id="Checkbox2" type="checkbox" onclick="checkAllbox(this)" runat="server" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                     <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex+ 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Society Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Society") %>'></asp:Label>
                                                        <asp:Label ID="lblAddtionsDeducEntry_ID" CssClass="hidden" runat="server" Text='<%# Eval("AddtionsDeducEntry_ID") %>'></asp:Label>
                                                      <asp:Label ID="lblNPDetails_ID" CssClass="hidden" runat="server" Text='<%# Eval("NPDetails_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:label ID="lblHeadAmount" CssClass="form-control" runat="server" Text='<%# Eval("HeadAmount")%>'></asp:label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText=" Update Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUpdateAmount"  CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:label ID="lblHeadRemark" CssClass="form-control" runat="server" Text='<%# Eval("HeadRemark")%>'></asp:label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update Remark">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUpdateHeadRemark" CssClass="form-control" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnUpdate" Visible="false" OnClientClick="if (!confirm('Are you sure you want Update/Transfer?')) return false;"  runat="server" Text="Update" CssClass="btn btn-success" style="margin-top:25px;" OnClick="btnUpdate_Click"/>
                                        </div>
                                    </div>
                                </fieldset>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
      <script>
          function ValidatePage() {

              if (typeof (Page_ClientValidate) == 'function') {
                  Page_ClientValidate('a');
              }

              if (Page_IsValid) {

                  if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
               
            }
        }

    </script>
</asp:Content>

