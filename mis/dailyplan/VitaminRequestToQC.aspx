<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VitaminRequestToQC.aspx.cs" Inherits="mis_dailyplan_VitaminRequestToQC"  MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .NonPrintable {
                  display: none;
              }
          @media print {
             
              .noprint {
                display: none;
            }
              .NonPrintable {
                  display: block;
              }  
          }

    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <%--ConfirmationModal Start --%>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
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
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success noprint">

                <div class="box-header">
                    <h3 class="box-title">Vitamin Request To QC</h3>
                </div>
                <div class="box-body">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                    <fieldset runat="server">
                        <legend>Vitamin Request To QC</legend>
                        <div class="row">
                           

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlDS" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select DS!'></i>"
                                                    ErrorMessage="Select DS." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Shift</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlShift" CssClass="form-control select2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Production Section<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlPSection" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Section!'></i>"
                                                    ErrorMessage="Select Section." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlPSection" AutoPostBack="true" OnSelectedIndexChanged="ddlPSection_SelectedIndexChanged"
                                                runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Test Request Type<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlTestRequestType" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Test Request Type!'></i>"
                                                    ErrorMessage="Select Test Request Type." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlTestRequestType" AutoPostBack="true" OnSelectedIndexChanged="ddlTestRequestType_SelectedIndexChanged" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Test Request For<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlmilktestrequestfor" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Test Request For!'></i>"
                                                    ErrorMessage="Select Test Request For." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlmilktestrequestfor"  runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3" id="divVariant" runat="server">
                                        <div class="form-group">
                                            <label>Variant<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvVariant" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlVariant" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Variant!'></i>"
                                                    ErrorMessage="Select Test Request For." SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlVariant"  runat="server" CssClass="form-control select2" >
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>                                                                       
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Milk Quantity(In Kg)<span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Milk Quantity" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity !'></i>"
                                                    ControlToValidate="txtMilkQty" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:TextBox runat="server" autocomplete="off" onkeypress="return validateDec(this,event)" CssClass="form-control" ID="txtMilkQty" placeholder="Enter Vitamin Quantity."></asp:TextBox>
                                        </div>
                                    </div>                                   
                                                         
                        </div> 
                        <div class="row">
                            <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="txtRemarks_R" TextMode="MultiLine" runat="server" placeholder="Enter Remarks..." class="form-control" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                        </div>                                               
                                    
                               <div class="row">
                            <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="a" OnClientClick="return ValidatePage();" CssClass="btn btn-primary" Text="Save" />
                                     <a href="VitaminRequestToQC.aspx" class="btn btn-default">Cancel</a>
                                    </div>                                
                            </div>
                        </div>                                  
                    </fieldset>


                </div>

            </div>

            <fieldset runat="server">
                <legend>Vitamin Request Details</legend>

                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-3 noprint">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFilterOffice" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 noprint">
                            <div class="input-group">
                                <label>Filter Date<span class="text-danger">*</span></label>
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <i class="far fa-calendar-alt"></i>
                                    </span>
                                </div>
                                <asp:TextBox ID="txtFilterDT" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtFilterDT_TextChanged"></asp:TextBox>
                            </div>
                        </div>

                        <hr />
                      
                        <div class="row">
                            <div class="col-md-12 pull-right noprint">
                            <div class="form-group">
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click"/>
                                <asp:Button ID="btnPrint" runat="server"  Visible="false" CssClass="btn btn-default" Text="Print" OnClientClick="window.print();"/>
                                
                            </div>      
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVitaminRequest_No" runat="server" Text='<%# Eval("VitaminRequest_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Shift">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShift" runat="server" Text='<%# Eval("ShiftName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Production Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductSection_Name" runat="server" Text='<%# Eval("ProductSection_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestType" runat="server" Text='<%# Eval("RequestType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Request For">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestFor" runat="server" Text='<%# Eval("RequestFor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Variant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVariant" runat="server" Text='<%# Eval("Variant") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Milk Qty(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMilkQuantity" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequest_Status" runat="server" Text='<%# Eval("Request_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                             <asp:TemplateField HeaderText="Vitamin Dispatch Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVitaminReceived_DT" runat="server" Text='<%# Eval("VitaminReceived_DT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Vitamin Dispatch Qty(In Kg)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVitaminReceivedQuantity" runat="server" Text='<%# Eval("VitaminReceivedQuantity") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("VitaminRequest_ID").ToString() %>' CausesValidation="false" Enabled='<%# Eval("Status").ToString()=="Pending"?true:false %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequest_Remark" runat="server"  Text='<%# Eval("Request_Remark") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            
                        </div>

                    </div>
                </div>
            </fieldset>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>

        
       function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            debugger;
            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }

            }
        }

    </script>
    
   <%-- <script src="../../../mis/js/jquery.js" type="text/javascript"></script>--%>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
    <script type="text/javascript">
        $('#checkAll').click(function () {

            var inputList = document.querySelectorAll('.GridView3 tbody input[type="checkbox"]:not(:disabled)');

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
</asp:Content>

