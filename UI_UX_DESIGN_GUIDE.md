# ProjectTrain - UI/UX Design System

## 📐 Design Overview

ProjectTrain là một Web App hiện đại để Quản lý Đặt Vé Tàu Hỏa, được thiết kế theo phong cách **Minimalism** (tối giản) kết hợp **Glassmorphism**.

### Design Principles
- **Thẩm mỹ**: Sạch sẽ, tối giản, đáng tin cậy
- **Responsive**: Hoạt động hoàn hảo trên Desktop, Tablet, Mobile
- **Accessibility**: Dễ sử dụng cho mọi người dùng
- **Performance**: Tải nhanh, tương tác mượt mà

---

## 🎨 Color Palette

| Tên | Hex | Sử Dụng |
|-----|-----|--------|
| Primary Blue | `#003d7a` | Tiêu đề, nút chính, liên kết |
| Primary Light | `#005fa3` | Hover, gradient |
| Accent Orange | `#FFA500` | Ghế được chọn, highlight |
| Success Green | `#90EE90` | Ghế trống, thành công |
| Warning Red | `#ff4444` | Giá tiền, ghế đã đặt, lỗi |
| Dark Gray | `#333333` | Text chính |
| Light Gray | `#f8f9fa` | Background nhẹ |
| Border | `#e0e0e0` | Đường kẻ |

### Color Usage in Seat Selection
```
Ghe Trong (Available):    #90EE90 (Xanh lá)
Ghe Da Chon (Selected):   #FFA500 (Cam)
Ghe Da Dat (Booked):      #CCCCCC (Xam)
```

---

## 🔤 Typography

| Element | Font | Size | Weight |
|---------|------|------|--------|
| H1 (Hero) | System Font | 3rem | 700 |
| H2 (Section) | System Font | 2rem | 700 |
| H3 (Card) | System Font | 1.3rem | 600 |
| H4 (Label) | System Font | 0.95rem | 600 |
| Body | System Font | 1rem | 400 |
| Small | System Font | 0.9rem | 400 |

Font Stack: `-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif`

---

## 📦 Component Structure

### Layout Components
```
MainLayout.razor          (Khung layout chính)
  ├─ NavMenu.razor       (Thanh điều hướng cố định)
  ├─ @Body               (Nội dung trang)
  └─ Footer.razor        (Chân trang)
```

### Page Components
```
Pages/
├─ Home.razor            (Trang chủ + Tìm kiếm)
├─ SeatSelection.razor   (Chọn chỗ ngồi) ⭐ CỐT LÕI
├─ TrainList.razor       (Danh sách chuyến tàu)
└─ Checkout.razor        (Thanh toán)
```

### Reusable Components
```
TrainCard.razor          (Thẻ hiển thị chuyến tàu)
```

---

## 📄 Page Details

### 1. Trang Chủ (Home.razor)

#### Sections:
1. **Hero Banner**
   - Gradient xanh dương (135deg từ #003d7a → #0077cc)
   - SVG wave pattern overlay
   - Tiêu đề lớn + subtitle
   - Chiều cao: 400px (Desktop), 300px (Mobile)

2. **Search Form Card**
   - Glassmorphism style: Backdrop blur + semi-transparent white
   - Bóng mềm (box-shadow: 0 8px 32px rgba(...))
   - Form 4 hàng:
     - Hàng 1: Ga đi + Ga đến (Select)
     - Hàng 2: Ngày đi + Số hành khách (Date + Number)
     - Hàng 3: Loại ghế (Select) + Nút "Tìm vé ngay"
   - Nút tìm: Gradient xanh (#003d7a → #0055aa)

3. **Features Section (6 cards)**
   - Grid 3 cột (Desktop) → 1 cột (Mobile)
   - Hover: Border xanh + Shadow + translateY(-8px)
   - Icons emoji: ✈️, 🔒, 💰, 🎫, 📞, 🔄

4. **Promo Section (3 cards)**
   - Background: Gradient nhạt xanh (#f0f5ff → #e6f0ff)
   - Border-left: 4px solid #003d7a
   - Discount highlight màu đỏ

5. **Popular Routes (4 cards)**
   - Header gradient xanh
   - Hover: translateY(-8px) + shadow

---

### 2. Trang Chọn Chỗ Ngồi (SeatSelection.razor) ⭐

#### Sections:
1. **Header**
   - Gradient xanh + Box shadow
   - Trip info: Ga đi → Ga đến
   - Nút quay lại

2. **Coach Selector (Flexible tabs)**
   - Hiển thị 4 toa
   - Active state: Gradient xanh + white text
   - Scroll ngang trên Mobile

3. **Coach Info Stats**
   - 3 stats: Trong (Xanh), Đã chọn (Cam), Đã đặt (Xám)
   - Có color indicator 20x20px

4. **Seat Grid (CỐT LÕI)**
   ```
   Grid Layout:
   [ Seat A1 ][ Seat A2 ] [AISLE] [ Seat A3 ][ Seat A4 ]
   [ Seat B1 ][ Seat B2 ] [AISLE] [ Seat B3 ][ Seat B4 ]
   ...
   
   Seat Size: 50x50px (Desktop), 40x40px (Mobile)
   Gap: 0.8rem
   Border-radius: 8px
   ```

   **Seat States:**
   - Available: bg #90EE90, cursor pointer, hover scale 1.05 + shadow
   - Selected: bg #FFA500, white text, box-shadow
   - Booked: bg #CCCCCC, disabled, opacity 0.6

5. **Booking Summary (Sticky)**
   - Position sticky: top 100px
   - Card: white bg + shadow + rounded
   - Sections:
     - Ghế đã chọn (Editable list)
     - Loại vé (Radio: Thường/VIP)
     - Bảng tính giá (Với VIP 1.3x)
     - Input voucher
     - Nút "Tiếp tục thanh toán"

---

### 3. Trang Danh Sách Chuyến Tàu (TrainList.razor)

#### Sections:
1. **Header**
   - "Kết quả tìm kiếm" + số chuyến
   - Nút "Thay đổi tìm kiếm"

2. **Filter Sidebar (Sticky)**
   - Khung giờ (Morning, Afternoon, Evening, Night)
   - Loại ghế (Thường, VIP, Nằm)
   - Phạm vi giá (Slider)
   - Đánh giá (5 sao, 4+ sao, 3+ sao)
   - 2 nút: "Áp dụng" + "Đặt lại"

3. **Sort Bar**
   - 4 nút: Giờ khởi hành, Giá, Thời gian hành trình, Đánh giá
   - Active button: Gradient xanh

4. **Train Cards (List)**
   - Sử dụng component TrainCard.razor
   - Xếp dọc, gap 1.5rem

5. **Pagination**
   - Nút số trang
   - Active: Gradient xanh, white text

---

### 4. Trang Thanh Toán (Checkout.razor)

#### Sections:
1. **Progress Steps**
   - 3 bước: Chọn vé → Thanh toán → Xác nhận
   - Step icons 40x40px
   - Completed: xanh lá, Active: white bg + xanh text

2. **Passenger Info Form**
   - Các trường: Họ tên, Email, SĐT, CMND, Địa chỉ
   - Validation: Focus highlight #f0f5ff

3. **Voucher Input**
   - Input + Nút "Áp dụng"
   - Success message: xanh, Error: đỏ

4. **Payment Methods (4 options)**
   - Radio buttons + icons
   - Hover: border #003d7a, bg #f0f5ff
   - Loại:
     - 💳 Ví hệ thống (Hiển thị số dư)
     - 🏦 VNPay
     - 💰 Thẻ tín dụng
     - 🏧 Chuyển khoản

5. **Order Summary (Sticky)**
   - Trip info
   - Seats info
   - Price breakdown table
   - Warning box (nếu số dư không đủ)
   - Nút xác nhận (Disabled nếu không đồng ý điều khoản)

---

## 🎯 Key UI Patterns

### Button Styles
```css
/* Primary Button */
background: linear-gradient(135deg, #003d7a 0%, #0055aa 100%);
color: white;
padding: 0.8rem 1.5rem;
border-radius: 8px;
transition: all 0.3s ease;

/* Secondary Button */
background: #f0f0f0;
color: #333;
border: 1px solid #ddd;
```

### Input Fields
```css
border: 2px solid #e0e0e0;
border-radius: 8px;
padding: 0.8rem;
transition: all 0.3s ease;

&:focus {
  outline: none;
  border-color: #003d7a;
  background: #f0f5ff;
  box-shadow: 0 0 8px rgba(0, 61, 122, 0.2);
}
```

### Cards
```css
background: white;
border-radius: 12px;
padding: 2rem;
box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
border: 1px solid #e0e0e0;
transition: all 0.3s ease;

&:hover {
  box-shadow: 0 8px 24px rgba(0, 61, 122, 0.2);
  transform: translateY(-4px);
}
```

### Gradients
```css
/* Primary Gradient */
background: linear-gradient(135deg, #003d7a 0%, #005fa3 100%);

/* Light Gradient Background */
background: linear-gradient(135deg, #f0f5ff 0%, #e6f0ff 100%);
```

---

## 📱 Responsive Breakpoints

| Device | Width | Breakpoint |
|--------|-------|-----------|
| Desktop | > 1024px | N/A |
| Tablet | 768px - 1024px | max-width: 1024px |
| Mobile | < 768px | max-width: 768px |
| Small Mobile | < 480px | max-width: 480px |

### Key Changes:
- **Navigation**: Desktop menu → Mobile hamburger menu
- **Grid**: Multi-column → Single column
- **Font sizes**: Reduced 10-15%
- **Padding/Margin**: Reduced 20-30%
- **Seat grid**: 50x50px → 40x40px

---

## 🔧 CSS Custom Properties

Sử dụng CSS Variables toàn cục trong MainLayout.razor:

```css
:root {
  --color-primary: #003d7a;
  --color-primary-light: #005fa3;
  --color-accent: #FFA500;
  --color-success: #90EE90;
  --color-warning: #ff4444;
  --color-dark: #333;
  --color-light: #f8f9fa;
  --color-border: #e0e0e0;
  --border-radius: 8px;
  --transition: all 0.3s ease;
}
```

---

## 🎬 Animations

### Transitions
- Default: `all 0.3s ease`
- Button hover: `transform: translateY(-2px)` + shadow increase
- Card hover: `translateY(-4px) + box-shadow`

### Keyframe Animations
```css
/* Slide Down - Hero */
@keyframes slideDown {
  from { opacity: 0; transform: translateY(-30px); }
  to { opacity: 1; transform: translateY(0); }
}

/* Slide Up - Search Card */
@keyframes slideUp {
  from { opacity: 0; transform: translateY(30px); }
  to { opacity: 1; transform: translateY(0); }
}
```

---

## 📝 Naming Conventions

### CSS Classes
- BEM Methodology: `.block__element--modifier`
- Examples:
  - `.train-card`
  - `.train-card__header`
  - `.train-card__header--active`

### Component Names
- PascalCase: `Home.razor`, `SeatSelection.razor`
- Descriptive: `TrainCard.razor`, `MainLayout.razor`

---

## 🚀 Files Created

```
ProjectTrainWeb/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor      ✅ Cập nhật
│   │   ├── NavMenu.razor         ✅ Cập nhật
│   │   └── Footer.razor          ✅ Tạo mới
│   ├── Pages/
│   │   ├── Home.razor            ✅ Cập nhật
│   │   ├── SeatSelection.razor   ✅ Tạo mới (CỐT LÕI)
│   │   ├── TrainList.razor       ✅ Tạo mới
│   │   └── Checkout.razor        ✅ Tạo mới
│   └── TrainCard.razor           ✅ Tạo mới
└── wwwroot/
    └── app.css                   (Có sẵn)
```

---

## 🎓 Notes for Developers

1. **Icons**: Sử dụng emoji cho tính đơn giản và hiệu suất
2. **Glassmorphism**: Dùng `backdrop-filter: blur()` + semi-transparent background
3. **Sticky Elements**: Order summary, filter sidebar sticky top 100px (navbar height)
4. **Z-index**: 
   - Navbar: 100
   - Dropdowns: 101
   - Error UI: 105
5. **Dark Mode**: Có thể extend CSS variables để hỗ trợ `prefers-color-scheme`

---

## 📞 Future Enhancements

1. **Dark Mode**: Tạo theme tối
2. **Animations**: Thêm motion-safe alternatives
3. **Accessibility**: ARIA labels, semantic HTML
4. **i18n**: Hỗ trợ đa ngôn ngữ
5. **Progressive Web App**: Service workers, offline support
6. **Real-time Updates**: SignalR cho cập nhật chuyến tàu

---

**Version**: 1.0  
**Last Updated**: 2026-06-04  
**Author**: UI/UX Design Team
