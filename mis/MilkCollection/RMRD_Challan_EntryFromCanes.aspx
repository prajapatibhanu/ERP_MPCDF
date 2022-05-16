<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRD_Challan_EntryFromCanes.aspx.cs" Inherits="mis_MilkCollection_RMRD_Challan_EntryFromCanes" %>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" OnClick="btnSave_Click" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Canes Collection</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Route Detail</legend>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
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
                                        <label>Shift<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvShift" runat="server" Display="Dynamic" OnInit="rfvShift_Init" InitialValue="0" ControlToValidate="ddlShift" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" ValidationGroup="Save" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                            <asp:ListItem Value="Evening">Evening</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="Save"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlccbmcdetail_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                              <%-- <div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkCollectionUnit" AutoPostBack="true" OnSelectedIndexChanged="ddlMilkCollectionUnit_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="5">BMC</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="6">DCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-md-2">
                                    <label>Society<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvBMC" runat="server" Display="Dynamic" ValidationGroup="Save" ControlToValidate="ddlSociety" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Society!'></i>" ErrorMessage="Select Society" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSociety" CssClass="form-control select2" runat="server">
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>BMC Root<span style="color: red;"> *</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvRoot" ValidationGroup="Save"
                                                InitialValue="0" ErrorMessage="Select BMC Root" Text="<i class='fa fa-exclamation-circle' title='Select BMC Root !'></i>"
                                                ControlToValidate="ddlBMCTankerRootName" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></span>
                                        <asp:DropDownList ID="ddlBMCTankerRootName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBMCTankerRootName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Milk Collection-Add </legend>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Type<span style="color: red;"> *</span></label>
                                                <asp:DropDownList ID="ddlMilkType" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                    <asp:ListItem Selected="True" Value="1">Buf</asp:ListItem>
                                                    <asp:ListItem Value="2">Cow</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Milk Quantity (KG)<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvMilkQuantity" runat="server" Display="Dynamic" ValidationGroup="b" ControlToValidate="txtMilkQuantity"  Text="<i class='fa fa-exclamation-circle' title='Enter Milk Quantity!'></i>" ErrorMessage="Enter Milk Quantity" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                                <asp:TextBox ID="txtMilkQuantity" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Milk Quality<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMilkQuality" runat="server" Display="Dynamic" ControlToValidate="ddlMilkQuality" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Quality!'></i>" ErrorMessage="Select Milk Quality" SetFocusOnError="true" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>

                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlMilkQuality" CssClass="form-control" runat="server" OnInit="ddlMilkQuality_Init">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Total Can<span style="color: red;"> *</span></label>
                                                 <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvTotalCan" runat="server" Display="Dynamic" ValidationGroup="b" ControlToValidate="txtTotalCan"  Text="<i class='fa fa-exclamation-circle' title='Enter Total Can!'></i>" ErrorMessage="Enter Total Can" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                                <asp:TextBox ID="txtTotalCan" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>                                       
                                      <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Good Can</label>
                                                <asp:TextBox ID="txtGoodCan" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>  
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" ValidationGroup="b" ID="btnAddSocietyDetails" Text="Add" OnClick="btnAddSocietyDetails_Click" />
                                            </div>
                                        </div>
                                    </div>     
                                                          
                                   <div class="col-md-12">
                                       <div class="table-responsive">
                                           <asp:GridView ID="gv_SampleDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="Milk Type">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblMilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Quantity">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quality">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("Quality") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Total Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblTotalCan" runat="server" Text='<%# Eval("TotalCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField  HeaderText="Good Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblGoodCan" runat="server" Text='<%# Eval("GoodCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                  
                                               </Columns>
                                           </asp:GridView>
                                       </div>
                                   </div>
                                    
                            </fieldset>
                            <div class="row">
                                        <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" OnClick="btnSave_Click" Style="margin-top: 20px;" ValidationGroup="Save" ID="btnSave" Text="Save" />
                                                 <asp:Button runat="server" CssClass="btn btn-default"  Style="margin-top: 20px;"  ID="btnClear" Text="Clear" />
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
                        <div class="box-header">
                            <h3 class="box-title">Canes Collection Details</h3>
                        </div>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtfilterdate" autocomplete="off" AutoPostBack="true" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtfilterdate_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                            <div class="row">
                           <div class="col-md-12">
                                       <div class="table-responsive">
                                           <asp:GridView ID="gvEntryList" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvEntryList_RowCommand">
                                               <Columns>
                                                   
                                                 <asp:TemplateField HeaderText="Date">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("D_Date") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="C_ReferenceNo">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Milk Collection Unit">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblMCU" runat="server" Text='<%# Eval("MilkCollectionUnit") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Society">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblSociety" runat="server" Text='<%# Eval("Society") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Society Code">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblSocietyCode" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Sample No">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblV_SampleNo" runat="server" Text='<%# Eval("V_SampleNo") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Route">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblRoute" runat="server" Text='<%# Eval("Route") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                  
                                                   <asp:TemplateField HeaderText="B/C">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("V_MilkType") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Milk Quality">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblV_MilkQuality" runat="server" Text='<%# Eval("V_MilkQuality") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Total Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblTotalCan" runat="server" Text='<%# Eval("TotalCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField  HeaderText="Good Can">
                                                       <ItemTemplate>
                                                           <asp:Label ID="lblGoodCan" runat="server" Text='<%# Eval("GoodCan") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Kilo">
                                                       <ItemTemplate>
                                                            <asp:Label ID="lblI_MilkQuantity" runat="server" Text='<%# Eval("I_MilkQuantity") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                       <ItemTemplate>
                                                           <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("BI_MilkInRMRDCanRefID") %>' Visible='<%# Eval("HideShowBtn").ToString() == "Show"?true:false %>' OnClientClick="return confirm('Do you really want to delete record?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Columns>
                                           </asp:GridView>
                                       </div>
                                   </div>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <script type="text/javascript">
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
            }
        }
    </script>
        <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
   <script src="js/buttons.colVis.min.js"></script>
   
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100],
            iDisplayLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
            "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('Canes Collection Details').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('Canes Collection Details').bold().fontsize(3).toUpperCase(),
                    filename: 'Canes Collection Details',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
</script>
</asp:Content>

