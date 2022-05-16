<%@ Page Title="" Language="C#" MasterPageFile="~/mis/RTIMaster.master" AutoEventWireup="true" CodeFile="RTIPaymentForm.aspx.cs" Inherits="RTI_RTIApplicantsForms_RTIPaymentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                            <h3 class="box-title">Online Request Payment Form / ऑनलाइन अनुरोध भुगतान फॉर्म</h3>
                            <br />
                            <br />
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body ">
                            <div class="row">
                                <!-- For Online Payment-->
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Payment For RTI</legend>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <table class="table table-responsive table-striped table-bordered table-hover ">
                                                        <tbody>
                                                            <tr>
                                                                <th>Registartion No.</th>
                                                                <td>123/AAS/2541</td>
                                                                <th>Applicant Name</th>
                                                                <td>Aman Verma</td>
                                                            </tr>
                                                            <tr>
                                                                <th>Date Of RTI Filed</th>
                                                                <td>12/08/2018</td>
                                                                <th>RTI Fee</th>
                                                                <td>Rs.10</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <th>Payment Mode</th>
                                                                <%-- <asp:RadioButtonList runat ="server" CssClass="table-bordered table-responsive form-group" RepeatDirection="Horizontal">
                                                                    <asp:ListItem>Internet Banking</asp:ListItem>
                                                                    <asp:ListItem>ATM-cum-debit Card of SBI</asp:ListItem>
                                                                    <asp:ListItem>Credit or Debit Card</asp:ListItem>
                                                                </asp:RadioButtonList>--%>
                                                                <td>
                                                                    <asp:RadioButton ID="rbtnIB" runat="server" Text="Internet Banking" ClientIDMode="Static" Checked="true" onclick="checkChecks1()" /></td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbtnATM" runat="server" Text="ATM-cum-debit Card of SBI" ClientIDMode="Static" onclick="checkChecks2()" /></td>
                                                                <td>
                                                                    <asp:RadioButton ID="rbtnCDC" runat="server" Text="Credit or Debit Card" ClientIDMode="Static" onclick="checkChecks3()" /></td>

                                                            </tr>
                                                            <tr>
                                                                <th></th>
                                                                <td>
                                                                    <asp:RadioButton ID="rbtnStamp" runat="server" Text="Stamp" ClientIDMode="Static" onclick="checkChecks4()" /></td>
                                                                <td colspan="2">
                                                                    <asp:FileUpload ID="fuStamp" runat="server" /></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">

                            <div class="row text-center">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button runat="server" Text="Submit" CssClass="btn btn-success form-control" ID="btnSubmit" Width="12%" ClientIDMode="Static" OnClick="btnSubmit_Click" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        function checkChecks1() {
            debugger
            if (document.getElementById('rbtnIB').checked) {
                document.getElementById('rbtnATM').checked = false;
                document.getElementById('rbtnCDC').checked = false;
                document.getElementById('rbtnStamp').checked = false;
                //document.getElementById('fuStamp').disabled = true;
                //alert ("You may only check ONE checkbox");
                return false;
            }
        }
        function checkChecks2() {
            debugger
            if (document.getElementById('rbtnATM').checked) {
                document.getElementById('rbtnIB').checked = false;
                document.getElementById('rbtnCDC').checked = false;
                document.getElementById('rbtnStamp').checked = false;
                //document.getElementById('fuStamp').disabled = true;
                //alert ("You may only check ONE checkbox");
                return false;
            }
        }
        function checkChecks3() {
            debugger
            if (document.getElementById('rbtnCDC').checked) {
                document.getElementById('rbtnIB').checked = false;
                document.getElementById('rbtnATM').checked = false;
                document.getElementById('rbtnStamp').checked = false;
                //document.getElementById('fuStamp').disabled = true;
                //alert ("You may only check ONE checkbox");
                return false;
            }
        }
        function checkChecks4() {
            debugger
            if (document.getElementById('rbtnStamp').checked) {
                document.getElementById('rbtnIB').checked = false;
                document.getElementById('rbtnATM').checked = false;
                document.getElementById('rbtnCDC').checked = false;
                // document.getElementById('fuStamp').disabled = false;
                //alert ("You may only check ONE checkbox");
                return false;
            }
        }
    </script>
</asp:Content>

