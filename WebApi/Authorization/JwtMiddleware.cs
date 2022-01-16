using WebApi.Services;

namespace WebApi.Authorization;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrEmpty(token))
        {
            var userId = jwtUtils.ValidateToken(token ?? string.Empty);
            // attach user to context on successful jwt validation
            context.Items["User"] = userId.HasValue ? userService.GetById(userId.Value) : null;
        }

        await _next(context);
    }
}