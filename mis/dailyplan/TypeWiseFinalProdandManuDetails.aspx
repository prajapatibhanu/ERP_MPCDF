<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TypeWiseFinalProdandManuDetails.aspx.cs" Inherits="mis_dailyplan_TypeWiseFinalProdandManuDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
   
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Type Wise Final Product Manufactured & Balance Details</h3>
                </div>
                <div class="box-body">
                    <div class="row noprint">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" visible="false">
                            <div class="form-group">
                                <label>Shift</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="ddlShift" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Shift!'></i>" ErrorMessage="Select Shift" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlShift" AutoPostBack="true" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection"  runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                         <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvFDate" runat="server" Display="Dynamic" ControlToValidate="txtFDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtFDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtTDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-2">
                            <div class="form-group">
                                <label>Type</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="ddlType" Text="<i class='fa fa-exclamation-circle' title='Select Type!'></i>" ErrorMessage="Select Type" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlType" onkeypress="javascript: return false;" Width="100%"  onpaste="return false;"  runat="server"  CssClass="form-control"  ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:Button ID="btnSearch" style="margin-top:21px;" runat="server" CssClass="btn btn-success" Text="Search" ValidationGroup="Submit" OnClick="btnSearch_Click"></asp:Button>
                            </div>
                        </div>
                    </div>

             
                         <div class="row">
                             <div class="col-md-12">
                                <div class="form-group">

                                      <asp:Button ID="btnExport" runat="server" Text="Export" Visible="false" CssClass="btn btn-primary noprint" OnClick="btnExport_Click"/>
                                      <asp:Button ID="btnprint" Visible="false" Text="Print" runat="server" CssClass="btn btn-primary noprint" OnClientClick="window.print();" />
                                </div>
                                 <div id="divreport" runat="server">

                         </div>
                         </div>
                            </div>
          

                </div>
            </div>
        </section>
         <section class="content">
            <div id="divprint" class="NonPrintable" runat="server"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

