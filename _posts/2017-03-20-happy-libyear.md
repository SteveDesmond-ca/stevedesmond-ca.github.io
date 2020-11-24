---
title: Happy LibYear!
description: A dotnet CLI tool for managing dependency freshness
date: 2017-03-20
comments: https://twitter.com/stevedesmond_ca/status/843845328810004480
---
<p>Fellow Ithaca NY developer, and <a href="https://www.meetup.com/ithaca-web-people/" target="_blank">local meetup</a> co-host <a href="https://jaredbeck.com/" target="_blank">Jared Beck</a> recently came up with a great way to quantify a much-ignored but easily-resolvable facet of technical debt: managing outdated dependencies.</p>
<p>Like NDepend's new "Smart Technical Debt Estimation" feature, <a href="https://libyear.com/" target="_blank">libyear</a> provides a simple, calculated number to help contextualize the cost of well-maintained software.</p>
<p class="text-center">
  <a href="https://libyear.com" target="_blank">
    <img src="/images/blog/libyear.png" alt="libyear"/>
    <br/>
    <caption>Image courtesy of libyear.com</caption>
  </a>
</p>
<p>Since today is the first day of spring, and therefore a new year for anyone in the northern hemisphere who prefers to follow the natural patterns of the Earth as opposed to the man-made arbitrarity of the Gregorian calendar, I figured I should celebrate by releasing my .NET implementation of libyear!</p>
<p>It runs against <a href="https://nuget.org" target="_blank">NuGet.org</a>, supporting both .NET Core's csproj and project.json files, as well as packages.config if you're still using .NET Classic. Yes, I'm calling it that from now on.</p>
<p><b>Updated 7/22/2018:</b> You can now download <kbd>dotnet libyear 2.0</kbd> for .NET Core 2.1 by running:</p>
<div class="code">dotnet tool install -g libyear</div>
<p>From there, simply run <kbd>dotnet libyear {csproj | dir}</kbd> to list dependencies for a given project or directory (or for the current directory if no argument is given).</p>
<p>Let's see what that looks like for <a href="https://github.com/stevedesmond-ca/stevedesmond.ca" target="_blank">this site's code</a>:
  <div class="code">~/code/scratch$ dotnet libyear ~/code/www
~/code/www/Test/Test.csproj
Package                                          Installed                    Released     Latest   Released     Age (y)
Microsoft.NET.Test.Sdk                           15.0.0-preview-20170106-08   2017-01-06   15.0.0   2017-02-23   0.1
Selenium.WebDriver                               3.1.0                        2017-02-16   3.3.0    2017-03-07   0.1
Selenium.WebDriverBackedSelenium                 3.1.0                        2017-02-16   3.3.0    2017-03-07   0.1
xunit                                            2.2.0                        2017-02-19   2.2.0    2017-02-19   0.0
xunit.runner.visualstudio                        2.2.0                        2017-02-19   2.2.0    2017-02-19   0.0
Project is 0.2 libyears behind

~/code/www/Web/Web.csproj
Package                                          Installed                    Released     Latest   Released     Age (y)
MailKit                                          1.10.2                       2017-01-28   1.12.0   2017-03-12   0.1
Microsoft.ApplicationInsights.AspNetCore         1.0.2                        2016-09-28   2.0.0    2017-01-24   0.3
Microsoft.AspNetCore.Diagnostics                 1.1.0                        2016-11-15   1.1.1    2017-03-06   0.3
Microsoft.AspNetCore.Mvc                         1.1.1                        2017-01-26   1.1.2    2017-03-06   0.1
Microsoft.AspNetCore.Mvc.Core                    1.1.1                        2017-01-26   1.1.2    2017-03-06   0.1
Microsoft.AspNetCore.Server.Kestrel              1.1.0                        2016-11-15   1.1.1    2017-03-06   0.3
Microsoft.AspNetCore.StaticFiles                 1.1.0                        2016-11-15   1.1.1    2017-03-06   0.3
Microsoft.EntityFrameworkCore.Sqlite             1.1.0                        2016-11-15   1.1.1    2017-03-06   0.3
Microsoft.Extensions.Configuration.CommandLine   1.1.0                        2016-11-15   1.1.1    2017-03-06   0.3
Microsoft.Extensions.Configuration.Json          1.1.0                        2016-11-15   1.1.1    2017-03-06   0.3
Project is 2.5 libyears behind

Total is 2.7 libyears behind</div>
</p>
<p>You can see that since I passed a directory with more than one project, it found them all recursively and displayed the total at the bottom.</p>
<p>If you look closely at the test project's results, you can see that my 2 xunit packages are up-to-date. If I had run it with a <kbd>-q</kbd> or <kbd>--quiet</kbd> flag, it would have only displayed the outdated ones, omitting those that are up-to-date -- useful for large projects with many dependencies, or solutions with many projects, to be able to focus in on the problem areas!</p>
<p>There's also a <kbd>-u</kbd> or <kbd>--update</kbd> flag to update all dependencies to the latest version. I can see this being really useful for some <a href="https://greenkeeper.io/" target="_blank">Greenkeeper</a>-type scenarios.</p>
<p>"Wait a minute," you might be saying to yourself, perhaps out loud, despite being the only one in the room, "doesn't Visual Studio already have all this functionality, under <b>Manage NuGet Packages</b>?" And you'd be right, for the most part it does. But this is cross-platform. And runs from the command-line. And is 15KB, not 15GB. Options!</p>
<p>Now, I know that these sorts of simplifications are ripe for debate due to their potential abuse by those-who-shall-not-be-named less-technical number-cruncher types, but as shown in the cartoon at the top, it's at the very least a good first step in getting useful metrics as a way to inform decision-making processes.</p>
<p>You can find the code for <a href="https://github.com/stevedesmond-ca/dotnet-libyear" target="_blank">dotnet-libyear on GitHub here</a>, or check out Jared's <a href="https://github.com/jaredbeck/libyear-npm" target="_blank">npm</a> and <a href="https://github.com/jaredbeck/libyear-bundler" target="_blank">bundler</a> packages!</p>