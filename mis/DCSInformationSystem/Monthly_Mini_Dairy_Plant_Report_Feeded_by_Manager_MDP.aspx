<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Monthly_Mini_Dairy_Plant_Report_Feeded_by_Manager_MDP.aspx.cs" Inherits="mis_DCSInformationSystem_Monthly_Mini_Dairy_Plant_Report_Feeded_by_Manager_MDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <%-- <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />--%>
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-body">
                <div class="box-header">
                    <h3 class="box-title">Monthly Mini Dairy Plant Report Feeded by Manager MDP</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Monthly Mini Dairy Plant Report
                        </legend>
                        <div class="row">
                            <div class="col-md-12">
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label88" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth" runat="server"  CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label89" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="ddlyear" runat="server"  CssClass="form-control" >
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </br>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblDCScode" Text="(1) Opening Balance Of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label41" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox37" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label46" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox38" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label47" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox39" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <fieldset>
                                <legend>(2) Milk Purchase From DCS
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label48" Text="(i) Good" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label49" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox40" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label50" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox41" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label51" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox42" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label52" Text="(ii) Sour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>


                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox1" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox2" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox3" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label56" Text="(iii) Curdle" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>


                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label4" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox4" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label5" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox5" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label6" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox6" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label61" Text="(3) Milk Received from Other CC/DP" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label62" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox43" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label63" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox52" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label64" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox53" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label65" Text="(4) Milk Sale Return" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label66" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox54" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label67" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox55" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label68" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox56" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <fieldset>
                                <legend>(5) SMP Used for Recombination
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label74" Text="(i) Milk Inflow (1 to 5)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label75" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox60" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label76" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox61" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label77" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox62" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                       <%-- </div>

                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" Text="(6) S/C milk allocated for product" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox7" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox8" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox9" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <%--</div>
                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label33" Text="(7)Good Milk Allocated for Product" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label34" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox25" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label35" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox26" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox10" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                       <%-- </div>--%>


                        <fieldset>
                            <legend>(8) Milk Packed
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label40" Text="(i) Whole Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label13" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox11" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox12" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label15" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox13" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                           
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label44" Text="(ii) Standarised" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox14" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label17" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox15" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label18" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox16" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label54" Text="(iii) Chah" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox17" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox18" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label21" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox19" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label22" Text="(iv) Toned" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label23" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox20" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label24" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox21" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label25" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox22" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label26" Text="(v)Double Toned" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label27" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox23" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label28" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox24" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label29" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox27" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label30" Text="(vi) Skimmed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label31" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox28" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label36" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox29" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label37" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox30" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label38" Text="(vii) Chah Special" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label42" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox31" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label43" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox32" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label45" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox33" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                 </div>
                        </fieldset>

                        <fieldset>
                            <legend>(9) Bulk Milk Sold
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" Text="(i) Whole Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label32" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox34" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label39" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox35" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label53" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox36" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label58" Text="(ii) Skimmed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label55" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox44" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label57" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox45" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label59" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox57" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label60" Text="(10) Milk Despatch to Dairy Plant" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label69" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox46" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label70" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox47" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label71" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox48" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <fieldset>
                            <legend>(11) Closing Balance of Milk
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label72" Text="(i) Milk Outflow (6 to 11)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label73" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox49" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label78" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox50" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label79" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox51" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>(12) Milk Processing Variation
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label80" Text="(i) Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label81" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox58" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label82" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox59" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label83" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox63" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label84" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label85" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox64" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label86" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox65" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label87" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox66" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>(13) Indigenous Product Manufactured
                            </legend>
                            <div class="row">
                            <fieldset>
                                <legend>(i) Butter Milk
                                </legend>
                                 <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label92" Text="(a) Spiced" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label93" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox70" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label94" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox71" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label95" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox72" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label96" Text="(b) Plain" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label97" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox73" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label98" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox74" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label99" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox75" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                     </div>
                            </fieldset>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label100" Text="(ii) Curd" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label101" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox76" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label102" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox77" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label103" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox78" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label104" Text="(iii) Shrikhand" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label105" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox79" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label106" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox80" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label107" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox81" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label108" Text="(iv) Lassi" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label109" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox82" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label110" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox83" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label111" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox84" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label112" Text="(v) Paneer" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label113" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox85" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label114" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox86" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label115" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox87" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label116" Text="(vi) Peda" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label117" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox88" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label118" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox89" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label119" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox90" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label120" Text="(vii) Other" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label121" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox91" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label122" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox92" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label123" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox93" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </fieldset>


                        <fieldset>
                            <legend>(14) Product Manufacturing Variation from Good Milk
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label124" Text="(i) Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label125" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox94" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label126" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox95" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label127" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox96" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label128" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label129" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox97" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label130" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox98" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label131" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox99" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>(15) White Butter Mfg from S/C Milk
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label132" Text="(i) Opening Balance of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label133" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox100" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label134" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox101" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label135" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox102" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label136" Text="(ii) WB Manufactured" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label137" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox103" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label138" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox104" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label139" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox105" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label140" Text="(iii) Closing Balance of Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label141" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox106" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label142" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox107" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label143" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox108" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>(16) Product Manufacturing Variation from S/C Milk
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label144" Text="(i) Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label145" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox109" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label146" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox110" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label147" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox111" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label148" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label149" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox112" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label150" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox113" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label151" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox114" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                       <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label152" Text="(17) Tanker Milk Received by DP" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label153" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox115" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label154" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox116" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label155" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                    <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox117" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <fieldset>
                            <legend>(18) Tanker Milk Variation
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label156" Text="(i) Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label157" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox118" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label158" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox119" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label159" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox120" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label160" Text="(ii) %" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label161" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox121" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label162" Text="Fat" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox122" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label163" Text="SNF" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox123" MaxLength="150" placeholder="in %" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <fieldset>
                            <legend>(19) Milk & Milk Product Sold
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label164" Text="(i) Whole Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label165" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox124" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label166" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox125" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                         
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label168" Text="(ii) Standarised" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label169" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox127" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label170" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox128" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label172" Text="(iii) Chah" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label173" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox130" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label174" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox131" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label176" Text="(iv) Toned" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label177" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox133" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label178" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox134" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label180" Text="(v) Double Tond" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label181" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox136" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label182" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox137" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label184" Text="(vi) Skimmed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label185" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox139" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label186" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox140" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label188" Text="(vii) Chah Special" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label189" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox142" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label190" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox143" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label192" Text="(viii) Bulk Whole Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label193" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox145" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label194" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox146" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label196" Text="(ix) Bulk Skimmed Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label197" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox148" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label198" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox149" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label200" Text="(x) Spiced Butter Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label201" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox151" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label202" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox152" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label204" Text="(xi) Plain Butter Milk" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label205" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox154" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label206" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox155" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label208" Text="(xii) Curd" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label209" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox157" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label210" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox158" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label212" Text="(xiii) Shrikhand" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label213" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox160" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label214" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox161" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label216" Text="(xiv) Lassi" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label217" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox163" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label218" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox164" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label220" Text="(xv) Paneer" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label221" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox166" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label222" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox167" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label224" Text="(xvi) Peda" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label225" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox169" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label226" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox170" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label228" Text="(xvii) Other" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label229" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox172" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label230" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox173" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                                   </div>
                        </fieldset>

                        <fieldset>
                            <legend>(20) Stock
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label232" Text="(i) Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label233" Text="WB" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox175" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label234" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox176" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label235" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox177" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                         
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label236" Text="(ii) Manufactured" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label237" Text="WB" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox178" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label238" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox179" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label239" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox180" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label240" Text="(iii) Recieved" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label241" Text="WB" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox181" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label242" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox182" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label243" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox183" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label244" Text="(iv) Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label245" Text="WB" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox184" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label246" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox185" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label247" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox186" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label248" Text="(v) Despatch to DP" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label249" Text="WB" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox187" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label250" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox188" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label251" Text="Cattle Feed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox189" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label252" Text="(vi) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label253" Text="WB" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox190" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label254" Text="Ghee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox191" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label255" Text="Cattle" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox192" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                   </div>
                        </fieldset>

                        <fieldset>
                            <legend>(21) Details of Packing Material
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label256" Text="(i) Whole Milk 500 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label257" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox193" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label258" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox194" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label259" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox195" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label167" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox126" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label260" Text="(ii) Standarised 500 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label261" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox196" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label262" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox197" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label263" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox198" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label171" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox129" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label264" Text="(iii) Chah 1 Ltr Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label265" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox199" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label266" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox200" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label267" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox201" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label175" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox132" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label268" Text="(iv) Toned 500 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label269" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox202" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label270" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox203" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label271" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox204" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label179" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox135" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label272" Text="(v) Double Tond 200 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label273" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox205" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label274" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox206" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label275" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox207" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label183" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox138" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label276" Text="(vi) Double Tond 500 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label277" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox208" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label278" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox209" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label279" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox210" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label187" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox141" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label324" Text="(vii) Skimmed 200 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label325" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox244" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label326" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox245" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label327" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox246" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label191" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox144" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label280" Text="(viii) Chah Special 1 Ltr Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label281" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox211" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label282" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox212" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label283" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox213" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label195" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox147" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label284" Text="(ix) Spiced Butter Milk 200 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label285" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox214" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label286" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox215" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label287" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox216" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label199" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox150" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label288" Text="(x) Plain Butter Milk 500 Ml Roll" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label289" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox217" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label290" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox218" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label291" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox219" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label203" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox153" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label292" Text="(xi) Curd 100 Gram Cup" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label293" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox220" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label294" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox221" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label295" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox222" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label207" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox156" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label296" Text="(xii) Curd Corrugated Box" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label297" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox223" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label298" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox224" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label299" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox225" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label211" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox159" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label300" Text="(xiii) Curd 500 Ml Film" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label301" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox226" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label302" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox227" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label303" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox228" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label215" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox162" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label304" Text="(xiv) Shrikhand 100 Gram Cup" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label305" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox229" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label306" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox230" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label307" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox231" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label219" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox165" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label308" Text="(xv) Shrikhand Corrugated Box" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label309" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox232" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label310" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox233" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label311" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox234" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label223" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox168" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label312" Text="(xvi) Lassi 200 Ml Glass" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label313" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox235" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label314" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox236" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label315" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox237" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label227" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox171" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label316" Text="(xvii) Lassi Corrugated Box" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label317" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox238" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label318" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox239" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label319" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox240" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label231" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox174" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label320" Text="(xviii) Paneer Pouches" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label321" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox241" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label322" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox242" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label323" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox243" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label336" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox253" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label328" Text="(xix) Peda 250 Gram Box" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label329" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox247" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label330" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox248" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label331" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox249" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label337" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox254" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label332" Text="(xx) Peda 500 Gram Box" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label333" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox250" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label334" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox251" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label335" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox252" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label338" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox255" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                                </div>
                        </fieldset>

                        <fieldset>
                            <legend>(22) Details of Consumables
                            </legend>
                            <div class="row">
                            <fieldset>
                                <legend>(i) Chemical
                                </legend>
                                <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label344" Text="(a) Acid" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label345" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox260" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label346" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox261" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label347" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox262" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label348" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox263" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label349" Text="(b) Alcohol" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label350" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox264" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label351" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox265" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label352" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox266" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label353" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox267" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                    </div>
                            </fieldset>

                            <fieldset>
                                <legend>(ii) Detergent
                                </legend>
                                <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label359" Text="(a) Soap Solution" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label360" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox272" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label361" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox273" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label362" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox274" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label363" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox275" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label364" Text="(b) Caustic Soda" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label365" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox276" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label366" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox277" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label367" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox278" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label368" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox279" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label369" Text="(c) Washing Soda" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label370" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox280" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label371" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox281" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label372" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox282" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label373" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox283" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                    </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label374" Text="(iii) Sugar" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label375" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox284" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label376" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox285" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label377" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox286" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label378" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox287" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label379" Text="(iv) Elaichee" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label380" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox288" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label381" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox289" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label382" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox290" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label383" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox291" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label384" Text="(v) Jeera" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label385" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox292" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label386" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox293" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label387" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox294" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label388" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox295" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label389" Text="(vi) Kalimirch" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label390" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox296" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label391" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox297" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label392" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox298" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label393" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox299" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label394" Text="(vii) Kala Namak" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label395" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox300" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label396" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox301" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label397" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox302" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label398" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox303" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label399" Text="(viii) Sada Namak" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label400" Text="Opening" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox304" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label401" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox305" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label402" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox306" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label403" Text="Closing" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox307" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </fieldset>

                        <fieldset>
                            <legend>(23) DCS to CC Transportation Expenses
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label404" Text="(i) Head Load" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label405" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox308" MaxLength="150" placeholder="in DCS Nos" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label406" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox309" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label407" Text="(ii) Vehicle" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label408" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox310" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label409" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox311" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </fieldset>

                        <fieldset>
                            <legend>(24) Cattle Feed Transportation
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label410" Text="(i) Vehicle" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label411" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox312" MaxLength="150" placeholder="No." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label412" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox313" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label413" Text="(ii) Loading" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label414" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox314" MaxLength="150" placeholder="Bag" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label415" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox315" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label416" Text="(iii) Unloading" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label417" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox316" MaxLength="150" placeholder="Bag" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label418" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox317" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </fieldset>

                        <fieldset>
                            <legend>(25) Marketing Expenses
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label419" Text="(i) Transportation" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label420" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox318" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label421" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox319" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>
                            
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label422" Text="(ii) Sales Promotion & Others" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label423" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox320" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label424" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox321" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </fieldset>

                        <fieldset>
                            <legend>(26) Expenditures
                            </legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label425" Text="(i) Electricity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label426" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox322" MaxLength="150" placeholder="in KWh" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label427" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox323" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                          
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label428" Text="(ii) Diesel" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label429" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox324" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label430" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox325" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              
                            <fieldset>
                                <legend>(iii) Chemical
                                </legend>
                                  <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label434" Text="(a) Acid" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label435" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox328" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label436" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox329" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label437" Text="(b) Alcohol" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label438" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox330" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label439" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox331" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                      </div>
                            </fieldset>

                            <fieldset>
                                <legend>(iv) Detergent
                                </legend>
                                  <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label443" Text="(a) Soap Solution" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label444" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox334" MaxLength="150" placeholder="in Ltr" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label445" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox335" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label446" Text="(b) Caustic Soda" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label447" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox336" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label448" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox337" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label449" Text="(c) Washing Soda" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label450" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox338" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label451" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox339" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                      </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label452" Text="(v) Contract Labour" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label453" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox340" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label454" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox341" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label455" Text="(vi) Security" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label456" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox342" MaxLength="150" placeholder="in Nos" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label457" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox343" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label458" Text="(vii) Stationery" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label459" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox344" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label460" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox345" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <fieldset>
                                <legend>(viii) Others
                                </legend>
                                <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label464" Text="1." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label465" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox348" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label466" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox349" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label467" Text="2." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label468" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox350" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label469" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox351" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label470" Text="3." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label471" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox352" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label472" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox353" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label473" Text="4." Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label474" Text="Unit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox354" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label475" Text="Amount (₹)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox355" MaxLength="150" placeholder="0" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                    </div>
                            </fieldset>
                                </div>
                        </fieldset>
                            </div>
                    </fieldset>
                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="DCSMaster.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body" style="display: none">
                <div class="box-header">
                    <h3 class="box-title">DCS Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                    </div>
                </div>

            </div>
            </section>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 32 && charCode < 48) || charCode > 57) {

                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>


