<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EngneeringSectionEntryForm.aspx.cs" Inherits="mis_EngneeringDepartment_EngneeringSectionEntryForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                            <h3 class="box-title">Engineering Section Entry Form</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Month<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Month" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                ControlToValidate="ddlMonth" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="ddlMonth_TextChanged">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem>January</asp:ListItem>
                                            <asp:ListItem>February</asp:ListItem>
                                            <asp:ListItem>March</asp:ListItem>
                                            <asp:ListItem>April</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>June</asp:ListItem>
                                            <asp:ListItem>July</asp:ListItem>
                                            <asp:ListItem>August</asp:ListItem>
                                            <asp:ListItem>September</asp:ListItem>
                                            <asp:ListItem>October</asp:ListItem>
                                            <asp:ListItem>November</asp:ListItem>
                                            <asp:ListItem>December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvYear" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Year" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                ControlToValidate="ddlYear" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                     </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Entry Date<span style="color: red;">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                                    ErrorMessage="Select Entry Date" Text="<i class='fa fa-exclamation-circle' title='Select Entry Date !'></i>"
                                                    ControlToValidate="txtEntryDate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtEntryDate" data-date-start-date="0d"  autocomplete="off" placeholder="Select Entry Date" CssClass="form-control DateAdd"  runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Head Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Head Name !'></i>"
                                                ControlToValidate="ddlHeadName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlHeadName" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeadName_SelectedIndexChanged">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Sub Head Name<span style="color: red;">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" InitialValue="0"
                                                ErrorMessage="Select Sub Head Name" Text="<i class='fa fa-exclamation-circle' title='Select Sub Head Name !'></i>"
                                                ControlToValidate="ddlSubHeadName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlSubHeadName" CssClass="form-control" runat="server">
                                            <%--<asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem>Electricity Bill</asp:ListItem>
                                            <asp:ListItem>MainTainance & Rubber Goods</asp:ListItem>
                                            <asp:ListItem>Homogenizer / Refrigeration</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Unit</label>
                                        <asp:TextBox ID="txtNumber" CssClass="form-control" runat="server"  MaxLength="20" placeholder="Enter Unit"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Qty</label>
                                        <asp:TextBox ID="txtQty" CssClass="form-control"  runat="server" MaxLength="20" placeholder="Enter Qty"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Amount</label>
                                        <asp:TextBox ID="txtAmount" onkeypress="return validateDec(this,event)" CssClass="form-control" runat="server" placeholder="Enter Amount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Remark</label>
                                        <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" placeholder="Enter Remark"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" Style="margin-top: 20px;" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btnSubmit_Click" />
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
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                            EmptyDataText="No Record Found." DataKeyNames="ENGSectionEntryId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:Label ID="lblIsActive" Visible="false" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entry Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryMonth" runat="server" Text='<%# Eval("EntryMonth") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryYear" runat="server" Text='<%# Eval("EntryYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHeadName" runat="server" Text='<%# Eval("ENGHeadName") %>'></asp:Label>
                                                        <asp:Label ID="lblHeadID" Visible="false" runat="server" Text='<%# Eval("ENGHeadID") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub  Head Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubHeadName" runat="server" Text='<%# Eval("ENGSubHeadName") %>'></asp:Label>
                                                        <asp:Label ID="lblSubHeadID" Visible="false" runat="server" Text='<%# Eval("ENGSubHeadID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNumber" runat="server" Text='<%# Eval("Number") %>'></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                         <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("ENGSectionEntryId") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ENGSectionEntryId") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;

        }

        function validateDec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }
        function validateint(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
           // var number = el.value.split('.');
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            ////just one dot (thanks ddlab)
            //if (number.length > 1 && charCode == 46) {
            //    return false;
            //}
            ////get the carat position
            //var caratPos = getSelectionStart(el);
            //var dotPos = el.value.indexOf(".");
            //if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
            //    return false;
            //}
            return true;
        }

    </script>
</asp:Content>

