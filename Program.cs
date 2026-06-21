using ProjectTrainWeb.Components;
using ProjectTrainWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Dang ky TrainBookingService vao DI container (Singleton = du lieu ton tai suot vong doi ung dung)
builder.Services.AddSingleton<TrainBookingService>();

// Dang ky cac Service con (OOP - tach trach nhiem theo Single Responsibility Principle)
// Moi service inject TrainBookingService de truy cap du lieu chung
builder.Services.AddScoped<NguoiDungService>();   // Dang nhap, dang ky, nap tien
builder.Services.AddScoped<ChuyenTauService>();   // CRUD chuyen tau, kiem tra ghe
builder.Services.AddScoped<DatVeService>();       // Dat ve (da hinh VeThuong/VeVIP), huy ve
builder.Services.AddScoped<ThanhToanService>();   // Thanh toan, qua han, khuyen mai
builder.Services.AddScoped<ThongKeService>();     // Thong ke doanh thu (pattern matching)
builder.Services.AddScoped<ThongBaoService>();    // Quan ly thong bao
builder.Services.AddSingleton<ThanhToanDuyetService>(); // Duyet thanh toan + QR + cau hinh he thong

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
