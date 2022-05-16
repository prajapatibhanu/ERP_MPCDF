<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptFinBalanceSheet20_21.aspx.cs" Inherits="mis_Finance_RptFinBalanceSheet20_21" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .tdtext {
            font-weight: 800 !important;
            /*color: #e82319 !important;*/
        }

        .align-right {
            text-align: right !important;
        }

        .align-rightB {
            text-align: right !important;
            font-weight: 700 !important;
            /*color: #e82319 !important;*/
        }



        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header hide_print">
                    <h3 class="box-title">Balance Sheet (2020-21)</h3>

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>

                <div class="box-body">

                    <h3 style="text-align: center">भोपाल सहकारी दुग्ध संघ मर्यादित, भोपाल<br />
                        स्थिति विवरण पत्रक वर्ष 2020-2021</h3>
                    <table class="table table-bordered">
                        <tr>
                            <th>पूर्व वर्ष राशि</th>
                            <th>देयताएं अंशपूंजी</th>
                            <th>राशि रुपये</th>
                            <th>राशि रुपये</th>
                            <th>पूर्व वर्ष राशि</th>
                            <th>सम्पत्तियां </th>
                            <th>राशि रुपये</th>
                            <th>राशि रुपये</th>
                        </tr>
                        <tr>
                            <td class="align-rightB">100000000.00</td>
                            <td class="tdtext">अधिकृत अंशपूंजी</td>
                            <td class="align-right">100000000.00</td>
                            <td class="align-rightB">100000000.00</td>
                            <td></td>
                            <td class="tdtext">स्थाई सम्पत्तियां (परिशिष्ठ 08)</td>
                            <td></td>
                            <td class="align-rightB">239926967.93</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="tdtext">सदस्यताएँ एवं प्रदत्त </td>
                            <td></td>
                            <td></td>
                            <td class="align-rightB">751375797.22</td>
                            <td class="tdtext">सकल मूल्य </td>
                            <td class="align-right">611657381.38</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">94683248.00</td>
                            <td>अंशपूंजी (परिशिष्ठ 1)</td>
                            <td class="align-right">107676648.00</td>
                            <td class="align-rightB">107676648.00</td>
                            <td class="align-rightB">342577651.23</td>
                            <td class="tdtext">(-) घसारा</td>
                            <td class="align-right">371730413.45</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="align-right">408798145.99</td>
                            <td class="tdtext">शुद्ध मूल्य</td>
                            <td class="align-right">239926967.93</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">368753569.02</td>
                            <td>रिजर्व एवं सरप्लस (परिशिष्ठ 2)</td>
                            <td class="align-right">436095897.75</td>
                            <td class="align-rightB">436095897.75</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="tdtext">लाभ हानि खाता</td>
                            <td></td>
                            <td></td>
                            <td class="align-right">6131990.00</td>
                            <td>विनियोग (परिशिष्ठ 09)</td>
                            <td class="align-right">5759400.00</td>
                            <td class="align-rightB">5759400.00</td>
                        </tr>
                        <tr>
                            <td class="align-right">696321797.16</td>
                            <td>प्रारम्भिक शेष</td>
                            <td class="align-right">698109686.05</td>
                            <td class="align-rightB">763339951.45</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">38989716.01</td>
                            <td>शुद्ध लाभ ( लाभ हानि खाते से हस्तांतरण )</td>
                            <td class="align-rightB">239480818.00</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">37201827.12</td>
                            <td>लाभ हानि नियोजन खाते में वितरण</td>
                            <td class="align-right">174250552.60</td>
                            <td></td>
                            <td></td>
                            <td>चालू सम्पत्तियां ऋण  एवं अग्रिम चालू सम्पत्तियां </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">423009593.64</td>
                            <td>ग्रान्ट एवं सब्सीडी (परिशिष्ठ 03)</td>
                            <td class="align-right">66549628.24</td>
                            <td class="align-rightB">66549628.24</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="align-right">865797502.54</td>
                            <td>नगद, बैंक शेष एवं एफडीआर (परिशिष्ठ 10)</td>
                            <td class="align-right">1147047375.05</td>
                            <td class="align-rightB">2044734628.02</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="align-right">349504451.66</td>
                            <td>अंतिम स्कंध (परिशिष्ठ 11)</td>
                            <td class="align-right">414202846.83</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">36099363.06</td>
                            <td>अल्पकालीन ऋण (परिशिष्ठ 4)</td>
                            <td class="align-right">1359072.06</td>
                            <td class="align-rightB">1359072.06</td>
                            <td class="align-right">258484538.15</td>
                            <td>विविध देनदार (परिशिष्ठ 12)</td>
                            <td class="align-right">169657276.87</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="align-right">330834332.15</td>
                            <td>अन्य चालू  सम्पत्तियां (परिशिष्ठ 13)</td>
                            <td class="align-right">200099940.66</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td class="align-right">103817150.00</td>
                            <td>जमा (परिशिष्ठ 14)</td>
                            <td class="align-right">113727188.61</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="tdtext">चालू देयताएं एवं प्रावधान</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">143055736.52</td>
                            <td>प्रावधान</td>
                            <td class="align-right">290794589.37</td>
                            <td class="align-rightB">949748995.76</td>
                            <td></td>
                            <td class="tdtext">ऋण एवं अग्रिम</td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">1159742.02</td>
                            <td>देय कर </td>
                            <td class="align-right">14204967.99</td>
                            <td></td>
                            <td class="align-right">2661860.04</td>
                            <td>अग्रिम (दुग्ध समिति)  (परिशिष्ठ 15)</td>
                            <td class="align-right">2049870.46</td>
                            <td class="align-rightB">34349197.31</td>
                        </tr>
                        <tr>
                            <td class="align-right">271142078.23</td>
                            <td>अन्य देयताएं (परिशिष्ठ 5)</td>
                            <td class="align-right">319860263.10</td>
                            <td></td>
                            <td class="align-right">1649407.97</td>
                            <td>अग्रिम (स्टाफ)  (परिशिष्ठ 16)</td>
                            <td class="align-right">1445724.29</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">306687669.93</td>
                            <td>विविध लेनदार (परिशिष्ठ 6)</td>
                            <td class="align-right">215855929.72</td>
                            <td></td>
                            <td class="align-right">129343256.09</td>
                            <td>अन्य अग्रिम (परिशिष्ठ 17)</td>
                            <td class="align-right">30853602.56</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">116641432.16</td>
                            <td>सुरक्षा राशि जमा (परिशिष्ठ 7)</td>
                            <td class="align-right">137443181.56</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-rightB">2457022634.59</td>
                            <td class="tdtext">योग</td>
                            <td class="align-rightB">2324770193.26</td>
                            <td class="align-rightB">2324770193.26</td>
                            <td class="align-rightB">2457022634.59</td>
                            <td class="tdtext">योग</td>
                            <td class="align-rightB">2324770193.26</td>
                            <td class="align-rightB">2324770193.26</td>
                        </tr>

                    </table>
                    <table class="table" style="margin-top: 50px; margin-bottom: 50px;">
                        <tr>
                            <td style="text-align: center; font-weight: 800;">लेखापाल </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी ऑडिट</td>
                            <td style="text-align: center; font-weight: 800;">प्रबंधक (वित्त)</td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी वित्त</td>
                            <td style="text-align: center; font-weight: 800;">मुख्य कार्यपालन अधिकारी</td>
                            <td style="text-align: center; font-weight: 800;">अध्यक्ष </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी अंकेक्षक</td>
                        </tr>
                    </table>
                    <h3 style="text-align: center">भोपाल सहकारी दुग्ध संघ मर्यादित, भोपाल<br />
                        समाशोधन खाता वर्ष 2020-2021</h3>
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <td>2019-20</td>
                                <td>विवरण</td>
                                <td>राशि </td>
                                <td>2019-20</td>
                                <td>विवरण</td>
                                <td>राशि</td>
                            </tr>

                            <tr>
                                <td></td>
                                <td class="tdtext">निधि  को  हस्तांतरण </td>
                                <td></td>
                                <td class="align-right">696321797.16</td>
                                <td>प्रारम्भिक शेष 
                                </td>
                                <td class="align-right">698109686.05</td>
                            </tr>
                            <tr>
                                <td class="align-right">9747429.00</td>
                                <td>रिजर्व फण्ड</td>
                                <td class="align-right">59870204.52</td>
                                <td class="align-right">38989716.01</td>
                                <td>चालू  वर्ष में लाभ</td>
                                <td class="align-right">239480818.00</td>
                            </tr>
                            <tr>
                                <td class="align-right">9468324.80 </td>
                                <td>लाभांश  देय</td>
                                <td class="align-right">10767664.39 </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">100000.00
                                </td>
                                <td>सहकारी संघ चन्दा / अभिदाय/अंशदान
                                </td>
                                <td class="align-right">11471772.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">779794.32
                                </td>
                                <td>प्रशिक्षण निधि </td>
                                <td class="align-right">4789616.21
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">779794.32
                                </td>
                                <td>लाभांश समीकरण</td>
                                <td class="align-right">4789616.75
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">1949485.80
                                </td>
                                <td>प्रशिक्षण एवं शिक्षण (सहकारिता)
                                </td>
                                <td class="align-right">11974040.87
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">6358850.00
                                </td>
                                <td>आधार भूत संरचना</td>
                                <td class="align-right">5507050.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">0.00
                                </td>
                                <td>भवन निधि  </td>
                                <td class="align-right">5000000.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">1169691.48
                                </td>
                                <td>डेयरी उद्योग विकास अनुसंधान </td>
                                <td class="align-right">7184424.12
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">3898971.60
                                </td>
                                <td>सामाजिक कल्याण कोष </td>
                                <td class="align-right">23948081.74
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">1000000.00
                                </td>
                                <td>वाहन निधि  </td>
                                <td class="align-right">5000000.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">1949485.80
                                </td>
                                <td>पेट्रोनेज   रिफंन्ड(दुग्ध उत्पादक) </td>
                                <td class="align-right">23948082.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-rightB">37201827.12
                                </td>
                                <td class="tdtext">योग </td>
                                <td class="align-rightB">174250552.60
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">698109686.05
                                </td>
                                <td>संचित  लाभ
                                </td>
                                <td class="align-right">763339951.45
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>(स्थिति  विवरण में  हस्तांतरित)</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="align-rightB">735311513.17
                                </td>
                                <td class="tdtext">कुल  योग 
                                </td>
                                <td class="align-rightB">937590504.05
                                </td>
                                <td class="align-rightB">735311513.17
                                </td>
                                <td class="tdtext">कुल योग </td>
                                <td class="align-rightB">937590504.05
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="table" style="margin-top: 50px; margin-bottom: 50px;">
                        <tr>
                            <td style="text-align: center; font-weight: 800;">लेखापाल </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी ऑडिट</td>
                            <td style="text-align: center; font-weight: 800;">प्रबंधक (वित्त)</td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी वित्त</td>
                            <td style="text-align: center; font-weight: 800;">मुख्य कार्यपालन अधिकारी</td>
                            <td style="text-align: center; font-weight: 800;">अध्यक्ष </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी अंकेक्षक</td>
                        </tr>
                    </table>
                    <h3 style="text-align: center">भोपाल सहकारी दुग्ध संघ मर्यादित, भोपाल<br />
                        लाभ हानि खाता वर्ष 2020-2021</h3>
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <td>2019-20
                                </td>
                                <td>विवरण
                                </td>
                                <td>राशि</td>
                                <td>2019-20 </td>
                                <td>विवरण </td>
                                <td>राशि  </td>
                            </tr>

                            <tr>
                                <td class="align-right">69056583.41
                                </td>
                                <td>प्रशासनिक व्यय(परिशिष्ट 24)
                                </td>
                                <td class="align-right">56074823.54
                                </td>
                                <td class="align-right">646303289.42
                                </td>
                                <td>सकल  लाभ , व्यापार  खाता से  हस्तांतरित 
                                </td>
                                <td class="align-right">990754230.99
                                </td>
                            </tr>
                            <tr>
                                <td class="align-right">95718400.00
                                </td>
                                <td>व्यापारिक  व अन्य कर (परिशिष्ट 25)
                                </td>
                                <td class="align-right">88917529.28
                                </td>
                                <td class="align-right">112059125.78
                                </td>
                                <td>अप्रत्यक्ष  आय (परिशिष्ट 23)
                                </td>
                                <td class="align-right">85236187.17
                                </td>
                            </tr>
                            <tr>
                                <td class="align-right">24641464.34
                                </td>
                                <td>घसारा(परिशिष्ट 08  ब ) </td>
                                <td class="align-right">29152763.20
                                </td>
                                <td class="align-right">15697332.00
                                </td>
                                <td>अप्रत्यक्ष  आय पचामा संयंत्र से    </td>
                                <td class="align-right">35539580.06 </td>
                            </tr>
                            <tr>
                                <td class="align-right">25534102.18
                                </td>
                                <td>क्षेत्र संचालन व्यय (परिशिष्ट 26)
                                </td>
                                <td class="align-right">26119247.50
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>0.00
                                </td>
                                <td>अप्रत्यक्षव्यय  पचामा  </td>
                                <td class="align-right">33962500.96
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">75901805.21
                                </td>
                                <td>विवरण  व्यय (परिशिष्ट 27) </td>
                                <td class="align-right">71066835.22
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">286431676.05
                                </td>
                                <td>वेतन एवं भत्ते(परिशिष्ट 28)  </td>
                                <td class="align-right">330755480.52  </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">0.00
                                </td>
                                <td>डूबत ऋण  </td>
                                <td class="align-right">40000000.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">0.00
                                </td>
                                <td>वित्तीय व्यय  </td>
                                <td class="align-right">0.00  </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">196775716.01
                                </td>
                                <td>आयकर  पूर्व शुद्ध  लाभ   </td>
                                <td class="align-right">435480818.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-rightB">774059747.20
                                </td>
                                <td class="tdtext">योग </td>
                                <td class="align-rightB">1111529998.22
                                </td>
                                <td class="align-rightB">774059747.20
                                </td>
                                <td class="tdtext">योग </td>
                                <td class="align-rightB">1111529998.22
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-right">157786000.00
                                </td>
                                <td>आयकर  </td>
                                <td class="align-right">196000000.00
                                </td>
                                <td class="align-right">196775716.01
                                </td>
                                <td>आयकर पूर्व शुद्ध  लाभ  </td>
                                <td class="align-right">435480818.00 </td>
                            </tr>
                            <tr>
                                <td class="align-right">38989716.01
                                </td>
                                <td>लाभ   हानि खाते से शुद्ध लाभ का   </td>
                                <td class="align-right">239480818.00
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>हस्तांतरण   समाशोधन   खाते में  </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="align-rightB">970835463.21
                                </td>
                                <td class="tdtext">कुल  योग </td>
                                <td class="align-rightB">1547010816.22
                                </td>
                                <td class="align-rightB">970835463.21
                                </td>
                                <td class="tdtext">कुल योग 
                                </td>
                                <td class="align-rightB">1547010816.22
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="table" style="margin-top: 50px; margin-bottom: 50px;">
                        <tr>
                            <td style="text-align: center; font-weight: 800;">लेखापाल </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी ऑडिट</td>
                            <td style="text-align: center; font-weight: 800;">प्रबंधक (वित्त)</td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी वित्त</td>
                            <td style="text-align: center; font-weight: 800;">मुख्य कार्यपालन अधिकारी</td>
                            <td style="text-align: center; font-weight: 800;">अध्यक्ष </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी अंकेक्षक</td>
                        </tr>
                    </table>
                    <h3 style="text-align: center">भोपाल सहकारी दुग्ध संघ मर्यादित, भोपाल<br />
                        व्यापारिक खाता वर्ष 2020-2021</h3>
                    <table class="table table-bordered">
                        <tr>
                            <th>2019-20</th>
                            <th>विवरण</th>
                            <th>राशि</th>
                            <th>2019-20</th>
                            <th>विवरण</th>
                            <th>राशि</th>

                        </tr>
                        <tr>
                            <td class="align-right">346146698.13</td>
                            <td>प्रारम्भिक स्कंध</td>
                            <td class="align-right">349504451.66</td>
                            <td class="align-right">7438702564.06</td>
                            <td>विक्रय (परिशिष्ट 18)</td>
                            <td class="align-right">6693506291.68</td>
                        </tr>
                        <tr>
                            <td class="align-right">6067236621.82</td>
                            <td>क्रय(परिशिष्ट 19)</td>
                            <td class="align-right">5016792307.21</td>
                            <td class="align-right">349504451.66</td>
                            <td>अंतिम स्कंध (परिशिष्ट 11)</td>
                            <td class="align-right">414202846.83</td>
                        </tr>
                        <tr>
                            <td class="align-right">660729705.80</td>
                            <td>प्रत्यक्ष व्यय  (परिशिष्ट 20)</td>
                            <td class="align-right">676777117.02</td>
                            <td class="align-right">316663344.51</td>
                            <td>स्टाक स्थानांतरित (पचामा संयंत्र से)</td>
                            <td class="align-right">274633024.99</td>
                        </tr>
                        <tr>
                            <td class="align-right">32070344.00</td>
                            <td>प्रत्यक्ष व्यय  पचामा(परिशिष्ट 21)</td>
                            <td class="align-right">35016266.00</td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">35720356.55</td>
                            <td>दुग्ध शीतकेन्द्र  व्यय (परिशिष्ट 22)</td>
                            <td class="align-right">38864765.63</td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-right">316663344.51</td>
                            <td>स्टाक स्थानांतरित ( पचामा संयंत्र से )</td>
                            <td class="align-right">274633024.99</td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-rightB">646303289.42</td>
                            <td>सकल लाभ का हस्तांतरण लाभ हानि खाते में</td>
                            <td class="align-rightB">990754230.99</td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="align-rightB">8104870360.23</td>
                            <td class="tdtext">कुल योग</td>
                            <td class="align-rightB">7382342163.50</td>
                            <td class="align-rightB">8104870360.23</td>
                            <td class="tdtext">कुल योग</td>
                            <td class="align-rightB">7382342163.50</td>
                        </tr>
                    </table>
                    <table class="table" style="margin-top: 50px; margin-bottom: 50px;">
                        <tr>
                            <td style="text-align: center; font-weight: 800;">लेखापाल </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी ऑडिट</td>
                            <td style="text-align: center; font-weight: 800;">प्रबंधक (वित्त)</td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी वित्त</td>
                            <td style="text-align: center; font-weight: 800;">मुख्य कार्यपालन अधिकारी</td>
                            <td style="text-align: center; font-weight: 800;">अध्यक्ष </td>
                            <td style="text-align: center; font-weight: 800;">प्रभारी अंकेक्षक</td>
                        </tr>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>





