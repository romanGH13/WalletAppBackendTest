
using Microsoft.EntityFrameworkCore;
using WalletAppBackend.Entities;
using WalletAppBackend.Persistence;
using WalletAppBackend.Repositories;
using WalletAppBackend.Services;

namespace WalletAppBackend
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

            builder.Services.AddDbContext<MainDbContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString"))
            );

            builder.Services.AddScoped<IMainDbContext, MainDbContext>();

            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

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