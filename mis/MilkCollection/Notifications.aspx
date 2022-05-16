<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="mis_MilkCollection_Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .notification {
  background-color: #2e9eff;
  color: white;
  text-decoration: none;
  padding: 15px 26px;
  position: relative;
  display: inline-block;
  border-radius: 2px;
}

.notification:hover {
  background: white;
}

.notification .badge {
  position: absolute;
  top: -10px;
  right: -10px;
  padding: 5px 10px;
  border-radius: 50%;
  background: orange;
  color: white;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Notifications</h3>
                        </div>
                        <div class="box-body">
                           
                            <div class="col-md-4">
                                <a href="../dailyPlan/MilkTestingRequestToQCFeedBack.aspx" class="notification">
                                    <i class="fa fa-bell" aria-hidden="true
                                        "></i>&nbsp;<span>QC Test Request Notification</span>
                                    <span class="badge" id="spn1" runat="server"></span>
                                </a>
                            </div>
                            <div class="col-md-4">
                                <a href="ReceiveTankerChallan.aspx" class="notification">
                                    <i class="fa fa-bell" aria-hidden="true
                                        "></i>&nbsp;<span>MCMS Lab Testing Notification</span>
                                   <span class="badge" id="spn2" runat="server"></span>
                                </a>
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

