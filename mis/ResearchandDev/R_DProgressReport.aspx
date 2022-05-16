<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="R_DProgressReport.aspx.cs" Inherits="mis_ResearchandDev_R_DProgressReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <script src="/mis/js/jquery.js" type="text/javascript"></script>
    <script src="../js/bootstrap-multiselect.js"></script>
    <link href="/mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <%--<script src="/mis/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "white";
            }
            else {
                //If not checked change back to original color
                row.style.backgroundColor = "white";
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function ShowPopupAddDates() {
            $('#addModalDates').modal('show');
        }
        function ShowPopupSurvery() {
            $('#SurveryModel').modal('show');
        }
        function ShowPopupImplement() {
            $('#ImplementModel').modal('show');
        }
        function ShowPopupEditImplement() {
            $('#EditImplementModel').modal('show');
        }

    </script>

    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkoffice.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Progress Of Research & Development Plan</h3>
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                               <fieldset>
                            <legend>View Progress of Research & Development Plan and Implementation to Dugdh Sangh (अनुसंधान और विकास योजना की जानकारी देखे और दुग्ध संघ को कार्यान्वयन करें)
                            </legend>
                            <div class="col-md-12">
                                <asp:GridView ID="grdlist" CssClass="table table-bordered" runat="server" Width="100%" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnRowCommand="grdlist_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RDType" HeaderText="Type" />
                                        <asp:BoundField DataField="PlanType" HeaderText="Plan Type" />
                                        <asp:BoundField DataField="ResearchTitle" HeaderText="Title" />
                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                                        <asp:BoundField DataField="RDStatus" HeaderText="Status" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnview" runat="server" Text="View" ToolTip="View Progress of Research & Devlopement" CssClass="btn btn-info" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="Detail" ><i class="fa  fa-camera"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnSurveysave" runat="server" Text="Survey" ToolTip="Survey of Research & Devlopement" CssClass="btn btn-dropbox" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="Survey" Visible='<%#Convert.ToBoolean(Eval("TIRDStatus")) %>' ><i class="fa fa-area-chart"></i></asp:LinkButton>

                                                <asp:LinkButton ID="btnimplemention" runat="server" Text="Implement To DS" ToolTip="Research & Devlopement Implementation to Dugdh Sangh" CssClass="btn btn-primary" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="implement" Visible='<%#Convert.ToBoolean(Eval("TIRDStatus")) %>' ><i class="fa fa-arrow-circle-down"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEditimplemention" runat="server" Text="Edit Implementation" ToolTip="Edit Research & Devlopement Implementation to Dugdh Sangh" CssClass="btn btn-bitbucket" CommandArgument='<%#Eval("RDPlanID") %>' CommandName="Editimplement" Visible='<%#Convert.ToBoolean(Eval("isimplemented")) %>' ><i class="fa fa-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdncount" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnValue" runat="server" Value="0" />
                            </div></fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="addModalDates" tabindex="-1" role="dialog" aria-labelledby="addModalDates">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Project Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 50%">Research Type:
                                        <asp:Label ID="lblRDType" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>
                                    <td style="width: 50%">Plan Type:
                                        <asp:Label ID="lblPlanType" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 50%">Title:
                                        <asp:Label ID="lblTitle" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>
                                    <td style="width: 50%">Start Date:
                                        <asp:Label ID="lblStartDate" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2">Details:<asp:Label ID="lbldetails" runat="server" ForeColor="Maroon">                                   
                                    </asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2">Expected OutCome:<asp:Label ID="lblOutcomes" runat="server" ForeColor="Maroon">                                   
                                    </asp:Label></td>
                                </tr>
                            </table>
                            <span style="color: maroon">Progress Details</span>
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 100%" colspan="2">
                                        <asp:GridView ID="grd" CssClass="table table-bordered" runat="server" EmptyDataText="No Data Available" Width="100%" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnRowCommand="grdlist_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProgressDate" HeaderText="Progress Date" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                <asp:BoundField DataField="RDStatus" HeaderText="Status" />

                                            </Columns>

                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>


                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <%-- <asp:Button runat="server" ID="Button1" class="btn btn-success" Text="Save"></asp:Button>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="SurveryModel" tabindex="-1" role="dialog" aria-labelledby="SurveryModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Project Details</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered">
                                <tr>
                                    <td style="width: 100%">Research Type:
                                        <asp:Label ID="lblRDType1" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>
                                    <%--<td style="width: 50%">Plan Type:
                                        <asp:Label ID="lblPlanType1" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>--%>
                                </tr>
                                <tr>
                                    <td style="width: 100%">Title:
                                        <asp:Label ID="lblTitle1" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>
                                    <%--  <td style="width: 50%">Start Date:
                                        <asp:Label ID="lblStartDate1" runat="server" ForeColor="Maroon">                                   
                                        </asp:Label></td>--%>
                                </tr>
                                <tr>
                                    <td style="width: 100%">Details:<asp:Label ID="lbldetails1" runat="server" ForeColor="Maroon">                                   
                                    </asp:Label></td>
                                </tr>
                                <%-- <tr>
                                    <td style="width: 100%" colspan="2">Expected OutCome:<asp:Label ID="lblOutcomes1" runat="server" ForeColor="Maroon">                                   
                                    </asp:Label></td>
                                </tr>--%>
                            </table>
                            <span style="color: maroon">SurveryDetails</span>
                            <div class="row" id="SurveyEntry" runat="server">
                                <table class="table table-bordered">
                                    <tr>
                                        <td>
                                            <div class="col-md-6">

                                                <label>Survey Date</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtSurveyDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtSurveyDate" BackColor="Yellow" ForeColor="Maroon" ValidationGroup="b" ToolTip="Please Enter Survey Date"></asp:RequiredFieldValidator>

                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="col-md-12">

                                                <label>Survey Details</label>
                                                <asp:TextBox ID="txtSurveyDetails" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control">                                   
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSurveyDetails" BackColor="Yellow" ForeColor="Maroon" ValidationGroup="b" ToolTip="Please Enter Survey Details"></asp:RequiredFieldValidator>


                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="row" id="SurveyDetail" runat="server">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdsurvey" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Data Availble">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SurveyDate" HeaderText="Survey Date" />
                                            <asp:BoundField DataField="SurveyRemark" HeaderText="Remark" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button runat="server" ID="btnSurvey" class="btn btn-success" Text="Save" OnClick="btnSurvey_Click" CausesValidation="true" ValidationGroup="b"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ImplementModel" tabindex="-1" role="dialog" aria-labelledby="ImplementModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Implementation Research to DS</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Assign Research to DS center</span>
                            <div class="row">
                                <div class="col-md-12" id="ImplementationEntry" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:GridView ID="chkoffice" runat="server" CssClass="table table-bordered"
                                                AutoGenerateColumns="false" Font-Names="Arial"
                                                Font-Size="11pt" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                                            <asp:HiddenField ID="hdnDS" runat="server" Value='<%#Eval("OfficeID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="150px" DataField="OfficeName" HeaderText="दुग्ध संघ" />
                                                </Columns>
                                            </asp:GridView>

                                            <%--<input type="checkbox" id="ckbCheckAll" onchange="Selectall()" />
                                            <%--<asp:CheckBox runat="server" ID="cbSelectAll" Text="Select/Deselect All" CssClass="JchkAll" onchange="Selectall();"/>--%>
                                            <%--<asp:CheckBoxList ID="chkoffice" runat="server" Width="160%" CellPadding="3" CellSpacing="3" CssClass="table table-bordered JchkGrid"></asp:CheckBoxList>--%>
                                            <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one DS." ValidationGroup="m"
                                                ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12" id="ImplementationDetail" runat="server">
                                    <asp:GridView ID="grdimplementation" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Data Availble">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DSNAME" HeaderText="DS NAME" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button runat="server" ID="btnimplement" class="btn btn-success" Text="Save" OnClick="btnimplement_Click" CausesValidation="true" ValidationGroup="m"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="EditImplementModel" tabindex="-1" role="dialog" aria-labelledby="EditImplementModel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <span style="color: white">Update Implementated DS</span>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                            <span style="color: maroon">Edit Assign Research to DS center</span>
                            <div class="row">
                                <div class="col-md-12" id="Div1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:CheckBoxList ID="chkDS" runat="server" Width="160%" CellPadding="3" CellSpacing="3" CssClass="table table-bordered"></asp:CheckBoxList>
                                            <asp:CustomValidator ID="CustomValidator2" ErrorMessage="Please select at least one DS." ValidationGroup="m"
                                                ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
                                        </div>

                                    </div>
                                </div>

                            </div>

                        </div>
                        <%-- OnClick="btn_save_Click"--%>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            <asp:Button runat="server" ID="btneditimplementation" class="btn btn-success" Text="Save" OnClick="btneditimplementation_Click" CausesValidation="true" ValidationGroup="m"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

