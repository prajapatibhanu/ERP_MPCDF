<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Monthly_Information_tobe_Feed_by_DCS_Secretary_Through_Mobile_App.aspx.cs" Inherits="mis_DCSInformationSystem_Monthly_Information_tobe_Feed_by_DCS_Secretary_Through_Mobile_App" %>

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
                    <h3 class="box-title">Monthly Information to be Feed by DCS Secretary Through Mobile App</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Monthly Information by DCS Secretary 
                        </legend>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblDCScode" Text="(1) DCS Code" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="Enter DCS Code" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label41" Text="(2) Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                       <asp:DropDownList ID="DDlMonth" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlMonth_SelectedIndexChanged">
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
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label46" Text="(3) Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label47" Text="(4) Functional Day" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox39" MaxLength="150" placeholder="Functional Day" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>




                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label61" Text="(5) Milk Collection" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label62" Text="Cow" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox43" MaxLength="150" placeholder="in Ltrs." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label63" Text="Buffalo" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox52" MaxLength="150" placeholder="in Ltrs." ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label65" Text="(6) Amount Paid to Production" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label66" Text="Cow" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox54" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label67" Text="Buffalo" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox55" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <%--</div>

                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" Text="(7) Milk Pourers" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" Text="Members" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox7" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" Text="Non-Members" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox8" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--</div>

                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label33" Text="(8) Milk Supply quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label34" Text="Members" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox25" MaxLength="150" placeholder="in Ltrs" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label35" Text="Non-Members" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox26" MaxLength="150" placeholder="in Ltrs" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%-- </div>

                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label60" Text="(9) Local Milk Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label64" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox41" MaxLength="150" placeholder="in Ltrs" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label68" Text="Amount" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox42" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%-- </div>


                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label69" Text="(10) Sample Milk Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label70" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox44" MaxLength="150" placeholder="in Ltrs" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label71" Text="Amount" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox45" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--    </div>


                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label164" Text="(11) Milk Supply to the milk union quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label165" Text="Cow" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox124" MaxLength="150" placeholder="in Ltrs" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label166" Text="Buffalo" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox125" MaxLength="150" placeholder="in Ltrs" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <%--</div>

                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label72" Text="(12) Milk Quantity Recived by the Milk Union" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label73" Text="Cow" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox46" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label74" Text="Buffalo" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox47" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <%-- </div>

                        <div class="row">--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label85" Text="(13) Amount Paid by the Milk Union" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label86" Text="Cow" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox59" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label87" Text="Buffalo" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox60" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>



                            <fieldset>
                                <legend>(14) Ghee
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label176" Text="(i) Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label177" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox133" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label178" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox134" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label180" Text="(ii) Purchase" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label181" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox136" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label182" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox137" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label184" Text="(iii) Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label185" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox139" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label186" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox140" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label188" Text="(iv) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label189" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox142" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label190" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox143" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>(15) Cattle Feed
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" Text="(i) Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox1" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox2" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label4" Text="(ii) Purchase" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label5" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox3" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label6" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox4" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" Text="(iii) Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label11" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox5" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label12" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox6" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" Text="(iv) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label14" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox9" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label15" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox10" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>(16) Mineral Mixture
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label16" Text="(i) Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label17" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox11" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label18" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox12" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label19" Text="(ii) Purchase" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label20" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox13" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label21" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox14" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label22" Text="(iii) Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label23" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox15" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label24" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox16" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label25" Text="(iv) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label26" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox17" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label27" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox18" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend>(17) Fodder Seeds
                                </legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label28" Text="(i) Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label29" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox19" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label30" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox20" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label31" Text="(ii) Purchase" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label32" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox21" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label36" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox22" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label37" Text="(iii) Sold" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label38" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox23" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label39" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox24" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label40" Text="(iv) Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label42" Text="Quantity" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox27" MaxLength="150" placeholder="in KG" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="Label43" Text="Amount(Rs)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox28" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label44" Text="(18) Artificial Insemination Performed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label45" Text="Cow" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox29" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label48" Text="Buffalo" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox30" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label49" Text="(19) Calves Born" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label50" Text="Cow(Male/Female)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox31" MaxLength="150" placeholder="Male/Female" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label51" Text="Buffalo(Male/Female)" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox32" MaxLength="150" placeholder="Male/Female" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label52" Text="(20) First Aided Cases Trated" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                         <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox33" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                               <%-- <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label53" Text="" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox33" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>--%>
                            </div>

                            <%--<div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label54" Text="(21) LN2" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
            <fieldset>
                                <legend>(21) LN2
                                </legend>
                                 <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label55" Text="Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox34" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label56" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox35" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label75" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox48" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label76" Text="Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox49" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                                     </div>
                </fieldset>
                            <%--<div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label77" Text="(22) Seman" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                             <fieldset>
                                <legend>(22) Seman
                                </legend>
                                 <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label78" Text="Opening Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox50" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label79" Text="Received" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox51" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label80" Text="Consumed" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox53" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label81" Text="Closing Balance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox56" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                                     </div>
                                 </fieldset>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label82" Text="(23) New Cattle Induction" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label83" Text="Under Scheme" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox57" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label84" Text="Self Finance" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox58" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label57" Text="(24) Monthly Profitability" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label58" Text="Gross Profit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox36" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label59" Text="Net Profit" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>

                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="TextBox40" MaxLength="150" placeholder="in Rupees" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>


                            </div>

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

