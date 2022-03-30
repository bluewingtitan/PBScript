# PBScript

PBScript is a simple (domain) scripting language pointed towards usage in games (especially unity and godot+C#).
The name derrives from "Plant Based Script", as it was originally developed for a small game I am working on that has something to do with plants.

It allows for easy injection of special actions a player may use by attaching objects with defined actions to the runtime environment.

For a compact documentation of PBScript look into the [Documentation](Documentation/Index.html) (HIGHLY W.I.P. + many upcoming changes! Lang is not stable yet!)

# No JIT, no meta-compiler, no magic
PBScript turns a script into a structure of language elements and let's you execute them line by line. It's definitely not as performant as other languages, but easier to extend (especially by less experienced people, which one of the goals). I am working on a Meta2-Driven Implementation of a compiler to "bytecode" and a runtime to excute it again, but that project will live in a seperate repository for now, as it does not (yet) belong to the core PBScript arsenal.


# Caution
The current version of PBScript is well tested in regards of functionality, but the api, language features, syntax, ... may (and propably will to some extend) change in the future and some edge-cases may very well still exist.

