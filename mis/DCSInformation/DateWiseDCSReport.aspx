<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DateWiseDCSReport.aspx.cs" Inherits="mis_DCSInformation_DateWiseDCSReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content" style="min-height: 750px;">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Union Wise DCS Report</h3>
                    </div>
                    <div class="box-body">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnSearch" Style="margin-top: 20px;" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <%--<table class="table table-bordered">
                                            <tr>
                                                <th>S.NO</th>
                                                <th>Name of Milk Union</th>
                                                <th>Name of District Covered</th>
                                                <th>No of memebers already having KCC</th>
                                                <th>No of Blank form received by DCS</th>
                                                <th>No of form Fillup by Male's</th>
                                                <th>No of form Fillup by Female's</th>
                                                <th>Total No of form Fillup by Member's</th>
                                                <th>No of Male's form certified by DCS</th>
                                                <th>No of Female's form certified by DCS</th>
                                                <th>Total No of form certified by DCS</th>
                                                <th>No of Male's form Submitted to Bank</th>
                                                <th>No of Female's form Submitted to Bank</th>
                                                <th>Total No of form Submitted to Bank</th>
                                                <th>No of Male's Apply for New Card</th>
                                                <th>No of Female's Apply for New Card</th>
                                                <th>Total Member's Apply for New Card</th>
                                                <th>No of card for limit enhancement</th>
                                                <th>No of Acknowledgement received from Banks</th>
                                                <th>Total Cards Issued/Limit Extended</th>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td rowspan="11" style="padding-top: 150px;">Bhopal Milk Union</td>
                                                <td>Bhopal</td>
                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Sehore</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>Shajapur</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>Rajgarh</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td>Betul</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td>Hoshangabad</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>7</td>
                                                <td>Harda</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>8</td>
                                                <td>Vidisha</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>9</td>
                                                <td>Raisen</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>10</td>
                                                <td>Guna</td>

                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td>11</td>
                                                <td>Ashok Nagar</td>
                                                <td>100</td>
                                                <td>50</td>
                                                <td>20</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>60</td>
                                                <td>40</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>100</td>
                                                <td>200</td>
                                                <td>30</td>
                                                <td>50</td>
                                                <td>80</td>
                                                <td>50</td>
                                                <td>50</td>
                                                <td>80</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td><b>Sub Total</b></td>
                                                <td></td>
                                                <td><b>1100</b></td>
                                                <td><b>550</b></td>
                                                <td><b>220</b></td>
                                                <td><b>330</b></td>
                                                <td><b>550</b></td>
                                                <td><b>660</b></td>
                                                <td><b>440</b></td>
                                                <td><b>1100</b></td>
                                                <td><b>1100</b></td>
                                                <td><b>1100</b></td>
                                                <td><b>2200</b></td>
                                                <td><b>330</b></td>
                                                <td><b>550</b></td>
                                                <td><b>880</b></td>
                                                <td><b>550</b></td>
                                                <td><b>550</b></td>
                                                <td><b>880</b></td>
                                            </tr>
                                        </table>--%>
                                        <asp:GridView ID="gvDetails" runat="server" EmptyDataText="No Record Found" ShowHeaderWhenEmpty="true" CssClass="table table-bordered" AutoGenerateColumns="false" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Name of Milk Union" DataField="MilkUnion" ItemStyle-Width="30%" />
                                                <asp:BoundField HeaderText="Name of District Covered" DataField="District" />
                                                <asp:BoundField HeaderText="No of Male form's Submitted by DCS" DataField="NoofMalesformsSubmittedbyDCS" />
                                                <asp:BoundField HeaderText="No of Female form's Submitted by DCS" DataField="NoofFemalesformsSubmittedbyDCS" />
                                                <asp:BoundField HeaderText="Total form's Submitted by DCS" DataField="TotalNoofformsSubmittedbyDCS" />
                                                <asp:BoundField HeaderText="No of Male forms Submitted for increase in limit(Already having KCC)" DataField="OutofwhichNoofMalesformsSubmittedforincreaseinlimit" />
                                                <asp:BoundField HeaderText="No of Female forms Submitted for increase in limit(Already having KCC)" DataField="OutofwhichNoofFemalesformsSubmittedforincreaseinlimit" />
                                                <asp:BoundField HeaderText="Total forms Submitted for increase in limit(Already having KCC)" DataField="OutofwhichTotalNoofformsSubmittedforincreaseinlimit" />
                                                <asp:BoundField HeaderText="No of Male Application for New KCC having Land" DataField="MaleApplicationforNewKCChavingLand" />
                                                <asp:BoundField HeaderText="No of Female Application for New KCC having Land" DataField="FemaleApplicationforNewKCChavingLand" />
                                                <asp:BoundField HeaderText="Total  Application for New KCC having Land" DataField="TotalApplicationforNewKCChavingLand" />
                                                <asp:BoundField HeaderText="No of Male Application for New KCC having no Land(पूर्व KCC ना हो)" DataField="MaleApplicationforNewKCChavingnoLand" />
                                                <asp:BoundField HeaderText="No of Female Application for New KCC having no Land(पूर्व KCC ना हो)" DataField="FemaleApplicationforNewKCChavingnoLand" />
                                                <asp:BoundField HeaderText="Total  Application for New KCC having no Land(पूर्व KCC ना हो)" DataField="TotalApplicationforNewKCChavingnoLand" />
                                                <asp:BoundField HeaderText="Any Other's(Male)" DataField="AnyOthersByMale" />
                                                <asp:BoundField HeaderText="Any Other's(Female)" DataField="AnyOthersByFemale" />
                                                <asp:BoundField HeaderText="Any Other's(Total)" DataField="AnyOthersByTotal" />
                                                <asp:BoundField HeaderText="No of Male form's Submitted by Union to the Bank" DataField="NoofMaleformsSubmittedbyUniontotheBank" />
                                                <asp:BoundField HeaderText="No of Female form's Submitted by Union to the Bank" DataField="NoofFemaleformsSubmittedbyUniontotheBank" />
                                                <asp:BoundField HeaderText="Total form's Submitted by Union to the Bank" DataField="TotalNoofformsSubmittedbyUniontotheBank" />
                                                <asp:BoundField HeaderText="No of Card Issued for Male" DataField="NoofCardIssuedByMale" />
                                                <asp:BoundField HeaderText="No of Card Issued for Female" DataField="NoofCardIssuedByFemale" />
                                                <asp:BoundField HeaderText="Total No of Card Issued" DataField="TotalNoofCardIssued" />
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
    <script>
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }

            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            var Fromday = 0;
            var FromMonth = 0;
            var FromYear = 0;
            var Today = 0;
            var ToMonth = 0;
            var ToYear = 0;
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];

                Fromday = dateParts[0];
                FromMonth = dateParts[1];
                FromYear = dateParts[2];

                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];

                Today = dateParts[0];
                ToMonth = dateParts[1];
                ToYear = dateParts[2];

                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        //if (FromYear == ToYear && ToMonth <= 12 && ToMonth > 3 && FromMonth >= 4) {
                        //}
                        //if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        //}
                        //else if (FromYear < ToYear && ToMonth <= 3 && FromMonth >= 4) {
                        //}
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear != ToYear && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                        }
                    }
                    else {
                        msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                    }

                }
            }
           <%-- if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }
    </script>
</asp:Content>

