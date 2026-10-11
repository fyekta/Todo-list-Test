namespace App.WebApi.Middlewares
{

    public class GetOnlyMiddleware
    {
        private readonly RequestDelegate _next;

        public GetOnlyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Get)
            {
                context.Response.StatusCode =
                    StatusCodes.Status405MethodNotAllowed;

                return;
            }

            await _next(context);
        }
    }

}
