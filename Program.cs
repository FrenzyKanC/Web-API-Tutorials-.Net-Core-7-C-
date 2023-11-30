
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorials_.Net_Core_7_C_.Data;
using Web_API_Tutorials_.Net_Core_7_C_.MyLogging;

namespace Web_API_Tutorials_.Net_Core_7_C_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // in-build loggers by default usable: debug, console
            var builder = WebApplication.CreateBuilder(args);
            // builder.Logging.ClearProviders(); löscht alle loggers
            // builder.Logging.AddConsole(); added die console zu den loggers

            // added db server con
            // import db and update: view -> other windows -> package manager console -> Add-Migration -> InitialDBSetup
            // danach Update-Database damit sie im ssms ist
            builder.Services.AddDbContext<CollegeDBContext>(options =>
            {
                // hard coded server connection via server string
                // options.UseSqlServer("Data Source=DESKTOP-TDL170A\\SQLEXPRESS;Initial Catalog=CollegeAppDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

                // server connection via appsettings.json avoiding hard coding
                options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDB"));
            });

            // Added new libs: builder.Services.AddControllers().AddNewtonsoftJson();
            // added options: non supportet format request error: zb für xml obwohl nur json supportet wird
            // added: .AddXmlDataContractSerializerFormatters() damit xml supportet wird
            builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
            .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // unterstütze web api dependency injections
            // added: <Constructor Injection, return instance of this>
            // return type muss angepasst werden: LogToFile, LogToDB...
            builder.Services.AddScoped<IMyLogger, LogToFile>();
            builder.Services.AddSingleton<IMyLogger, LogToFile>();
            builder.Services.AddTransient<IMyLogger, LogToFile>();

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
