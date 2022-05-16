<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FilledTankerReferenceDetailsViewAndCancel.aspx.cs" Inherits="mis_MilkCollection_FilledTankerReferenceDetailsViewAndCancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->

            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Filled Tanker Gate Pass Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>


                    <div class="table-responsive">

                        <asp:GridView ID="gv_viewreferenceno" ShowHeader="true" AutoGenerateColumns="false" CssClass="datatable table table-hover table-bordered pagination-ys" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSealNo" ToolTip='<%# Eval("BI_MilkInOutRefID") %>' runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reference Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDT_TankerDispatchDate" runat="server" Text='<%# Eval("DT_CreatedOn","{0:d-MMM-yyyy hh:mm tt}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								
								 <asp:TemplateField HeaderText="Vehicle No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblV_VehicleNo" ToolTip='<%# Eval("V_DriverName") +" ("+ Eval("V_DriverMobileNo")+ ")" %>' runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reference No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblC_ReferenceNo" runat="server" Text='<%# Eval("C_ReferenceNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
 
                                <asp:TemplateField HeaderText="CC Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								
								<asp:TemplateField HeaderText="Challan No / Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChallan_No" runat="server" Text='<%# Eval("Challan_No") %>'></asp:Label>
										
                                    </ItemTemplate>
                                </asp:TemplateField>
								
								  
								
								 
  
								<asp:TemplateField HeaderText="Received at any CC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQAEntryStatus" runat="server" Text='<%# Eval("QAEntryStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								
								
							  

                                <asp:TemplateField HeaderText="Reference Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" ToolTip='<%# Eval("RefCancelRemark") %>' runat="server" Text='<%# Eval("RefCancelStatus") %>'></asp:Label>
										 <br/> <asp:Label ID="lblEmp_Name1" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
										 <br/><asp:Label ID="lblRefCancelDT1" runat="server" Text='<%# Eval("RefCancelDT","{0:d-MMM-yyyy hh:mm tt}") %>'></asp:Label>
										 
                                        <asp:Label ID="lblRefCancelStatusF" Visible="false" runat="server" Text='<%# Eval("RefCancelStatusF") %>'></asp:Label>
                                        <asp:Label ID="lblBI_MilkInOutRefID" Visible="false" runat="server" Text='<%# Eval("BI_MilkInOutRefID") %>'></asp:Label>
                                        <asp:Label ID="lblChallan_Validation" Visible="false" runat="server" Text='<%# Eval("Challan_Validation") %>'></asp:Label>
                                        <asp:Label ID="lblI_TankerID" Visible="false" runat="server" Text='<%# Eval("I_TankerID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <a href='../MilkCollection/GatePassReferenceDetails.aspx?Rid=<%# new APIProcedure().Encrypt(Eval("BI_MilkInOutRefID").ToString()) %>' target="_blank" title="Print Gate Pass"><i class="fa fa-print"></i></a>
                                        &nbsp;&nbsp; 
                                        <asp:LinkButton ID="btnRefCancel" ToolTip="Gate Pass Cancel" OnClick="btnRefCancel_Click" runat="server"><i class='<%#  Eval("RefCancelStatusF").ToString() =="True" || Eval("Challan_Validation").ToString() !="" ? "" : "fa fa-close" %>' style='font-size:20px;color:red'></i></asp:LinkButton>

                                        <%--OnClientClick="return confirm('Do you want to Check Printer Status')"--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </section>


        <asp:ValidationSummary ID="v1" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />

        <div class="modal" id="ItemDetailsModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content" style="height: 420px;">
                    <div class="modal-header">
                        <asp:LinkButton runat="server" class="close" ID="btnCrossButton" OnClick="btnCrossButton_Click"><span aria-hidden="true">×</span></asp:LinkButton>

                        <h4 class="modal-title">Cancel Reference</h4>
                    </div>
                    <div class="modal-body">

                        <asp:Label ID="lblModalMsg" runat="server" Text=""></asp:Label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row" style="height: 250px; overflow: scroll;">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <section class="content">
                                                <div class="box box-Manish" style="min-height: 250px;">
                                                    <div class="box-header">
                                                        <h3 class="box-title">Cancel Reference</h3>
                                                    </div>
                                                    <!-- /.box-header -->
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:Label ID="lblPopupMsg" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <fieldset>
                                                            <legend>Remark</legend>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <label>Remark.<span style="color: red;"> *</span></label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="save"
                                                                            ErrorMessage="Enter Cancel Remark." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Cancel Remark. !'></i>"
                                                                            ControlToValidate="txtRefCancelRemark" Display="Dynamic" runat="server">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:TextBox runat="server" autocomplete="off" TextMode="MultiLine" CssClass="form-control" ID="txtRefCancelRemark" MaxLength="100" placeholder="Enter Remark"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>

                                                    </div>

                                                </div>
                                                <!-- /.box-body -->

                                            </section>
                                            <!-- /.content -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-primary" ValidationGroup="save" ID="btnSaveTankerDetails" Text="Submit" OnClientClick="return ValidateT()" />

                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

    </div>

    <div class="modal fade" id="myModalT" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #d9d9d9;">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                    <h4 class="modal-title" id="myModalLabelT">Confirmation</h4>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <p>
                        <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT" OnClick="btnYesT_Click" Style="margin-top: 20px; width: 50px;" />
                    <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
             
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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





    <script type="text/javascript">

        function ValidateT() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSaveTankerDetails.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSaveTankerDetails.ClientID%>').value.trim() == "Submit") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT').modal('show');
                    return false;
                }
            }
        }

        function myItemDetailsModal() {
            $("#ItemDetailsModal").modal('show');

        }

    </script>
	
	

</asp:Content>

