# 🚂 HỆ THỐNG QUẢN LÝ ĐẶT VÉ TÀU HỎA — ProjectTrainWeb

> Dự án web Blazor Server quản lý đặt vé tàu hỏa, áp dụng các nguyên lý **Lập Trình Hướng Đối Tượng (OOP)**: Kế thừa, Đa hình, Trừu tượng hóa, Đóng gói.

---

## 📋 MỤC LỤC

1. [Yêu cầu hệ thống](#-yêu-cầu-hệ-thống)
2. [Hướng dẫn cài đặt](#-hướng-dẫn-cài-đặt-từng-bước)
3. [Cách chạy dự án](#-cách-chạy-dự-án)
4. [Chức năng chính](#-chức-năng-chính)
5. [Cấu trúc dự án](#-cấu-trúc-dự-án)
6. [Nguyên lý OOP được áp dụng](#-nguyên-lý-oop-được-áp-dụng)
7. [Tài khoản mẫu](#-tài-khoản-mẫu-để-test)
8. [Phần C++ Console](#-phần-c-console-tham-khảo)
9. [Hướng dẫn phát triển thêm](#-hướng-dẫn-phát-triển-thêm)

---

## 💻 YÊU CẦU HỆ THỐNG

| Thành phần | Phiên bản tối thiểu | Bắt buộc? |
|---|---|---|
| Hệ điều hành | Windows 10/11 (64-bit) | ✅ |
| .NET SDK | 10.0 trở lên | ✅ |
| Visual Studio Code | Phiên bản mới nhất | ✅ |
| Trình duyệt web | Chrome / Edge / Firefox | ✅ |
| g++ (MinGW) | 8.0+ | ⚠️ Chỉ cần nếu chạy phần C++ |

---

## 📥 HƯỚNG DẪN CÀI ĐẶT TỪNG BƯỚC

### Bước 1: Cài đặt .NET SDK

**.NET SDK** là bộ công cụ để build và chạy ứng dụng C# / Blazor.

1. Truy cập: https://dotnet.microsoft.com/download
2. Tải bản **.NET 10.0 SDK** (hoặc mới hơn) cho Windows x64
3. Chạy file cài đặt, nhấn **Next → Next → Install**
4. Mở **PowerShell** hoặc **CMD**, kiểm tra:

```powershell
dotnet --version
```

Kết quả mong đợi: `10.0.xxx` (ví dụ: `10.0.203`)

> ⚠️ **Lưu ý:** Nếu lệnh `dotnet` không nhận, hãy đóng và mở lại terminal.

---

### Bước 2: Cài đặt Visual Studio Code

1. Truy cập: https://code.visualstudio.com/
2. Tải và cài đặt phiên bản mới nhất cho Windows
3. Mở VS Code sau khi cài xong

---

### Bước 3: Cài đặt Extensions cho VS Code

Mở **Terminal** trong VS Code (Ctrl + `) và chạy từng lệnh:

```powershell
# [BẮT BUỘC] Extension C# — IntelliSense, debug, go-to-definition
code --install-extension ms-dotnettools.csharp

# [BẮT BUỘC] C# Dev Kit — Solution explorer, test runner
code --install-extension ms-dotnettools.csdevkit

# [BẮT BUỘC] .NET Runtime — Hỗ trợ chạy .NET trong VS Code
code --install-extension ms-dotnettools.vscode-dotnet-runtime

# [KHUYẾN NGHỊ] Blazor WASM Companion — Debug Blazor
code --install-extension ms-dotnettools.blazorwasm-companion
```

Hoặc cài thủ công:
1. Mở VS Code → nhấn `Ctrl + Shift + X` (Extensions)
2. Tìm kiếm **"C#"** → cài extension của Microsoft
3. Tìm kiếm **"C# Dev Kit"** → cài
4. Tìm kiếm **"Blazor WASM Companion"** → cài

---

### Bước 4: Cấu hình NuGet (Nguồn thư viện)

Mở **Terminal** và chạy:

```powershell
dotnet nuget add source https://api.nuget.org/v3/index.json --name "nuget.org"
```

Kiểm tra đã thêm thành công:

```powershell
dotnet nuget list source
```

Kết quả mong đợi:
```
Registered Sources:
  1.  nuget.org [Enabled]
      https://api.nuget.org/v3/index.json
```

> ⚠️ **Nếu đã có sẵn** nguồn `nuget.org`, lệnh sẽ báo lỗi "already exists" — không sao, bỏ qua.

---

### Bước 5: Cài đặt g++ (Chỉ cần nếu chạy phần C++ Console)

Nếu bạn muốn chạy phần C++ console (folder `Project_Train`):

1. Truy cập: https://www.msys2.org/
2. Tải và cài MSYS2
3. Mở **MSYS2 UCRT64** terminal, chạy:

```bash
pacman -S mingw-w64-ucrt-x86_64-gcc
```

4. Thêm đường dẫn `C:\msys64\ucrt64\bin` vào biến môi trường **PATH**
5. Kiểm tra:

```powershell
g++ --version
```

---

## 🚀 CÁCH CHẠY DỰ ÁN

### Chạy phần Web (Blazor Server)

```powershell
# 1. Mở terminal, di chuyển tới thư mục dự án web
cd TRAIN_PRO\newproject\ProjectTrainWeb

# 2. Khôi phục các gói thư viện (chỉ cần lần đầu)
dotnet restore

# 3. Build dự án (kiểm tra lỗi)
dotnet build

# 4. Chạy web server
dotnet run
```

Sau khi chạy, terminal sẽ hiển thị:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5144
```

**→ Mở trình duyệt, truy cập: http://localhost:5144**

> 💡 Nhấn `Ctrl + C` trong terminal để dừng web server.

---

### Chạy phần C++ Console (Tham khảo thuật toán)

```powershell
# 1. Di chuyển tới thư mục
cd TRAIN_PRO\Project_Train

# 2. Biên dịch tất cả file
g++ -std=c++11 -o Project_Train.exe NguoiDung.cpp TauHoa.cpp VeTau.cpp HeThongDatVe.cpp main.cpp

# 3. Chạy chương trình
.\Project_Train.exe
```

---

## ⭐ CHỨC NĂNG CHÍNH

### 👤 Hành khách

| # | Chức năng | Mô tả |
|---|---|---|
| 1 | **Đăng ký / Đăng nhập** | Tạo tài khoản mới, đăng nhập bằng tài khoản/mật khẩu |
| 2 | **Tìm kiếm chuyến tàu** | Tìm theo ga đi, ga đến, xem thông tin chi tiết |
| 3 | **Chọn toa & ghế** | Xem sơ đồ ghế trực quan, chọn ghế trống |
| 4 | **Đặt vé (Thường/VIP)** | Chọn loại vé → tính giá bằng **đa hình** (VeThuong / VeVIP) |
| 5 | **Thanh toán** | Thanh toán ngay hoặc giữ vé (có 3 ngày để thanh toán) |
| 6 | **Hủy vé** | Hủy vé + hoàn tiền tự động nếu đã thanh toán |
| 7 | **Xem vé của tôi** | Danh sách vé đã đặt, trạng thái thanh toán |
| 8 | **Nạp tiền** | Nạp tiền vào ví hệ thống |
| 9 | **Áp dụng mã khuyến mãi** | Nhập mã giảm giá khi thanh toán |
| 10 | **Xem thông báo** | Nhận thông báo đặt vé, hủy vé, quá hạn |

### 🔐 Quản trị viên (Admin)

| # | Chức năng | Mô tả |
|---|---|---|
| 1 | **Quản lý chuyến tàu** | Thêm / Xóa chuyến tàu (tự động tạo toa & ghế) |
| 2 | **Thống kê doanh thu** | Phân loại theo vé Thường/VIP, theo chuyến tàu |
| 3 | **Xem danh sách hành khách** | Thông tin tài khoản, số dư |
| 4 | **Xem lịch sử hệ thống** | Ghi nhận mọi thao tác (audit log) |

### ⚙️ Hệ thống tự động

| Chức năng | Mô tả |
|---|---|
| **Tự động hủy vé quá hạn** | Vé chưa thanh toán quá 3 ngày → tự động hủy + giải phóng ghế |
| **Tính giá vé đa hình** | VeThuong = giá gốc, VeVIP = giá gốc × 1.3 (cao hơn 30%) |

---

## 📁 CẤU TRÚC DỰ ÁN

```
TRAIN_PRO/
│
├── README_HUONG_DAN.md           # 📖 File hướng dẫn này
├── Project_Train/                # 📂 Phần C++ Console (thuật toán gốc)
│
└── newproject/ProjectTrainWeb/   # 📂 Phần Web Blazor Server
    │
    ├── Program.cs                    # Điểm khởi động ứng dụng + Đăng ký DI
    ├── ProjectTrainWeb.csproj        # File cấu hình dự án (.NET 10.0)
│
├── Models/                       # 📦 Lớp dữ liệu (OOP Models)
│   ├── NguoiDung.cs              #   Lớp trừu tượng — Người dùng (Abstract)
│   ├── HanhKhach.cs              #   Kế thừa NguoiDung — Hành khách
│   ├── QuanTriVien.cs            #   Kế thừa NguoiDung — Quản trị viên
│   ├── ChuyenTau.cs              #   Chuyến tàu (chứa List<ToaTau>)
│   ├── ToaTau.cs                 #   Toa tàu (chứa List<GheNgoi>)
│   ├── GheNgoi.cs                #   Ghế ngồi (trạng thái đặt/trống)
│   ├── GaTau.cs                  #   Ga tàu (tên ga, thành phố)
│   ├── VeTau.cs                  #   Lớp trừu tượng — Vé tàu (Abstract)
│   ├── VeThuong.cs               #   Kế thừa VeTau — Vé thường
│   ├── VeVIP.cs                  #   Kế thừa VeTau — Vé VIP (×1.3)
│   ├── GiaoDich.cs               #   Giao dịch tài chính
│   ├── KhuyenMai.cs              #   Mã khuyến mãi / voucher
│   ├── ThongBao.cs               #   Thông báo hệ thống
│   └── LichSuHeThong.cs          #   Audit log
│
├── Services/                     # ⚙️ Business Logic (OOP Services)
│   ├── TrainBookingService.cs    #   Facade — Dữ liệu mẫu + trạng thái chung
│   ├── NguoiDungService.cs       #   Đăng nhập / Đăng ký / Nạp tiền
│   ├── ChuyenTauService.cs       #   CRUD chuyến tàu + kiểm tra ghế trống
│   ├── DatVeService.cs           #   Đặt vé (đa hình) + Hủy vé
│   ├── ThanhToanService.cs       #   Thanh toán + Quá hạn 3 ngày + Khuyến mãi
│   ├── ThongKeService.cs         #   Thống kê doanh thu (pattern matching)
│   └── ThongBaoService.cs        #   Quản lý thông báo
│
├── Components/                   # 🎨 Giao diện Blazor
│   ├── App.razor                 #   Root component
│   ├── Routes.razor              #   Routing
│   ├── _Imports.razor            #   Global using directives
│   ├── TrainCard.razor           #   Component hiển thị thông tin chuyến tàu
│   ├── Layout/
│   │   ├── MainLayout.razor      #   Layout chính (header, sidebar, footer)
│   │   ├── NavMenu.razor         #   Menu điều hướng
│   │   └── Footer.razor          #   Footer
│   └── Pages/
│       ├── Home.razor            #   Trang chủ — Tìm kiếm chuyến tàu
│       ├── TrainList.razor       #   Danh sách chuyến tàu
│       ├── SeatSelection.razor   #   Chọn toa & ghế (sơ đồ trực quan)
│       ├── Checkout.razor        #   Thanh toán đặt vé
│       ├── Login.razor           #   Đăng nhập
│       ├── Register.razor        #   Đăng ký tài khoản
│       ├── MyTickets.razor       #   Vé của tôi
│       ├── TicketLookup.razor    #   Tra cứu vé
│       ├── Profile.razor         #   Thông tin cá nhân + nạp tiền
│       ├── Notifications.razor   #   Thông báo
│       ├── ActivityHistory.razor #   Lịch sử hoạt động
│       ├── AdminDashboard.razor  #   Bảng điều khiển Admin
│       └── Error.razor           #   Trang lỗi
│
└── wwwroot/                      # 📂 File tĩnh (CSS, JS, hình ảnh)
```

---

## 🧬 NGUYÊN LÝ OOP ĐƯỢC ÁP DỤNG

### 1. Kế thừa (Inheritance)

```
NguoiDung (abstract)           VeTau (abstract)
    ├── HanhKhach                  ├── VeThuong
    └── QuanTriVien                └── VeVIP
```

- `HanhKhach` và `QuanTriVien` kế thừa từ `NguoiDung`, chia sẻ thuộc tính TaiKhoan, MatKhau, HoTen.
- `VeThuong` và `VeVIP` kế thừa từ `VeTau`, chia sẻ thuộc tính MaVe, ChuyenTau, NgayDat.

### 2. Đa hình (Polymorphism)

```csharp
// Biến kiểu lớp cha, nhưng gọi phương thức lớp con
VeTau ve = new VeVIP(...);
double gia = ve.TinhGiaVe(); // Gọi VeVIP.TinhGiaVe() = GiaCoBan × 1.3

VeTau ve2 = new VeThuong(...);
double gia2 = ve2.TinhGiaVe(); // Gọi VeThuong.TinhGiaVe() = GiaCoBan
```

- **File áp dụng**: `DatVeService.cs` (đặt vé), `ThongKeService.cs` (thống kê)

### 3. Trừu tượng hóa (Abstraction)

```csharp
public abstract class VeTau
{
    public abstract double TinhGiaVe(); // Bắt buộc lớp con phải cài đặt
}
```

- `NguoiDung` và `VeTau` là lớp trừu tượng — không thể tạo trực tiếp, chỉ tạo thông qua lớp con.

### 4. Đóng gói (Encapsulation)

```csharp
// Thao tác với dữ liệu thông qua phương thức, không truy cập trực tiếp
ghe.DatGhe();              // Thay vì: ghe.TrangThai = true
hanhKhach.TruTien(50000);  // Có kiểm tra số dư bên trong
```

- **File áp dụng**: `ChuyenTauService.cs`, `ThanhToanService.cs`

### 5. Pattern Matching (thay thế dynamic_cast C++)

```csharp
// C++: VeVIP* veVip = dynamic_cast<VeVIP*>(ve);
// C#:
if (ve is VeVIP veVip)
{
    doanhThuVIP += veVip.TinhGiaVe();
}
```

- **File áp dụng**: `ThongKeService.cs` (phân loại vé Thường/VIP)

### 6. Composition (Quan hệ "chứa")

```
ChuyenTau ──chứa──▶ List<ToaTau> ──chứa──▶ List<GheNgoi>
```

- Mỗi chuyến tàu chứa nhiều toa, mỗi toa chứa nhiều ghế.

---

## 🔑 TÀI KHOẢN MẪU ĐỂ TEST

| Vai trò | Tài khoản | Mật khẩu | Số dư |
|---|---|---|---|
| 🔐 Admin | `admin` | `admin123` | — |
| 👤 Hành khách 1 | `user1` | `123456` | 5.000.000 VND |
| 👤 Hành khách 2 | `user2` | `123456` | 3.000.000 VND |

### Mã khuyến mãi mẫu

| Mã code | Giảm | Giảm tối đa | Hạn sử dụng |
|---|---|---|---|
| `NEWTRAVEL2026` | 20% | 200.000 VND | 3 tháng |
| `SUMMER10` | 10% | 100.000 VND | 1 tháng |
| `VIP30` | 30% | 500.000 VND | 2 tháng |

---

## 📂 PHẦN C++ CONSOLE (THAM KHẢO)

Folder `Project_Train/` chứa phiên bản **C++ console** — là nền tảng thuật toán gốc trước khi chuyển sang web.

### File C++ và vai trò

| File | Vai trò | Tương ứng C# |
|---|---|---|
| `NguoiDung.h/cpp` | Lớp NguoiDung, HanhKhach, QuanTriVien | `Models/NguoiDung.cs`, `HanhKhach.cs`, `QuanTriVien.cs` |
| `TauHoa.h/cpp` | Lớp GheNgoi, ToaTau, ChuyenTau | `Models/GheNgoi.cs`, `ToaTau.cs`, `ChuyenTau.cs` |
| `VeTau.h/cpp` | Lớp VeTau, VeThuong, VeVIP | `Models/VeTau.cs`, `VeThuong.cs`, `VeVIP.cs` |
| `HeThongDatVe.h/cpp` | Hệ thống chính (menu, đặt vé, thống kê) | `Services/*.cs` (6 service files) |
| `main.cpp` | Điểm khởi động | `Program.cs` |

### Cách biên dịch C++

```powershell
cd TRAIN_PRO\Project_Train
g++ -std=c++11 -o Project_Train.exe NguoiDung.cpp TauHoa.cpp VeTau.cpp HeThongDatVe.cpp main.cpp
.\Project_Train.exe
```

---

## 🔧 HƯỚNG DẪN PHÁT TRIỂN THÊM

### Thêm Service mới

1. Tạo file `TenService.cs` trong folder `Services/`
2. Inject `TrainBookingService` để truy cập dữ liệu chung:

```csharp
public class TenService
{
    private readonly TrainBookingService _data;

    public TenService(TrainBookingService data)
    {
        _data = data;
    }

    // Thêm logic ở đây...
}
```

3. Đăng ký trong `Program.cs`:

```csharp
builder.Services.AddScoped<TenService>();
```

4. Sử dụng trong Blazor page:

```razor
@inject TenService TenSvc
```

### Thêm trang Blazor mới

1. Tạo file `TenTrang.razor` trong `Components/Pages/`
2. Thêm route:

```razor
@page "/ten-trang"
@inject TrainBookingService BookingSvc
@rendermode InteractiveServer

<h3>Trang mới</h3>

@code {
    // Logic ở đây
}
```

### Thêm Model mới

1. Tạo file `TenModel.cs` trong `Models/`
2. Nếu cần đa hình: kế thừa từ lớp abstract
3. Thêm danh sách vào `TrainBookingService.cs`

---

## 📝 GHI CHÚ QUAN TRỌNG

- **Dữ liệu mẫu**: Dự án sử dụng dữ liệu mẫu trong bộ nhớ (không có database). Mỗi lần khởi động lại sẽ reset dữ liệu.
- **Giá vé VIP**: Cao hơn **30%** so với vé thường (`HeSoVip = 1.3`).
- **Quy tắc quá hạn**: Vé chưa thanh toán quá **3 ngày** sẽ bị tự động hủy.
- **Framework**: Blazor Server (.NET 10.0) — Interactive Server Rendering.

---

## 👥 NHÓM PHÁT TRIỂN

- **Môn học**: Lập Trình Hướng Đối Tượng (OOP)
- **Công nghệ**: C++ (Console) + C# Blazor Server (Web)
- **Phiên bản**: 1.0 — Tháng 6/2026

---

*Nếu gặp lỗi khi cài đặt hoặc chạy, hãy kiểm tra lại các bước ở mục [Hướng dẫn cài đặt](#-hướng-dẫn-cài-đặt-từng-bước).*
