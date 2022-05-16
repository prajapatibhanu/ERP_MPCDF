﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRMyProfile.aspx.cs" Inherits="mis_HR_HRMyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employee Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-5">
                            <fieldset>
                                <legend>Personal Detail</legend>
                                <div class="table table-responsive">
                                    <asp:DetailsView ID="DVPersonalDetail" runat="server" AutoGenerateRows="false" CssClass="table table-bordered table-hover table-striped" >
                                        <Fields>
                                            <asp:TemplateField ItemStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <asp:Image ID="imgEmp_ProfileImage" ImageUrl='<% #Eval("Emp_ProfileImage")%>' runat="server" Width="128px" Height="132px" CssClass="pull-left" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMsg" runat="server" Text='<% #Eval("Emp_Name")%>' Font-Size="Large" ForeColor="DodgerBlue" Font-Bold="true"></asp:Label><br />
                                                    <asp:Label ID="Label1" runat="server" Text='<% #Eval("Designation_Name")%>' ForeColor="CadetBlue"></asp:Label><br />
                                                    <asp:Label ID="Label2" runat="server" Text='<% #Eval("Department_Name")%>' ForeColor="CornflowerBlue"></asp:Label><br />
                                                    <asp:Label ID="Label3" runat="server" Text='<% #Eval("Emp_MobileNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Emp_Dob" HeaderText="Date Of Birth" />
                                            <asp:BoundField DataField="Emp_Gender" HeaderText="Gender" />
                                            <asp:BoundField DataField="Emp_FatherHusbandName" HeaderText="Father/ Husband Name" />
                                            <asp:BoundField DataField="Emp_MaritalStatus" HeaderText="Marital Status" />
                                            <asp:BoundField DataField="Emp_BloodGroup" HeaderText="Blood Group" />
                                            <%--<asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No" />--%>
                                            <%--<asp:BoundField DataField="Emp_AadharNo" HeaderText="Aadhar No" />--%>
                                            <asp:BoundField DataField="Emp_PanCardNo" HeaderText="PanCard No" />
                                            <asp:BoundField DataField="Emp_Email" HeaderText="Email" />
                                            <asp:BoundField DataField="Emp_HusbWifeName" HeaderText="Husband/ Wife Name" />
                                            <asp:BoundField DataField="Emp_HusbWifeJob" HeaderText="Husband/ Wife Job" />
                                            <asp:BoundField DataField="Emp_HusbWifeDep" HeaderText="Husband/ Wife Dept" />
                                            <asp:BoundField DataField="Emp_Category" HeaderText="Category" />
                                            <asp:BoundField DataField="Emp_Religion" HeaderText="Religion" />
                                            <asp:BoundField DataField="Emp_Disability" HeaderText="Disability" />
                                            <asp:BoundField DataField="Emp_DisabilityType" HeaderText="Disability Type" />
<%--                                            <asp:BoundField DataField="Emp_CurState" HeaderText="Current State" />
                                            <asp:BoundField DataField="Emp_CurCity" HeaderText=" Current City" />
                                            <asp:BoundField DataField="Emp_CurPinCode" HeaderText="Current PinCode" />
                                            <asp:BoundField DataField="Emp_CurAddress" HeaderText="Current Address" />
                                            <asp:BoundField DataField="Emp_PerState" HeaderText="Permanent State" />
                                            <asp:BoundField DataField="Emp_PerCity" HeaderText="Permanent City" />
                                            <asp:BoundField DataField="Emp_PerPinCode" HeaderText="Permanent PinCode" />
                                            <asp:BoundField DataField="Emp_PerAddress" HeaderText="Permanent Address" />--%>
                                             <asp:BoundField DataField="Emp_CurrentAddress" HeaderText="Current Address" />
                                            
                                        </Fields>
                                    </asp:DetailsView>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-7">
                            <fieldset>
                                <legend>Official Detail</legend>
                                <div class="table table-responsive">
                                    <asp:DetailsView ID="DVOfficialDetail" runat="server" AutoGenerateRows="false" CssClass="table table-bordered table-hover table-striped">
                                        <Fields>
                                            <asp:BoundField DataField="OfficeType_Title" HeaderText="Present Posting" />
                                           <%-- <asp:BoundField DataField="Office_Name" HeaderText="Office" />--%>
                                            <asp:BoundField DataField="UserName" HeaderText="UserName" />
                                          <%--   <asp:BoundField DataField="Password" HeaderText="Password" />
                                            <asp:BoundField DataField="Level_Name" HeaderText="Level" />--%>
                                            <%--<asp:BoundField DataField="PayScale_Name" HeaderText="PayScale" />
                                            <asp:BoundField DataField="GradePay_Name" HeaderText="GradPay" />--%>
                                            <asp:BoundField DataField="Emp_BasicSalery" HeaderText="Basic Salary" />
                                            <asp:BoundField DataField="Emp_JoiningDate" HeaderText="Joining Date" />
                                            <asp:BoundField DataField="Emp_PostingDate" HeaderText="Posting Date On Present Post" />
                                            <asp:BoundField DataField="Emp_RetirementDate" HeaderText="Retirement Date" />
                                            <asp:BoundField DataField="Emp_TypeOfRecruitment" HeaderText="Type Of Initial Recruitment" />
                                            <asp:BoundField DataField="Emp_EpfNumber" HeaderText="EPF Number" />
                                            <asp:BoundField DataField="Emp_GINumber" HeaderText="Group Insurance Number" />
<%--                                        <asp:BoundField DataField="Emp_GpfType" HeaderText="GPF Type" />
                                            <asp:BoundField DataField="Emp_GpfNo" HeaderText="GPF No" />
                                            <asp:BoundField DataField="Emp_TypeOfPost" HeaderText="Type Of Post" />--%>
                                        </Fields>
                                    </asp:DetailsView>
                                </div>
                            </fieldset>
                              <div id="divNomineeDetail" runat="server">
                                <fieldset>
                                    <legend>Nominee Detail</legend>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVNomineeDetail" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nominee_Name" HeaderText="Nominee Name" />
                                                <asp:BoundField DataField="Nominee_Relation" HeaderText="Nominee Relation" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                        </div>
                        </div>
                    </div>
                    <div id="divBankDetail" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Bank Detail</legend>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVBankDetail" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                                 <asp:BoundField DataField="Bank_AccountType" HeaderText="Account Type" />
                                                <asp:BoundField DataField="Bank_BranchName" HeaderText="Branch Name" />
                                                <asp:BoundField DataField="Bank_AccountNo" HeaderText="Account No" />
                                                <asp:BoundField DataField="Bank_IfscCode" HeaderText="IfscCode" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                    <div id="divChildDetail" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Child Detail</legend>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVChildDetail" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Child_Name" HeaderText="Name" />
                                                <asp:BoundField DataField="Child_Type" HeaderText="Gender" />
                                                <asp:BoundField DataField="Child_Dob" HeaderText="DOB" />
                                                <asp:BoundField DataField="Child_Education" HeaderText="Education" />
                                                <%--<asp:BoundField DataField="Child_Business" HeaderText="Business" />--%>
                                                <asp:BoundField DataField="Child_MaritalStatus" HeaderText="Marital Status" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                    <div id="divFixedAssetsDetail" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Fixed Assets Detail</legend>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVFixedAssetsDetail" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Property_Type" HeaderText="Property Type" />
                                                <asp:BoundField DataField="Property_Location" HeaderText="Property Location" />
                                                <asp:BoundField DataField="Property_PurchaseYear" HeaderText="Purchase Year" />
                                                <asp:BoundField DataField="Property_PurchasePrice" HeaderText="Purchase Price" />
                                                <asp:BoundField DataField="Property_CurrentRate" HeaderText="Current Price" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div id="divOtherDetail" runat="server">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Other Detail</legend>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVOtherDetail" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Training." ItemStyle-Width="49%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther_Training" Text='<% #Eval("Other_Training")%>' runat="server"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Achievement" ItemStyle-Width="49%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOther_Achievement" Text='<% #Eval("Other_Achievement")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
