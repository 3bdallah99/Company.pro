using Company.pro.BLL;
using Company.pro.BLL.Interfaces;
using Company.pro.BLL.Repositories;
using Company.pro.DAL.Data.Contexts;
using Company.pro.PL.Mapping;
using Company.pro.PL.Services;
using Microsoft.EntityFrameworkCore;

namespace Company.pro.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Built-In MVC Services 
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // Allow DI For DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); // Allow DI For CompanyDbContext  
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddScoped<IScopedService,ScopedService>(); //Per Requset
            builder.Services.AddScoped<ITransentService,TransentService>(); //Per Operation 
            builder.Services.AddScoped<ISengeltonService,SengeltonService>(); // Per App
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
        }
    }
}
