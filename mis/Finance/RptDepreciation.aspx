<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDepreciation.aspx.cs" Inherits="mis_Finance_RptDepreciation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box box-header">
                            <h3 class="box-title">Depreciation Report</h3>
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Year</label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem Selected="True">2016-17</asp:ListItem>
                                        <asp:ListItem>2017-18</asp:ListItem>
                                        <asp:ListItem>2018-19</asp:ListItem>
                                        <asp:ListItem>2019-20</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" runat="server" Style="margin-top: 21px;" Text="Search" CssClass="btn btn-primary btn-block" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <h3 style="text-align: center">अवक्षयण पत्रक वर्ष 2016-17</h3>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th style="text-align: center">खातो के नाम</th>
                                                <th style="text-align: center">दर</th>
                                                <th style="text-align: center">लगत</th>
                                                <th style="text-align: center">क्रय</th>
                                                <th style="text-align: center">विक्रय</th>
                                                <th style="text-align: center">वास्तविक लगत</th>
                                                <th style="text-align: center">अवक्षयण</th>
                                                <th style="text-align: center">अवक्षयण अवलिखित</th>
                                                <th colspan="4" style="text-align: center">अवक्षयण</th>
                                                <th style="text-align: center">शुद्ध मूल्य</th>
                                            </tr>
                                            <tr>
                                                <th style="text-align: center"></th>
                                                <th style="text-align: center"></th>
                                                <th style="text-align: center">01.04.2010</th>
                                                <th style="text-align: center"></th>
                                                <th style="text-align: center"></th>
                                                <th style="text-align: center">As On 31-03-17</th>
                                                <th style="text-align: center">31-03-17 तक</th>
                                                <th style="text-align: center">मूल्य</th>
                                                <th style="text-align: center">प्रारम्भिक अवलिखित मूल्य</th>
                                                <th style="text-align: center">वर्ष में किये क्रय</th>
                                                <th style="text-align: center">वर्ष 31.03.17</th>
                                                <th style="text-align: center">31-03-17 तक</th>
                                                <th style="text-align: center">वर्ष 31.03.17 पर</th>
                                            </tr>

                                            <tr>
                                                <td style="text-align: left">भवन</td>
                                                <td style="text-align: right">10%</td>
                                                <td style="text-align: right">23381496.10</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">23381496.10</td>
                                                <td style="text-align: right">19327867.96</td>
                                                <td style="text-align: right">4053628.14</td>
                                                <td style="text-align: right">405362.81</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">405362.81</td>
                                                <td style="text-align: right">19733230.77</td>
                                                <td style="text-align: right">3648265.33</td>

                                            </tr>
                                            <tr>
                                                <td style="text-align: left">सयंत्र एमं मशीनरी</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">59736383.00</td>
                                                <td style="text-align: right">4401858.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">64138241.00</td>
                                                <td style="text-align: right">15736607.27</td>
                                                <td style="text-align: right">48401633.73</td>
                                                <td style="text-align: right">6599966.36</td>
                                                <td style="text-align: right">330139.35</td>
                                                <td style="text-align: right">6930105.71</td>
                                                <td style="text-align: right">22666712.98</td>
                                                <td style="text-align: right">41471528.02</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">सयंत्र एमं मशीनरी</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">89014321.44</td>
                                                <td style="text-align: right">5326791.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">94341112.44</td>
                                                <td style="text-align: right">75480199.26</td>
                                                <td style="text-align: right">18860913.18</td>
                                                <td style="text-align: right">2030118.33</td>
                                                <td style="text-align: right">799018.65</td>
                                                <td style="text-align: right">2829136.98</td>
                                                <td style="text-align: right">78309336.24</td>
                                                <td style="text-align: right">16031776.20</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">मिल्क केन मेन</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">35564311.66</td>
                                                <td style="text-align: right">13175.00</td>
                                                <td style="text-align: right">252477.56</td>
                                                <td style="text-align: right">3532509.10</td>
                                                <td style="text-align: right">307499834.16</td>
                                                <td style="text-align: right">4575174.49</td>
                                                <td style="text-align: right">646428.29</td>
                                                <td style="text-align: right">1976.25</td>
                                                <td style="text-align: right">648404.54</td>
                                                <td style="text-align: right">31398239.15</td>
                                                <td style="text-align: right">3926769.95</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">बिजली उपकरण</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">1387412.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">1387412.00</td>
                                                <td style="text-align: right">709797.46</td>
                                                <td style="text-align: right">677614.54</td>
                                                <td style="text-align: right">101642.18</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">101642.18</td>
                                                <td style="text-align: right">811439.64</td>
                                                <td style="text-align: right">575972.36</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">बिजली उपकरण</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">2159167.20</td>
                                                <td style="text-align: right">184023.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">2343190.20</td>
                                                <td style="text-align: right">1846519.05</td>
                                                <td style="text-align: right">496671.15</td>
                                                <td style="text-align: right">46897.22</td>
                                                <td style="text-align: right">27603.45</td>
                                                <td style="text-align: right">74500.67</td>
                                                <td style="text-align: right">1921019.73</td>
                                                <td style="text-align: right">422170.47</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">भूमि</td>
                                                <td style="text-align: right">0%</td>
                                                <td style="text-align: right">176295.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">176295.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">176295.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">1762295.00</td>
                                            </tr>


                                            <tr>
                                                <td style="text-align: left">कार्यालय उपकरण</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">832245.00</td>
                                                <td style="text-align: right">84700.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">916945.00</td>
                                                <td style="text-align: right">300120.72</td>
                                                <td style="text-align: right">616824.28</td>
                                                <td style="text-align: right">79818.64</td>
                                                <td style="text-align: right">6352.50</td>
                                                <td style="text-align: right">86171.14</td>
                                                <td style="text-align: right">386291.86</td>
                                                <td style="text-align: right">530653.14</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">कार्यालय उपकरण</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">3684740.23</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">3684740.23</td>
                                                <td style="text-align: right">2635622.20</td>
                                                <td style="text-align: right">1049118.03</td>
                                                <td style="text-align: right">157367.70</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">157367.70</td>
                                                <td style="text-align: right">2792989.90</td>
                                                <td style="text-align: right">89175.33</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">मिल्क पार्लर/बूथ</td>
                                                <td style="text-align: right">5%</td>
                                                <td style="text-align: right">3664656.46</td>
                                                <td style="text-align: right">1230929.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">4895585.46</td>
                                                <td style="text-align: right">473783.00</td>
                                                <td style="text-align: right">4421802.46</td>
                                                <td style="text-align: right">319087.35</td>
                                                <td style="text-align: right">61546.45</td>
                                                <td style="text-align: right">380633.80</td>
                                                <td style="text-align: right">854416.79</td>
                                                <td style="text-align: right">4041168167</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">मिल्क पार्लर/बूथ</td>
                                                <td style="text-align: right">10%</td>
                                                <td style="text-align: right">12898180.13</td>
                                                <td style="text-align: right">252789.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">13150969.13</td>
                                                <td style="text-align: right">8643857.22</td>
                                                <td style="text-align: right">4507111.91</td>
                                                <td style="text-align: right">425432.29</td>
                                                <td style="text-align: right">252789.0</td>
                                                <td style="text-align: right">459711.19</td>
                                                <td style="text-align: right">9094568.41</td>
                                                <td style="text-align: right">4056400.72</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">पशु चिकित्सा उपकरण</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">79907.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">79907.00</td>
                                                <td style="text-align: right">26451.96</td>
                                                <td style="text-align: right">53555.04</td>
                                                <td style="text-align: right">8033.26</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">8033.26</td>
                                                <td style="text-align: right">3485.22</td>
                                                <td style="text-align: right">45521.78</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">पशु चिकित्सा उपकरण</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">2686148.51</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">2686148.51</td>
                                                <td style="text-align: right">2051412.26</td>
                                                <td style="text-align: right">634736.25</td>
                                                <td style="text-align: right">95210.44</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">95210.44</td>
                                                <td style="text-align: right">2146622.69</td>
                                                <td style="text-align: right">539525.81</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">वाहन</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">6861953.15</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">6861953.15</td>
                                                <td style="text-align: right">2234819.35</td>
                                                <td style="text-align: right">4627133.80</td>
                                                <td style="text-align: right">694070.07</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">694070.07</td>
                                                <td style="text-align: right">2928889.42</td>
                                                <td style="text-align: right">3933063.73</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">वाहन</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">6405489.36</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">6405489.36</td>
                                                <td style="text-align: right">5277933.79</td>
                                                <td style="text-align: right">1127555.57</td>
                                                <td style="text-align: right">169133.34</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">169133.34</td>
                                                <td style="text-align: right">5447067.12</td>
                                                <td style="text-align: right">958422.24</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">प्रचार प्रसार सामग्री</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">1368952.84</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">1368952.84</td>
                                                <td style="text-align: right">1239460.38</td>
                                                <td style="text-align: right">129492.46</td>
                                                <td style="text-align: right">94231.87</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">94231.87</td>
                                                <td style="text-align: right">1258884.25</td>
                                                <td style="text-align: right">110068.59</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">टोल काटा</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">1240758.00</td>
                                                <td style="text-align: right">3900.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">1244558.00</td>
                                                <td style="text-align: right">374167.75</td>
                                                <td style="text-align: right">868490.25</td>
                                                <td style="text-align: right">129688.54</td>
                                                <td style="text-align: right">292.00</td>
                                                <td style="text-align: right">129981.04</td>
                                                <td style="text-align: right">506148.79</td>
                                                <td style="text-align: right">738509.21</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">टोल काटा</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">428881.97</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">428881.97</td>
                                                <td style="text-align: right">291645.25</td>
                                                <td style="text-align: right">137236.72</td>
                                                <td style="text-align: right">20585.51</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">20585.51</td>
                                                <td style="text-align: right">312230.76</td>
                                                <td style="text-align: right">116651.21</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">कंप्यूटर सामग्री</td>
                                                <td style="text-align: right">30%</td>
                                                <td style="text-align: right">3373499.00</td>
                                                <td style="text-align: right">349469.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">3722968.00</td>
                                                <td style="text-align: right">2522948.84</td>
                                                <td style="text-align: right">1200019.16</td>
                                                <td style="text-align: right">510330.09</td>
                                                <td style="text-align: right">104840.70</td>
                                                <td style="text-align: right">615170.79</td>
                                                <td style="text-align: right">3138119.64</td>
                                                <td style="text-align: right">584848.36</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">कंप्यूटर सामग्री</td>
                                                <td style="text-align: right">16%</td>
                                                <td style="text-align: right">3501892.41</td>
                                                <td style="text-align: right">356965.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">3858857.41</td>
                                                <td style="text-align: right">3392620.00</td>
                                                <td style="text-align: right">467237.41</td>
                                                <td style="text-align: right">66163.44</td>
                                                <td style="text-align: right">214179.00</td>
                                                <td style="text-align: right">280342.44</td>
                                                <td style="text-align: right">3671962.45</td>
                                                <td style="text-align: right">186894.96</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">स्थाई सम्पत्ति जी.एम.एस.</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">247512.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">247512.00</td>
                                                <td style="text-align: right">2357243.17</td>
                                                <td style="text-align: right">116876183</td>
                                                <td style="text-align: right">17531.52</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">17531.52</td>
                                                <td style="text-align: right">2375774.70</td>
                                                <td style="text-align: right">99345.30</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">स्थाई सम्पत्ति</td>
                                                <td style="text-align: right">10%</td>
                                                <td style="text-align: right">1097977.57</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">1097977.57</td>
                                                <td style="text-align: right">790023.45</td>
                                                <td style="text-align: right">302954.12</td>
                                                <td style="text-align: right">30295.41</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">30295.41</td>
                                                <td style="text-align: right">825318.86</td>
                                                <td style="text-align: right">272658.71</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">सयंत्र एम मशीनरी शीतकेंद्र</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">1405480.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">1405480.00</td>
                                                <td style="text-align: right">590119.31</td>
                                                <td style="text-align: right">814360.69</td>
                                                <td style="text-align: right">122154.10</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">122154.10</td>
                                                <td style="text-align: right">713273.42</td>
                                                <td style="text-align: right">692206.58</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">सयंत्र एम मशीनरी शीतकेंद्र</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">9314376.64</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">9314376.64</td>
                                                <td style="text-align: right">8125049.83</td>
                                                <td style="text-align: right">1189326.81</td>
                                                <td style="text-align: right">178399.02</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">178399.02</td>
                                                <td style="text-align: right">8303448.85</td>
                                                <td style="text-align: right">1010927.79</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">स्थाई सम्पत्ति गुना</td>
                                                <td style="text-align: right">10%</td>
                                                <td style="text-align: right">309300.92</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">309300.92</td>
                                                <td style="text-align: right">230710.22</td>
                                                <td style="text-align: right">78590.70</td>
                                                <td style="text-align: right">7859.07</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">7859.07</td>
                                                <td style="text-align: right">238569.29</td>
                                                <td style="text-align: right">70731.63</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">एल.सी.डी.प्रोजेक्टर</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">130936.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">130936.00</td>
                                                <td style="text-align: right">85688.51</td>
                                                <td style="text-align: right">45247.49</td>
                                                <td style="text-align: right">6787.12</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">6787.12</td>
                                                <td style="text-align: right">92475.63</td>
                                                <td style="text-align: right">38460.37</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">प्रयोगशाला उपकरण</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">126038.00</td>
                                                <td style="text-align: right">560969.00</td>
                                                <td style="text-align: right">12199.00</td>
                                                <td style="text-align: right">675808.00</td>
                                                <td style="text-align: right">63110.23</td>
                                                <td style="text-align: right">609697.77</td>
                                                <td style="text-align: right">5329.47</td>
                                                <td style="text-align: right">84295.35</td>
                                                <td style="text-align: right">89624.82</td>
                                                <td style="text-align: right">155735.05</td>
                                                <td style="text-align: right">520072.95</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">प्रयोगशाला उपकरण</td>
                                                <td style="text-align: right">7.5%</td>
                                                <td style="text-align: right">577952.00</td>
                                                <td style="text-align: right">532136.00</td>
                                                <td style="text-align: right">23741.00</td>
                                                <td style="text-align: right">1086347.00</td>
                                                <td style="text-align: right">75344.30</td>
                                                <td style="text-align: right">1011002.70</td>
                                                <td style="text-align: right">68268.86</td>
                                                <td style="text-align: right">39910.20</td>
                                                <td style="text-align: right">108179.06</td>
                                                <td style="text-align: right">185523.35</td>
                                                <td style="text-align: right">902823.65</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">नलकूप</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">222096.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">222096.00</td>
                                                <td style="text-align: right">113465.73</td>
                                                <td style="text-align: right">108630.27</td>
                                                <td style="text-align: right">16294.54</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">16294.54</td>
                                                <td style="text-align: right">129760.27</td>
                                                <td style="text-align: right">92335.73</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">स्थाई संपत्ति(हटा,राजनगर,छतरपुर)</td>
                                                <td style="text-align: right">10%</td>
                                                <td style="text-align: right">4887058.90</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">4887058.90</td>
                                                <td style="text-align: right">1821582.79</td>
                                                <td style="text-align: right">3065476.11</td>
                                                <td style="text-align: right">306547.61</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">306547.61</td>
                                                <td style="text-align: right">2128130.40</td>
                                                <td style="text-align: right">2758928.50</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">नवीन पशु आहार संयंत्र सागर</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">199642000.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">199642000.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">199642000.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">199642000.00</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">नवीन संयंत्र एन.डी.डी.बी</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">158119350.00</td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right">158119350.00</td>
                                                <td style="text-align: right">2317902.50</td>
                                                <td style="text-align: right">134401447.50</td>
                                                <td style="text-align: right">20160217.13</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">20160217.13</td>
                                                <td style="text-align: right">43878119.63</td>
                                                <td style="text-align: right">114241230.38</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">पशु आहार संयंत्र पचामा</td>
                                                <td style="text-align: right">15%</td>
                                                <td style="text-align: right">24676100.54</td>
                                                <td style="text-align: right">341694.00</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">25017794.54</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">33057303.54</td>
                                                <td style="text-align: right">2896907.90</td>
                                                <td style="text-align: right">0.00</td>
                                                <td style="text-align: right">2896907.90</td>
                                                <td style="text-align: right">2896907.90</td>
                                                <td style="text-align: right">22120886.64</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left"></td>
                                                <td style="text-align: right"></td>
                                                <td style="text-align: right"><b>481514312.19</b></td>
                                                <td style="text-align: right"><b>214511637.00</b></td>
                                                <td style="text-align: right"><b>1218726.96</b></td>
                                                <td style="text-align: right"><b>694807222.23</b></td>
                                                <td style="text-align: right"><b>217960520.58</b></td>
                                                <td style="text-align: right"><b>484886210.65</b></td>
                                                <td style="text-align: right"><b>37792891.80</b></td>
                                                <td style="text-align: right"><b>1797344.60</b></td>
                                                <td style="text-align: right"><b>39590236.40</b></td>
                                                <td style="text-align: right"><b>257550756.98</b></td>
                                                <td style="text-align: right"><b>437256465.25</b></td>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

