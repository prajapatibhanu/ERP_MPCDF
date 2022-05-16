<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptBMCEntryAtQC.aspx.cs" Inherits="mis_MilkCollection_RptBMCEntryAtQC" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .border
        {
            border-top:1px solid black;
            border-left:1px solid black;
            border-right:1px solid black;
            border-bottom:1px solid black;
        }
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

            .pagebreak {
                page-break-before: always;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <%--ConfirmationModal End --%>
   
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">BMC/DCS Milk Collection Entry Report</h3>
                        </div>
                        <asp:label id="lblMsg" runat="server" text=""></asp:label>
                        <div class="box-body">
                           
                            <div class="row noprint">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <span class="pull-right">
                                            <asp:requiredfieldvalidator id="rfvDate" runat="server" display="Dynamic" controltovalidate="txtDate" text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" errormessage="Enter Date" setfocusonerror="true" forecolor="Red" validationgroup="Save"></asp:requiredfieldvalidator>
                                        </span>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="far fa-calendar-alt"></i>
                                                </span>
                                            </div>
                                            <asp:textbox id="txtDate" autocomplete="off" cssclass="form-control DateAdd" data-date-end-date="0d" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:textbox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Reference No.<span style="color: red;"> *</span> 
                                    </label>
                                    <%--<span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                            InitialValue="0" ErrorMessage="Select Reference No" Text="<i class='fa fa-exclamation-circle' title='Select Reference No !'></i>"
                                            ControlToValidate="ddlReferenceNo" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator></span>--%>
                                    <asp:DropDownList ID="ddlReferenceNo" Width="100%" runat="server" CssClass="form-control select2" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:button id="btnSearch" runat="server" style="margin-top: 22px;" text="Search" cssclass="btn btn-success" validationgroup="Save" onclick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 pull-left noprint">
                            <div class="form-group">
                                <asp:Button ID="btnPrint" runat="server" Visible="false" CssClass="btn btn-default" Text="Print" OnClientClick="window.print();"/>
                                <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-default" Text="Excel" OnClick="btnExport_Click" />
                            </div>
                                
                            </div>

                                <div class="col-md-12">
								<asp:Label ID="lblRptMsg" runat="server" Text=""></asp:Label>
                                     <div id="divReport" runat="server">
                                         
                                     </div>
                                    <div class="table-responsive">

                                        <asp:gridview id="gvBMCDetails"  cssclass="table table-bordered" autogeneratecolumns="false" EmptyDataText ="No Record Found" runat="server">                             
                                     <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                
                                              <asp:Label ID="lblRowNo" Visible='<%# Eval("RownoVisible").ToString()=="Yes"?true:false %>' runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                          
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name_E" Font-Bold='<%# Eval("RownoVisible").ToString()=="No"?true:false %>' runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Temp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Temp" runat="server" Text='<%# Eval("Temp") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblD_MilkQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT" runat="server" Text='<%# Eval("FAT").ToString()=="0.00"?" ":Eval("FAT").ToString() %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR").ToString()=="0.00"?" ":Eval("CLR").ToString() %>'></asp:Label>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("SNF").ToString()=="0.00"?" ":Eval("SNF").ToString() %>'></asp:Label> 
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kg Fat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKgFat" runat="server" Text='<%# Eval("FatKg") %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kg SNF">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKgSNF" runat="server" Text='<%# Eval("SnfKg") %>'></asp:Label>                                           
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     
                                    </Columns>
                                </asp:gridview>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div id="divPrintReport" class="NonPrintable" runat="server">
                
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    
</asp:Content>

