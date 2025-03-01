using Microsoft.EntityFrameworkCore;
using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_DataAccess.Repository;
using SampleWebApplication_DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// ASP.NET Core�A�v���P�[�V�����̃T�[�r�X�R���e�i�ɃR���g���[���ƃr���[�̃T�[�r�X��ǉ�
builder.Services.AddControllersWithViews();

// SQL Server�̐ڑ����`
builder.Services.AddDbContext<ApplicationDbContext>
    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Core�̈ˑ��������R���e�i��
// IUnitOfWork �Ƃ��̎����N���X UnitOfWork ���X�R�[�v�t���T�[�r�X�Ƃ��ēo�^
// �X�R�[�v�t���T�[�r�X(HTTP���N�G�X�g���ƂɐV�����C���X�^���X���쐬���A���̃��N�G�X�g�̊Ԃ�������)
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
