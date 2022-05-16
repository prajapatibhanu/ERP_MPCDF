<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyPlan.aspx.cs" Inherits="mis_dailyplan_DailyPlan" %>

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

        .col-md-12.link_opening_balance.text-center a {
            font-size: 14px;
            color: #044782;
            text-shadow: 1px 1px 3px #ccbbbb;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Daily Production Plan / दैनिक उत्पादन योजना</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <span id="ctl00_ContentBody_lblMsg"></span>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>तारीख / Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" onpaste="return false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Shift /  पारी<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success btnmargin" Text="Submit" ClientIDMode="Static" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <hr />
                        <div class="col-md-12 link_opening_balance text-center" id="link_opening_balance" runat="server">

                            <div class="col-md-3">
                                <b><a href="#" onclick="openAvailabilityModal()">View availability of milk </a></b>
                            </div>
                            <div class="col-md-1">| </div>
                            <div class="col-md-4">
                                <b><a href="#">View availability of processed milk </a></b>
                            </div>
                            <div class="col-md-1">| </div>
                            <div class="col-md-3">
                                <b><a href="#">View availability of products </a></b>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <br />
                    </div>

                    <div class="row">
                        <hr />
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <h4 class="text-bold text-center"><u>Daily Production Plan / दैनिक उत्पादन योजना :</u></h4>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <h5 class="text-center" runat="server" visible="false" id="headingmilk"><b><u>Milk Section:</u></b></h5>
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridView1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" DataKeyNames="Item_id" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particular">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("Item_id")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TotalDemand" HeaderText="Demand (No.)" />
                                                    <asp:BoundField DataField="TotalDemandLast" HeaderText="Last Day Demand (No.)" />
                                                    <asp:TemplateField HeaderText="Recipe" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbAction" CssClass="label label-info" runat="server" CommandName="select">View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target (No.)">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblGenStatus" runat="server" CssClass="hidden" Text='<%# Eval("GenStatus").ToString()%>' />--%>
                                                            <asp:Label ID="lblProductSection_ID" runat="server" CssClass="hidden" Text='<%# Eval("ProductSection_ID").ToString()%>' />
                                                            <asp:TextBox ID="txtTarget" runat="server" ClientIDMode="Static" onkeypress="return validateNum(event)" Text='<%#Eval("LastTarget") %>' CssClass="form-control" placeholder="Enter Target"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <h5 class="text-center" runat="server" visible="false" id="headingproduct"><b><u>Product Section:</u></b></h5>
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridView2" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" DataKeyNames="Item_id"
                                                runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true"
                                                ClientIDMode="Static" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particular">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("Item_id")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ProductSection_Name" HeaderText="Section" />
                                                    <asp:BoundField DataField="TotalDemand" HeaderText="Demand (No.)" />
                                                    <asp:BoundField DataField="TotalDemandLast" HeaderText="Last Day Demand (No.)" />
                                                    <asp:TemplateField HeaderText="Recipe" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbAction" CssClass="label label-info" runat="server" CommandName="select">View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target (No.)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductSection_ID" runat="server" CssClass="hidden" Text='<%# Eval("ProductSection_ID").ToString()%>' />
                                                            <asp:TextBox ID="txtPTarget" runat="server" Text='<%#Eval("LastTarget") %>' ClientIDMode="Static" onkeypress="return validateNum(event)" CssClass="form-control" placeholder="Enter Product Target"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <%--                                    <input type="button" runat="server" id="f" name="" visible="false" value="Save Production Plan" class="btn btn-block btn-success" onclick=""  style="margin-top: 23px;" />--%>
                                    <asp:Button ID="btnSave" runat="server" Text="Save Production Plan" Visible="false" class="btn btn-block btn-success" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <hr />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <h4 class="text-bold text-center"><u>As per the target, required item for production :</u></h4>
                        </div>
                        <div class="row">

                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <h5 class="text-center" runat="server" visible="false" id="headingmilk2"><b><u>Milk Section:</u></b></h5>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GridView3" runat="server" class="table table-hover table-bordered table-striped pagination-ys"
                                            ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False" DataKeyNames="Item_id"
                                            OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particular">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("Item_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Detail" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbAction" CssClass="label label-info" runat="server" CommandName="select">View</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="TotalItemRequired" HeaderText="Required Quantity" />
                                                <asp:TemplateField HeaderText="Buffer Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBufferQuantity" runat="server" Text="0"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item to be issue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAcutalInv" runat="server" Text='<%# Eval("TotalItemRequired") %>' CssClass="form-control" placeholder=" "></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-6">
                                <div class="col-md-12">
                                    <h5 class="text-center" runat="server" visible="false" id="headingproduct2"><b><u>Product Section:</u></b></h5>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GridView4" runat="server" class="table table-hover table-bordered table-striped pagination-ys "
                                            ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False" DataKeyNames="Item_id"
                                            OnSelectedIndexChanged="GridView4_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particular">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ItemName" runat="server" Text='<%# Eval("ItemName") %>' ToolTip='<%# Eval("Item_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Detail" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbAction" CssClass="label label-info" runat="server" CommandName="select">View</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="TotalItemRequired" HeaderText="Required Quantity" />
                                                <asp:TemplateField HeaderText="Buffer Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBufferQuantity" runat="server" Text="0"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item to be issue">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAcutalInv" runat="server" Text='<%# Eval("TotalItemRequired") %>' CssClass="form-control" placeholder=" "></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Target">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPTarget" runat="server" Text='<%#Eval("TotalItemRequired") %>' CssClass="form-control" placeholder=""></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="btnVerifyItem" runat="server" Text="Verify items for production" Visible="false" class="btn btn-block btn-success" OnClick="btn_VerifyItem" />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>


                <div id="myModalProduct" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <asp:GridView ID="GridView6" CssClass="table" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="Recipe Not Set">
                                        <Columns>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item / Ingredient Name" />
                                            <asp:BoundField DataField="Item_Quantity" HeaderText="Item / Ingredient Quantity Required For Single Unit Production" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Item / Ingredient Unit" />
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-md-8"></div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <button type="button" class="btn btn-block btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <asp:GridView ID="GridView5" CssClass="table" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                            <asp:BoundField DataField="inSingleUnit" HeaderText="Item / Ingredient Required For Single Unit Production" />

                                            <asp:BoundField DataField="ProductTarget" HeaderText="Production Target (No.)" />
                                            <asp:BoundField DataField="TotalItemRequired" HeaderText="Total Required Quantity" />
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-md-8"></div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <button type="button" class="btn btn-block btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div id="myAvailabilityModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">
                                    <asp:Label ID="Label1" runat="server" Text="View availability of milk"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">

                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Particular</th>
                                                <th>Available Quantity</th>
                                                <th>Fat Ratio</th>
                                                <th>SNF Ratio</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Whole Milk</td>
                                                <td>215000 Liter</td>
                                                <td>5.75</td>
                                                <td>8.07</td>
                                            </tr>

                                        </tbody>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <div class="row">
                                        <div class="col-md-8"></div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-block btn-default" data-dismiss="modal">Close</button>
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
    <script>

        function openAvailabilityModal() {
            $("#myAvailabilityModal").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
        function callalert() {
            $("#myModal").modal('show');
        }

        function callalertproduct() {
            $("#myModalProduct").modal('show');
        }

        function validateform() {
            //alert();
            var msg = "";
            if (document.getElementById('<%=ddlShift.ClientID%>').selectedIndex == 0) {
                msg += "Please Select Shift. \n"
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg = "Please Select Date."
            }

            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

