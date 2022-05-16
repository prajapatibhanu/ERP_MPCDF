<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TDS_TCS_Master.aspx.cs" Inherits="mis_Finance_TDS_TCS_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">TCS & TDS Master</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>TCS</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Applicable on Date</label><span style="color: red">*</span>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox runat="server" Placeholder="Applicable on Date" ID="txtTCS_ApplicableOn" CssClass="form-control DateAdd" ClientIDMode="Static" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <fieldset>
                                                    <legend>If PAN No. Exist</legend>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    Applicable on
                                                                    <br />
                                                                    Turnover Amt.<span style="color: red">*</span></label>
                                                                <asp:TextBox runat="server" Placeholder="Enter Amt..." ID="txtTCS_Pan_Exist_TurnoverAmt" CssClass="form-control capitalize" ClientIDMode="Static"  onkeypress="return validateDec(this,event)" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    Before Turnover
                                                                    <br />
                                                                    Rate (%)<span style="color: red">*</span></label>
                                                                <asp:TextBox runat="server" Placeholder="Enter Rate (%)..." ID="txtTCS_Pan_Exist_TurnoverBeforeRate" CssClass="form-control" ClientIDMode="Static"  onkeypress="return validateDecUnit(this,event)" MaxLength="5"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    After Turnover
                                                                    <br />
                                                                    Rate (%)<span style="color: red">*</span></label>
                                                                <asp:TextBox runat="server" Placeholder="Enter Rate (%)..." ID="txtTCS_Pan_Exist_TurnoverAfterRate" CssClass="form-control" ClientIDMode="Static"  onkeypress="return validateDecUnit(this,event)" MaxLength="5"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>

                                            <div class="col-md-6">
                                                <fieldset>
                                                    <legend>If PAN No. Not Exist</legend>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    Applicable on
                                                                    <br />
                                                                    Turnover Amt.<span style="color: red">*</span></label>
                                                                <asp:TextBox runat="server" Placeholder="Enter Amt..." ID="txtTCS_Pan_NotExist_TurnoverAmt" CssClass="form-control" ClientIDMode="Static"  onkeypress="return validateDec(this,event)" MaxLength="10" ></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    Before Turnover
                                                                    <br />
                                                                    Rate (%)<span style="color: red">*</span></label>
                                                                <asp:TextBox runat="server" Placeholder="Enter Rate (%)... " ID="txtTCS_Pan_NotExist_TurnoverBeforeRate" CssClass="form-control" ClientIDMode="Static"  onkeypress="return validateDecUnit(this,event)" MaxLength="5"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    After Turnover
                                                                    <br />
                                                                    Rate (%)<span style="color: red">*</span></label>
                                                                <asp:TextBox runat="server" Placeholder="Enter Rate (%)..." ID="txtTCS_Pan_NotExist_TurnoverAfterRate" CssClass="form-control" ClientIDMode="Static"  onkeypress="return validateDecUnit(this,event)" MaxLength="5"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>TDS</legend>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label>Applicable on Date</label><span style="color: red">*</span>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>

                                                    <asp:TextBox runat="server" Placeholder="Applicable on Date" ID="txtTDS_ApplicableOn" CssClass="form-control DateAdd" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>If PAN No. Exist Rate (%)<span style="color: red">*</span></label>
                                                    <asp:TextBox runat="server" Placeholder="Enter Rate (%)..." ID="txtTDS_Pan_Exist_Rate" CssClass="form-control capitalize" ClientIDMode="Static" onkeypress="return validateDecUnit(this,event)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>If PAN No. Not Exist Rate (%)<span style="color: red">*</span></label>
                                                    <asp:TextBox runat="server" Placeholder="Enter Rate (%)..." ID="txtTDS_Pan_NotExist_Rate" CssClass="form-control capitalize" ClientIDMode="Static" onkeypress="return validateDecUnit(this,event)" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" runat="server" Text="Submit" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" href="TDS_TCS_Master.aspx">Clear</a>
                                    </div>
                                </div>

                            </div>
                            <div class="box-footer"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
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
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtTCS_ApplicableOn.ClientID%>').value.trim() == "") {
                msg = msg + "Select TCS Applicable On Date. \n";
            }
            if (document.getElementById('<%=txtTCS_Pan_Exist_TurnoverAmt.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TCS if Pan Exist Turnover Amt. \n";
            }
            if (document.getElementById('<%=txtTCS_Pan_Exist_TurnoverBeforeRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TCS if Pan Exist Turnover Before Rate. \n";
            }
            if (document.getElementById('<%=txtTCS_Pan_Exist_TurnoverAfterRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TCS if Pan Exist Turnover After Rate. \n";
            }
            if (document.getElementById('<%=txtTCS_Pan_NotExist_TurnoverAmt.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TCS Pan Not Exist Turnover Amt. \n";
            }
            if (document.getElementById('<%=txtTCS_Pan_NotExist_TurnoverBeforeRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TCS Pan Not Exist Turnover Before Rate. \n";
            }
            if (document.getElementById('<%=txtTCS_Pan_NotExist_TurnoverAfterRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TCS Pan Not Exist Turnover After Rate. \n";
            }
            if (document.getElementById('<%=txtTDS_ApplicableOn.ClientID%>').value.trim() == "") {
                msg = msg + "Select TDS Applicable On Date. \n";
            }
            if (document.getElementById('<%=txtTDS_Pan_Exist_Rate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TDS Pan Exist Rate. \n";
            }
            if (document.getElementById('<%=txtTDS_Pan_NotExist_Rate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TDS Pan Not Exist Rate. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Submit Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }


        function validateDecUnit(el, evt) {
            var digit = 3;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

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
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>


