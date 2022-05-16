<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyProductionReprocess.aspx.cs" Inherits="mis_dailyplan_DailyProductionReprocess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        li.header {
            font-size: 14px !important;
            color: #F44336 !important;
        }

        span#ctl00_spnUsername {
            text-transform: uppercase;
            font-weight: 600;
            font-size: 16px;
        }

        li.dropdown.tasks-menu.classhide a {
            padding: 4px 10px 0px 0px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        .navbar {
            background: #ebf4ff !important;
            box-shadow: 0px 0px 8px #0058a6;
            color: #0058a6;
        }

        .skin-green-light .main-header .logo {
            background: #fff !important;
        }


        a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
            box-shadow: 1px 1px 1px #808080;
        }

        a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
            box-shadow: 1px 1px 1px #808080;
        }

        .btn-success {
            background-color: #1d7ce0;
            border-color: #1d7ce0;
        }

            .btn-success:hover, .btn-success:active, .btn-success.hover, .btn-success:focus, .btn-success.focus, .btn-success:active:focus {
                background-color: #1d7ce0;
                border-color: #1d7ce0;
            }

            .btn-success:hover {
                color: #fff;
                background-color: #1d7ce0;
                border-color: #1d7ce0;
            }

        fieldset {
            border: 1px solid #ff7836;
            padding: 15px;
            margin-bottom: 15px;
        }

        legend {
            width: initial;
            padding: 4px 15px;
            margin: 0;
            font-size: 12px;
            font-weight: bold;
            color: #00427b;
            text-transform: uppercase;
            border: 1px solid #ff7836;
        }

        table .select2 {
            width: 100px !important;
        }

        .btnmargin {
            margin-top: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Reprocess</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <span id="ctl00_ContentBody_lblMsg"></span>
                <div class="box-body">



                    <div class="row">
                        <h4 class="text-center text-orange"><u>Production Detail</u></h4>
                        <table class="table">
                            <tr>
                                <td>Particular</td>
                                <td>Sanchi Gold 500 Ml</td>
                                <td>Production Date</td>
                                <td>28/05/2020</td>
                            </tr>
                            <tr>
                                <td>Production Quantity</td>
                                <td>1500</td>
                                <td>Shift</td>
                                <td>Morning</td>
                            </tr>
                            <tr>
                                <td>Batch No</td>
                                <td>B-2020-2020</td>
                                <td>Lot No</td>
                                <td>L-105-456</td>
                            </tr>
                            <tr>
                                <td>Lab Test Result</td>
                                <td><span class="badge bg-red">Fail</span></td>
                                <td>Lab Test Remark</td>
                                <td>Fat Less Than Recommentation</td>
                            </tr>
                        </table>
                        <h4 class="text-center text-orange"><u>Add Supplements</u></h4>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Item/Ingredients/Supplements<span class="text-danger">*</span></label>
                                <select name="ctl00$ContentBody$ddlItem" onchange="javascript:setTimeout('__doPostBack(\'ctl00$ContentBody$ddlItem\',\'\')', 0)" id="ctl00_ContentBody_ddlItem" class="form-control">
                                    <option value="0">Select</option>
                                    <option selected="selected" value="45">Sanchi Gold 500 Ml</option>
                                    <option value="54">Loos Curd </option>
                                    <option value="55">loose Butter</option>
                                    <option value="56">Packet 2 * 3</option>
                                    <option value="57">Packet 6* 10</option>
                                    <option value="58">Cup 200 grm</option>
                                    <option value="59">Cup 500 Gram</option>
                                    <option value="60">Pouch 1 ltr</option>
                                    <option value="61">Tin 5 Kg</option>
                                    <option value="62">Cow Milk  1 Ltr</option>
                                    <option value="63">Ghee 1 Kg</option>
                                    <option value="64">Ghee 5 Kg</option>
                                    <option value="65">Shree Khand 200 Gram</option>
                                    <option value="66">Shree Khand 500 Gram</option>
                                    <option value="46">Sanchi Gold 200 Ml</option>
                                    <option value="47">Sanchi 1 Ltr</option>
                                    <option value="48">Sanchi Chena Rabdi 200 Grm</option>
                                    <option value="49">Lassi 200 Ml.</option>
                                    <option value="50">FAT</option>
                                    <option value="51">SNF</option>
                                    <option value="52">Whole Milk</option>
                                    <option value="53">Packet 4*6</option>
                                    <option value="67">Plain Curd 200 Gram</option>
                                    <option value="68">Plain Curd 500 Gram</option>
                                    <option value="69">Butter 100 Gram</option>
                                    <option value="70">Spice Butter Milk 200 Gram</option>
                                    <option value="71">Spiced Butter Milk 500 Gram</option>
                                    <option value="72">Milk Powder</option>
                                    <option value="73">Loose Curd</option>
                                    <option value="74">Loose Butter</option>
                                    <option value="75">sugar</option>
                                    <option value="76">Elaichi</option>
                                    <option value="77">Salt</option>
                                    <option value="78">Jira </option>
                                    <option value="79">Water</option>
                                    <option value="80">Raw Milk</option>

                                </select>
                                <small><span id="valddlItem" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Quantity<span class="text-danger">*</span></label>
                                <input name="ctl00$ContentBody$txtQuantity" type="text" id="ctl00_ContentBody_txtQuantity" class="form-control">
                                <small><span id="valtxtQuantity" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Quantity Unit</label>
                                <select name="ctl00$ContentBody$ddlUnit" id="ctl00_ContentBody_ddlUnit" class="form-control" readonly="readonly">
                                    <option value="20">Mililitre</option>
                                    <option value="20">KG</option>
                                    <option value="20">Gram</option>
                                    <option value="20">Liter</option>
                                    <option value="20">Percentage</option>

                                </select>
                                <small><span id="valddlUnit" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <input type="submit" name="ctl00$ContentBody$btnSubmit" value="Add" style="margin-top: 19px;" onclick="return validateform();" id="btnSubmit" class="btn btn-block btn-success">
                            </div>
                        </div>

                    </div>

                    <div class="col-md-10">
                        <table class="table">
                            <tr>
                                <th>SNo.</th>
                                <th>Supplement</th>
                                <th>Quantity</th>
                                <th>Unit</th>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>Loose Butter</td>
                                <td>150</td>
                                <td>Gram</td>
                            </tr>
                        </table>
                    </div>
                    <div class="clearfix"></div>
                    <h4 class="text-center text-orange"><u>Production After Processing</u></h4>
                    <div class="clearfix"></div>


                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Production Quantity (Packet)<span class="text-danger">*</span></label>
                            <input name="" type="text" id="" class="form-control">
                            <small><span id="" class="text-danger"></span></small>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Batch No<span class="text-danger">*</span></label>
                            <input name="" type="text" id="" class="form-control">
                            <small><span id="" class="text-danger"></span></small>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Lot No<span class="text-danger">*</span></label>
                            <input name="" type="text" id="" class="form-control">
                            <small><span id="" class="text-danger"></span></small>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>QC Sample Quantity(Packet)<span class="text-danger">*</span></label>
                            <input name="" type="text" id="" class="form-control">
                            <small><span id="" class="text-danger"></span></small>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>QC Sample No<span class="text-danger">*</span></label>
                            <input name="" type="text" id="" class="form-control">
                            <small><span id="" class="text-danger"></span></small>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <input type="submit" name="ctl00$ContentBody$btnSubmit" value="Save Production Detail" style="margin-top: 19px;" onclick="return validateform();" id="btnSubmit" class="btn btn-block btn-success">
                        </div>
                    </div>


                </div>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

