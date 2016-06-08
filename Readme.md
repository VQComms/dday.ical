# DDay.iCal

## How to build

Build `antlr/antlr.net-runtime.sln`

Build `DDay.iCal.sln` 

## CoreCLR

Run `nuget pack antlr.runtime.DotNet.nuspec`
Run `nuget pack DDay.Collections.DotNet.nuspec`
Run `nuget pack DDay.iCal.DotNet.nuspec`

Publish the resulting *.nupkg to a nuget feed or use your filesystem as a nuget feed.

In your consuming CoreCLR app add the following (replacing any relevant version numbers where needed):

```
    "dependencies": {
        "DDay.iCal-netcore": "1.1.0-alpha",
     }
```

## Original README

DDay.iCal is an iCalendar (RFC2445) class library for .NET 2.0 and
above. It's aimed at providing full RFC 2445 compliance, while providing
full compatibility with popular calendaring applications.

===============================================================

To get started developing with DDay.iCal, simply download the latest binary
release, add a reference to DDay.iCal.dll within your project, and you can
begin using it.  For examples, see the example projects included with the
binary or source distributions.

===============================================================

If you'd like to contribute to DDay.iCal, please let me know.  To compile
the parsing engine from source, you will need to download and install ANTLR
version 2.7.6 from antlr.org, and add the file antlr-2.7.6.exe to your
PATH environment variable. However, the majority of DDay.iCal development
does not require ANTLR.

If you have any questions, please e-mail me: doug@ddaysoftware.com
