using E_leaening_System.Models;
using E_leaening_System.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyData>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("mypolicy", policy => policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MyData>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;// token not expire
    options.RequireHttpsMetadata = false; //token in http or https
    options.TokenValidationParameters = new TokenValidationParameters() // check parameter token valid or not 
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudiance"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program)); //auto mapper
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

//builder.Services.AddScoped<IRepository<Course>, Repository<Course>>();
builder.Services.AddScoped<IRepository<Instructor>, Repository<Instructor>>();
builder.Services.AddScoped<IRepository<Student>, Repository<Student>>();
builder.Services.AddScoped<IRepository<Admin>, Repository<Admin>>();

builder.Services.AddScoped<IRepository<Certificate>, Repository<Certificate>>();
builder.Services.AddScoped<IRepository<Quiz>, Repository<Quiz>>();

builder.Services.AddScoped<IRepository<TheQuizzes>, Repository<TheQuizzes>>();
builder.Services.AddScoped<IRepository<Content>, Repository<Content>>();
builder.Services.AddScoped<IRepository<Quiz>, Repository<Quiz>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("mypolicy");
//app.UseAuthentication();// check token
//app.UseAuthorization();

app.MapControllers();


app.Run();
