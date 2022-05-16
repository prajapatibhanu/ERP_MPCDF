<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="UpdateProducerMaster.aspx.cs" Inherits="mis_Masters_UpdateProducerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card-header">
                    <h3 class="card-title">Update Producer Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                दुग्ध संघ 

                            </label>
                            <%--<span class="pull-right">
                                <asp:RequiredFieldValidator ID="rq1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="ddlDS" InitialValue="0" ErrorMessage="Select Dugdh Sangh." Text="<i class='fa fa-exclamation-circle' title='Please Dugdh Sangh !'></i>"></asp:RequiredFieldValidator>
                            </span>--%>
                            <asp:DropDownList ID="ddlDS" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>दुग्ध सोसायटी का नाम <%--<span style="color: red;">*</span>--%></label>
                            <%--<span class="pull-right">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" Display="Dynamic" ValidationGroup="a" runat="server" ControlToValidate="DdlDCS" InitialValue="0" ErrorMessage="Select DCS." Text="<i class='fa fa-exclamation-circle' title='Please DCS !'></i>"></asp:RequiredFieldValidator>
                            </span>--%>
                            <asp:DropDownList ID="DdlDCS" runat="server" CssClass="form-control select2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                       
                    <div class="col-md-12">
                       
                            <asp:GridView ID="GridDetails" runat="server" CssClass="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ProducerId">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip=<%# Eval("ProducerId") %> runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProducerName" HeaderText="ProducerName" /> 
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile No" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" />  
                                    <asp:BoundField DataField="UserName" HeaderText="Producer Card No" />


                                     <asp:TemplateField HeaderText="Producer Code" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtProducerCode" onpaste="return false;"  AutoComplete="off" runat="server"  Text='<%# Eval("ProducerCardNo") %>' Width="100px" CssClass="form-control" OnTextChanged="txtProducerCode_TextChanged" AutoPostBack="true"></asp:TextBox> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                     
                                </Columns>
                                <EmptyDataTemplate>No District Found</EmptyDataTemplate>
                            </asp:GridView>
                        
                    </div>


                    
                </div>
        </section>
    </div>

     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

     
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
        
        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],

            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
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
