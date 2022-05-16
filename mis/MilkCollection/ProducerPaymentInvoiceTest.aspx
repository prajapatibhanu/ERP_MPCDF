<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProducerPaymentInvoiceTest.aspx.cs" Inherits="mis_MilkCollection_ProducerPaymentInvoiceTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .table1 > thead > tr > th1, .table1 > tbody > tr > th1, .table1 > tfoot > tr > th1, .table1 > thead > tr > td, .table1 > tbody > tr > td, .table1 > tfoot > tr > td {
            padding: 6px;
            line-height: 1.42857143;
            vertical-align: top;
        }

        .NonPrintable {
                  display: none;
              }

        .table2 > thead > tr > th, .table2 > tbody > tr > th, .table2 > tfoot > tr > th, .table2 > thead > tr > td, .table2 > tbody > tr > td, .table2 > tfoot > tr > td {
            padding: 6px;
            line-height: 1.42857143;
            vertical-align: top;
            border: 1px solid black;
        }

        .headertable {
            width: 100%;
        }
        .page {

            /*            margin-top: 200px;*/
            page-break-after: always;
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
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Producer Payment Invoice</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body ">
                    <fieldset class="noprint">
                        <legend>Filter</legend>
                        <div class="row noprint">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Milk Collection Office </label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtSociatyName" Text="<i class='fa fa-exclamation-circle' title='Enter Sociaty Name!'></i>" ErrorMessage="Enter Sociaty Name" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtSociatyName" Enabled="false" autocomplete="off" placeholder="Enter Sociaty Name" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Payment From Date  </label>
                                    <span style="color: red">*</span>
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

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Payment To Date  </label>
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

                            <div class="col-md-1" style="margin-top: 20px;">
                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="a" ID="btnSubmit" Text="Search" AccessKey="S" />

                                    </div>
                                </div>
                            </div>




                        </div>
                        <div class="row" runat="server">
                            <div class="col-md-12">
                                <table class="table1">
                                    <tbody>
                                        <tr>
                                            <td><b>समिति कोड</b></td>
                                            <td>
                                                <asp:Label ID="lblOfficeCode" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>समिति का नाम</b></td>
                                            <td>
                                                <asp:Label ID="lblOfficeName" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>समिति पेमेंट साइकिल कोड</b></td>
                                            <td>
                                                <asp:Label ID="lblCyclecode" runat="server" Text=""></asp:Label></td>
                                            <td><b>तारीख से</b></td>
                                            <td>
                                                <asp:Label ID="lblFromDate" runat="server" Text=""></asp:Label></td>
                                            <td><b>तारीख तक</b></td>
                                            <td>
                                                <asp:Label ID="lblToDate" runat="server" Text=""></asp:Label></td>
                                            <td colspan="9" style="padding-left: 250px;"><b>प्रिंट दिनांक</b></td>
                                            <td>
                                                <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>भुगतान के प्रकार:</b></td>
                                            <td>सभी</td>
                                        </tr>
                                    </tbody>
                                </table>

                                <div class="table-responsive">
                                    <table class="table2" style="max-width: 100%">
                                        <tbody>
                                            <tr>
                                                <th>क्रमांक</th>
                                                <th>नाम</th>
                                                <th>कुल शिफ्ट</th>
                                                <th>बैंक खता नंबर</th>
                                                <th>आईएफएससी कोड</th>
                                                <th>मात्रा(ली.)</th>
                                                <th>औसत. फैट(%)</th>
                                                <th>औसत. एस.एन.एफ(%)</th>
                                                <th>औसत.सीएल.आर.(%)</th>
                                                <th>राशि(रु.)</th>
                                                <th>Earning</th>
                                                <th>कटोती(रु.)</th>
                                                <th>Adjust Amount</th>
                                                <th>Final Amount</th>
                                                <th>हस्ताक्षर</th>
                                            </tr>

                                            <tr>
                                                <td colspan="15">पेमेंट प्रकार - </td>
                                            </tr>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>1</td>
                                                        <td><%# Eval("ProducerName")%></td>
                                                        <td><%# Eval("TotalShift")%></td>
                                                        <td><%# Eval("AccountNo")%></td>
                                                        <td><%# Eval("IFSC")%></td>
                                                        <td><%# Eval("TotalLtr_MilkQty")%></td>
                                                        <td><%# Eval("Avg_Fat")%></td>
                                                        <td><%# Eval("Avg_SNF")%></td>
                                                        <td><%# Eval("Avg_CLR")%></td>
                                                        <td><%# Eval("MilkValue")%></td>
                                                        <td><%# Eval("EarningValue")%></td>
                                                        <td><%# Eval("SaleValue")%></td>
                                                        <td><%# Eval("AdjustAmount")%></td>
                                                        <td><%# Eval("PayableAmount")%></td>
                                                        <td></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>



                                            <tr>
                                                <td colspan="5"><b>कैश अनुमान टोटल</b></td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text=""></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <%-- <asp:GridView ID="gvdetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="true"></asp:GridView>--%>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

