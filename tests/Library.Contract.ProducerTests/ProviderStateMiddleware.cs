using System.Text;
using System.Text.Json;
using Library.Models.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PactNet;

namespace Library.ContractTests;

public class ProviderStateMiddleware
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    private readonly IDictionary<string, Func<IDictionary<string, string>, Task>> _providerStates;
    private readonly RequestDelegate _next;
    
    public ProviderStateMiddleware(RequestDelegate next)
    {
        _next = next;

        _providerStates = new Dictionary<string, Func<IDictionary<string, string>, Task>>
        {
            ["A GET Request to get all books"] = this.EnsureEventExistsAsync
        };
    }
    
    private async Task EnsureEventExistsAsync(IDictionary<string, string> parameters)
    {
        //JsonElement id = (JsonElement)parameters["id"];

        //await this.orders.InsertAsync(new OrderDto(id.GetInt32(), OrderStatus.Fulfilling, DateTimeOffset.Now));
    }
    
    public async Task Invoke(HttpContext context)
    {
        if (!(context.Request.Path.Value?.StartsWith("/provider-states") ?? false))
        {
            await _next.Invoke(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status200OK;

        if (context.Request.Method == HttpMethod.Post.ToString())
        {
            string jsonRequestBody;

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                jsonRequestBody = await reader.ReadToEndAsync();
            }

            try
            {
                ProviderState providerState = JsonSerializer.Deserialize<ProviderState>(jsonRequestBody, Options);
                
                if (!string.IsNullOrEmpty(providerState?.State))
                {
                    await _providerStates[providerState.State].Invoke(providerState.Params);
                }
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Failed to deserialise JSON provider state body:");
                await context.Response.WriteAsync(jsonRequestBody);
                await context.Response.WriteAsync(string.Empty);
                await context.Response.WriteAsync(e.ToString());
            }
        }
    }
}

public class ProviderStateMiddlewareStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            builder.UseMiddleware<ProviderStateMiddleware>();
            next(builder);
        };
    }
}