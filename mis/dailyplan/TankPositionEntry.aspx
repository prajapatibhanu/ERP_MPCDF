<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TankPositionEntry.aspx.cs" Inherits="mis_dailyplan_TankPositionEntry" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            debugger;
            var GridView2 = document.getElementById("<%=gvTankPosition.ClientID %>");
            var chkkk = GridView2.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked;
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        

    </script>
    
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
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Tank Position</h3>
                </div>
                <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtEntryDate" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvArrivalTime" runat="server" Display="Dynamic" ControlToValidate="txtEntryTime" Text="<i class='fa fa-exclamation-circle' title='Enter Time!'></i>" ErrorMessage="Enter Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                            </span>
                            <div class="form-group">
                                <label>Time<span style="color: red;">*</span></label>
                                <div class="input-group bootstrap-timepicker timepicker">
                                    <asp:TextBox ID="txtEntryTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvTankPosition" runat="server" CssClass="table table-bordered gvTankPosition" AutoGenerateColumns="false" OnRowDataBound="gvTankPosition_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="2%">
                                            <HeaderTemplate>
                                               <%-- <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />--%>
                                                 <asp:CheckBox ID="check_All" runat="server" ClientIDMode="static" OnCheckedChanged="check_All_CheckedChanged" AutoPostBack="true"/>
                                                All
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" runat="server" OnCheckedChanged="chkselect_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SOURCE" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MCName" runat="server" Text='<%# Eval("V_MCName") %>'></asp:Label>
                                                <asp:Label ID="lblI_MCID" CssClass="hidden" runat="server" Text='<%# Eval("I_MCID") %>'></asp:Label>
                                                <asp:Label ID="lblV_MCType" CssClass="hidden" runat="server" Text='<%# Eval("V_MCType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tank Position" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvddlTankPosition" runat="server" Display="Dynamic"
                                                        ControlToValidate="ddlTankPosition" InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Tank Position!'></i>"
                                                        ErrorMessage="Select Tank Position" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlTankPosition" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlTankPosition_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Empty">Empty</asp:ListItem>
                                                    <asp:ListItem Value="Under Processing">Under Processing</asp:ListItem>
                                                    <asp:ListItem Value="Under Filling Transfer">Under Filling Transfer</asp:ListItem>
                                                    <asp:ListItem Value="Under Agitation">Under Agitation</asp:ListItem>
                                                    <asp:ListItem Value="Ready">Ready</asp:ListItem>
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variant" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvddlVariant" runat="server" Display="Dynamic"
                                                        ControlToValidate="ddlVariant" InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Variant!'></i>"
                                                        ErrorMessage="Select Variant" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlVariant" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlVariant_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oral Test" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvddlOption" runat="server" Display="Dynamic"
                                                        ControlToValidate="ddlOption" InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Oral Test!'></i>"
                                                        ErrorMessage="Select Oral Test" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlOption" runat="server" CssClass="form-control select2"></asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TEMP" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtTEMP" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtTEMP"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter TEMP!'></i>"
                                                        ErrorMessage="Enter TEMP" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revTemprature" ControlToValidate="txtTEMP" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtTEMP" runat="server" autocomplete="off" Text="" CssClass="form-control"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAT % (0.05 - 10)" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtFAT" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtFAT"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter FAT!'></i>"
                                                        ErrorMessage="Enter FAT" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revFat_S" ControlToValidate="txtFAT" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Minimum FAT % required 0.05 and maximum 10." Display="Dynamic" ControlToValidate="txtFAT" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 0.05 and maximum 10.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save" Type="Double" MinimumValue="0.05" MaximumValue="10"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtFAT" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text="" CssClass="form-control" OnTextChanged="txtFAT_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CLR (20 - 36)" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtCLR" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtCLR"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter CLR!'></i>"
                                                        ErrorMessage="Enter CLR" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revCLR" ControlToValidate="txtCLR" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d+)?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RegularExpressionValidator>

                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Minimum FAT % required 20 and maximum 36." Display="Dynamic" ControlToValidate="txtCLR" Text="<i class='fa fa-exclamation-circle' title='Minimum FAT % required 20 and maximum 36.!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save" Type="Double" MinimumValue="20" MaximumValue="36"></asp:RangeValidator>
                                                </span>
                                                <asp:TextBox ID="txtCLR" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text="" CssClass="form-control" OnTextChanged="txtCLR_TextChanged" AutoPostBack="true"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF %" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtSNF" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtSNF"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter SNF!'></i>"
                                                        ErrorMessage="Enter SNF" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revSNF_S" ControlToValidate="txtSNF" Display="Dynamic" ValidationExpression="^[0-9]\d*(\.\d{1,2})?$" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid Input!'></i>" ErrorMessage="Invalid Input" SetFocusOnError="true" ForeColor="Red" ValidationGroup="save"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtSNF" autocomplete="off" onkeypress="return validateDec(this,event)" runat="server" Text="" CssClass="form-control"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="COB" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvddlCOB" runat="server" Display="Dynamic"
                                                        ControlToValidate="ddlCOB"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select COB!'></i>"
                                                        ErrorMessage="Select COB" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList ID="ddlCOB" runat="server" CssClass="form-control select2">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                    <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACIDITY" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtACIDITY" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtACIDITY"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter ACIDITY!'></i>"
                                                        ErrorMessage="Enter ACIDITY" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtACIDITY" autocomplete="off" runat="server" Text="" CssClass="form-control">                                              
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtRemark"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Remark!'></i>"
                                                        ErrorMessage="Enter Remark" SetFocusOnError="true" ForeColor="Red"
                                                        ValidationGroup="save" Enabled="false"></asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtRemark" autocomplete="off" runat="server" Text="" CssClass="form-control"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
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
                        <asp:Label ID="gridlblmsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtFilterDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:TextBox ID="txtFilterDate" autocomplete="off" placeholder="Tanker Arrival Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtFilterDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExcel_Click" />
                                        <asp:Button ID="btnShowntoplant" runat="server" Visible="false" CssClass="btn btn-primary" Text="Shown To Plant" OnClick="btnShowntoplant_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvDetail" runat="server"  CssClass="table table-bordered gvDetail" AutoGenerateColumns="false" EmptyDataText="No Record Found">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                <asp:Label ID="lblRowNumber" CssClass="hidden" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("TankPositionID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Time" DataField="EntryTIme" />
                                        <asp:BoundField HeaderText="Source" DataField="Source" />
                                        <asp:BoundField HeaderText="Tank Position" DataField="TankPosition" />
                                        <asp:BoundField HeaderText="Variant" DataField="ItemTypeName" />
                                        <asp:BoundField HeaderText="OT" DataField="OT" />
                                        <asp:BoundField HeaderText="TEMP" DataField="TEMP" />
                                        <asp:BoundField HeaderText="FAT" DataField="FAT" />

                                        <asp:BoundField HeaderText="CLR" DataField="CLR" />
                                        <asp:BoundField HeaderText="SNF" DataField="SNF" />
                                        <asp:BoundField HeaderText="Acidity" DataField="Acidity" />
                                        <asp:BoundField HeaderText="COB" DataField="COB" />
                                        <asp:BoundField HeaderText="Remark" DataField="Remark" />
                                        <asp:TemplateField HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                            <HeaderTemplate>
                                               <%-- <input id="rptchk1" type="checkbox" onclick="ReportCheckAll(this)" runat="server" />--%>
                                                 <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static"/>
                                                Shown To Plant
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="rptchk2" runat="server" Checked='<%# Eval("showntoplant").ToString()=="1"?true:false %>' Enabled='<%# Eval("showntoplant").ToString()=="0"?true:false %>' />
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
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtEntryTime').timepicker();
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
        $('#checkAll').click(function () {
            
            var inputList = document.querySelectorAll('.gvDetail tbody input[type="checkbox"]:not(:disabled)');
          
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

        $('#check_All').click(function () {

            var inputList = document.querySelectorAll('.gvTankPosition tbody input[type="checkbox"]:not(:disabled)');

            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('check_All').checked) {
                    inputList[i].checked = true;

                    //ValidatorEnable(Amount,true)
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });
        $(document).on('focus', '.select2-selection.select2-selection--single', function (e) {
            $(this).closest(".select2-container").siblings('select:enabled').select2('open');
        });
    </script>
</asp:Content>

