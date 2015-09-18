Grosvener .NET Developer Practicum
==================================


Nothing fancy, but here it is.   A few notes upfront.

I'm really struggling to understand the scope of item #3.   I have no idea what your environment looks like there, so I considered
just stripping everything down to a common denominator codebase, skipping the csproj stuff and just  running `csc.exe` on the source files.  
That would work anywhere, but it would give warped view of the way I usually write things.  So in the end, I decided to just go ahead and let
Visual Studio generate the standard .sln and .csproj files, and I'm running those from msbuild.   

`Build.bat` assumes the following:

* It assumes you have the visual studio tools on path, so this should be run from a *vsvars*-style command prompt.  

* It assumes it's being run from the solution folder.  At the end, it copies the .exe to ./dist.

* It assumes `nuget restore` isn't blocked.

* I wrote all this in VS2015, I'm not sure what version you have so I didn't check backward compatibility (I wasn't sure what to check it *with*).  
However, I went through and got rid of all the C# 7 stuff I could find, so it should compile fine with older compilers.  If you need something older, 
let me know.

A few notes about the actual solution, although this is stuff we'll talk about.

* There's a bug in the instructions.  I assume you already know this.  The list of dishes in the documentation uses "Toast" (capital 'T'), while 
the example output uses 'toast' (lower-case 't').   I went with the examples, documentation is always wrong.

* There's an interesting ambiguity in the instructions.  What should "morning, 1, 2, 1" print?  "eggs, toast, error" or just "eggs, error". I could 
interpret it either way, so I went with the one that was easiest to code :)




