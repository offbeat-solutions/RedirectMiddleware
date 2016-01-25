using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Offbeat.Middleware {
	/// <summary>
	/// Middleware to handle redirecting based on hostname
	/// </summary>
	public class RedirectMiddleware {
		private readonly RequestDelegate _next;
		private readonly RedirectOptions _options;

		public RedirectMiddleware(RequestDelegate next, RedirectOptions options) {
			_next = next;
			_options = options;
		}

		public async Task Invoke(HttpContext httpContext) {
			if (_options.Evaluate(httpContext)) {
				return;
			}
			else {
				await _next.Invoke(httpContext);
			}
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class RedirectMiddlewareExtensions {
		public static IApplicationBuilder UseRedirectMiddleware(this IApplicationBuilder builder, Action<RedirectOptions> configure) {
			var options = new RedirectOptions();
			configure(options);
			return builder.UseMiddleware<RedirectMiddleware>(options);
		}
	}
}
