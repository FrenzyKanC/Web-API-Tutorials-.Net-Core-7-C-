
using Web_API_Tutorials_.Net_Core_7_C_.MyLogging;

namespace Web_API_Tutorials_.Net_Core_7_C_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Added new libs: builder.Services.AddControllers().AddNewtonsoftJson();

            // added options: non supportet format request error: zb für xml obwohl nur json supportet wird
            // added: .AddXmlDataContractSerializerFormatters() damit xml supportet wird
            builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
            .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // dependency injection
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
