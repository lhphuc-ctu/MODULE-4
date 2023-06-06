﻿namespace OutputCaching
{
    public static class Gravatar
    {
        public static async Task WriteGravatar(HttpContext context)
        {
            const string type = "monsterid";
            const int size = 200;
            var hash = Guid.NewGuid().ToString("n");

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";

            await context.Response.WriteAsync($"<img src=\"https://www.gravatar.com/avatar/{hash}?s={size}&d={type}\"/>");
            await context.Response.WriteAsync($"<pre>Generated at {DateTime.Now:hh:mm:ss:ff}</pre>");
        }
    }
}