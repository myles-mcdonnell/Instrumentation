!! When first building; 

   1) Build (will fail due to PostSharp package missing), 
   2) Restart VS,
   3) Build (will succeed)  !!

Tooling:

VS2010 (PostSharp not playing nicely with 2012 when debugging unit tests)
Resharper 7
PostSharp
NuGet

Stack:

PostSharp
MOQ
Nunit
FluentAssertions
SmartThreadPool
