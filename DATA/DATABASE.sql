USE [QLSHOPPING]
GO

/****** Object:  UserDefinedFunction [dbo].[autoKey_SANPHAM]    Script Date: 03/01/2023 2:34:31 SA ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--hàm tự động tạo key SANPHAM------
ALTER function [dbo].[autoKey_SANPHAM]()
returns char(5)
as
begin
declare @key_next char(10)
declare @max int
--
	select @max=COUNT(MASP) + 1 from SANPHAM where MASP like 'ND'
	if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'SP' + RIGHT('0' + CAST(@max as varchar(5)), 5)
		end
	else
		begin
			set @key_next = 'SP' + RIGHT(CAST(@max as varchar(5)), 5)
		end
--nếu key đã tồn tại
	while(exists(select	MASP from SANPHAM where MASP = @key_next))
	begin
		set @max=@max + 1
		if(@max < 10) -- nếu số đếm  < 10
		begin
			set @key_next = 'SP' + RIGHT('0' + CAST(@max as varchar(5)), 5)
		end
	else
		begin
			set @key_next = 'SP' + RIGHT(CAST(@max as varchar(5)), 5)
		end
	end
	return @key_next
end
GO


CREATE TABLE [dbo].[CHITIETDONHANG](
	[MADH] [char](5) NOT NULL,
	[MASP] [char](5) NOT NULL,
	[SL] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MASP] ASC,
	[MADH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CHITIETGIOHANG](
	[MASP] [char](5) NOT NULL,
	[MAND] [char](5) NOT NULL,
	[SOLUONG] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MASP] ASC,
	[MAND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DANHGIA](
	[MASP] [char](5) NOT NULL,
	[MAND] [char](5) NOT NULL,
	[DANHGIA] [int] NOT NULL,
	[BINHLUAN] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[MASP] ASC,
	[MAND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DONHANG](
	[MADH] [char](5) NOT NULL,
	[MAND] [char](5) NOT NULL,
	[NGAYLAP] [smalldatetime] NOT NULL,
	[GIATRI] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MADH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[NGUOIDUNG](
	[MAND] [char](5) NOT NULL,
	[QUYEN] [char](10) NOT NULL,
	[TENND] [nvarchar](20) NOT NULL,
	[TENDAYDU] [nvarchar](100) NULL,
	[PASS] [nvarchar](100) NOT NULL,
	[SDT] [nvarchar](20) NULL,
	[EMAIL] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MAND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PHANQUYEN](
	[MAPQ] [char](3) NOT NULL,
	[TENPQ] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MAPQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SANPHAM](
	[MASP] [char](5) NOT NULL,
	[TENSP] [nvarchar](100) NOT NULL,
	[SOLUONG] [int] DEFAULT ((0)),
	[DANHGIA] [int] DEFAULT ((5)),
	[DONGIA] [int] NOT NULL,
	[NAMSX] [int] NOT NULL,
	[HANGSX] [NVARchar](100) NOT NULL,
	[GIOITINH] [NVARCHAR](10) NOT NULL,
	[MOTA] [nvarchar](1000) NULL,
	[HINH] [nvarchar](100) not NULL
PRIMARY KEY CLUSTERED 
(
	[MASP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

alter table SANPHAM alter column GIOITINH char(4) not null

-- CTDH
ALTER TABLE [dbo].[CHITIETDONHANG]  WITH CHECK ADD  CONSTRAINT [fk01_CTDH] FOREIGN KEY([MASP])
REFERENCES [dbo].[SANPHAM] ([MASP])
GO

ALTER TABLE [dbo].[CHITIETDONHANG]  WITH CHECK ADD  CONSTRAINT [fk02_CTDH] FOREIGN KEY([MADH])
REFERENCES [dbo].[DONHANG] ([MADH])
GO

--gh
ALTER TABLE [dbo].[CHITIETGIOHANG]  WITH CHECK ADD  CONSTRAINT [fk01_GH] FOREIGN KEY([MASP])
REFERENCES [dbo].[SANPHAM] ([MASP])
GO

ALTER TABLE [dbo].[CHITIETGIOHANG]  WITH CHECK ADD  CONSTRAINT [fk02_GH] FOREIGN KEY([MAND])
REFERENCES [dbo].[NGUOIDUNG] ([MAND])
GO

--DG
ALTER TABLE [dbo].[DANHGIA]  WITH CHECK ADD  CONSTRAINT [fk01_DG] FOREIGN KEY([MASP])
REFERENCES [dbo].[SANPHAM] ([MASP])
GO

ALTER TABLE [dbo].[DANHGIA]  WITH CHECK ADD  CONSTRAINT [fk02_DG] FOREIGN KEY([MAND])
REFERENCES [dbo].[NGUOIDUNG] ([MAND])
GO

--DH
ALTER TABLE [dbo].[DONHANG]  WITH CHECK ADD  CONSTRAINT [fk01_DH] FOREIGN KEY([MAND])
REFERENCES [dbo].[NGUOIDUNG] ([MAND])
GO

--ND
ALTER TABLE [dbo].[NGUOIDUNG]  WITH CHECK ADD  CONSTRAINT [fk01_ND] FOREIGN KEY([MAPQ])
REFERENCES [dbo].[PHANQUYEN] ([MAPQ])
GO

---insert
INSERT INTO SANPHAM(TENSP, SOLUONG, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH)
VALUES ( N'Đồng hồ ADRIATICA 25 mm', 13, 3, 122000, 2012, N'Adriatica',N'Nữ', N'Adriatica thể hiện một khái niệm về chất lượng, độ chính xác, độ tin cậy cao kết hợp với sự thanh lịch và độc đáo trong thiết kế. Một trong những mục tiêu chính của họ là tạo ra các thiết kế và mô hình mới hoàn toàn, phù hợp với sự thay đổi của thị hiếu khách hàng, đồng thời giữ giá cả cạnh tranh. Adriatica hứa hẹn sẽ đem đến cho khách hàng những sản phẩm hoàn hảo, chuẩn mực cùng với thiết kế độc đáo, sang trọng.', 'https://cdn.tgdd.vn/Products/Images/7264/218476/adriatica-a3436-9113q-nu-2-org.jpg');

INSERT INTO SANPHAM(TENSP, SOLUONG, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH)
VALUES (N'Đồng hồ ADRIATICA 25 mm', 13, 3, 122000, 2012, N'Adriatica', N'Nữ', N'Adriatica thể hiện một khái niệm về chất lượng, độ chính xác, độ tin cậy cao kết hợp với sự thanh lịch và độc đáo trong thiết kế. Một trong những mục tiêu chính của họ là tạo ra các thiết kế và mô hình mới hoàn toàn, phù hợp với sự thay đổi của thị hiếu khách hàng, đồng thời giữ giá cả cạnh tranh. Adriatica hứa hẹn sẽ đem đến cho khách hàng những sản phẩm hoàn hảo, chuẩn mực cùng với thiết kế độc đáo, sang trọng.', 'https://cdn.tgdd.vn/Products/Images/7264/218476/adriatica-a3436-9113q-nu-2-org.jpg');

INSERT INTO NGUOIDUNG(TENDN, SDT, EMAIL, PASS, QUYEN, TENND)
VALUES ('HH','4124','GMAIL', 'HH', 'ADMIN', 'PHAN THANH NHANB')

----------------
CREATE proc [dbo].[Proc_GetDecentralization]
as
select * from PHANQUYEN
GO
--
CREATE proc [dbo].[Proc_GetAllProduct]
as
select * from SANPHAM
GO
--dangnhap
  alter proc [dbo].[Proc_DangNhap](@tendangnhap nvarchar(100),@matkhau nvarchar(100))
 as
 select * from NGUOIDUNG where TENDN=@tendangnhap and PASS=@matkhau
GO


-- themtk
create proc [dbo].[Proc_ThemNguoiDung](@Tennguoidung nvarchar(500),@tendangnhap nvarchar(100),@matkhau nvarchar(100),@Email nvarchar(100),@CurrentID int output)
as
if(exists(select * from NGUOIDUNG where tendangnhap=@tendangnhap or email=@Email))
 begin
 set @CurrentID=-1
 return
 end
 insert into nguoidung(TenNguoiDung,TenDangNhap,MatKhau,Email) values(@Tennguoidung,@tendangnhap,@matkhau,@Email)
 set @CurrentID=@@IDENTITY
GO

---sywaa
alter table SANPHAM alter column GIOITINH [NVARchar](10) NOT NULL
alter table DONHANG alter column GIATRI int not null
alter table NGUOIDUNG alter column TENDN CHAR(100) not null
alter table DONHANG alter column NGAYLAP smalldatetime DEFAULT getdate();

alter table NGUOIDUNG ADD TENND nvarchar(100) null

DELETE FROM SANPHAM
DELETE FROM NGUOIDUNG
DELETE FROM DONHANG
delete FROM CHITIETDONHANG
delete FROM CHITIETGIOHANG

SELECT * FROM SANPHAM
SELECT * FROM NGUOIDUNG
SELECT * FROM CHITIETGIOHANG
SELECT * FROM Donhang
SELECT * FROM CHITIETDONHANG

select * from NGUOIDUNG where TENDN='NHAN' and PASS='NHAN'
exists(select * from SANPHAM where MASP='SP08')
delete SANPHAM 
where MASP='SP08'

INSERT INTO SANPHAM(TENSP, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH)
VALUES ( N'Đồng hồ ADRIATICA 25 mm', 3, 122000, 2012, N'Adriatica',N'Nữ', N'Adriatica thể hiện một khái niệm về chất lượng, độ chính xác, độ tin cậy cao kết hợp với sự thanh lịch và độc đáo trong thiết kế. Một trong những mục tiêu chính của họ là tạo ra các thiết kế và mô hình mới hoàn toàn, phù hợp với sự thay đổi của thị hiếu khách hàng, đồng thời giữ giá cả cạnh tranh. Adriatica hứa hẹn sẽ đem đến cho khách hàng những sản phẩm hoàn hảo, chuẩn mực cùng với thiết kế độc đáo, sang trọng.', 'https://cdn.tgdd.vn/Products/Images/7264/218476/adriatica-a3436-9113q-nu-2-org.jpg');

delete NGUOIDUNG
where MAND='ND03'

 select SANPHAM.MASP, TENSP, CHITIETDONHANG.SL, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH
 from SANPHAM, CHITIETDONHANG
 where SANPHAM.MASP = CHITIETDONHANG.MASP
 and MADH= 'DH11'