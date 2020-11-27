---
title: Happy LibYear!
description: A dotnet CLI tool for managing dependency freshness
date: 2017-03-20
comments: https://twitter.com/stevedesmond_ca/status/843845328810004480
---

Fellow Ithaca NY developer, and [local meetup](https://www.meetup.com/ithaca-web-people/) co-host [Jared Beck](https://jaredbeck.com/) recently came up with a great way to quantify a much-ignored but easily-resolvable facet of technical debt: managing outdated dependencies.

Like NDepends new "Smart Technical Debt Estimation" feature,
[libyear](https://libyear.com/) provides a simple, calculated number to
help contextualize the cost of well-maintained software.

[![libyear](/assets/blog/libyear.png)](https://libyear.com)

<figcaption>Image courtesy of libyear.com</figcaption>

Since today is the first day of spring, and therefore a new year for anyone in the northern hemisphere who prefers to follow the natural patterns of the Earth as opposed to the man-made arbitrarity of the Gregorian calendar, I figured I should celebrate by releasing my .NET implementation of libyear!

It runs against [NuGet.org](https://nuget.org), supporting both .NET Core's csproj and project.json files, as well as packages.config if you're still using .NET Classic. Yes, I'm calling it that from now on.

**Updated 7/22/2018:** You can now download dotnet libyear 2.0 for .NET Core 2.1 by running:

`dotnet tool install -g libyear`

From there, simply run `dotnet libyear {csproj | dir}` to list dependencies for a given project or directory (or for the current directory if no argument is given).

Lets see what that looks like for [this site's code](https://github.com/stevedesmond-ca/stevedesmond.ca):

```~/code/scratch$ dotnet libyear ~/code/www
~/code/www/Test/Test.csproj
Package                           Installed                   Released    Latest  Released    Age (y)
Microsoft.NET.Test.Sdk            15.0.0-preview-20170106-08  2017-01-06  15.0.0  2017-02-23  0.1
Selenium.WebDriver                3.1.0                       2017-02-16  3.3.0   2017-03-07  0.1
Selenium.WebDriverBackedSelenium  3.1.0                       2017-02-16  3.3.0   2017-03-07  0.1
xunit                             2.2.0                       2017-02-19  2.2.0   2017-02-19  0.0
xunit.runner.visualstudio         2.2.0                       2017-02-19  2.2.0   2017-02-19  0.0
Project is 0.2 libyears behind
```

You can see that since I passed a directory with more than one project, it found them all recursively and displayed the total at the bottom.

If you look closely at the test projects results, you can see that my 2 xunit packages are up-to-date. If I had run it with a `-q` or `--quiet` flag, it would have only displayed the outdated ones, omitting those that are up-to-date -- useful for large projects with many dependencies, or solutions with many projects, to be able to focus in on the problem areas!

Theres also a `-u` or `--update` flag to update all dependencies to the latest version. I can see this being really useful for some [Greenkeeper](https://greenkeeper.io/)-type scenarios.

"Wait a minute," you might be saying to yourself, perhaps out loud, despite being the only one in the room, "doesnt Visual Studio already have all this functionality, under **Manage NuGet Packages**?" And youd be right, for the most part it does. But this is cross-platform. And runs from the command-line. And is 15KB, not 15GB. Options!

Now, I know that these sorts of simplifications are ripe for debate due to their potential abuse by those-who-shall-not-be-named less-technical number-cruncher types, but as shown in the cartoon at the top, its at the very least a good first step in getting useful metrics as a way to inform decision-making processes.

You can find the code for [dotnet-libyear on GitHub here](https://github.com/stevedesmond-ca/dotnet-libyear), or check out Jared's [npm](https://github.com/jaredbeck/libyear-npm) and
[bundler](https://github.com/jaredbeck/libyear-bundler) packages!