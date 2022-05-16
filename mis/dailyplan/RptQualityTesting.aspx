<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptQualityTesting.aspx.cs" Inherits="mis_dailyplan_RptQualityTesting" %>

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

        .billbybill {
            background-color: #e2e2e2;
            color: black;
        }

        .ledger {
            background-color: #e6e6e6;
        }

        .ChildGrid th {
            background-color: #b1a5a56b !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Section Wise QC Testing Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <span id="ctl00_ContentBody_lblMsg"></span>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Product Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlProductSection" runat="server" CssClass="form-control"></asp:DropDownList>

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

                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Search" ClientIDMode="Static" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" Visible="true" class="table table-hover table-bordered table-striped " ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static"
                                        AutoGenerateColumns="False" DataKeyNames="LabTest_ID" OnRowCommand="GridView1_RowCommand">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <%-- <asp:Label ID="lblLabTest_ID" CssClass="hidden" Text='<%# Eval("LabTest_ID") %>' runat="server" />--%>
                                                    <asp:Label ID="lblLabTest_ID" CssClass="hidden" Text='<%# Eval("LabTest_ID") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Particular">
                                                <ItemTemplate>
                                                    <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("LabTest_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ShiftName" HeaderText="Shift" />
                                            <asp:BoundField DataField="SampleBatch_No" HeaderText="Batch No." />
                                            <asp:BoundField DataField="SampleLot_No" HeaderText="Lot No." />

                                            <asp:BoundField DataField="SampleQuantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="SampleName" HeaderText="Sample No" />
                                            <asp:BoundField DataField="Test_Date" HeaderText="Test Date" />
                                            <asp:BoundField DataField="Test_Result" HeaderText="Result" HtmlEncode="false" />
                                            <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="label label-info" CommandName="View" HeaderText="Action" Text="Result" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="ModalResult" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
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
                                                        <asp:Label ID="lblLabTestResult" Text='<%# Eval("TestResultValue").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                </div>

                            </div>
                            <div class="modal-footer">
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

        function ModalResultFun() {
            $("#ModalResult").modal({
                backdrop: 'static',
                keyboard: false
            });
        }

        document.onkeydown = handleKeyDown;
        function HideBillByBill() {
            //$(".BillByBill").css("display", "block");
            $(".BillByBill").css("display", "none");
        }

        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlDS.ClientID%>').selectedIndex == 0) {
                msg += "Select Dugdh Sangh. \n";
            }
            if (document.getElementById('<%=ddlProductSection.ClientID%>').selectedIndex == 0) {
                msg += "Select Product Section. \n";
            }
           <%-- if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Select Shift. \n";
            }--%>

            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "Select Date \n";
            }
            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }
        }
    </script>
</asp:Content>





