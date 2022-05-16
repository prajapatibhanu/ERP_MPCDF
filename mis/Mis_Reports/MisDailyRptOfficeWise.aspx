<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MisDailyRptOfficeWise.aspx.cs" Inherits="mis_MisDailyRptOfficeWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Add" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-Manish">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Daily Report Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Daily Report Details</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtReportDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Select Date" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>

                                                <asp:TextBox ID="txtReportDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>



                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Office Name<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvdofficelist" runat="server" Display="Dynamic" ControlToValidate="ddlOfficeName" Text="<i class='fa fa-exclamation-circle' title='Select Machine Name!'></i>" ErrorMessage="Select Office Name" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                            </span>

                                             <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control select2">
                                               
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><span style="color: red;"></span></label>
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-primary" Text="Search" ValidationGroup="Add" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>


                            </fieldset>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-bottom: 15px">
                            <div class="form-group">
                                <asp:Button ID="btnPrint" CssClass="btn btn-primary" Visible="false" Text="Print" runat="server" OnClientClick="Print()" />
                                <asp:Button ID="btnExcel" CssClass="btn btn-primary" Visible="false" Text="Excel" runat="server" OnClick="btnExcel_Click" />
                            </div>

                        </div>

                    </div>
                    <div class="row">
                        <div id="div_page_content" runat="server" class="NonPrintable">
                            <div class="col-md-12" id="gridShowdata" runat="server">

                                <%--<table class="table table-bordered" >
                                <tr>
                                    <th>Particulars</th>
                                    <th>BDS</th>
                                    <th>IDS</th>
                                    <th>UDS</th>
                                    <th>GDS</th>
                                    <th>JDS</th>
                                    <th>BKD</th>
                                    <th>Total</th>
                                </tr>
                                <tr>
                                    <td>Current TGT</td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTBDS" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTIDS" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTUDS" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTGDS" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTJDS" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTBKD" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCurrentTGTTotal" runat="server" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>On Reporting Date</td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="0"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="0"></asp:Label></td>

                                </tr>
                                <%-- <tr>
                                    <td>Fat %</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="0"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Table Butter</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_TableButter" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Sweet SMP</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_SweetSMP" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>ShriKhand</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_ShriKhand" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Paneer</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Paneer" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Flavoured Milk</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_FlavouredMilk" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Butter Milk</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_ButterMilk" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Lassi</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Lassi" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Peda</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Peda" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>SweetCurd</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_SweetCurd" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td>Plain Curd</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_PlainCurd" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td>Probiotic Curd</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_ProbioticCurd" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Chhena Kheer/ Rabadi</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_ChhenaKheer_Rabadi" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Khowa(Mawa)</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Khowa_Mawa" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Rasgulla</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Rasgulla" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Gulab Jamun</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_GulabJamun" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Milk Cake</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_MilkCake" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Chakka</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Chakka" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Thandai Pet Bottle</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_Thandai" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>F/Milk Bottle</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_F_MilkBottle" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Lassi Lite</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_LassiLite" CssClass="form-control" Text="0" runat="server" MaxLength="20" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Nariyal Barfi</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_NariyalBarfi" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Gulab Jamun Mix</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_GulabJamunMix" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Paneer Achaar</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_PaneerAchaar" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Coffee Mix Powder</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_CoffeeMixPowder" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Cooking Butter</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_CookingButter" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Low Fat Paneer</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_LowFatPaneer" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Peda Prasad</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_PedaPrasad" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Sanchi Ice- Cream</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_SanchiIceCream" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Sanchi Golden Milk</td>
                                    <td>
                                        <asp:TextBox ID="txtProductSale_SanchiGoldenMilk" CssClass="form-control" Text="0" runat="server" MaxLength="20" onpaste="return false;" onkeypress="return validateNum(event)"></asp:TextBox></td>
                                </tr>
                            </table>--%>
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
         function validateform() {
             var msg = "";

             if (document.getElementById('<%=txtReportDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Report Date. \n";
            }
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == "") {
                msg = msg + "Select Office Name. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Search") {
                    if (confirm("Do you really want to Search Records ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
    </script>
   

   
    

    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        function Print() {
            window.print();
        }
    </script>
</asp:Content>

