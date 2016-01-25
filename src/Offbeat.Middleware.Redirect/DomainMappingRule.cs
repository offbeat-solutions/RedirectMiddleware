using System;
using Microsoft.AspNet.Http;
using System.Text.RegularExpressions;

namespace Offbeat.Middleware {
	public class DomainMappingRule : IRedirectRule {
		private string sourceDomain;

		private string destinationDomain;

		private bool permanentRedirect;

		private DomainMatchType matchType = DomainMatchType.StartsWith;

		internal DomainMappingRule(string sourceDomain, DomainMatchType matchType = DomainMatchType.StartsWith ) {
			this.sourceDomain = sourceDomain;
			this.permanentRedirect = true;
			this.matchType = matchType;
		}

		public DomainMappingRule ThenMapTo(string domain) {
			if (Regex.IsMatch(domain, "^https?://") == false) {
				destinationDomain = $"http://{domain}";
			}
			else {
				destinationDomain = domain;
			}
			return this;
		}

		public DomainMappingRule AsPermanentRedirect() {
			this.permanentRedirect = true;
			return this;
		}

		public DomainMappingRule AsTemporalRedirect() {
			this.permanentRedirect = false;
			return this;
		}

		public bool Evaluate(HttpContext context) {
			if (MatchDomain(context) &&
				!context.Response.HasStarted) {

				var destinationUri = new Uri(destinationDomain);
				var originalpath = new Uri(context.Request.Path.Value, UriKind.Relative);
				var destinationUriAndPath = new Uri(destinationUri, originalpath);

				context.Response.Headers.Add("Location",
					new Microsoft.Extensions.Primitives.StringValues(
						destinationUriAndPath.ToString()));

				context.Response.StatusCode = permanentRedirect ? 301 : 302;

				return true;
			}
			return false;
		}

		private bool MatchDomain(HttpContext context) {
			if ( matchType == DomainMatchType.StartsWith ) {
				return context.Request.Host.ToString().StartsWith(sourceDomain, StringComparison.CurrentCultureIgnoreCase);
			}
			else if ( matchType == DomainMatchType.Exact) {
				return context.Request.Host.ToString().Equals(sourceDomain, StringComparison.CurrentCultureIgnoreCase);
			}

			throw new NotImplementedException();
		}
	}
	
	internal enum DomainMatchType {
		StartsWith,
		Exact
	}
}
