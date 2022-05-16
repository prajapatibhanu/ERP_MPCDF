<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Misformatreport.aspx.cs" Inherits="mis_Mis_Reports_Misformatreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <%--  <div class="row">--%>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Monthly Costing Entry</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Month:</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="0">Select</asp:ListItem>
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
                        </div>
                    </div>
                    <div class="row"></div>
                    <div id="FOM" runat="server" class="box-body">
                        <fieldset>
                            <legend>Farmer's Organisation And MemberShip</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Farmer's Organisation</legend>
                                        <table style="width: 100%" class="table table-bordered">
                                            <tr>
                                                <td>NO. Of Functional Routes:</td>
                                                <td>DCS-Organised:</td>
                                                <td>DCS-Functional:</td>
                                                <td>DCS-Closed Temp.</td>
                                            </tr>
                                            <tr id="Fom1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtfunctionalRoutes" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdcsorg" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdcsfunctional" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtclosedtemp" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Total Membership:(Organised DCS)</legend>
                                        <table style="width: 100%" class="table table-bordered">
                                            <tr>
                                                <td>General:</td>
                                                <td>Schedule Caste:</td>
                                                <td>Schedule Tribe:</td>
                                                <td>Other Backword Classes:</td>
                                                <td>Total</td>
                                            </tr>
                                            <tr id="TotalMembership1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtgeneral1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtschedCaste1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtschedtribe1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtotherbackw1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txttotal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Total Membership:(Organised DCS)</legend>
                                        <table style="width: 100%" class="table table-bordered">
                                            <tr>
                                                <td>Landless Laboures</td>
                                                <td>Marginal Farmers</td>
                                                <td>Small Farmers</td>
                                                <td>Large Farmers</td>
                                                <td>Others</td>
                                                <td>Total</td>
                                            </tr>
                                            <tr id="totalmembershipDCs1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtLanlessLboure1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtmarginlfarmrs1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtsmallfarmrs1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlargfarmrs1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtothers1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txttotl1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Female Membership:(Organised DCS)</legend>
                                        <table runat="server" style="width: 100%" class="table table-bordered">
                                            <tr>
                                                <td>General</td>
                                                <td>Schedule Cast</td>
                                                <td>Schedule Tribe</td>
                                                <td>Other Backword</td>
                                                <td>Total</td>
                                            </tr>
                                            <tr id="femalemembership1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtgen1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtschedcast1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtschetrib1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtotherbckw1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtttl1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Milk Procurement Cost</legend>
                                    <table style="width: 100%" class="table table-bordered">
                                        <tr>
                                            <td>Salary And Wages</td>
                                            <td>TA & Transportation</td>
                                            <td>MISL</td>
                                            <td>Total Cost</td>
                                        </tr>
                                        <tr id="milkprocurment1" runat="server">
                                            <td>
                                                <asp:TextBox ID="txtSalaryWags1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTAtransport1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmisl1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txttotlcost1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>MEMBERSHIP/POURERS</legend>
                                    <table style="width: 100%" class="table table-bordered">
                                        <tr>
                                            <td>Membership Func. of DCS</td>
                                            <td>Milk Pourers Members</td>
                                            <td>Milk Pourers Non Members</td>
                                            <td>Milk Pourers Total</td>
                                        </tr>
                                        <tr id="MembershipPoures1" runat="server">
                                            <td>
                                                <asp:TextBox ID="txtmemfuncdcs1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmilkpursmember1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmilkpoursnonmember1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="milkpourstotal1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>DUES POSITION</legend>
                                    <table style="width: 100%" class="table table-bordered">
                                        <tr>
                                            <td>Dues position Old</td>
                                            <td>Dues position Current</td>
                                            <td>Dues position Total</td>
                                        </tr>
                                        <tr id="duesposition1" runat="server">
                                            <td>
                                                <asp:TextBox ID="txtduesposold1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtduesposcurrent1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTotaldues1" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </fieldset>
                    </div>
                    <div id="MPE" runat="server" class="box-body">
                        <fieldset>
                            <legend>MILK PROCUREMENT ENHANCEMENT</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>No. Of AI Center(AI ACTIVITIES)</legend>
                                        <table class="table table-bordered" style="width: 100%">
                                            <tr>
                                                <td>No.Of Ai Center single</td>
                                                <td>No.Of Ai Center Cluster</td>
                                                <td>No.Of Ai Center Total</td>
                                                <td>No. Of Ai perfomed Single(Cow)</td>
                                                <td>No. Of Ai perfomed Buff</td>
                                                <td>No. Of Ai perfomed Cluster(Cow)</td>
                                                <td>No. Of Ai perfomed Buff</td>
                                                <td>No. Of Ai perfomed Total(Cow)</td>
                                                <td>No. Of Ai perfomed Total(Buff)</td>
                                            </tr>
                                            <tr id="NoOfAiCenter1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtaicentersingle1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtaicenterCluster1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtaicentertotal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtaisingleCow1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtsinglebuff1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtclusterCow1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtclusterbuff1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txttotalcow1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txttotalbuff1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div class="col-md-8">
                                    <fieldset>
                                        <legend>Total AI Performed(NOs)</legend>
                                        <table style="width: 100%" runat="server" class="table table-bordered">
                                            <tr>
                                                <td>Total AI Performed (NOs)</td>
                                                <td>Calves reported Born Total Cow</td>
                                                <td>Calves reported Born Buff</td>
                                                <td>Animal Husbandry First Aid Cases(NOs)</td>
                                                <td>Animal Husbandry AHW Cases(NOs)</td>
                                                <td>Cattle Field Sold By DCS(mts)</td>
                                                <td>No.Of DSC Selling BCF</td>
                                            </tr>
                                            <tr id="totalAiperformed1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txttotlaiperformed1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcalvreportborncow1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcalvsreportbornbuff1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAHfirstaidcase1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAHWcasenos1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcattlfildAHWnos1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtsellingbcf1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>AI ACTIVITIES COST(Rs.)</legend>
                                        <table class="table table-bordered" runat="server" style="width: 100%">
                                            <tr>
                                                <td>Salary And Wages</td>
                                                <td>Other Direct Cost</td>
                                                <td>Total Cost</td>
                                            </tr>
                                            <tr id="aiactivitiescostrs1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="salarywages1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="otherdirectcost1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="totalcost1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>AHC COST(Rs.)</legend>
                                        <table class="table table-bordered" style="width: 100%">
                                            <tr>
                                                <td>Salary And Wages</td>
                                                <td>Other DIrect Cost</td>
                                                <td>Total Cost</td>

                                            </tr>
                                            <tr id="ahccost1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtahcsalaryages1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtahcOtherdireccost1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtahccostotalcost1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>OTHER INPUT COST(Rs.)</legend>
                                        <table style="width: 100%" class="table table-bordered">
                                            <tr>
                                                <td>Salary And Wages</td>
                                                <td>Other Input Cost</td>
                                                <td>Total Cost</td>
                                            </tr>
                                            <tr id="otherinputcost1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtotherinputsalarywages1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtotherinputotherinpucost1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="otherinputttalcost1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                    <fieldset>
                                        <legend>CATTLE INDUCTION</legend>
                                        <table style="width: 100%" class="table table-bordered">
                                            <tr>
                                                <td>Project</td>
                                                <td>Self Finance</td>
                                                <td>Total</td>
                                            </tr>
                                            <tr id="cattleinduction1" runat="server">
                                                <td>
                                                    <asp:TextBox ID="txtcattleinducproject1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcattleinducselffinance1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcattleinductotal1" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                   <div class="row">
                        <div class="col-md-2 pull-right">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" Style="margin-top: 20px;" CssClass="btn btn-primary btn-block" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

