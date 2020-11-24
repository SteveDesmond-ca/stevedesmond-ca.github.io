---
title: I am async, and I have opinions!
description: "To suffix, or not to suffix: that is the question"
date: 2017-08-26
comments: https://twitter.com/stevedesmond_ca/status/901504104232620038
---
<p>A recent discussion on Twitter got me thinking again about this naming convention in .NET that I'm not a fan of. Since my most recent contract just ended (<a href="/work-with-steve" target="_blank">hint hint</a>) and my DevOpsDays PDX talk has been presented (<a href="/talks/devopsdayspdx" target="_blank">also hint hint</a>) I finally have some time to blog again!</p>
<p>This all started out a couple days ago with an innocent question:</p>
<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">has there been a consensus yet about whether or not we need to suffix async methods with &quot;async&quot;? <a href="https://twitter.com/hashtag/dta?src=hash">#dta</a> <a href="https://twitter.com/hashtag/csharp?src=hash">#csharp</a></p>— Nico Vermeir (@NicoVermeir) <a href="https://twitter.com/NicoVermeir/status/899892936007180288">August 22, 2017</a></blockquote>
<script async="async" src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
<p>Here's where I was introduced to it yesterday morning:</p>
<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">Should you suffix Task-based methods with Async? Yes. <a href="https://t.co/PFftsc1r0v">https://t.co/PFftsc1r0v</a></p>— Immo Landwerth (@terrajobst) <a href="https://twitter.com/terrajobst/status/900970451937054720">August 25, 2017</a></blockquote>
<script async="async" src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
<p>Then this comes in last night:</p>
<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">No.<br/><br/>Suffix non-task based methods with Sync.<br/><br/>And if your API is mixed sync and async, it is probably broken. <a href="https://t.co/tz0pDS5AID">https://t.co/tz0pDS5AID</a></p>— Damian Hickey (@randompunter) <a href="https://twitter.com/randompunter/status/901215790011207681">August 25, 2017</a></blockquote>
<script async="async" src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
<p>Essentially, the discussion comes down to this: which of the following is correct?</p>
<div class="code">public class NumberGetter
{
    public async Task&lt;int&gt; GetNumberAsync() { ... }
}</div>
<div class="code">public class NumberGetter
{
    public async Task&lt;int&gt; GetNumber() { ... }
}</div>
<p>My preference is definitely the latter, and the reasoning behind it is this:</p>
<blockquote>The name of the method should tell you what the method does. Not how it works; what it does.</blockquote>
<p>The rest of the signature tells you how it works: the accessibility level, the return type, the parameters, ..., that it's asynchronous.</p>
<p>It's the same reason we don't use Hungarian notation anymore (its <a href="https://www.joelonsoftware.com/2005/05/11/making-wrong-code-look-wrong/" target="_blank">original usefulness</a> aside, given later rampant abuse). Having a great type system, and tools that allow us to easily determine and work with those types, has removed the need to shoehorn that information redundantly into the name.</p>
<p>The same goes for <kbd>async</kbd>: we've got a keyword for it, take it out of the name. If your tools don't allow you to easily see that it's <kbd>async</kbd>, get (or make) better tools.</p>
<hr class="alert-light" style="width: 25%;" />
<p>Great, so we've decided this is best:</p>
<div class="code">public class NumberGetter
{
    public async Task&lt;int&gt; GetNumber() { ... }
}</div>
<p>But what if you've got a class that does both synchronous and asynchronous versions of the same methods?</p>
<div class="code">public class NumberGetter
{
    public int GetNumber() { ... }
    public async Task&lt;int&gt; GetNumberAsync() { ... }
}</div>
<p>Or, alternatively:</p>
<div class="code">public class NumberGetter
{
    public int GetNumberSync() { ... }
    public async Task&lt;int&gt; GetNumber() { ... }
}</div>
<p>Stop right there, go back to Damian's tweet, and read the last sentence again.</p>
<p>Do it.</p>
<p>No, actually do it.</p>
<blockquote>If your API is mixed sync and async, it is probably broken.</blockquote>
<p><i><b>Update @ 8:15pm:</b> Clarification — my own stance is a little more conservative, replacing "broken" with "not ideally designed."</i></p>
<p>The "correct" architecture is the following:</p>
<div class="code">public class NumberGetter
{
    public async Task&lt;int&gt; GetNumber() { ... }
}</div>
<p>"Hold up!" you say. "Isn't that the exact same as what you were talking about before? Where'd the synchronous one go?"</p>
<p>If the job of getting a number is inherently asynchronous, that should be the default implementation. Making a synchronous version of the same thing is either a workaround for consumers without an <kbd>async</kbd> context, or else it should be deprecated because that was the old way of doing it. If the synchronous version doesn't need to exist at all, remove it. If you absolutely need both a synchronous and asynchronous version, they belong in separate classes.</p>
<div class="code">public class NumberGetter
{
    public async Task&lt;int&gt; GetNumber() { ... }
}

public class LegacyNumberGetter
{
    public int GetNumber() { ... }
}</div>
<hr class="alert-light" style="width: 25%;" />
<p>OK, ready to yell at me about this? Use the "Comment / Share" link below!</p>