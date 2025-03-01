using Microsoft.EntityFrameworkCore;
using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_DataAccess.Repository;
using SampleWebApplication_DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// ASP.NET Coreアプリケーションのサービスコンテナにコントローラとビューのサービスを追加
builder.Services.AddControllersWithViews();

// SQL Serverの接続を定義
builder.Services.AddDbContext<ApplicationDbContext>
    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Coreの依存性注入コンテナに
// IUnitOfWork とその実装クラス UnitOfWork をスコープ付きサービスとして登録
// スコープ付きサービス(HTTPリクエストごとに新しいインスタンスを作成し、そのリクエストの間だけ存続)
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
