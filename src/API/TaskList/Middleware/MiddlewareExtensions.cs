namespace TaskList.Middleware
{
    /// <summary>
    /// Static class for adding custom middleware to the application pipeline.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the <see cref="ExceptionHandlerMiddleware"/> to the application pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }        
    }
}
