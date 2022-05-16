<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="GheeSheetEntry.aspx.cs" Inherits="mis_dailyplan_GheeSheetEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">GHEE SHEET ENTRY</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Filter</legend>
                                <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                            </fieldset>
                        </div>
                    </div>
                    <fieldset>
                        <legend>GHEE SHEET ENTRY</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table class="table table-bordered">
                                        <tr>
                                            <th style="text-align:center">Particulars</th>
                                            <th style="text-align:center">Qty.Kg.</th>
                                            <th style="text-align:center">Fat.Kg.</th>
                                            <th style="text-align:center">SNF.Kg.</th>
                                            <th style="text-align:center">Particulars</th>
                                            <th style="text-align:center">Qty.Kg.</th>
                                            <th style="text-align:center">Fat.Kg.</th>
                                            <th style="text-align:center">SNF.Kg.</th>
                                        </tr>
                                        <tr>
                                            <td>Sour Milk</td>
                                            <td><asp:TextBox ID="txtQtyKg" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox1" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox2" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Cream Mfg.</td>
                                            <td><asp:TextBox ID="TextBox3" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox4" CssClass="form-control"  runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox5" CssClass="form-control"  runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td><asp:TextBox ID="TextBox6" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox7" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox8" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Sour S.M.to Case in/Sales</td>
                                            <td><asp:TextBox ID="TextBox9"  CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox10" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox11" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>Receipt</td>
                                            <td><asp:TextBox ID="TextBox12" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox13" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox14" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Closing Balance</td>
                                            <td><asp:TextBox ID="TextBox15" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox16" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox17" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>Issued to Separation</td>
                                            <td><asp:TextBox ID="TextBox18" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox19" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox20" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox21" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox22" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox23" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox24" CssClass="form-control"    runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox25" CssClass="form-control"    runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox26" CssClass="form-control"    runat="server"></asp:TextBox></td>
                                            <td>Loss Gain</td>
                                            <td><asp:TextBox ID="TextBox27" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox28" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox29" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>CURDLED MILK</td>
                                            <td><asp:TextBox ID="TextBox30" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox31" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox32" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td><asp:TextBox ID="TextBox36" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox37" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox38" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Curdled B M to Case in/Sales</td>
                                            <td><asp:TextBox ID="TextBox39" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox40" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox41" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>Receipt</td>
                                            <td><asp:TextBox ID="TextBox42" CssClass="form-control"  runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox43" CssClass="form-control"  runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox44" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Closing Balance</td>
                                            <td><asp:TextBox ID="TextBox45" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox46" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox47" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>Issued for Butter mfg.</td>
                                            <td><asp:TextBox ID="TextBox48" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox49" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox50" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox51" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox52" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox53" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox54" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox55" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox56" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Loss Gain</td>
                                            <td><asp:TextBox ID="TextBox57" CssClass="form-control"  runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox58" CssClass="form-control"  runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox59" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>WHITE BUTTER</td>
                                            <td><asp:TextBox ID="TextBox60" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox61" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox62" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                             <td></td>
                                             <td></td>
                                        </tr>
                                         <tr>
                                            <td>Opening Balance</td>
                                            <td><asp:TextBox ID="TextBox66" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox67" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox68" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                             <td></td>
                                             <td></td>
                                        </tr>
                                        <tr>
                                            <td>1-W.Butter</td>
                                            <td><asp:TextBox ID="TextBox72" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox73" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox74" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>Ghee Mfg.</td>
                                            <td><asp:TextBox ID="TextBox75" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox76" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox77" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td>2-Cream</td>
                                            <td><asp:TextBox ID="TextBox78" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox79" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox80" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td>W.B. For Milk Recombination</td>
                                            <td><asp:TextBox ID="TextBox81" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox82" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox83" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>3-White Butter</td>
                                            <td><asp:TextBox ID="TextBox84" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox85" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox86" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>4-Table Butter</td>
                                            <td><asp:TextBox ID="TextBox90" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox91" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox92" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Rece. from Butter Section</td>
                                            <td><asp:TextBox ID="TextBox96" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox97" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox98" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Sour Cream Mfg.</td>
                                            <td><asp:TextBox ID="TextBox102" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox103" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox104" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td>Closing Balance 1-W,Butter</td>
                                            <td><asp:TextBox ID="TextBox105"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox106"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox107"  CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Curdle Butter Mfg.</td>
                                            <td><asp:TextBox ID="TextBox108" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox109" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox110" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td>2- Cream</td>
                                            <td><asp:TextBox ID="TextBox111" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox112" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox113" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Issue to Ghee Mfg.*</td>
                                            <td><asp:TextBox ID="TextBox114" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox115" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox116" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox117" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox118" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox119" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox120" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox121" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox122" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td>Loss Gain</td>
                                            <td><asp:TextBox ID="TextBox123" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox124" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox125" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="8">*Not to be totalled<span style="padding-left:300px; font-size:20px;">GHEE</span></td>
                                        </tr>
                                        <tr >
                                            <td colspan="8">
                                                <table class="table table-bordered">
                                                    <tr>
                                            <th>Particulars</th>
                                            <th>Loose</th>
                                            <th>Sanchi RP 1 Ltr</th>
                                            <th>Sanchi RP 1/2 Ltr</th>
                                            <th>Sanchi RP 200 ml</th>
                                            <th>Sanchi Tin 15 kg</th>
                                            <th>Sneha RP 1 Ltr</th>
                                            <th>Sneha RP 1/2 Ltr</th>
                                            <th>Sanchi</th>
                                            <th>Sanchi</th>
                                            <th>Tin 15 kg</th>
                                            <th></th>
                                            <th>Total</th>
                                            <th>Caesin Dry</th>
                                                    </tr>
                                                
                                        <tr>
                                            <td>Opening Balance</td>
                                            <td><asp:TextBox ID="TextBox126" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox127" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox128" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox129" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox130" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox131" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox132" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox133" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox134" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox135" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox136" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox137" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox138" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                             <td>Mfg.</td>
                                            <td><asp:TextBox ID="TextBox139" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox140" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox141" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox142" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox143" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox144" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox145" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox146" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox147" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox148" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox149" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox150" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox151" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                             <td>Return From</td>
                                            <td><asp:TextBox ID="TextBox152" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox153" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox154" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox155" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox156" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox157" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox158" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox159" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox160" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox161" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox162" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox163" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox164" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                              <td>F.P .........</td>
                                            <td><asp:TextBox ID="TextBox165" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox166" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox167" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox168" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox169" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox170" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox171" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox172" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox173" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox174" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox175" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox176" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox177" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td><asp:TextBox ID="TextBox178" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox179" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox180" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox181" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox182" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox183" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox184" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox185" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox186" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox187" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox188" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox189" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox190" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>For Packing</td>
                                            <td><asp:TextBox ID="TextBox191" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox192" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox193" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox194" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox195" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox196" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox197" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox198" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox199" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox200" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox201" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox202" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox203" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td><asp:TextBox ID="TextBox204" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox205" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox206" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox207" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox208" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox209" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox210" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox211" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox212" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox213" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox214" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox215" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox216" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Issue to F.P.</td>
                                            <td><asp:TextBox ID="TextBox217" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox218" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox219" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox220" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox221" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox222" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox223" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox224" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox225" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox226" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox227" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox228" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox229" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Loss RL/TL/O.C</td>
                                            <td><asp:TextBox ID="TextBox230" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox231" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox232" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox233" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox234" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox235" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox236" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox237" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox238" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox239" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox240" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox241" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox242" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
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
                                            <td></td>
                                            <td></td>
                                            <td></td>

                                        </tr>
                                        <tr>
                                            <td>Col./Bal.</td>
                                            <td><asp:TextBox ID="TextBox256" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox257" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox258" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox259" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox260" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox261" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox262" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox263" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox264" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox265" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox266" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox267" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox268" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>TOTAL</td>
                                            <td><asp:TextBox ID="TextBox269" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox270" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox271" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox272" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox273" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox274" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox275" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox276" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox277" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox278" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox279" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox280" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="TextBox281" CssClass="form-control" runat="server"></asp:TextBox></td>
                                        </tr>
                                        </table>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

