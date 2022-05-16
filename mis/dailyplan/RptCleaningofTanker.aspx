<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptCleaningofTanker.aspx.cs" Inherits="mis_dailyplan_RptCleaningofTanker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        div#myModal {
            Z-INDEX: 9999;
        }
         .NonPrintable {
                  display: none;
              }
        @media print {
              .NonPrintable {
                  display: block;
              }
              .noprint {
                display: none;
            }
              
          }

    </style>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Cleaning Of Tanker Request</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Cleaning Of Tanker</legend>
                        <div class="row">
                            <div class="col-md-3 noprint">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFilterOffice" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>From Date</label><span class="text-danger">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>To Date</label><span class="text-danger">*</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">
                                                <i class="far fa-calendar-alt"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="txtToDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-success" Style="margin-top: 21px;" runat="server" Text="Search" OnClick="btnSearch_Click" />

                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Cleaning Of Tanker Request Details</legend>
                        <div class="row">
                            <div class="col-md-12 pull-right noprint">
                                <div class="form-group">
                                    <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" />
                                    <asp:Button ID="btnShowntoplant" runat="server" Visible="false" CssClass="btn btn-primary" Text="Shown To Plant" OnClick="btnShowntoplant_Click" OnClientClick="return confirm('Do you really want to update this record?')" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" EmptyDataText="No Record Found" runat="server"  CssClass="table table-bordered GridView1" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("RequestDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTankerCleaningRequest_No" runat="server" Text='<%# Eval("TankerCleaningRequest_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tanker No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTankerNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvddlStatus" runat="server" Display="Dynamic" ControlToValidate="ddlStatus" Text="<i class='fa fa-exclamation-circle' title='Select Status!'></i>" ErrorMessage="Select Status" InitialValue="0" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Update"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Visible="false" CssClass="form-control select2">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                        <asp:ListItem Value="Cleaned">Cleaned</asp:ListItem>
                                                    </asp:DropDownList>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("CleaningOfTanker_ID") %>' Visible='<%# Eval("Status").ToString()=="Pending"?true:false %>' CommandName="EditRecord"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkView" CssClass="label label-default" runat="server" CommandArgument='<%# Eval("CleaningOfTanker_ID") %>' Visible='<%# Eval("Status").ToString()=="Pending"?false:true %>' CommandName="ViewRecord">View</asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    <asp:TextBox ID="txtRemark" Visible="false" runat="server" Text='<%# Eval("Remark") %>' CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="2%">
                                                <HeaderTemplate>
                                                    <%-- <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />--%>
                                                    <asp:CheckBox ID="check_All" runat="server" ClientIDMode="static" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" Checked='<%# Eval("ShownToPlant").ToString()=="1"?true:false %>' runat="server" ToolTip='<%# Eval("CleaningOfTanker_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
             <!--Start Add Tanker Cleaning Modal -->
    <div class="modal fade" id="TankerCleaningModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Tanker Cleaning Report</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <h4 style="text-align: center"><span id="spnOfcName" runat="server"></span></h4>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <h5 style="text-align: center"><span id="spnRequestNo" runat="server"></span></h5>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <h4 style="text-align: center">गुण नियंत्रण शाखा</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <h5 style="text-align: center">Tanker/Tank/Silo क्लीनिंग रिपोर्ट</h5>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>दिनांक/समय</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTankerCleanedDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtTankerCleanedDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 pull-left">
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvTime" runat="server" Display="Dynamic" ControlToValidate="txtTankerCleanedTime" Text="<i class='fa fa-exclamation-circle' title='Enter Time!'></i>" ErrorMessage="Enter Time" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>

                            </span>
                            <div class="form-group">
                                <div class="input-group bootstrap-timepicker timepicker">
                                    <asp:TextBox ID="txtTankerCleanedTime" class="form-control input-small" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>टैंकर क्रमांक</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtTankerNo" Text="<i class='fa fa-exclamation-circle' title='Enter टैंकर क्रमांक!'></i>" ErrorMessage="Enter टैंकर क्रमांक" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtTankerNo"  runat="server" CssClass="form-control" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>टैंक/आर.एम.टी./पी.एम.टी.नं.</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                 <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtTank_RMT_PMT" Text="<i class='fa fa-exclamation-circle' title='Enter टैंक/आर.एम.टी./पी.एम.टी.नं.!'></i>" ErrorMessage="Enter टैंक/आर.एम.टी./पी.एम.टी.नं." SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtTank_RMT_PMT" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>मेन होल/गैसकिट</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtMainHole_GasKit" Text="<i class='fa fa-exclamation-circle' title='Enter मेन होल/गैसकिट!'></i>" ErrorMessage="Enter मेन होल/गैसकिट" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtMainHole_GasKit" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>एयर बेंट</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtAirBent" Text="<i class='fa fa-exclamation-circle' title='Enter एयर बेंट!'></i>" ErrorMessage="Enter एयर बेंट" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtAirBent" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>अनलोडिंग वाल्व</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                 <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ControlToValidate="txtUnLoadingValve" Text="<i class='fa fa-exclamation-circle' title='Enter अनलोडिंग वाल्व!'></i>" ErrorMessage="Enter अनलोडिंग वाल्व" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtUnLoadingValve" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>इनर शैल</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtInnerShell" Text="<i class='fa fa-exclamation-circle' title='Enter इनर शैल!'></i>" ErrorMessage="Enter इनर शैल" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtInnerShell" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>रिमार्क</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtCleanedRemark" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <asp:Button ID="btnSave" ValidationGroup="Save" runat="server"  OnClientClick="return ValidatePage()" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End-->
        </section>
   
    
     <section class="content">
          <div id="divPrint"  class="NonPrintable" runat="server"></div>
      </section>
        </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


    <script>
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        $('#check_All').click(function () {

            var inputList = document.querySelectorAll('.GridView1 tbody input[type="checkbox"]:not(:disabled)');

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
        function ShowTankerCleaningModal() {
            $('#TankerCleaningModal').modal('show');
        }
        function ViewTankerCleaningModal() {
            $('#TankerCleaningDetailsViewModal').modal('show');
        }

    </script>
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtTankerCleanedTime').timepicker();

    </script>
</asp:Content>

