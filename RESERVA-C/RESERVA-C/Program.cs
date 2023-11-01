using Microsoft.EntityFrameworkCore;
using RESERVA_C.Data;

namespace RESERVA_C
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //agregar servicio, prueba 01 Zodda
            // builder.Services.AddDbContext<ReservaContext>(options => options.UseInMemoryDatabase("ReservaDb"));
            builder.Services.AddDbContext<ReservaContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("CineDB2CG5"))
                     );

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