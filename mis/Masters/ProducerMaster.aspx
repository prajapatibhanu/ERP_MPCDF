<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProducerMaster.aspx.cs" Inherits="mis_Masters_ProducerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">
        function printDiv() {
            var divToPrint = document.getElementById('DivIdToPrint');
            var htmlToPrint = '';
            htmlToPrint += divToPrint.outerHTML;
            var newWin = window.open('', 'Print-Window', 'letf=0,top=0,width=800%,height=600,toolbar=0,scrollbars=0,status=0');
            // alert('1');
            newWin.document.open();
            newWin.document.write('<html><head><link rel=\"stylesheet\" href=\"print.css\" type=\"text/css\" media=\"print\"/></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            // mywindow.document.write("");
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 1000);
        }
    </script>
    <link href="print.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card-header">
                    <h3 class="card-title">दुग्ध उत्पादक रजिस्ट्रेशन</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label>दुग्ध संघ </label>
                            <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>दुग्ध सोसायटी का नाम</label>
                            <asp:DropDownList ID="DdlDCS" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnljila" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>जिला</label>
                                    <asp:TextBox ID="txtdivision" MaxLength="60" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>तहसील</label>
                                    <asp:TextBox ID="txtdistrict" MaxLength="60" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>विधानसभा</label>
                                    <asp:TextBox ID="txtAssembly" MaxLength="60" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>ग्राम पंचायत/क़स्बा </label>
                                    <asp:TextBox ID="txtGram" MaxLength="60" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>पता</label>
                                    <span class="pull-right">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAddress"
                                            ValidationExpression="^[^'@%#$&=^!~?]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet and Numbers Allow !'></i>" ErrorMessage="Only Alphabet and Numbers Allow" />
                                    </span>
                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <div class="row">
                        <fieldset>
                            <legend>किसान की जानकारी</legend>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>दुग्ध् उत्पादक का नाम [Hindi]<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtProducerName" ErrorMessage="Enter Producer Name." Text="<i class='fa fa-exclamation-circle' title='Enter Producer Name !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtProducerName"
                                            ValidationExpression="^[^'@%#$&=^!~?]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtProducerName" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>दुग्ध् उत्पादक का नाम [English]<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtPnameInEnglish" ErrorMessage="Enter Producer Name." Text="<i class='fa fa-exclamation-circle' title='Enter Producer Name !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox ID="txtPnameInEnglish" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>पिता/पति का नाम <%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="rfvFather" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtFatherHusbandName" ErrorMessage="Enter Producer Name." Text="<i class='fa fa-exclamation-circle' title='Enter Father/Husband Name !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="REFatherHusbandName" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtFatherHusbandName"
                                            ValidationExpression="^[^'@%#$&=^!~?]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtFatherHusbandName" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>जन्मतिथि</label>
                                    
                                    <asp:TextBox ID="txtDOB" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="-3650d" runat="server"></asp:TextBox>
                                    <%-- <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                        <asp:TextBox ID="txtDOB" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="-3650d" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>वर्ग<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="DdlCategory" InitialValue="0" ErrorMessage="Select Category." Text="<i class='fa fa-exclamation-circle' title='Please Category !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="DdlCategory" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="चुने" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="सामान्य" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="अनु.जा." Value="2"></asp:ListItem>
                                        <asp:ListItem Text="अनु.ज.जा." Value="3"></asp:ListItem>
                                        <asp:ListItem Text="ओ.बी.सी." Value="4"></asp:ListItem>
                                        <asp:ListItem Text="NA" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>लिंग<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvGender" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlGender" InitialValue="0" ErrorMessage="Select Gender." Text="<i class='fa fa-exclamation-circle' title='Please Select Gender !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="चुने" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="पुरुष" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="स्त्री" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>मोबाइल नंबर <%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtMobile" ErrorMessage="Enter Mobile No." Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtMobile"
                                            ValidationExpression="[6-9]{1}[0-9]{9}"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Mobile No. !'></i>" ErrorMessage="Enter Valid Mobile No" />
                                    </span>
                                    <asp:TextBox ID="txtMobile" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>परिवार के कुल सदस्य<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtFamilyMembers" ErrorMessage="Enter Family Members No." Text="<i class='fa fa-exclamation-circle' title='Enter Family Members No.!'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtFamilyMembers"
                                            ValidationExpression="^[1-9][0-9]*|0$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Valid Family Members No.!'></i>" ErrorMessage="Enter Valid Family Members No." />
                                    </span>
                                    <asp:TextBox ID="txtFamilyMembers" MaxLength="2" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>भूमि की स्तिथि<%--<span style="color: red;">*</span>--%></label>
                                    <%-- <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvBhumiStithi" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlBhumiStithi" InitialValue="1" ErrorMessage="Select Bhumi Stithi." Text="<i class='fa fa-exclamation-circle' title='Please Select Bhumi Stithi !'></i>"></asp:RequiredFieldValidator>
                                </span>--%>
                                    <asp:DropDownList ID="ddlBhumiStithi" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="चुने" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="भूमिहर" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="भूमिहीन" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>कृषक का प्रकार<%--<span style="color: red;">*</span>--%></label>
                                    <%--   <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFarmerType" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlFarmerType" InitialValue="1" ErrorMessage="Select Farmer Type." Text="<i class='fa fa-exclamation-circle' title='Please Select Farmer Type !'></i>"></asp:RequiredFieldValidator>
                                </span>--%>
                                    <asp:DropDownList ID="ddlFarmerType" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="चुने" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="सिमान्त" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="लघु" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="मध्यम" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="बृहद" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>शैक्षणिक योग्यता <%--<span style="color: red;">*</span>--%></label>
                                    <%--<span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtEducation" ErrorMessage="Enter Education." Text="<i class='fa fa-exclamation-circle' title='Enter Education!'></i>"></asp:RequiredFieldValidator>                                          
                                        </span>--%>
                                    <asp:DropDownList ID="ddlEducation" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Text="चुने" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="प्राथमिक शिक्षा" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="हाई स्कुल" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="हायर सेकंडरी" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="स्नातक" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>ईमेल (यदि हो तो )</label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtProducerName" ErrorMessage="Enter Producer Name." Text="<i class='fa fa-exclamation-circle' title='Enter Producer Name !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revemail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid Email" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Email !'></i>" />
                                    </span>
                                    <asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>आधार नं.<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RfvAadhar" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAadhar" ErrorMessage="Enter Aadhar Number." Text="<i class='fa fa-exclamation-circle' title='Enter Aadhar Number !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ForeColor="Red" ControlToValidate="txtAadhar"
                                            ValidationExpression="^[0-9]{12}$"
                                            Display="Dynamic" ErrorMessage="Invalid Aadhar" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid Aadhar !'></i>" />
                                    </span>
                                    <asp:TextBox ID="txtAadhar" MaxLength="12" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>सदस्य कोड<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtCard" ErrorMessage="Enter Card Number." Text="<i class='fa fa-exclamation-circle' title='Enter Card Number !'></i>"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ForeColor="Red" ControlToValidate="txtCard"
                                            ValidationExpression="^[0-9]+$"
                                            Display="Dynamic" ErrorMessage="Invalid Card" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle' title='Invalid card !'></i>" />
                                    </span>
                                    <asp:TextBox ID="txtCard" MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>

                        </fieldset>
                    </div>

                    <fieldset>
                        <legend>पशुओ की जानकारी</legend>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>पशुओ की संख्या <%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtCattleNo" ErrorMessage="Enter Cattle No." Text="<i class='fa fa-exclamation-circle' title='Enter Cattle No !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtCattleNo"
                                            ValidationExpression="^[0-9]{1,10}$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Cattle No. !'></i>" ErrorMessage="Enter Valid Cattle No." />
                                    </span>
                                    <asp:TextBox ID="txtCattleNo" Text="0" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>गाय <%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtCowNo" ErrorMessage="Enter Cow No." Text="<i class='fa fa-exclamation-circle' title='Enter Cow No. !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtCowNo"
                                            ValidationExpression="^[0-9]{1,10}$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Cow No. !'></i>" ErrorMessage="Enter Valid Cow No." />
                                    </span>
                                    <asp:TextBox ID="txtCowNo" MaxLength="3" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>गाय ब्रिड<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtCowbreed" ErrorMessage="Enter Cow Breed." Text="<i class='fa fa-exclamation-circle' title='Enter  Cow Breed !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtCowbreed"
                                            ValidationExpression="^[^'@%#$&=^!~?]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtCowbreed" MaxLength="200" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>भैंस <%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtBuffelowNo" ErrorMessage="Enter buffalo No." Text="<i class='fa fa-exclamation-circle' title='buffalo No. !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtBuffelowNo"
                                            ValidationExpression="^[0-9]{1,10}$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter buffalo No. !'></i>" ErrorMessage="Enter Valid buffalo No." />
                                    </span>
                                    <asp:TextBox ID="txtBuffelowNo" MaxLength="60" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>भैंस ब्रिड<%--<span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtBuffBreed" ErrorMessage="Enter Buffalow Breed." Text="<i class='fa fa-exclamation-circle' title='Enter Buffalow Breed !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtBuffBreed"
                                            ValidationExpression="^[^'@%#$&=^!~?]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet Allow !'></i>" ErrorMessage="Only Alphabet Allow" />
                                    </span>
                                    <asp:TextBox ID="txtBuffBreed" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        कुल दुग्ध उत्पादन (लि./दिन )
                                   <%--     <span style="color: red;">*</span>--%>
                                    </label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtMilkProduce" ErrorMessage="Enter buffalo No." Text="<i class='fa fa-exclamation-circle' title='Milk Produce per day !'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtMilkProduce"
                                            ValidationExpression="^(\d*\.)?\d+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Milk Produce per day !'></i>" ErrorMessage="Enter Valid buffalo No." />
                                    </span>
                                    <asp:TextBox ID="txtMilkProduce" MaxLength="4" Text="0.00" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>बैंक की जानकारी </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        बैंक का नाम 
                                 <%--       <span style="color: red;">*</span>--%>
                                    </label>
                                    <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="DdlBank" InitialValue="0" ErrorMessage="Select Bank." Text="<i class='fa fa-exclamation-circle' title='Please Select Bank !'></i>"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <asp:DropDownList ID="DdlBank" OnSelectedIndexChanged="DdlBank_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        ब्रांच का नाम
                                       <%-- <span style="color: red;">*</span>--%></label>
                                    <%-- <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlBankBranch" InitialValue="0" ErrorMessage="Please Select Branch." Text="<i class='fa fa-exclamation-circle' title='Please Select Branch !'></i>"></asp:RequiredFieldValidator>
                                    </span>--%>
                                    <%-- <asp:DropDownList ID="ddlBankBranch" OnSelectedIndexChanged="ddlBankBranch_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtBankBranch" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtIFSC" ErrorMessage="Enter buffalo No." Text="<i class='fa fa-exclamation-circle' title='Milk Produce per day !'></i>"></asp:RequiredFieldValidator> 
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAddress"
                                            ValidationExpression="^[a-zA-z0-9\s]+$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Only Alphabet and Numbers Allow !'></i>" ErrorMessage="Only Alphabet and Numbers Allow" />--%>
                                    </span>
                                    <label>आई.एफ.एस.सी. कोड <%--<span style="color: red;">*</span>--%></label>
                                    <asp:TextBox ID="txtIFSC" MaxLength="60" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        खाता नं 
                                    <%--    <span style="color: red;">*</span>--%></label>
                                    <span class="pull-right">
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="txtAccountNo" ErrorMessage="Enter  Account No." Text="<i class='fa fa-exclamation-circle' title='Enter Account No!'></i>"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="a" ForeColor="Red" runat="server" ControlToValidate="txtAccountNo"
                                            ValidationExpression="^[0-9]{1,20}$"
                                            Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Enter Account No !'></i>" ErrorMessage="Enter Valid  Account No." />
                                    </span>
                                    <asp:TextBox ID="txtAccountNo" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>अपलोड करें </legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>आधार कार्ड <%--<span style="color: red;">*</span>--%></label>
                                    <asp:FileUpload ID="FUAadharCard" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>बैंक पासबुक <%--<span style="color: red;">*</span>--%></label>
                                    <asp:FileUpload ID="FUPassBook" runat="server" />
                                </div>
                            </div>

                        </div>
                    </fieldset>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="a" CssClass="btn btn-primary" runat="server" Text="Submit" />
                            </div>
                        </div>
                    </div>


                    <div class="col-md-12">

                        <asp:GridView ID="GridDetails" runat="server" CssClass="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ProducerId" OnRowCommand="GridDetails_RowCommand" OnDataBinding="GridDetails_DataBinding">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="दुग्ध् उत्पादक का नाम">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProducerName" Text='<%# Eval("ProducerName") + "("+Eval("ProducerCardNo")+")" %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Name In English">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProducerNameEnglish" Text='<%# Eval("ProducerNameEnglish") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Mobile" HeaderText="मोबाइल नंबर" />
                                <asp:BoundField DataField="Gender" HeaderText="जेंडर" />
                                <asp:BoundField DataField="CategoryName" HeaderText="वर्ग" />
                                <asp:BoundField DataField="FarmerType" HeaderText="कृषक का प्रकार" />
                                <asp:BoundField DataField="BankName" HeaderText="बैंक का नाम" />
                                <asp:BoundField DataField="IFSC" HeaderText="IFSC कोड" />
                                <asp:BoundField DataField="AccountNo" HeaderText="खाता क्रमांक" />

                                <asp:TemplateField HeaderText="अपडेट करें">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="button button-mini button-green" CommandName="EditRequest" CommandArgument='<%# Bind("ProducerId") %>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="कार्ड प्रिंट">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button button-mini button-green" CommandName="PrintRequest" CommandArgument='<%# Bind("ProducerId") %>'><i class="fa fa-print"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No District Found</EmptyDataTemplate>
                        </asp:GridView>

                    </div>



                </div>
        </section>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 style="float: left;">दुग्ध उत्पादक पहचान पत्र</h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                    </button>
                </div>
                <div class="clearfix"></div>
                <div class="modal-body">
                    <div class="row">
                        <div id="DivIdToPrint">
                            <div class="pagebreak">
                                <table width="100%" style="background: url(../image/card_wm.png) no-repeat center center;">
                                    <tr>
                                        <td align="center" colspan="2" style="line-height: 18px;">
                                            <div style="float: left; width: 15%; text-align: left;">
                                                <img src="../image/bds_logo.png" style="width: 36px;" />
                                            </div>
                                            <div style="float: left; text-align: center; width: 85%;">
                                                <p style="text-align: center; text-transform: uppercase; font-size: 11pt; margin: 0; font-weight: bold;">
                                                    <asp:Label ID="lblds" runat="server"></asp:Label>
                                                </p>
                                                <p style="text-align: center; text-transform: uppercase; font-size: 9pt; margin: 0; font-weight: bold; color: #d72f31; padding: 0;">(दुग्ध उत्पादक पहचान पत्र)</p>
                                                <p style="text-align: center; border: 1px solid #000; background: #000; width: 40px; margin: 5px auto 10px;"></p>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="35%" style="font-size: 12px;">दुग्ध समिति का नाम :</td>
                                        <td valign="top" width="65%">
                                            <asp:Label ID="lblSociety" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: 12px;">विकासखण्ड :</td>
                                        <td valign="top">
                                            <asp:Label ID="lblBlock" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: 12px;">सदस्यता क्र. :</td>
                                        <td valign="top">
                                            <asp:Label ID="lblCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: 12px;">सदस्य का नाम :</td>
                                        <td valign="top">
                                            <asp:Label ID="lblProducer" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: 12px;">मोबाइल :</td>
                                        <td valign="top">
                                            <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="position: absolute; bottom: 40px; right: 10px;">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <div class="pagebreak">
                                <table width="100%" style="background: url(../image/card_wm.png) no-repeat center center;">
                                    <tr>
                                        <td align="center" colspan="2" style="line-height: 18px;">
                                            <div style="float: left; width: 15%; text-align: left;">
                                                <img src="../image/bds_logo.png" style="width: 36px;" />
                                            </div>
                                            <div style="float: left; text-align: center; width: 85%;">
                                                <p style="text-align: center; text-transform: uppercase; font-size: 11pt; margin: 0; font-weight: bold;">
                                                    <asp:Label ID="lblDS1" runat="server"></asp:Label>
                                                </p>
                                                <p style="text-align: center; text-transform: uppercase; font-size: 9pt; margin: 0; font-weight: bold; color: #d72f31; padding: 0;">(दुग्ध उत्पादक पहचान पत्र)</p>
                                                <p style="text-align: center; border: 1px solid #000; background: #000; width: 40px; margin: 5px auto 10px;"></p>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <ol style="margin: 5px 10px 0px; padding: 0px; line-height: 18px;">
                                                <li>यह कार्ड सर्वथा अहस्तांतरणीय है ।</li>
                                                <li>इस कार्ड को सुरक्षित रखने की पूर्ण जिम्मेदारी सदस्य की होगी ।</li>
                                                <li>सदस्य को समिति में दुग्ध प्रदाय करते समय कार्ड प्रस्तुत करना होगा ।</li>
                                                <li>कार्ड खोने पर समिति में आवेदन देकर तथा रु. 100 का शुल्क देकर नया कार्ड प्राप्त किया जा सकेगा । </li>
                                            </ol>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="baseline" style="padding: 20px 20px 0; font-size: 12px;">
                                            <div style="width: 50%; float: left;">सचिव का नाम</div>
                                            <div style="width: 50%; float: left; text-align: right;">हस्ताक्षर सचिव</div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <input type='button' id='btn' class="btn mt-2 mb-2 btn-sm btn-primary" value='PRINT' onclick="printDiv();" />
                </div>
            </div>
        </div>

        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script src="../../js/jquery-1.10.2.js"></script>
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8,9]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>

    <script>
        function ShowModal() {
            $('#myModal').modal('toggle');
        }
        function HideModal() {
            $('#myModal').hide(t);
        }
        function printData() {
            var divToPrint = document.getElementById("printTable");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }
        //$(document).ready(function () {
        //    $(".dataTable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        //    $("header").addClass("fix");
        //});
        //$('#button_print').on('click', function () {
        //    printData();
        //})

        // $('.dataTable').datepicker();
        //$(document).ready(function () {
        //    alert('1');
        //    $('dataTable').dataTable();
        //});

    </script>
</asp:Content>

