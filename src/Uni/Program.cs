using FluentValidation;

using Microsoft.AspNetCore.StaticFiles;

using Uni.Controllers.Api;

using WebMarkupMin.AspNetCore7;

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

builder.Services.AddWebMarkupMin(options => options.DisablePoweredByHttpHeaders = true)
    .AddHtmlMinification();

builder.Services.AddScoped<IValidator<Book.Booking>, Book.Booking.Validator>();

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

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = mimeProvider
});

app.UseWebMarkupMin();

app.UseRouting();

app.UseCors();

if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        context.Response.Headers.TryAdd("Content-Security-Policy", "default-src 'self'; frame-ancestors 'self'");
        context.Response.Headers.TryAdd("X-Frame-Options", "SAMEORIGIN");
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Response.Headers.TryAdd("Referrer-Policy", "no-referrer-when-downgrade");
        await next();
    });
}

app.MapRazorPages();

app.MapControllers();

app.Run();
