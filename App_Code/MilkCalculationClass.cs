using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MilkCalculationClass
/// </summary>
public class MilkCalculationClass
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    public MilkCalculationClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
     
    public decimal GetSNFPer(decimal FatInPer, decimal CLR)
    {
        decimal SNFInPer = 0;

        try
        {

            DataSet DS_SNFPER = apiprocedure.ByProcedure("MilkCalculation",
                                      new string[] { "flag", "FatInPer", "CLR" },
                                      new string[] { "1", FatInPer.ToString(), CLR.ToString() }, "dataset");

            if (DS_SNFPER != null)
            {
                if (DS_SNFPER.Tables.Count > 0)
                {
                    if (DS_SNFPER.Tables[0].Rows.Count > 0)
                    {
                        SNFInPer = Convert.ToDecimal(DS_SNFPER.Tables[0].Rows[0]["SNFInPer"]);
                    }
                    else
                    {
                        return SNFInPer;
                    }
                }
                else
                {
                    return SNFInPer;
                }

            }

            return SNFInPer;

        }
        catch (Exception)
        {

            return SNFInPer;
        }

    }

    public decimal GetSNFPer_DCS(decimal FatInPer, decimal CLR)
    {
        decimal SNFInPer = 0;

        try
        {

            DataSet DS_SNFPER = apiprocedure.ByProcedure("MilkCalculation",
                                      new string[] { "flag", "FatInPer", "CLR" },
                                      new string[] { "5", FatInPer.ToString(), CLR.ToString() }, "dataset");

            if (DS_SNFPER != null)
            {
                if (DS_SNFPER.Tables.Count > 0)
                {
                    if (DS_SNFPER.Tables[0].Rows.Count > 0)
                    {
                        SNFInPer = Convert.ToDecimal(DS_SNFPER.Tables[0].Rows[0]["SNFInPer"]);
                    }
                    else
                    {
                        return SNFInPer;
                    }
                }
                else
                {
                    return SNFInPer;
                }

            }

            return SNFInPer;

        }
        catch (Exception)
        {

            return SNFInPer;
        }

    }

    public decimal GetLtrToKg(decimal CLR, decimal MilkQtyInLtr)
    {
        decimal LtrToKg = 0;

        try
        {

            DataSet DS_LtrToKg = apiprocedure.ByProcedure("MilkCalculation",
                                      new string[] { "flag", "CLR", "MilkQtyInLtr" },
                                      new string[] { "2", CLR.ToString(), MilkQtyInLtr.ToString() }, "dataset");

            if (DS_LtrToKg != null)
            {
                if (DS_LtrToKg.Tables.Count > 0)
                {
                    if (DS_LtrToKg.Tables[0].Rows.Count > 0)
                    {
                        LtrToKg = Convert.ToDecimal(DS_LtrToKg.Tables[0].Rows[0]["LtrToKg"]);
                    }
                    else
                    {
                        return LtrToKg;
                    }
                }
                else
                {
                    return LtrToKg;
                }

            }

            return LtrToKg;

        }
        catch (Exception)
        {

            return LtrToKg;
        }

    }

    public decimal GetFATInKg(decimal MilkQtyInKg, decimal FatInPer)
    {
        decimal FATInKg = 0;

        try
        {

            DataSet DS_FATInKg = apiprocedure.ByProcedure("MilkCalculation",
                                      new string[] { "flag", "MilkQtyInKg", "FatInPer" },
                                      new string[] { "3", MilkQtyInKg.ToString(), FatInPer.ToString() }, "dataset");

            if (DS_FATInKg != null)
            {
                if (DS_FATInKg.Tables.Count > 0)
                {
                    if (DS_FATInKg.Tables[0].Rows.Count > 0)
                    {
                        FATInKg = Convert.ToDecimal(DS_FATInKg.Tables[0].Rows[0]["FATInKg"]);
                    }
                    else
                    {
                        return FATInKg;
                    }
                }
                else
                {
                    return FATInKg;
                }

            }

            return FATInKg;

        }
        catch (Exception)
        {

            return FATInKg;
        }

    }

    public decimal GetSNFInKg(decimal MilkQtyInKg, decimal SNFPer)
    {
        decimal SNFInKg = 0;

        try
        {

            DataSet DS_SNFInKg = apiprocedure.ByProcedure("MilkCalculation",
                                      new string[] { "flag", "MilkQtyInKg", "SNFPer" },
                                      new string[] { "4", MilkQtyInKg.ToString(), SNFPer.ToString() }, "dataset");

            if (DS_SNFInKg != null)
            {
                if (DS_SNFInKg.Tables.Count > 0)
                {
                    if (DS_SNFInKg.Tables[0].Rows.Count > 0)
                    {
                        SNFInKg = Convert.ToDecimal(DS_SNFInKg.Tables[0].Rows[0]["SNFInKg"]);
                    }
                    else
                    {
                        return SNFInKg;
                    }
                }
                else
                {
                    return SNFInKg;
                }

            }

            return SNFInKg;

        }
        catch (Exception)
        {

            return SNFInKg;
        }

    }
	
	
	public string GetRatePerLtrOrKg(decimal FAT, decimal CLR,string OfficeId)
    {
        string MilkRatePerLtrOrKG = "";

        try
        {

            DataSet DS_RatePerLtrOrKG = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                                       new string[] { "flag", "Fat", "CLR", "OfficeId" },
                                       new string[] { "7", FAT.ToString(), CLR.ToString(), OfficeId }, "dataset");

            if (MilkRatePerLtrOrKG != null)
            {
                if (DS_RatePerLtrOrKG.Tables.Count > 0)
                {
                    if (DS_RatePerLtrOrKG.Tables[0].Rows.Count > 0)
                    {
                        MilkRatePerLtrOrKG = DS_RatePerLtrOrKG.Tables[0].Rows[0]["Rate_Per_Ltr"].ToString();
                    }
                    else
                    {
                        return MilkRatePerLtrOrKG;
                    }
                }
                else
                {
                    return MilkRatePerLtrOrKG;
                }

            }

            return MilkRatePerLtrOrKG;

        }
        catch (Exception)
        {

            return MilkRatePerLtrOrKG;
        }

    }
	
	public decimal GetCLR_DCS(decimal FatInPer, decimal SNF_Per)
    {
        decimal CLRInPer = 0;

        try
        {

            DataSet DS_CLRPER = apiprocedure.ByProcedure("MilkCalculation",
                                      new string[] { "flag", "FatInPer", "SNFPer" },
                                      new string[] { "10", FatInPer.ToString(), SNF_Per.ToString() }, "dataset");

            if (DS_CLRPER != null)
            {
                if (DS_CLRPER.Tables.Count > 0)
                {
                    if (DS_CLRPER.Tables[0].Rows.Count > 0)
                    {
                        CLRInPer = Convert.ToDecimal(DS_CLRPER.Tables[0].Rows[0]["CLR"]);
                    }
                    else
                    {
                        return CLRInPer;
                    }
                }
                else
                {
                    return CLRInPer;
                }

            }

            return CLRInPer;

        }
        catch (Exception)
        {

            return CLRInPer;
        }

    }
     
}