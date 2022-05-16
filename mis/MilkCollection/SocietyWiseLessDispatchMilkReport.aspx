<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SocietyWiseLessDispatchMilkReport.aspx.cs" Inherits="mis_MilkCollection_SocietyWiseLessDispatchMilkReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
         @media print {
             
              .noprint {
                display: none;
            }
              
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row ">
                <div class="col-md-12">
                    <div class="box box-primary ">
                        <div class="box-header">
                            <h3 class="box-title">Less than Filter Milk Supply</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body noprint">
                            <fieldset>
                        <legend>Filter</legend>
                        <div class="row">
                            <div class="col-md-12">

                                <%--<div class="col-md-2">
                                    <label>Milk Collection Unit<span style="color: red;">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ValidationGroup="a" ControlToValidate="ddlMilkCollectionUnit" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Milk Collection Unit!'></i>" ErrorMessage="Select Milk Collection Unit" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </span>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlMilkCollectionUnit"  CssClass="form-control select2" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="5">BMC</asp:ListItem>
                                            <asp:ListItem Value="6">DCS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                 <div class="col-md-2">
                                <div class="form-group">
                                    <label>CC<span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select CC" Text="<i class='fa fa-exclamation-circle' title='Select CC !'></i>"
                                            ControlToValidate="ddlccbmcdetail" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>
                                    <asp:DropDownList ID="ddlccbmcdetail"  runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>From Date </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="rfv1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="txtFdt" errormessage="Please Enter From Date." text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:requiredfieldvalidator>
                                           
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtFdt" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date </label>
                                        <span style="color: red">*</span>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" validationgroup="a" display="Dynamic" controltovalidate="txtFdt" errormessage="Please Enter From Date." text="<i class='fa fa-exclamation-circle' title='Please Enter From Date !'></i>"></asp:requiredfieldvalidator>
                                           
                                        </span>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtTdt" onkeypress="javascript: return false;" width="100%" maxlength="10" onpaste="return false;" placeholder="Select From Date" runat="server" autocomplete="off" cssclass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                <div class="form-group">
                                    <label>No of Dispatch Time<span style="color: red;"> *</span></label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            InitialValue="0" ErrorMessage="Select No of Dispatch Time" Text="<i class='fa fa-exclamation-circle' title='Select No of Dispatch Time !'></i>"
                                            ControlToValidate="ddlDispatchTime" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>--%>
                                    <asp:DropDownList ID="ddlDispatchTime"  runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="5">1-5</asp:ListItem>
                                        <asp:ListItem Value="10">6-10</asp:ListItem>
                                        <%--<asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>


                                <div class="col-md-1" style="margin-top: 20px;" runat="server">
                                    <div class="form-group">
                                        <asp:button runat="server" cssclass="btn btn-primary"  validationgroup="a" id="btnSubmit" OnClick="btnSubmit_Click"  text="Search" accesskey="S" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Report</legend>
                                 <div class="row">
                                    <div class="col-md-12 noprint">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                             <asp:Button ID="btnprint" Text="Print" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                        </div>
                                    </div>

                                     <div class="col-md-12">
                                         <div class="table-responsive">
                                             <asp:GridView ID="gvReport" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCreated="gvReport_RowCreated" OnRowCommand="gvReport_RowCommand">
                                                <HeaderStyle BackColor="Tan" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center"/>
                                                  <Columns>
                                                     <asp:TemplateField HeaderText="S.No">
                                                         <ItemTemplate>
                                                             <asp:Label id="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Society">
                                                         <ItemTemplate>
                                                             <asp:Label id="lblSociety" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Milk Cycle">
                                                         <ItemTemplate>
                                                             <asp:Label id="lblActualDispatchCount" runat="server" Text='<%# Eval("ActualDispatchCount") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Dispatched Milk Cycle">
                                                         <ItemTemplate>
                                                             <asp:Label id="lblDipatchcount" runat="server" Text='<%# Eval("Dipatchcount") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Dispatch Detail" ItemStyle-CssClass="noprint" HeaderStyle-CssClass="noprint">
                                                         <ItemTemplate>
                                                             <asp:LinkButton id="lnkView" runat="server" Text="View" CommandName="ViewRecord" CommandArgument='<%# Eval("Office_ID") %>'></asp:LinkButton>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                 </Columns>
                                             </asp:GridView>
                                         </div>
                                     </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="dvReport" runat="server"></div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="modal fade" id="ModalDispatchDetailView" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Dispatch Details</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVViewDispatchDetails" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EntryDate" HeaderText="Date" />
                                        <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                        <asp:BoundField DataField="MilkType" HeaderText="Buf/Cow" />
                                        <asp:BoundField DataField="MilkQuantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="MilkQuality" HeaderText="Category"/>
                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function ShowModal() {
            $('#ModalDispatchDetailView').modal('show');
        }
    </script>
</asp:Content>

