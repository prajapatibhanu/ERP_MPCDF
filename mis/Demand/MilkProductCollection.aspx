<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MilkProductCollection.aspx.cs" Inherits="mis_Demand_MilkProductCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
            <div class="content-wrapper">
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">Milk Product Demand Collection</h3>
                                <span id="ctl00_ContentBody_lblmsg"></span>
                            </div>
                            <div class="box-body">

                                <div class="row">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Distributor Name<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">D1</option>
                                                <option value="2">D2</option>
                                                <option value="3">D3</option>
                                            </select>
                                            <small><span id="valddlstate" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Distributor No.<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtmobno" type="text" maxlength="10" id="ctl00_ContentBody_txtmobno" class="form-control MobileNo" placeholder="Booth No." onkeypress="javascript:tbx_fnNumeric(event, this);" />
                                            <small><span id="valtxtmobno" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Product Name<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">Plain Curd</option>
                                                <option value="2">Sweet Curd</option>
                                                <option value="3">Srikhand</option>
                                                <option value="3">Paneer</option>
                                                <option value="3">Butter Milk</option>
                                                <option value="3">Flavoured Milk</option>
                                                <option value="3">Peda</option>
                                                <option value="3">Lassi</option>
                                                <option value="3">Chena</option>
                                                <option value="3">Milk Cake</option>
                                                <option value="3">Plain Butter Milk</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Variant<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">200   gm</option>
                                                <option value="2">500   gm</option>
                                                <option value="3">1000 gm</option>

                                            </select>
                                            <small><span id="valddlstate" class="text-danger"></span></small>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>No. Of Packet<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtemail" type="text" maxlength="50" id="ctl00_ContentBody_txtemail" class="form-control" placeholder="No. Of Packets" />
                                            <small><span id="valtxtemail" class="text-danger"></span></small>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <input type="submit" style="margin-top:25px" name="ctl00$ContentBody$btnSave" value="Add" id="btnAdd" class="btn btn-success btn-block" />

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
                                                            <th scope="col">S.No.<br /></th>
                                                            <th scope="col">Product Name<br /></th>
                                                            <th scope="col">Variant<br /></th>
                                                            <th scope="col">No. Of Packets<br /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                1
                                                            </td>
                                                            <td>
                                                                Plain Curd
                                                            </td>
                                                            <td>
                                                                200 gm
                                                            </td>
                                                            <td>
                                                                6
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                1
                                                            </td>
                                                            <td>
                                                                Plain Curd
                                                            </td>
                                                            <td>
                                                                500 gm
                                                            </td>
                                                            <td>
                                                                4
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td style="text-align: right; font-weight: bold;">
                                                                Total  Demand (No of Packets)
                                                            </td>
                                                            <td style="font-weight: bold;">
                                                                10
                                                            </td>

                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                </div>



                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <input type="submit" name="ctl00$ContentBody$btnSave" value="Save" id="btnSave" class="btn btn-primary btn-block" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a href="#" class="btn btn-block btn-default">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Milk Product Demand Collection Details </h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <div>
                                        <table class="datatable table table-striped table-bordered table-hover" cellspacing="0" rules="all" border="1" id="ctl00_ContentBody_gvOpeningStock" style="border-collapse:collapse;">
                                            <thead>
                                                <tr>
                                                    <th scope="col">S.No.<br /></th>
                                                    <th scope="col">Date<br /></th>
                                                    <th scope="col">Shift<br /></th>
                                                    <th scope="col">Distributor Name<br /></th>
                                                    <th scope="col">Distributor No<br /></th>
                                                    <th scope="col">List Of Product<br /></th>
                                                    <th scope="col">Total Packets<br /></th>
                                                    <th scope="col">Action <br /></th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        1
                                                    </td>
                                                    <td>
                                                        1/29/2019
                                                    </td>
                                                    
                                                    <td>
                                                        Morning
                                                    </td>
                                                    <td>
                                                        sachi
                                                    </td>
                                                    <td>
                                                        D001
                                                    </td>
                                                    <td>
                                                        Chena
                                                    </td>
                                                    <td>
                                                        10
                                                    </td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        2
                                                    </td>
                                                    <td>
                                                        1/30/2019
                                                    </td>
                                                  
                                                    <td>
                                                        Evening
                                                    </td>
                                                    <td>
                                                        sachi
                                                    </td>
                                                    <td>
                                                        D002
                                                    </td>
                                                    <td>
                                                        Paneer
                                                    </td>
                                                    <td>
                                                       20
                                                    </td>
                                                    <td>
                                                        <a id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkUpdate" title="Update" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkUpdate&#39;,&#39;&#39;)"><i class="fa fa-pencil"></i></a>
                                                        &nbsp;&nbsp;&nbsp;<a onclick="return confirm(&#39;Are you sure to Delete?&#39;);" id="ctl00_ContentBody_gvOpeningStock_ctl02_lnkDelete" title="Delete" href="javascript:__doPostBack(&#39;ctl00$ContentBody$gvOpeningStock$ctl02$lnkDelete&#39;,&#39;&#39;)" style="color: red;"><i class="fa fa-trash"></i></a>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
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

