using Microsoft.AspNetCore.StaticFiles;

// Configure services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHsts(options =>
{
    //options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromSeconds(31536000);
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin()));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/exception");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStatusCodePagesWithReExecute("/error/{0}");

FileExtensionContentTypeProvider mimeProvider = new();

if (app.Environment.IsDevelopment())
{
    mimeProvider.Mappings[".less"] = "text/prs.less";
    mimeProvider.Mappings[".ts"] = "text/prs.typescript";
}

app.UseStaticFiles(new StaticFileOptions()
{
    ContentTypeProvider = mimeProvider
});

app.UseRouting();

app.UseCors();

if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; frame-ancestors 'self'");
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
        await next();
    });
}

app.MapRazorPages();

app.MapControllers();

app.Run();
