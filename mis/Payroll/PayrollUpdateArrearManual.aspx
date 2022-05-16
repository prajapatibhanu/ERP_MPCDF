<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateArrearManual.aspx.cs" Inherits="mis_Payroll_PayrollUpdateArrearManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        input.txtTotalEarning, input.txtTotalDeduction {
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
                    <h3 class="box-title">Update Employee Arrear Details</h3>

                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Financial Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Arrear Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="Arrear 1">Arrear 1</asp:ListItem>
                                    <asp:ListItem Value="Arrear 2">Arrear 2</asp:ListItem>
                                    <asp:ListItem Value="Salary">Salary</asp:ListItem>
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
                                            <asp:CheckBox ID="chkSelect" runat="server" />
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
                                            <%--<asp:Label ID="lblEmp_ID" runat="server" CssClass="hidden" Text='<%# Eval("UAN") %>'></asp:Label>
                                            <asp:Label ID="lblEPF" runat="server" CssClass="hidden" Text='<%# Eval("EPF") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--                                   
                                    <asp:BoundField DataField="UAN" HeaderText="UAN" />
                                    <asp:BoundField DataField="EPF" HeaderText="EPF" />
                                    --%>

                                    <asp:TemplateField HeaderText="BASIC">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBasicSalary" runat="server" Text='<%# Eval("BasicSalary") %>' CssClass="form-control txtBasicSalary" onkeypress="return validateNum(event)" placeholder="BasicSalary"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DA">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDA" runat="server" Text='<%# Eval("DA") %>' CssClass="form-control txtDA" onkeypress="return validateNum(event)" placeholder="DA"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="HRA">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtHRA" runat="server" Text='<%# Eval("HRA") %>' CssClass="form-control txtHRA" onkeypress="return validateNum(event)" placeholder="HRA"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Conv.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtConv" runat="server" Text='<%# Eval("Conv") %>' CssClass="form-control txtConv" onkeypress="return validateNum(event)" placeholder="Conv"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ord.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOrd" runat="server" Text='<%# Eval("Ord") %>' CssClass="form-control txtOrd" onkeypress="return validateNum(event)" placeholder="Ord"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Wash">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWash" runat="server" Text='<%# Eval("Wash") %>' CssClass="form-control txtWash" onkeypress="return validateNum(event)" placeholder="Wash"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other Earning">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOtherEarning" runat="server" Text='<%# Eval("OtherEarning") %>' CssClass="form-control txtOtherEarning" onkeypress="return validateNum(event)" placeholder="Other"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Earning">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTotalEarning" runat="server" Text='<%# Eval("TotalEarning") %>' CssClass="form-control txtTotalEarning" onkeypress="return validateNum(event)" placeholder="Total"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="EPF">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEPF" runat="server" Text='<%# Eval("EPF") %>' CssClass="form-control txtEPF" onkeypress="return validateNum(event)" placeholder="EPF"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ADA">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtADA" runat="server" Text='<%# Eval("ADA") %>' CssClass="form-control txtADA" onkeypress="return validateNum(event)" placeholder="ADA"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ITax">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtITax" runat="server" Text='<%# Eval("ITax") %>' CssClass="form-control txtITax" onkeypress="return validateNum(event)" placeholder="ITax"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PTax">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPTax" runat="server" Text='<%# Eval("PTax") %>' CssClass="form-control txtPTax" onkeypress="return validateNum(event)" placeholder="PTax"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Other Deduction">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOtherDeduction" runat="server" Text='<%# Eval("OtherDeduction") %>' CssClass="form-control txtOtherDeduction" onkeypress="return validateNum(event)" placeholder="Other"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Deduction">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTotalDeduction" runat="server" Text='<%# Eval("TotalDeduction") %>' CssClass="form-control txtTotalDeduction" onkeypress="return validateNum(event)" placeholder="Total"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NetPayable">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNetPayable" runat="server" Text='<%# Eval("NetPayable") %>' CssClass="form-control txtNetSalary" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <%--
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
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" OnClientClick="if (!confirm('कृपया सुनिश्चित करें की जिनका डाटा सुरक्षित करना / बदलना है,  उनके सामने का चेकबॉक्स चेक्ड है।')) return false;" />
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

        //$(document).ready(function () {
        //    var checkbox = $('table tbody input[type="checkbox"]:disabled');
        //    for (var i = 0; i < checkbox.length; i++) {
        //        $(checkbox[i]).parents('tr').css('background-color', 'rgba(255, 24, 0, 0.55)');
        //        //    $(checkbox[i]).parents('tr').css('color', '#FFFFFF');
        //        //$('table tbody input[type="checkbox"]').css('width', '25px');
        //    }
        //});


        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Salary Type. \n";
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
        $('.txtBasicSalary,.txtDA,.txtHRA,.txtConv,.txtOrd,.txtWash,.txtOtherEarning').on('focusout', function () {
            var txtBasicSalary = parseInt($(this).parent().parent().find('.txtBasicSalary').val());
            var txtDA = parseInt($(this).parent().parent().find('.txtDA').val());
            var txtHRA = parseInt($(this).parent().parent().find('.txtHRA').val());
            var txtConv = parseInt($(this).parent().parent().find('.txtConv').val());
            var txtOrd = parseInt($(this).parent().parent().find('.txtOrd').val());
            var txtWash = parseInt($(this).parent().parent().find('.txtWash').val());
            var txtOtherEarning = parseInt($(this).parent().parent().find('.txtOtherEarning').val());
            var txtTotalEarning = parseInt(txtBasicSalary + txtDA + txtHRA + txtConv + txtOrd + txtWash + txtOtherEarning);
            var txttalDeduction = parseInt($(this).parent().parent().find('.txtTotalDeduction').val());

            $(this).parent().parent().find('.txtTotalEarning').val(txtTotalEarning);
            //$(this).parent().parent().css("background", "#337ab740");
            $(this).parent().parent().find('.txtNetSalary').val(txtTotalEarning - txttalDeduction);

        });

        $('.txtEPF,.txtADA,.txtITax,.txtPTax,.txtOtherDeduction').on('focusout', function () {
            var txtEPF = parseInt($(this).parent().parent().find('.txtEPF').val());
            var txtADA = parseInt($(this).parent().parent().find('.txtADA').val());
            var txtITax = parseInt($(this).parent().parent().find('.txtITax').val());
            var txtPTax = parseInt($(this).parent().parent().find('.txtPTax').val());
            var txtOtherDeduction = parseInt($(this).parent().parent().find('.txtOtherDeduction').val()); 
            var txttalDeduction = parseInt(txtADA + txtITax + txtPTax + txtEPF + txtOtherDeduction);
            var txtTotalEarning = parseInt($(this).parent().parent().find('.txtTotalEarning').val());
            $(this).parent().parent().find('.txtTotalDeduction').val(txttalDeduction);
            $(this).parent().parent().find('.txtNetSalary').val(txtTotalEarning - txttalDeduction);
        });



        


        //$('.txtEPS_CONTRI_REMITTED').on('focusout', function () {

        //    var EPS_CONTRI_REMITTED = $(this).val();
        //    if (EPS_CONTRI_REMITTED > 1250) {
        //        EPS_CONTRI_REMITTED = 1250
        //    }
        //    var EPF_CONTRI_REMITTED = $(this).parent().parent().find('.txtEPF_CONTRI_REMITTED').val();

        //    if (EPF_CONTRI_REMITTED < 0) {
        //        EPF_CONTRI_REMITTED = 0;
        //    }
        //    if (EPS_CONTRI_REMITTED < 0) {
        //        EPS_CONTRI_REMITTED = 0;
        //    }
        //    var EPF_EPS_DIFF_REMITTED = (EPF_CONTRI_REMITTED - EPS_CONTRI_REMITTED);

        //    if (EPF_EPS_DIFF_REMITTED < 0) {
        //        EPF_EPS_DIFF_REMITTED = 0;
        //    }
        //    $(this).parent().parent().find('.txtEPS_CONTRI_REMITTED').val(EPS_CONTRI_REMITTED);
        //    $(this).parent().parent().find('.txtEPF_EPS_DIFF_REMITTED').val(EPF_EPS_DIFF_REMITTED);


        //    //if ((parseInt(EPS_CONTRI_REMITTED) + parseInt(EPF_EPS_DIFF_REMITTED)) == (parseInt(EPS_CONTRI_REMITTED))) {
        //    $(this).parent().parent().css("background", "#337ab72b");
        //    //}else{
        //    //    $(this).parent().parent().css("background", "#dd4b3947");
        //    //}

        //});

    </script>


</asp:Content>

