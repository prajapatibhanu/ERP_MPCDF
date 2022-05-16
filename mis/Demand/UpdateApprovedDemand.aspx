<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateApprovedDemand.aspx.cs" Inherits="mis_Demand_UpdateApprovedDemand" %>

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
                        <h3 class="box-title">Update Approved Demand</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <fieldset>
                            <legend>Date ,Shift,Item Category / दिनांक ,शिफ्ट , वस्तू वर्ग
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
                                                ControlToValidate="txtOrderDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtOrderDate"
                                                ErrorMessage="Invalid Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOrderDate" MaxLength="10" placeholder="Select Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                     </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Shift / शिफ्ट</label>
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
                                        <label>Item Category / वस्तू वर्ग</label>
                                        <asp:DropDownList ID="ddlItemCategory" OnInit="ddlItemCategory_Init" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>  

                                    <div class="col-md-4" id="pnlSearchBy" runat="server" visible="true">
                                <div class="form-group">
                                        <label></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                               ErrorMessage="Select Route or Distributor or Institution" Text="<i class='fa fa-exclamation-circle' title='Select Route or Distributor or Institution !'></i>"
                                                ControlToValidate="rblReportType" ForeColor="Red" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:RadioButtonList ID="rblReportType"  runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem class="radio-inline" Text="Route wise&nbsp;&nbsp;" Value="1"></asp:ListItem> 
                                            <asp:ListItem class="radio-inline" Text="Distributor wise&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                             <asp:ListItem class="radio-inline" Text="Institution Wise&nbsp;&nbsp;" Value="3"></asp:ListItem>
                                            
                                        </asp:RadioButtonList>
                                    </div>
                                </div> 
                                      <div class="col-md-1">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-primary" OnClick="btnSearch_Click" ValidationGroup="a" ID="btnSearch" Text="Search" />

                                        </div>
                                    </div>
                                    <div class="col-md-1" style="margin-top: 20px;">
                                        <div class="form-group">

                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="btn btn-default" />
                                        </div>
                                    </div>                    
                            </div>                          
                        </fieldset>
                               
                    </div>

                </div>
            </div>
                 <div class="col-md-12" id="pnlData" runat="server" visible="false">
                <div class="box box-Manish">
                    <div class="box-header">
                        <h3 class="box-title">Update Approved Demand List</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label ID="lblReportMsg" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row" id="pnlrouteOrDistOrInstdata" runat="server" visible="false">   
                                <div class="col-md-12" >
                                        <fieldset>
                                            <legend><span id="pnllegand" runat="server"></span></legend>
                                           
                                                <div class="col-md-12">
                                                   <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Route">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRoute" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Route") %>' CommandName="RoutwiseBooth" CommandArgument='<%#Eval("RouteId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                                                        <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" ShowFooter="true" class="datatable table table-striped table-bordered" AllowPaging="false"
                                                AutoGenerateColumns="true" EmptyDataText="No Record Found."  EnableModelValidation="True">
                                                <Columns>
                                                   <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Distributor/Superstockist Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnDistributor" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Distributor/Superstockist Name") %>' CommandName="DistwiseBooth" CommandArgument='<%#Eval("DistributorId") %>' runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               </Columns>
                                               
                                            </asp:GridView>
                                                   <asp:GridView ID="GridView3" runat="server" OnRowDataBound="GridView3_RowDataBound" OnRowCommand="GridView3_RowCommand" class="datatable table table-striped table-bordered" AllowPaging="false"
                                            AutoGenerateColumns="true" ShowFooter="true" EmptyDataText="No Record Found." EnableModelValidation="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." HeaderStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Institution Name">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnOrganization" CssClass="btn btn-block btn-secondary" Text='<%#Eval("Organization Name") %>' CommandName="Orgwise" CommandArgument='<%#Eval("OrganizationId") %>' runat="server"></asp:LinkButton>
                                                            </ItemTemplate>
                                                </asp:TemplateField> 
                                            </Columns>
                                        </asp:GridView>    
                                                </div>
                                            </div>                                 
                                        </fieldset>
                                    </div>
                            </div>

                      
                               
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
