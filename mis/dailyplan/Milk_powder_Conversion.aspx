<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mis/MainMaster.master" CodeFile="Milk_powder_Conversion.aspx.cs" Inherits="mis_dailyplan_Milk_powder_Conversion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Milk Powder Conversion </h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="dvdetails" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>In Flow</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <table id="tblInFlow" runat="server" class="table table-bordered">
                                                <tr>
                                                    <th colspan="6">Opening Balance</th>
                                                </tr>
                                                <tr>
                                                    <th>Particular</th>
                                                    <th>QTY</th>
                                                    <th>FAT %</th>
                                                    <th>FAT in Kg</th>
                                                    <th>SNF %</th>
                                                    <th>SNF in Kg</th>
                                                </tr>
                                                <tr>
                                                    <td><b>Milk Opening</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMOQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateMilkOpening();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMOFatPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateMilkOpening();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMOFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMOSNFPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateMilkOpening();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMOSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Condense Milk</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCMQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateCondenseMilk();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCMFatPer" CssClass="form-control" Text="0.2" onblur="CalculateCondenseMilk();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCMFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCMSNFPer" CssClass="form-control" Text="40"  onchange="return validateDec(this,event)" onblur="CalculateCondenseMilk();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCMSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Skimmed Milk recived from Processing</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSMQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)"  onblur="CalculateSkimMilkFat(),CalculateSkimMilkSnf();"  ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSMFatPer" CssClass="form-control"  onkeypress="return validateDec(this,event)"  ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSMFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" onblur="CalculateSkimMilkFat();" runat="server" ></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSMSNFPer" CssClass="form-control"   onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSMSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" onblur="CalculateSkimMilkSnf();" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Whole Milk recived from Processing</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtWMRQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateWholeMilk();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                       <%--<asp:TextBox ID="txtWMRFatPer" CssClass="form-control" Text="3.1" onblur="CalculateWholeMilk();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>--%>
                                                        <asp:TextBox ID="txtWMRFatPer" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtWMRFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <%--<asp:TextBox ID="txtWMRSNFPer" CssClass="form-control" Text="8.5" onblur="CalculateWholeMilk();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>--%>
                                                    <asp:TextBox ID="txtWMRSNFPer" CssClass="form-control"  onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtWMRSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><b>CSP</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCSPQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateCSP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCSPFatPer" CssClass="form-control" Text="1"  onkeypress="return validateDec(this,event)" onblur="CalculateCSP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCSPFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCSPSNFPer" CssClass="form-control" Text="95.5" onkeypress="return validateDec(this,event)" onblur="CalculateCSP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtCSPSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Others</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOtherQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateOther();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOtherFatPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateOther();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOtherFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOtherSNFPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateOther();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOtherSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Total</b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Out Flow</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <table id="tblOutFlow" runat="server" class="table table-bordered">
                                                    <tr>
                                                        <th colspan="6">Dispatch</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Particular</th>
                                                        <th>QTY</th>
                                                        <th>FAT %</th>
                                                        <th>FAT in Kg</th>
                                                        <th>SNF %</th>
                                                        <th>SNF in Kg</th>
                                                    </tr>
                                                    <tr>
                                                        <td><b>SMP Mfg</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSMPQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateSMP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSMPFatPer" CssClass="form-control" Text="1.1"  onblur="CalculateSMP();" onkeypress="return  validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSMPFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSMPSNFPer" CssClass="form-control" Text="95.5"  onblur="CalculateSMP();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSMPSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                     <tr>
                                                        <td><b>WMP Mfg</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWMPQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateWMP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWMPFatPer" CssClass="form-control"   onblur="CalculateWMP();" onkeypress="return  validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWMPFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWMPSNFPer" CssClass="form-control"   onblur="CalculateWMP();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWMPSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Condense Milk return to processing</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCMRQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateCondenseMilkReceive();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCMRFatPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateCondenseMilkReceive();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCMRFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCMRSNFPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateCondenseMilkReceive();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCMRSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Milk return to processing</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtMRQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateMilkReturnProcessing();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtMRFatPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateMilkReturnProcessing();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtMRFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtMRSNFPer" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateMilkReturnProcessing();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtMRSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Closing Balance ( Only Condense Milk)</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCBRQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateCB();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCBRFatPer" CssClass="form-control" Text="0.02" onblur="CalculateCB();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCBRFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCBRSNFPer" CssClass="form-control" Text="40" onblur="CalculateCB();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCBRSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>CSP</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutCSPQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" onblur="CalculateOCSP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutCSPFatPer" CssClass="form-control" Text="1"  onkeypress="return validateDec(this,event)" onblur="CalculateOCSP();" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutCSPFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutCSPSNFPer" CssClass="form-control" Text="95.5" onblur="CalculateOCSP();" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutCSPSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>

                                                    <tr>
                                                        <td><b>Total</b></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutTotalQtyInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutTotalFatInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtOutTotalSNFInKg" CssClass="form-control" onkeypress="return validateDec(this,event)" ClientIDMode="Static" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table id="tblVariation" runat="server" class="table table-bordered">
                                        <tr>
                                            <th>Variation</th>
                                            <th></th>
                                            <th></th>
                                            <th>FAT in Kg</th>
                                            <th></th>
                                            <th>SNF in Kg</th>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtTFatInKg" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtTSnfInKg" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Recovery %</td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtReFatInKg" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtReSNFInKg" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Norms (Fix content to show)</td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtNormFatInKg" runat="server" Text="99" CssClass="form-control"></asp:TextBox></td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtNormSNFInKg" runat="server" Text="97.5" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="Submit" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                    <a id="btnClear" runat="server" href="Milk_powder_Conversion.aspx" class="btn btn-default">clear</a>
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
    <script type="text/javascript">
        function CalculateMilkOpening() {
            debugger;
            document.getElementById('<%=lblMsg.ClientID%>').value = "";
            var MOQty = document.getElementById('<%=txtMOQtyInKg.ClientID%>').value;
            var MOFat = document.getElementById('<%=txtMOFatPer.ClientID%>').value;
            var MOSNF = document.getElementById('<%=txtMOSNFPer.ClientID%>').value;
            if (MOQty == "") {
                MOQty = "0";
            }
            if (MOFat == "") {
                MOFat = "0";
            }
            if (MOSNF == "") {
                MOSNF = "0";
            }
            var TotalMOFatInKg = parseFloat((parseFloat(MOQty) * parseFloat(MOFat)) / parseFloat(100)).toFixed(1);
            var TotalMOSNFInKg = parseFloat((parseFloat(MOQty) * parseFloat(MOSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtMOFatInKg.ClientID%>').value = TotalMOFatInKg;
            document.getElementById('<%= txtMOSNFInKg.ClientID%>').value = TotalMOSNFInKg;
            CalculateTotalInQty();
        }
        function CalculateCondenseMilk() {
            debugger;
            var CQty = document.getElementById('<%=txtCMQtyInKg.ClientID%>').value;
            var CFat = document.getElementById('<%=txtCMFatPer.ClientID%>').value;
            var CSNF = document.getElementById('<%=txtCMSNFPer.ClientID%>').value;
            if (CQty == "") {
                CQty = "0";
            }
            if (CFat == "") {
                CFat = "0";
            }
            if (CSNF == "") {
                CSNF = "0";
            }
            var TotalFatInKg = parseFloat((parseFloat(CQty) * parseFloat(CFat)) / parseFloat(100)).toFixed(1);
            var TotalSNFInKg = parseFloat((parseFloat(CQty) * parseFloat(CSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtCMFatInKg.ClientID%>').value = TotalFatInKg;
            document.getElementById('<%= txtCMSNFInKg.ClientID%>').value = TotalSNFInKg;
            CalculateTotalInQty();
        }

        function CalculateSkimMilkFatKg() {
            debugger;
            var SMQty = document.getElementById('<%=txtSMQtyInKg.ClientID%>').value;
            var SMFat = document.getElementById('<%=txtSMFatPer.ClientID%>').value;
            var SMSNF = document.getElementById('<%=txtSMSNFPer.ClientID%>').value;
            if (SMQty == "") {
                SMQty = "0";
            }
            if (SMFat == "") {
                SMFat = "0";
            }
            if (SMSNF == "") {
                SMSNF = "0";
            }
            var TSMFatInKg = parseFloat((parseFloat(SMQty) * parseFloat(SMFat)) / parseFloat(100)).toFixed(1);
            var TSMSNFInKg = parseFloat((parseFloat(SMQty) * parseFloat(SMSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtSMFatInKg.ClientID%>').value = TSMFatInKg;
           // document.getElementById('<%= txtSMSNFInKg.ClientID%>').value = TSMSNFInKg;
            CalculateTotalInQty();
        }
        function CalculateSkimMilkSnfKg() {
            debugger;
            var SMQty = document.getElementById('<%=txtSMQtyInKg.ClientID%>').value;
            var SMFat = document.getElementById('<%=txtSMFatPer.ClientID%>').value;
            var SMSNF = document.getElementById('<%=txtSMSNFPer.ClientID%>').value;
            if (SMQty == "") {
                SMQty = "0";
            }
            if (SMFat == "") {
                SMFat = "0";
            }
            if (SMSNF == "") {
                SMSNF = "0";
            }
            var TSMFatInKg = parseFloat((parseFloat(SMQty) * parseFloat(SMFat)) / parseFloat(100)).toFixed(1);
            var TSMSNFInKg = parseFloat((parseFloat(SMQty) * parseFloat(SMSNF)) / parseFloat(100)).toFixed(1);
            //document.getElementById('<%= txtSMFatInKg.ClientID%>').value = TSMFatInKg;
            document.getElementById('<%= txtSMSNFInKg.ClientID%>').value = TSMSNFInKg;
            CalculateTotalInQty();
        }
		function CalculateSkimMilkFat() {
            debugger;
            var SMQty = document.getElementById('<%=txtSMQtyInKg.ClientID%>').value;
            var SMFatKg = document.getElementById('<%=txtSMFatInKg.ClientID%>').value;
           
            if (SMQty == "") {
                SMQty = "0";
            }
            if (SMFatKg == "") {
                SMFatKg = "0";
            }
            
            var TSMFatPer = parseFloat((parseFloat(SMFatKg) / parseFloat(SMQty)) * parseFloat(100)).toFixed(1);
            
            document.getElementById('<%= txtSMFatPer.ClientID%>').value = TSMFatPer;
            
            CalculateTotalInQty();
        }
        function CalculateSkimMilkSnf() {
            debugger;
            var SMQty = document.getElementById('<%=txtSMQtyInKg.ClientID%>').value;
            var SMSnfKg = document.getElementById('<%=txtSMSNFInKg.ClientID%>').value;

            if (SMQty == "") {
                SMQty = "0";
            }
            if (SMSnfKg == "") {
                SMSnfKg = "0";
            }

            var TSMSnfPer = parseFloat((parseFloat(SMSnfKg) / parseFloat(SMQty)) * parseFloat(100)).toFixed(1);

            document.getElementById('<%= txtSMSNFPer.ClientID%>').value = TSMSnfPer;

            CalculateTotalInQty();
        }
        function CalculateWholeMilk() {
            debugger;
            var WMQty = document.getElementById('<%=txtWMRQtyInKg.ClientID%>').value;
            var WMFat = document.getElementById('<%=txtWMRFatPer.ClientID%>').value;
            var WMSNF = document.getElementById('<%=txtWMRSNFPer.ClientID%>').value;
            if (WMQty == "") {
                WMQty = "0";
            }
            if (WMFat == "") {
                WMFat = "0";
            }
            if (WMSNF == "") {
                WMSNF = "0";
            }
            var TWMFatInKg = parseFloat((parseFloat(WMQty) * parseFloat(WMFat)) / parseFloat(100)).toFixed(1);
            var TWMSNFInKg = parseFloat((parseFloat(WMQty) * parseFloat(WMSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtWMRFatInKg.ClientID%>').value = TWMFatInKg;
            document.getElementById('<%= txtWMRSNFInKg.ClientID%>').value = TWMSNFInKg;
            CalculateTotalInQty();
        }

        function CalculateCSP() {
            debugger;
            var CSPQty = document.getElementById('<%=txtCSPQtyInKg.ClientID%>').value;
            var CSPFat = document.getElementById('<%=txtCSPFatPer.ClientID%>').value;
            var CSPSNF = document.getElementById('<%=txtCSPSNFPer.ClientID%>').value;
            if (CSPQty == "") {
                CSPQty = "0";
            }
            if (CSPFat == "") {
                CSPFat = "0";
            }
            if (CSPSNF == "") {
                CSPSNF = "0";
            }
            var TCSFatInKg = parseFloat((parseFloat(CSPQty) * parseFloat(CSPFat)) / parseFloat(100)).toFixed(1);
            var TCSSNFInKg = parseFloat((parseFloat(CSPQty) * parseFloat(CSPSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtCSPFatInKg.ClientID%>').value = TCSFatInKg;
            document.getElementById('<%= txtCSPSNFInKg.ClientID%>').value = TCSSNFInKg;
            CalculateTotalInQty();
        }
        function CalculateOther() {
            debugger;
            var OtherQty = document.getElementById('<%=txtOtherQtyInKg.ClientID%>').value;
            var OtherFat = document.getElementById('<%=txtOtherFatPer.ClientID%>').value;
            var OtherSNF = document.getElementById('<%=txtOtherSNFPer.ClientID%>').value;
            if (OtherQty == "") {
                OtherQty = "0";
            }
            if (OtherFat == "") {
                OtherFat = "0";
            }
            if (OtherSNF == "") {
                OtherSNF = "0";
            }
            var TOtherFatInKg = parseFloat((parseFloat(OtherQty) * parseFloat(OtherFat)) / parseFloat(100)).toFixed(1);
            var TOtherSNFInKg = parseFloat((parseFloat(OtherQty) * parseFloat(OtherSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtOtherFatInKg.ClientID%>').value = TOtherFatInKg;
            document.getElementById('<%= txtOtherSNFInKg.ClientID%>').value = TOtherSNFInKg;
            CalculateTotalInQty();
        }
        function CalculateSMP() {
            debugger;
            var SMPPQty = document.getElementById('<%=txtSMPQtyInKg.ClientID%>').value;
            var SMPPFat = document.getElementById('<%=txtSMPFatPer.ClientID%>').value;
            var SMPPSNF = document.getElementById('<%=txtSMPSNFPer.ClientID%>').value;
            if (SMPPQty == "") {
                SMPPQty = "0";
            }
            if (SMPPFat == "") {
                SMPPFat = "0";
            }
            if (SMPPSNF == "") {
                SMPPSNF = "0";
            }
            var TSMPFatInKg = parseFloat((parseFloat(SMPPQty) * parseFloat(SMPPFat)) / parseFloat(100)).toFixed(1);
            var TSMPSNFInKg = parseFloat((parseFloat(SMPPQty) * parseFloat(SMPPSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtSMPFatInKg.ClientID%>').value = TSMPFatInKg;
            document.getElementById('<%= txtSMPSNFInKg.ClientID%>').value = TSMPSNFInKg;
            CalculateTotalOutQty();
        }
        function CalculateWMP() {
            debugger;
            var WMPPQty = document.getElementById('<%=txtWMPQtyInKg.ClientID%>').value;
            var WMPPFat = document.getElementById('<%=txtWMPFatPer.ClientID%>').value;
            var WMPPSNF = document.getElementById('<%=txtWMPSNFPer.ClientID%>').value;
            if (WMPPQty == "") {
                WMPPQty = "0";
            }
            if (WMPPFat == "") {
                WMPPFat = "0";
            }
            if (WMPPSNF == "") {
                WMPPSNF = "0";
            }
            var TWMPFatInKg = parseFloat((parseFloat(WMPPQty) * parseFloat(WMPPFat)) / parseFloat(100)).toFixed(1);
            var TWMPSNFInKg = parseFloat((parseFloat(WMPPQty) * parseFloat(WMPPSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtWMPFatInKg.ClientID%>').value = TWMPFatInKg;
            document.getElementById('<%= txtWMPSNFInKg.ClientID%>').value = TWMPSNFInKg;
            CalculateTotalOutQty();
        }
        function CalculateCondenseMilkReceive() {
            debugger;
            var CMRQty = document.getElementById('<%=txtCMRQtyInKg.ClientID%>').value;
            var CMRFat = document.getElementById('<%=txtCMRFatPer.ClientID%>').value;
            var CMRSNF = document.getElementById('<%=txtCMRSNFPer.ClientID%>').value;
            if (CMRQty == "") {
                CMRQty = "0";
            }
            if (CMRFat == "") {
                CMRFat = "0";
            }
            if (CMRSNF == "") {
                CMRSNF = "0";
            }
            var TCMRFatInKg = parseFloat((parseFloat(CMRQty) * parseFloat(CMRFat)) / parseFloat(100)).toFixed(1);
            var TCMRSNFInKg = parseFloat((parseFloat(CMRQty) * parseFloat(CMRSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtCMRFatInKg.ClientID%>').value = TCMRFatInKg;
            document.getElementById('<%= txtCMRSNFInKg.ClientID%>').value = TCMRSNFInKg;
            CalculateTotalOutQty();
        }
        function CalculateMilkReturnProcessing() {
            debugger;
            var MRPQty = document.getElementById('<%=txtMRQtyInKg.ClientID%>').value;
            var MRPFat = document.getElementById('<%=txtMRFatPer.ClientID%>').value;
            var MRPSNF = document.getElementById('<%=txtMRSNFPer.ClientID%>').value;
            if (MRPQty == "") {
                MRPQty = "0";
            }
            if (MRPFat == "") {
                MRPFat = "0";
            }
            if (MRPSNF == "") {
                MRPSNF = "0";
            }
            var TMPRFatInKg = parseFloat((parseFloat(MRPQty) * parseFloat(MRPFat)) / parseFloat(100)).toFixed(1);
            var TMPRSNFInKg = parseFloat((parseFloat(MRPQty) * parseFloat(MRPSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtMRFatInKg.ClientID%>').value = TMPRFatInKg;
            document.getElementById('<%= txtMRSNFInKg.ClientID%>').value = TMPRSNFInKg;
            CalculateTotalOutQty();
        }
        function CalculateCB() {
            debugger;
            var CBQty = document.getElementById('<%=txtCBRQtyInKg.ClientID%>').value;
            var CBFat = document.getElementById('<%=txtCBRFatPer.ClientID%>').value;
            var CBSNF = document.getElementById('<%=txtCBRSNFPer.ClientID%>').value;
            if (CBQty == "") {
                CBQty = "0";
            }
            if (CBFat == "") {
                CBFat = "0";
            }
            if (CBSNF == "") {
                CBSNF = "0";
            }
            var TCBFatInKg = parseFloat((parseFloat(CBQty) * parseFloat(CBFat)) / parseFloat(100)).toFixed(1);
            var TCBSNFInKg = parseFloat((parseFloat(CBQty) * parseFloat(CBSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtCBRFatInKg.ClientID%>').value = TCBFatInKg;
            document.getElementById('<%= txtCBRSNFInKg.ClientID%>').value = TCBSNFInKg;
            CalculateTotalOutQty();
        }
        function CalculateOCSP() {
            debugger;
            var OCSPQty = document.getElementById('<%=txtOutCSPQtyInKg.ClientID%>').value;
            var OCSPFat = document.getElementById('<%=txtOutCSPFatPer.ClientID%>').value;
            var OCSPSNF = document.getElementById('<%=txtOutCSPSNFPer.ClientID%>').value;
            if (OCSPQty == "") {
                OCSPQty = "0";
            }
            if (OCSPFat == "") {
                OCSPFat = "0";
            }
            if (OCSPSNF == "") {
                OCSPSNF = "0";
            }
            var TOCSPFatInKg = parseFloat((parseFloat(OCSPQty) * parseFloat(OCSPFat)) / parseFloat(100)).toFixed(1);
            var TOCSPSNFInKg = parseFloat((parseFloat(OCSPQty) * parseFloat(OCSPSNF)) / parseFloat(100)).toFixed(1);
            document.getElementById('<%= txtOutCSPFatInKg.ClientID%>').value = TOCSPFatInKg;
            document.getElementById('<%= txtOutCSPSNFInKg.ClientID%>').value = TOCSPSNFInKg;
            CalculateTotalOutQty();
        }

        function CalculateTotalInQty() {
            debugger;
            var MOQtyInKg = document.getElementById('<%=txtMOQtyInKg.ClientID%>').value;
            var CMQtyInKg = document.getElementById('<%=txtCMQtyInKg.ClientID%>').value;
            var SMQtyInKg = document.getElementById('<%=txtSMQtyInKg.ClientID%>').value;
            var WMRQtyInKg = document.getElementById('<%=txtWMRQtyInKg.ClientID%>').value;
            var CSPQtyInKg = document.getElementById('<%=txtCSPQtyInKg.ClientID%>').value;
            var OtherQtyInKg = document.getElementById('<%=txtOtherQtyInKg.ClientID%>').value;

            var MOFatInKg = document.getElementById('<%=txtMOFatInKg.ClientID%>').value;
            var CMFatInKg = document.getElementById('<%=txtCMFatInKg.ClientID%>').value;
            var SMFatInKg = document.getElementById('<%=txtSMFatInKg.ClientID%>').value;
            var WMRFatInKg = document.getElementById('<%=txtWMRFatInKg.ClientID%>').value;
            var CSPFatInKg = document.getElementById('<%=txtCSPFatInKg.ClientID%>').value;
            var OtherFatInKg = document.getElementById('<%=txtOtherFatInKg.ClientID%>').value;

            var MOSNFInKg = document.getElementById('<%=txtMOSNFInKg.ClientID%>').value;
            var CMSNFInKg = document.getElementById('<%=txtCMSNFInKg.ClientID%>').value;
            var SMSNFInKg = document.getElementById('<%=txtSMSNFInKg.ClientID%>').value;
            var WMRSNFInKg = document.getElementById('<%=txtWMRSNFInKg.ClientID%>').value;
            var CSPSNFInKg = document.getElementById('<%=txtCSPSNFInKg.ClientID%>').value;
            var OtherSNFInKg = document.getElementById('<%=txtOtherSNFInKg.ClientID%>').value;

            if (MOQtyInKg == "") {
                MOQtyInKg = "0";
            }
            if (CMQtyInKg == "") {
                CMQtyInKg = "0";
            }
            if (SMQtyInKg == "") {
                SMQtyInKg = "0";
            }
            if (WMRQtyInKg == "") {
                WMRQtyInKg = "0";
            }
            if (CSPQtyInKg == "") {
                CSPQtyInKg = "0";
            }
            if (OtherQtyInKg == "") {
                OtherQtyInKg = "0";
            }

            if (MOFatInKg == "") {
                MOFatInKg = "0";
            }
            if (CMFatInKg == "") {
                CMFatInKg = "0";
            }
            if (SMFatInKg == "") {
                SMFatInKg = "0";
            }
            if (WMRFatInKg == "") {
                WMRFatInKg = "0";
            }
            if (CSPFatInKg == "") {
                CSPFatInKg = "0";
            }
            if (OtherFatInKg == "") {
                OtherFatInKg = "0";
            }


            if (MOSNFInKg == "") {
                MOSNFInKg = "0";
            }
            if (CMSNFInKg == "") {
                CMSNFInKg = "0";
            }
            if (SMSNFInKg == "") {
                SMSNFInKg = "0";
            }
            if (WMRSNFInKg == "") {
                WMRSNFInKg = "0";
            }
            if (CSPSNFInKg == "") {
                CSPSNFInKg = "0";
            }
            if (OtherSNFInKg == "") {
                OtherSNFInKg = "0";
            }


            var TInQyt = parseFloat((parseFloat(MOQtyInKg) + parseFloat(CMQtyInKg) + parseFloat(SMQtyInKg) + parseFloat(WMRQtyInKg) + parseFloat(CSPQtyInKg) + parseFloat(OtherQtyInKg))).toFixed(3);
            var TInFat = parseFloat((parseFloat(MOFatInKg) + parseFloat(CMFatInKg) + parseFloat(SMFatInKg) + parseFloat(WMRFatInKg) + parseFloat(CSPFatInKg) + parseFloat(OtherFatInKg))).toFixed(3);
            var TInSNF = parseFloat((parseFloat(MOSNFInKg) + parseFloat(CMSNFInKg) + parseFloat(SMSNFInKg) + parseFloat(WMRSNFInKg) + parseFloat(CSPSNFInKg) + parseFloat(OtherSNFInKg))).toFixed(3);
            document.getElementById('<%= txtTotalQtyInKg.ClientID%>').value = TInQyt;
            document.getElementById('<%= txtTotalFatInKg.ClientID%>').value = TInFat;
            document.getElementById('<%= txtTotalSNFInKg.ClientID%>').value = TInSNF;


            var TotalInFat = document.getElementById('<%= txtTotalFatInKg.ClientID%>').value;
            var TotalInSNF = document.getElementById('<%= txtTotalSNFInKg.ClientID%>').value;
            var TotalOutFat = document.getElementById('<%= txtOutTotalFatInKg.ClientID%>').value;
            var TotalOutSNF = document.getElementById('<%= txtOutTotalSNFInKg.ClientID%>').value;

            if (TotalInFat == "") {
                TotalInFat = "0";
            }
            if (TotalInSNF == "") {
                TotalInSNF = "0";
            }
            if (TotalOutFat == "") {
                TotalOutFat = "0";
            }
            if (TotalOutSNF == "") {
                TotalOutSNF = "0";
            }

            var TotalFat = parseFloat((parseFloat(TotalOutFat) - parseFloat(TotalInFat))).toFixed(3);
            var TotalSNF = parseFloat((parseFloat(TotalOutSNF) - parseFloat(TotalInSNF))).toFixed(3);
            var TotalRecoveryFat = parseFloat((parseFloat(TotalOutFat) / parseFloat(TotalInFat)) * parseFloat(100)).toFixed(3);
            var TotalRecoverySNF = parseFloat((parseFloat(TotalOutSNF) / parseFloat(TotalInSNF)) * parseFloat(100)).toFixed(3);
            if (TotalRecoveryFat == "") {
                TotalRecoveryFat = "0"
            }
            else {
                TotalRecoveryFat = TotalRecoveryFat;
            }

            if (TotalRecoverySNF == "") {
                TotalRecoverySNF = "0"
            }
            else {
                TotalRecoverySNF = TotalRecoverySNF;
            }
            document.getElementById('<%= txtTFatInKg.ClientID%>').value = TotalFat;
            document.getElementById('<%= txtTSnfInKg.ClientID%>').value = TotalSNF;
            document.getElementById('<%= txtReFatInKg.ClientID%>').value = TotalRecoveryFat;
            document.getElementById('<%= txtReSNFInKg.ClientID%>').value = TotalRecoverySNF;

            var RecoveryFat = document.getElementById('<%= txtReFatInKg.ClientID%>').value;
            var NormRecoveryFat = document.getElementById('<%= txtNormFatInKg.ClientID%>').value;

            if ((parseFloat(RecoveryFat) - parseFloat(NormRecoveryFat)) > parseFloat("2.0")) {
                $("#btnSave").hide();
            }
            else if ((parseFloat(NormRecoveryFat) - parseFloat(RecoveryFat)) > parseFloat("2.0")) {
                $("#btnSave").hide();
            }
            else {
                $("#btnSave").show();
            }

        }
        function CalculateTotalOutQty() {
            debugger;
            var SMPQtyInKg = document.getElementById('<%=txtSMPQtyInKg.ClientID%>').value;
            var WMPQtyInKg = document.getElementById('<%=txtWMPQtyInKg.ClientID%>').value;
        var CMRQtyInKg = document.getElementById('<%=txtCMRQtyInKg.ClientID%>').value;
        var MRQtyInKg = document.getElementById('<%=txtMRQtyInKg.ClientID%>').value;
        var CBRQtyInKg = document.getElementById('<%=txtCBRQtyInKg.ClientID%>').value;
        var OutCSPQtyInKg = document.getElementById('<%=txtOutCSPQtyInKg.ClientID%>').value;

        var SMPFatInKg = document.getElementById('<%=txtSMPFatInKg.ClientID%>').value;
        var WMPFatInKg = document.getElementById('<%=txtWMPFatInKg.ClientID%>').value;
        var CMRFatInKg = document.getElementById('<%=txtCMRFatInKg.ClientID%>').value;
        var MRFatInKg = document.getElementById('<%=txtMRFatInKg.ClientID%>').value;
        var CBRFatInKg = document.getElementById('<%=txtCBRFatInKg.ClientID%>').value;
        var OutCSPFatInKg = document.getElementById('<%=txtOutCSPFatInKg.ClientID%>').value;

        var SMPSNFInKg = document.getElementById('<%=txtSMPSNFInKg.ClientID%>').value;
        var WMPSNFInKg = document.getElementById('<%=txtWMPSNFInKg.ClientID%>').value;
        var CMRSNFInKg = document.getElementById('<%=txtCMRSNFInKg.ClientID%>').value;
        var MRSNFInKg = document.getElementById('<%=txtMRSNFInKg.ClientID%>').value;
        var CBRSNFInKg = document.getElementById('<%=txtCBRSNFInKg.ClientID%>').value;
        var OutCSPSNFInKg = document.getElementById('<%=txtOutCSPSNFInKg.ClientID%>').value;

        if (SMPQtyInKg == "") {
            SMPQtyInKg = "0";
        }
        if (WMPQtyInKg == "") {
            WMPQtyInKg = "0";
        }
        if (CMRQtyInKg == "") {
            CMRQtyInKg = "0";
        }
        if (MRQtyInKg == "") {
            MRQtyInKg = "0";
        }
        if (CBRQtyInKg == "") {
            CBRQtyInKg = "0";
        }
        if (OutCSPQtyInKg == "") {
            OutCSPQtyInKg = "0";
        }

        if (SMPFatInKg == "") {
            SMPFatInKg = "0";
        }
        if (WMPFatInKg == "") {
            WMPFatInKg = "0";
        }
        if (CMRFatInKg == "") {
            CMRFatInKg = "0";
        }
        if (MRFatInKg == "") {
            MRFatInKg = "0";
        }
        if (CBRFatInKg == "") {
            CBRFatInKg = "0";
        }
        if (OutCSPFatInKg == "") {
            OutCSPFatInKg = "0";
        }


        if (SMPSNFInKg == "") {
            SMPSNFInKg = "0";
        }
        if (WMPSNFInKg == "") {
            WMPSNFInKg = "0";
        }
        if (CMRSNFInKg == "") {
            CMRSNFInKg = "0";
        }
        if (MRSNFInKg == "") {
            MRSNFInKg = "0";
        }
        if (CBRSNFInKg == "") {
            CBRSNFInKg = "0";
        }
        if (OutCSPSNFInKg == "") {
            OutCSPSNFInKg = "0";
        }


        var TQyt = parseFloat((parseFloat(SMPQtyInKg) + parseFloat(WMPQtyInKg) + parseFloat(CMRQtyInKg) + parseFloat(MRQtyInKg) + parseFloat(CBRQtyInKg) + parseFloat(OutCSPQtyInKg))).toFixed(3);
        var TFat = parseFloat((parseFloat(SMPFatInKg) + parseFloat(WMPFatInKg) + parseFloat(CMRFatInKg) + parseFloat(MRFatInKg) + parseFloat(CBRFatInKg) + parseFloat(OutCSPFatInKg))).toFixed(3);
        var TSNF = parseFloat((parseFloat(SMPSNFInKg) + parseFloat(WMPSNFInKg) +  parseFloat(CMRSNFInKg) + parseFloat(MRSNFInKg) + parseFloat(CBRSNFInKg) + parseFloat(OutCSPSNFInKg))).toFixed(3);
        document.getElementById('<%= txtOutTotalQtyInKg.ClientID%>').value = TQyt;
        document.getElementById('<%= txtOutTotalFatInKg.ClientID%>').value = TFat;
        document.getElementById('<%= txtOutTotalSNFInKg.ClientID%>').value = TSNF;


        var TotalInFat = document.getElementById('<%= txtTotalFatInKg.ClientID%>').value;
        var TotalInSNF = document.getElementById('<%= txtTotalSNFInKg.ClientID%>').value;
        var TotalOutFat = document.getElementById('<%= txtOutTotalFatInKg.ClientID%>').value;
        var TotalOutSNF = document.getElementById('<%= txtOutTotalSNFInKg.ClientID%>').value;

        if (TotalInFat == "") {
            TotalInFat = "0";
        }
        if (TotalInSNF == "") {
            TotalInSNF = "0";
        }
        if (TotalOutFat == "") {
            TotalOutFat = "0";
        }
        if (TotalOutSNF == "") {
            TotalOutSNF = "0";
        }

        var TotalFat = parseFloat((parseFloat(TotalOutFat) - parseFloat(TotalInFat))).toFixed(3);
        var TotalSNF = parseFloat((parseFloat(TotalOutSNF) - parseFloat(TotalInSNF))).toFixed(3);
        var TotalRecoveryFat = parseFloat((parseFloat(TotalOutFat) / parseFloat(TotalInFat)) * parseFloat(100)).toFixed(3);
        var TotalRecoverySNF = parseFloat((parseFloat(TotalOutSNF) / parseFloat(TotalInSNF)) * parseFloat(100)).toFixed(3);
        document.getElementById('<%= txtTFatInKg.ClientID%>').value = TotalFat;
        document.getElementById('<%= txtTSnfInKg.ClientID%>').value = TotalSNF;
        document.getElementById('<%= txtReFatInKg.ClientID%>').value = TotalRecoveryFat;
        document.getElementById('<%= txtReSNFInKg.ClientID%>').value = TotalRecoverySNF;

        var RecoveryFat = document.getElementById('<%= txtReFatInKg.ClientID%>').value;
        var NormRecoveryFat = document.getElementById('<%= txtNormFatInKg.ClientID%>').value;

        if ((parseFloat(RecoveryFat) - parseFloat(NormRecoveryFat)) > parseFloat("2.0")) {
            $("#btnSave").hide();
        }
        else if ((parseFloat(NormRecoveryFat) - parseFloat(RecoveryFat)) > parseFloat("2.0")) {
            $("#btnSave").hide();
        }
        else {
            $("#btnSave").show();
        }
    }
    </script>
</asp:Content>

