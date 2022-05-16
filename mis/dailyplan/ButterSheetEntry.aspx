<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ButterSheetEntry.aspx.cs" Inherits="mis_dailyplan_ButterSheetEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">BUTTER SHEET ENTRY</h3>
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                        <asp:dropdownlist id="ddlDS" runat="server" cssclass="form-control"></asp:dropdownlist>
                                        <small><span id="valddlDS" class="text-danger"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Production Section<span class="text-danger">*</span></label>
                                        <asp:dropdownlist id="ddlPSection" runat="server" cssclass="form-control"></asp:dropdownlist>
                                        <small><span id="valddlDSPS" class="text-danger"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="rfvDate" runat="server" display="Dynamic" controltovalidate="txtDate" text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" errormessage="Enter Date" setfocusonerror="true" forecolor="Red" validationgroup="Submit"></asp:requiredfieldvalidator>
                                        </span>
                                        <asp:textbox id="txtDate" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shift</label>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" display="Dynamic" controltovalidate="ddlShift" initialvalue="0" text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" errormessage="Select Shift" setfocusonerror="true" forecolor="Red" validationgroup="Submit"></asp:requiredfieldvalidator>
                                        </span>
                                        <div class="form-group">
                                            <asp:dropdownlist id="ddlShift" cssclass="form-control" runat="server">
                                    </asp:dropdownlist>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <fieldset>
                        <legend>BUTTER SHEET ENTRY</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table class="table table-bordered">
                                        <tr>
                                            <th style="text-align: center">Particulars</th>
                                            <th style="text-align: center">Qty.Kg.</th>
                                            <th style="text-align: center">Fat.Kg.</th>
                                            <th style="text-align: center">SNF.Kg.</th>
                                            <th style="text-align: center">Particulars</th>
                                            <th style="text-align: center">Qty.Kg.</th>
                                            <th style="text-align: center">Fat.Kg.</th>
                                            <th style="text-align: center">SNF.Kg.</th>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; font-size:15px;"><b><u>CREAM</u></b></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>WHITE BUTTER MFG.</td>
                                            <td>
                                                <asp:textbox id="TextBox3" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox4" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox5" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>OPENING BALANCE</td>
                                            <td>
                                                <asp:textbox id="TextBox6" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox7" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox8" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>TABLE BUTTER MFG.</td>
                                            <td>
                                                <asp:textbox id="TextBox9" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox10" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox11" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RECEIVING FROM PROCESSING I</td>
                                            <td>
                                                <asp:textbox id="TextBox12" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox13" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox14" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td rowspan="2">B.M PROCESSING</td>
                                            <td rowspan="2">
                                                <asp:textbox id="TextBox15" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td rowspan="2">
                                                <asp:textbox id="TextBox16" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td rowspan="2">
                                                <asp:textbox id="TextBox17" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>RECEIVING FROM PROCESSING II</td>
                                            <td>
                                                <asp:textbox id="TextBox1" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox2" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox18" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td>RECEIVING FROM PROCESSING III</td>
                                            <td>
                                                <asp:textbox id="TextBox22" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox23" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox24" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>CREAM TO PROCESSING</td>
                                            <td>
                                                <asp:textbox id="TextBox25" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox26" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox27" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>CREAM TO W.BUTTER MFG.</td>
                                            <td>
                                                <asp:textbox id="TextBox30" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox31" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>

                                            <td>
                                                <asp:textbox id="TextBox28" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>CREAM TO SALE</td>
                                            <td>
                                                <asp:textbox id="TextBox29" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox33" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox34" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>CREAM TO TA.BUTTER MFG.</td>
                                            <td>
                                                <asp:textbox id="TextBox32" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox35" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>

                                            <td>
                                                <asp:textbox id="TextBox36" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>CLOSING BALANCE</td>
                                            <td>
                                                <asp:textbox id="TextBox37" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox38" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox39" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="font-size:15px;"><b>TOTAL:-</b></td>
                                            <td>
                                                <asp:textbox id="TextBox45" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                                <td>
                                                    <asp:textbox id="TextBox46" cssclass="form-control" runat="server"></asp:textbox>
                                                </td>
                                                <td>
                                                    <asp:textbox id="TextBox47" cssclass="form-control" runat="server"></asp:textbox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><b>TOTAL:-</b></td>
                                            <td>
                                                <asp:textbox id="TextBox48" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox49" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox50" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>LOSS/GAIN</td>
                                            <td>
                                                <asp:textbox id="TextBox51" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox52" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox53" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center; font-size:15px;"><b><u>WHITE BUTTER</u></b></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>ISSUE TO GHEE SECTION</td>
                                            <td>
                                                <asp:textbox id="TextBox57" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox58" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox59" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>OPENING BALANCE</td>
                                            <td>
                                                <asp:textbox id="TextBox60" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox61" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox62" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>ISSUE TO F.P SECTION</td>
                                            <td>
                                                <asp:textbox id="TextBox19" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox20" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox21" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>MFG.FROM CREAM</td>
                                            <td>
                                                <asp:textbox id="TextBox40" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox41" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox42" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>ISSUE TO PROC. FOR COMBINATION</td>
                                           <td>
                                                <asp:textbox id="TextBox183" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox184" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox185" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>FROM F.P. SECTION</td>
                                            <td>
                                                <asp:textbox id="TextBox72" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox73" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox74" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>CLOSING BALANCE</td>
                                            <td>
                                                <asp:textbox id="TextBox75" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox76" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox77" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="font-size:15px;"><b>TOTAL:-</b></td>
                                            <td>
                                                <asp:textbox id="TextBox81" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox82" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox83" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><b>TOTAL:-</b></td>
                                            <td>
                                                <asp:textbox id="TextBox84" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox85" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox86" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>LOSS/GAIN</td>
                                            <td>
                                                <asp:textbox id="TextBox43" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox44" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                            <td>
                                                <asp:textbox id="TextBox54" cssclass="form-control" runat="server"></asp:textbox>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="8">
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>Particulars</th>
                                                        <th>Loose</th>
                                                        <th>25Kg. C Boxes</th>
                                                        <th>25Kg. Poly liner</th>
                                                        <th>25Kg. C Box</th>
                                                        <th>15Kg. C Box</th>
                                                        <th>TOTAL</th>
                                                        <th>Loose</th>
                                                        <th>1/2Kg. Pack</th>
                                                        <th>1/10Kg. Pack</th>
                                                        <th>1/20Kg. Pack</th>
                                                        <th>TOTAL</th>
                                                    </tr>

                                                    <tr>
                                                        <td style="width:150px;"><b>OPENING BALANCE</b></td>
                                                        <td>
                                                            <asp:textbox id="TextBox126" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td >
                                                            <asp:textbox id="TextBox127" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox128" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox129" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox130" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox131" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox132" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox133" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox134" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox135" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox136" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">MFG..</td>
                                                        <td>
                                                            <asp:textbox id="TextBox55" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox56" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox63" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox64" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox65" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox66" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox67" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox68" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox69" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox70" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox71" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;"> Return From F.P./</td>
                                                        <td>
                                                            <asp:textbox id="TextBox78" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox79" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox80" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox87" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox88" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox89" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox90" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox91" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox92" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox93" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox94" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width:150px;">Total</td>
                                                        <td>
                                                            <asp:textbox id="TextBox95" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox96" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox97" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox98" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox99" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox100" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox101" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox102" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox103" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox104" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox105" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">For Packing</td>
                                                        <td>
                                                            <asp:textbox id="TextBox106" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox107" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox108" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox109" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox110" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox111" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox112" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox113" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox114" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox115" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox116" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;"></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">Total</td>
                                                        <td>
                                                            <asp:textbox id="TextBox117" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox118" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox119" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox120" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox121" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox122" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox123" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox124" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox125" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox137" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox138" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">Issue to F.P.</td>
                                                        <td>
                                                            <asp:textbox id="TextBox139" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox140" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox141" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox142" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox143" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox144" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox145" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox146" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox147" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox148" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox149" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">Issue to Ghee</td>
                                                        <td>
                                                            <asp:textbox id="TextBox150" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox151" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox152" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox153" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox154" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox155" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox156" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox157" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox158" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox159" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox160" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;"> Total</td>
                                                        <td>
                                                            <asp:textbox id="TextBox161" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox162" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox163" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox164" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox165" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox166" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox167" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox168" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox169" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox170" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox171" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">Loss RI/TL O/C</td>
                                                        <td>
                                                            <asp:textbox id="TextBox172" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox173" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox174" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox175" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox176" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox177" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox178" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox179" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox180" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox181" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox182" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:150px;">Closing Balance</td>
                                                        <td>
                                                            <asp:textbox id="TextBox269" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox270" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox271" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox272" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox273" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox274" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox275" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox276" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox277" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox278" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>
                                                        <td>
                                                            <asp:textbox id="TextBox279" cssclass="form-control" runat="server"></asp:textbox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>

                                        </tr>
                                    </table>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:button id="btnSave" runat="server" text="Save" cssclass="btn btn-success" />
                                            <asp:button id="btnClear" runat="server" text="Clear" cssclass="btn btn-default" />
                                        </div>
                                    </div>
                                </div>
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

