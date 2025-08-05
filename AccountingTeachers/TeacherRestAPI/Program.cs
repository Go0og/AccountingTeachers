using Contracts.InteractorContract;
using Contracts.PresenterContract;
using Contracts.StorageContract;
using DataBaseImplements.Implements;
using Interactors;
using Presenters;

namespace TeacherRestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // -------STORAGE----------
            builder.Services.AddTransient<ITeacherStorage, TeacherStorage>();
            builder.Services.AddTransient<IUserStorage, UserStorage>();
            builder.Services.AddTransient<IDepartmentStorage, DeparmentStorage>();

            // ------INTERACTORS---------
            builder.Services.AddTransient<IteacherLogic, TeacherLogic>();
            builder.Services.AddTransient<IUserLogic, UserLogic>();
            builder.Services.AddTransient<IDepartmentLogic, DepartmensLogic>();

            // -----PRESENTERS----------

            builder.Services.AddTransient<ITeacherPresenter, TeacherPresenter>();
            builder.Services.AddTransient<IUserPresenter, UserPresenter>();
            builder.Services.AddTransient<IDepartmentPresenter, DepartmentPresenter>();

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
