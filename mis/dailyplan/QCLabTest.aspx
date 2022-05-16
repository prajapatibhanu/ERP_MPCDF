<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="QCLabTest.aspx.cs" Inherits="mis_dailyplan_QCLabTest" %>

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
                    <h3 class="box-title">Quality Control</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <span id="ctl00_ContentBody_lblMsg"></span>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Product Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <small><span id="valddlProductSection" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                    <small><span id="valOrderDate" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                <small><span id="valShift" class="text-danger"></span></small>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Search" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <h4 class="text-center" runat="server" visible="true" id="pendingsample"><b><u>Pending Samples Of Date
                                            <span style="color: orange; font-weight: bold">
                                                <asp:Label ID="lblSelectedDate1" runat="server" Text=""></asp:Label></span></u></b></h4>
                                        <div class="table table-responsive">

                                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys "
                                                ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False"
                                                DataKeyNames="LabTest_ID" OnRowCommand="GridView1_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            <asp:Label ID="lblGLabTest_ID" CssClass="hidden" runat="server" Text='<%# Eval("LabTest_ID") %>'></asp:Label>
                                                            <asp:Label ID="lblGItem_id" CssClass="hidden" runat="server" Text='<%# Eval("Item_id") %>'></asp:Label>
                                                            <asp:Label ID="lblGSampleBy_Section" CssClass="hidden" runat="server" Text='<%# Eval("SampleBy_Section") %>'></asp:Label>
                                                            <asp:Label ID="lblGSampleDate" CssClass="hidden" runat="server" Text='<%# Eval("SampleDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particular">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("LabTest_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sample Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSampleQuantity" runat="server" Text='<%# Eval("SampleQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sample Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSampleName" runat="server" Text='<%# Eval("SampleName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSampleBatch_No" runat="server" Text='<%# Eval("SampleBatch_No") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lot No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSampleLot_No" runat="server" Text='<%# Eval("SampleLot_No") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="label label-info" CommandName="View" HeaderText="Update Result" Text="Update Result" />
                                                    <%-- <asp:BoundField DataField="SampleQuantity" HeaderText="Sample Quantity" />
                                                    <asp:BoundField DataField="SampleName" HeaderText="Sample Name" />
                                                    <asp:BoundField DataField="SampleBatch_No" HeaderText="Batch No" />
                                                    <asp:BoundField DataField="SampleLot_No" HeaderText="Lot No" /><asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbAction" CssClass="label label-info" runat="server" CommandName="select">Update Result</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <h4 class="text-bold text-center"><u>Test Result Updated For Sample Sent On  <span style="color: orange; font-weight: bold">
                                    <asp:Label ID="lblSelectedDate" runat="server" Text=""></asp:Label></span></u></h4>

                                <div class="table table-responsive">
                                    <asp:GridView ID="GridView2" runat="server" Visible="true"
                                        class="table table-hover table-bordered table-striped pagination-ys "
                                        ShowHeaderWhenEmpty="true" ClientIDMode="Static"
                                        AutoGenerateColumns="False" DataKeyNames="LabTest_ID" OnRowDeleting="GridView2_RowDeleting">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Particular">
                                                <ItemTemplate>
                                                    <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("LabTest_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="SampleBatch_No" HeaderText="Batch No." />
                                            <asp:BoundField DataField="SampleLot_No" HeaderText="Lot No." />

                                            <asp:BoundField DataField="TotalFat" HeaderText="Total FAT (Gram)" />
                                            <asp:BoundField DataField="TotalSnf" HeaderText="Total SNF (Gram)" />

                                            <asp:BoundField DataField="SampleQuantity" HeaderText="QC Sample Quantity(Packet)" />
                                            <asp:BoundField DataField="SampleName" HeaderText="QC Sample No" />
                                            <asp:BoundField DataField="Test_Date" HeaderText="Test Date" />
                                            <asp:BoundField DataField="Test_Result" HeaderText="Result" HtmlEncode="false" />
                                           <%-- <asp:TemplateField HeaderText="Test Values" HeaderStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="StringValue" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode((string)Eval("StringValue")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <%--<asp:BoundField DataField="StringValue" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("KeyCode")) %>' HeaderText="Test Values" HtmlEncode="false" />--%>
                                            <%--<asp:BoundField DataField="Test_Remark" HeaderText="Test Remark (Test No.)" />--%>



                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-warning" CausesValidation="False" CommandName="Delete" Text="Reset" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div id="ModalAttendence" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></h4>
                                    <asp:Label ID="lblMsgTest" runat="server" ClientIDMode="Static" Text=""></asp:Label><br />
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="QCParameterID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("QCParameterID").ToString()%>' runat="server" />
                                                            <asp:Label ID="lblMinValueValid" CssClass="hidden" Text='<%# Eval("MinValueValid").ToString()%>' runat="server" />
                                                            <asp:Label ID="lblMaxValueValid" CssClass="hidden" Text='<%# Eval("MaxValueValid").ToString()%>' runat="server" />
                                                             <asp:Label ID="lblQCParameterID" CssClass="hidden" Text='<%# Eval("QCParameterID").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Parameter Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCParameterName" Text='<%# Eval("QCParameterName").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Calculation Method">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCalculationMethod" Text='<%# Eval("CalculationMethod").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Min. Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMinValue" Text='<%# Eval("MinValue").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Max. Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaxValue" Text='<%# Eval("MaxValue").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Test Result">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLabTestResult" runat="server" CssClass="form-control" placeholder="Test Values..."  onkeypress="return validate3Dec(this,event);" MaxLength="12"></asp:TextBox>
                                                            <asp:TextBox ID="txtDateLabTestResult" runat="server" CssClass="form-control DateAdd" placeholder="Select Date..." ></asp:TextBox>
                                                            <asp:DropDownList ID="ddlLabTestResult" runat="server" CssClass="form-control" ClientIDMode="Static">                                                               
                                                            </asp:DropDownList>
                                                             <asp:Label ID="lblTestResultValue" CssClass="hidden" Text='<%# Eval("TestResultValue").ToString()%>' runat="server" />
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <%--             <asp:GridView ID="GridView3" runat="server" class="table table-hover table-bordered table-striped pagination-ys "
                                                ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False"
                                                EmptyDataText="No Record Found">
                                                <Columns>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particular">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%# Eval("QCParameterName") %>' ToolTip='<%# Eval("QCParameterID")%>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Test Values">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLabTestResult" runat="server" Text='<%#Eval("LabTestResult") %>' CssClass="form-control" placeholder="Test Values"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>--%>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-3"></div>

                                                <div class="col-md-3" style="text-align: right"><b>Test Result :</b></div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnCalculateResult" runat="server" class="btn btn-block btn-default" Text="Calculate Result" OnClick="btnCalculateResult_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlTestReport" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>



                </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        // onkeypress="return validate3Dec(this,event)"
        function validate3Dec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 2)) {
                return false;
            }
            return true;
        }

        $(document).ready(function () {
            $('.loader').fadeOut();
        });

        function ModalAttendenceFun(msg) {
            //$("#ModalAttendence").modal('show');
            $("#ModalAttendence").modal({
                backdrop: 'static',
                keyboard: false
            });
            if (msg != "")
                alert(msg);
        }

        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView3 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


        $(document).ready(function () {


            var checkbox = $('table tbody input[type="checkbox"]:disabled');
            for (var i = 0; i < checkbox.length; i++) {
                $(checkbox[i]).parents('tr').css('background-color', 'rgba(255, 24, 0, 0.55)');
                //    $(checkbox[i]).parents('tr').css('color', '#FFFFFF');

                //$('table tbody input[type="checkbox"]').css('width', '25px');
            }



        });

        function getConfirmation() {
            debugger;
            var retVal = confirm("Record will be reset. Are you sure want to continue?");
            if (retVal == true) {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
            else {

                return false;
            }
        }

        function validateform() {
            debugger;
            var msg = "";
            $("#valddlDS").html("");
            $("#valddlProductSection").html("");
            if (document.getElementById('<%=ddlDS.ClientID%>').selectedIndex == 0) {
                msg += "Select Dugdh Sangh. \n"
                $("#valddlDS").html("Select Dugdh Sangh");
            }
            if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n"
                $("#valddlProductSection").html("Select Product Section");
            }
            if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Select Shift. \n"
                $("#valShift").html("Select Shift");
            }
<%--            if (document.getElementById('<%=txtBatchNo.ClientID%>').value.trim() == "") {
                msg += "Enter Batch No. \n"
                $("#valBatchNo").html("Enter Batch No.");
            }
            if (document.getElementById('<%=txtLotNo.ClientID%>').value.trim() == "") {
                msg += "Enter Lot No. \n"
                $("#valLotNo").html("Enter Lot No.");
            }--%>
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "Select Date \n"
                $("#valOrderDate").html("Select Date");
            }
            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }
        }
    </script>
</asp:Content>

