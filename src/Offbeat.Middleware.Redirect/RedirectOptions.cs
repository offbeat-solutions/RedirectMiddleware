using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Offbeat.Middleware
{
    public sealed class RedirectOptions
    {
		private IList<IRedirectRule> rules = new List<IRedirectRule>();

		public DomainMappingRule IfDomainStartsWith(string source) {
			var rule = new DomainMappingRule(source);
			rules.Add(rule);
			return rule;
		}

		public DomainMappingRule IfDomainEquals(string source) {
			var rule = new DomainMappingRule(source, DomainMatchType.Exact);
			rules.Add(rule);
			return rule;
		}

		internal bool Evaluate(HttpContext httpContext) {
			foreach (var rule in rules) {
				if ( rule.Evaluate(httpContext)) {
					return true;
				}
			}
			return false;
		}
	}
}
