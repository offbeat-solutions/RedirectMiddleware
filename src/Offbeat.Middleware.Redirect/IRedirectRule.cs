using Microsoft.AspNet.Http;

namespace Offbeat.Middleware {
	public interface IRedirectRule {
		bool Evaluate(HttpContext context);
	}
}
