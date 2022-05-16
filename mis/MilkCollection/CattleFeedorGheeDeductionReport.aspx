<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CattleFeedorGheeDeductionReport.aspx.cs" Inherits="mis_MilkCollection_CattleFeedorGheeDeductionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">

   <div class="content-wrapper">
       <section class="content">
           <div class="row">
               <div class="col-md-12">
                   <div class="box box-success">
                       <div class="box-header">
                           <h3 class="box-title">Cattle Feed /Ghee Deduction Report</h3>
                       </div>
                       <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                       <div class="box-body">
                           <fieldset>
                               <legend>Filter</legend>
                               <div class="row">
                                   <div class="col-md-2">
                                       <div class="form-group">
                                      
                                           <label>CC</label>
                                            <span class="pull-right"></span>
                                           <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                          
                                       </div>
                                   </div>

                                   <div class="col-md-2">
                                       <div class="form-group">
                                           <label>From Date</label>
                                           <span style="color:red">*</span>
                                           <span class="pull-right">
                                               <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFdt" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid From Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                           </span>

                                           <div class="input-group date">
                                               <div class="input-group-addon">
                                                   <i class="fa fa-calendar"></i>
                                               </div>
                                               <asp:TextBox ID="txtFdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                           </div>
                                       </div>
                                   </div>

                                   <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Please Enter To Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter To Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtTdt" ErrorMessage="Invalid To Date" Text="<i class='fa fa-exclamation-circle' title='Invalid To Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtTdt" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Select To Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                   <div class="col-md-1" style="margin-top:20px;">
                                       <div class="form-group">
                                      <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />
 
                                       </div>
                                   </div>
                               </div>
                           </fieldset>
                       </div>
                       <div class="box-body">
                           <fieldset>
                               <legend>Report</legend>
                               <div class="row">
                                <div class="col-m-12 pull-right noprint">
                                    <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click" /> 

                                    </div>
                                </div
                                   <div class="col-md-12">
                                       
                                            <asp:GridView ID="GridView1" ShowFooter="True" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1  %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Society Code" DataField="Office_Code" />
                                                    <asp:BoundField HeaderText="Society Name" DataField="Office_Name_E" />
                                                    <asp:BoundField HeaderText="Billing Period" DataField="BillingPeriod" />
                                                    <asp:BoundField HeaderText="Head Name" DataField="HeadName" />  
                                                    <asp:TemplateField HeaderText="Head Amount">
                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHeadAmount" runat="server" Text='<%# Eval("HeadAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:BoundField HeaderText="Remark" DataField="HeadRemark" />  
                                                </Columns>
                                            </asp:GridView>
                                       
                                   </div>
                               </div>
                           </fieldset>
                       </div>
                   </div>
               </div>
           </div>
       </section>
       <asp:HiddenField ID="hfvalue" runat="server" />
   </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">

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
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
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
                    title: document.getElementById('<%= hfvalue.ClientID%>').value + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CC:-' + $("#ddlccbmcdetail option:selected").text() + '<br/><span style="margin-top:50px;"> CC wise CattleFeed/Ghee Deduction Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + 'PERIOD :' + document.getElementById('<%= txtFdt.ClientID%>').value + ' -  ' + document.getElementById('<%= txtTdt.ClientID%>').value + "</span>",
                    customize: function (doc) {
                        $(doc.document.body).find('h1').css('font-size', '12pt');
                        $(doc.document.body).find('h1').css('text-align', 'left');
                        $(doc.document.body).find('h1').css('padding-top', '20px');
                    },
                    exportOptions: {

                        // javascript: print(),

                        columns: [0, 1, 2, 3, 4, 5,6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5,6]
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

