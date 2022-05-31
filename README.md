# PBScript

PBScript is a simple (domain) scripting language pointed towards usage in games (especially unity and godot+C#).
The name derrives from "Plant Based Script", as it was originally developed for a small game I am working on that has something to do with plants.

It allows for easy injection of special actions a player may use by attaching objects with defined actions to the runtime environment.

For a compact documentation of PBScript look into the [Documentation](Documentation/Index.html) (HIGHLY W.I.P. + many upcoming changes! Lang is not stable yet!)

# No JIT, no compiler, no magic
PBScript turns a script into a structure of language elements and let's you execute them line by line. It's definitely not as performant as other languages, but easier to extend (especially by less experienced people, which was one of the main goals). It still only takes a few hundred nanoseconds to run normal-sized scripts related to the game it's developed for, which is more than sufficient for what it's developed for.

# Caution
The current version of PBScript is well tested in regards of functionality, but the api, language features, syntax, ... may (and propably will to some extend) change in the future and some edge-cases may very well still exist.


# Roadmap
This repository is mostly scrapped. I am developing a new Implementation with compilation to bytecode and a own little VM seperatly. It will support functions, libraries written directly in PBScript, wayy more efficient execution (3000%+ regarding Time AND Memory). This repository is an experimental version of what will follow at best.
