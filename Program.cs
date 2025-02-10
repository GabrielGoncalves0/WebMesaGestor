using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebMesaGestor.Application.Mapping;
using WebMesaGestor.Application.Services;
using WebMesaGestor.Domain.Interfaces;
using WebMesaGestor.Infra.Data;
using WebMesaGestor.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuracao Banco
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var versao = ServerVersion.AutoDetect(connectionString);

    options.UseMySql(connectionString, versao);
});

// Contrato interface e Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
//builder.Services.AddScoped<ICaixaRepository, CaixaRepository>();
//builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
//builder.Services.AddScoped<IGrupoOpcaoRepository, GrupoOpcaoRepository>();
//builder.Services.AddScoped<IMesaRepository, MesaRepository>();
//builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
//builder.Services.AddScoped<IOpcaoRepository, OpcaoRepository>();
//builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
//builder.Services.AddScoped<IOpcProPedRepository, OpcProPedRepository>();
//builder.Services.AddScoped<IProPedRepository, ProPedRepository>();
//builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<SetorService>();
//builder.Services.AddScoped<CategoriaService>();
//builder.Services.AddScoped<MesaService>();
//builder.Services.AddScoped<CaixaService>();
//builder.Services.AddScoped<GrupoOpcaoService>();
//builder.Services.AddScoped<ProdutoService>();
//builder.Services.AddScoped<OpcaoService>();
//builder.Services.AddScoped<PedidoService>();
//builder.Services.AddScoped<OpcaoProPedService>();
//builder.Services.AddScoped<ProPedService>();
//builder.Services.AddScoped<TransacaoService>();
//builder.Services.TryAddScoped<ITokenRepository, TokenService>();

builder.Services.AddAutoMapper(typeof(UsuarioMapping));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Version = "v1",
        Title = "WebMesaGestor",
        Description = "API para gestão de mesas de restaurantes",
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "O cabeçalho de autorização JWT deve ser informado no formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
