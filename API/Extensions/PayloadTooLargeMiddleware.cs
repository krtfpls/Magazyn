// namespace API.Extensions
// {
//    public class PayloadTooLargeMiddleware
// {
//     private readonly RequestDelegate _next;
//       private readonly ILogger<ExceptionMiddleware> _logger;

//     public PayloadTooLargeMiddleware(RequestDelegate next,  ILogger<ExceptionMiddleware> logger)
//     {
//         _next = next;
//         _logger = logger;
//     }

//     public async Task Invoke(HttpContext context)
//     {
//         try
//         {
//             await _next(context);
//         }
//         catch (Exception ex)
//         {
//             _logger.LogError(ex, ex.Message);
//             context.Response.StatusCode = 413;
//             await context.Response.WriteAsync("Payload size limit exceeded");
//         }
//     }
// }

// }