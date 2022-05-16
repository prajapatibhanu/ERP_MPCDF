<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RMRDChallanEntryReportViaCanes.aspx.cs" Inherits="mis_MilkCollection_RMRDChallanEntryReportViaCanes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header noprint">
                            <h3 class="box-title">Truck Sheet Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row noprint">
                                <fieldset>
                                    <legend>Filter</legend>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>From Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="txtFromDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>To Date</label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtToDate" Text="<i class='fa fa-exclamation-circle' title='Enter Date!'></i>" ErrorMessage="Enter Date" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="far fa-calendar-alt"></i>
                                                    </span>
                                                </div>
                                                <asp:TextBox ID="txtToDate" autocomplete="off" CssClass="form-control DateAdd" data-date-end-date="0d" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Chilling Center</label>
                                            <%--<span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator144" ValidationGroup="Save"
                                                    InitialValue="0" ErrorMessage="Select Chilling Center" Text="<i class='fa fa-exclamation-circle' title='Select Chilling Center !'></i>"
                                                    ControlToValidate="ddlCC" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator></span>--%>
                                            <asp:DropDownList ID="ddlCC" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" runat="server" Style="margin-top: 22px;" Text="Search" CssClass="btn btn-success" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divdetail" runat="server" visible="false">
                                <fieldset>
                                    <legend>Report</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to dbf" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Button ID="btnPrint" Text="Print" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                            <asp:Button ID="btnExcel" Text="Excel" runat="server" CssClass="btn btn-primary" OnClick="btnExcel_Click" />
                                        </div>
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <%--<asp:GridView ID="gv_MilkCollectionChallanEntryDetails" ShowFooter="true" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="datatable table table-bordered" runat="server">
                                <Columns>
                                     <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>'  runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("EntryDate"))).ToString("dd/MM/yyyy") %>'></asp:Label>                                            
                                            </ItemTemplate>                                         
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Society">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                            </ItemTemplate>                      
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Milk Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblV_MilkType" runat="server" Text='<%# Eval("MilkType") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Milk Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblI_MilkSupplyQty" runat="server" Text='<%# Eval("MilkQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fat %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFat" runat="server" Text='<%# Eval("Fat") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CLR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLR" runat="server" Text='<%# Eval("CLR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF" runat="server" Text='<%# Eval("Snf") %>'></asp:Label>
                                            </ItemTemplate>                                          
                                        </asp:TemplateField>

                                       

                                        <asp:TemplateField HeaderText="FAT (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFAT_IN_KG" runat="server" Text='<%# Eval("FatInKg") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SNF (In KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNF_IN_KG" runat="server" Text='<%# Eval("SnfInKg") %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                  
                                        
                                        <asp:TemplateField HeaderText="Milk Quality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("MilkQuality") %>'></asp:Label>
                                            </ItemTemplate>
                                          
                                        </asp:TemplateField>

                                    </Columns>
                            </asp:GridView>--%>
                                                <div class="col-md-12">
                                                    <h3 style="text-align: center">ट्रक शीट - संघ द्वारा भरी जाने वाली जानकारी</h3>
                                                </div>
                                                <div class="col-md-12">
                                                    <h5 style="text-align: center"><span id="spndate" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spncc" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spnShift" runat="server"></span></h5>
                                                </div>
                                                <asp:GridView ID="gv_MilkCollectionChallanEntryDetails" ShowFooter="true" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="datatable table table-bordered" runat="server" OnRowCreated="gv_MilkCollectionChallanEntryDetails_RowCreated">

                                                   <Columns>
												<asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                             </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="दिनांक" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("EntryDate"))).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="शिफ्ट" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="शीत केंद्र का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCCName" runat="server" Text='<%# Eval("CCName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="दुग्ध संस्था का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="संस्था कोड नंबर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="अच्छा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBMilkQuantity" runat="server" Text='<%# Eval("GBMilkQuantity").ToString()=="0.00"?"":Eval("GBMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="खट्टा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("SBMilkQuantity").ToString()=="0.00"?"":Eval("SBMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फटा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("CBMilkQuantity").ToString()=="0.00"?"":Eval("CBMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBFat" runat="server" Text='<%# Eval("BFat").ToString()=="0.00"?"":Eval("BFat") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="सी.एल.आर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBClr" runat="server" Text='<%# Eval("BClr").ToString()=="0.00"?"":Eval("BClr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ प्रतिशित">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBSnf" runat="server" Text='<%# Eval("BSnf").ToString()=="0.000"?"":Eval("BSnf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="अच्छा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCMilkQuantity" runat="server" Text='<%# Eval("GCMilkQuantity").ToString()=="0.00"?"":Eval("GCMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="खट्टा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("SCMilkQuantity").ToString()=="0.00"?"":Eval("SCMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फटा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("CCMilkQuantity").ToString()=="0.00"?"":Eval("CCMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCFat" runat="server" Text='<%# Eval("CFat").ToString()=="0.00"?"":Eval("CFat") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="सी.एल.आर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCClr" runat="server" Text='<%# Eval("CClr").ToString()=="0.00"?"":Eval("CClr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ प्रतिशित">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCSnf" runat="server" Text='<%# Eval("CSnf").ToString()=="0.000"?"":Eval("CSnf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBFatInKg" runat="server" Text='<%# Eval("BFatInKg").ToString()=="0.000"?"":Eval("BFatInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBSnfInKg" runat="server" Text='<%# Eval("BSnfInKg").ToString()=="0.000"?"":Eval("BSnfInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCFatInKg" runat="server" Text='<%# Eval("CFatInKg").ToString()=="0.000"?"":Eval("CFatInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCSnfInKg" runat="server" Text='<%# Eval("CSnfInKg").ToString()=="0.000"?"":Eval("CSnfInKg") %>'></asp:Label>
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
                    </div>
                </div>
            </div>
        </section>
        <section class="content NonPrintable">
            <div id="divprint" runat="server">
                <div class="col-md-12">
                    <h3 style="text-align: center">ट्रक शीट - संघ द्वारा भरी जाने वाली जानकारी</h3>
                </div>
                <div class="col-md-12">
                    <h5 style="text-align: center"><span id="Span1" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span2" runat="server"></span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span3" runat="server"></span></h5>
                </div>
                <asp:GridView ID="gvPrint" ShowFooter="true" ShowHeader="true" EmptyDataText="No Record Found" EmptyDataRowStyle-ForeColor="Red" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowCreated="gvPrint_RowCreated">

                   <Columns>
												<asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                             </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="दिनांक" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# (Convert.ToDateTime(Eval("EntryDate"))).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="शिफ्ट" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDt_Date" runat="server" Text='<%# Eval("Shift") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="शीत केंद्र का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCCName" runat="server" Text='<%# Eval("CCName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="दुग्ध संस्था का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Eval("Office_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="संस्था कोड नंबर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Code" runat="server" Text='<%# Eval("Office_Code") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="अच्छा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBMilkQuantity" runat="server" Text='<%# Eval("GBMilkQuantity").ToString()=="0.00"?"":Eval("GBMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="खट्टा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("SBMilkQuantity").ToString()=="0.00"?"":Eval("SBMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फटा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("CBMilkQuantity").ToString()=="0.00"?"":Eval("CBMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBFat" runat="server" Text='<%# Eval("BFat").ToString()=="0.00"?"":Eval("BFat") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="सी.एल.आर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBClr" runat="server" Text='<%# Eval("BClr").ToString()=="0.00"?"":Eval("BClr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ प्रतिशित">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBSnf" runat="server" Text='<%# Eval("BSnf").ToString()=="0.000"?"":Eval("BSnf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="अच्छा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCMilkQuantity" runat="server" Text='<%# Eval("GCMilkQuantity").ToString()=="0.00"?"":Eval("GCMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="खट्टा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("SCMilkQuantity").ToString()=="0.00"?"":Eval("SCMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फटा दूध">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblV_Shift" runat="server" Text='<%# Eval("CCMilkQuantity").ToString()=="0.00"?"":Eval("CCMilkQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCFat" runat="server" Text='<%# Eval("CFat").ToString()=="0.00"?"":Eval("CFat") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="सी.एल.आर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCClr" runat="server" Text='<%# Eval("CClr").ToString()=="0.00"?"":Eval("CClr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ प्रतिशित">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCSnf" runat="server" Text='<%# Eval("CSnf").ToString()=="0.000"?"":Eval("CSnf") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBFatInKg" runat="server" Text='<%# Eval("BFatInKg").ToString()=="0.000"?"":Eval("BFatInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBSnfInKg" runat="server" Text='<%# Eval("BSnfInKg").ToString()=="0.000"?"":Eval("BSnfInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फैट कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCFatInKg" runat="server" Text='<%# Eval("CFatInKg").ToString()=="0.000"?"":Eval("CFatInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="एस.एन.एफ कि.ग्रां.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCSnfInKg" runat="server" Text='<%# Eval("CSnfInKg").ToString()=="0.000"?"":Eval("CSnfInKg") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                </asp:GridView>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../Finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Finance/js/jquery.dataTables.min.js"></script>
    <script src="../Finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../Finance/js/dataTables.buttons.min.js"></script>
    <script src="../Finance/js/buttons.flash.min.js"></script>
    <script src="../Finance/js/jszip.min.js"></script>
    <script src="../Finance/js/pdfmake.min.js"></script>
    <script src="../Finance/js/vfs_fonts.js"></script>
    <script src="../Finance/js/buttons.html5.min.js"></script>
    <script src="../Finance/js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <%-- <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script type="text/javascript">
        window.addEventListener('keydown', function (e) { if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) { if (e.target.nodeName == 'INPUT' && e.target.type == 'text') { e.preventDefault(); return false; } } }, true);
        $('.datatable').DataTable({
            paging: true,
            lengthMenu: [10, 25, 50, 100],
            iDisplayLength: 50,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
            }],
            "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('CC Canes Collection Report').bold().fontsize(3).toUpperCase(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    title: ('CC Canes Collection Report').bold().fontsize(3).toUpperCase(),
                    filename: 'CCCanesCollectionReport',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',

                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
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
</asp:Content>

