<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MPRU_MonthlyEntryFrom.aspx.cs" Inherits="mis_Mis_Reports_MPRU_MonthlyEntryFrom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        td {
            min-width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">MPRU MONTHLY ENTRY</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>MPRU REPORT</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Year <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlform_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Month <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlform_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Entry Form <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlform" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlform_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Farmer Organisation</asp:ListItem>
                                                <asp:ListItem Value="2">Milk Procurement and Sale</asp:ListItem>
                                                <asp:ListItem Value="3">Products Manufacture and Sale</asp:ListItem>
                                                <asp:ListItem Value="4">Recombination</asp:ListItem>
                                                <asp:ListItem Value="5">Processing and Product Making</asp:ListItem>
                                                <asp:ListItem Value="6">Packaging and CC</asp:ListItem>
                                                <asp:ListItem Value="7">Marketing</asp:ListItem>
                                                <asp:ListItem Value="8">Administration</asp:ListItem>
                                                <asp:ListItem Value="9">Receipts</asp:ListItem>
                                                <asp:ListItem Value="10">Capacity Utilisation</asp:ListItem>
                                                <asp:ListItem Value="11">Material Balancing</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div id="FO" runat="server">
                                    <fieldset>
                                        <legend>Farmer's Organisation</legend>
                                        <fieldset>
                                            <legend>1.0 FARMERS ORGANISATION AND MEMBERSHIP :</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="8">FARMERS' ORGANISATION :</th>
                                                        </tr>
                                                        <tr>
                                                            <th>No. of Functional Routes</th>
                                                            <th>DCS-Organised</th>
                                                            <th>DCS-Functional</th>
                                                            <th>DCS-Closed Temp.</th>
                                                            <th>New DCS-Organised During the Month</th>
                                                            <th>New DCS-Registered During the Month</th>
                                                            <th>No. Of DCS-Revived During the Month.</th>
                                                            <th>No. Of DCS-Closed During the Month.</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNOofroutes" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOsubFOM()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsorgnised" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOsubFOM()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsfunctional" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsclosedtemp" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtnewdcsorganisedmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtnewdcsregisteredmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsrevivedmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcolesdmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>1.1 Farmer Membership(Organised-DCS)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="11">TOTAL MEMBERSHIP :(Organised DCS)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>General</th>
                                                            <th>Schedule-Caste</th>
                                                            <th>Scheduled-Tribe</th>
                                                            <th>Other Backword-Classes</th>
                                                            <th>Total</th>
                                                            <th>Landless Laboures</th>
                                                            <th>Margional-Farmers</th>
                                                            <th>Samll-Farmers</th>
                                                            <th>Large-Farmers</th>
                                                            <th>Others</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtGeneral" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtSceduledcaste" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtscheduletribe" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbackworsclasses" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmembershiptotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlandlesslabour" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmarginalfarmer" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsmallfarmer" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlargefarmer" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtothers" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="5">FEMALE MEMBERSHIP :(Organised DCS)</th>
                                                            <th colspan="3">DUES POSITION</th>
                                                        </tr>
                                                        <tr>
                                                            <th>General</th>
                                                            <th>Schedule-Caste</th>
                                                            <th>Schedule-Tribe</th>
                                                            <th>Other-Backword</th>
                                                            <th>Total</th>
                                                            <th>OLD</th>
                                                            <th>Current</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemalGeneral" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtScheCastfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtschedtribfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherbackwordfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtoldDues" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcurrentDues" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttoalDues" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>1.2 Farmer Members(Functional-DCS)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="11">TOTAL MEMBERSHIP :(Functional DCS)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>General</th>
                                                            <th>Schedule-Caste</th>
                                                            <th>Scheduled-Tribe</th>
                                                            <th>Other Backword-Classes</th>
                                                            <th>Total</th>
                                                            <th>Landless Laboures</th>
                                                            <th>Margional-Farmers</th>
                                                            <th>Samll-Farmers</th>
                                                            <th>Large-Farmers</th>
                                                            <th>Others</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtgenralFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtschedulcasteFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtscheduletribeFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbackwordFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlandlesslaboueFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmarinalfarmerFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsmallfarmerFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlargefarmerFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthersFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFunTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table style="width: 100%" class="table table-bordered">
                                                        <tr>
                                                            <th colspan="5">Female Membership(Functional-DCS)</th>
                                                            <th colspan="3">MEMBERSHIP v/s POURERS :</th>
                                                        </tr>

                                                        <tr>
                                                            <th rowspan="2">General</th>
                                                            <th rowspan="2">Schedule-Caste</th>
                                                            <th rowspan="2">Schedule-Tribe</th>
                                                            <th rowspan="2">Other Backword-Class</th>
                                                            <th rowspan="2">Total</th>
                                                            <th colspan="5">MILK POURERS</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Members</th>
                                                            <th>Non-Members</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemalegeneral" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemaleschedulcaste" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemaletribe" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemalebackword" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemaletotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmembers" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtnonmerbers" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalPourers" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>1.3 ARTIFICIAL INSEMINATION</legend>
                                            <div class="col-md-12">
                                                <fieldset>
                                                    <legend>AI ACTIVITIES</legend>
                                                    <div class="table table-responsive">
                                                        <table style="width: 100%" class="table table-bordered">
                                                            <tr>
                                                                <th colspan="9">AI ACTIVITIES :</th>
                                                                <th rowspan="3">Total AI's Performed(NOs)</th>
                                                            </tr>
                                                            <tr>
                                                                <th colspan="2">NO OF AI CENTRE:</th>
                                                                <th rowspan="2">Total</th>
                                                                <th colspan="6">NO OF AI PERFORMED :</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Single</th>
                                                                <th>Cluster</th>
                                                                <th>Single Cow</th>
                                                                <th>Buff</th>
                                                                <th>Cluster Cow</th>
                                                                <th>Buff</th>
                                                                <th>Total Cow</th>
                                                                <th>Buff</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIcentersingle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIcentercluster" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIcentertotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIperformSinglecow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIperformBuff1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIperformclustercow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIPerformBuff2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAItotalCow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAItotalBuff" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAiperformedtotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="2">CALVES REPORTED BORN </th>
                                                            <th colspan="2">ANIMAL HUSBANDRY </th>
                                                            <th rowspan="2">Cattle Field Sold By(DCS)</th>
                                                            <th rowspan="2">No. of DCS selling DCS BCF</th>
                                                            <th rowspan="2">MM Sale By DCS to Produsers</th>
                                                            <th colspan="3">Cattle Induction</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Total Cow</th>
                                                            <th>Buff</th>
                                                            <th>First Aid Cases(NOs)</th>
                                                            <th>AHW Cases(NOs)</th>
                                                            <th>Project</th>
                                                            <th>Self-Finance</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcalvborntotalcow" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcalvbornbuff" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtanimalhusfirstAid" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtaniamlhusAHWcase" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattlefiedsold" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsselingbcf" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmmsalebydcs" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattinducproject" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattinducselffinance" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattleinductotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>1.4 PROCUREMENT ACTIVITIES COST</legend>
                                            <div class="table table-responsive">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th colspan="5">PROCUREMENT ACTIVITIES COST : (Rs)</th>
                                                        <th colspan="8">AI ACTIVITIES COST : (Rs)</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Salary and Wages</th>
                                                        <th>Ta & Transportation</th>
                                                        <th>Contract Laboure</th>
                                                        <th>Other Expanses</th>
                                                        <th>Total</th>
                                                        <th>Salary and Wages</th>
                                                        <th>Transportation</th>
                                                        <th>Cost Of LN2 Consumed</th>
                                                        <th>Cost Of LN2 Transportation</th>
                                                        <th>Cost Of Semen & Straws </th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Less Income</th>
                                                        <th>Total Cost</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalarywagesPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTaandtransportPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcontractlabourPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherexpansesPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryandwagesAiActivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttransportAiActivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLn2ConsumedAiAcitivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLn2transportAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsemenandstrawesAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherdirectcostAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlessincomeAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalcostAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th colspan="3">AHC Cost:(Rs)</th>
                                                        <th colspan="3">FODDER PROPOGATION COST : (Rs.)</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Salary and Wages</th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Total Cost</th>
                                                        <th>Salary and Wages</th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Total Cost</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAHCsalaryandwages" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAHCotherdirectcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAHCtotalCost" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPCsalryandwages" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPCotherdirectcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPCtotalcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th colspan="3">TRAINING & EXTANTION COST : (Rs.)</th>
                                                        <th colspan="4">OTHER INPUT COST : (Rs.)</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Salary and Wages</th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Less Income</th>
                                                        <th>Total Cost</th>
                                                        <th>Salary and Wages</th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Total Cost</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTEcostsalaryandwages" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTEcostotherdirectcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTEcostlessincome" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalCost" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryandwagesOTI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherincmecostOTI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalcostOTI" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-3 pull-right">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><b>Grand Total</b></label>
                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFOGrandTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnFO" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnFO_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="MOP" runat="server">
                                    <fieldset>
                                        <legend>MILK PROCUREMENT AND SALE</legend>
                                        <fieldset>
                                            <legend>2.0 MILK PRODUCTION AND SALE :</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="6" style="font-weight: bold">MILK PROCUREMENT : (In Kgs)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>DCS Milk(RMRD)</th>
                                                            <th>DCS Milk(CCs)</th>
                                                            <th>SMG Milk</th>
                                                            <th>NMG Milk</th>
                                                            <th>Other</th>
                                                            <th>Total Milk Proc.</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDCSmilkRMRD" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDCSmilkCCS" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGMILK" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGmilk" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOTHER" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalMilkProc" onpaste="return false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="12">LOCAL MILK SALE (Within Milk Shed Area): In Litres :</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Whole Milk</th>
                                                            <th>Full Cream Milk</th>
                                                            <th>STD Milk</th>
                                                            <th>Toned Milk</th>
                                                            <th>DT Milk</th>
                                                            <th>Skim Milk</th>
                                                            <th>Raw Chilld Milk</th>
                                                            <th>Chai Special Milk</th>
                                                            <th>Cow Milk</th>
                                                            <th>Sanchi Lite Milk</th>
                                                            <th>Chaha Milk</th>
                                                            <th>Total Milk Sale</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtfullcreammilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtstdmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttonedmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdtmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtrawchilldmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtchaispecimilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcowmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsanchilitemilk" onpaste="return false" ClientIDMode="Static" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtchahamilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalmilksale" onpaste="return false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="4">SMG MILK SALE :(In Litres)</th>
                                                            <th colspan="4">NMG MILK SALE : (In Litres)</th>
                                                            <th colspan="4">OTHER SALE (in Litres)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Whole Milk</th>
                                                            <th>Skim Milk</th>
                                                            <th>Other</th>
                                                            <th>Total SMG Sale</th>
                                                            <th>Whole Milk</th>
                                                            <th>Skim Milk</th>
                                                            <th>Other</th>
                                                            <th>Total NMG Sale</th>
                                                            <th>Whole Milk In Lit.</th>
                                                            <th>Skim Milk In Lit.</th>
                                                            <th>Other</th>
                                                            <th>Total BULK Sale</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk_SMG" onpaste="return false" ClientIDMode="Static" onkeyup="SMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_SMG" onpaste="return false" ClientIDMode="Static" onkeyup="SMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_SMG" onpaste="return false" ClientIDMode="Static" onkeyup="SMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalsmgsale_SMG" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk_NMG" onpaste="return false" ClientIDMode="Static" onkeyup="NMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_NMG" onpaste="return false" ClientIDMode="Static" onkeyup="NMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_NMG" onpaste="return false" ClientIDMode="Static" onkeyup="NMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalNMGsale_NMG" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholmilkinLit_OSALE" onpaste="return false" ClientIDMode="Static" onkeyup="OScalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilkinLit_OSALE" onpaste="return false" ClientIDMode="Static" onkeyup="OScalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_OSALE" onpaste="return false" ClientIDMode="Static" onkeyup="OScalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalBulkSale_OSALE" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="2">MILK PROCUREMENT(KGPD)</th>
                                                            <th colspan="2">LOCAL MILK SALE(LPD)</th>
                                                            <th colspan="2">TOTAL MILK SALE (LPD)</th>
                                                            <th colspan="2">SMG MILK SALE (LPD)</th>
                                                            <th colspan="2">NMG+OTH SALE (LPD)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>

                                                        </tr>
                                                        <tr>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKPROC_monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKPROC_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILK_MOnthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMilk_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalMilkSale_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalMilkSale_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGmilk_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGmilk_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGOTH_MOnthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGOTH_Cummulat" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend>2.2 GOAT MILK PROCUREMENT & MILK SALE :</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="2">MILK PROCUREMENT(KGPD)</th>
                                                            <th colspan="2">LOCAL MILK SALE(LPD)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                        </tr>
                                                        <tr>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKproKGPD_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKprocKGPD_Cummulative" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILKLPD_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalmilkLPD_Cummulative" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnMOP" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnMOP_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="PMandSALE" runat="server">
                                    <fieldset>
                                        <legend>PRODUCTS MANUFACTURING AND SALE</legend>
                                        <fieldset>
                                            <legend>3.0 PRODUCTS MANUFACTURED  AND SALE :</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="5">PRODUCTS MANUFACTURED : In Kgs</th>
                                                            <th rowspan="3">PANEER</th>
                                                            <th colspan="8">PRODUCTS SALE (Main and Indigenous ): In KGS/Litres :</th>
                                                        </tr>
                                                        <tr>
                                                            <th rowspan="2">GHEE</th>
                                                            <th rowspan="2">SKIM MILK POWDER</th>
                                                            <th rowspan="2">TABLE BUTTER</th>
                                                            <th rowspan="2">WHITE BUTTER</th>
                                                            <th rowspan="2">WMP</th>

                                                            <th colspan="4" style="font-weight: bold">GHEE SALE IN KGs:</th>
                                                            <th rowspan="2">SKIM MILK POWDER</th>
                                                            <th rowspan="2">TABLE BUTTER</th>
                                                            <th rowspan="2">WHITE BUTTER</th>
                                                            <th rowspan="2">SHRI-KHAND</th>
                                                        </tr>
                                                        <tr>
                                                            <th>SANCHI(Kg)</th>
                                                            <th>SNEHA</th>
                                                            <th>OTHER</th>
                                                            <th>TOTAL SALE</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtGhee" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSkimmilkpowder" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttablebutter" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwhitebutter" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpaneer" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSanchi_GHEESale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSneha_GHEESale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_GHEESale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal_GHEESale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_Prodctsale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttablebutter_Prodctsale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwhitebutter_Prodctsale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtshrikhand_Prodctsale" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>PANEER</th>
                                                            <th>FLAV. MILK IN LIt.</th>
                                                            <th>BTR. MILK IN LIt.</th>
                                                            <th>SWEET CURD</th>
                                                            <th>PEDA</th>
                                                            <th>PLAIN CURD</th>
                                                            <th>ORANGE SIP</th>
                                                            <th>PRO-BIOTIC CURD</th>
                                                            <th>WHOLE MILK</th>
                                                            <th>CHENAN RABDI</th>
                                                            <th>PRESS CURD</th>
                                                            <th>CREAM</th>
                                                            <th>LASSI</th>
                                                            <th>AMRAKHAND</th>
                                                            <th>SMP(CSP)</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtPAneer_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflavmilk_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBtrmilk_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSweetcurd_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpeda_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtplaincurd_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtorangsip_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtprobioticcurd_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChenarabdi_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpresscurd_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcream_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtlassi_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtamarkhand_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsmp_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>MAWA</th>
                                                            <th>DRY CASEIN</th>
                                                            <th>COOKING BUTTER</th>
                                                            <th>Gulab Jamun</th>
                                                            <th>Rash Gulla</th>
                                                            <th>MAWA GULAB JAMUN</th>
                                                            <th>MILK CAKE SANCHI</th>
                                                            <th>Thandai</th>
                                                            <th>MDM(Sweeten SMP)</th>
                                                            <th>Light Lassi</th>
                                                            <th>Pudina Raita</th>
                                                            <th>WMP</th>
                                                            <th>Paneer Achar</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtmawa_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdrycasein_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcookingbutter_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgulabjamun_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtrasgulla_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmawagulabjanum_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmilkcake_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtThandai_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMDm_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtlightlassi_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpudinaraita_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpannerachar_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>Sanchi Lite Milk</th>
                                                            <th>Nariyal Barfi</th>
                                                            <th>Gulab Jamun Mix</th>
                                                            <th>Coffee Mix</th>
                                                            <th>Cooking Butter</th>
                                                            <th>Low Fat Paneer</th>
                                                            <th>Whey Drink</th>
                                                            <th>Sanchi Tea Mix</th>
                                                            <th>Peda Prasadi</th>
                                                            <th>Ice-Cream</th>
                                                            <th>Golden Milk</th>
                                                            <th>Sugar Free Peda</th>
                                                            <th>Health Vita Plus</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtsanchilitemilk_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtnariyalbarfi_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgulabjamunMix_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcoffemix_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcookingbutter" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtlowfatpanner_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwheydrink_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsanchitea_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpedaprasadi_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txticecream_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgoldenmilk_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsugarfreepeda_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txthealthvita_PMAS" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnPMandSale" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnPMandSale_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="recombination" runat="server">
                                    <fieldset>
                                        <legend>RECOMBINATION</legend>
                                        <fieldset>
                                            <legend>4.0 RECOMBINATION / RECONSTITUTION (DAIRY & CC / IMC) : </legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="6">FOR MILK: (Quantity)</th>
                                                            <th colspan="6">FOR PRODUCTS: (Quantity)</th>
                                                            <th colspan="2">COMM.USED</th>
                                                            <th rowspan="2">TOTAL RECOMBINATION/<br />
                                                                RECONSTITUTION</th>
                                                        </tr>
                                                        <tr>
                                                            <th>SMP</th>
                                                            <th>WB</th>
                                                            <th>GHEE</th>
                                                            <th>WMP</th>
                                                            <th>PANEER</th>
                                                            <th>TOTAL</th>
                                                            <th>SMP</th>
                                                            <th>WB</th>
                                                            <th>GHEE</th>
                                                            <th>WMP</th>
                                                            <th>PANEER</th>
                                                            <th>TOTAL</th>
                                                            <th>For Milk(RS:)</th>
                                                            <th>For Product(RS:)</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtSMP_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWB_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGHEE_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPANEER_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTOTAL_forMilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMP_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWB_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGHEE_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPANEER_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTOTAL_forproduct" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtformilk_Commused" onpaste="return false" ClientIDMode="Static" onkeyup="RCommcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtforproduct_Commused" onpaste="return false" ClientIDMode="Static" onkeyup="RCommcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRecombination" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnrecombination" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnrecombination_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="PPMaking" runat="server">
                                    <fieldset>
                                        <legend>PROCESSING AND PRODUCTS MAKING</legend>
                                        <fieldset>
                                            <legend>5.0 FINANCIAL PERFORMANCE : (Rs.)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table style="width: 100%" class="table table-bordered">
                                                        <tr>
                                                            <th colspan="11">PROCESSING/PRODUCT MAKING COST : DIRECT</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Salary and Allow</th>
                                                            <th>Cont. Laboures</th>
                                                            <th>Consumable Common</th>
                                                            <th>Consumable Direct</th>
                                                            <th>Chemical & Detergent</th>
                                                            <th>Electricity</th>
                                                            <th>Water</th>
                                                            <th>Furnance Oil/Gas</th>
                                                            <th>Repair And Maintenance</th>
                                                            <th>Other Exps.</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryandallow_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContlaboures_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtCosumbleCommon_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConsumbledirect_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtchemicaldetergent_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtElectricity_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtWater_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfurnanceoil_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtrepairmaintance_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOtherExps_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                        <fieldset>
                                            <legend>5.1 FINANCIAL PERFORMANCE : (Rs.)</legend>
                                            <div class="table-responsive">
                                                <table style="width: 100%" class="table table-bordered">
                                                    <tr>
                                                        <th colspan="11">PROCESSING/PRODUCT MAKING COST : ALLOCATED</th>
                                                        <th rowspan="2">Milk Processing Cost(Rs.)</th>
                                                        <th rowspan="2">Milk Prepack Cost(Rs)</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Salary and Allow</th>
                                                        <th>Cont. Laboures</th>
                                                        <th>Consumable Common</th>
                                                        <th>Consumable Direct</th>
                                                        <th>Chemical & Detergent</th>
                                                        <th>Electricity</th>
                                                        <th>Water</th>
                                                        <th>Furnance Oil/Gas</th>
                                                        <th>Repair And Maintenance</th>
                                                        <th>Other Exps.</th>
                                                        <th>Total</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryallow_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContlaboures_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConsumblecommon_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConsumbleDirect_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtchemicaldetergent_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtElectricity_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtWater_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFurnanceoil_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtRepairMaint_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOtherExps_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTOTal_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtMilkProssCA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtMilkPrepackCA" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-3 pull-right">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><b>Grand Total</b></label>
                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtPPMGrandTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnPPMaking" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnPPMaking_Click1"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="PackagingAndCC" runat="server">
                                    <fieldset>
                                        <legend>PACKAGING AND CC</legend>
                                        <fieldset>
                                            <legend>6.0 FINANCIAL PERFORMANCE : (Rs.)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="5">PACKING COST :</th>
                                                            <th colspan="11">CHILLING COST :</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Milk At CC</th>
                                                            <th>Milk At Dairy</th>
                                                            <th>Main Products</th>
                                                            <th>INDG Products</th>
                                                            <th>Total</th>
                                                            <th>Salary And Allow</th>
                                                            <th>Cont. Laboures</th>
                                                            <th>Electricity</th>
                                                            <th>Coal/FO & Diesel</th>
                                                            <th>Cosumable</th>
                                                            <th>Chemistry And Detergent</th>
                                                            <th>Repair And Maint.</th>
                                                            <th>BMC</th>
                                                            <th>Security</th>
                                                            <th>Other Exps.</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtmilCC_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMilkDairy_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmainproduct_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtINDGproduct_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal_PC" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsalaryallow_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtContlaboure_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtElectricity_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCoalDiesel_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtConsumble_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChemandDeter_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRepairMAint_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBMC_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSecurity_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOtherExps_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal_CC" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnPackagingAndCC" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnPackagingAndCC_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="Marketing" runat="server">
                                    <fieldset>
                                        <legend>MARKETTING</legend>
                                        <fieldset>
                                            <legend>7.0 FINANCIAL PERFORMANCE (conted): (Rs.)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="9">MARKETING COST :(Local Milk)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Salary And Allow</th>
                                                            <th>transportation</th>
                                                            <th>Sales Promotion</th>
                                                            <th>MPCDF Service-Charge</th>
                                                            <th>Advertisment & Ohter</th>
                                                            <th>Advance Card</th>
                                                            <th>Contract Labour</th>
                                                            <th>Others</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryAllow_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttranportation_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalesprom_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtservicecharg_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAdvertise_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAdvanceCard_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContractlabour_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthers_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                            <table style="width: 100%" class="table table-bordered">
                                                <tr>
                                                    <th colspan="5">MARKETING COST :(SMG/NMG Milk)</th>
                                                    <th colspan="5">MARKETING COST :(MAIN/INDG.PRODUCTS)</th>
                                                    <th rowspan="2">Total MKTG Cost</th>
                                                </tr>
                                                <tr>
                                                    <th>Salary And Allow</th>
                                                    <th>Transportation</th>
                                                    <th>Service-Charge</th>
                                                    <th>Ohters</th>
                                                    <th>Total</th>
                                                    <th>Salary And Allow</th>
                                                    <th>transportation</th>
                                                    <th>Service-Charge</th>
                                                    <th>Ohter Tax</th>
                                                    <th>Total</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalryAllow_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTransport_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtServicCharg_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthers_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalryAndAllow_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTransport_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtServicCharg_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthersTax_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalMKTG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnMarketing" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnMarketing_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="Administration" runat="server">
                                    <fieldset>
                                        <legend>ADMINISTRATION</legend>
                                        <fieldset>
                                            <legend>8.0 FINANCIAL PERFORMANCE (conted): (Rs.)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="11">ADMINISTRATION :</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Salary And Allow</th>
                                                            <th>Medical-TA</th>
                                                            <th>CONVEYANCE</th>
                                                            <th>SECURITY</th>
                                                            <th>SUPERVISION VEHICLES</th>
                                                            <th>CONTRACT LABOUR</th>
                                                            <th>INSURANCE & OTH. Taxes</th>
                                                            <th>LEGAL & AUDIT FEES</th>
                                                            <th>STATIONERY</th>
                                                            <th>OTHERS</th>
                                                            <th>TOTAL</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryAndAllow_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtMedicalTA_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConveyence_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtSecurity_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtSupervisionVehic_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContractLabour_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtInsuranceOTH_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLegalAuditFee_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtStationary_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOther_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="7">PROVISIONS :</th>
                                                            <th colspan="3">LOAN INTEREST :</th>
                                                            <th rowspan="2">DEPRECIATION</th>
                                                        </tr>
                                                        <tr>
                                                            <th>BONUS</th>
                                                            <th>AUDIT FEES</th>
                                                            <th>GROUP GRATUITY</th>
                                                            <th>LIVERIES</th>
                                                            <th>LEAVE SALARY OF RETIRED EMPLOYEES</th>
                                                            <th>OTHER EXPS.</th>
                                                            <th>TOTAL</th>
                                                            <th>NDDB</th>
                                                            <th>BANK LOAN</th>
                                                            <th>TOTAL</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtBonus_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAuditFees_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtGroupGratutiy_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLiveires_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLeavesalary_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherExps_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNDDB_LI" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtBANKLoan_LI" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_LI" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtDepreciation" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="4">PRODUCT MAKING CHARGES PAID :</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Product Making</th>
                                                            <th>Conversion Charge</th>
                                                            <th>Other</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtProductMaking" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConversionCharge" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPOther" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnAdministration" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnAdministration_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="Reciepts" runat="server">
                                    <fieldset>
                                        <legend>RECETPTS</legend>
                                        <fieldset>
                                            <legend>9.0 FINANCIAL PERFORMANCE (conted): (Rs.)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="3">TOTAL RECEIPTS :</th>
                                                            <th rowspan="2">COMMOD-ITIES USED</th>
                                                            <th rowspan="2">COMMOD-ITIES PURCH/RETURNS</th>
                                                            <th rowspan="2">OPENING STOCKS</th>
                                                            <th rowspan="2">CLOSING STOCKS</th>
                                                            <th rowspan="2">OTHER INCOME</th>
                                                            <th rowspan="2">TOTAL NET RECEIPTS</th>
                                                            <th colspan="4">SURPLUS/DEFICIT(-)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>MILK SALE</th>
                                                            <th>PRODUCTS</th>
                                                            <th>TOTAL SALE</th>
                                                            <th>BEFORE IDA/OFF</th>
                                                            <th>BEFORE DEFFERED</th>
                                                            <th>BEFORE DEPRECIATION</th>
                                                            <th>NET INCLU. DEP.</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmilksaleTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtProductsTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalSaleTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtCommoditiesTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcommditiesPurchTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtopeningStocksTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtClosingStocksTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOtherIncomeTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNetRecieptsTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbeforIDATR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbeforDEFERDTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbeforDEPRECIATIONTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNETINCLUDEPTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th rowspan="2">TOTAL VARRI. COST</th>
                                                            <th colspan="2">TOTAL FIXED COST</th>
                                                            <th rowspan="2">TOTAL FIXED COST</th>
                                                            <th rowspan="2">TOTAL COST</th>
                                                            <th rowspan="2">TOTAL FIXED COST EXCL.INTT./DEPR</th>
                                                            <th rowspan="2">TOTAL SALE</th>
                                                            <th rowspan="2">BEFORE IDA/OF Operating Profit</th>
                                                            <th rowspan="2">NET INCLUDING IDT</th>
                                                            <th rowspan="2">TOTAL SALE TURNOVER WITH CFF</th>
                                                            <th rowspan="2">TOTAL OPERATING LOSS/PROFIT WITH CFF</th>
                                                            <th rowspan="2">NET PROFIT/LOSS WITH CFF</th>
                                                        </tr>
                                                        <tr>
                                                            <th>SALARY&WAGES</th>
                                                            <th>OTHERS</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalvarriCostTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryWages_TFCOSTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthers_TFCOSTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalfixCostTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtToCostTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTFcostEXCLINTTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtToSaleTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtIDAOpertingProfTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNEtIncluIDTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtToSaleWithCFFTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOPLOssProfitCFFTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNETprofitLossTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnReceipt" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnReceipt_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="CapUtilisation" runat="server">
                                    <fieldset>
                                        <legend>CAPACITY UTILISATION</legend>
                                        <fieldset>
                                            <legend>10.0 CAPACITY UTILISATION IN %AGE</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="5">PROCESSING CAPACITY :-</th>
                                                            <th colspan="2">CHILLING CAPACITY:-</th>
                                                            <th colspan="3">BCF MANUFACTURING CAPACITY :-</th>
                                                            <th colspan="2">SMP MANUFACTURING :-</th>
                                                        </tr>
                                                        <tr>
                                                            <th>THROUGHPUT (IN KGs) WITHOUT WC</th>
                                                            <th>THROUGHPUT (IN KGs)</th>
                                                            <th>THROUGHPUT (IN LTs)</th>
                                                            <th>THROUGHPUT PER DAY</th>
                                                            <th>CAPACITY UTILSATION (IN %AGE)</th>
                                                            <th>ALL CCs THROUGHPUT</th>
                                                            <th>CAPACITY UTILSATION (IN %AGE)</th>
                                                            <th>BCF SALE CFF</th>
                                                            <th>BCF PROD. CFF</th>
                                                            <th>CAPACITY UTILSATION (IN %AGE)</th>
                                                            <th>SMP PRODN. IN MTs</th>
                                                            <th>CAPACITY UTILSATION (IN %AGE)</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtthruoputwithoutWC_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtthroughpuINKGS_PC" runat="server" onkeyup="CapacityUti()" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtthroughpuINLTS_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtthroughputPERDAY_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcapacityutilisationINKGS_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAllCCsthruoput_CC" onkeyup="CapUtiCC()" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcapacityuti_CC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtbcfsale_BMC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtbcfProdCFF_BMC" runat="server" onkeyup="CapacityUtiSMP()" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcapacityuti_BMC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMPProd_SMP" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcapacityuti_SMP" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnCapUtilisation" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnCapUtilisation_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="materialbalancing" runat="server">
                                    <fieldset>
                                        <legend>MATERIAL BALANCING</legend>
                                        <fieldset>
                                            <legend>11.0 MATERIAL BALANCING :</legend>
                                            <table style="width: 100%" class="table table-bordered">
                                                <tr>
                                                    <th colspan="7">MILK PROCUREMENT : (In Kgs)</th>
                                                    <th colspan="4">TOTAL INPUT :</th>
                                                </tr>
                                                <tr>
                                                    <th>DCS MILK COW</th>
                                                    <th>DCS MILK BUFF</th>
                                                    <th>DCS MILK TOTAL</th>
                                                    <th>FAT</th>
                                                    <th>SNF</th>
                                                    <th>FAT%</th>
                                                    <th>SNF%</th>
                                                    <th>QUANTITY IN KGs</th>
                                                    <th>FAT IN KGs</th>
                                                    <th>SNF IN KGs</th>
                                                    <th>VALUE IN Rs.</th>
                                                </tr>
                                                <tr>
                                                    <%--there is only one table in database for both table available here--%>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsmilkcow_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsmilkbuff_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsmilktotal_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfat_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnf_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatpercent_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfpercent_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtquantityinkgs_TI" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatinkgs_TI" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfinkgs_TI" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtvalueinrs_TI" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%" class="table table-bordered">
                                                <tr>
                                                    <th colspan="4">TOTAL OUTPUT :</th>
                                                    <th colspan="4">MATERIAL GAIN (-)/LOSS (+)</th>
                                                </tr>
                                                <tr>
                                                    <th>QUANTITY IN KGs</th>
                                                    <th>FAT IN KGs</th>
                                                    <th>SNF IN KGs</th>
                                                    <th>VALUE IN Rs.</th>
                                                    <th>FAT IN KGs</th>
                                                    <th>SNF IN KGs</th>
                                                    <th>FAT %AGE</th>
                                                    <th>SNF %AGE</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtquantityinkgs_TO" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatinkgs_TO" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfinKgs_TO" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtValueinrs_To" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatinkgs_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsngfinkgs_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatpercentage_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfpercentage_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnmaterialbalancing" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnmaterialbalancing_Click"></asp:Button>
                                        </div>
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
        // =========  MATERIAL BALANCING =====
        function CalMB() {
            let dcsmilkcow = document.getElementById('<%=txtdcsmilkcow_MP.ClientID %>').value;
            let dcsmilkbuff = document.getElementById('<%=txtdcsmilkbuff_MP.ClientID %>').value;
            if (dcsmilkcow === '')
                dcsmilkcow = '0';
            if (dcsmilkcow === '.')
                dcsmilkcow = '0';
            if (dcsmilkbuff === '')
                dcsmilkbuff = '0';
            if (dcsmilkbuff === '.')
                dcsmilkbuff = '0';
            let dcsmilktotal = parseFloat(dcsmilkcow) + parseFloat(dcsmilkbuff);
            if (isNaN(dcsmilktotal))
                dcsmilktotal = "0";
            document.getElementById('<%=txtdcsmilktotal_MP.ClientID %>').value = dcsmilktotal.toFixed(2);

            let fat = document.getElementById('<%=txtfat_MP.ClientID %>').value;
            if (fat === '')
                fat = '0';
            if (fat === '.')
                fat = '0';
            let fatpercent = ((parseFloat(fat) / parseFloat(dcsmilktotal)) * 100);
            if (isNaN(fatpercent))
                fatpercent = "0";
            document.getElementById('<%=txtfatpercent_MP.ClientID %>').value = fatpercent.toFixed(2);

            let snf = document.getElementById('<%=txtsnf_MP.ClientID %>').value;
            if (snf === '')
                snf = '0';
            if (snf === '.')
                snf = '0';
            let snfpercent = ((parseFloat(snf) / parseFloat(dcsmilktotal)) * 100);
            if (isNaN(snfpercent))
                snfpercent = "0";
            document.getElementById('<%=txtsnfpercent_MP.ClientID %>').value = snfpercent.toFixed(2);
        }
        function CalMBInOut() {
            let fatTI = document.getElementById('<%=txtfatinkgs_TI.ClientID %>').value;
            let fatTO = document.getElementById('<%=txtfatinkgs_TO.ClientID %>').value;
            if (fatTI === '')
                fatTI = '0';
            if (fatTI === '.')
                fatTI = '0';
            if (fatTO === '')
                fatTO = '0';
            if (fatTO === '.')
                fatTO = '0';
            let fatMG = parseFloat(fatTI) - parseFloat(fatTO);
            if (isNaN(fatMG))
                fatMG = "0";
            document.getElementById('<%=txtfatinkgs_MG.ClientID %>').value = fatMG.toFixed(2);

            let snfTI = document.getElementById('<%=txtsnfinkgs_TI.ClientID %>').value;
            let snfTO = document.getElementById('<%=txtsnfinKgs_TO.ClientID %>').value;
            if (snfTI === '')
                snfTI = '0';
            if (snfTI === '.')
                snfTI = '0';
            if (snfTO === '')
                snfTO = '0';
            if (snfTO === '.')
                snfTO = '0';
            let sngfMG = parseFloat(snfTI) - parseFloat(snfTO);
            if (isNaN(sngfMG))
                sngfMG = "0";
            document.getElementById('<%=txtsngfinkgs_MG.ClientID %>').value = sngfMG.toFixed(2);


            let fatpercentageMG = ((parseFloat(fatMG) / parseFloat(fatTI)) * 100);
            let snfpercentageMG = ((parseFloat(sngfMG) / parseFloat(snfTI)) * 100);
            if (isNaN(fatpercentageMG))
                fatpercentageMG = "0";
            if (isNaN(snfpercentageMG))
                snfpercentageMG = "0";
            document.getElementById('<%=txtfatpercentage_MG.ClientID %>').value = fatpercentageMG.toFixed(2);
            document.getElementById('<%=txtsnfpercentage_MG.ClientID %>').value = snfpercentageMG.toFixed(2);

        }
    </script>

    <script type="text/javascript">
        //=========== CAPACITY UTILISATION=================
        function CapacityUti() {
            
            let dy = noDays();
            let throughpuINKGS = document.getElementById('<%=txtthroughpuINKGS_PC.ClientID %>').value;
            if (throughpuINKGS === '')
                throughpuINKGS = '0';
            if (throughpuINKGS === '.')
                throughpuINKGS = '0';

            let throughpuINLTS = Math.round(parseFloat(throughpuINKGS) / 1.03);
            if (isNaN(throughpuINLTS))
                throughpuINLTS = "0";
            document.getElementById('<%=txtthroughpuINLTS_PC.ClientID %>').value = throughpuINLTS;
            
            let throughputPERDAY = Math.round(parseFloat(throughpuINLTS) / parseInt(dy));
            if (isNaN(throughputPERDAY))
                throughputPERDAY = "0";
            document.getElementById('<%=txtthroughputPERDAY_PC.ClientID %>').value = throughputPERDAY;

            let capacityutilisationINKGS = ((parseFloat(throughputPERDAY) / 300000) * 100);
            document.getElementById('<%=txtcapacityutilisationINKGS_PC.ClientID %>').value = capacityutilisationINKGS.toFixed(2);
        }

        function CapUtiCC() {
            
            let dy = noDays();
            let AllCCsthruoput = document.getElementById('<%=txtAllCCsthruoput_CC.ClientID %>').value;
            if (AllCCsthruoput === '')
                AllCCsthruoput = '0';
            if (AllCCsthruoput === '.')
                AllCCsthruoput = '0';

            let capacityuti = ((((parseFloat(AllCCsthruoput) / 1.03) / parseInt(dy)) / 559500) * 100);
            if (isNaN(capacityuti))
                throughpuINLTS = "0";
            document.getElementById('<%=txtcapacityuti_CC.ClientID %>').value = capacityuti.toFixed(2);
        }
        function CapacityUtiSMP() {
            
            let dy = noDays();
            let bcfProdCFF = document.getElementById('<%=txtbcfProdCFF_BMC.ClientID %>').value;
            if (bcfProdCFF === '')
                bcfProdCFF = '0';
            if (bcfProdCFF === '.')
                bcfProdCFF = '0';
            let capacityuti = (((parseFloat(bcfProdCFF) / parseInt(dy)) / 250) * 100);
            if (isNaN(capacityuti))
                capacityuti = "0";
            document.getElementById('<%=txtcapacityuti_BMC.ClientID %>').value = capacityuti.toFixed(2);

        }
        function noDays() {
            let daysInMonth = '0';
            let month = document.getElementById('<%=ddlmonth.ClientID %>').value;
            let year = document.getElementById('<%=ddlYear.ClientID %>').value;
            if(month != '0' & year != '0')
                daysInMonth = new Date(year, month, 0).getDate();
            return daysInMonth;

        }

    </script>
    <script type="text/javascript">


        function getSum() {
            let sum = 0
            for (let i = 0; i < arguments.length; i++) {

                if (arguments[i] === '')
                    arguments[i] = '0';

                sum += parseFloat(arguments[i]);
            }
            return sum.toFixed(2);
        }


    </script>
    <script type="text/javascript">
        function ADMcalc() {
            document.getElementById('<%=txtTotal_AD.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryAndAllow_AD.ClientID %>').value,
                                                                               document.getElementById('<%=txtMedicalTA_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtConveyence_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtSecurity_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtSupervisionVehic_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtContractLabour_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtInsuranceOTH_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtLegalAuditFee_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtStationary_AD.ClientID %>').value,
                                                                                document.getElementById('<%=txtOther_AD.ClientID %>').value);
            document.getElementById('<%=txtTotal_Provi.ClientID %>').value = getSum(document.getElementById('<%=txtBonus_Provi.ClientID %>').value,
                                                                                document.getElementById('<%=txtAuditFees_Provi.ClientID %>').value,
                                                                                document.getElementById('<%=txtGroupGratutiy_Provi.ClientID %>').value,
                                                                                document.getElementById('<%=txtLiveires_Provi.ClientID %>').value,
                                                                                document.getElementById('<%=txtLeavesalary_Provi.ClientID %>').value,
                                                                                document.getElementById('<%=txtotherExps_Provi.ClientID %>').value);
            document.getElementById('<%=txtFPTotal.ClientID %>').value = getSum(document.getElementById('<%=txtProductMaking.ClientID %>').value,
                                                                                document.getElementById('<%=txtConversionCharge.ClientID %>').value,
                                                                                document.getElementById('<%=txtFPOther.ClientID %>').value);
            document.getElementById('<%=txtTotal_LI.ClientID %>').value = getSum(document.getElementById('<%=txtNDDB_LI.ClientID %>').value,
                                                                                document.getElementById('<%=txtBANKLoan_LI.ClientID %>').value);
        }
        // ================ Farmer's Organisation =============================
        function FOsubFOM() {
          
            let DCSo = document.getElementById('<%=txtdcsorgnised.ClientID %>').value;
            let DCSf = document.getElementById('<%=txtdcsfunctional.ClientID %>').value;
            if (DCSo === '')
                DCSo = '0';
            if (DCSo === '.')
                DCSo = '0';
            if (DCSf === '')
                DCSf = '0';
            if (DCSf === '.')
                DCSf = '0';
            let tolt = parseFloat(DCSo) - parseFloat(DCSf);
            if (isNaN(tolt))
                tolt = "0";
            document.getElementById('<%=txtdcsclosedtemp.ClientID %>').value = tolt.toFixed(2);
        }
        function FOcalc() {


            document.getElementById('<%=txtmembershiptotal.ClientID %>').value = getSum(document.getElementById('<%=txtGeneral.ClientID %>').value,
                                                                                document.getElementById('<%=txtSceduledcaste.ClientID %>').value,
                                                                                document.getElementById('<%=txtscheduletribe.ClientID %>').value,
                                                                                document.getElementById('<%=txtbackworsclasses.ClientID %>').value);
            document.getElementById('<%=txttotal.ClientID %>').value = getSum(document.getElementById('<%=txtlandlesslabour.ClientID %>').value,
                                                                                document.getElementById('<%=txtmarginalfarmer.ClientID %>').value,
                                                                                document.getElementById('<%=txtsmallfarmer.ClientID %>').value,
                                                                                document.getElementById('<%=txtlargefarmer.ClientID %>').value,
                                                                                document.getElementById('<%=txtothers.ClientID %>').value);
            document.getElementById('<%=txttotalfemale.ClientID %>').value = getSum(document.getElementById('<%=txtfemalGeneral.ClientID %>').value,
                                                                                document.getElementById('<%=txtScheCastfemale.ClientID %>').value,
                                                                                document.getElementById('<%=txtschedtribfemale.ClientID %>').value,
                                                                                document.getElementById('<%=txtotherbackwordfemale.ClientID %>').value);
            document.getElementById('<%=txttoalDues.ClientID %>').value = getSum(document.getElementById('<%=txtoldDues.ClientID %>').value,
                                                                                document.getElementById('<%=txtcurrentDues.ClientID %>').value);
            document.getElementById('<%=txtTotalFun.ClientID %>').value = getSum(document.getElementById('<%=txtgenralFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtschedulcasteFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtscheduletribeFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtbackwordFun.ClientID %>').value);
            document.getElementById('<%=txtFunTotal.ClientID %>').value = getSum(document.getElementById('<%=txtlandlesslaboueFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtmarinalfarmerFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtsmallfarmerFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtlargefarmerFun.ClientID %>').value,
                                                                                document.getElementById('<%=txtOthersFun.ClientID %>').value);
            document.getElementById('<%=txtfemaletotal.ClientID %>').value = getSum(document.getElementById('<%=txtfemalegeneral.ClientID %>').value,
                                                                                document.getElementById('<%=txtfemaleschedulcaste.ClientID %>').value,
                                                                                document.getElementById('<%=txtfemaletribe.ClientID %>').value,
                                                                                document.getElementById('<%=txtfemalebackword.ClientID %>').value);
            document.getElementById('<%=txttotalPourers.ClientID %>').value = getSum(document.getElementById('<%=txtmembers.ClientID %>').value,
                                                                                document.getElementById('<%=txtnonmerbers.ClientID %>').value);
            document.getElementById('<%=txtAIcentertotal.ClientID %>').value = getSum(document.getElementById('<%=txtAIcentersingle.ClientID %>').value,
                                                                                document.getElementById('<%=txtAIcentercluster.ClientID %>').value);
            document.getElementById('<%=txtAItotalCow.ClientID %>').value = getSum(document.getElementById('<%=txtAIperformSinglecow.ClientID %>').value,
                                                                                document.getElementById('<%=txtAIperformclustercow.ClientID %>').value);
            document.getElementById('<%=txtAItotalBuff.ClientID %>').value = getSum(document.getElementById('<%=txtAIperformBuff1.ClientID %>').value,
                                                                                document.getElementById('<%=txtAIPerformBuff2.ClientID %>').value);
            document.getElementById('<%=txtAiperformedtotal.ClientID %>').value = getSum(document.getElementById('<%=txtAItotalCow.ClientID %>').value,
                                                                                document.getElementById('<%=txtAItotalBuff.ClientID %>').value);
            document.getElementById('<%=txtcattleinductotal.ClientID %>').value = getSum(document.getElementById('<%=txtcattinducproject.ClientID %>').value,
                                                                                document.getElementById('<%=txtcattinducselffinance.ClientID %>').value);
            document.getElementById('<%=txttotalPAC.ClientID %>').value = getSum(document.getElementById('<%=txtsalarywagesPAC.ClientID %>').value,
                                                                                document.getElementById('<%=txtTaandtransportPAC.ClientID %>').value,
                                                                                document.getElementById('<%=txtcontractlabourPAC.ClientID %>').value,
                                                                                document.getElementById('<%=txtotherexpansesPAC.ClientID %>').value);
            document.getElementById('<%=txttotalcostAiactivites.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryandwagesAiActivites.ClientID %>').value,
                                                                                document.getElementById('<%=txttransportAiActivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtLn2ConsumedAiAcitivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtLn2transportAiactivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtsemenandstrawesAiactivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtotherdirectcostAiactivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtlessincomeAiactivites.ClientID %>').value);
            document.getElementById('<%=txtFPCtotalcost.ClientID %>').value = getSum(document.getElementById('<%=txtFPCsalryandwages.ClientID %>').value,
                                                                                document.getElementById('<%=txtFPCotherdirectcost.ClientID %>').value);
            document.getElementById('<%=txtAHCtotalCost.ClientID %>').value = getSum(document.getElementById('<%=txtAHCsalaryandwages.ClientID %>').value,
                                                                                document.getElementById('<%=txtAHCotherdirectcost.ClientID %>').value);
            document.getElementById('<%=txtTotalCost.ClientID %>').value = getSum(document.getElementById('<%=txtTEcostsalaryandwages.ClientID %>').value,
                                                                                document.getElementById('<%=txtTEcostotherdirectcost.ClientID %>').value,
                                                                                document.getElementById('<%=txtTEcostlessincome.ClientID %>').value);
            document.getElementById('<%=txttotalcostOTI.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryandwagesOTI.ClientID %>').value,
                                                                                document.getElementById('<%=txtotherincmecostOTI.ClientID %>').value);
            document.getElementById('<%=txtFOGrandTotal.ClientID %>').value = getSum(document.getElementById('<%=txtAHCtotalCost.ClientID %>').value,
                                                                                document.getElementById('<%=txtFPCtotalcost.ClientID %>').value,
                                                                                document.getElementById('<%=txtTotalCost.ClientID %>').value,
                                                                                document.getElementById('<%=txttotalcostOTI.ClientID %>').value);
        }
        //===================  RECOMBINATION  ==========================================
        function RMcalc() {

            document.getElementById('<%=txtTOTAL_forMilk.ClientID %>').value = getSum(document.getElementById('<%=txtSMP_forMilk.ClientID %>').value,
                                                                        document.getElementById('<%=txtWB_forMilk.ClientID %>').value,
                                                                        document.getElementById('<%=txtGHEE_forMilk.ClientID %>').value,
                                                                        document.getElementById('<%=txtWMP_forMilk.ClientID %>').value,
                                                                        document.getElementById('<%=txtPANEER_forMilk.ClientID %>').value);
        }
        function RPcalc() {

            document.getElementById('<%=txtTOTAL_forproduct.ClientID %>').value = getSum(document.getElementById('<%=txtSMP_forproduct.ClientID %>').value,
                                                                        document.getElementById('<%=txtWB_forproduct.ClientID %>').value,
                                                                        document.getElementById('<%=txtGHEE_forproduct.ClientID %>').value,
                                                                        document.getElementById('<%=txtWMP_forproduct.ClientID %>').value,
                                                                        document.getElementById('<%=txtPANEER_forproduct.ClientID %>').value);
        }

        function RCommcalc() {

            document.getElementById('<%=txtRecombination.ClientID %>').value = getSum(document.getElementById('<%=txtformilk_Commused.ClientID %>').value,
                                                                        document.getElementById('<%=txtforproduct_Commused.ClientID %>').value);
        }
        // ================== MILK PROCUREMENT AND SALE =======================================
        function MPcalc() {
            document.getElementById('<%=txttotalMilkProc.ClientID %>').value = getSum(document.getElementById('<%=txtDCSmilkRMRD.ClientID %>').value,
                                                                                       document.getElementById('<%=txtDCSmilkCCS.ClientID %>').value,
                                                                                       document.getElementById('<%=txtSMGMILK.ClientID %>').value,
                                                                                       document.getElementById('<%=txtNMGmilk.ClientID %>').value,
                                                                                       document.getElementById('<%=txtOTHER.ClientID %>').value);
        }
        function LMScalc() {
            document.getElementById('<%=txttotalmilksale.ClientID %>').value = getSum(document.getElementById('<%=txtwholemilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtfullcreammilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtstdmilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txttonedmilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtdtmilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtskimmilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtrawchilldmilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtchaispecimilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtcowmilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtsanchilitemilk.ClientID %>').value,
                                                                                        document.getElementById('<%=txtchahamilk.ClientID %>').value);
        }
        function SMGcalc() {
            document.getElementById('<%=txtTotalsmgsale_SMG.ClientID %>').value = getSum(document.getElementById('<%=txtwholemilk_SMG.ClientID %>').value,
                                                                                        document.getElementById('<%=txtskimmilk_SMG.ClientID %>').value,
                                                                                        document.getElementById('<%=txtOther_SMG.ClientID %>').value);
        }
        function NMGcalc() {
            document.getElementById('<%=txtTotalNMGsale_NMG.ClientID %>').value = getSum(document.getElementById('<%=txtwholemilk_NMG.ClientID %>').value,
                                                                                        document.getElementById('<%=txtskimmilk_NMG.ClientID %>').value,
                                                                                        document.getElementById('<%=txtOther_NMG.ClientID %>').value);
        }
        function OScalc() {
            document.getElementById('<%=txttotalBulkSale_OSALE.ClientID %>').value = getSum(document.getElementById('<%=txtwholmilkinLit_OSALE.ClientID %>').value,
                                                                                         document.getElementById('<%=txtskimmilkinLit_OSALE.ClientID %>').value,
                                                                                        document.getElementById('<%=txtOther_OSALE.ClientID %>').value);
        }
        // =============== PROCESSING AND PRODUCTS MAKING ========================
        function PPMcalc() {


            document.getElementById('<%=txtTotal_FPCD.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryandallow_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtContlaboures_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtCosumbleCommon_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtConsumbledirect_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtchemicaldetergent_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtElectricity_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtWater_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtfurnanceoil_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtrepairmaintance_FPCD.ClientID %>').value,
                                                                                document.getElementById('<%=txtOtherExps_FPCD.ClientID %>').value);


            document.getElementById('<%=txtTOTal_CA.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryallow_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtContlaboures_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtConsumblecommon_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtConsumbleDirect_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtchemicaldetergent_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtElectricity_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtWater_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtFurnanceoil_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtRepairMaint_CA.ClientID %>').value,
                                                                                document.getElementById('<%=txtOtherExps_CA.ClientID %>').value);
            document.getElementById('<%=txtPPMGrandTotal.ClientID %>').value = getSum(document.getElementById('<%=txtTotal_FPCD.ClientID %>').value,
                                                                    document.getElementById('<%=txtTOTal_CA.ClientID %>').value);
        }
        //=========================== PACKAGING AND CC =========================
        function PCCcalcPC() {


            document.getElementById('<%=txtTotal_PC.ClientID %>').value = getSum(document.getElementById('<%=txtmilCC_PC.ClientID %>').value,
                                                                                document.getElementById('<%=txtMilkDairy_PC.ClientID %>').value,
                                                                                document.getElementById('<%=txtmainproduct_PC.ClientID %>').value,
                                                                                document.getElementById('<%=txtINDGproduct_PC.ClientID %>').value);
        }
        function PCCcalcCC() {


            document.getElementById('<%=txtTotal_CC.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryallow_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtContlaboure_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtElectricity_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtCoalDiesel_CC.ClientID %>').value,
																				document.getElementById('<%=txtConsumble_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtChemandDeter_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtRepairMAint_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtBMC_CC.ClientID %>').value,
																				document.getElementById('<%=txtSecurity_CC.ClientID %>').value,
                                                                                document.getElementById('<%=txtOtherExps_CC.ClientID %>').value);
        }
        ///===========================Marketing======================================
        function MarkcalcCC() {
            document.getElementById('<%=txtTotal_MarkCostLM.ClientID %>').value = getSum(document.getElementById('<%=txtsalaryAllow_MarkCostLM.ClientID %>').value,
                                                                               document.getElementById('<%=txttranportation_MarkCostLM.ClientID %>').value,
                                                                                document.getElementById('<%=txtsalesprom_MarkCostLM.ClientID %>').value,
                                                                                document.getElementById('<%=txtservicecharg_MarkCostLM.ClientID %>').value,
																				document.getElementById('<%=txtAdvertise_MarkCostLM.ClientID %>').value,
                                                                                document.getElementById('<%=txtAdvanceCard_MarkCostLM.ClientID %>').value,
                                                                                document.getElementById('<%=txtContractlabour_MarkCostLM.ClientID %>').value,
                                                                                document.getElementById('<%=txtOthers_MarkCostLM.ClientID %>').value);
            document.getElementById('<%=txtTotal_SMGNMG.ClientID %>').value = getSum(document.getElementById('<%=txtsalryAllow_SMGNMG.ClientID %>').value,
                                                                                   document.getElementById('<%=txtTransport_SMGNMG.ClientID %>').value,
                                                                                document.getElementById('<%=txtServicCharg_SMGNMG.ClientID %>').value,
                                                                                document.getElementById('<%=txtOthers_SMGNMG.ClientID %>').value);
            document.getElementById('<%=txtTotal_INDG.ClientID %>').value = getSum(document.getElementById('<%=txtsalryAndAllow_INDG.ClientID %>').value,
                                                                                       document.getElementById('<%=txtTransport_INDG.ClientID %>').value,
                                                                                document.getElementById('<%=txtServicCharg_INDG.ClientID %>').value,
                                                                                document.getElementById('<%=txtOthersTax_INDG.ClientID %>').value);
            document.getElementById('<%=txtTotalMKTG.ClientID %>').value = getSum(document.getElementById('<%=txtTotal_MarkCostLM.ClientID %>').value,
                                                                                   document.getElementById('<%=txtTotal_SMGNMG.ClientID %>').value,
                                                                                        document.getElementById('<%=txtTotal_INDG.ClientID %>').value);
        }
        //============================Recepits====================
        function RCTcalc() {
            document.getElementById('<%=txtTotalSaleTR.ClientID %>').value = getSum(document.getElementById('<%=txtmilksaleTR.ClientID %>').value,
                                                                                         document.getElementById('<%=txtProductsTR.ClientID %>').value);
            document.getElementById('<%=txtNetRecieptsTR.ClientID %>').value = getSum(document.getElementById('<%=txtTotalSaleTR.ClientID %>').value,
                                                                                        document.getElementById('<%=txtCommoditiesTR.ClientID %>').value,
                                                                                        document.getElementById('<%=txtClosingStocksTR.ClientID %>').value,
                                                                                        document.getElementById('<%=txtOtherIncomeTR.ClientID %>').value);
            DCSfo();
        }
        function DCSfo() {
            let RCTCPR = document.getElementById('<%=txtNetRecieptsTR.ClientID %>').value;
            let RCTCPT = document.getElementById('<%=txtcommditiesPurchTR.ClientID %>').value;
            let RCTOS = document.getElementById('<%=txtopeningStocksTR.ClientID %>').value;
            if (RCTCPR === '')
                RCTCPR = '0';
            if (RCTCPT === '')
                RCTCPT = '0';
            if (RCTOS === '')
                RCTOS = '0';
            let tolt = parseFloat(RCTCPR) - parseFloat(RCTOS) - parseFloat(RCTCPT)
            if (isNaN(tolt))
                tolt = "0";
            document.getElementById('<%=txtNetRecieptsTR.ClientID %>').value = tolt.toFixed(2);
         }

         function getSum() {
             let sum = 0
             for (let i = 0; i < arguments.length; i++) {

                 if (arguments[i] === '')
                     arguments[i] = '0';
                 if (arguments[i] === '.')
                     arguments[i] = '0';
                 sum += parseFloat(arguments[i]);
             }
             return sum.toFixed(2);
         }



    </script>


    <script>
        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                  (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

                return true;
            else
                return false;
        }
        var validate = function (e) {
            var t = e.value;
            e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
        }
    </script>

</asp:Content>

