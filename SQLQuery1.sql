CREATE DATABASE QUANLYNHANSU
GO

use QUANLYNHANSU
go
create table tb_NHANVIEN(
MANV int primary key identity(1000000000,1) check (MANV between 1000000000 and 9999999999),
HOTEN nvarchar(50),
GIOITINH nvarchar(50),
NGAYSINH datetime,
DIENTHOAI nvarchar(50),
EMAIL nvarchar(50),
MATKHAU varchar(50),
CCCD nvarchar(50),
DIACHI nvarchar(300),
HINHANH varchar(100),
IDPB int,
IDBP int,
IDCV int,
IDTD int,
IDDT int,
IDTG int
)

go
create table tb_CHUCVU(
IDCV int primary key identity(1,1),
TENCV nvarchar(50),
)

go
create table tb_DANTOC(
IDDT int primary key identity(1,1),
TENDT nvarchar(50),
)

go
create table tb_TONGIAO(
IDTG int primary key identity(1,1),
TENTG nvarchar(50),
)

go
create table tb_BOPHAN(
IDBP int primary key identity(1,1),
TENBP nvarchar(50),
)

go
create table tb_BAOHIEM(
IDBH int primary key identity(1,1),
SOBH nvarchar(50),
NGAYCAP datetime,
NOICAP nvarchar(50),
NOIKHAMBENH nvarchar(50),
MANV int,
)

go
create table tb_PHONGBAN(
IDPB int primary key identity(1,1),
TENPB nvarchar(50),
)

go
create table tb_HOPDONG(
SOHD int primary key identity(1,1),
NGAYBAT datetime,
NGAYKETTHUC datetime,
NGAYKY datetime,
NOIDUNG nvarchar(MAX),
LANKY int,
THOIHAN nvarchar(50),
HESOLUONG float,
MANV int,
)

go
create table tb_KHENTHUONG(
ID int primary key identity(1,1),
SOKTKL int,
NOIDUNG nvarchar(50),
NGAY datetime,
MANV int,
LOAI int,
)

go
create table tb_KYLUAT(
ID int primary key identity(1,1),
SOKTKL int,
NOIDUNG nvarchar(50),
NGAY datetime,
MANV int,
LOAI int,
)


go
create table tb_TANGCA(
ID int primary key identity(1,1),
NAM int,
THANG int,
NGAY int,
SOGIO float,
MANV int,
IDLOAICA int,
)

go
create table tb_LOAICA(
IDLOAICA int primary key identity(1,1),
TENLOAICA nvarchar(50),
HESO float,
)

go
create table tb_TRINHDO(
IDTD int primary key identity(1,1),
TENTD nvarchar(50),
)

go
create table tb_BANGCONG(
MACONG int primary key identity(1,1),
TENNV nvarchar(100),
MANV int,
TENCV nvarchar(50),
TENPB nvarchar(50),
NGAYLAM date,
NGAYRA date,
GIOLAM time,
GIORA time,
TONGGIOLAM int
)

go
create table tb_LOAICONG(
IDLC int primary key identity(1,1),
TENLC nvarchar(50),
HESO float,
)

go
create table tb_PHUCAP(
IDPC int primary key identity(1,1),
TENPC nvarchar(50),
SOTIEN float,
)

go
create table tb_NHANVIEN_PHUCAP(
ID int primary key identity(1,1),
MANV int,
IDPC int,
NGAYCAP datetime,
NOIDUNG nvarchar(50),
SOTIEN float,
)

go
create table tb_UNGLUONG(
ID int primary key identity(1,1),
NAM int,
THANG  int,
NGAY int,
SOTIEN float,
TRANGTHANG bit,
MANV int,
)