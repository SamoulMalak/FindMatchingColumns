using FindMatchingColumns.BL.IServices;
using FindMatchingColumns.BL.Services;
using FindMatchingColumns.Data.IRepository;
using FindMatchingColumns.Data.Repository;

namespace FindMatchingColumns.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connection = builder.Configuration.GetConnectionString("Default");
            #region Register Repository 
            builder.Services.AddSingleton<IPolicyReopsitory>
                (
                reg=>
                {
                    return new PolicyReopsitory(connection);
                }
                );
            #endregion

            #region Register Services
            builder.Services.AddScoped<IMatchingServices>(
                reg =>
                {
                    return new MatchingServices(connection);
                });
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}