CREATE DATABASE QLNSFAHASA
USE QLNSFAHASA
CREATE TABLE DonViTinh (
MaDVT VARCHAR(10) PRIMARY KEY NOT NULL,
    DVT NVARCHAR(20) NOT NULL
);
CREATE TABLE DanhMucHinhThucThanhToan (
    MaLTT VARCHAR(10) PRIMARY KEY NOT NULL,
    LoaiHinhThucTT NVARCHAR(20)
);

CREATE TABLE NCC (
    MaNCC VARCHAR(10) PRIMARY KEY NOT NULL,
    TenNCC NVARCHAR(100),
    DiaChiNCC NVARCHAR(100)
);
CREATE TABLE DanhMucLoaiSanPham (
    MaLSP VARCHAR(10) PRIMARY KEY NOT NULL,
    TenLSP NVARCHAR(100)
);
CREATE TABLE SanPham (
    MaSP VARCHAR(10) PRIMARY KEY NOT NULL,
    TenSP NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(100)NOT NULL,
    DonGia DECIMAL(10, 2) NOT NULL,
    SoLuong INT NOT NULL,
    NXB DATE NOT NULL,
    ANH nvarchar(100) NOT NULL,
    MaNCC VARCHAR(10) NOT NULL,
    MaLSP VARCHAR(10) NOT NULL,
    MaDVT VARCHAR(10) NOT NULL,
    FOREIGN KEY (MaNCC) REFERENCES NCC(MaNCC),
    FOREIGN KEY (MaLSP) REFERENCES DanhMucLoaiSanPham(MaLSP),
    FOREIGN KEY (MaDVT) REFERENCES DonViTinh(MaDVT)
);
CREATE TABLE NhanVien (
    MaNV VARCHAR(10) PRIMARY KEY NOT NULL,
    HoTenNV NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100),
    NgaySinh DATE NOT NULL,
    SDT VARCHAR(10),
    Luong DECIMAL(10, 2),
    ChucVu NVARCHAR(20),
	Email VARCHAR(100) NOT NULL,
    GioiTinh BIT,
	MatKhau VARCHAR(50) NOT NULL,
	RoleUser varchar(50) NOT NULL
);
CREATE TABLE KhachHang (
	MaKH VARCHAR(10) PRIMARY KEY NOT NULL,
	HoTenKH NVARCHAR(100) NOT NULL,
	DiaChiKH NVARCHAR(100),
	NgaySinh DATE NOT NULL,
	SDT VARCHAR(10) NOT NULL,
	MatKhau VARCHAR(50) NOT NULL,
	Email varchar(100) NOT NULL
);
  
CREATE TABLE HoaDon (
    MaHD VARCHAR(10) PRIMARY KEY NOT NULL,
    NgayLap DATE,
    MaKH VARCHAR(10) NOT NULL,
    MaNV VARCHAR(10),
    MaLTT VARCHAR(10),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaLTT) REFERENCES DanhMucHinhThucThanhToan(MaLTT)
);
CREATE TABLE CTHD (
    MaHD VARCHAR(10) NOT NULL,
    MaSP VARCHAR(10) NOT NULL,
    SoLuongBan INT,
    TongTien DECIMAL(10, 2),
	PRIMARY KEY (MaHD, MaSP),
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
CREATE TABLE ThamSo (
    MaTS VARCHAR(10) PRIMARY KEY NOT NULL,
    TenTS NVARCHAR(50),
    DVT NVARCHAR(20),
    GiaTri INT,
    TinhTrang BIT
);
INSERT INTO DonViTinh (MaDVT, DVT) VALUES
('DVT1', N'Bộ'),
('DVT2', N'Quyển');
INSERT INTO DanhMucHinhThucThanhToan (MaLTT, LoaiHinhThucTT)
VALUES
    ('LTT1', N'Trực tiếp'),
    ('LTT2', N'Quét mã');
INSERT INTO NCC (MaNCC, TenNCC, DiaChiNCC)
VALUES
    ('NCC1', N'Nhà xuất bản Kim Đồng', N'123 Nguyễn Văn Cừ, Hà Nội'),
('NCC2', N'Nhà xuất bản Trẻ', N'456 Lê Lợi, TP.HCM'),
('NCC3', N'Nhà xuất bản Đại học Quốc gia Hà Nội', N'789 Đường Xuân Thủy, Hà Nội'),
('NCC4', N'Nhà sách Tiki', N'Số 2, Đường Dương Đình Nghệ, Cầu Giấy, Hà Nội'),
('NCC5', N'Nhà sách Fahasa', N'40 Trần Hưng Đạo, Quận 1, TP.HCM'),
('NCC6', N'Nhà sách Vinabook', N'341 CMT8, Phường 10, Quận 3, TP.HCM'),
('NCC7', N'Nhà sách Văn Lang', N'43 Nguyễn Huệ, Hoàn Kiếm, Hà Nội'),
('NCC8', N'Nhà sách Minh Khai', N'48 Lê Thánh Tông, Quận Hoàn Kiếm, Hà Nội'),
('NCC9', N'Nhà sách PNC', N'Số 41, đường Nguyễn Du, phường Bến Thành, quận 1, TP.HCM'),
('NCC010', N'Nhà sách Tiền Phong', N'1517 Đinh Tiên Hoàng, Hoàn Kiếm, Hà Nội');
INSERT INTO DanhMucLoaiSanPham (MaLSP, TenLSP)
VALUES
('LSP1', N'Văn học nước ngoài'),
('LSP2', N'Tâm lý - Kỹ năng sống'),
('LSP3', N'Khoa học - Kỹ thuật'),
('LSP4', N'Sách giáo khoa'),
('LSP5', N'Tiểu thuyết'),
('LSP6', N'Truyện tranh'),
('LSP7', N'Chính trị - Công tác xã hội');
INSERT INTO SanPham (MaSP, TenSP, MoTa, DonGia, SoLuong, NXB, ANH, MaNCC, MaLSP, MaDVT) VALUES
('SP1', N'Châu phi nghìn trùng', N'Từ những trải nghiệm của một con người, nhưng lại mở cho chúng ta cánh cửa tới châu Phi rộng lớn.', 45000, 50, '20220101', 'chauphinghintrung.png', 'NCC1', N'LSP1', 'DVT2'),
('SP2', N'Tìm lại sức mạnh tình thân', N'Sách giới thiệu bảy công cụ giúp phát triển sức mạnh nội tâm và tăng cường sức khoẻ ở mọi cấp độ. ', 32000, 1, '20211201', 'timlaisucmanhtinhthan.jpg', 'NCC2', 'LSP2', 'DVT2'),
('SP3', N'Đàn bà 30', N'Vì năm tháng đã trôi qua lặng yên, có thứ đã đến, như thành đạt, như từng trải, như tiền. ”', 48000, 30, '20220315', 'danba.jpg', 'NCC1', N'LSP2', 'DVT2'),
('SP4', N'Harry Potter và Hòn đá phù thủy', N'Cuốn sách đầu tiên trong series Harry Potter của tác giả J.K. Rowling', 200000, 20, '20200514', 'harrypotter1.jpg', 'NCC1', 'LSP5', 'DVT1'),
('SP5', N'Kỷ lục mới nhất về thế giới tự nhiên và khoa học kỹ thuật', N'Với cuốn sách này, độc giả Việt Nam sẽ được cung cấp thêm rất nhiều thông tin bổ ích, lý thú.', 250000, 30, '20101010', 'kylucmoinhatvethegioitunhienkhoahockythuat.jpg', 'NCC2', 'LSP3', 'DVT1'),
('SP6', N'Dấu chân trên cát', N'Cuốn sách kể về hành trình khám phá sa mạc Sahara của nhà báo  nhà văn Nguyễn Ngọc Bích', 170000, 15, '20180701', 'dauchantrenca.jpg', 'NCC1', 'LSP1', 'DVT1'),
('SP7', N'Tiếng Anh 10 Friends Global - Student Book', N'Như tên gọi Friends Global sẽ đưa học sinh vào thế giới ngôn ngữ thực', 46000, 25, '20170220', 'tienganh.jpg', 'NCC2', 'LSP4', 'DVT2'),
('SP8', N'Bánh xốp', N'Thế giới trong Bánh Xốp đã hình thành nên cuộc sống hệ thống toàn cầu hóa của chủ nghĩa tư bản', 52, 40, '200101', 'banhxop.jpg', 'NCC1', 'LSP5', 'DVT2'),
('SP9', N'7 chuyến du hành vào thiên nhiên', N'Cuốn sách “mang bảy sắc cầu vồng trên nền trời xanh bao la, cũng là bảy cung bậc cảm xúc khác nhau”', 48000, 20, '20150505', '7chuyenduhanh.jpg', 'NCC2', 'LSP6', 'DVT2'),
('SP10', N'Phụ nữ Việt Nam những khoảnh khắc', N'Những khoảnh khắc đẹp trong suốt 90 năm mà phụ nữ đã chung tay cùng cả nước tạo nên sức mạnh', 210000, 30, '20191231', 'pnvn.jpg', 'NCC1', 'LSP7', 'DVT1');

INSERT INTO NhanVien (MaNV, HoTenNV, DiaChi, NgaySinh, SDT, Luong, ChucVu, Email, GioiTinh, MatKhau, RoleUser)
VALUES
    ('NV1', N'Huỳnh Công Lợi', N'Nha Trang  Khánh Hòa', '20021211', '0375521057', 10, N'Quản Lí','congloi@gmail.com', 1,'loi123456','Admin'),
   ('NV2', N'Lê Hoàng Thiện', N'  Khánh Hòa', '19950101', '0375521057', 30, N'Bán Hàng','hoangthien@gmail.com', 1,'thien123456','NhanVien'),
    ('NV3', N'Nguyễn Thành Đạt', N'Ninh Hoà', '19950101', '0789423651', 30, N'Bán Hàng','thanhdat@gmail.com', 1,'dat123456','NhanVien'),
('NV4', N'Phan Trần Thu Phương', N'Ninh Hòa', '19990406', '0136542369', 80, N'Quản Lí','thuphuong@gmail.com', 0,'phuong123456','Admin');

INSERT INTO KhachHang (MaKH, HoTenKH, DiaChiKH, NgaySinh, SDT, MatKhau, Email)
VALUES
    ('KH1', N'Trần Ngọc Tiến', N'Diên Khánh', '19990101','0792288731','tien123456','ngoctien@gmail.com'),
    ('KH2', N'Úc Tiển', N'Cam Ranh','19950101', '0792288731','tuyen123456','uctuyen@gmail.com'),
    ('KH3', N'Trương Khánh Hòa', N'Cam Ranh', '19950101','0977942997','hoa123456','khanhhoa@gmail.com'),
    ('KH4', N'Nguyễn Duy Thiên', N'Cam Ranh', '19950101','0987543580','thien123456','duythien@gmail.com'),
	('KH5', N'Nguyễn Thị Diễm Kiều', N'Phú Yên', '19950101','0355792288','kieu123456','diemkieu@gmail.com'),
	('KH6', N'Phan Minh Tiến', N'Phú Yên', '19950101','1234567890','tien123456','minhtien@gmail.com'),
	('KH7', N'Vũ Lê Hoàng Nam', N'Nha Trang', '19950101','1235567777','nam123456','hoangnam@gmail.com');
INSERT INTO HoaDon (MaHD, NgayLap, MaKH, MaNV, MaLTT)
VALUES
    ('HD1', '20230101', 'KH1', 'NV3', 'LTT1'),
    ('HD2', '20230201', 'KH2', 'NV2', 'LTT2'),
    ('HD3', '20230301', 'KH3', 'NV3', 'LTT1'),
    ('HD4', '20230401', 'KH4', 'NV2', 'LTT2'),
    ('HD5', '20230501', 'KH5', 'NV3', 'LTT1'),
    ('HD6', '20230601', 'KH6', 'NV2', 'LTT2'),
    ('HD7', '20230701', 'KH7', 'NV2', 'LTT1')
    ;
INSERT INTO CTHD (MaHD, MaSP, SoLuongBan, TongTien)
VALUES
    ('HD1', 'SP1', 1, 45),
    ('HD1', 'SP2', 1, 32),
    ('HD2', 'SP3', 2, 96),
    ('HD2', 'SP4', 1, 200),
    ('HD3', 'SP5', 3, 750),
    ('HD3', 'SP6', 1, 170),
    ('HD4', 'SP7', 1, 46),
    ('HD4', 'SP8', 2, 104),
    ('HD5', 'SP9', 3, 210),
    ('HD5', 'SP10', 1, 144);
INSERT INTO ThamSo (MaTS, TenTS, DVT, GiaTri, TinhTrang)
VALUES ('TS1', N'ThamSo1', 'USD', 220, 1),
		('TS2', N'ThamSo2', 'VND', 10, 1),
		('TS3', N'ThamSo3', 'EUR', 0, 0);

CREATE PROCEDURE [dbo].[TimKiem]
    @TuKhoa NVARCHAR(1)
AS
BEGIN
    SET NOCOUNT ON;
    
     --Tìm kiếm trong bảng KhachHang
    SELECT * FROM KHACHHANG 
    WHERE MaKH LIKE '%' + @TuKhoa + '%' OR HoTenKH LIKE '%' + @TuKhoa + '%' OR DiaChiKH LIKE '%' + @TuKhoa + '%' ;
    
     --Tìm kiếm trong bảng HoaDon
    SELECT * FROM HOADON 
    WHERE MaHD LIKE '%' + @TuKhoa + '%' OR NgayLap LIKE '%' + @TuKhoa + '%';
    
     --Tìm kiếm trong bảng SANPHAM
    SELECT * FROM SanPham 
    WHERE MaSP LIKE '%' + @TuKhoa + '%' OR TenSP LIKE '%' + @TuKhoa + '%' OR DonGia LIKE '%' + @TuKhoa + '%';
    
     --Tìm kiếm trong bảng NCC
    SELECT * FROM NCC 
    WHERE MaNCC LIKE '%' + @TuKhoa + '%' OR TenNCC LIKE '%' + @TuKhoa + '%';
    
     --Tìm kiếm trong bảng LoaiSP
    SELECT * FROM DanhMucLoaiSanPham 
    WHERE MaLSP LIKE '%' + @TuKhoa + '%' OR TenLSP LIKE '%' + @TuKhoa + '%';
END
GO

