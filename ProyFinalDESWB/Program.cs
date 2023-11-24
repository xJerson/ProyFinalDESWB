using ProyFinalDESWB.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<ConsultoresDAO>();
builder.Services.AddScoped<EmpleadoDao>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "Empleados",
    pattern: "{controller=Empleado}/{action=ListadoEmpleados}/{id?}");

/*
app.MapControllerRoute(
    name: "Consultores",
    pattern: "{controller=Consultor}/{action=ListarConsultores}/{id?}");
*/
/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=cliente}/{action=GrabarCliente}/{id?}");
*/
/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/
app.Run();