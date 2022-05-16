<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_RoutewiseVehicleSheet.aspx.cs" Inherits="mis_DemandSupply_Rpt_RoutewiseVehicleSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .columngreen {
            background-color: #aee6a3 !important;
        }

        .columnred {
            background-color: #f05959 !important;
        }

        .columnmilk {
            background-color: #bfc7c5 !important;
        }

        .columnproduct {
            background-color: #f5f376 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">


                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Route wise Vehicle Sheet</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Route wise Vehicle Sheet<%--Date ,Shift,Item Category,Route,Vehicle No. / दिनांक ,शिफ्ट ,वस्तू वर्ग, मार्ग,वाहन क्र.--%>
                                </legend>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>


                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date / दिनांक<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date !'></i>"
                                                    ControlToValidate="txtDeliveryDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDeliveryDate"
                                                    ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" ID="txtDeliveryDate" autocomplete="off" CssClass="form-control" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Shift / शिफ्ट<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Shift" Text="<i class='fa fa-exclamation-circle' title='Select Shift !'></i>"
                                                    ControlToValidate="ddlShift" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlShift" runat="server" OnInit="ddlShift_Init" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Item Category / वस्तू वर्ग<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Category" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                    ControlToValidate="ddlItemCategory" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location / लोकेशन<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Location" Text="<i class='fa fa-exclamation-circle' title='Select Location !'></i>"
                                                    ControlToValidate="ddlLocation" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLocation" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Route / मार्ग<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Route" Text="<i class='fa fa-exclamation-circle' title='Select Route !'></i>"
                                                    ControlToValidate="ddlRoute" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control select2">
                                                  <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                       <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Vehicle No./वाहन क्र. </label>
                                             <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    InitialValue="0" ErrorMessage="Select Vehicle No." Text="<i class='fa fa-exclamation-circle' title='Select Vehicle No. !'></i>"
                                                    ControlToValidate="ddlVehicleNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>
                                            <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 22px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" AccessKey="S" />

                                        </div>
                                    </div>
                                     <div class="col-md-1" style="margin-top: 22px;">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-default" OnClick="btnClear_Click" ID="btnClear" Text="Clear" />

                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>

                    </div>
                </div>

                <div class="col-md-12" id="pnldata" runat="server" visible="false">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Route wise Vehicle Sheet</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <fieldset>
                                <legend>Route wise Vehicle Sheet
                                </legend>
                               

                                <div class="row">
                                   <div class="col-md-12">
                                        <div class="col-md-3">
                                        <div class="form-group">
                                           <span style="color:black;">  Date :</span>
                                          <b><asp:Label ID="lblDate" runat="server"></asp:Label></b>
                                       </div>
                                           </div>
                                        <div class="col-md-3">
                                        <div class="form-group">
                                             <span style="color:black;">Shift : </span>
                                        <b> <asp:Label ID="lblShift" runat="server"></asp:Label></b>
                                       </div>
                                           </div>
                                        <div class="col-md-3">
                                        <div class="form-group">
                                           <span style="color:black;"> Item Category :</span>
                                       <b>  <asp:Label ID="lblItemCategory" runat="server"></asp:Label></b>
                                       </div>
                                           </div>
                                       <div class="col-md-3">
                                        <div class="form-group">
                                         <span style="color:black;">    Route : </span>
                                        <b> <asp:Label ID="lblRouteName" runat="server"></asp:Label></b>
                                       </div>
                                           </div>
                                       <div class="col-md-3">
                                        <div class="form-group">
                                          <span style="color:black;"> Vehicle No. :</span>
                                        <b>   <asp:Label ID="lblVehicleNo" runat="server"></asp:Label></b>
                                       </div>
                                           </div>
                                  </div>
                                    <div class="col-md-12">

                                    <div class="table-responsive">
                                         <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                    AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Route">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkbtnRoute" Text='<%#Eval("Route") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                      <div id="divtable" runat="server"></div>
                                    </div>
                                        
                                    </div>
                                    </div>
                            </fieldset>

                        </div>

                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

