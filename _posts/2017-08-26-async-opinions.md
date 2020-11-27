---
title: I am async, and I have opinions!
description: "To suffix, or not to suffix: that is the question"
date: 2017-08-26
comments: https://twitter.com/stevedesmond_ca/status/901504104232620038
---

A recent discussion on Twitter got me thinking again about this naming convention in .NET that I'm not a fan of. Since my most recent contract just ended ([hint hint](/work-with-steve)) and my DevOpsDays PDX talk has been presented ([also hint hint](/talks/devopsdayspdx)) I finally have some time to blog again!

This all started out a couple days ago with an innocent question:

> has there been a consensus yet about whether or not we need to suffix async methods with "async"? [#dta](https://twitter.com/hashtag/dta?src=hash) [#csharp](https://twitter.com/hashtag/csharp?src=hash)
>
> --- Nico Vermeir (@NicoVermeir) [August 22, 2017](https://twitter.com/NicoVermeir/status/899892936007180288)

Here's where I was introduced to it yesterday morning:

> Should you suffix Task-based methods with Async? Yes.
> <https://t.co/PFftsc1r0v>
>
> --- Immo Landwerth (@terrajobst) [August 25,
> 2017](https://twitter.com/terrajobst/status/900970451937054720)

Then this comes in last night:

> No.
> 
> Suffix non-task based methods with Sync.
> 
> And if your API is mixed sync and async, it is probably broken.
> <https://t.co/tz0pDS5AID>
>
> --- Damian Hickey (@randompunter) [August 25,
> 2017](https://twitter.com/randompunter/status/901215790011207681)

Essentially, the discussion comes down to this: which of the following is correct?

```
public class NumberGetter
{
  public async Task<int> GetNumberAsync() { ... }
}
```

```
public class NumberGetter
{
  public async Task<int> GetNumber() { ... }
}
```

My preference is definitely the latter, and the reasoning behind it is this:

> The name of the method should tell you what the method does. Not how it works; what it does.

The rest of the signature tells you how it works: the accessibility
level, the return type, the parameters, ..., that it's asynchronous.

It's the same reason we don't use Hungarian notation anymore (its [original usefulness](https://www.joelonsoftware.com/2005/05/11/making-wrong-code-look-wrong/) aside, given later rampant abuse). Having a great type system, and tools that allow us to easily determine and work with those types, has removed the need to shoehorn that information redundantly into the name.

The same goes for async: we've got a keyword for it, take it out of the name. If your tools don't allow you to easily see that it's async, get (or make) better tools.

---

Great, so we've decided this is best:

```
public class NumberGetter
{
  public async Task<int> GetNumber() { ... }
}
```

But what if you've got a class that does both synchronous and asynchronous versions of the same methods?

```
public class NumberGetter
{
  public int GetNumber() { ... }
  public async Task<int> GetNumberAsync() { ... }
}
```

Or, alternatively:

```
public class NumberGetter
{
  public int GetNumberSync() { ... }
  public async Task<int> GetNumber() { ... }
}
```

Stop right there, go back to Damian's tweet, and read the last sentence again.

Do it.

No, actually do it.

> If your API is mixed sync and async, it is probably broken.

***Update @ 8:15pm:** Clarification -- my own stance is a little more conservative, replacing "broken" with "not ideally designed."*

The "correct" architecture is the following:

```
public class NumberGetter
{
  public async Task<int> GetNumber() { ... }
}
```

"Hold up!" you say. "Isn't that the exact same as what you were talking about before? Where'd the synchronous one go?"

If the job of getting a number is inherently asynchronous, that should be the default implementation. Making a synchronous version of the same thing is either a workaround for consumers without an async context, or else it should be deprecated because that was the old way of doing it. If the synchronous version doesn't need to exist at all, remove it. If you absolutely need both a synchronous and asynchronous version, they belong in separate classes.

```
public class NumberGetter
{
  public async Task<int> GetNumber() { ... }
}

public class LegacyNumberGetter
{
  public int GetNumber() { ... }
}
```

---

OK, ready to yell at me about this? Use the "Share / Comment" link below!