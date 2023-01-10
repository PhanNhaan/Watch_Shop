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
alter proc [dbo].[ThemNguoiDung](@tendangnhap nvarchar(100),@matkhau nvarchar(100),@Email nvarchar(100), @sdt nvarchar(100), @Quyen nvarchar(100) ,@CurrentID nvarchar(10) output)
as
if(exists(select * from nguoidung where tendn=@tendangnhap))
 begin
 set @CurrentID= ''
 return
 end
set @CurrentID = [dbo].[autoKey_NGUOIDUNG]()
 insert into nguoidung(TENDN, PASS, EMAIL, SDT, QUYEN) values(@tendangnhap, @matkhau, @Email, @sdt, @Quyen)
 
GO

--themsp
alter PROC [dbo].[ThemSanPham](@tensanpham nvarchar(100), @Gia int, @mota nvarchar(1000), @hinh nvarchar(100), @namsanxuat int, @hangsx nvarchar(100), @gioitinh nvarchar(10), @CurrentID nvarchar(10) output)
as
begin try

if(exists(select * from SANPHAM where TENSP=@tensanpham))
 begin
  set @CurrentID=''
  return
 end
set @CurrentID=[dbo].[autoKey_SANPHAM]()
INSERT INTO SANPHAM(TENSP, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH)
values(@tensanpham, @Gia, @namsanxuat, @hangsx, @gioitinh, @mota, @hinh)

end try
begin catch
 set @CurrentID=''
 end catch
GO

--suwa sp
create PROC [dbo].[CapNhatSanPham](@masp nvarchar(10), @tensanpham nvarchar(100), @Gia int, @mota nvarchar(1000), @hinh nvarchar(100), @namsanxuat int, @hangsx nvarchar(100), @gioitinh nvarchar(10),@CurrentID nvarchar(10) output)
as
begin try

if(exists(select * from Sanpham where TENSP=@tensanpham and MASP<>@masp))
 begin
  set @CurrentID=''
  return
 end
update Sanpham 
set TENSP=@tensanpham, DONGIA=@Gia, NAMSX=@namsanxuat, HANGSX=@hangsx, GIOITINH=@gioitinh, MOTA= @mota, HINH=@hinh
where MASP=@masp
set @CurrentID=@masp
end try
begin catch
 set @CurrentID=''
 end catch
GO

--xoa sp
alter PROC [dbo].[XoaSanPham](@masp nvarchar(10) ,@CurrentID nvarchar(10) output)
as
begin try

delete SANPHAM 
where MASP=@masp
set @CurrentID=@masp

end try
begin catch
 set @CurrentID=''
 end catch
GO

--themcartsp
alter PROC [dbo].[ThemVaoGioHang](@masp nvarchar(10), @mand nvarchar(10), @sl int, @CurrentID nvarchar(10) output)
as
begin try

if(exists(select * from CHITIETGIOHANG where MASP=@masp and MAND=@mand))
 begin
  set @CurrentID=''
  return
 end

INSERT INTO CHITIETGIOHANG(MASP, MAND, SOLUONG)
values(@masp, @mand, @sl)
set @CurrentID = @mand
end try
begin catch
 set @CurrentID=''
 end catch
GO

--get giohang
alter proc [dbo].[LayGioHang](@mand nvarchar(10))
as
 select SANPHAM.MASP, TENSP, CHITIETGIOHANG.SOLUONG, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH
 from SANPHAM, CHITIETGIOHANG
 where SANPHAM.MASP = CHITIETGIOHANG.MASP
 and MAND= @mand
GO

--Sua giohang
alter PROC [dbo].[CapNhatGioHang](@masp nvarchar(10), @mand nvarchar(10), @sl int, @CurrentID nvarchar(10) output)
as
begin try

update CHITIETGIOHANG 
set SOLUONG=@sl
where MASP=@masp and MAND=@mand
set @CurrentID=@masp

end try
begin catch
 set @CurrentID=''
 end catch
GO

--xoa gh
create PROC [dbo].[XoaGioHang](@masp nvarchar(10), @mand nvarchar(10), @CurrentID nvarchar(10) output)
as
begin try

delete CHITIETGIOHANG
where MASP=@masp and MAND=@mand

set @CurrentID=@masp

end try
begin catch
 set @CurrentID=''
 end catch
GO

--all nd
CREATE proc [dbo].[TatCaNguoiDung]
as
select * from NGUOIDUNG
GO

--xoa nd
create PROC [dbo].[XoaNguoiDung](@mand nvarchar(10) ,@CurrentID nvarchar(10) output)
as
begin try

delete NGUOIDUNG
where MAND = @mand
set @CurrentID=@mand

end try
begin catch
 set @CurrentID=''
 end catch
GO

--them dh
alter PROC [dbo].[ThemDonHang](@mand nvarchar(10), @giatri int, @CurrentID nvarchar(10) output)
as
begin try

set @CurrentID = [dbo].[autoKey_DONHANG]()
INSERT INTO DONHANG(MAND, NGAYLAP, GIATRI)
values(@mand, getdate(), @giatri)

end try
begin catch
 set @CurrentID=''
 end catch
GO

--them ctdh
Create PROC [dbo].[ThemCTDH](@masp nvarchar(10), @madh nvarchar(10), @sl int, @CurrentID nvarchar(10) output)
as
begin try

if(exists(select * from CHITIETDONHANG where MASP=@masp and MADH=@madh))
 begin
  set @CurrentID=''
  return
 end

INSERT INTO CHITIETDONHANG(MASP, MADH, SL)
values(@masp, @madh, @sl)
set @CurrentID = @madh

end try
begin catch
 set @CurrentID=''
 end catch
GO

--xoa ctgh
create PROC [dbo].[XoaCTGH](@mand nvarchar(10) ,@CurrentID nvarchar(10) output)
as
begin try

delete CHITIETGIOHANG
where MAND = @mand
set @CurrentID=@mand

end try
begin catch
 set @CurrentID=''
 end catch
GO

--lay dh theo nd
alter proc [dbo].[TatCaDonHangTheoNguoiDung] (@mand nvarchar(10))
as

select MADH, NGUOIDUNG.MAND, TENDN, NGAYLAP, GIATRI
from DONHANG, NGUOIDUNG
where DONHANG.MAND = NGUOIDUNG.MAND
and NGUOIDUNG.MAND = @mand

GO

--lay tat ca donhang
alter proc [dbo].[TatCaDonHang]
as
select MADH, NGUOIDUNG.MAND, TENDN, NGAYLAP, GIATRI
from DONHANG, NGUOIDUNG
where DONHANG.MAND = NGUOIDUNG.MAND

GO

-- lay ctdh
create proc [dbo].[LayCTDH](@madh nvarchar(10))
as

 select SANPHAM.MASP, TENSP, CHITIETDONHANG.SL, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH
 from SANPHAM, CHITIETDONHANG
 where SANPHAM.MASP = CHITIETDONHANG.MASP
 and MADH= @madh

GO

alter proc [dbo].[LayGioHang](@mand nvarchar(10))
as
 select SANPHAM.MASP, TENSP, CHITIETGIOHANG.SOLUONG, DANHGIA, DONGIA, NAMSX, HANGSX, GIOITINH, MOTA, HINH
 from SANPHAM, CHITIETGIOHANG
 where SANPHAM.MASP = CHITIETGIOHANG.MASP
 and MAND= @mand
GO