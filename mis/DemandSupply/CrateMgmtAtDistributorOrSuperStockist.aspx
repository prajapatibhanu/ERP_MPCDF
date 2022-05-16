<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CrateMgmtAtDistributorOrSuperStockist.aspx.cs" Inherits="mis_DemandSupply_CrateMgmtAtDistributorOrSuperStockist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
    </style>
    <script>
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
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

        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
             var checkBoxes = gridView.getElementsByTagName("input");
             for (var i = 0; i < checkBoxes.length; i++) {
                 if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }
         function ReceivedCrateValidate(sender, args) {
             var gridView = document.getElementById("<%=GridView1.ClientID %>");
             var receivedcrate = gridView.getElementsByTagName("input");
             for (var i = 0; i < receivedcrate.length; i++) {
                 if (receivedcrate[i].value != "" && receivedcrate[i].type == "text") {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }

         function calculate() {
             var salaries = document.getElementById('<%=GridView1.ClientID %>').getElementsByTagName("input");
             var total = 0;
             for (var i = 0; i < salaries.length ; i++) {
                 if (salaries[i].type == "text" && salaries[i].id.indexOf("txttotalcratereceived") >= 0)
                     total = total + parseFloat(salaries[i].value);
             }
             var label = document.getElementById('<%=GridView1.ClientID %>').getElementsByTagName("span");
             if (label[0].id.indexOf("lblCrateReceived") >= 0)
                 label[0].innerHTML = total;
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
            <!-- SELECT2 EXAMPLE -->
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Crate Management</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Date ,Shift / दिनांक ,शिफ्ट 
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtSupplyDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtSupplyDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtSupplyDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlShift" runat="server" OnInit="ddlShift_Init" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category / वस्तू वर्ग</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" AutoPostBack="true" OnInit="ddlItemCategory_Init" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="pnlparlourdetails" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="false" EmptyDataText="No Record Found." DataKeyNames="MilkOrProductDemandId" EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="30">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                                                ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Booth Name" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBName" Text='<%# Eval("BName") %>' runat="server" />
                                                            <asp:Label ID="lblRetailerTypeID" Visible="false" Text='<%# Eval("RetailerTypeID") %>' runat="server" />
                                                            <asp:Label ID="lblRouteId" Visible="false" Text='<%# Eval("RouteId") %>' runat="server" />
                                                            <asp:Label ID="lblBoothId" Visible="false" Text='<%# Eval("BoothId") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplied Crate" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" autocomplete="off" Enabled="false" CausesValidation="true" Text='<%# Eval("totalcrate")%>' CssClass="form-control" ID="txttotalcrate" ClientIDMode="Static"></asp:TextBox>
                                                            <asp:HiddenField ID="hfTotalCrate" Value='<%# Eval("totalcrate")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="5px">
                                                        <HeaderTemplate>
                                                            No. of Crate Received 
                                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                                                ClientValidationFunction="ReceivedCrateValidate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="revcr" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                                    ErrorMessage="Invalid No. of Crate Received, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid No. of Crate Received, Accept Number only. !'></i>" ControlToValidate="txttotalcratereceived"
                                                                    ValidationExpression="^[0-9]*$">
                                                                </asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox runat="server" autocomplete="off" Text="0" onfocusout="FetchData(this)" CausesValidation="true" CssClass="form-control" ID="txttotalcratereceived" MaxLength="5" onkeypress="return validateNum(event);" placeholder="Enter No. of Crate Received." ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCrateReceived" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of Crate Broken" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="revcb" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                                    ErrorMessage="Invalid No. of Crate Broken, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid No. of Crate Broken, Accept Number only. !'></i>" ControlToValidate="txttotalcratebroken"
                                                                    ValidationExpression="^[0-9]*$">
                                                                </asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox runat="server" autocomplete="off" Text="0" onfocusout="FetchData(this)" CausesValidation="true" CssClass="form-control" ID="txttotalcratebroken" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter No. of Crate Broken." ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCrateBroken" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of Crate Missing" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="revcm" Enabled="false" runat="server" Display="Dynamic" ValidationGroup="a"
                                                                    ErrorMessage="Invalid No. of Crate Missing, Accept Number only. !" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Invalid No. of Crate Missing, Accept Number only. !'></i>" ControlToValidate="txttotalcratemissing"
                                                                    ValidationExpression="^[0-9]*$">
                                                                </asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox runat="server" autocomplete="off" Text="0" onfocusout="FetchData(this)" CausesValidation="true" CssClass="form-control" ID="txttotalcratemissing" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter No. of Crate Missing." ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblCrateMissing" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Date" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtReceivedDate"
                                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtReceivedDate" MaxLength="10" placeholder="Select Received Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-end-date="1d" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <span class="pull-right">
                                                                <asp:RegularExpressionValidator ID="revcremark" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtremark"
                                                                    ErrorMessage="Only Alphanumeric & some special symbol ',()-_.' allow " Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric & some special symbol ',()-_.' !'></i>"
                                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z\s,-_.)(]+$">
                                                                </asp:RegularExpressionValidator>
                                                            </span>
                                                            <asp:TextBox runat="server" autocomplete="off" CausesValidation="true" CssClass="form-control" ID="txtremark" MaxLength="200" placeholder="Enter Remark." ClientIDMode="Static"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                                <div class="row" id="pnlsubmit" runat="server" visible="false">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit"
                                                Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>

                        </div>

                    </div>
                </div>

            </div>
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {


                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function FetchData(button) {
            var row = button.parentNode.parentNode;
            var label1 = GetChildControl(row, "hfTotalCrate").value;
            var label2 = GetChildControl(row, "txttotalcratereceived").value;
            var label3 = GetChildControl(row, "txttotalcratebroken").value;
            var label4 = GetChildControl(row, "txttotalcratemissing").value;

            if (label2 == '') {
                label2 = 0;

            }
            if (label3 == '') {

                label3 = 0;
            }
            if (label4 == '') {

                label4 = 0;
            }

            var rbm = (parseInt(label2) + parseInt(label3) + parseInt(label4));

            if (parseInt(rbm) > parseInt(label1)) {
                alert('Sum of Received,Broken & Missing Crate Must Less than or Equal to Supplied Crate.');
                GetChildControl(row, "txttotalcratereceived").value = ''
                GetChildControl(row, "txttotalcratebroken").value = '';
                GetChildControl(row, "txttotalcratemissing").value = '';
                GetChildControl(row, "txttotalcratereceived").focus();

            }
            else {

            }

            return false;
        };
        function GetChildControl(element, id) {
            var child_elements = element.getElementsByTagName("*");
            for (var i = 0; i < child_elements.length; i++) {
                if (child_elements[i].id.indexOf(id) != -1) {
                    return child_elements[i];
                }
            }
        };
    </script>
</asp:Content>
