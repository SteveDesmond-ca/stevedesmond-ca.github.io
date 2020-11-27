---
title: Performance is Paramount
description: Investigating server-side optimizations for ASP.NET Core web apps
date: 2016-02-01
comments: https://twitter.com/stevedesmond_ca/status/694339607102672897
---

```
Note: while the content of this post is still relevant,
several specifics have changed over the past 5 years,
and therefore some links may no longer work.
```

Performance has always been one of the most fundamental and important aspects of user experience on the web for me. If pages are slow to load, or interactions feel clunky, no amount of fancy design or exclusive content is going to make up for that.

I'm going to show you a couple of the server-side performance improvements I've made here since the site launched last spring, using [LoadTestToolbox](https://github.com/stevedesmond-ca/LoadTestToolbox), an open-source web app load testing suite I started recently.

I'll focus specifically on [the main home page](/), since it aggregates and displays a wider array of data than any other page or section, using the following command:

`drill http://server:5000 100 10 {filename}.png`

That is to say, these tests all hit kestrel directly (rather than reverse-proxying through IIS, like production does) at 100 requests per second for 10 seconds. [The server](/about/datacenter#server) is about the equivalent of an Azure Standard A2 VM, if you're looking for a rough point of comparison.

### 1. Simple database calls

As you may recall, this past summer I created a [simple CMS framework](/blog/now-with-more-cms) for the site to run on. But pulling data from a database on every request, even if it's living on the same server, is not ideal.

<figure>
<img src="/assets/blog/perf-beta-nocache.png" alt="Performance chart" />
<figcaption>Performance on ASP.NET Core beta8 with database fetches</figcaption>
</figure>

Let's take a minute to decode some of what we're seeing.

Overall, the server cannot keep up with the traffic; load times keep increasing as the requests come in. It looks like it would almost be able to handle it, if it weren't for those spikes, which appear to be garbage collection runs.

So, we've got a fully-optimized database (proper columns, good indexing, etc.) and it's running locally, so there's no network traffic to be able to remove. Other than upgrading the server, what's a developer to do?

### 2. Static In-Memory Cache

Why go to the database every time for data that we know isn't changing? Implementing a [static in-memory cache](https://github.com/stevedesmond-ca/stevedesmond.ca/blob/cbf52b3cee26a3faff549dc22a9f83640689a3b7/Web/Models/Cache.cs) (and automatically flushing only whenever the data is changed through the admin area) provides an easy way to cut down on unnecessary load times.

<figure>
<img src="/assets/blog/perf-beta-cache.png" alt="Performance chart" />
<figcaption>Performance on ASP.NET Core beta8 with static in-memory cache</figcaption>
</figure>

Wow! We've got the load quite under control now, and the average response time is pretty good too, waffling around ~5-12ms. You can also see the speedy recovery after the larger GC spikes.

OK, so 5ms is pretty good, but I think we can do better. And lo and behold, all it took was some time...

### 3. Framework Upgrades

No matter how awesome a driver you are, your car is only going to be as fast as the engine that runs it. The .NET and ASP.NET teams have done a fantastic job at optimizing their respective frameworks, and upgrading from Beta 8 to RC1 saw a dramatic improvement. I'd like to specifically call out the efforts of [Ben Adams](https://twitter.com/ben_a_adams), who has been tireless in finding and contributing performance improvements in the ASP.NET stack.

This is, obviously, one of the benefits of open-source software: those who would otherwise not be able to help improve a product, making it better for everyone.

So just how good did it get?

<figure>
<img src="/assets/blog/perf-rc1-cache.png" alt="Performance chart" />
<figcaption>Performance on ASP.NET Core rc1 with static in-memory cache</figcaption>
</figure>

Look at that! Look at it! The load times! The stability! I was so amped when I first saw this that I tweeted about it: (numbers were from New Relic)

> Upgraded stevedesmond.ca to [@aspnet](https://twitter.com/aspnet) 5 RC (from b8), response time went from 20ms to 4ms. Not a typo. [@DamianEdwards](https://twitter.com/DamianEdwards) [@shanselman](https://twitter.com/shanselman) [@jongalloway](https://twitter.com/jongalloway)
>
> -- Steve Desmond (@stevedesmond_ca) [November 22, 2015](https://twitter.com/stevedesmond_ca/status/668555154464112641)

The power of an awesome framework.

#### 2.5. Just for fun

There's another permutation of caching+framework that I hadn't happened to have looked at until just now. What if I had upgraded to rc1 without having implemented the cache?

Lucky for you, I destroyed my beautiful code to find out!

<figure>
<img src="/assets/blog/perf-rc1-nocache.png" alt="Performance chart" />
<figcaption>Performance on ASP.NET Core rc1 with no cache</figcaption>
</figure>

Not bad; about the same improvement as the work I did myself. But coming around to my conclusion, not good enough on its own.

### Conclusion

So what did I learn from all this? A couple, rather obvious things were re-iterated for me, though with some backing evidence:

- Un-optimized code on an un-optimized framework is going to result less-than-ideal performance
- Efficient code can get you most of the way there, as can an efficient framework
- It takes great code and a great framework to have fully-optimized response times

The big thing I got out of it is that due to the hard work of the teams at Microsoft, and the community helping build these platforms, .NET Core is really coming around as a strong contender in the modern web/app arena.

Performance is just one of the things that makes my platform of choice great. What are some of your reasons? And not necessarily .NET -- why do you like what you like?

More generally, what are some of the performance tricks you have up your sleeve? How do you squeeze every millisecond out of what you've got? Or *do you*?

I'd love to hear your thoughts -- sound off below!
