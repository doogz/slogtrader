The NumbersGameSdk defines a number of interfaces for modelling a number-crunching algorithm.
The


Company A wishes to offer number crunching services to one of their clients, remotely.
In our case, we'll use the numbers game from countdown, practicing some thread-safe, multithreaded coding,
with unit tests. The client deliverable is an API, NumberGameSdk, for which the product owner said:


"I want to see the shortest solution within 15 seconds, and the shortest within 15 seconds, if one's possible - or a clear indication that one isn't."
"I don't ever want to see unnecessary steps"
"I want to see the shortest solution within "

I imagine commisioning this piece of software so that I can deliver some number crunching maths service to a remote client.
The client deliverable is the NumberGameSdk API.


requisite functionality is provided in the 
a solution is possible, it must be arrived at within the first 15 seconds, otherwise we can't guarantee its timely consumption by the presenter won't have time to commit it to memory (apparently.)
I want a bespoke maths chalk-board view of the solution, and, I also want the solution to be rendered as prounceable text for insertion into an autocue. The autocue view should scroll vertically at a set number
of words per minute. Ideally the scroll would be smooth, but I'll accept text line-by-line rather than pixel line-by-line to get started.

I also don't want to see solutions with unnecessary steps in them - ever - 


