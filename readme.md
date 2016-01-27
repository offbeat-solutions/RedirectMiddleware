RedirectMiddleware
==================

*version 0.2*

This is an ASP.NET Core middleware that will redirect one 
host to another host. It can be used to remove www 
(or add www) to the beginning of hostnames, for examples. 

If you move to another host, you can also redirect the old 
host to the new host.

If you add multiple hosts that should all redirect to a single
host, you can use this redirection middleware.

The redirection can match the hostname and port number as 
well (authority).

The redirection will also retain the path of the request.

Usage
-----
Include the middleware package/assembly in your project. After that, in your startup file configure
a new middleware as follows:

```C#
app.UseRedirectMiddleware(opt => {
	opt.WhenAuthorityIs("www.example.com").ThenRedirectTo("example.com").Temporary();
});
```

This will redirect all request for 
www.example.com to example.com.

```C#
app.UseRedirectMiddleware(opt => {
	opt.WhenAuthorityIs("www.example.com").ThenRedirectTo("example.com").Temporary();
	opt.WhenAuthorityIs("www.old-example.com").ThenRedirectTo("example.com");
	opt.WhenAuthorityIs("old-example.com").ThenRedirectTo("example.com");
	opt.WhenAuthorityStartsWith("my-example").ThenRedirectTo("example.com");
});
```

This will add other rules to redirect our old domain to the 
new domain. To it will redirect also www.old-example.com to
example.com. The last rule will redirect anything starting 
with *my-example* to example.com. So this will match:

* my-example.com
* my-example.org
* my-example.net:6789

Where to add it
---------------
It is best to add the middle quite early in the 
middleware stack to prevent unnecesary processing 
of the request.


Domain matching
---------------
You can either match the domain or you can have redirect 
if the domain starts with a certain string. Use 
`WhenAuthorityIs()` if you want to test for 
equality. Use `WhenAuthorityStartsWith()` if you want 
to match only the beginning of the string.

Remember the domain names might also contain port numbers!


Temporal vs permanent redirect
------------------------------
By default your redirects will be permanent redirects, 
so the middleware will return 301. 
To make them temporal redirects, use `Temporary()` like 
in the above example.
