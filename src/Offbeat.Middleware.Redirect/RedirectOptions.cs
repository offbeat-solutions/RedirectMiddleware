using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Offbeat.Middleware {
	public sealed class RedirectOptions {
		private IList<IRedirectRule> rules = new List<IRedirectRule>();

		internal RedirectOptions() {
		}

		public DomainMappingRule WhenAuthorityStartsWith(string source) {
			var rule = new DomainMappingRule(source);
			rules.Add(rule);
			return rule;
		}

		public DomainMappingRule WhenAuthorityIs(string source) {
			var rule = new DomainMappingRule(source, DomainMatchType.Exact);
			rules.Add(rule);
			return rule;
		}

		internal bool Evaluate(HttpContext httpContext) {
			foreach (var rule in rules) {
				if (rule.Evaluate(httpContext)) {
					return true;
				}
			}
			return false;
		}
	}
}
