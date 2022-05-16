<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CFP_Rpt_PriceDifferenct.aspx.cs" Inherits="mis_CattleFeed_CFP_Rpt_PriceDifferenct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
        <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Price Difference Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                        <div class="box-body">
                            <fieldset>
                                <legend>
                                    Price Difference Report
                                </legend>
                             <div class="col-md-12">
                                    <asp:Label ID="Label1" CssClass="Autoclr" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtFromDate" ErrorMessage="Please Enter From Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" ErrorMessage="Invalid From Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>To Date  </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtToDate" ErrorMessage="Please Enter Date." Text="<i class='fa fa-exclamation-circle' title='Please Enter Date !'></i>"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtToDate" ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" onkeypress="javascript: return false;" Width="100%" onpaste="return false;" placeholder="Select Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                             <label>Party Name  </label>
                                            <asp:DropDownList ID="ddlParty" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                  <asp:ListItem Value="1" Text="PARAS AGRO 7 ASSOCIATES"></asp:ListItem>
                                                  <asp:ListItem Value="2" Text="Test 2"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                         </div>
                                     <div class="col-md-3" style="text-align: center;margin-top:23px;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" CausesValidation="true" ValidationGroup="a" />
                                    &nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" CausesValidation="false" />
                                </div>

                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="lblName" runat="server">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;M/S. PARAS AGRO 7 ASSOCIATES, INDORE</asp:Label> 
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;GURANTEE: AS PER REGISTRATION DOCUMENTS
                                <div class="col-md-12">
                                        <div class="form-group">
                                       &nbsp; DETAILS OF MATERIAL SUPPLIED BY YOU AGAINST ABOVE PURCHASE ORDER NO
                                            </div>
                                   
                                    <div class="table-responsive">
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <th>Bill No.</th>
                                            <th>B.Date</th>
                                               <th>BILL AMT.</th>
                                              <th>REC DT.</th>
                                              <th>GR</th>
                                              <th>NC CODE</th>
                                              <th>DISTP. WT</th>
                                              <th>RECPT WT</th>
                                              <th>SHORT MT</th>
                                              <th>OF REC QTYIEGI</th>
                                              <th>ADVANCE </th>
                                            <th>MOIS </th>
                                              <th>Protn. </th>
                                              <th>Fat </th>
                                            <th>Fib </th>
                                             <th>Slice </th>
                                            <th>REBATE % </th>
                                            <th>REBATE AMT </th>
                                            <th>REMIU % </th>
                                            <th>PREMIUM AMT </th>
                                             <th>TORN BAG </th>
                                             <th>DED.TORN BAG </th>
                                            <th>FINAL AMT </th>
                                        </tr>
                                        <tr>
                                            <td>46</td>
                                             <td>30.7.21</td>
                                             <td>45654.66</td>
                                             <td>30.7.21</td>
                                             <td>549</td>
                                             <td>307</td>
                                             <td>4.980</td>
                                             <td>4.380</td>
                                             <td>0.000</td>
                                             <td>4096.22</td>
                                            <td></td>
                                             <td>7.65</td>
                                             <td>2.33</td>
                                             <td>1.44</td>
                                             <td>1.24</td>
                                             <td>0.86</td>
                                            <td></td>
                                             <td>2661.73</td>
                                             <td></td>
                                            <td>0.00</td>
                                             <td>25</td>
                                             <td>310.00</td>
                                             <td>306532.27</td>
                                        </tr>
                                        <tr>
                                            <td>47</td>
                                             <td>30.7.21</td>
                                             <td>65654.66</td>
                                             <td>30.7.21</td>
                                             <td>549</td>
                                             <td>307</td>
                                             <td>3.980</td>
                                             <td>3.380</td>
                                             <td>0.000</td>
                                             <td>4096.22</td>
                                            <td></td>
                                             <td>7.65</td>
                                             <td>2.33</td>
                                             <td>1.44</td>
                                             <td>1.24</td>
                                             <td>0.86</td>
                                            <td></td>
                                             <td>1661.73</td>
                                             <td></td>
                                            <td>0.00</td>
                                             <td>24</td>
                                             <td>310.00</td>
                                             <td>310532.27</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"><b>Total</b></td>
                                            <td>115446.00</td>
                                             <td></td>
                                             <td></td>
                                             <td></td>
                                             <td>8.216</td>
                                             <td>7.460</td>
                                             <td></td>
                                             <td>80188.44</td>
                                             <td></td>
                                             <td></td>
                                             <td></td>
                                             <td></td>
                                             <td></td>
                                             <td></td>
                                             <td></td>
                                             <td>4323.46</td>
                                             <td></td>
                                             <td></td>
                                             <td>49</td>
                                             <td>620</td>
                                             <td>617064.54</td>
                                        </tr>
                                    </table>
                                        </div>
                                    </div>
                            </fieldset>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

