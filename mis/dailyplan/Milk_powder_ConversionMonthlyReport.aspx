<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mis/MainMaster.master" CodeFile="Milk_powder_ConversionMonthlyReport.aspx.cs" Inherits="mis_dailyplan_Milk_powder_ConversionMonthlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
   
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="box box-primary">
                <div class="box-header no-print">
                    <h3 class="box-title">Milk Powder Conversion Monthly Report</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>Dugdh Sangh<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>Production Section<span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlPSection" runat="server" CssClass="form-control"></asp:DropDownList>
                                <small><span id="valddlDSPS" class="text-danger"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>From Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" Display="Dynamic" ControlToValidate="txtFDate" Text="<i class='fa fa-exclamation-circle' title='Enter From Date!'></i>" ErrorMessage="Enter From Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtFDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 no-print">
                            <div class="form-group">
                                <label>To Date</label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtTDate" Text="<i class='fa fa-exclamation-circle' title='Enter To Date!'></i>" ErrorMessage="Enter To Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ID="txtTDate" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 no-print">
                            <div class="form-group" style="margin-top : 20px;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button id="btnPrint" runat="server" class="btn btn-primary noprint" Text="Print" OnClientClick="window.print();"></asp:Button>
                                <asp:Button ID="btnExport" runat="server" Text="Export" Visible="false" CssClass="btn btn-primary noprint " OnClick="btnExport_Click"/>

                            </div>
                        </div>
                    </div>
                    <div id="dvdetails" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                              <h3 style="text-align:center; margin:7px; font-weight: 800; font-size: 20px;"><asp:Label ID="lblOfficeName"  runat="server" Text=""></asp:Label></h3>
                              
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">

                         <h5 style='text-align:center; margin:7px; font-weight: 500; font-size: 13px;'><asp:Label ID ="lbldairy" runat="server" Text=""></asp:Label></h5>

                            </div>
                        </div>

                        <div class="row">
                           
                            <div class="col-md-12 ">

                                   <h6 style="text-align:center; margin:7px; font-weight:800; font-size:18px;"><asp:Label ID="lblSheetName" runat="server" Text=""></asp:Label></h6> 
                                
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                    <h5 style="text-align:center;"><asp:Label ID="lblDate" runat="server" Text=""></asp:Label></h5>

                            </div>
                        </div>

                        <div class="row">
                            <asp:GridView ID="gvInflow" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particular">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIParticular" runat="server" Text='<%# Eval("ParticularName").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIQty" runat="server" Text='<%# Eval("Qty").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Fat %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIFatPer" runat="server" Text='<%# Eval("FatPer").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Fat In Kg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIFatInKg" runat="server" Text='<%# Eval("FatInKg").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SNF %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblISNFPer" runat="server" Text='<%# Eval("SNFPer").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="SNFInKg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblISNFInKg" runat="server" Text='<%# Eval("SNFInKg").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:GridView ID="gvOutflow" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particular">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOParticular" runat="server" Text='<%# Eval("ParticularName").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOQty" runat="server" Text='<%# Eval("Qty").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Fat %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFatPer" runat="server" Text='<%# Eval("FatPer").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Fat In Kg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOFatInKg" runat="server" Text='<%# Eval("FatInKg").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SNF %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOSNFPer" runat="server" Text='<%# Eval("SNFPer").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="SNFInKg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOSNFInKg" runat="server" Text='<%# Eval("SNFInKg").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                           <%-- <asp:GridView ID="gvVariation" runat="server" ShowFooter="true" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Variation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVariationName" runat="server" Text='<%# Eval("VariationName").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fat In Kg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVFatInKg" runat="server" Text='<%# Eval("FatInKg").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SNF In Kg">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVSNFInKg" runat="server" Text='<%# Eval("SNFInKg").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>--%>
                            <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table id="tblVariation" visible="false" runat="server" class="table table-bordered">
                                        <tr>
                                            <th>Variation</th>
                                           
                                            <th>FAT in Kg</th>
                                           
                                            <th>SNF in Kg</th>
                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            
                                            <td>
                                                <asp:Label ID="txtTFatInKg" runat="server" ></asp:Label></td>
                                            
                                            <td>
                                                <asp:Label ID="txtTSnfInKg" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Recovery %</td>
                                            
                                            <td>
                                                <asp:Label ID="txtReFatInKg" runat="server" ></asp:Label></td>
                                            
                                            <td>
                                                <asp:Label ID="txtReSNFInKg" runat="server" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Norms (Fix content to show)</td>
                                           
                                            <td>
                                                <asp:Label ID="txtNormFatInKg" runat="server" Text="99" ></asp:Label></td>
                                            
                                            <td>
                                                <asp:Label ID="txtNormSNFInKg" runat="server" Text="97.5" ></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

