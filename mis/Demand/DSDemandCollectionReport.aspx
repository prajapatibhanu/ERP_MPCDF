<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DSDemandCollectionReport.aspx.cs" Inherits="mis_Demand_DSDemandCollectionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    
        <div class="content-wrapper">

            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" style="height:990px;">
                            <div class="box-header">
                                <h3 class="box-title">Dugdh Sangh Demand Collection Report</h3>
                                <span id="ctl00_ContentBody_lblmsg"></span>
                            </div>
                            <div class="box-body">

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Name of Plant<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">Plant 1</option>
                                                <option value="2">Plant 2</option>
                                                <option value="3">Plant 3</option>
                                            </select>
                                            <small><span id="valddlstate" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date </label>
                                            <span style="color: red">*</span>
                                            <span class="pull-right">
                                                <span id="ctl00_ContentBody_rfv1" style="color:Red;display:none;"><i class='fa fa-exclamation-circle' title='Please Enter Date !'></i></span>
                                                <span id="ctl00_ContentBody_revdate" style="color:Red;display:none;"><i class='fa fa-exclamation-circle' title='Invalid Date !'></i></span>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input name="ctl00$ContentBody$txtTransactionDt" type="text" maxlength="10" id="txtTransactionDt" class="form-control" onkeypress="javascript: return false;" onpaste="return false;" placeholder="Select Date" autocomplete="off" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" style="width:100%;" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Route No.<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">R1</option>
                                                <option value="2">R2</option>
                                                <option value="3">R3</option>

                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Shift<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">Morning</option>
                                                <option value="2">Evening</option>
                                                <option value="3">Special</option>
                                            </select>
                                            <small><span id="valddlstate" class="text-danger"></span></small>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <label></label>
                                        <div class="form-group">
                                            <input type="submit" name="ctl00$ContentBody$btnSave" value="Search" id="btnSave" class="btn btn-primary btn-block" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <div>
                                                <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse:collapse;">
                                                    <thead>
                                                        <tr>
                                                            <th style="background-color: #DFE2E3; text-align:center; ">Distributor<br /> Name<br /></th>
                                                            <th style="background-color: #DFE2E3">Distributor Code<br /></th>
                                                            <th style="background-color: #DFE2E3"> Standard Milk(500)<br /></th>
                                                            <th style="background-color: #DFE2E3">Double Toned Milk(500)</th>
                                                            <th style="background-color: #DFE2E3"> Double Toned Milk(200)<br /></th>
                                                            <th style="background-color: #DFE2E3">Light 500<br /></th>
                                                            <th style="background-color: #DFE2E3"><br />Full Cream Milk(500)<br /></th>
                                                            <th style="background-color: #DFE2E3"><br />Toned Milk(500)<br /></th>
                                                            <th style="background-color: #DFE2E3">CCM<br />(500)<br /></th>
                                                            <th style="background-color: #DFE2E3">Chaha Milk<br /></th>
                                                            <th style="background-color: #DFE2E3">Chai Spl Milk<br /></th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>

                                                        <tr>
                                                            <td>Distributor Name 1</td>
                                                            <td>D101</td>
                                                            <td>50</td>
                                                            <td>47</td>
                                                            <td>3</td>
                                                            <td>76</td>
                                                            <td>43</td>
                                                            <td>23</td>
                                                            <td>53</td>
                                                            <td>3</td>
                                                            <td>21</td>

                                                        </tr>
                                                        <tr>
                                                            <td>Distributor Name2</td>
                                                            <td>D102</td>
                                                            <td>50</td>
                                                            <td>87</td>
                                                            <td>35</td>
                                                            <td>56</td>
                                                            <td>03</td>
                                                            <td>33</td>
                                                            <td>93</td>
                                                            <td>30</td>
                                                            <td>21</td>

                                                        </tr>
                                                        <tr>
                                                            <td>Distributor Name3</td>
                                                            <td>D103</td>
                                                            <td>50</td>
                                                            <td>90</td>
                                                            <td>88</td>
                                                            <td>76</td>
                                                            <td>83</td>
                                                            <td>43</td>
                                                            <td>53</td>
                                                            <td>86</td>
                                                            <td>21</td>

                                                        </tr>
                                                        <tr>

                                                            <td colspan="2" style="text-align: center;  font-weight: bold">Total Supply (No. Of Packets)</td>
                                                            <td>150</td>
                                                            <td>120</td>
                                                            <td>9</td>
                                                            <td>545</td>
                                                            <td>178</td>
                                                            <td>320</td>
                                                            <td>340</td>
                                                            <td>435</td>
                                                            <td>323</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="10"></td>

                                                        </tr>
                                                        <tr>

                                                            <td colspan="11" style=" background-color: #DFE2E3; font-weight: bold">Crate Summary</td>

                                                        </tr>
                                                        <tr>

                                                            <td colspan="2" style="text-align: center; font-weight: bold">Crate Details</td>
                                                            <td>86</td>
                                                            <td>4</td>
                                                            <td>71</td>
                                                            <td>7</td>
                                                            <td>211</td>
                                                            <td>0</td>
                                                            <td>0</td>
                                                            <td>17</td>
                                                            <td>23</td>
                                                        </tr>
                                                        <tr>

                                                            <td colspan="2" style="text-align:center; font-weight:bold">Extra PKT(+/-)</td>
                                                            <td>+10</td>
                                                            <td>13</td>
                                                            <td>-10</td>
                                                            <td>9</td>
                                                            <td>18</td>
                                                            <td>6</td>
                                                            <td>2</td>
                                                            <td>-3</td>
                                                            <td>-2</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="10"></td>

                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align:center; font-weight:bold">Total Crate (In No.)</td>
                                                            <td>9323</td>
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
                                                            <td colspan="2" style="text-align:center; font-weight:bold">Total  Demand ( No of Kg)</td>
                                                            <td>8790</td>
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
                                                            <td colspan="2" style="text-align:center; font-weight:bold">Total  Demand (In Rupees)</td>
                                                            <td>73000</td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>


                                                        </tr>
                                                    </tbody>
                                                </table>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

