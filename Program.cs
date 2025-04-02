using Microsoft.EntityFrameworkCore;
using MiniValidation;
using NotaAPI.Data;
using NotaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

var app = builder.Build();

app.MapPost("/notas", async (Nota nota, AppDbContext db) =>
{
    if (!MiniValidator.TryValidate(nota, out var errors))
        return Results.ValidationProblem(errors);

    db.Notas.Add(nota);
    await db.SaveChangesAsync();
    return Results.Created($"/notas/{nota.Id}", nota);
});

app.MapGet("/notas", async (AppDbContext db) => await db.Notas.ToListAsync());

app.MapGet("/notas/{id}", async (Guid id, AppDbContext db) =>
{
    var nota = await db.Notas.FindAsync(id);
    return nota is not null ? Results.Ok(nota) : Results.NotFound();
});

app.MapPut("/notas/{id}", async (Guid id, Nota input, AppDbContext db) =>
{
    var nota = await db.Notas.FindAsync(id);
    if (nota is null) return Results.NotFound();

    nota.Aluno = input.Aluno;
    nota.Disciplina = input.Disciplina;
    nota.Valor = input.Valor;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/notas/{id}", async (Guid id, AppDbContext db) =>
{
    var nota = await db.Notas.FindAsync(id);
    if (nota is null) return Results.NotFound();

    db.Notas.Remove(nota);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
