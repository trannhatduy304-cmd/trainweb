# PROJECT: HỆ THỐNG QUẢN LÝ VÀ ĐẶT VÉ TÀU HỎA (TRAIN_PRO)

## 1. Tóm tắt dự án
Dự án **TRAIN_PRO** là một hệ thống web mô phỏng ứng dụng quản lý và đặt vé tàu hỏa trực tuyến. Hệ thống được chuyển đổi từ nền tảng C++ Console sang nền tảng Web hiện đại (ASP.NET Core Blazor), với mục tiêu chính là **thực hành và áp dụng triệt để các nguyên lý Lập trình Hướng đối tượng (OOP)**. 

Dự án không chỉ dừng lại ở các tính năng cho khách hàng (như tìm kiếm chuyến tàu, chọn toa, chọn ghế, thanh toán hóa đơn) mà còn bao gồm một hệ thống quản trị (Admin) đầy đủ để kiểm duyệt vé, quản lý người dùng và thay đổi cấu hình trang web.

---

## 2. Các chức năng chính của hệ thống

### 👨‍💻 Chức năng dành cho Hành Khách (Khách hàng)
- **Đăng ký / Đăng nhập**: Quản lý phiên làm việc của người dùng.
- **Tìm kiếm chuyến tàu**: Theo ga đi, ga đến, ngày đi.
- **Lựa chọn chỗ ngồi**: Trực quan hóa các toa tàu (toa thường, toa VIP), ghế trống/ghế đã đặt.
- **Đặt vé và chọn loại vé**: Hỗ trợ nhiều loại vé (Vé thường, Vé VIP).
- **Thanh toán vé**: Quá trình chờ admin duyệt (Mô phỏng gửi yêu cầu thanh toán).
- **Tra cứu và quản lý vé**: Xem lịch sử đặt vé, hủy vé.
- **Cập nhật hồ sơ**: Đổi mật khẩu, xem thông báo.

### 👑 Chức năng dành cho Quản Trị Viên (Admin)
- **Duyệt yêu cầu thanh toán**: Xác nhận hoặc từ chối yêu cầu đặt vé của khách hàng.
- **Quản lý danh sách vé**: Theo dõi toàn bộ vé trên hệ thống (đang chờ duyệt, đã thanh toán, đã hủy).
- **Quản lý tài khoản**: Theo dõi danh sách hành khách, cấp quyền hoặc xóa tài khoản.
- **Quản lý cấu hình hệ thống**: Thay đổi thông tin liên hệ, link mạng xã hội, các chính sách ở trang chủ.

---

## 3. Kiến trúc Lập trình Hướng Đối Tượng (OOP) - Trọng tâm dự án
Dự án được thiết kế chặt chẽ xoay quanh 4 tính chất cơ bản của OOP (Đóng gói, Kế thừa, Đa hình, Trừu tượng) cùng các kỹ thuật thiết kế class chuyên sâu.

### 3.1. Tính Kế thừa (Inheritance)
Cho phép tái sử dụng code, tạo ra các phân cấp rõ ràng trong hệ thống:
- **Người dùng**: Class `NguoiDung` là lớp cơ sở (Base Class) chứa các thuộc tính chung (Mã ID, Tài khoản, Mật khẩu, Họ tên). Từ đó, phân nhánh ra:
  - `HanhKhach`: Kế thừa `NguoiDung` và mở rộng thêm Số điện thoại.
  - `QuanTriVien`: Kế thừa `NguoiDung` và mở rộng thêm Mức cấp quyền (Level).
- **Vé Tàu**: Class `VeTau` là lớp cơ sở. Hệ thống có 2 loại vé kế thừa từ nó:
  - `VeThuong` (Vé phổ thông).
  - `VeVIP` (Vé thương gia, có hệ số tính giá cao hơn).

### 3.2. Tính Đa hình (Polymorphism)
Cho phép một giao diện phương thức thể hiện nhiều hành vi khác nhau tùy thuộc vào đối tượng đang gọi:
- Hàm `TinhGiaVe()` được định nghĩa `virtual` ở lớp cha `VeTau` (trả về giá cơ bản của ghế).
- Lớp `VeVIP` sẽ `override` (ghi đè) lại hàm này để tính giá: `GiaCoBan * 1.3`.
- Nhờ Đa hình, trong các vòng lặp tính tổng tiền giỏ hàng (`danhSachVe.Sum(v => v.TinhGiaVe())`), hệ thống không cần dùng câu lệnh `if-else` hay `switch-case` để kiểm tra từng loại vé. Hàm tính giá tự động thích ứng chính xác theo kiểu vé thực tế được khởi tạo.

### 3.3. Tính Đóng gói (Encapsulation)
Bảo vệ tính toàn vẹn của dữ liệu bằng cách che giấu các thuộc tính nội bộ và chỉ cho phép giao tiếp thông qua các phương thức được kiểm soát (Getter/Setter/Method):
- Các thuộc tính như `TrangThai` của `GheNgoi`, hay `DaThanhToan` của `VeTau` không được gán trực tiếp từ bên ngoài.
- Phải gọi thông qua các phương thức nghiệp vụ hợp lệ: `ghe.DatGhe()`, `ghe.HuyGhe()`, `ve.ThanhToanVe()`, `ve.HuyVe()`. Việc này đảm bảo logic thay đổi trạng thái (chẳng hạn kiểm tra xem ghế đã bị ai đó đặt chưa trước khi đổi trạng thái) không bị bỏ qua.

### 3.4. Tính Trừu tượng (Abstraction)
Ẩn đi độ phức tạp nội bộ của hệ thống:
- Các đối tượng nghiệp vụ được mô hình hóa sát với thực tế (`ChuyenTau`, `GaTau`, `ToaTau`).
- Khách hàng không cần biết thuật toán cấp phát mã vé bên dưới, chỉ cần gọi hàm `DatVe()` thông qua `DatVeService` là một vé được sinh ra và gửi về giỏ hàng.

### 3.5. Quan hệ thành phần (Composition / Aggregation)
Hệ thống thể hiện rõ sự phân cấp quản lý đối tượng:
- `ChuyenTau` "bao gồm" (has-a) một danh sách `List<ToaTau>`.
- Mỗi `ToaTau` lại "bao gồm" một `List<GheNgoi>`.
- Điều này phản ánh tư duy OOP mạnh mẽ: Để tìm một chiếc ghế, hệ thống phải duyệt qua chuyến tàu -> chọn toa tàu -> rồi mới trỏ đến ghế.

---

## 4. Thiết kế Web & Công nghệ (Tổng quan)
Mặc dù trọng tâm là C# và OOP, dự án vẫn đáp ứng yêu cầu của một ứng dụng web thực tế:
- **Công nghệ**: Sử dụng ASP.NET Core Blazor Server. (Giúp code C# có thể chạy trực tiếp để render ra HTML/CSS mà không cần viết quá nhiều Javascript).
- **Dependency Injection**: Áp dụng triệt để DI. Các service xử lý nghiệp vụ (`DatVeService`, `ThanhToanDuyetService`, `NguoiDungService`) được Inject trực tiếp vào các Component UI (Razor Pages). Điều này tách biệt rõ ràng giữa Business Logic (Class C#) và User Interface (HTML/CSS).
- **Giao diện (UI/UX)**: 
  - CSS thuần tùy biến cao, mang phong cách hiện đại với Glassmorphism, đổ bóng mờ, và hiệu ứng Hover.
  - Tổ chức cấu trúc Layout rõ ràng: `NavMenu`, `Footer`, `MainLayout`, giúp tái sử dụng mã nguồn.
  - Dữ liệu cấu hình như Thông tin liên hệ hay Mạng xã hội được render động (Dynamic) qua code C#, cho phép Admin tùy chỉnh trực tiếp mà không cần sửa source code HTML.
