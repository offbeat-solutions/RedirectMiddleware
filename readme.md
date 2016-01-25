RedirectMiddleware
==================

This is an ASP.NET Core middleware that will redirect one host to another host. It can be used to 
remove www (or add www) to the beginning of hostnames, for examples. If you move to another host,
you can also redirect the old host to the new host.

The redirection will retain the path of the request.

version 0.1.


Usage
-----
Include the middleware package/assembly in your project. After that, in your startup file configure
a new middleware as follows:

```
app.UseRedirectMiddleware(opt => {
	opt.IfDomainEquals("www.example.com").ThenMapTo("example.com").AsTemporalRedirect();
});
```

This will redirect all request for www.example.com to example.com.

```
app.UseRedirectMiddleware(opt => {
	opt.IfDomainEquals("www.example.com").ThenMapTo("example.com").AsTemporalRedirect();
	opt.IfDomainEquals("www.old-example.com").ThenMapTo("example.com");
	opt.IfDomainEquals("old-example.com").ThenMapTo("example.com");
});
```

This will add other rules to redirect our old domain to the new domain.

It is best to add if quite early in the middleware stack to prevent unnecesary processing of the
request.


Domain matching
---------------
You can either match the domain or you can have redirect if the domain starts with a certain string.
Use `IfDomainEquals()` if you want to test for equality. Use `IfDomainStartsWith()` if you want to match 
only the beginning of the string.

Remember the domain names might also contain port numbers!


Temporal vs permanent redirect
------------------------------
By default your redirects will be permanent redirects, so the middleware will return 301. To make them 
temporal redirects, use `AsTemporalRedirect()` like in the above example.
