<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollPayBillMonth_Wise.aspx.cs" Inherits="mis_Payroll_PayRollPayBillMonth_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
      <style>      
        .header {
  padding : 20px 0 20px 0;
  margin-bottom:20px;
  overflow :auto;
  
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
             .pagebreak { page-break-before: always; }
              
            }
          }
         .table1-bordered > thead > tr > th, .table1-bordered > tbody > tr > th, .table1-bordered > tfoot > tr > th, .table1-bordered > thead > tr > td, .table1-bordered > tbody > tr > td, .table1-bordered > tfoot > tr > td {
            border: 1px solid #000000 !important;
        }
        .thead
        {
            display:table-header-group;
        }
        .text-center{
            text-align: center;
        }
        .text-right{
            text-align: right;
        }
        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 2px 5px;
           
        }   
            
           .lblpagenote {
            display: block;
            background: #e0eae7;
            text-align: -webkit-center;
            margin-bottom: 12px;
            padding: 10px;
        }   
           
           .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../image/loader/ProgressImage.gif') 50% 50% no-repeat rgba(0, 0, 0, 0.3);
        }   
            .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
    padding: 1px;
    font-size: 10px;
    border: 1px solid black !important;
    font-family: verdana;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
     <div class="loader"></div>
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Monthly Pay Bill</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" ClientIDMode="Static" Enabled="false">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlOffice" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFinancialYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="January">January</asp:ListItem>
                                            <asp:ListItem Value="February">February</asp:ListItem>
                                            <asp:ListItem Value="March">March</asp:ListItem>
                                            <asp:ListItem Value="April">April</asp:ListItem>
                                            <asp:ListItem Value="May">May</asp:ListItem>
                                            <asp:ListItem Value="June">June</asp:ListItem>
                                            <asp:ListItem Value="July">July</asp:ListItem>
                                            <asp:ListItem Value="August">August</asp:ListItem>
                                            <asp:ListItem Value="September">September</asp:ListItem>
                                            <asp:ListItem Value="October">October</asp:ListItem>
                                            <asp:ListItem Value="November">November</asp:ListItem>
                                            <asp:ListItem Value="December">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                            <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                            <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                            <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                            <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-5">
                                        <div class="form-group">
                                            <label>Bank Name<span style="color: red;"> *</span></label>
                                             <asp:ListBox runat="server" ID="ddlBank" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div> 
                                <div class="col-md-2" style="margin-top:20px;">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>

                                </div>
                                <%--<div class="col-md-2" style="margin-top:20px;">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="PayRollPayBillMonth_Wise.aspx" class="btn btn-block">Clear</a>
                                    </div>

                                </div>--%>
                                 <div class="col-md-2" id="pnlshow" runat="server" visible="false" style="margin-top:20px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-primary" Text="Print" OnClientClick="window.print()" />
                                        <asp:Button ID="btnExportAll" runat="server" CssClass="btn btn-success" OnClick="btnExportAll_Click" Text="Export" />
                                    </div>

                                </div>
                            </div>
                           <%-- <div class="row">
                                
                            </div>--%>
                            <%--<div class="row">

                                <div class="col-md-12">
                                    <p style="color: #123456; font-size: 15px;" runat="server">
                                        <asp:Label ID="lblDeductionDetails" CssClass="lblDeductionDetails" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>

                                <div class="col-md-12">

                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Emp_Name" HeaderText="Employee/Officer" />
                                            <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                                            <asp:BoundField DataField="Salary_NetSalary" HeaderText="Amount" ItemStyle-CssClass="alignR" />
                                            <asp:BoundField DataField="Bank_AccountNo" HeaderText="Bank Account Number" />
                                            <asp:BoundField DataField="Bank_IfscCode" HeaderText="IFSC Code" />
                                           <asp:BoundField DataField="Bank_Name" HeaderText="Address" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>--%>

                           

                            <div class="row">
                               
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div_page_content" runat="server" class="page_content"></div>
                                  </div>
                                </div>
                            </div>
                        
                  
              
                       
                 
                </div>
            </div>
                    </div>
                </div>
        </section>
          <section class="content">
            <div id="Print" runat="server" class="NonPrintable"></div> 
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

<%--    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
   <%-- <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>--%>
       <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script>

        
            $('.loader').fadeOut();
            $("#<%=btnShow.ClientID%>").click((function () {

                if (Page_IsValid) {
                    $('.loader').show();
                    return true;

                }
            }));

     

        function validateform() {
            var msg = "";
            $("#valddlOffice").html("");
            $("#valddlFinancialYear").html("");
            $("#valddlMonth").html("");

            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select year. \n";
                $("#valddlFinancialYear").html("Select year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valddlMonth").html("Select Month.");
            }
            if (document.getElementById('<%=ddlEmp_TypeOfPost.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type of Post. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }


        $(document).ready(function () {
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
                        title: $('.lblDeductionDetails').attr('title'),
                        exportOptions: {
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.lblDeductionDetails').attr('title'),
                        exportOptions: {
                            orthogonal: 'sort',
                            format: {
                                body: function (data, row, column, node) {
                                    data = data.trim();
                                    data = column === 4 ? "\0" + data : data;
                                    return data.replace(/(&nbsp;|<([^>]+)>)/ig, "");
                                    //var data = data.find("span").text();
                                    //return data;
                                }
                            }
                        },
                        //customizeData: function (data) {
                        //    for (var i = 0; i < data.body.length; i++) {
                        //        for (var j = 0; j < data.body[i].length; j++) {
                        //            if (j ==4) {
                        //                data.body[i][j] = '\u200C' + data.body[i][j];
                        //            }

                        //        }
                        //    }
                        //},
                        // footer: true
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
            //t.on('order.dt search.dt', function () {
            //    t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            //        cell.innerHTML = i + 1;
            //    });
            //}).draw();
        });
        $(function () {
            $('[id*=ddlBank]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
        });
    </script>
   
</asp:Content>

