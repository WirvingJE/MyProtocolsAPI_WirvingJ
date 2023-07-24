using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_WirvingJ.Models;

namespace MyProtocolsAPI_WirvingJ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //vamos a leer la etiqueta CNNSTR de appsetting. json para configurar la connecona la base de datos 

            var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));
            
            //elim
            CnnStrBuilder.Password = "123456";
            CnnStrBuilder.IntegratedSecurity = true;


            string cnnStr = CnnStrBuilder.ConnectionString;

            builder.Services.AddDbContext<MyProtocolsDBContext>(options => options.UseSqlServer(cnnStr));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}