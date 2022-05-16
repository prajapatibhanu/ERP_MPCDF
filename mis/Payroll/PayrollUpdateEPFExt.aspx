<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateEPFExt.aspx.cs" Inherits="mis_Payroll_PayrollUpdateEPFExt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        input.txtEPF_WAGES, input.txtEPS_WAGES, input.txtEDLI_WAGES, input.txtEPF_EPS_DIFF_REMITTED, input.txtNCP_DAYS, input.txtREFUND_OF_ADVANCES {
            background: #334fb72e !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Update External Employee EPF Details</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" runat="server" Text="Search" OnClientClick="return validateform();" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <div class="table table-responsive">
                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False" EmptyDataText="No Record Found...!!">
                                <Columns>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Checked="true" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>' />
                                            <asp:Label ID="lblGenStatus" runat="server" CssClass="hidden" Text='<%# Eval("GenStatus").ToString()%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="Emp_Name" runat="server" Text='<%# Eval("Emp_Name") %>' ToolTip='<%# Eval("Emp_ID")%>'></asp:Label>
                                            <asp:Label ID="lblUAN" runat="server" CssClass="hidden" Text='<%# Eval("UAN") %>'></asp:Label>
                                            <asp:Label ID="lblEPF" runat="server" CssClass="hidden" Text='<%# Eval("EPF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UAN" HeaderText="UAN" />
                                    <asp:BoundField DataField="EPF" HeaderText="EPF" />


                                    <asp:TemplateField HeaderText="GROSS WAGES">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGROSS_WAGES" runat="server" Text='<%# Eval("GROSS_WAGES") %>' CssClass="form-control txtGROSS_WAGES" onkeypress="return validateNum(event)" placeholder="GROSS_WAGES" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EPF WAGES">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEPF_WAGES" runat="server" Text='<%# Eval("EPF_WAGES") %>' CssClass="form-control txtEPF_WAGES" onkeypress="return validateNum(event)" placeholder="EPF_WAGES" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EPS WAGES">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEPS_WAGES" runat="server" Text='<%# Eval("EPS_WAGES") %>' CssClass="form-control txtEPS_WAGES" onkeypress="return validateNum(event)" placeholder="EPS_WAGES" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EDLI WAGES">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEDLI_WAGES" runat="server" Text='<%# Eval("EDLI_WAGES") %>' CssClass="form-control txtEDLI_WAGES" onkeypress="return validateNum(event)" placeholder="EDLI_WAGES" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EPF CONTRI REMITTED">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEPF_CONTRI_REMITTED" runat="server" Text='<%# Eval("EPF_CONTRI_REMITTED") %>' CssClass="form-control txtEPF_CONTRI_REMITTED" onkeypress="return validateNum(event)" placeholder="EPF_CONTRI_REMITTED" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EPS CONTRI REMITTED">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEPS_CONTRI_REMITTED" runat="server" Text='<%# Eval("EPS_CONTRI_REMITTED") %>' CssClass="form-control txtEPS_CONTRI_REMITTED" onkeypress="return validateNum(event)" placeholder="EPS_CONTRI_REMITTED" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EPF EPS DIFF REMITTED">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEPF_EPS_DIFF_REMITTED" runat="server" Text='<%# Eval("EPF_EPS_DIFF_REMITTED") %>' CssClass="form-control txtEPF_EPS_DIFF_REMITTED" onkeypress="return validateNum(event)" placeholder="EPF_EPS_DIFF_REMITTED" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NCP DAYS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNCP_DAYS" runat="server" Text='<%# Eval("NCP_DAYS") %>' CssClass="form-control txtNCP_DAYS" onkeypress="return validateNum(event)" placeholder="NCP_DAYS" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REFUND OF ADVANCES">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtREFUND_OF_ADVANCES" runat="server" Text='<%# Eval("REFUND_OF_ADVANCES") %>' CssClass="form-control txtREFUND_OF_ADVANCES" onkeypress="return validateNum(event)" placeholder="REFUND_OF_ADVANCES" Enabled='<%# Eval("GenStatus").ToString() == "Generated" ? false : true %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" />
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
        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


        $(document).ready(function () {
            var checkbox = $('table tbody input[type="checkbox"]:disabled');
            for (var i = 0; i < checkbox.length; i++) {
                $(checkbox[i]).parents('tr').css('background-color', 'rgba(255, 24, 0, 0.55)');
                //    $(checkbox[i]).parents('tr').css('color', '#FFFFFF');
                //$('table tbody input[type="checkbox"]').css('width', '25px');
            }
        });



        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
                <%--if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }--%>
            }
        }

    </script>

    <script>
        $('.txtGROSS_WAGES').on('focusout', function () {
            var GROSS_WAGESAmount = $(this).val();
            var EPF_CONTRI_REMITTED = (GROSS_WAGESAmount * 0.12);
            if (GROSS_WAGESAmount > 15000) {
                GROSS_WAGESAmount = 15000
            }

            if (GROSS_WAGESAmount < 0) {
                GROSS_WAGESAmount = 0;
            }
            $(this).parent().parent().find('.txtEPF_WAGES').val(parseInt(GROSS_WAGESAmount));
            $(this).parent().parent().find('.txtEPS_WAGES').val(parseInt(GROSS_WAGESAmount));
            $(this).parent().parent().find('.txtEPF_CONTRI_REMITTED').val(parseInt(EPF_CONTRI_REMITTED));



            $(this).parent().parent().css("background", "#337ab740");
        });


        $('.txtEPS_CONTRI_REMITTED').on('focusout', function () {

            var EPS_CONTRI_REMITTED = $(this).val();
            if (EPS_CONTRI_REMITTED > 1250) {
                EPS_CONTRI_REMITTED = 1250
            }
            var EPF_CONTRI_REMITTED = $(this).parent().parent().find('.txtEPF_CONTRI_REMITTED').val();
            
            if (EPF_CONTRI_REMITTED < 0) {
                EPF_CONTRI_REMITTED = 0;
            }
            if (EPS_CONTRI_REMITTED < 0) {
                EPS_CONTRI_REMITTED = 0;
            }
            var EPF_EPS_DIFF_REMITTED = (EPF_CONTRI_REMITTED - EPS_CONTRI_REMITTED);

            if (EPF_EPS_DIFF_REMITTED < 0) {
                EPF_EPS_DIFF_REMITTED=0;
            }
            $(this).parent().parent().find('.txtEPS_CONTRI_REMITTED').val(EPS_CONTRI_REMITTED);
            $(this).parent().parent().find('.txtEPF_EPS_DIFF_REMITTED').val(EPF_EPS_DIFF_REMITTED);

           
            //if ((parseInt(EPS_CONTRI_REMITTED) + parseInt(EPF_EPS_DIFF_REMITTED)) == (parseInt(EPS_CONTRI_REMITTED))) {
                $(this).parent().parent().css("background", "#337ab72b");
            //}else{
            //    $(this).parent().parent().css("background", "#dd4b3947");
            //}
            
        });

    </script>


</asp:Content>

