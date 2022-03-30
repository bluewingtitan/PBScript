# PBScript

PBScript is a simple (domain) scripting language pointed towards usage in games (especially unity and godot+C#).
The name derrives from "Plant Based Script", as it was originally developed for a small game I am working on that has something to do with plants.

It allows for easy injection of special actions a player may use by attaching objects with defined actions to the runtime environment.

For a compact documentation of PBScript look into the [Documentation](Documentation/Index.html) (HIGHLY W.I.P. + many upcoming changes! Lang is not stable yet!)

# No JIT, no compiler, no magic
PBScript turns a script into a structure of language elements and let's you execute them line by line. It's definitely not as performant as other languages, but easier to extend (especially by less experienced people, which was one of the main goals). It still only takes a few houndred nano to run full programs, which is more than sufficient for the game context it's developed for.

I am working on a Meta2-Driven Implementation of a compiler to "bytecode" and a runtime to excute it again, but that project will live in a seperate repository for now, as it does not (yet) belong to the core PBScript arsenal.


# Caution
The current version of PBScript is well tested in regards of functionality, but the api, language features, syntax, ... may (and propably will to some extend) change in the future and some edge-cases may very well still exist.


# Roadmap

## \[Almost there] Full Support Multi-Argument-Calls
> The current way of how calls are handled allow for that, but it's still pretty sloppy. It's also the main-reason why the language version that actually supports that (and is around 3000x (not %!) faster than the current main branch version) is not on the main branch yet.

## \[Next Highest Priority] Functions
> Functions are great! They let users design modules that they may use multiple times over their code.

## \[Mostly a Concept at that point] User-Defined Library
> Basically a term for letting users request their own scripts and let them use functions out of them.
