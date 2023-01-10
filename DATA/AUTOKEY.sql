--hàm tự động tạo key SANPHAM------
CREATE function [dbo].[autoKey_SANPHAM]()
returns nvarchar(10)
as
begin
declare @key_next nvarchar(10)
declare @max int
--
	select @max=COUNT(MASP) + 1 from SANPHAM where MASP like 'ND'
	if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'SP' + RIGHT('0' + CAST(@max as varchar(10)), 10)
		end
	else
		begin
			set @key_next = 'SP' + RIGHT(CAST(@max as varchar(10)), 10)
		end
--nếu key đã tồn tại
	while(exists(select	MASP from SANPHAM where MASP = @key_next))
	begin
		set @max=@max + 1
		if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'SP' + RIGHT('0' + CAST(@max as varchar(10)), 10)
		end
	else
		begin
			set @key_next = 'SP' + RIGHT(CAST(@max as varchar(10)), 10)
		end
	end
	return @key_next
end
GO

ALTER TABLE [dbo].[SANPHAM] ADD  CONSTRAINT [auto_keySP]  DEFAULT ([dbo].[autoKey_SANPHAM]()) FOR [MASP]
GO

--hàm tự động tạo key NGUOIDUNG------
CREATE function [dbo].[autoKey_NGUOIDUNG]()
returns nvarchar(10)
as
begin
declare @key_next nvarchar(10)
declare @max int
--
	select @max=COUNT(MAND) + 1 from NGUOIDUNG where MAND like 'ND'
	if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'ND' + RIGHT('0' + CAST(@max as nvarchar(10)), 10)
		end
	else
		begin
			set @key_next = 'ND' + RIGHT(CAST(@max as nvarchar(10)), 10)
		end
--nếu key đã tồn tại
	while(exists(select	MAND from NGUOIDUNG where MAND = @key_next))
	begin
		set @max=@max + 1
		if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'ND' + RIGHT('0' + CAST(@max as varchar(10)), 10)
		end
	else
		begin
			set @key_next = 'ND' + RIGHT(CAST(@max as varchar(10)), 10)
		end
	end
	return @key_next
end
GO

ALTER TABLE [dbo].[NGUOIDUNG] ADD  CONSTRAINT [auto_keyND]  DEFAULT ([dbo].[autoKey_NGUOIDUNG]()) FOR [MAND]
GO

--hàm tự động tạo key DONHANG------
CREATE function [dbo].[autoKey_DONHANG]()
returns nvarchar(10)
as
begin
declare @key_next nvarchar(10)
declare @max int
--
	select @max=COUNT(MADH) + 1 from DONHANG where MADH like 'DH'
	if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'DH' + RIGHT('0' + CAST(@max as varchar(10)), 10)
		end
	else
		begin
			set @key_next = 'DH' + RIGHT(CAST(@max as varchar(10)), 10)
		end
--nếu key đã tồn tại
	while(exists(select	MADH from DONHANG where MADH = @key_next))
	begin
		set @max=@max + 1
		if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'DH' + RIGHT('0' + CAST(@max as varchar(10)), 10)
		end
	else
		begin
			set @key_next = 'DH' + RIGHT(CAST(@max as varchar(10)), 10)
		end
	end
	return @key_next
end
GO

ALTER TABLE [dbo].[DONHANG] ADD  CONSTRAINT [auto_keyDH]  DEFAULT ([dbo].[autoKey_DONHANG]()) FOR [MADH]
GO