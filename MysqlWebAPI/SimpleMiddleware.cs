namespace MysqlWebAPI
{
    public class SimpleMiddleware
    {
        private RequestDelegate _next;
        public SimpleMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.ToString();
            context.Response.Headers.Add("custom-header", path);
            if (path.Contains("GetUserById"))
            {
                await context.Response.WriteAsync("http://alx/pl/");
            }
            else 
            {
                await _next(context);
            }
            
        }
    }
}
