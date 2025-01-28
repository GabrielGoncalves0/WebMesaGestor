using Microsoft.EntityFrameworkCore;
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
builder.Services.AddScoped<ICaixaRepository, CaixaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IGrupoOpcaoRepository, GrupoOpcaoRepository>();
builder.Services.AddScoped<IMesaRepository, MesaRepository>();
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IOpcaoRepository, OpcaoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IOpcProPedRepository, OpcProPedRepository>();
builder.Services.AddScoped<IProPedRepository, ProPedRepository>();

// Services
builder.Services.AddScoped<CaixaService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<GrupoOpcaoService>();
builder.Services.AddScoped<MesaService>();
builder.Services.AddScoped<SetorService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<OpcaoService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<OpcaoProPedService>();

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
