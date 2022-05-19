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
                <asp:label id="lblMsg" runat="server"></asp:label>
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
                                            <asp:dropdownlist id="ddlYear" runat="server" cssclass="form-control" autopostback="true" onselectedindexchanged="ddlform_SelectedIndexChanged"></asp:dropdownlist>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Month <span style="color: red;">*</span></label>
                                            <asp:dropdownlist id="ddlmonth" runat="server" cssclass="form-control" autopostback="true" onselectedindexchanged="ddlform_SelectedIndexChanged">
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
                                            </asp:dropdownlist>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Entry Form <span style="color: red;">*</span></label>
                                            <asp:dropdownlist id="ddlform" runat="server" cssclass="form-control" autopostback="true" onselectedindexchanged="ddlform_SelectedIndexChanged">
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
                                            </asp:dropdownlist>
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
                                                                <asp:textbox clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtNOofroutes" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator268" runat="server" errormessage="Value required" controltovalidate="txtNOofroutes" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOsubFOM()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsorgnised" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator269" runat="server" errormessage="Value required" controltovalidate="txtdcsorgnised" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOsubFOM()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsfunctional" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator270" runat="server" errormessage="Value required" controltovalidate="txtdcsfunctional" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsclosedtemp" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator271" runat="server" errormessage="Value required" controltovalidate="txtdcsclosedtemp" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtnewdcsorganisedmonth" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator272" runat="server" errormessage="Value required" controltovalidate="txtnewdcsorganisedmonth" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtnewdcsregisteredmonth" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator273" runat="server" errormessage="Value required" controltovalidate="txtnewdcsregisteredmonth" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsrevivedmonth" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator274" runat="server" errormessage="Value required" controltovalidate="txtdcsrevivedmonth" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcolesdmonth" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator275" runat="server" errormessage="Value required" controltovalidate="txtcolesdmonth" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtGeneral" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator276" runat="server" errormessage="Value required" controltovalidate="txtGeneral" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtSceduledcaste" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator277" runat="server" errormessage="Value required" controltovalidate="txtSceduledcaste" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtscheduletribe" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator278" runat="server" errormessage="Value required" controltovalidate="txtscheduletribe" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtbackworsclasses" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator279" runat="server" errormessage="Value required" controltovalidate="txtbackworsclasses" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtmembershiptotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator280" runat="server" errormessage="Value required" controltovalidate="txtmembershiptotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtlandlesslabour" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator281" runat="server" errormessage="Value required" controltovalidate="txtlandlesslabour" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtmarginalfarmer" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator282" runat="server" errormessage="Value required" controltovalidate="txtmarginalfarmer" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsmallfarmer" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator283" runat="server" errormessage="Value required" controltovalidate="txtsmallfarmer" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtlargefarmer" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator284" runat="server" errormessage="Value required" controltovalidate="txtlargefarmer" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtothers" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator285" runat="server" errormessage="Value required" controltovalidate="txtothers" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator286" runat="server" errormessage="Value required" controltovalidate="txttotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfemalGeneral" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator287" runat="server" errormessage="Value required" controltovalidate="txtfemalGeneral" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtScheCastfemale" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator288" runat="server" errormessage="Value required" controltovalidate="txtScheCastfemale" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtschedtribfemale" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator289" runat="server" errormessage="Value required" controltovalidate="txtschedtribfemale" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtotherbackwordfemale" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator290" runat="server" errormessage="Value required" controltovalidate="txtotherbackwordfemale" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttotalfemale" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator291" runat="server" errormessage="Value required" controltovalidate="txttotalfemale" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtoldDues" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator292" runat="server" errormessage="Value required" controltovalidate="txtoldDues" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcurrentDues" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator293" runat="server" errormessage="Value required" controltovalidate="txtcurrentDues" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttoalDues" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator294" runat="server" errormessage="Value required" controltovalidate="txttoalDues" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtgenralFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator295" runat="server" errormessage="Value required" controltovalidate="txtgenralFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtschedulcasteFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator296" runat="server" errormessage="Value required" controltovalidate="txtschedulcasteFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtscheduletribeFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator297" runat="server" errormessage="Value required" controltovalidate="txtscheduletribeFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtbackwordFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator298" runat="server" errormessage="Value required" controltovalidate="txtbackwordFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotalFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator299" runat="server" errormessage="Value required" controltovalidate="txtTotalFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtlandlesslaboueFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator300" runat="server" errormessage="Value required" controltovalidate="txtlandlesslaboueFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtmarinalfarmerFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator301" runat="server" errormessage="Value required" controltovalidate="txtmarinalfarmerFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsmallfarmerFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator302" runat="server" errormessage="Value required" controltovalidate="txtsmallfarmerFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtlargefarmerFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator303" runat="server" errormessage="Value required" controltovalidate="txtlargefarmerFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOthersFun" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator304" runat="server" errormessage="Value required" controltovalidate="txtOthersFun" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFunTotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator305" runat="server" errormessage="Value required" controltovalidate="txtFunTotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfemalegeneral" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator306" runat="server" errormessage="Value required" controltovalidate="txtfemalegeneral" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfemaleschedulcaste" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator307" runat="server" errormessage="Value required" controltovalidate="txtfemaleschedulcaste" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfemaletribe" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator308" runat="server" errormessage="Value required" controltovalidate="txtfemaletribe" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfemalebackword" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator309" runat="server" errormessage="Value required" controltovalidate="txtfemalebackword" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfemaletotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator310" runat="server" errormessage="Value required" controltovalidate="txtfemaletotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtmembers" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator311" runat="server" errormessage="Value required" controltovalidate="txtmembers" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtnonmerbers" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator312" runat="server" errormessage="Value required" controltovalidate="txtnonmerbers" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttotalPourers" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator313" runat="server" errormessage="Value required" controltovalidate="txttotalPourers" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIcentersingle" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator314" runat="server" errormessage="Value required" controltovalidate="txtAIcentersingle" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIcentercluster" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator315" runat="server" errormessage="Value required" controltovalidate="txtAIcentercluster" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIcentertotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator316" runat="server" errormessage="Value required" controltovalidate="txtAIcentertotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIperformSinglecow" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator317" runat="server" errormessage="Value required" controltovalidate="txtAIperformSinglecow" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIperformBuff1" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator318" runat="server" errormessage="Value required" controltovalidate="txtAIperformBuff1" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIperformclustercow" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator319" runat="server" errormessage="Value required" controltovalidate="txtAIperformclustercow" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAIPerformBuff2" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator320" runat="server" errormessage="Value required" controltovalidate="txtAIPerformBuff2" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAItotalCow" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator321" runat="server" errormessage="Value required" controltovalidate="txtAItotalCow" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAItotalBuff" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator322" runat="server" errormessage="Value required" controltovalidate="txtAItotalBuff" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                </td>
                                                                <td>
                                                                    <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAiperformedtotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                    <asp:regularexpressionvalidator id="RegularExpressionValidator323" runat="server" errormessage="Value required" controltovalidate="txtAiperformedtotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcalvborntotalcow" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator324" runat="server" errormessage="Value required" controltovalidate="txtcalvborntotalcow" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcalvbornbuff" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator325" runat="server" errormessage="Value required" controltovalidate="txtcalvbornbuff" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtanimalhusfirstAid" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator326" runat="server" errormessage="Value required" controltovalidate="txtanimalhusfirstAid" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtaniamlhusAHWcase" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator327" runat="server" errormessage="Value required" controltovalidate="txtaniamlhusAHWcase" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcattlefiedsold" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator328" runat="server" errormessage="Value required" controltovalidate="txtcattlefiedsold" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsselingbcf" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator329" runat="server" errormessage="Value required" controltovalidate="txtdcsselingbcf" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtmmsalebydcs" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator330" runat="server" errormessage="Value required" controltovalidate="txtmmsalebydcs" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcattinducproject" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator331" runat="server" errormessage="Value required" controltovalidate="txtcattinducproject" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcattinducselffinance" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator332" runat="server" errormessage="Value required" controltovalidate="txtcattinducselffinance" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcattleinductotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator333" runat="server" errormessage="Value required" controltovalidate="txtcattleinductotal" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalarywagesPAC" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator334" runat="server" errormessage="Value required" controltovalidate="txtsalarywagesPAC" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTaandtransportPAC" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator335" runat="server" errormessage="Value required" controltovalidate="txtTaandtransportPAC" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtcontractlabourPAC" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator336" runat="server" errormessage="Value required" controltovalidate="txtcontractlabourPAC" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtotherexpansesPAC" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator337" runat="server" errormessage="Value required" controltovalidate="txtotherexpansesPAC" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttotalPAC" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator338" runat="server" errormessage="Value required" controltovalidate="txttotalPAC" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalaryandwagesAiActivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator339" runat="server" errormessage="Value required" controltovalidate="txtsalaryandwagesAiActivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttransportAiActivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator340" runat="server" errormessage="Value required" controltovalidate="txttransportAiActivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtLn2ConsumedAiAcitivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator341" runat="server" errormessage="Value required" controltovalidate="txtLn2ConsumedAiAcitivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtLn2transportAiactivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator342" runat="server" errormessage="Value required" controltovalidate="txtLn2transportAiactivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsemenandstrawesAiactivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator343" runat="server" errormessage="Value required" controltovalidate="txtsemenandstrawesAiactivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtotherdirectcostAiactivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator344" runat="server" errormessage="Value required" controltovalidate="txtotherdirectcostAiactivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtlessincomeAiactivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator345" runat="server" errormessage="Value required" controltovalidate="txtlessincomeAiactivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttotalcostAiactivites" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator346" runat="server" errormessage="Value required" controltovalidate="txttotalcostAiactivites" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAHCsalaryandwages" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator347" runat="server" errormessage="Value required" controltovalidate="txtAHCsalaryandwages" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAHCotherdirectcost" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator348" runat="server" errormessage="Value required" controltovalidate="txtAHCotherdirectcost" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAHCtotalCost" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator349" runat="server" errormessage="Value required" controltovalidate="txtAHCtotalCost" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFPCsalryandwages" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator350" runat="server" errormessage="Value required" controltovalidate="txtFPCsalryandwages" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFPCotherdirectcost" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator351" runat="server" errormessage="Value required" controltovalidate="txtFPCotherdirectcost" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFPCtotalcost" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator352" runat="server" errormessage="Value required" controltovalidate="txtFPCtotalcost" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTEcostsalaryandwages" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator353" runat="server" errormessage="Value required" controltovalidate="txtTEcostsalaryandwages" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTEcostotherdirectcost" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator354" runat="server" errormessage="Value required" controltovalidate="txtTEcostotherdirectcost" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTEcostlessincome" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator355" runat="server" errormessage="Value required" controltovalidate="txtTEcostlessincome" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotalCost" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator356" runat="server" errormessage="Value required" controltovalidate="txtTotalCost" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalaryandwagesOTI" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator357" runat="server" errormessage="Value required" controltovalidate="txtsalaryandwagesOTI" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtotherincmecostOTI" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator358" runat="server" errormessage="Value required" controltovalidate="txtotherincmecostOTI" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOIcostlessincome" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator359" runat="server" errormessage="Value required" controltovalidate="txtOIcostlessincome" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttotalcostOTI" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator360" runat="server" errormessage="Value required" controltovalidate="txttotalcostOTI" validationgroup="FO" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                <asp:textbox clientidmode="Static" onkeyup="FOcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFOGrandTotal" runat="server" cssclass="form-control"></asp:textbox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:button id="btnFO" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="FO" onclick="btnFO_Click"></asp:button>
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
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator222" runat="server" errormessage="Value required" controltovalidate="txtDCSmilkRMRD" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDCSmilkCCS" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator223" runat="server" errormessage="Value required" controltovalidate="txtDCSmilkCCS" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGMILK" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator224" runat="server" errormessage="Value required" controltovalidate="txtSMGMILK" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGmilk" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator225" runat="server" errormessage="Value required" controltovalidate="txtNMGmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOTHER" onpaste="return false" ClientIDMode="Static" onkeyup="MPcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator226" runat="server" errormessage="Value required" controltovalidate="txtOTHER" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID = "hfTotalMP" runat = "server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalMilkProc" onpaste="return false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                           <asp:regularexpressionvalidator id="RegularExpressionValidator227" runat="server" errormessage="Value required" controltovalidate="txttotalMilkProc" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                           <asp:regularexpressionvalidator id="RegularExpressionValidator228" runat="server" errormessage="Value required" controltovalidate="txtwholemilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                 </td>
                                                            <td>
                                                                <asp:TextBox ID="txtfullcreammilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator229" runat="server" errormessage="Value required" controltovalidate="txtfullcreammilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtstdmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator230" runat="server" errormessage="Value required" controltovalidate="txtstdmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttonedmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator231" runat="server" errormessage="Value required" controltovalidate="txttonedmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdtmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator232" runat="server" errormessage="Value required" controltovalidate="txtdtmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator233" runat="server" errormessage="Value required" controltovalidate="txtskimmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtrawchilldmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator234" runat="server" errormessage="Value required" controltovalidate="txtskimmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtchaispecimilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator235" runat="server" errormessage="Value required" controltovalidate="txtchaispecimilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcowmilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator236" runat="server" errormessage="Value required" controltovalidate="txtcowmilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsanchilitemilk" onpaste="return false" ClientIDMode="Static" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator237" runat="server" errormessage="Value required" controltovalidate="txtsanchilitemilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtchahamilk" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" onkeyup="LMScalc()" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator238" runat="server" errormessage="Value required" controltovalidate="txtchahamilk" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalmilksale" onpaste="return false" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator239" runat="server" errormessage="Value required" controltovalidate="txttotalmilksale" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID = "hftotalmilksale" runat = "server" />
                                                                <asp:HiddenField ClientIDMode="Static" ID = "hfDay" runat = "server" />
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
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator240" runat="server" errormessage="Value required" controltovalidate="txtwholemilk_SMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_SMG" onpaste="return false" ClientIDMode="Static" onBlur="SMGcalc1()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator241" runat="server" errormessage="Value required" controltovalidate="txtskimmilk_SMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_SMG" onpaste="return false" ClientIDMode="Static" onBlur="SMGcalc2()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator242" runat="server" errormessage="Value required" controltovalidate="txtOther_SMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalsmgsale_SMG" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator243" runat="server" errormessage="Value required" controltovalidate="txtTotalsmgsale_SMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID = "hfSMS_TotalSMGSale" runat = "server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholemilk_NMG" onpaste="return false" ClientIDMode="Static" onBlur="NMGcalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator244" runat="server" errormessage="Value required" controltovalidate="txtwholemilk_NMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilk_NMG" onpaste="return false" ClientIDMode="Static" onBlur="NMGcalc1()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator245" runat="server" errormessage="Value required" controltovalidate="txtskimmilk_NMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_NMG" onpaste="return false" ClientIDMode="Static" onBlur="NMGcalc2()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator246" runat="server" errormessage="Value required" controltovalidate="txtOther_NMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalNMGsale_NMG" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator247" runat="server" errormessage="Value required" controltovalidate="txtTotalNMGsale_NMG" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID = "hfTotal_NMS_OS" runat = "server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtwholmilkinLit_OSALE" onpaste="return false" ClientIDMode="Static" onBlur="OScalc()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator248" runat="server" errormessage="Value required" controltovalidate="txtwholmilkinLit_OSALE" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtskimmilkinLit_OSALE" onpaste="return false" ClientIDMode="Static" onBlur="OScalc1()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator249" runat="server" errormessage="Value required" controltovalidate="txtskimmilkinLit_OSALE" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOther_OSALE" onpaste="return false" ClientIDMode="Static" onBlur="OScalc2()" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>

                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator250" runat="server" errormessage="Value required" controltovalidate="txtOther_OSALE" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txttotalBulkSale_OSALE" onpaste="return false" ClientIDMode="Static" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator251" runat="server" errormessage="Value required" controltovalidate="txttotalBulkSale_OSALE" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator252" runat="server" errormessage="Value required" controltovalidate="txtMILKPROC_monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKPROC_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator253" runat="server" errormessage="Value required" controltovalidate="txtMILKPROC_Cummulat" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILK_MOnthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator254" runat="server" errormessage="Value required" controltovalidate="txtLocalMILK_MOnthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMilk_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator255" runat="server" errormessage="Value required" controltovalidate="txtLocalMilk_Cummulat" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalMilkSale_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator256" runat="server" errormessage="Value required" controltovalidate="txtTotalMilkSale_Monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTotalMilkSale_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator257" runat="server" errormessage="Value required" controltovalidate="txtTotalMilkSale_Cummulat" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGmilk_Monthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator258" runat="server" errormessage="Value required" controltovalidate="txtSMGmilk_Monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSMGmilk_Cummulat" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator259" runat="server" errormessage="Value required" controltovalidate="txtSMGmilk_Cummulat" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGOTH_MOnthly" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator260" runat="server" errormessage="Value required" controltovalidate="txtNMGOTH_MOnthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNMGOTH_Cummulat" oninput="validate(this)" AutoComplete="off" MaxLength="8" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator261" runat="server" errormessage="Value required" controltovalidate="txtNMGOTH_Cummulat" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator262" runat="server" errormessage="Value required" controltovalidate="txtMILKproKGPD_KG_Monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKproKGPD_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator263" runat="server" errormessage="Value required" controltovalidate="txtMILKproKGPD_Monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMILKprocKGPD_Cummulative" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator264" runat="server" errormessage="Value required" controltovalidate="txtMILKprocKGPD_Cummulative" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                <asp:HiddenField ClientIDMode="Static" ID = "hfMP_TillMonthGMPMS" runat = "server" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILKLPD_Ltr_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator265" runat="server" errormessage="Value required" controltovalidate="txtLocalMILKLPD_Ltr_Monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalMILKLPD_Monthly" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator266" runat="server" errormessage="Value required" controltovalidate="txtLocalMILKLPD_Monthly" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLocalmilkLPD_Cummulative" ClientIDMode="Static" onkeyup="GMPcalc()" onpaste="return false" oninput="validate(this)" AutoComplete="off" MaxLength="10" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator267" runat="server" errormessage="Value required" controltovalidate="txtLocalmilkLPD_Cummulative" validationgroup="MPAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                                 <asp:HiddenField ClientIDMode="Static" ID = "hfLMS_TillMonthGMPMS" runat = "server" />
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
                                                                <asp:textbox id="txtGhee" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator167" runat="server" errormessage="Value required" controltovalidate="txtGhee" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSkimmilkpowder" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator168" runat="server" errormessage="Value required" controltovalidate="txtSkimmilkpowder" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txttablebutter" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator169" runat="server" errormessage="Value required" controltovalidate="txttablebutter" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtwhitebutter" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator170" runat="server" errormessage="Value required" controltovalidate="txtwhitebutter" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtWMP" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator171" runat="server" errormessage="Value required" controltovalidate="txtWMP" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtpaneer" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator172" runat="server" errormessage="Value required" controltovalidate="txtpaneer" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSanchi_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator173" runat="server" errormessage="Value required" controltovalidate="txtSanchi_GHEESale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSneha_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator174" runat="server" errormessage="Value required" controltovalidate="txtSneha_GHEESale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtOther_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator175" runat="server" errormessage="Value required" controltovalidate="txtOther_GHEESale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtTotal_GHEESale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator176" runat="server" errormessage="Value required" controltovalidate="txtTotal_GHEESale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtskimmilk_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator177" runat="server" errormessage="Value required" controltovalidate="txtskimmilk_Prodctsale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txttablebutter_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator178" runat="server" errormessage="Value required" controltovalidate="txttablebutter_Prodctsale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtwhitebutter_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator179" runat="server" errormessage="Value required" controltovalidate="txtwhitebutter_Prodctsale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtshrikhand_Prodctsale" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator180" runat="server" errormessage="Value required" controltovalidate="txtshrikhand_Prodctsale" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox id="txtPAneer_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator181" runat="server" errormessage="Value required" controltovalidate="txtPAneer_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtflavmilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator182" runat="server" errormessage="Value required" controltovalidate="txtflavmilk_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtBtrmilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator183" runat="server" errormessage="Value required" controltovalidate="txtBtrmilk_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSweetcurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator184" runat="server" errormessage="Value required" controltovalidate="txtSweetcurd_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtpeda_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator185" runat="server" errormessage="Value required" controltovalidate="txtpeda_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtplaincurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator186" runat="server" errormessage="Value required" controltovalidate="txtplaincurd_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtorangsip_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator187" runat="server" errormessage="Value required" controltovalidate="txtorangsip_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtprobioticcurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator188" runat="server" errormessage="Value required" controltovalidate="txtprobioticcurd_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtwholemilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator189" runat="server" errormessage="Value required" controltovalidate="txtwholemilk_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtChenarabdi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator190" runat="server" errormessage="Value required" controltovalidate="txtChenarabdi_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtpresscurd_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator191" runat="server" errormessage="Value required" controltovalidate="txtpresscurd_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcream_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator192" runat="server" errormessage="Value required" controltovalidate="txtcream_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtlassi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator193" runat="server" errormessage="Value required" controltovalidate="txtlassi_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtamarkhand_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator194" runat="server" errormessage="Value required" controltovalidate="txtamarkhand_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtsmp_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator195" runat="server" errormessage="Value required" controltovalidate="txtsmp_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox id="txtmawa_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator196" runat="server" errormessage="Value required" controltovalidate="txtmawa_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtdrycasein_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator197" runat="server" errormessage="Value required" controltovalidate="txtdrycasein_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcookingbutter_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator198" runat="server" errormessage="Value required" controltovalidate="txtcookingbutter_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtgulabjamun_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator199" runat="server" errormessage="Value required" controltovalidate="txtgulabjamun_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtrasgulla_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator200" runat="server" errormessage="Value required" controltovalidate="txtrasgulla_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtmawagulabjanum_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator201" runat="server" errormessage="Value required" controltovalidate="txtmawagulabjanum_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtmilkcake_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator202" runat="server" errormessage="Value required" controltovalidate="txtmilkcake_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtThandai_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator203" runat="server" errormessage="Value required" controltovalidate="txtThandai_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtMDm_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator204" runat="server" errormessage="Value required" controltovalidate="txtMDm_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtlightlassi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator205" runat="server" errormessage="Value required" controltovalidate="txtlightlassi_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtpudinaraita_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator206" runat="server" errormessage="Value required" controltovalidate="txtpudinaraita_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtWMP_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator207" runat="server" errormessage="Value required" controltovalidate="txtWMP_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtpannerachar_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator208" runat="server" errormessage="Value required" controltovalidate="txtpannerachar_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox id="txtsanchilitemilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator209" runat="server" errormessage="Value required" controltovalidate="txtsanchilitemilk_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtnariyalbarfi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator210" runat="server" errormessage="Value required" controltovalidate="txtnariyalbarfi_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtgulabjamunMix_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator211" runat="server" errormessage="Value required" controltovalidate="txtgulabjamunMix_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcoffemix_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator212" runat="server" errormessage="Value required" controltovalidate="txtcoffemix_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcookingbutter" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator213" runat="server" errormessage="Value required" controltovalidate="txtcookingbutter" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtlowfatpanner_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator214" runat="server" errormessage="Value required" controltovalidate="txtlowfatpanner_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtwheydrink_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator215" runat="server" errormessage="Value required" controltovalidate="txtwheydrink_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtsanchitea_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator216" runat="server" errormessage="Value required" controltovalidate="txtsanchitea_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtpedaprasadi_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator217" runat="server" errormessage="Value required" controltovalidate="txtpedaprasadi_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txticecream_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator218" runat="server" errormessage="Value required" controltovalidate="txticecream_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtgoldenmilk_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator219" runat="server" errormessage="Value required" controltovalidate="txtgoldenmilk_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtsugarfreepeda_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator220" runat="server" errormessage="Value required" controltovalidate="txtsugarfreepeda_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txthealthvita_PMAS" onpaste="return false" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator221" runat="server" errormessage="Value required" controltovalidate="txthealthvita_PMAS" validationgroup="PMAS" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnPMandSale" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="PMAS" onclick="btnPMandSale_Click"></asp:button>
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
                                                                <asp:textbox id="txtSMP_forMilk" onpaste="return false" clientidmode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator152" runat="server" errormessage="Value required" controltovalidate="txtSMP_forMilk" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtWB_forMilk" onpaste="return false" clientidmode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator153" runat="server" errormessage="Value required" controltovalidate="txtWB_forMilk" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtGHEE_forMilk" onpaste="return false" clientidmode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator154" runat="server" errormessage="Value required" controltovalidate="txtGHEE_forMilk" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtWMP_forMilk" onpaste="return false" clientidmode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator155" runat="server" errormessage="Value required" controltovalidate="txtWMP_forMilk" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtPANEER_forMilk" onpaste="return false" clientidmode="Static" onkeyup="RMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator156" runat="server" errormessage="Value required" controltovalidate="txtPANEER_forMilk" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtTOTAL_forMilk" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator157" runat="server" errormessage="Value required" controltovalidate="txtTOTAL_forMilk" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSMP_forproduct" onpaste="return false" clientidmode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator158" runat="server" errormessage="Value required" controltovalidate="txtSMP_forproduct" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtWB_forproduct" onpaste="return false" clientidmode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator159" runat="server" errormessage="Value required" controltovalidate="txtWB_forproduct" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtGHEE_forproduct" onpaste="return false" clientidmode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator160" runat="server" errormessage="Value required" controltovalidate="txtGHEE_forproduct" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtWMP_forproduct" onpaste="return false" clientidmode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator161" runat="server" errormessage="Value required" controltovalidate="txtWMP_forproduct" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtPANEER_forproduct" onpaste="return false" clientidmode="Static" onkeyup="RPcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator162" runat="server" errormessage="Value required" controltovalidate="txtPANEER_forproduct" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtTOTAL_forproduct" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator163" runat="server" errormessage="Value required" controltovalidate="txtTOTAL_forproduct" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtformilk_Commused" onpaste="return false" clientidmode="Static" onkeyup="RCommcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator164" runat="server" errormessage="Value required" controltovalidate="txtformilk_Commused" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtforproduct_Commused" onpaste="return false" clientidmode="Static" onkeyup="RCommcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator165" runat="server" errormessage="Value required" controltovalidate="txtforproduct_Commused" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtRecombination" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator166" runat="server" errormessage="Value required" controltovalidate="txtRecombination" validationgroup="REC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnrecombination" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="REC" onclick="btnrecombination_Click"></asp:button>
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
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalaryandallow_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator128" runat="server" errormessage="Value required" controltovalidate="txtsalaryandallow_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtContlaboures_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator129" runat="server" errormessage="Value required" controltovalidate="txtContlaboures_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtCosumbleCommon_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator130" runat="server" errormessage="Value required" controltovalidate="txtCosumbleCommon_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtConsumbledirect_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator131" runat="server" errormessage="Value required" controltovalidate="txtConsumbledirect_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtchemicaldetergent_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator132" runat="server" errormessage="Value required" controltovalidate="txtchemicaldetergent_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtElectricity_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator133" runat="server" errormessage="Value required" controltovalidate="txtElectricity_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtWater_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator134" runat="server" errormessage="Value required" controltovalidate="txtWater_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfurnanceoil_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator135" runat="server" errormessage="Value required" controltovalidate="txtfurnanceoil_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtrepairmaintance_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator136" runat="server" errormessage="Value required" controltovalidate="txtrepairmaintance_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOtherExps_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator137" runat="server" errormessage="Value required" controltovalidate="txtOtherExps_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_FPCD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator138" runat="server" errormessage="Value required" controltovalidate="txtTotal_FPCD" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalaryallow_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator139" runat="server" errormessage="Value required" controltovalidate="txtsalaryallow_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtContlaboures_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator140" runat="server" errormessage="Value required" controltovalidate="txtContlaboures_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtConsumblecommon_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator141" runat="server" errormessage="Value required" controltovalidate="txtConsumblecommon_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtConsumbleDirect_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator142" runat="server" errormessage="Value required" controltovalidate="txtConsumbleDirect_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtchemicaldetergent_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator143" runat="server" errormessage="Value required" controltovalidate="txtchemicaldetergent_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtElectricity_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator144" runat="server" errormessage="Value required" controltovalidate="txtElectricity_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtWater_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator145" runat="server" errormessage="Value required" controltovalidate="txtWater_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFurnanceoil_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator146" runat="server" errormessage="Value required" controltovalidate="txtFurnanceoil_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtRepairMaint_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator147" runat="server" errormessage="Value required" controltovalidate="txtRepairMaint_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOtherExps_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator148" runat="server" errormessage="Value required" controltovalidate="txtOtherExps_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTOTal_CA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator149" runat="server" errormessage="Value required" controltovalidate="txtTOTal_CA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtMilkProssCA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator150" runat="server" errormessage="Value required" controltovalidate="txtMilkProssCA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                        </td>
                                                        <td>
                                                            <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtMilkPrepackCA" runat="server" cssclass="form-control"></asp:textbox>
                                                            <asp:regularexpressionvalidator id="RegularExpressionValidator151" runat="server" errormessage="Value required" controltovalidate="txtMilkPrepackCA" validationgroup="PPM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                <asp:textbox clientidmode="Static" onkeyup="PPMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtPPMGrandTotal" runat="server" cssclass="form-control"></asp:textbox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:button id="btnPPMaking" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="PPM" onclick="btnPPMaking_Click1"></asp:button>
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
                                                                <asp:textbox id="txtmilCC_PC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator112" runat="server" errormessage="Value required" controltovalidate="txtmilCC_PC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtMilkDairy_PC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator113" runat="server" errormessage="Value required" controltovalidate="txtMilkDairy_PC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtmainproduct_PC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator114" runat="server" errormessage="Value required" controltovalidate="txtmainproduct_PC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtINDGproduct_PC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcPC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator115" runat="server" errormessage="Value required" controltovalidate="txtINDGproduct_PC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtTotal_PC" clientidmode="Static" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator116" runat="server" errormessage="Value required" controltovalidate="txtTotal_PC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtsalaryallow_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator117" runat="server" errormessage="Value required" controltovalidate="txtsalaryallow_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtContlaboure_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator118" runat="server" errormessage="Value required" controltovalidate="txtContlaboure_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtElectricity_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator119" runat="server" errormessage="Value required" controltovalidate="txtElectricity_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtCoalDiesel_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator120" runat="server" errormessage="Value required" controltovalidate="txtCoalDiesel_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtConsumble_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator121" runat="server" errormessage="Value required" controltovalidate="txtConsumble_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtChemandDeter_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator122" runat="server" errormessage="Value required" controltovalidate="txtChemandDeter_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtRepairMAint_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator123" runat="server" errormessage="Value required" controltovalidate="txtRepairMAint_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtBMC_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator124" runat="server" errormessage="Value required" controltovalidate="txtBMC_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSecurity_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator125" runat="server" errormessage="Value required" controltovalidate="txtSecurity_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtOtherExps_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator126" runat="server" errormessage="Value required" controltovalidate="txtOtherExps_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtTotal_CC" onpaste="return false" clientidmode="Static" onkeyup="PCCcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator127" runat="server" errormessage="Value required" controltovalidate="txtTotal_CC" validationgroup="P&CC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnPackagingAndCC" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="P&CC" onclick="btnPackagingAndCC_Click"></asp:button>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalaryAllow_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator92" runat="server" errormessage="Value required" controltovalidate="txtsalaryAllow_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txttranportation_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator93" runat="server" errormessage="Value required" controltovalidate="txttranportation_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalesprom_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator94" runat="server" errormessage="Value required" controltovalidate="txtsalesprom_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtservicecharg_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator95" runat="server" errormessage="Value required" controltovalidate="txtservicecharg_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAdvertise_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator96" runat="server" errormessage="Value required" controltovalidate="txtAdvertise_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAdvanceCard_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator97" runat="server" errormessage="Value required" controltovalidate="txtAdvanceCard_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtContractlabour_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator98" runat="server" errormessage="Value required" controltovalidate="txtContractlabour_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOthers_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator99" runat="server" errormessage="Value required" controltovalidate="txtOthers_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_MarkCostLM" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator100" runat="server" errormessage="Value required" controltovalidate="txtTotal_MarkCostLM" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalryAllow_SMGNMG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator101" runat="server" errormessage="Value required" controltovalidate="txtsalryAllow_SMGNMG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTransport_SMGNMG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator102" runat="server" errormessage="Value required" controltovalidate="txtTransport_SMGNMG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtServicCharg_SMGNMG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator103" runat="server" errormessage="Value required" controltovalidate="txtServicCharg_SMGNMG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOthers_SMGNMG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator104" runat="server" errormessage="Value required" controltovalidate="txtOthers_SMGNMG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_SMGNMG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator105" runat="server" errormessage="Value required" controltovalidate="txtTotal_SMGNMG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalryAndAllow_INDG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator106" runat="server" errormessage="Value required" controltovalidate="txtsalryAndAllow_INDG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTransport_INDG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator107" runat="server" errormessage="Value required" controltovalidate="txtTransport_INDG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtServicCharg_INDG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator108" runat="server" errormessage="Value required" controltovalidate="txtServicCharg_INDG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOthersTax_INDG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator109" runat="server" errormessage="Value required" controltovalidate="txtOthersTax_INDG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_INDG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator110" runat="server" errormessage="Value required" controltovalidate="txtTotal_INDG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="MarkcalcCC()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotalMKTG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator111" runat="server" errormessage="Value required" controltovalidate="txtTotalMKTG" validationgroup="Mar" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnMarketing" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="Mar" onclick="btnMarketing_Click"></asp:button>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtDCSMilkRMC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" errormessage="Value required" controltovalidate="txtDCSMilkRMC" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtSMGMilkRMC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" errormessage="Value required" controltovalidate="txtSMGMilkRMC" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtNMGMilkRMC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" errormessage="Value required" controltovalidate="txtNMGMilkRMC" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtOtherMilkRMC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" errormessage="Value required" controltovalidate="txtOtherMilkRMC" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtCOMMUsedRMC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" errormessage="Value required" controltovalidate="txtCOMMUsedRMC" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtTotalRMC" runat="server" cssclass="form-control"></asp:textbox>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtDCSdairyCCimc" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator7" runat="server" errormessage="Value required" controltovalidate="txtDCSdairyCCimc" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtccIMCtoDAIRYpt" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator8" runat="server" errormessage="Value required" controltovalidate="txtccIMCtoDAIRYpt" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtSMGMILKPT" runat="server" cssclass="form-control">

                                                                </asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator9" runat="server" errormessage="Value required" controltovalidate="txtSMGMILKPT" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtNMGMILKpt" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator10" runat="server" errormessage="Value required" controltovalidate="txtNMGMILKpt" validationgroup="a"
                                                                    validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeyup="rowmatecost()" onkeypress="return isNumberKey(this, event);" id="txtTotalPT" runat="server" cssclass="form-control"></asp:textbox>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnroematerial" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" onclick="btnroematerial_click" validationgroup="a"></asp:button>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsalaryAndAllow_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator66" runat="server" errormessage="Value required" controltovalidate="txtsalaryAndAllow_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtMedicalTA_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator67" runat="server" errormessage="Value required" controltovalidate="txtMedicalTA_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtConveyence_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator68" runat="server" errormessage="Value required" controltovalidate="txtConveyence_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtSecurity_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator69" runat="server" errormessage="Value required" controltovalidate="txtSecurity_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtSupervisionVehic_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator70" runat="server" errormessage="Value required" controltovalidate="txtSupervisionVehic_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtContractLabour_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator71" runat="server" errormessage="Value required" controltovalidate="txtContractLabour_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtInsuranceOTH_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator72" runat="server" errormessage="Value required" controltovalidate="txtInsuranceOTH_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtLegalAuditFee_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator73" runat="server" errormessage="Value required" controltovalidate="txtLegalAuditFee_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtStationary_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator74" runat="server" errormessage="Value required" controltovalidate="txtStationary_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtOther_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator75" runat="server" errormessage="Value required" controltovalidate="txtOther_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_AD" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator76" runat="server" errormessage="Value required" controltovalidate="txtTotal_AD" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtBonus_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator77" runat="server" errormessage="Value required" controltovalidate="txtBonus_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtAuditFees_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator78" runat="server" errormessage="Value required" controltovalidate="txtAuditFees_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtGroupGratutiy_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator79" runat="server" errormessage="Value required" controltovalidate="txtGroupGratutiy_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtLiveires_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator80" runat="server" errormessage="Value required" controltovalidate="txtLiveires_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtLeavesalary_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator81" runat="server" errormessage="Value required" controltovalidate="txtLeavesalary_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtotherExps_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator82" runat="server" errormessage="Value required" controltovalidate="txtotherExps_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_Provi" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator83" runat="server" errormessage="Value required" controltovalidate="txtTotal_Provi" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtNDDB_LI" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator84" runat="server" errormessage="Value required" controltovalidate="txtNDDB_LI" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtBANKLoan_LI" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator85" runat="server" errormessage="Value required" controltovalidate="txtBANKLoan_LI" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtTotal_LI" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator86" runat="server" errormessage="Value required" controltovalidate="txtTotal_LI" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtDepreciation" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator87" runat="server" errormessage="Value required" controltovalidate="txtDepreciation" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtProductMaking" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator88" runat="server" errormessage="Value required" controltovalidate="txtProductMaking" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtConversionCharge" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator89" runat="server" errormessage="Value required" controltovalidate="txtConversionCharge" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFPOther" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator90" runat="server" errormessage="Value required" controltovalidate="txtFPOther" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="ADMcalc()" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtFPTotal" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator91" runat="server" errormessage="Value required" controltovalidate="txtFPTotal" validationgroup="ADM" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnAdministration" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="ADM" onclick="btnAdministration_Click"></asp:button>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtmilksaleTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator41" runat="server" errormessage="Value required" controltovalidate="txtmilksaleTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtProductsTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator42" runat="server" errormessage="Value required" controltovalidate="txtProductsTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtTotalSaleTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator43" runat="server" errormessage="Value required" controltovalidate="txtTotalSaleTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtCommoditiesTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator44" runat="server" errormessage="Value required" controltovalidate="txtCommoditiesTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtcommditiesPurchTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator45" runat="server" errormessage="Value required" controltovalidate="txtcommditiesPurchTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtopeningStocksTR" runat="server" cssclass="form-control" ToolTip="Last Month Closing"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator46" runat="server" errormessage="Value required" controltovalidate="txtopeningStocksTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtClosingStocksTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator47" runat="server" errormessage="Value required" controltovalidate="txtClosingStocksTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtOtherIncomeTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator48" runat="server" errormessage="Value required" controltovalidate="txtOtherIncomeTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtNetRecieptsTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator49" runat="server" errormessage="Value required" controltovalidate="txtNetRecieptsTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtbeforIDATR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator50" runat="server" errormessage="Value required" controltovalidate="txtbeforIDATR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtbeforDEFERDTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator51" runat="server" errormessage="Value required" controltovalidate="txtbeforDEFERDTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtbeforDEPRECIATIONTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator52" runat="server" errormessage="Value required" controltovalidate="txtbeforDEPRECIATIONTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtNETINCLUDEPTR" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator53" runat="server" errormessage="Value required" controltovalidate="txtNETINCLUDEPTR" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txttotalvarriCostTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator54" runat="server" errormessage="Value required" controltovalidate="txttotalvarriCostTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtsalaryWages_TFCOSTTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator55" runat="server" errormessage="Value required" controltovalidate="txtsalaryWages_TFCOSTTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtOthers_TFCOSTTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator56" runat="server" errormessage="Value required" controltovalidate="txtOthers_TFCOSTTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txttotalfixCostTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator57" runat="server" errormessage="Value required" controltovalidate="txttotalfixCostTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtToCostTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator58" runat="server" errormessage="Value required" controltovalidate="txtToCostTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtTFcostEXCLINTTTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator59" runat="server" errormessage="Value required" controltovalidate="txtTFcostEXCLINTTTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtToSaleTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator60" runat="server" errormessage="Value required" controltovalidate="txtToSaleTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtIDAOpertingProfTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator61" runat="server" errormessage="Value required" controltovalidate="txtIDAOpertingProfTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtNEtIncluIDTTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator62" runat="server" errormessage="Value required" controltovalidate="txtNEtIncluIDTTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtToSaleWithCFFTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator63" runat="server" errormessage="Value required" controltovalidate="txtToSaleWithCFFTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtOPLOssProfitCFFTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator64" runat="server" errormessage="Value required" controltovalidate="txtOPLOssProfitCFFTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox onpaste="return false" clientidmode="Static" onkeyup="RCTcalc()" oninput="validate(this)" autocomplete="off" maxlength="10" onkeypress="return isNumberKey(this, event);" id="txtNETprofitLossTVC" runat="server" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator65" runat="server" errormessage="Value required" controltovalidate="txtNETprofitLossTVC" validationgroup="FPC" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnReceipt" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" validationgroup="FPC" onclick="btnReceipt_Click"></asp:button>
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
                                                                <asp:textbox id="txtthruoputwithoutWC_PC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator29" runat="server" errormessage="Value required" controltovalidate="txtthruoputwithoutWC_PC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtthroughpuINKGS_PC" runat="server" onkeyup="CapacityUti()" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator30" runat="server" errormessage="Value required" controltovalidate="txtthroughpuINKGS_PC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtthroughpuINLTS_PC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator31" runat="server" errormessage="Value required" controltovalidate="txtthroughpuINLTS_PC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtthroughputPERDAY_PC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator32" runat="server" errormessage="Value required" controltovalidate="txtthroughputPERDAY_PC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcapacityutilisationINKGS_PC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator33" runat="server" errormessage="Value required" controltovalidate="txtcapacityutilisationINKGS_PC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtAllCCsthruoput_CC" onkeyup="CapUtiCC()" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator34" runat="server" errormessage="Value required" controltovalidate="txtAllCCsthruoput_CC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcapacityuti_CC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator35" runat="server" errormessage="Value required" controltovalidate="txtcapacityuti_CC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtbcfsale_BMC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator36" runat="server" errormessage="Value required" controltovalidate="txtbcfsale_BMC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtbcfProdCFF_BMC" runat="server" onkeyup="CapacityUtiSMP()" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator37" runat="server" errormessage="Value required" controltovalidate="txtbcfProdCFF_BMC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcapacityuti_BMC" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator38" runat="server" errormessage="Value required" controltovalidate="txtcapacityuti_BMC" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtSMPProd_SMP" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator39" runat="server" errormessage="Value required" controltovalidate="txtSMPProd_SMP" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                            <td>
                                                                <asp:textbox id="txtcapacityuti_SMP" runat="server" onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" cssclass="form-control"></asp:textbox>
                                                                <asp:regularexpressionvalidator id="RegularExpressionValidator40" runat="server" errormessage="Value required" controltovalidate="txtcapacityuti_SMP" validationgroup="CU" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnCapUtilisation" runat="server" style="margin-top: 19px;" validationgroup="CU" cssclass="btn btn-primary btn-block" text="Save" onclick="btnCapUtilisation_Click"></asp:button>
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
                                                        <asp:textbox onpaste="return false" onkeyup="CalMB()" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsmilkcow_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" errormessage="Value required" controltovalidate="txtdcsmilkcow_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" onkeyup="CalMB()" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsmilkbuff_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator11" runat="server" errormessage="Value required" controltovalidate="txtdcsmilkbuff_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtdcsmilktotal_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator12" runat="server" errormessage="Value required" controltovalidate="txtdcsmilktotal_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" onkeyup="CalMB()" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfat_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator13" runat="server" errormessage="Value required" controltovalidate="txtfat_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" onkeyup="CalMB()" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsnf_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator14" runat="server" errormessage="Value required" controltovalidate="txtsnf_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfatpercent_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator15" runat="server" errormessage="Value required" controltovalidate="txtfatpercent_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsnfpercent_MP" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator16" runat="server" errormessage="Value required" controltovalidate="txtsnfpercent_MP" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtquantityinkgs_TI" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator17" runat="server" errormessage="Value required" controltovalidate="txtquantityinkgs_TI" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfatinkgs_TI" onkeyup="CalMBInOut()" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator18" runat="server" errormessage="Value required" controltovalidate="txtfatinkgs_TI" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsnfinkgs_TI" onkeyup="CalMBInOut()" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator19" runat="server" errormessage="Value required" controltovalidate="txtsnfinkgs_TI" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtvalueinrs_TI" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator20" runat="server" errormessage="Value required" controltovalidate="txtvalueinrs_TI" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
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
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtquantityinkgs_TO" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator21" runat="server" errormessage="Value required" controltovalidate="txtquantityinkgs_TO" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>

                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfatinkgs_TO" onkeyup="CalMBInOut()" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator22" runat="server" errormessage="Value required" controltovalidate="txtfatinkgs_TO" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsnfinKgs_TO" onkeyup="CalMBInOut()" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator23" runat="server" errormessage="Value required" controltovalidate="txtsnfinKgs_TO" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtValueinrs_To" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator24" runat="server" errormessage="Value required" controltovalidate="txtValueinrs_To" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>

                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfatinkgs_MG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator25" runat="server" errormessage="Value required" controltovalidate="txtfatinkgs_MG" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsngfinkgs_MG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator26" runat="server" errormessage="Value required" controltovalidate="txtsngfinkgs_MG" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtfatpercentage_MG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator27" runat="server" errormessage="Value required" controltovalidate="txtfatpercentage_MG" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                    <td>
                                                        <asp:textbox onpaste="return false" clientidmode="Static" oninput="validate(this)" autocomplete="off" maxlength="8" onkeypress="return isNumberKey(this, event);" id="txtsnfpercentage_MG" runat="server" cssclass="form-control"></asp:textbox>
                                                        <asp:regularexpressionvalidator id="RegularExpressionValidator28" runat="server" errormessage="Value required" controltovalidate="txtsnfpercentage_MG" validationgroup="MB" validationexpression="(?:\d*\.\d{1,2}|\d+)$"></asp:regularexpressionvalidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </fieldset>
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:button id="btnmaterialbalancing" runat="server" style="margin-top: 19px;" cssclass="btn btn-primary btn-block" text="Save" onclick="btnmaterialbalancing_Click" validationgroup="MB"></asp:button>
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


            document.getElementById('<%=txtTotalMilkSale_Monthly.ClientID %>').value = getSum(document.getElementById('<%=txtLocalMILK_MOnthly.ClientID %>').value,
                                                                                            document.getElementById('<%=txtSMGmilk_Monthly.ClientID %>').value,
                                                                                document.getElementById('<%=txtNMGOTH_MOnthly.ClientID %>').value);

            document.getElementById('<%=txtTotalMilkSale_Cummulat.ClientID %>').value = getSum(document.getElementById('<%=txtLocalMilk_Cummulat.ClientID %>').value,
                                                                                            document.getElementById('<%=txtSMGmilk_Cummulat.ClientID %>').value,
                                                                                document.getElementById('<%=txtNMGOTH_Cummulat.ClientID %>').value);
        }
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
            TMScalMPS();
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
            TMScalMPS();
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
            TMScalMPS();
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

