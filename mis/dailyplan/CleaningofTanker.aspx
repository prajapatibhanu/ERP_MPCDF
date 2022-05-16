<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CleaningofTanker.aspx.cs" Inherits="mis_dailyplan_CleaningofTanker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
         .NonPrintable {
                  display: none;
              }
        @media print {
              .NonPrintable {
                  display: block;
              }
              .noprint {
                display: none;
            }
              
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <%--Confirmation Modal Start --%>

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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes"  Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="save" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Cleaning Of Tanker</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Cleaning Of Tanker</legend>
                        <div class="row">
                           <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label><span class="text-danger">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtEntryDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtEntryDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Tanker No</label>
                                    <asp:DropDownList ID="ddlTankerNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" CssClass="btn btn-success" style="margin-top:21px;" runat="server" Text="Save" OnClick="btnSave_Click"/>
                                    <a href="CleaningofTanker.aspx" class="btn btn-default" style="margin-top:21px;" runat="server">Clear</a>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Details</legend>
                        <div class="row">
                           <div class="col-md-2">
                            <div class="form-group">
                                <label>Date</label><span class="text-danger">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFilterDate" runat="server" Display="Dynamic" ControlToValidate="txtFilterDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </span>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="far fa-calendar-alt"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="txtFilterDate" autocomplete="off" placeholder="Date" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtFilterDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>  
                            
                        </div>
                        <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" EmptyDataText="No Record Found" runat="server" ShowHeaderWhenEmpty ="true" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tanker No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTankerNo" runat="server" Text='<%# Eval("V_VehicleNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    
                                                    <asp:LinkButton ID="lnkView" CssClass="label label-default" runat="server" CommandArgument='<%# Eval("CleaningOfTanker_ID") %>' Visible='<%# Eval("Status").ToString()=="Pending"?false:true %>' CommandName="ViewRecord">Print</asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns> 
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    </fieldset>
                    
                </div>
            </div>
        </section>
    </div>
    <section class="content">
          <div id="divPrint"  class="NonPrintable" runat="server"></div>
      </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

