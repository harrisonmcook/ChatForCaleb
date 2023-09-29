
using Microsoft.AspNetCore.ResponseCompression;

using ChatService.Hubs;
using System.Reflection.Metadata.Ecma335;

namespace ChatService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
           
            builder.Services.AddRazorPages();
            //builder.Services.AddCors();
           builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder
                    //.WithOrigins("http://localhost:3000/")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((string origin)=> true)
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
                

                 
            });
            });
            builder.Services.AddSignalR();

            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
               
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.MapControllers();

            /*    app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:3000") //Source
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<ChatHub>("/chathub");
                });*/

            app.MapHub<ChatHub>("/chathub");
           
            app.Run();
        }
    }
}