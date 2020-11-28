---
title: Introducing SimpleGPIO
description: Accelerating Time to Love At First Blink
date: 2018-07-03
comments: https://twitter.com/stevedesmond_ca/status/1014171786563457024 ---
---

*TL;DR: Check out my new .NET Standard IoT library,
[SimpleGPIO](https://github.com/stevedesmond-ca/SimpleGPIO)!*

A few years ago, I saw a video on the Internet. Perhaps you've seen it, too...

<div class="widescreen">
  <iframe src="https://www.youtube.com/embed/E2evC2xTNWg" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen="true"></iframe>
</div>

Something about this got my gears turning. I watched [Simone's other videos](https://www.youtube.com/channel/UC3KEoMzNz8eYnwBC34RaKCQ/videos?flow=grid&view=0&sort=da). I bought a Raspberry Pi and a starter kit. I went through all the tutorials. And then, life happened. And it all started gathering dust.

Fast-forward to the present. My kids have been part of a co-op activity camp for the past couple of summers, where parents volunteer to host and lead an activity with everyone. At the end of August, I'll be sharing about robots, including a hands-on project of having them help me build a real one.

When Scott Hanselman had Simone [on Hanselminutes](https://hanselminutes.com/518/march-is-for-makers-arduinos-and-useless-robots-with-simone-giertz) shortly after her first videos were posted, part of their discussion was around what she called "Love at First Blink", that feeling you get when everything clicks for the first time. As developers, we get little moments like that every day, and this is part of what I'm hoping to be able to share. I'm not sure if the kids are old enough to fully grasp what's going on, but at the very least we can all build some cool stuff together!

I'm fascinated with combining hardware and software to make code do something physical...not just using a keyboard/mouse and representing something on a screen, but changing -- and even interacting with -- the real world.

I've also noticed that a lot of what I work on in my spare time is about lowering barriers to entry. The early work to get [.NET Core running on ARM](/blog/net-core-on-arm) -- along with [ARM binaries for VSCode](https://github.com/stevedesmond-ca/vscode-arm/releases) -- now means you don't need a $500+ machine to build .NET apps with an awesome lightweight IDE.

Combining these two interests, I have open-sourced [SimpleGPIO](https://github.com/stevedesmond-ca/SimpleGPIO), a low-ceremony .NET Standard library for all your IoT needs.

Many GPIO libraries available for various languages today are too low-level for my taste. There are a bunch of .NET ones, but let's take Python's RPi.GPIO, which is the first Google result for "GPIO library", as well as SunFounder's and SparkFun's recommendation in all their examples.

Here's how you turn on an LED with this library:

```
GPIO.setmode(GPIO.BOARD)
GPIO.setup(16,GPIO.OUT)
GPIO.output(16,GPIO.HIGH)
```

The official Windows (UWP) SDK is similarly verbose:

```
var gpio = GpioController.GetDefault();
var redLED = gpio.OpenPin(23)
var mode = GpioPinDriveMode.Output;
redLED.SetDriveMode(mode);
redLED.Write(GpioPinValue.Low);
```

Now, I'm not at all trying to diminish the value of learning [from the metal up](https://www.hanselman.com/blog/TeachingCodingFromTheMetalUpOrFromTheGlassBack.aspx). In fact, I think it's incredibly important for developers to actually understand what's happening under the hood and behind the scenes of their applications. But our day-to-day work needn't conjure up jokes of "[how many lines of code does it take to turn on a lightbulb?](http://blog.chrisbriggsy.com/Beginners-guide-to-GPIO-Win10-IoT-Core-Insider-Preview/)" In the same way that .NET handles garbage collection so I don't need to think as much about memory leaks, I want my libraries to abstract away
less-important implementation details, so that my code can focus on clarity and value.

Here's the same "LED-on" functionality with SimpleGPIO:

```
var pi = new RaspberryPi();
var redLED = pi.Pin16;
redLED.TurnOn();
```

If you're trying to modify a component's state, *of course* you want to enable the pin and set its direction to out, so the library handles all of this for you. Similarly, once you have a good grasp of how GPIO works, you shouldn't need to think about high vs low voltage, you just want to get the electrons flowing.

You're still welcome to work with SimpleGPIO in lower-level terms, if that's your cup of tea, e.g. pi.GPIO23.Voltage = Voltage.High; but I think the ability to name variables and use helper methods makes the code much clearer, and reduces cognitive load on the developer. Plus, as always, taking advantage of C#'s relatively strong static typing (vs relying on lower-level primitives) leads to less error-prone code.

Here to show you just how simple this library is, it's me!

<div class="widescreen">
  <iframe src="https://www.youtube.com/embed/ZMKIGc313ow?rel=0" frameborder="0" allow="autoplay encrypted-media" allowfullscreen="true"></iframe>
</div>

Pretty cool, right? The code for all of these examples can be found [in the repo](https://github.com/stevedesmond-ca/SimpleGPIO). Also available there is, without a doubt, the most thorough documentation I have ever written for an open-source project!

So, if you've got any IoT projects and you want to give something new a spin, or if you just want to play around with it, I'd love to hear how your experience went. Either way, let me know what you think!