<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkCollectionAdditionDeductionEntry.aspx.cs" Inherits="mis_MilkCollection_MilkCollectionAdditionDeductionEntry" %>

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
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Route Wise Addition Deduction Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BMC Root<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvBMCRoot" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                    ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Head Type" Text="<i class='fa fa-exclamation-circle' title='Select Head Type !'></i>"
                                                    ControlToValidate="ddlHeadType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlHeadType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlHeadType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                                <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                    ControlToValidate="ddlHeaddetails" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlHeaddetails" OnInit="ddlHeaddetails_Init" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div id="divEntry" runat="server" visible="false">
                                <fieldset>
                                    <legend>Fill Head Details</legend>
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <asp:GridView ID="gvDetails"  CssClass="table table-bordered gvDetails" AutoGenerateColumns="false" runat="server"  OnRowDataBound="gvDetails_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" OnCheckedChanged="checkAll_CheckedChanged" AutoPostBack="true"/>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblItemOffice_ID" CssClass="hidden" runat="server" Text='<%# Eval("Office_ID")%>'></asp:Label>
                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Head Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblItemBillingHead_ID" CssClass="hidden" runat="server" Text='<%# Eval("ItemBillingHead_ID")%>'></asp:Label>
                                                                <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvHeadAmount" ValidationGroup="a"
                                                                    ErrorMessage="Enter Head Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Head Amount !'></i>"
                                                                    ControlToValidate="txtHeadAmount" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator></span>
                                                         <asp:TextBox ID="txtHeadAmount"  onkeypress="return validateDec(this,event)" Text='<%# Eval("HeadAmount")%>' MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="rfvQuantity" ValidationGroup="a"
                                                                    ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                                    ControlToValidate="txtQuantity" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator></span>                                                     
                                                        <asp:TextBox ID="txtQuantity" onkeypress="return validateDec(this,event)" Text='<%# Eval("Quantity")%>' MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <%--<span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                                    ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                                    ControlToValidate="txtRemark" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator></span>  --%>                                                   
                                                        <asp:TextBox ID="txtRemark"  Text='<%# Eval("Remark")%>'  runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnSave" ValidationGroup="a" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Report</h3>
                        </div>
                        <asp:Label ID="lblRptMsg" runat="server" Text=""></asp:Label>

                        <div class="box-body">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtFilterDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFilterDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Type<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Search"
                                                    InitialValue="0" ErrorMessage="Select Head Type" Text="<i class='fa fa-exclamation-circle' title='Select Head Type !'></i>"
                                                    ControlToValidate="ddlFltHeadType" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlFltHeadType" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFltHeadType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="ADDITION">ADDITIONS</asp:ListItem>
                                                <asp:ListItem Value="DEDUCTION">DEDUCTIONS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlFltHeaddetails" Width="150px" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnFltSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Search" OnClick="btnFltSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:GridView ID="GridView1" CssClass="table table-bordered" AutoGenerateColumns="false" runat="server" OnRowCommand="GridView1_RowCommand">

                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
												 <asp:TemplateField HeaderText="Head Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemBillingHead_Type" runat="server" Text='<%# Eval("ItemBillingHead_Type")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemBillingHead_Name" runat="server" Text='<%# Eval("ItemBillingHead_Name")%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeadAmount" runat="server" Text='<%# Eval("HeadAmount")%>'></asp:Label>
                                                        <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvHeadAmount" ValidationGroup="Update"
                                                            ErrorMessage="Enter Head Amount" Text="<i class='fa fa-exclamation-circle' title='Enter Head Amount !'></i>"
                                                            ControlToValidate="txtHeadAmount" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator></span>
                                                         <asp:TextBox ID="txtHeadAmount" Visible="false" Text='<%# Eval("HeadAmount")%>' onkeypress="return validateDec(this,event)" MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvQuantity" ValidationGroup="Update"
                                                            ErrorMessage="Enter Qunatity" Text="<i class='fa fa-exclamation-circle' title='Enter Qunatity !'></i>"
                                                            ControlToValidate="txtQuantity" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator></span>                                                     
                                                        <asp:TextBox ID="txtQuantity" Visible="false" Text='<%# Eval("Quantity")%>' onkeypress="return validateDec(this,event)" MaxLength="25" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("HeadRemark")%>'></asp:Label>
                                                                <%--<span class="pull-right">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                                    ErrorMessage="Enter Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Quantity !'></i>"
                                                                    ControlToValidate="txtRemark" Enabled="false" ForeColor="Red" Display="Dynamic" runat="server">
                                                                </asp:RequiredFieldValidator></span>  --%>                                                   
                                                        <asp:TextBox ID="txtHeadRemark" Visible="false"  Text='<%# Eval("HeadRemark")%>'  runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("AddtionsDeducEntry_ID")%>' Enabled='<%# Eval("Count").ToString()=="0"?true:false%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdate" runat="server" ValidationGroup="Update" Visible="false" CommandName="UpdateRecord" CommandArgument='<%# Eval("AddtionsDeducEntry_ID")%>' OnClientClick="return confirm('Do you really want to update record?')">Update</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
												<asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" CausesValidation="false" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("AddtionsDeducEntry_ID")%>' Visible='<%# Eval("Count").ToString()=="0"?true:false%>' OnClientClick="return confirm('Do you really want to Delete record?')"><i class="fa fa-trash"></i></asp:LinkButton>  
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
        $('#checkAll').click(function () {
            debugger;
            var inputList = document.querySelectorAll('.gvDetails tbody input[type="checkbox"]:not(:disabled)');
            var Amount = document.querySelectorAll('.gvDetails tbody input[type="textbox"]');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true; 
                    
                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });

    </script>
    <script>
        function myFunction() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("gvDetails");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[3];
                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
</script>
</asp:Content>

