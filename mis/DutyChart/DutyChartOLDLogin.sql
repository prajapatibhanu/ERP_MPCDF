USE [DB_MPCDF_ERP]
GO
/****** Object:  StoredProcedure [dbo].[Sp_tblDutyChartMapping]    Script Date: 11/27/2021 10:42:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Sp_tblDutyChartMapping]
@flag int = null
, @FromDate datetime = null
, @ToDate datetime = null
, @Id int = null
, @dtCount int = 0
, @RouteCount int = 0
, @DriverCount int = 0
, @TesterCount int = 0
, @i int = 1
, @RouteID int = null
, @TankerID int = null
, @DriverID int = null
, @TesterID int = null
, @OfficeId int = null
, @CleanerID int = null
, @CreatedBy int = null
, @CreatedIP varchar(50) = null
, @Year int = null
, @Month int = null
AS
BEGIN
	if(@flag = 1)
	BEGIN
		declare @msg varchar(20), @Errormsg varchar(50)
		Declare @date table(d datetime)
		Declare @d datetime
		Declare @d1 datetime
		set @d=@FromDate
		set @d1=@ToDate
		set @RouteCount = (select count(RouteID) from tblDutyChart_TankerRouteMapping where IsActive = 1 and OfficeID = @OfficeId)
		set @DriverCount = (select count(Driver_ID) from tblDriverMaster where IsActive = 1)
		set @TesterCount = (select count(Tester_ID) from tblTesterMaster where IsActive = 1)

		--select ROW_NUMBER() over(order by TankerID) as RowNo, * into #tempTanker from tblDutyChart_TankerRouteMapping where OfficeID = @OfficeId order by TankerID
		--select ROW_NUMBER() over(order by TankerID) as RowNo, TankerID, MilkCollectionFrom ,RouteID, DriverID
		--into #tempTanker 
		--from tblDutyChart_TankerRouteMapping  TRM 
		--inner join Mst_TankerDetail TD on TRM.TankerID = TD.I_TankerID and TD.Isactive = 1
		--where OfficeID = @OfficeId 
		--order by TankerID
		select ROW_NUMBER() over(order by TankerID) as RowNo, TankerID, BMCTankerRootName ,RouteID, DriverID, UPPER(SUBSTRING(BMCTankerRootName, 1, 2)) as MilkCollectionFrom
		into #tempTanker 
		from tblDutyChart_TankerRouteMapping  TRM 
		inner join Mst_BMCTankerRootDetails TRD on TRM.RouteID = TRD.BMCTankerRoot_Id and TRM.Isactive = 1
		--inner join Mst_TankerDetail TD on TRM.TankerID = TD.I_TankerID and TD.Isactive = 1
		where OfficeID = @OfficeId 
		order by TankerID
		--select ROW_NUMBER() over(order by RouteID) as RowNo, * into #tempRoot from tblDutyChart_TankerRouteMapping where OfficeID = @OfficeId order by RouteID
		--select Driver_ID into #TempDriver from tblDriverMaster where IsActive = 1 and Office_ID = @OfficeId
		select Tester_ID into #TempTester from tblTesterMaster where IsActive = 1 and Office_ID = @OfficeId
		select Cleaner_ID into #TempCleaner from tblCleanerMaster where IsActive = 1 and Office_ID = @OfficeId

		if not exists (select * from tblDutyChartDetail where Year = @Year and Month = @Month)
		BEGIN
			while(@i <= @RouteCount)
			BEGIN
				set @TankerID = (select TankerID from #tempTanker where RowNo = @i)	
				set @RouteID = (select RouteID from #tempTanker where RowNo = @i)
				set @DriverID = (select DriverID from #tempTanker where RowNo = @i)
				set @TesterID = (select top 1 Tester_ID from #TempTester order by NEWID() desc)
				set @CleanerID = (select top 1 Cleaner_ID from #TempCleaner order by NEWID())

				if not exists(select * from tblDutyChartDetail where Year = @Year and Month = @Month and RouteID = @RouteID and TankerID = @TankerID)
				BEGIN
					if not exists(select * from tblDutyChartDetail where Year = @Year and Month = @Month and DriverID = @DriverID)
					BEGIN
						if not exists(select * from tblDutyChartDetail where Year = @Year and Month = @Month and TesterID = @TesterID)
						BEGIN
							if not exists(select * from tblDutyChartDetail where Year = @Year and Month = @Month and CleanerID = @CleanerID)
							BEGIN
								if exists(select * from #tempTanker where TankerID = @TankerID and MilkCollectionFrom = 'CC')
								BEGIN
									insert into tblDutyChartDetail (OfficeId,Year,Month,RouteID,TankerID,DriverID,TesterID,CleanerID,CreatedBy,CreatedOn,CreatedIP) 
									values(@OfficeId,@Year, @Month,@RouteID,@TankerID,@DriverID,'0', @CleanerID, @CreatedBy, DATEADD(minute, 330, getdate()), @CreatedIP)
								END
								else --if exists(select * from #tempTanker where TankerID = @TankerID and MilkCollectionFrom = 'BMC')
								BEGIN
									insert into tblDutyChartDetail (OfficeId,Year,Month,RouteID,TankerID,DriverID,TesterID,CleanerID,CreatedBy,CreatedOn,CreatedIP) 
									values(@OfficeId,@Year, @Month,@RouteID,@TankerID,@DriverID,@TesterID, @CleanerID, @CreatedBy, DATEADD(minute, 330, getdate()), @CreatedIP)
								END
								set @i = @i + 1
							END
							else
							BEGIN
								if(@i > 1)
								BEGIN
									set @i = @i - 1
								END
							END	
						END
						else
						BEGIN
							if(@i > 1)
							BEGIN
								set @i = @i - 1
							END
						END
					END
					else
					BEGIN
						if(@i > 1)
						BEGIN
							set @i = @i - 1
						END
					END	
				END	
				else
				BEGIN
					set @i = @i + 1
				END
			END	

			set @msg = 'Ok'
			set @Errormsg = 'Record Generated Successfully.'

			drop table #tempTanker
			--drop table #tempRoot
			--drop table #TempDriver 
			drop table #TempTester
			drop table #TempCleaner
		END
		ELSE
		BEGIN
			set @msg = 'NotOk'
			set @Errormsg = 'Record Already exists for this year and Month.'
		END
		select @msg as msg, @Errormsg as Errormsg
	END
	else if(@flag = 2)
	BEGIN
		--select BMCTankerRootName,V_VehicleNo,Driver_Name,
		--Tester_Name, convert(varchar(20), CD.CreatedOn, 103) as CreatedOn 
		--,Cleaner_Name
		--,D_VehicleCapacity
		--from tblDutyChartDetail CD 
		--left join Mst_TankerDetail TM on CD.TankerID = TM.I_TankerID
		--left join tblDriverMaster DM on CD.DriverID = DM.Driver_ID
		--left join tblTesterMaster TMM on CD.TesterID = TMM.Tester_ID
		--left join Mst_BMCTankerRootDetails TRM on CD.RouteID = TRM.BMCTankerRoot_Id
		--left join tblCleanerMaster CM on CD.CleanerID = CM.Cleaner_ID
		--where OfficeId = @OfficeId and CD.Year = @Year and CD.Month = @Month

		select BMCTankerRootName,
				V_VehicleNo,Driver_Name,
		(case when CD.TesterID = '0' then 'NA' else Tester_Name end) as Tester_Name, 
		convert(varchar(20), CD.CreatedOn, 103) as CreatedOn 
		,Cleaner_Name
		,D_VehicleCapacity
		from tblDutyChartDetail CD 
		left join tblDutyChart_TankerRouteMapping TRM on CD.RouteID = TRM.RouteID
		left join Mst_TankerDetail TM on TRM.TankerID = TM.I_TankerID
		left join tblDriverMaster DM on CD.DriverID = DM.Driver_ID
		left join tblTesterMaster TMM on CD.TesterID = TMM.Tester_ID
		left join Mst_BMCTankerRootDetails BTRM on CD.RouteID = BTRM.BMCTankerRoot_Id
		left join tblCleanerMaster CM on CD.CleanerID = CM.Cleaner_ID
		where CD.OfficeId = @OfficeId and CD.Year = @Year and CD.Month = @Month
		order by BTRM.BMCTankerRootName 
		--and Year = @Year and Month = @Month
	END
	--else if(@flag = 2)
	--BEGIN
	--	select BMCTankerRootName,V_VehicleNo,Driver_Name,
	--	Tester_Name  from tblDutyChartDetail CD 
	--	left join Mst_TankerDetail TM on CD.TankerID = TM.I_TankerID
	--	left join tblDriverMaster DM on CD.DriverID = DM.Driver_ID
	--	left join tblTesterMaster TMM on CD.TesterID = TMM.Tester_ID
	--	left join Mst_BMCTankerRootDetails TRM on CD.RouteID = TRM.BMCTankerRoot_Id
	--	where OfficeId = @OfficeId and CD.Year = @Year and CD.Month = @Month
	--	--and Year = @Year and Month = @Month
	--END
END
