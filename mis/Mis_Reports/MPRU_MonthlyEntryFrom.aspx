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
                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                <asp:ListItem Value="06">June</asp:ListItem>
                                                <asp:ListItem Value="07">July</asp:ListItem>
                                                <asp:ListItem Value="08">August</asp:ListItem>
                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                <asp:ListItem Value="01">January</asp:ListItem>
                                                <asp:ListItem Value="02">February</asp:ListItem>
                                                <asp:ListItem Value="03">March</asp:ListItem>
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
                                                <asp:ListItem Value="12">Raw Material Cost</asp:ListItem>
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
                                                                <asp:TextBox ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNOofroutes" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator268" runat="server" ErrorMessage="Value required" ControlToValidate="txtNOofroutes" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOsubFOM()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsorgnised" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator269" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsorgnised" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOsubFOM()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsfunctional" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator270" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsfunctional" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsclosedtemp" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator271" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsclosedtemp" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtnewdcsorganisedmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator272" runat="server" ErrorMessage="Value required" ControlToValidate="txtnewdcsorganisedmonth" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtnewdcsregisteredmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator273" runat="server" ErrorMessage="Value required" ControlToValidate="txtnewdcsregisteredmonth" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsrevivedmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator274" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsrevivedmonth" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcolesdmonth" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator275" runat="server" ErrorMessage="Value required" ControlToValidate="txtcolesdmonth" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtGeneral" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator276" runat="server" ErrorMessage="Value required" ControlToValidate="txtGeneral" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtSceduledcaste" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator277" runat="server" ErrorMessage="Value required" ControlToValidate="txtSceduledcaste" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtscheduletribe" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator278" runat="server" ErrorMessage="Value required" ControlToValidate="txtscheduletribe" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbackworsclasses" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator279" runat="server" ErrorMessage="Value required" ControlToValidate="txtbackworsclasses" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmembershiptotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator280" runat="server" ErrorMessage="Value required" ControlToValidate="txtmembershiptotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlandlesslabour" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator281" runat="server" ErrorMessage="Value required" ControlToValidate="txtlandlesslabour" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmarginalfarmer" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator282" runat="server" ErrorMessage="Value required" ControlToValidate="txtmarginalfarmer" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsmallfarmer" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator283" runat="server" ErrorMessage="Value required" ControlToValidate="txtsmallfarmer" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlargefarmer" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator284" runat="server" ErrorMessage="Value required" ControlToValidate="txtlargefarmer" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtothers" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator285" runat="server" ErrorMessage="Value required" ControlToValidate="txtothers" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator286" runat="server" ErrorMessage="Value required" ControlToValidate="txttotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemalGeneral" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator287" runat="server" ErrorMessage="Value required" ControlToValidate="txtfemalGeneral" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtScheCastfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator288" runat="server" ErrorMessage="Value required" ControlToValidate="txtScheCastfemale" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtschedtribfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator289" runat="server" ErrorMessage="Value required" ControlToValidate="txtschedtribfemale" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherbackwordfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator290" runat="server" ErrorMessage="Value required" ControlToValidate="txtotherbackwordfemale" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalfemale" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator291" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalfemale" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtoldDues" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator292" runat="server" ErrorMessage="Value required" ControlToValidate="txtoldDues" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcurrentDues" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator293" runat="server" ErrorMessage="Value required" ControlToValidate="txtcurrentDues" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttoalDues" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator294" runat="server" ErrorMessage="Value required" ControlToValidate="txttoalDues" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtgenralFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator295" runat="server" ErrorMessage="Value required" ControlToValidate="txtgenralFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtschedulcasteFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator296" runat="server" ErrorMessage="Value required" ControlToValidate="txtschedulcasteFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtscheduletribeFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator297" runat="server" ErrorMessage="Value required" ControlToValidate="txtscheduletribeFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtbackwordFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator298" runat="server" ErrorMessage="Value required" ControlToValidate="txtbackwordFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator299" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlandlesslaboueFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator300" runat="server" ErrorMessage="Value required" ControlToValidate="txtlandlesslaboueFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmarinalfarmerFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator301" runat="server" ErrorMessage="Value required" ControlToValidate="txtmarinalfarmerFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsmallfarmerFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator302" runat="server" ErrorMessage="Value required" ControlToValidate="txtsmallfarmerFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlargefarmerFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator303" runat="server" ErrorMessage="Value required" ControlToValidate="txtlargefarmerFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthersFun" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator304" runat="server" ErrorMessage="Value required" ControlToValidate="txtOthersFun" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFunTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator305" runat="server" ErrorMessage="Value required" ControlToValidate="txtFunTotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemalegeneral" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator306" runat="server" ErrorMessage="Value required" ControlToValidate="txtfemalegeneral" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemaleschedulcaste" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator307" runat="server" ErrorMessage="Value required" ControlToValidate="txtfemaleschedulcaste" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemaletribe" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator308" runat="server" ErrorMessage="Value required" ControlToValidate="txtfemaletribe" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemalebackword" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator309" runat="server" ErrorMessage="Value required" ControlToValidate="txtfemalebackword" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfemaletotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator310" runat="server" ErrorMessage="Value required" ControlToValidate="txtfemaletotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmembers" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator311" runat="server" ErrorMessage="Value required" ControlToValidate="txtmembers" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtnonmerbers" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator312" runat="server" ErrorMessage="Value required" ControlToValidate="txtnonmerbers" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalPourers" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator313" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalPourers" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIcentersingle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator314" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIcentersingle" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIcentercluster" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator315" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIcentercluster" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIcentertotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator316" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIcentertotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIperformSinglecow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator317" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIperformSinglecow" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIperformBuff1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator318" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIperformBuff1" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIperformclustercow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator319" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIperformclustercow" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAIPerformBuff2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator320" runat="server" ErrorMessage="Value required" ControlToValidate="txtAIPerformBuff2" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAItotalCow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator321" runat="server" ErrorMessage="Value required" ControlToValidate="txtAItotalCow" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAItotalBuff" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator322" runat="server" ErrorMessage="Value required" ControlToValidate="txtAItotalBuff" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAiperformedtotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator323" runat="server" ErrorMessage="Value required" ControlToValidate="txtAiperformedtotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcalvborntotalcow" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator324" runat="server" ErrorMessage="Value required" ControlToValidate="txtcalvborntotalcow" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcalvbornbuff" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator325" runat="server" ErrorMessage="Value required" ControlToValidate="txtcalvbornbuff" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtanimalhusfirstAid" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator326" runat="server" ErrorMessage="Value required" ControlToValidate="txtanimalhusfirstAid" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtaniamlhusAHWcase" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator327" runat="server" ErrorMessage="Value required" ControlToValidate="txtaniamlhusAHWcase" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattlefiedsold" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator328" runat="server" ErrorMessage="Value required" ControlToValidate="txtcattlefiedsold" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsselingbcf" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator329" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsselingbcf" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtmmsalebydcs" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator330" runat="server" ErrorMessage="Value required" ControlToValidate="txtmmsalebydcs" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattinducproject" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator331" runat="server" ErrorMessage="Value required" ControlToValidate="txtcattinducproject" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattinducselffinance" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator332" runat="server" ErrorMessage="Value required" ControlToValidate="txtcattinducselffinance" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcattleinductotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator333" runat="server" ErrorMessage="Value required" ControlToValidate="txtcattleinductotal" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalarywagesPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator334" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalarywagesPAC" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTaandtransportPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator335" runat="server" ErrorMessage="Value required" ControlToValidate="txtTaandtransportPAC" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtcontractlabourPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator336" runat="server" ErrorMessage="Value required" ControlToValidate="txtcontractlabourPAC" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherexpansesPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator337" runat="server" ErrorMessage="Value required" ControlToValidate="txtotherexpansesPAC" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalPAC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator338" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalPAC" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryandwagesAiActivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator339" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryandwagesAiActivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttransportAiActivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator340" runat="server" ErrorMessage="Value required" ControlToValidate="txttransportAiActivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLn2ConsumedAiAcitivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator341" runat="server" ErrorMessage="Value required" ControlToValidate="txtLn2ConsumedAiAcitivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLn2transportAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator342" runat="server" ErrorMessage="Value required" ControlToValidate="txtLn2transportAiactivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsemenandstrawesAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator343" runat="server" ErrorMessage="Value required" ControlToValidate="txtsemenandstrawesAiactivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherdirectcostAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator344" runat="server" ErrorMessage="Value required" ControlToValidate="txtotherdirectcostAiactivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtlessincomeAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator345" runat="server" ErrorMessage="Value required" ControlToValidate="txtlessincomeAiactivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalcostAiactivites" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator346" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalcostAiactivites" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAHCsalaryandwages" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator347" runat="server" ErrorMessage="Value required" ControlToValidate="txtAHCsalaryandwages" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAHCotherdirectcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator348" runat="server" ErrorMessage="Value required" ControlToValidate="txtAHCotherdirectcost" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAHCtotalCost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator349" runat="server" ErrorMessage="Value required" ControlToValidate="txtAHCtotalCost" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPCsalryandwages" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator350" runat="server" ErrorMessage="Value required" ControlToValidate="txtFPCsalryandwages" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPCotherdirectcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator351" runat="server" ErrorMessage="Value required" ControlToValidate="txtFPCotherdirectcost" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPCtotalcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator352" runat="server" ErrorMessage="Value required" ControlToValidate="txtFPCtotalcost" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th colspan="4">TRAINING & EXTANTION COST : (Rs.)</th>
                                                        <th colspan="4">OTHER INPUT COST : (Rs.)</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Salary and Wages</th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Less Income</th>
                                                        <th>Total Cost</th>
                                                        <th>Salary and Wages</th>
                                                        <th>Other Direct Cost</th>
                                                        <th>Less Income</th>
                                                        <th>Total Cost</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTEcostsalaryandwages" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator353" runat="server" ErrorMessage="Value required" ControlToValidate="txtTEcostsalaryandwages" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTEcostotherdirectcost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator354" runat="server" ErrorMessage="Value required" ControlToValidate="txtTEcostotherdirectcost" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTEcostlessincome" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator355" runat="server" ErrorMessage="Value required" ControlToValidate="txtTEcostlessincome" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalCost" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator356" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalCost" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryandwagesOTI" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator357" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryandwagesOTI" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherincmecostOTI" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator358" runat="server" ErrorMessage="Value required" ControlToValidate="txtotherincmecostOTI" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOIcostlessincome" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator359" runat="server" ErrorMessage="Value required" ControlToValidate="txtOIcostlessincome" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttotalcostOTI" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator360" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalcostOTI" ValidationGroup="FO" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFOGrandTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnFO" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="FO" OnClick="btnFO_Click"></asp:Button>
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
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator222" runat="server" ErrorMessage="Value required" ControlToValidate="txtDCSmilkRMRD" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDCSmilkCCS" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator223" runat="server" ErrorMessage="Value required" ControlToValidate="txtDCSmilkCCS" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGMILK" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator224" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMGMILK" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGmilk" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator225" runat="server" ErrorMessage="Value required" ControlToValidate="txtNMGmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOTHER" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator226" runat="server" ErrorMessage="Value required" ControlToValidate="txtOTHER" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID="hfTotalMP" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalMilkProc" onpaste="return false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator227" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalMilkProc" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator228" runat="server" ErrorMessage="Value required" ControlToValidate="txtwholemilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtfullcreammilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator229" runat="server" ErrorMessage="Value required" ControlToValidate="txtfullcreammilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtstdmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator230" runat="server" ErrorMessage="Value required" ControlToValidate="txtstdmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttonedmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator231" runat="server" ErrorMessage="Value required" ControlToValidate="txttonedmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdtmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator232" runat="server" ErrorMessage="Value required" ControlToValidate="txtdtmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator233" runat="server" ErrorMessage="Value required" ControlToValidate="txtskimmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtrawchilldmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator234" runat="server" ErrorMessage="Value required" ControlToValidate="txtskimmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtchaispecimilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator235" runat="server" ErrorMessage="Value required" ControlToValidate="txtchaispecimilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcowmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator236" runat="server" ErrorMessage="Value required" ControlToValidate="txtcowmilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsanchilitemilk" onpaste="return false" ClientIDMode="Static" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator237" runat="server" ErrorMessage="Value required" ControlToValidate="txtsanchilitemilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtchahamilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator238" runat="server" ErrorMessage="Value required" ControlToValidate="txtchahamilk" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalmilksale" onpaste="return false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator239" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalmilksale" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID="hftotalmilksale" runat="server" />
                                                                <asp:HiddenField ClientIDMode="Static" ID="hfDay" runat="server" />
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
                                                                <asp:TextBox ID="txtwholemilk_SMG" onpaste="return false" ClientIDMode="Static" onBlur="SMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator240" runat="server" ErrorMessage="Value required" ControlToValidate="txtwholemilk_SMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_SMG" onpaste="return false" ClientIDMode="Static" onBlur="SMGcalc1()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator241" runat="server" ErrorMessage="Value required" ControlToValidate="txtskimmilk_SMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_SMG" onpaste="return false" ClientIDMode="Static" onBlur="SMGcalc2()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator242" runat="server" ErrorMessage="Value required" ControlToValidate="txtOther_SMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalsmgsale_SMG" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator243" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalsmgsale_SMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID="hfSMS_TotalSMGSale" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk_NMG" onpaste="return false" ClientIDMode="Static" onBlur="NMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator244" runat="server" ErrorMessage="Value required" ControlToValidate="txtwholemilk_NMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_NMG" onpaste="return false" ClientIDMode="Static" onBlur="NMGcalc1()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator245" runat="server" ErrorMessage="Value required" ControlToValidate="txtskimmilk_NMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_NMG" onpaste="return false" ClientIDMode="Static" onBlur="NMGcalc2()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator246" runat="server" ErrorMessage="Value required" ControlToValidate="txtOther_NMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalNMGsale_NMG" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator247" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalNMGsale_NMG" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID="hfTotal_NMS_OS" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholmilkinLit_OSALE" onpaste="return false" ClientIDMode="Static" onBlur="OScalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator248" runat="server" ErrorMessage="Value required" ControlToValidate="txtwholmilkinLit_OSALE" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilkinLit_OSALE" onpaste="return false" ClientIDMode="Static" onBlur="OScalc1()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator249" runat="server" ErrorMessage="Value required" ControlToValidate="txtskimmilkinLit_OSALE" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_OSALE" onpaste="return false" ClientIDMode="Static" onBlur="OScalc2()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator250" runat="server" ErrorMessage="Value required" ControlToValidate="txtOther_OSALE" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalBulkSale_OSALE" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator251" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalBulkSale_OSALE" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator252" runat="server" ErrorMessage="Value required" ControlToValidate="txtMILKPROC_monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKPROC_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator253" runat="server" ErrorMessage="Value required" ControlToValidate="txtMILKPROC_Cummulat" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILK_MOnthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator254" runat="server" ErrorMessage="Value required" ControlToValidate="txtLocalMILK_MOnthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMilk_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator255" runat="server" ErrorMessage="Value required" ControlToValidate="txtLocalMilk_Cummulat" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalMilkSale_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator256" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalMilkSale_Monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalMilkSale_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator257" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalMilkSale_Cummulat" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGmilk_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator258" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMGmilk_Monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGmilk_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator259" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMGmilk_Cummulat" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGOTH_MOnthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator260" runat="server" ErrorMessage="Value required" ControlToValidate="txtNMGOTH_MOnthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGOTH_Cummulat" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator261" runat="server" ErrorMessage="Value required" ControlToValidate="txtNMGOTH_Cummulat" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                            <th colspan="3">MILK PROCUREMENT(KGPD)</th>
                                                            <th colspan="3">LOCAL MILK SALE(LPD)</th>
                                                        </tr>
                                                        <tr>
                                                            <th>Monthly</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                            <th>Monthly</th>
                                                            <th>Monthly</th>
                                                            <th>Cummulative</th>
                                                        </tr>
                                                        <tr>
                                                            <th>IN THE MONTH KG</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                            <th>IN THE MONTH Ltr.</th>
                                                            <th>IN THE MONTH</th>
                                                            <th>TILL MONTH</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKproKGPD_KG_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator262" runat="server" ErrorMessage="Value required" ControlToValidate="txtMILKproKGPD_KG_Monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKproKGPD_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator263" runat="server" ErrorMessage="Value required" ControlToValidate="txtMILKproKGPD_Monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKprocKGPD_Cummulative" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator264" runat="server" ErrorMessage="Value required" ControlToValidate="txtMILKprocKGPD_Cummulative" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID="hfMP_TillMonthGMPMS" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILKLPD_Ltr_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator265" runat="server" ErrorMessage="Value required" ControlToValidate="txtLocalMILKLPD_Ltr_Monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILKLPD_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator266" runat="server" ErrorMessage="Value required" ControlToValidate="txtLocalMILKLPD_Monthly" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalmilkLPD_Cummulative" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator267" runat="server" ErrorMessage="Value required" ControlToValidate="txtLocalmilkLPD_Cummulative" ValidationGroup="MPAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID="hfLMS_TillMonthGMPMS" runat="server" />
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
                                                                <asp:TextBox ID="txtGhee" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator167" runat="server" ErrorMessage="Value required" ControlToValidate="txtGhee" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSkimmilkpowder" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator168" runat="server" ErrorMessage="Value required" ControlToValidate="txtSkimmilkpowder" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttablebutter" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator169" runat="server" ErrorMessage="Value required" ControlToValidate="txttablebutter" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwhitebutter" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator170" runat="server" ErrorMessage="Value required" ControlToValidate="txtwhitebutter" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator171" runat="server" ErrorMessage="Value required" ControlToValidate="txtWMP" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpaneer" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator172" runat="server" ErrorMessage="Value required" ControlToValidate="txtpaneer" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSanchi_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator173" runat="server" ErrorMessage="Value required" ControlToValidate="txtSanchi_GHEESale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSneha_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator174" runat="server" ErrorMessage="Value required" ControlToValidate="txtSneha_GHEESale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator175" runat="server" ErrorMessage="Value required" ControlToValidate="txtOther_GHEESale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator176" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_GHEESale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator177" runat="server" ErrorMessage="Value required" ControlToValidate="txtskimmilk_Prodctsale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttablebutter_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator178" runat="server" ErrorMessage="Value required" ControlToValidate="txttablebutter_Prodctsale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwhitebutter_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator179" runat="server" ErrorMessage="Value required" ControlToValidate="txtwhitebutter_Prodctsale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtshrikhand_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator180" runat="server" ErrorMessage="Value required" ControlToValidate="txtshrikhand_Prodctsale" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ID="txtPAneer_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator181" runat="server" ErrorMessage="Value required" ControlToValidate="txtPAneer_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtflavmilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator182" runat="server" ErrorMessage="Value required" ControlToValidate="txtflavmilk_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBtrmilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator183" runat="server" ErrorMessage="Value required" ControlToValidate="txtBtrmilk_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSweetcurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator184" runat="server" ErrorMessage="Value required" ControlToValidate="txtSweetcurd_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpeda_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator185" runat="server" ErrorMessage="Value required" ControlToValidate="txtpeda_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtplaincurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator186" runat="server" ErrorMessage="Value required" ControlToValidate="txtplaincurd_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtorangsip_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator187" runat="server" ErrorMessage="Value required" ControlToValidate="txtorangsip_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtprobioticcurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator188" runat="server" ErrorMessage="Value required" ControlToValidate="txtprobioticcurd_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator189" runat="server" ErrorMessage="Value required" ControlToValidate="txtwholemilk_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChenarabdi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator190" runat="server" ErrorMessage="Value required" ControlToValidate="txtChenarabdi_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpresscurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator191" runat="server" ErrorMessage="Value required" ControlToValidate="txtpresscurd_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcream_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator192" runat="server" ErrorMessage="Value required" ControlToValidate="txtcream_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtlassi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator193" runat="server" ErrorMessage="Value required" ControlToValidate="txtlassi_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtamarkhand_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator194" runat="server" ErrorMessage="Value required" ControlToValidate="txtamarkhand_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsmp_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator195" runat="server" ErrorMessage="Value required" ControlToValidate="txtsmp_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ID="txtmawa_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator196" runat="server" ErrorMessage="Value required" ControlToValidate="txtmawa_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdrycasein_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator197" runat="server" ErrorMessage="Value required" ControlToValidate="txtdrycasein_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcookingbutter_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator198" runat="server" ErrorMessage="Value required" ControlToValidate="txtcookingbutter_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgulabjamun_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator199" runat="server" ErrorMessage="Value required" ControlToValidate="txtgulabjamun_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtrasgulla_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator200" runat="server" ErrorMessage="Value required" ControlToValidate="txtrasgulla_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmawagulabjanum_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator201" runat="server" ErrorMessage="Value required" ControlToValidate="txtmawagulabjanum_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmilkcake_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator202" runat="server" ErrorMessage="Value required" ControlToValidate="txtmilkcake_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtThandai_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator203" runat="server" ErrorMessage="Value required" ControlToValidate="txtThandai_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMDm_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator204" runat="server" ErrorMessage="Value required" ControlToValidate="txtMDm_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtlightlassi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator205" runat="server" ErrorMessage="Value required" ControlToValidate="txtlightlassi_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpudinaraita_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator206" runat="server" ErrorMessage="Value required" ControlToValidate="txtpudinaraita_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator207" runat="server" ErrorMessage="Value required" ControlToValidate="txtWMP_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpannerachar_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator208" runat="server" ErrorMessage="Value required" ControlToValidate="txtpannerachar_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox ID="txtsanchilitemilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator209" runat="server" ErrorMessage="Value required" ControlToValidate="txtsanchilitemilk_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtnariyalbarfi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator210" runat="server" ErrorMessage="Value required" ControlToValidate="txtnariyalbarfi_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgulabjamunMix_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator211" runat="server" ErrorMessage="Value required" ControlToValidate="txtgulabjamunMix_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcoffemix_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator212" runat="server" ErrorMessage="Value required" ControlToValidate="txtcoffemix_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcookingbutter" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator213" runat="server" ErrorMessage="Value required" ControlToValidate="txtcookingbutter" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtlowfatpanner_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator214" runat="server" ErrorMessage="Value required" ControlToValidate="txtlowfatpanner_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwheydrink_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator215" runat="server" ErrorMessage="Value required" ControlToValidate="txtwheydrink_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsanchitea_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator216" runat="server" ErrorMessage="Value required" ControlToValidate="txtsanchitea_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpedaprasadi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator217" runat="server" ErrorMessage="Value required" ControlToValidate="txtpedaprasadi_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txticecream_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator218" runat="server" ErrorMessage="Value required" ControlToValidate="txticecream_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgoldenmilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator219" runat="server" ErrorMessage="Value required" ControlToValidate="txtgoldenmilk_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsugarfreepeda_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator220" runat="server" ErrorMessage="Value required" ControlToValidate="txtsugarfreepeda_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txthealthvita_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator221" runat="server" ErrorMessage="Value required" ControlToValidate="txthealthvita_PMAS" ValidationGroup="PMAS" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnPMandSale" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="PMAS" OnClick="btnPMandSale_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <%--VALIDATION COMPLETED--%>
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
                                                                <asp:TextBox ID="txtSMP_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator152" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMP_forMilk" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWB_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator153" runat="server" ErrorMessage="Value required" ControlToValidate="txtWB_forMilk" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGHEE_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator154" runat="server" ErrorMessage="Value required" ControlToValidate="txtGHEE_forMilk" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator155" runat="server" ErrorMessage="Value required" ControlToValidate="txtWMP_forMilk" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPANEER_forMilk" onpaste="return false" ClientIDMode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator156" runat="server" ErrorMessage="Value required" ControlToValidate="txtPANEER_forMilk" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTOTAL_forMilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator157" runat="server" ErrorMessage="Value required" ControlToValidate="txtTOTAL_forMilk" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMP_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator158" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMP_forproduct" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWB_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator159" runat="server" ErrorMessage="Value required" ControlToValidate="txtWB_forproduct" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGHEE_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator160" runat="server" ErrorMessage="Value required" ControlToValidate="txtGHEE_forproduct" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtWMP_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator161" runat="server" ErrorMessage="Value required" ControlToValidate="txtWMP_forproduct" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPANEER_forproduct" onpaste="return false" ClientIDMode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator162" runat="server" ErrorMessage="Value required" ControlToValidate="txtPANEER_forproduct" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTOTAL_forproduct" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator163" runat="server" ErrorMessage="Value required" ControlToValidate="txtTOTAL_forproduct" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtformilk_Commused" onpaste="return false" ClientIDMode="Static" onkeyup="RCommcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator164" runat="server" ErrorMessage="Value required" ControlToValidate="txtformilk_Commused" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtforproduct_Commused" onpaste="return false" ClientIDMode="Static" onkeyup="RCommcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator165" runat="server" ErrorMessage="Value required" ControlToValidate="txtforproduct_Commused" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRecombination" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator166" runat="server" ErrorMessage="Value required" ControlToValidate="txtRecombination" ValidationGroup="REC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnrecombination" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="REC" OnClick="btnrecombination_Click"></asp:Button>
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
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryandallow_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator128" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryandallow_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContlaboures_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator129" runat="server" ErrorMessage="Value required" ControlToValidate="txtContlaboures_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtCosumbleCommon_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator130" runat="server" ErrorMessage="Value required" ControlToValidate="txtCosumbleCommon_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConsumbledirect_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator131" runat="server" ErrorMessage="Value required" ControlToValidate="txtConsumbledirect_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtchemicaldetergent_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator132" runat="server" ErrorMessage="Value required" ControlToValidate="txtchemicaldetergent_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtElectricity_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator133" runat="server" ErrorMessage="Value required" ControlToValidate="txtElectricity_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtWater_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator134" runat="server" ErrorMessage="Value required" ControlToValidate="txtWater_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfurnanceoil_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator135" runat="server" ErrorMessage="Value required" ControlToValidate="txtfurnanceoil_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtrepairmaintance_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator136" runat="server" ErrorMessage="Value required" ControlToValidate="txtrepairmaintance_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOtherExps_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator137" runat="server" ErrorMessage="Value required" ControlToValidate="txtOtherExps_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_FPCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator138" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_FPCD" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryallow_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator139" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryallow_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContlaboures_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator140" runat="server" ErrorMessage="Value required" ControlToValidate="txtContlaboures_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConsumblecommon_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator141" runat="server" ErrorMessage="Value required" ControlToValidate="txtConsumblecommon_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConsumbleDirect_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator142" runat="server" ErrorMessage="Value required" ControlToValidate="txtConsumbleDirect_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtchemicaldetergent_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator143" runat="server" ErrorMessage="Value required" ControlToValidate="txtchemicaldetergent_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtElectricity_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator144" runat="server" ErrorMessage="Value required" ControlToValidate="txtElectricity_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtWater_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator145" runat="server" ErrorMessage="Value required" ControlToValidate="txtWater_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFurnanceoil_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator146" runat="server" ErrorMessage="Value required" ControlToValidate="txtFurnanceoil_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtRepairMaint_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator147" runat="server" ErrorMessage="Value required" ControlToValidate="txtRepairMaint_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOtherExps_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator148" runat="server" ErrorMessage="Value required" ControlToValidate="txtOtherExps_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTOTal_CA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator149" runat="server" ErrorMessage="Value required" ControlToValidate="txtTOTal_CA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtMilkProssCA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator150" runat="server" ErrorMessage="Value required" ControlToValidate="txtMilkProssCA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtMilkPrepackCA" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator151" runat="server" ErrorMessage="Value required" ControlToValidate="txtMilkPrepackCA" ValidationGroup="PPM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                <asp:TextBox ClientIDMode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtPPMGrandTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnPPMaking" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="PPM" OnClick="btnPPMaking_Click1"></asp:Button>
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
                                                            <th>Chemical And Detergent</th>
                                                            <th>Repair And Maint.</th>
                                                            <th>BMC</th>
                                                            <th>Security</th>
                                                            <th>Other Exps.</th>
                                                            <th>Total</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtmilCC_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator112" runat="server" ErrorMessage="Value required" ControlToValidate="txtmilCC_PC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMilkDairy_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator113" runat="server" ErrorMessage="Value required" ControlToValidate="txtMilkDairy_PC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmainproduct_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator114" runat="server" ErrorMessage="Value required" ControlToValidate="txtmainproduct_PC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtINDGproduct_PC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator115" runat="server" ErrorMessage="Value required" ControlToValidate="txtINDGproduct_PC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal_PC" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator116" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_PC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsalaryallow_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator117" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryallow_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtContlaboure_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator118" runat="server" ErrorMessage="Value required" ControlToValidate="txtContlaboure_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtElectricity_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator119" runat="server" ErrorMessage="Value required" ControlToValidate="txtElectricity_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCoalDiesel_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator120" runat="server" ErrorMessage="Value required" ControlToValidate="txtCoalDiesel_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtConsumble_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator121" runat="server" ErrorMessage="Value required" ControlToValidate="txtConsumble_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChemandDeter_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator122" runat="server" ErrorMessage="Value required" ControlToValidate="txtChemandDeter_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRepairMAint_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator123" runat="server" ErrorMessage="Value required" ControlToValidate="txtRepairMAint_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBMC_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator124" runat="server" ErrorMessage="Value required" ControlToValidate="txtBMC_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSecurity_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator125" runat="server" ErrorMessage="Value required" ControlToValidate="txtSecurity_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOtherExps_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator126" runat="server" ErrorMessage="Value required" ControlToValidate="txtOtherExps_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotal_CC" onpaste="return false" ClientIDMode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator127" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_CC" ValidationGroup="P&CC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnPackagingAndCC" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="P&CC" OnClick="btnPackagingAndCC_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <%--VALIDATION COMPLETED--%>
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
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryAllow_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator92" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryAllow_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txttranportation_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator93" runat="server" ErrorMessage="Value required" ControlToValidate="txttranportation_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalesprom_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator94" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalesprom_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtservicecharg_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator95" runat="server" ErrorMessage="Value required" ControlToValidate="txtservicecharg_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAdvertise_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator96" runat="server" ErrorMessage="Value required" ControlToValidate="txtAdvertise_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAdvanceCard_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator97" runat="server" ErrorMessage="Value required" ControlToValidate="txtAdvanceCard_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContractlabour_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator98" runat="server" ErrorMessage="Value required" ControlToValidate="txtContractlabour_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthers_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator99" runat="server" ErrorMessage="Value required" ControlToValidate="txtOthers_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_MarkCostLM" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator100" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_MarkCostLM" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                    <th>Other</th>
                                                    <th>Total</th>
                                                    <th>Salary And Allow</th>
                                                    <th>transportation</th>
                                                    <th>Service-Charge</th>
                                                    <th>Other Tax</th>
                                                    <th>Total</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalryAllow_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator101" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalryAllow_SMGNMG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTransport_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator102" runat="server" ErrorMessage="Value required" ControlToValidate="txtTransport_SMGNMG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtServicCharg_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator103" runat="server" ErrorMessage="Value required" ControlToValidate="txtServicCharg_SMGNMG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthers_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator104" runat="server" ErrorMessage="Value required" ControlToValidate="txtOthers_SMGNMG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_SMGNMG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator105" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_SMGNMG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalryAndAllow_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator106" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalryAndAllow_INDG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTransport_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator107" runat="server" ErrorMessage="Value required" ControlToValidate="txtTransport_INDG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtServicCharg_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator108" runat="server" ErrorMessage="Value required" ControlToValidate="txtServicCharg_INDG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOthersTax_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator109" runat="server" ErrorMessage="Value required" ControlToValidate="txtOthersTax_INDG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_INDG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator110" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_INDG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotalMKTG" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator111" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalMKTG" ValidationGroup="Mar" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnMarketing" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="Mar" OnClick="btnMarketing_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>

                                <div id="RawMaterialCost" runat="server">
                                    <fieldset>
                                        <legend>Raw Material Cost</legend>
                                        <fieldset>
                                            <legend>8.0 FINANCIAL PERFORMANCE : (Rs.)</legend>
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th colspan="6">Raw Material Cost</th>
                                                            <th colspan="5">PROCUREMENT TRANSPORTATION</th>
                                                        </tr>
                                                        <tr>
                                                            <th>DCS Milk</th>
                                                            <th>SMG Milk</th>
                                                            <th>NMG Milk</th>
                                                            <th>Other Milk</th>
                                                            <th>COMM. Used</th>
                                                            <th>Total</th>
                                                            <th>DCS-DAIRY/CC/IMC</th>
                                                            <th>CC/IMC TO  DAIRY</th>
                                                            <th>SMG MILK</th>
                                                            <th>NMG MILK</th>
                                                            <th>TOTAL</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtDCSMilkRMC" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Value required" ControlToValidate="txtDCSMilkRMC" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtSMGMilkRMC" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMGMilkRMC" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtNMGMilkRMC" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Value required" ControlToValidate="txtNMGMilkRMC" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtOtherMilkRMC" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Value required" ControlToValidate="txtOtherMilkRMC" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtCOMMUsedRMC" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Value required" ControlToValidate="txtCOMMUsedRMC" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtTotalRMC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtDCSdairyCCimc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Value required" ControlToValidate="txtDCSdairyCCimc" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtccIMCtoDAIRYpt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Value required" ControlToValidate="txtccIMCtoDAIRYpt" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtSMGMILKPT" runat="server" CssClass="form-control">

                                                                </asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMGMILKPT" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtNMGMILKpt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="Value required" ControlToValidate="txtNMGMILKpt" ValidationGroup="a"
                                                                    ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" ID="txtTotalPT" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnroematerial" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnroematerial_click" ValidationGroup="a"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div id="Administration" runat="server">
                                    <fieldset>
                                        <legend>ADMINISTRATION</legend>
                                        <fieldset>
                                            <legend>9.0 FINANCIAL PERFORMANCE (conted): (Rs.)</legend>
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
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsalaryAndAllow_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator66" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryAndAllow_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtMedicalTA_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator67" runat="server" ErrorMessage="Value required" ControlToValidate="txtMedicalTA_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConveyence_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator68" runat="server" ErrorMessage="Value required" ControlToValidate="txtConveyence_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtSecurity_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator69" runat="server" ErrorMessage="Value required" ControlToValidate="txtSecurity_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtSupervisionVehic_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator70" runat="server" ErrorMessage="Value required" ControlToValidate="txtSupervisionVehic_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtContractLabour_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator71" runat="server" ErrorMessage="Value required" ControlToValidate="txtContractLabour_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtInsuranceOTH_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator72" runat="server" ErrorMessage="Value required" ControlToValidate="txtInsuranceOTH_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLegalAuditFee_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator73" runat="server" ErrorMessage="Value required" ControlToValidate="txtLegalAuditFee_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtStationary_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator74" runat="server" ErrorMessage="Value required" ControlToValidate="txtStationary_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtOther_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator75" runat="server" ErrorMessage="Value required" ControlToValidate="txtOther_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_AD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator76" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_AD" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtBonus_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator77" runat="server" ErrorMessage="Value required" ControlToValidate="txtBonus_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtAuditFees_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator78" runat="server" ErrorMessage="Value required" ControlToValidate="txtAuditFees_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtGroupGratutiy_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator79" runat="server" ErrorMessage="Value required" ControlToValidate="txtGroupGratutiy_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLiveires_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator80" runat="server" ErrorMessage="Value required" ControlToValidate="txtLiveires_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtLeavesalary_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator81" runat="server" ErrorMessage="Value required" ControlToValidate="txtLeavesalary_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtotherExps_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator82" runat="server" ErrorMessage="Value required" ControlToValidate="txtotherExps_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_Provi" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator83" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_Provi" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtNDDB_LI" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator84" runat="server" ErrorMessage="Value required" ControlToValidate="txtNDDB_LI" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtBANKLoan_LI" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator85" runat="server" ErrorMessage="Value required" ControlToValidate="txtBANKLoan_LI" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtTotal_LI" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator86" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotal_LI" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtDepreciation" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator87" runat="server" ErrorMessage="Value required" ControlToValidate="txtDepreciation" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtProductMaking" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator88" runat="server" ErrorMessage="Value required" ControlToValidate="txtProductMaking" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtConversionCharge" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator89" runat="server" ErrorMessage="Value required" ControlToValidate="txtConversionCharge" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPOther" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator90" runat="server" ErrorMessage="Value required" ControlToValidate="txtFPOther" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtFPTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator91" runat="server" ErrorMessage="Value required" ControlToValidate="txtFPTotal" ValidationGroup="ADM" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-3 pull-right">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label><b>Grand Total</b></label>
                                                <asp:TextBox ClientIDMode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtalltotal" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdministration" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="ADM" OnClick="btnAdministration_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="Reciepts" runat="server">
                                <fieldset>
                                    <legend>RECETPTS</legend>
                                    <fieldset>
                                        <legend>10.0 FINANCIAL PERFORMANCE (conted): (Rs.)</legend>
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
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtmilksaleTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator41" runat="server" ErrorMessage="Value required" ControlToValidate="txtmilksaleTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtProductsTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator42" runat="server" ErrorMessage="Value required" ControlToValidate="txtProductsTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtTotalSaleTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator43" runat="server" ErrorMessage="Value required" ControlToValidate="txtTotalSaleTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtCommoditiesTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator44" runat="server" ErrorMessage="Value required" ControlToValidate="txtCommoditiesTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtcommditiesPurchTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator45" runat="server" ErrorMessage="Value required" ControlToValidate="txtcommditiesPurchTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtopeningStocksTR" runat="server" CssClass="form-control" ToolTip="Last Month Closing"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator46" runat="server" ErrorMessage="Value required" ControlToValidate="txtopeningStocksTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtClosingStocksTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator47" runat="server" ErrorMessage="Value required" ControlToValidate="txtClosingStocksTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtOtherIncomeTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator48" runat="server" ErrorMessage="Value required" ControlToValidate="txtOtherIncomeTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtNetRecieptsTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator49" runat="server" ErrorMessage="Value required" ControlToValidate="txtNetRecieptsTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtbeforIDATR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator50" runat="server" ErrorMessage="Value required" ControlToValidate="txtbeforIDATR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtbeforDEFERDTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator51" runat="server" ErrorMessage="Value required" ControlToValidate="txtbeforDEFERDTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtbeforDEPRECIATIONTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator52" runat="server" ErrorMessage="Value required" ControlToValidate="txtbeforDEPRECIATIONTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtNETINCLUDEPTR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator53" runat="server" ErrorMessage="Value required" ControlToValidate="txtNETINCLUDEPTR" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txttotalvarriCostTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator54" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalvarriCostTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtsalaryWages_TFCOSTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator55" runat="server" ErrorMessage="Value required" ControlToValidate="txtsalaryWages_TFCOSTTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtOthers_TFCOSTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator56" runat="server" ErrorMessage="Value required" ControlToValidate="txtOthers_TFCOSTTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txttotalfixCostTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator57" runat="server" ErrorMessage="Value required" ControlToValidate="txttotalfixCostTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtToCostTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator58" runat="server" ErrorMessage="Value required" ControlToValidate="txtToCostTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtTFcostEXCLINTTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator59" runat="server" ErrorMessage="Value required" ControlToValidate="txtTFcostEXCLINTTTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtToSaleTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator60" runat="server" ErrorMessage="Value required" ControlToValidate="txtToSaleTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtIDAOpertingProfTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator61" runat="server" ErrorMessage="Value required" ControlToValidate="txtIDAOpertingProfTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtNEtIncluIDTTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator62" runat="server" ErrorMessage="Value required" ControlToValidate="txtNEtIncluIDTTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtToSaleWithCFFTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator63" runat="server" ErrorMessage="Value required" ControlToValidate="txtToSaleWithCFFTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtOPLOssProfitCFFTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator64" runat="server" ErrorMessage="Value required" ControlToValidate="txtOPLOssProfitCFFTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onpaste="return false" ClientIDMode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" ID="txtNETprofitLossTVC" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator65" runat="server" ErrorMessage="Value required" ControlToValidate="txtNETprofitLossTVC" ValidationGroup="FPC" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </fieldset>
                                </fieldset>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnReceipt" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="FPC" OnClick="btnReceipt_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div id="CapUtilisation" runat="server">
                                <fieldset>
                                    <legend>CAPACITY UTILISATION</legend>
                                    <fieldset>
                                        <legend>11.0 CAPACITY UTILISATION IN %AGE</legend>
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
                                                            <asp:TextBox ID="txtthruoputwithoutWC_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" ErrorMessage="Value required" ControlToValidate="txtthruoputwithoutWC_PC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtthroughpuINKGS_PC" runat="server" onkeyup="CapacityUti()" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ErrorMessage="Value required" ControlToValidate="txtthroughpuINKGS_PC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtthroughpuINLTS_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ErrorMessage="Value required" ControlToValidate="txtthroughpuINLTS_PC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtthroughputPERDAY_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator32" runat="server" ErrorMessage="Value required" ControlToValidate="txtthroughputPERDAY_PC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcapacityutilisationINKGS_PC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server" ErrorMessage="Value required" ControlToValidate="txtcapacityutilisationINKGS_PC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAllCCsthruoput_CC" onkeyup="CapUtiCC()" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator34" runat="server" ErrorMessage="Value required" ControlToValidate="txtAllCCsthruoput_CC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcapacityuti_CC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server" ErrorMessage="Value required" ControlToValidate="txtcapacityuti_CC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbcfsale_BMC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator36" runat="server" ErrorMessage="Value required" ControlToValidate="txtbcfsale_BMC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbcfProdCFF_BMC" runat="server" onkeyup="CapacityUtiSMP()" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator37" runat="server" ErrorMessage="Value required" ControlToValidate="txtbcfProdCFF_BMC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcapacityuti_BMC" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator38" runat="server" ErrorMessage="Value required" ControlToValidate="txtcapacityuti_BMC" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSMPProd_SMP" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator39" runat="server" ErrorMessage="Value required" ControlToValidate="txtSMPProd_SMP" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcapacityuti_SMP" runat="server" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator40" runat="server" ErrorMessage="Value required" ControlToValidate="txtcapacityuti_SMP" ValidationGroup="CU" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </fieldset>
                                </fieldset>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnCapUtilisation" runat="server" Style="margin-top: 19px;" ValidationGroup="CU" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnCapUtilisation_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div id="materialbalancing" runat="server">
                                <fieldset>
                                    <legend>MATERIAL BALANCING</legend>
                                    <fieldset>
                                        <legend>12.0 MATERIAL BALANCING :</legend>
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
                                                    <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsmilkcow_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsmilkcow_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsmilkbuff_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsmilkbuff_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtdcsmilktotal_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ErrorMessage="Value required" ControlToValidate="txtdcsmilktotal_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfat_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ErrorMessage="Value required" ControlToValidate="txtfat_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" onkeyup="CalMB()" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnf_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="Value required" ControlToValidate="txtsnf_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatpercent_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ErrorMessage="Value required" ControlToValidate="txtfatpercent_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfpercent_MP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ErrorMessage="Value required" ControlToValidate="txtsnfpercent_MP" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtquantityinkgs_TI" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ErrorMessage="Value required" ControlToValidate="txtquantityinkgs_TI" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatinkgs_TI" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ErrorMessage="Value required" ControlToValidate="txtfatinkgs_TI" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfinkgs_TI" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ErrorMessage="Value required" ControlToValidate="txtsnfinkgs_TI" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtvalueinrs_TI" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ErrorMessage="Value required" ControlToValidate="txtvalueinrs_TI" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
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
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtquantityinkgs_TO" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ErrorMessage="Value required" ControlToValidate="txtquantityinkgs_TO" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>

                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatinkgs_TO" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ErrorMessage="Value required" ControlToValidate="txtfatinkgs_TO" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfinKgs_TO" onkeyup="CalMBInOut()" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ErrorMessage="Value required" ControlToValidate="txtsnfinKgs_TO" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtValueinrs_To" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ErrorMessage="Value required" ControlToValidate="txtValueinrs_To" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>

                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatinkgs_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ErrorMessage="Value required" ControlToValidate="txtfatinkgs_MG" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsngfinkgs_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ErrorMessage="Value required" ControlToValidate="txtsngfinkgs_MG" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtfatpercentage_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server" ErrorMessage="Value required" ControlToValidate="txtfatpercentage_MG" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox onpaste="return false" ClientIDMode="Static" oninput="validate(this)" autocomplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" ID="txtsnfpercentage_MG" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" ErrorMessage="Value required" ControlToValidate="txtsnfpercentage_MG" ValidationGroup="MB" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </fieldset>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <asp:Button ID="btnmaterialbalancing" runat="server" Style="margin-top: 19px;" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnmaterialbalancing_Click" ValidationGroup="MB"></asp:Button>
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

        function noDays() {
            let daysInMonth = '0';
            let month = document.getElementById('<%=ddlmonth.ClientID %>').value;
            let year = document.getElementById('<%=ddlYear.ClientID %>').value;
            if (month != '0' & year != '0')
                daysInMonth = new Date(year, month, 0).getDate();
            return daysInMonth;

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
            if (month != '0' & year != '0')
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
            let tolt = parseFloat(DCSo) - parseFloat(DCSf).toFixed(2);
            if (isNaN(tolt))
                tolt = "0";
            document.getElementById('<%=txtdcsclosedtemp.ClientID %>').value = tolt;
            document.getElementById('<%=txtdcsselingbcf.ClientID %>').value = tolt;
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

            let totalcostAiactivites = getSum(document.getElementById('<%=txtsalaryandwagesAiActivites.ClientID %>').value,
                                                                                document.getElementById('<%=txttransportAiActivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtLn2ConsumedAiAcitivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtLn2transportAiactivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtsemenandstrawesAiactivites.ClientID %>').value,
                                                                                document.getElementById('<%=txtotherdirectcostAiactivites.ClientID %>').value);
            let lessincomeAiactivites = document.getElementById('<%=txtlessincomeAiactivites.ClientID %>').value;
            if (totalcostAiactivites === "")
                totalcostAiactivites = "0";
            if (totalcostAiactivites === ".")
                totalcostAiactivites = "0";
            if (lessincomeAiactivites === "")
                lessincomeAiactivites = "0";
            if (lessincomeAiactivites === ".")
                lessincomeAiactivites = "0";
            let tttotalcostAiactivites = (parseFloat(totalcostAiactivites) - parseFloat(lessincomeAiactivites)).toFixed(2);
            if (tttotalcostAiactivites === "")
                tttotalcostAiactivites = "0";
            if (tttotalcostAiactivites === ".")
                tttotalcostAiactivites = "0";
            document.getElementById('<%=txttotalcostAiactivites.ClientID %>').value = tttotalcostAiactivites;


            document.getElementById('<%=txtFPCtotalcost.ClientID %>').value = getSum(document.getElementById('<%=txtFPCsalryandwages.ClientID %>').value,
                                                                                document.getElementById('<%=txtFPCotherdirectcost.ClientID %>').value);
            document.getElementById('<%=txtAHCtotalCost.ClientID %>').value = getSum(document.getElementById('<%=txtAHCsalaryandwages.ClientID %>').value,
                                                                                document.getElementById('<%=txtAHCotherdirectcost.ClientID %>').value);

            let tTotalCost = getSum(document.getElementById('<%=txtTEcostsalaryandwages.ClientID %>').value,
                                                                                document.getElementById('<%=txtTEcostotherdirectcost.ClientID %>').value);
            let tTEcostlessincome = document.getElementById('<%=txtTEcostlessincome.ClientID %>').value;
            if (tTotalCost === "")
                tTotalCost = "0";
            if (tTotalCost === ".")
                tTotalCost = "0";
            if (tTEcostlessincome === "")
                tTEcostlessincome = "0";
            if (tTEcostlessincome === ".")
                tTEcostlessincome = "0";
            let txTEcostlessincome = (parseFloat(tTotalCost) - parseFloat(tTEcostlessincome)).toFixed(2);
            if (txTEcostlessincome === "")
                txTEcostlessincome = "0";
            if (txTEcostlessincome === ".")
                txTEcostlessincome = "0";
            document.getElementById('<%=txtTotalCost.ClientID %>').value = txTEcostlessincome;
            //====================================================================================
            let ttotalcostOTI = getSum(document.getElementById('<%=txtsalaryandwagesOTI.ClientID %>').value,
                                document.getElementById('<%=txtotherincmecostOTI.ClientID %>').value);
            let tOIcostlessincome = document.getElementById('<%=txtOIcostlessincome.ClientID %>').value;
            if (ttotalcostOTI === "")
                ttotalcostOTI = "0";
            if (ttotalcostOTI === ".")
                ttotalcostOTI = "0";
            if (tOIcostlessincome === "")
                tOIcostlessincome = "0";
            if (tOIcostlessincome === ".")
                tOIcostlessincome = "0";
            let tttotalcostOTI = (parseFloat(ttotalcostOTI) - parseFloat(tOIcostlessincome)).toFixed(2);
            if (tttotalcostOTI === ".")
                tttotalcostOTI = "0";
            document.getElementById('<%=txttotalcostOTI.ClientID %>').value = tttotalcostOTI;
            //===============================================
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
        function MPKGPD() {

            let MILKPROC_monthly = getSum(document.getElementById('<%=txtDCSmilkRMRD.ClientID %>').value,
                                          document.getElementById('<%=txtDCSmilkCCS.ClientID %>').value,
                                          document.getElementById('<%=txtOTHER.ClientID %>').value)
            let totalMILKPROC_monthly = (parseFloat(MILKPROC_monthly) / parseFloat(noofDays())).toFixed(2);
            document.getElementById('<%=txtMILKPROC_monthly.ClientID %>').value = totalMILKPROC_monthly;

            let hfTotalMP = document.getElementById('<%=hfTotalMP.ClientID %>').value;
            let hidallDay = document.getElementById('<%=hfDay.ClientID %>').value;

            if (hfTotalMP === "")
                hfTotalMP = "0";
            if (hfTotalMP === ".")
                hfTotalMP = "0";
            if (hidallDay === "")
                hidallDay = "0";
            if (hidallDay === ".")
                hidallDay = "0";

            let hidfTotal = ((parseFloat(hfTotalMP) + parseFloat(MILKPROC_monthly)) / parseFloat(hidallDay)).toFixed(2);
            if (isNaN(hidfTotal))
                hidfTotal = "0";
            document.getElementById('<%=txtMILKPROC_Cummulat.ClientID %>').value = hidfTotal;


        }
        function MPcalc() {

            document.getElementById('<%=txttotalMilkProc.ClientID %>').value = getSum(document.getElementById('<%=txtDCSmilkRMRD.ClientID %>').value,
                                                                                       document.getElementById('<%=txtDCSmilkCCS.ClientID %>').value,
                                                                                       document.getElementById('<%=txtSMGMILK.ClientID %>').value,
                                                                                       document.getElementById('<%=txtNMGmilk.ClientID %>').value,
                                                                                       document.getElementById('<%=txtOTHER.ClientID %>').value);
            MPKGPD();
        }
        function LMScalc() {

            let totalmilksale = getSum(document.getElementById('<%=txtwholemilk.ClientID %>').value,
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
            document.getElementById('<%=txttotalmilksale.ClientID %>').value = totalmilksale;
            let LocalMILK_MOnthly = (parseFloat(totalmilksale) / parseFloat(noofDays())).toFixed(2);
            if (isNaN(LocalMILK_MOnthly))
                LocalMILK_MOnthly = "0";

            document.getElementById('<%=txtLocalMILK_MOnthly.ClientID %>').value = LocalMILK_MOnthly;
            let Hideftotalmilksale = document.getElementById('<%=hftotalmilksale.ClientID %>').value;
            let hfallDay = document.getElementById('<%=hfDay.ClientID %>').value;
            if (Hideftotalmilksale === "")
                Hideftotalmilksale = "0";
            if (Hideftotalmilksale === ".")
                Hideftotalmilksale = "0";
            if (hfallDay === "")
                hfallDay = "0";
            if (hfallDay === ".")
                hfallDay = "0";
            let hfTotal = ((parseFloat(Hideftotalmilksale) + parseFloat(totalmilksale)) / parseFloat(hfallDay)).toFixed(2);
            if (isNaN(hfTotal))
                hfTotal = "0";
            document.getElementById('<%=txtLocalMilk_Cummulat.ClientID %>').value = hfTotal;

        }
        //============   SMG   ==========================================================
        function SMGHideField(totalsmg) {
            let SMGmilk_Monthly = (parseFloat(totalsmg) / parseFloat(noofDays())).toFixed(2);
            if (isNaN(SMGmilk_Monthly))
                SMGmilk_Monthly = "0";
            document.getElementById('<%=txtSMGmilk_Monthly.ClientID %>').value = SMGmilk_Monthly;

            let hidfSMS_TotalSMGSale = document.getElementById('<%=hfSMS_TotalSMGSale.ClientID %>').value;
            let hidfallDay = document.getElementById('<%=hfDay.ClientID %>').value;
            if (hidfSMS_TotalSMGSale === "")
                hidfSMS_TotalSMGSale = "0";
            if (hidfSMS_TotalSMGSale === ".")
                hidfSMS_TotalSMGSale = "0";
            if (hidfallDay === "")
                hidfallDay = "0";
            if (hidfallDay === ".")
                hidfallDay = "0";
            let SMGmilk_Cummulat = ((parseFloat(totalsmg) + parseFloat(hidfSMS_TotalSMGSale)) / parseFloat(hidfallDay)).toFixed(2);
            if (isNaN(SMGmilk_Cummulat))
                SMGmilk_Cummulat = "0";
            document.getElementById('<%=txtSMGmilk_Cummulat.ClientID %>').value = SMGmilk_Cummulat;
        }
        function SMGcalc() {
            let wholemilk_SMG = document.getElementById('<%=txtwholemilk_SMG.ClientID %>').value;
            let skimmilk_SMG = document.getElementById('<%=txtskimmilk_SMG.ClientID %>').value;
            let txtOther_SMG = document.getElementById('<%=txtOther_SMG.ClientID %>').value;
            if (wholemilk_SMG === "")
                wholemilk_SMG = "0";
            if (wholemilk_SMG === ".")
                wholemilk_SMG = "0";
            let totalwholemilk_SMG = (parseFloat(wholemilk_SMG) / 1.03).toFixed(2);
            if (totalwholemilk_SMG === "")
                totalwholemilk_SMG = "0";
            if (totalwholemilk_SMG === ".")
                totalwholemilk_SMG = "0";
            document.getElementById('<%=txtwholemilk_SMG.ClientID %>').value = totalwholemilk_SMG;
            let Totalsmgsale_SMG = getSum(totalwholemilk_SMG, skimmilk_SMG, txtOther_SMG)
            document.getElementById('<%=txtTotalsmgsale_SMG.ClientID %>').value = Totalsmgsale_SMG;
            SMGHideField(Totalsmgsale_SMG);
        }
        function SMGcalc1() {
            let wholemilk_SMG1 = document.getElementById('<%=txtwholemilk_SMG.ClientID %>').value;
            let skimmilk_SMG1 = document.getElementById('<%=txtskimmilk_SMG.ClientID %>').value;
            let txtOther_SMG1 = document.getElementById('<%=txtOther_SMG.ClientID %>').value;
            if (skimmilk_SMG1 === "")
                skimmilk_SMG1 = "0";
            if (skimmilk_SMG1 === ".")
                skimmilk_SMG1 = "0";
            let totlskimmilk_SMG = (parseFloat(skimmilk_SMG1) / 1.03).toFixed(2);
            if (totlskimmilk_SMG === "")
                totlskimmilk_SMG = "0";
            if (totlskimmilk_SMG === ".")
                totlskimmilk_SMG = "0";
            document.getElementById('<%=txtskimmilk_SMG.ClientID %>').value = totlskimmilk_SMG;
            let Totalsmgsale_SMG1 = getSum(wholemilk_SMG1, totlskimmilk_SMG, txtOther_SMG1);
            document.getElementById('<%=txtTotalsmgsale_SMG.ClientID %>').value = Totalsmgsale_SMG1;
            SMGHideField(Totalsmgsale_SMG1);
        }
        function SMGcalc2() {
            let wholemilk_SMG2 = document.getElementById('<%=txtwholemilk_SMG.ClientID %>').value;
            let skimmilk_SMG2 = document.getElementById('<%=txtskimmilk_SMG.ClientID %>').value;
            let txtOther_SMG2 = document.getElementById('<%=txtOther_SMG.ClientID %>').value;
            if (txtOther_SMG2 === "")
                txtOther_SMG2 = "0";
            if (txtOther_SMG2 === ".")
                txtOther_SMG2 = "0";
            let totltxtOther_SMG2 = (parseFloat(txtOther_SMG2) / 1.03).toFixed(2);
            if (totltxtOther_SMG2 === "")
                totltxtOther_SMG2 = "0";
            if (totltxtOther_SMG2 === ".")
                totltxtOther_SMG2 = "0";
            document.getElementById('<%=txtOther_SMG.ClientID %>').value = totltxtOther_SMG2;
            let Totalsmgsale_SMG2 = getSum(wholemilk_SMG2, skimmilk_SMG2, totltxtOther_SMG2);
            document.getElementById('<%=txtTotalsmgsale_SMG.ClientID %>').value = Totalsmgsale_SMG2;
            SMGHideField(Totalsmgsale_SMG2);
        }
        //===================  NMG  =================================================================
        function NMGHideField() {
            let Total_NMS_OS = getSum(document.getElementById('<%=txtTotalNMGsale_NMG.ClientID %>').value,
                               document.getElementById('<%=txtwholmilkinLit_OSALE.ClientID %>').value,
                               document.getElementById('<%=txtskimmilkinLit_OSALE.ClientID %>').value);

            let NMGOTH_MOnthly = (parseFloat(Total_NMS_OS) / parseFloat(noofDays())).toFixed(2);
            if (isNaN(NMGOTH_MOnthly))
                NMGOTH_MOnthly = "0";
            document.getElementById('<%=txtNMGOTH_MOnthly.ClientID %>').value = NMGOTH_MOnthly;

            let hidfTotal_NMS_OS = document.getElementById('<%=hfTotal_NMS_OS.ClientID %>').value;
            let hidefallDay = document.getElementById('<%=hfDay.ClientID %>').value;
            if (hidfTotal_NMS_OS === "")
                hidfTotal_NMS_OS = "0";
            if (hidfTotal_NMS_OS === ".")
                hidfTotal_NMS_OS = "0";
            if (hidefallDay === "")
                hidefallDay = "0";
            if (hidefallDay === ".")
                hidefallDay = "0";
            let NMGOTH_Cummulat = ((parseFloat(Total_NMS_OS) + parseFloat(hidfTotal_NMS_OS)) / parseFloat(hidefallDay)).toFixed(2);
            if (isNaN(NMGOTH_Cummulat))
                NMGOTH_Cummulat = "0";
            document.getElementById('<%=txtNMGOTH_Cummulat.ClientID %>').value = NMGOTH_Cummulat;
        }
        function NMGcalc() {
            let wholemilk_NMG = document.getElementById('<%=txtwholemilk_NMG.ClientID %>').value;
            let txtskimmilk_NMG = document.getElementById('<%=txtskimmilk_NMG.ClientID %>').value;
            let txtOther_NMG = document.getElementById('<%=txtOther_NMG.ClientID %>').value;
            if (wholemilk_NMG === "")
                wholemilk_NMG = "0";
            if (wholemilk_NMG === ".")
                wholemilk_NMG = "0";
            let totlwholemilk_NMG = (parseFloat(wholemilk_NMG) / 1.03).toFixed(2);
            if (totlwholemilk_NMG === "")
                totlwholemilk_NMG = "0";
            if (totlwholemilk_NMG === ".")
                totlwholemilk_NMG = "0";
            document.getElementById('<%=txtwholemilk_NMG.ClientID %>').value = totlwholemilk_NMG;
            document.getElementById('<%=txtTotalNMGsale_NMG.ClientID %>').value = getSum(totlwholemilk_NMG, txtskimmilk_NMG, txtOther_NMG);
            NMGHideField();
        }
        function NMGcalc1() {
            let wholemilk_NMG1 = document.getElementById('<%=txtwholemilk_NMG.ClientID %>').value;
            let txtskimmilk_NMG1 = document.getElementById('<%=txtskimmilk_NMG.ClientID %>').value;
            let txtOther_NMG1 = document.getElementById('<%=txtOther_NMG.ClientID %>').value;
            if (txtskimmilk_NMG1 === "")
                txtskimmilk_NMG1 = "0";
            if (txtskimmilk_NMG1 === ".")
                wholemilk_NMG = "0";
            let totltxtskimmilk_NMG1 = (parseFloat(txtskimmilk_NMG1) / 1.03).toFixed(2);
            if (totltxtskimmilk_NMG1 === "")
                totltxtskimmilk_NMG1 = "0";
            if (totltxtskimmilk_NMG1 === ".")
                totltxtskimmilk_NMG1 = "0";
            document.getElementById('<%=txtskimmilk_NMG.ClientID %>').value = totltxtskimmilk_NMG1;
            document.getElementById('<%=txtTotalNMGsale_NMG.ClientID %>').value = getSum(wholemilk_NMG1, totltxtskimmilk_NMG1, txtOther_NMG1);
            NMGHideField();
        }
        function NMGcalc2() {
            let wholemilk_NMG2 = document.getElementById('<%=txtwholemilk_NMG.ClientID %>').value;
            let txtskimmilk_NMG2 = document.getElementById('<%=txtskimmilk_NMG.ClientID %>').value;
            let txtOther_NMG2 = document.getElementById('<%=txtOther_NMG.ClientID %>').value;
            if (txtOther_NMG2 === "")
                txtOther_NMG2 = "0";
            if (txtOther_NMG2 === ".")
                txtOther_NMG2 = "0";
            let totltxtOther_NMG2 = (parseFloat(txtOther_NMG2) / 1.03).toFixed(2);
            if (totltxtOther_NMG2 === "")
                totltxtOther_NMG2 = "0";
            if (totltxtOther_NMG2 === ".")
                totltxtOther_NMG2 = "0";
            document.getElementById('<%=txtOther_NMG.ClientID %>').value = totltxtOther_NMG2;
            document.getElementById('<%=txtTotalNMGsale_NMG.ClientID %>').value = getSum(wholemilk_NMG2, txtskimmilk_NMG2, totltxtOther_NMG2);
            NMGHideField();
        }
        //===============  OS  ======================================================================================================
        function OScalc() {
            let wholmilkinLit_OSALE = document.getElementById('<%=txtwholmilkinLit_OSALE.ClientID %>').value;
            let skimmilkinLit_OSALE = document.getElementById('<%=txtskimmilkinLit_OSALE.ClientID %>').value;
            let Other_OSALE = document.getElementById('<%=txtOther_OSALE.ClientID %>').value;

            if (wholmilkinLit_OSALE === "")
                wholmilkinLit_OSALE = "0";
            if (wholmilkinLit_OSALE === ".")
                wholmilkinLit_OSALE = "0";
            let totlwholmilkinLit_OSALE = (parseFloat(wholmilkinLit_OSALE) / 1.03).toFixed(2);
            if (totlwholmilkinLit_OSALE === "")
                totlwholmilkinLit_OSALE = "0";
            if (totlwholmilkinLit_OSALE === ".")
                totlwholmilkinLit_OSALE = "0";
            document.getElementById('<%=txtwholmilkinLit_OSALE.ClientID %>').value = totlwholmilkinLit_OSALE;
            document.getElementById('<%=txttotalBulkSale_OSALE.ClientID %>').value = getSum(totlwholmilkinLit_OSALE, skimmilkinLit_OSALE, Other_OSALE);
            NMGHideField();
        }
        function OScalc1() {
            let wholmilkinLit_OSALE1 = document.getElementById('<%=txtwholmilkinLit_OSALE.ClientID %>').value;
            let skimmilkinLit_OSALE1 = document.getElementById('<%=txtskimmilkinLit_OSALE.ClientID %>').value;
            let Other_OSALE1 = document.getElementById('<%=txtOther_OSALE.ClientID %>').value;

            if (skimmilkinLit_OSALE1 === "")
                skimmilkinLit_OSALE1 = "0";
            if (skimmilkinLit_OSALE1 === ".")
                skimmilkinLit_OSALE1 = "0";
            let totlskimmilkinLit_OSALE1 = (parseFloat(skimmilkinLit_OSALE1) / 1.03).toFixed(2);
            if (totlskimmilkinLit_OSALE1 === "")
                totlskimmilkinLit_OSALE1 = "0";
            if (totlskimmilkinLit_OSALE1 === ".")
                totlskimmilkinLit_OSALE1 = "0";
            document.getElementById('<%=txtskimmilkinLit_OSALE.ClientID %>').value = totlskimmilkinLit_OSALE1;
            document.getElementById('<%=txttotalBulkSale_OSALE.ClientID %>').value = getSum(wholmilkinLit_OSALE1, totlskimmilkinLit_OSALE1, Other_OSALE1);
            NMGHideField();
        }
        function OScalc2() {
            let wholmilkinLit_OSALE2 = document.getElementById('<%=txtwholmilkinLit_OSALE.ClientID %>').value;
            let skimmilkinLit_OSALE2 = document.getElementById('<%=txtskimmilkinLit_OSALE.ClientID %>').value;
            let Other_OSALE2 = document.getElementById('<%=txtOther_OSALE.ClientID %>').value;

            if (Other_OSALE2 === "")
                Other_OSALE2 = "0";
            if (Other_OSALE2 === ".")
                Other_OSALE2 = "0";
            let totlOther_OSALE2 = (parseFloat(Other_OSALE2) / 1.03).toFixed(2);
            if (totlOther_OSALE2 === "")
                totlOther_OSALE2 = "0";
            if (totlOther_OSALE2 === ".")
                totlOther_OSALE2 = "0";
            document.getElementById('<%=txtOther_OSALE.ClientID %>').value = totlOther_OSALE2;
            document.getElementById('<%=txttotalBulkSale_OSALE.ClientID %>').value = getSum(wholmilkinLit_OSALE2, skimmilkinLit_OSALE2, totlOther_OSALE2);
        }
        //==============================================================================
        function GMPcalc() {
            noofDays();
            let hidfMP_TillMonthGMPMS = document.getElementById('<%=hfMP_TillMonthGMPMS.ClientID %>').value;
            let hidfLMS_TillMonthGMPMS = document.getElementById('<%=hfLMS_TillMonthGMPMS.ClientID %>').value;
            let hidfallDay = document.getElementById('<%=hfDay.ClientID %>').value;
            if (hidfMP_TillMonthGMPMS === "")
                hidfMP_TillMonthGMPMS = "0";
            if (hidfMP_TillMonthGMPMS === ".")
                hidfMP_TillMonthGMPMS = "0";
            if (hidfLMS_TillMonthGMPMS === "")
                hidfLMS_TillMonthGMPMS = "0";
            if (hidfLMS_TillMonthGMPMS === ".")
                hidfLMS_TillMonthGMPMS = "0";
            let MILKproKGPD_KG_Monthly = document.getElementById('<%=txtMILKproKGPD_KG_Monthly.ClientID %>').value;
            if (MILKproKGPD_KG_Monthly === "")
                MILKproKGPD_KG_Monthly = "0";
            if (MILKproKGPD_KG_Monthly === ".")
                MILKproKGPD_KG_Monthly = "0";
            let MILKproKGPD_Monthly = (parseFloat(MILKproKGPD_KG_Monthly) / parseFloat(noofDays())).toFixed(2);
            if (isNaN(MILKproKGPD_Monthly))
                MILKproKGPD_Monthly = "0";
            document.getElementById('<%=txtMILKproKGPD_Monthly.ClientID %>').value = MILKproKGPD_Monthly;

            let MILKprocKGPD_Cummulative = ((parseFloat(MILKproKGPD_KG_Monthly) + parseFloat(hidfMP_TillMonthGMPMS)) / parseFloat(hidfallDay)).toFixed(2);
            if (isNaN(MILKprocKGPD_Cummulative))
                MILKprocKGPD_Cummulative = "0";
            document.getElementById('<%=txtMILKprocKGPD_Cummulative.ClientID %>').value = MILKprocKGPD_Cummulative;

            let LocalMILKLPD_Ltr_Monthly = document.getElementById('<%=txtLocalMILKLPD_Ltr_Monthly.ClientID %>').value;
            if (LocalMILKLPD_Ltr_Monthly === "")
                LocalMILKLPD_Ltr_Monthly = "0";
            if (LocalMILKLPD_Ltr_Monthly === ".")
                LocalMILKLPD_Ltr_Monthly = "0";
            let LocalMILKLPD_Monthly = (parseFloat(LocalMILKLPD_Ltr_Monthly) / parseFloat(noofDays())).toFixed(2);
            if (isNaN(LocalMILKLPD_Monthly))
                LocalMILKLPD_Monthly = "0";
            document.getElementById('<%=txtLocalMILKLPD_Monthly.ClientID %>').value = LocalMILKLPD_Monthly;

            let LocalmilkLPD_Cummulative = ((parseFloat(LocalMILKLPD_Ltr_Monthly) + parseFloat(hidfLMS_TillMonthGMPMS)) / parseFloat(hidfallDay)).toFixed(2);
            if (isNaN(LocalmilkLPD_Cummulative))
                LocalmilkLPD_Cummulative = "0";
            document.getElementById('<%=txtLocalmilkLPD_Cummulative.ClientID %>').value = LocalmilkLPD_Cummulative;



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
        //=============Raw material cost================
        function rowmatecost() {

            document.getElementById('<%=txtTotalRMC.ClientID %>').value = getSum(document.getElementById('<%=txtDCSMilkRMC.ClientID %>').value,
                                                                                   document.getElementById('<%=txtSMGMilkRMC.ClientID %>').value,
                                                                                  document.getElementById('<%=txtNMGMilkRMC.ClientID %>').value,
                                                                                  document.getElementById('<%=txtOtherMilkRMC.ClientID %>').value,
                                                                                    document.getElementById('<%=txtCOMMUsedRMC.ClientID %>').value);
            document.getElementById('<%=txtTotalPT.ClientID %>').value = getSum(document.getElementById('<%=txtDCSdairyCCimc.ClientID %>').value,
                                                                                        document.getElementById('<%=txtccIMCtoDAIRYpt.ClientID %>').value,
                                                                                        document.getElementById('<%=txtSMGMILKPT.ClientID %>').value,
                                                                                        document.getElementById('<%=txtNMGMILKpt.ClientID %>').value);
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

        function noofDays() {
            let daysInMonth = '0';
            let month = document.getElementById('<%=ddlmonth.ClientID %>').value;
            let year = document.getElementById('<%=ddlYear.ClientID %>').value;
            if (month != '0' & year != '0')
                daysInMonth = new Date(year, month, 0).getDate();
            return daysInMonth;

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

