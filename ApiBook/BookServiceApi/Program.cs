using BookServiceApi.extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:8081") //modificar con tu puerto local
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddHttpClientExtensions(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "challenge Book V1"));


app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
