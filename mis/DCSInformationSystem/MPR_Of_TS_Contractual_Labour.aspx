<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MPR_Of_TS_Contractual_Labour.aspx.cs" Inherits="mis_DCSInformationSystem_MPR_Of_TS_Contractual_Labour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
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
                         <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
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
                    <h3 class="box-title">Monthly Progress Report Of Transport Section</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <asp:Label ID="lblMCL_ID" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>
                    
                    <fieldset>
                        <legend>Contractual Labour
                        </legend>
                        <div class="row">
                             <div class="col-md-12">
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:DropDownList ID="DDlMonth" runat="server"  CssClass="form-control" >
                                            <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                      <asp:DropDownList ID="ddlyear" runat="server"  CssClass="form-control"  >
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
                                    <asp:Label ID="lblOpeningBalance" Text="Number " Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <%-- <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                   <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label41" Text="Un-Skilled" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvnmUn_Skilled" ValidationGroup="a"
                                            ErrorMessage="Enter Number Un-Skilled" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Number Un-Skilled !'></i>"
                                            ControlToValidate="txtnmUn_Skilled" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtnmUn_Skilled" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label46" Text="Semi Skilled" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <asp:RequiredFieldValidator ID="rfvnmSemi_Skilled" ValidationGroup="a"
                                            ErrorMessage="Enter Number Semi Skilled" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Number Semi Skilled !'></i>"
                                            ControlToValidate="txtnmSemi_Skilled" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtnmSemi_Skilled" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label47" Text="Skilled" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvnmSkilled" ValidationGroup="a"
                                            ErrorMessage="Enter Number Skilled" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Number Skilled !'></i>"
                                            ControlToValidate="txtnmSkilled" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                      <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtnmSkilled" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                                 </div>
                            <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label19" Text="Wages Bill Amount " Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <%-- <asp:RequiredFieldValidator ID="rfvrfvDCScode" ValidationGroup="a"
                                            ErrorMessage="Enter DCS Code" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter DCS Code !'></i>"
                                            ControlToValidate="txtDCScode" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDCScode" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                   <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtDCScode" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label20" Text="Un-Skilled" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvWBAUn_Skilled" ValidationGroup="a"
                                            ErrorMessage="Enter WBA Un-Skilled" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter WBA Un-Skilled !'></i>"
                                            ControlToValidate="txtWBAUn_Skilled" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                           <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtWBAUn_Skilled" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label21" Text="Semi Skilled" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvWBASemi_Skilled" ValidationGroup="a"
                                            ErrorMessage="Enter WBA Semi Skilled" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter WBA Semi Skilled !'></i>"
                                            ControlToValidate="txtWBASemi_Skilled" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                           <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtWBASemi_Skilled" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                                 <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label22" Text="Skilled" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                    <span class="pull-right">
                                       <asp:RequiredFieldValidator ID="rfvWBASkilled" ValidationGroup="a"
                                            ErrorMessage="Enter WBA Skilled" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter WBA Skilled !'></i>"
                                            ControlToValidate="txtWBASkilled" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                           <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                    </span>
                                    <asp:TextBox runat="server" Text="0"  autocomplete="off" onkeypress="return validateDec(this, event)" CssClass="form-control" ID="txtWBASkilled" MaxLength="150" placeholder="" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                                 </div>

                             
                            

                             
                            
                        </div>
                       
                        <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary" ValidationGroup="a" ID="btnSubmit" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S" OnClick="btnSubmit_Click" />
                                   <asp:Button runat="server" CssClass="btn btn-block btn-primary" Visible="false" ValidationGroup="a" ID="btnupdate" Text="Update" OnClientClick="return ValidatePage();" AccessKey="S"  OnClick="btnupdate_Click"/>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="MPR_Of_TS_Contractual_Labour.aspx">Clear</a>
                            </div>
                        </div>
                    </div>

                        <div class="row">
                       
                    </div>
                         
                    </fieldset>

                    
                </div>
            </div>
            <!-- /.box-body -->
            <div class="box box-body" >
                <div class="box-header">
                    <h3 class="box-title">Contractual Labour Info</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                          <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" Text="Month" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
                                        <span class="pull-right">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                            ControlToValidate="DDlMonth" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                       <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtDCScode"
                                            ErrorMessage="Only Alphanumeric Allow in DCS Code" Text="<i class='fa fa-exclamation-circle' title='Only Alphanumeric Allow in DCS Code !'></i>"
                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]+$">
                                        </asp:RegularExpressionValidator>--%>
                                        </span>
                                        <asp:DropDownList ID="DDlMonth2" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlMonth2_SelectedIndexChanged" >
                                             <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" Text="Year" Style="display: inline-block; max-width: 100%; margin-bottom: 5px; font-weight: 700;" runat="server"></asp:Label><span style="color: red;"> *</span>
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
                                        <asp:DropDownList ID="ddlyear2" runat="server" CssClass="form-control"  OnSelectedIndexChanged="DDlMonth2_SelectedIndexChanged" >
                                            <%--<asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                                <asp:ListItem Value="1">Tanker</asp:ListItem>
                                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                                <asp:ListItem Value="3">Jeeps & Cars</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              <div class="col-md-4">
                         <asp:Button runat="server" BackColor="#2e9eff" CssClass="btn btn-success" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" Style="margin-top: 20px; width: 80px;" />
                       </div>
                            </div>
                        
                                <div class="col-md-12">
                        <asp:GridView runat="server" ID="gvCLdetail" Visible="false"  CssClass="table table-bordered"   ShowHeader="true" ShowFooter="false" AllowPaging="false" AutoGenerateColumns="false" OnSelectedIndexChanging="gvCLdetail_SelectedIndexChanging" OnRowCommand="gvCLdetail_RowCommand" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                             <asp:Label Visible="false" ID="lblMCL_Id" Text='<%# Eval("MCL_Id").ToString()%>' runat="server" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblYear" Text='<%# Eval("Year").ToString()%>' runat="server" class="form-control"></asp:Label>

                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Month">
                                                        <ItemTemplate>
                                                         <asp:Label  ID="lblMonth" Text='<%# Eval("month_name").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Un-Skilled(NUM) ">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblNum_unskilled" Text='<%# Eval("Num_unskilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Semi Skilled(NUM)">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblNum_semi_skilled" Text='<%# Eval("Num_semi_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skilled(NUM) ">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblNum_skilled" Text='<%# Eval("Num_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Un-Skilled(AMT)">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblWBA_unskilled" Text='<%# Eval("WBA_unskilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Semi Skilled(AMT)">
                                                        <ItemTemplate>
                                                                <asp:Label  ID="lbWBA_semi_skilled" Text='<%# Eval("WBA_semi_skilled").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                     
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skilled(AMT) ">
                                                        <ItemTemplate>
                                                               <asp:Label  ID="lblWBA_skilled" Text='<%# Eval("WBA_skilled").ToString()%>'  runat="server" class="form-control"></asp:Label>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Creation Date">
                                                        <ItemTemplate>
                                                              <asp:Label  ID="lblCreatedAt" Text='<%# Eval("CreatedAt").ToString()%>' runat="server" class="form-control"></asp:Label>
                                                        
                                                             </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-CssClass="text-center" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="EDIT" runat="server" Width="100%" CausesValidation="False"  ImageUrl="~/mis/image/edit.png" CommandName="select" CommandArgument='<%# Bind("MCL_Id") %>'></asp:ImageButton>
                                                                    </ItemTemplate>
                                                     </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                    </div>
                    </div>
                </div>

            </div>
            
        </section>
        <!-- /.content -->
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
        function onlyNumber(ob) {
            var invalidChars = /\D+/g;
            if (invalidChars.test(ob.value)) {
                ob.value = ob.value.replace(invalidChars, "");
            }
        }

        function validateDec(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            var chkhyphen = el.value.split('-');
            //if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45) {
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {

                return false;
            }
            //just one dot (thanks ddlab)
            if ((number.length > 1 && charCode == 46) || (chkhyphen.length > 1 && charCode == 45)) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }


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


