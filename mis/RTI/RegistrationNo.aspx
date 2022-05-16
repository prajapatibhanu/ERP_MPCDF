<%@ Page Title="" Language="C#" MasterPageFile="~/mis/RTIMaster.master" AutoEventWireup="true" CodeFile="RegistrationNo.aspx.cs" Inherits="RTI_ViewRTI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        th,td{
            background-color:white;
            color:black;
            text-align:left;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%--<section class="content-header">
            <div class="row">
                <!-- left column -->
                <div class="col-md-10 col-md-offset-1">
                    
                </div>
            </div>
        </section>--%>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success " id="DetailDiv" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Your RTI Request Successfully / आपका आरटीआई आवेदन सफलतापूर्वक हो चुका है|</h3>
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                       
                            <div class="box-body ">
                            <div class="row">

                                <!-- RTI File Date-->

                                <div class="col-md-12">
                                    <div class="form-group">
                                <label class="headingcolor" style="color: #1ccbb8;">Note:- Please Note Down Registration No.</label>
                            </div>
                                    <div class="form-group">
                                         <div id="printdiv">
                                                     <table class="table table-striped table-bordered table-hover">
                                                        <tbody>
                                                            <tr>
                                                                <th>Registartion No.</th>
                                                                <td id="tdreg" runat="server">123/AAS/2541</td>
                                                            </tr>
                                                             
                                                            <tr>
                                                                <th>Applicant Name</th>
                                                                <td>Aman Verma</td>
                                                            </tr>
                                                              
                                                            <tr>
                                                                <th>Date Of RTI Filling</th>
                                                                <td>12/08/2018</td>
                                                            </tr>
                                                             
                                                            <tr>
                                                                <th>Request filed with</th>
                                                                <td id="td3" runat="server" style="color:red">Pending</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                             </div>
                                                </div>
                                </div>
                            </div>

                            <%--For Contact Details--%>

                           
                                <div class ="row">
                                    <div class="col-md-12">
                                        <h4 class="box-title">Contact Details</h4>
                                        <div class="form-group">
                                                    <table class="table table-responsive table-striped table-bordered table-hover ">
                                                        <tbody>
                                                            <tr>
                                                                <th>Telephone No.</th>
                                                                <td id="td1" runat="server">0755-1234568</td>
                                                            </tr>
                                                            <tr>
                                                                <th>Email</th>
                                                                <td>mpagro@gmail.com</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                    </div>
                                </div>
                            

                            <%--For Print--%>

                            <div class="box-footer">
                            
                                <div class="row text-center">
                        <div class="col-md-12">
                            <div class="form-group">
                            <button type="button" class="btn btn-block btn-success" onclick="printscreen();" style="width:60px;">Print</button>
                        </div>
                    </div> 

                                </div>
                            </div>
                        </div>
                     
                        

                    </div>
                </div>

                
                <!-- /.box -->

            </div>


            <!-- /.row -->

        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function printscreen() {
            debugger
            var prtContent = document.getElementById("printdiv");
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
</asp:Content>

