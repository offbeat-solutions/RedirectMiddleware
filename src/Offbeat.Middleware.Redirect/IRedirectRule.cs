using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Offbeat.Middleware
{
	public interface IRedirectRule {
		bool Evaluate(HttpContext context);
	}
}
