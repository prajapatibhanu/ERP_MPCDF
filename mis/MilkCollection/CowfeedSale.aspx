<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CowfeedSale.aspx.cs" Inherits="mis_MilkCollection_CowfeedSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
            <div class="content-wrapper">

            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">Cow feed Sale</h3>
                                <span id="ctl00_ContentBody_lblmsg"></span>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Member ID<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtmobno" type="text" maxlength="10" id="ctl00_ContentBody_txtmobno" class="form-control" placeholder="Member ID" onkeypress="javascript:tbx_fnNumeric(event, this);" />
                                            <small><span id="valtxtmobno" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Member Name<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtmobno" type="text" maxlength="10" id="ctl00_ContentBody_txtmobno" class="form-control" placeholder="Member Name" onkeypress="javascript:tbx_fnNumeric(event, this);" />
                                            <small><span id="valtxtmobno" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Member Type<span class="text-danger">*</span></label>
                                            <select name="ctl00$ContentBody$ddlstate" id="ctl00_ContentBody_ddlstate" class="form-control Select2">
                                                <option value="0">Select</option>
                                                <option value="1">DCS</option>
                                                <option value="2">Producer</option>
                                            </select>
                                            <small><span id="valddlstate" class="text-danger"></span></small>
                                        </div>
                                    </div> 
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Product Name<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtmobno" type="text" maxlength="10" id="ctl00_ContentBody_txtmobno" class="form-control" placeholder="Product Name" onkeypress="javascript:tbx_fnNumeric(event, this);" />
                                            <small><span id="valtxtmobno" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Qty. (kg)<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtmobno" type="text" maxlength="10" id="ctl00_ContentBody_txtmobno" class="form-control" placeholder="Qt (kg)" onkeypress="javascript:tbx_fnNumeric(event, this);" />
                                            <small><span id="valtxtmobno" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Value(Rs)<span class="text-danger">*</span></label>
                                            <input name="ctl00$ContentBody$txtemail" type="text" maxlength="50" id="ctl00_ContentBody_txtemail" class="form-control" placeholder="Value(Rs)" />
                                            <small><span id="valtxtemail" class="text-danger"></span></small>
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <h3 class="box-title">Cow feed Sale Details </h3>
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
                                                            <th scope="col">Member ID<br /></th>
                                                            <th scope="col">Member Name<br /></th>
                                                            <th scope="col">Member Type<br /></th>
                                                            <th scope="col">Product Name<br /></th>
                                                            <th scope="col">Qty. (Kg)<br /></th>
                                                            <th scope="col">Value(Rs.) <br /></th>
                                                            <th scope="col">Action <br /></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                1
                                                            </td>
                                                            <td>
                                                                1001
                                                            </td>
                                                            <td>
                                                                ingoria
                                                            </td>
                                                            <td>
                                                                DCS
                                                            </td>
                                                            <td>
                                                                Cow Feed
                                                            </td>
                                                            <td>
                                                                55
                                                            </td>
                                                            <td>
                                                                45
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
                                                                1002
                                                            </td>
                                                            <td>
                                                                Paraswada
                                                            </td>
                                                            <td>
                                                                Famer
                                                            </td>
                                                            <td>
                                                                Medicines
                                                            </td>
                                                            <td>
                                                                55
                                                            </td>
                                                            <td>
                                                                45
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


                    </div>
                </div>
            </section>
        </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

