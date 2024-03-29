USE [DBMPAGRO]
GO
/****** Object:  StoredProcedure [dbo].[SpFinBillByBillTx]    Script Date: 7/3/2019 3:09:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  Procedure [dbo].[SpFinBillByBillTx]
@flag int = NULL,
@BillByBillTx_ID bigint = NULL, 
@VoucherTx_ID bigint = NULL, 
@Ledger_ID bigint = NULL, 
@BillByBillTx_RefType varchar(20) = NULL,
@BillByBillTx_Ref varchar(300) = NULL, 
@BillByBillTx_Amount decimal(18, 2) = NULL, 
@BillByBillTx_Date date = NULL, 
@Office_ID int = NULL, 
@BillByBillTx_FY varchar(20) = NULL, 
@BillByBillTx_IsActive int = NULL,
@BillByBillTx_OrderBy int = NULL,
@LedgerTx_OrderBy int = NULL
As 
BEGIN
	IF(@flag = 1)
	BEGIN
		SELECT * FROM TblFinBillByBillTx;
	END

	IF(@flag = 2)
	BEGIN
		SELECT 
		    --(case when VoucherTx_Date is null then  convert(varchar(50),BillByBillTx_Date,103) else convert(varchar(50),VoucherTx_Date,103) end) as VoucherTx_Date,
			(select top 1  convert(varchar(50),Bdate.BillByBillTx_Date,103)
         from TblFinBillByBillTx Bdate
  	     where Bdate.BillByBillTx_Ref=TblFinBillByBillTx.BillByBillTx_Ref order by Bdate.BillByBillTx_ID) as VoucherTx_Date ,
			BillByBillTx_Ref,
			SUM(BillByBillTx_Amount) as BillByBillTx_Amount, 
			(BillByBillTx_Ref 
			+ ' [ ' + (case 
				when isnull(sum(BillByBillTx_Amount),0) = 0 then  convert(varchar,Isnull(sum(BillByBillTx_Amount),0))
				when isnull(sum(BillByBillTx_Amount),0) > 0 then convert(varchar,Isnull(sum(BillByBillTx_Amount),0)) +' Cr'
				when  isnull(sum(BillByBillTx_Amount),0) < 0 then convert(varchar,ABS(IsNull(sum(BillByBillTx_Amount),0)))+' Dr'
			end) + ' ] ') as AgnstRef
			, (case 
				when isnull(sum(BillByBillTx_Amount),0) = 0 then  convert(varchar,Isnull(sum(BillByBillTx_Amount),0))
				when isnull(sum(BillByBillTx_Amount),0) > 0 then convert(varchar,Isnull(sum(BillByBillTx_Amount),0)) +' Cr'
				when  isnull(sum(BillByBillTx_Amount),0) < 0 then convert(varchar,ABS(IsNull(sum(BillByBillTx_Amount),0)))+' Dr'
			end) AS Amount
			FROM TblFinBillByBillTx
			left join  TblFinVoucherTx on TblFinBillByBillTx.VoucherTx_ID = TblFinVoucherTx.VoucherTx_ID
		WHERE TblFinBillByBillTx.Ledger_ID = @Ledger_ID and BillByBillTx_IsActive = 1 and TblFinBillByBillTx.Office_ID = @Office_ID
		group by BillByBillTx_Ref--,VoucherTx_Date,BillByBillTx_Date
		,TblFinBillByBillTx.Ledger_ID
		having SUM(BillByBillTx_Amount) != 0  
	END

	IF(@flag = 3)
	BEGIN
		INSERT INTO TblFinBillByBillTx (VoucherTx_ID, Ledger_ID, BillByBillTx_RefType, BillByBillTx_Ref, BillByBillTx_Amount,BillByBillTx_Date, Office_ID, BillByBillTx_FY, BillByBillTx_IsActive,BillByBillTx_OrderBy,LedgerTx_OrderBy)
		values(@VoucherTx_ID, @Ledger_ID, @BillByBillTx_RefType, @BillByBillTx_Ref, @BillByBillTx_Amount,@BillByBillTx_Date, @Office_ID, @BillByBillTx_FY, @BillByBillTx_IsActive,@BillByBillTx_OrderBy,@LedgerTx_OrderBy);
	END
	if(@flag =4)
		BEGIN
			DELETE  FROM TblFinBillByBillTx WHERE VoucherTx_ID = @VoucherTx_ID
		END
    IF(@flag = 5)
	BEGIN
		SELECT 
	         BillByBillTx_ID,
			 convert(varchar(50),BillByBillTx_Date,103) as BillByBillTx_Date,
			BillByBillTx_Ref,
			SUM(BillByBillTx_Amount) as BillByBillTx_Amount, 
			(BillByBillTx_Ref 
			+ ' [ ' + (case 
				when isnull(sum(BillByBillTx_Amount),0) = 0 then  convert(varchar,Isnull(sum(BillByBillTx_Amount),0))
				when isnull(sum(BillByBillTx_Amount),0) > 0 then convert(varchar,Isnull(sum(BillByBillTx_Amount),0)) +' Cr'
				when  isnull(sum(BillByBillTx_Amount),0) < 0 then convert(varchar,ABS(IsNull(sum(BillByBillTx_Amount),0)))+' Dr'
			end) + ' ] ') as AgnstRef
			, (case 
				when isnull(sum(BillByBillTx_Amount),0) = 0 then  convert(varchar,Isnull(sum(BillByBillTx_Amount),0))
				when isnull(sum(BillByBillTx_Amount),0) > 0 then convert(varchar,Isnull(sum(BillByBillTx_Amount),0)) +' Cr'
				when  isnull(sum(BillByBillTx_Amount),0) < 0 then convert(varchar,ABS(IsNull(sum(BillByBillTx_Amount),0)))+' Dr'
			end) AS Amount
			FROM TblFinBillByBillTx
			--left join  TblFinVoucherTx on TblFinBillByBillTx.VoucherTx_ID = TblFinVoucherTx.VoucherTx_ID
		WHERE TblFinBillByBillTx.Ledger_ID = @Ledger_ID and BillByBillTx_IsActive = 1 and TblFinBillByBillTx.Office_ID = @Office_ID and VoucherTx_ID is null
		group by BillByBillTx_Ref,BillByBillTx_Date,BillByBillTx_ID
		having SUM(BillByBillTx_Amount) != 0;
	END
	if(@flag = 6)
	BEGIN
		SELECT Count(BillByBillTx_ID) as Status
		from TblFinBillByBillTx where BillByBillTx_Ref = @BillByBillTx_Ref
		and Office_ID = @Office_ID and BillByBillTx_IsActive = 1
		
	END
	IF (@flag = 7)
  BEGIN
    SELECT 
	         BillByBillTx_ID,
			convert(varchar(50),BillByBillTx_Date,103) as BillByBillTx_Date,
			BillByBillTx_Ref,
			ABS(SUM(BillByBillTx_Amount)) as BillByBillTx_Amount, 
			SUM(BillByBillTx_Amount) as Amount, 
			(case 
				when isnull(sum(BillByBillTx_Amount),0) = 0 then  ''
				when isnull(sum(BillByBillTx_Amount),0) > 0 then  'Cr'
				when  isnull(sum(BillByBillTx_Amount),0) < 0 then 'Dr'
			end)as Type
			
			FROM TblFinBillByBillTx
			--left join  TblFinVoucherTx on TblFinBillByBillTx.VoucherTx_ID = TblFinVoucherTx.VoucherTx_ID
		WHERE TblFinBillByBillTx.Ledger_ID = @Ledger_ID and BillByBillTx_IsActive = 1 and TblFinBillByBillTx.Office_ID = @Office_ID and VoucherTx_ID is null
		group by BillByBillTx_Ref,BillByBillTx_Date,BillByBillTx_ID
		having SUM(BillByBillTx_Amount) != 0
		order by CONVERT(Date, BillByBillTx_Date) asc
		
    --AND LedgerTx_FY = @LedgerTx_FY;
  END
  if(@flag = 8)
  BEGIN
	if exists(Select * from TblFinBillByBillTx where VoucherTx_ID is not null and BillByBillTx_IsActive = 1 and BillByBillTx_Ref = @BillByBillTx_Ref and Ledger_ID=@Ledger_ID and Office_ID=@Office_ID)
	BEGIN
		Select 'false' as status
	END
	else
	BEGIN
		if exists(Select * from TblFinBillByBillTx
		          join TblFinVoucherTx on TblFinBillByBillTx.VoucherTx_ID = TblFinVoucherTx.VoucherTx_ID where TblFinVoucherTx.VoucherTx_IsActive = 1 and BillByBillTx_Ref = @BillByBillTx_Ref and TblFinBillByBillTx.Ledger_ID=@Ledger_ID and TblFinBillByBillTx.Office_ID=@Office_ID)
		BEGIN
			Select 'false' as status
		END
		ELSE
		BEGIN
			Select 'true' as status
		END
	END
  END
END

