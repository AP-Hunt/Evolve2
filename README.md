Evolve2
=======

A second revision of my .NET evolutionary graph research library. The original version (known as Evolve) was submitted as part of the dissertation for my software engineering degree under the supervision of [Dr George Mertzios](http://www.dur.ac.uk/george.mertzios/) at Durham University.

This library provides tools for performing experiments on [evolving graphs](http://en.wikipedia.org/wiki/Evolutionary_graph_theory) with the intention of being as generically applicable (to the problem space) as possible without sacrificing a clean API and simple setup.

Over time, the tools provided will expand. As of last writing, tools include:
* Graph construction with support for subgraphs (and running simulations on *just* a subgraph)
* Helpers for creating common graph structures. Current helpers:
    1. Chain
	2. Clique
	3. Burst
* A library of evolutionary graph simulations (Evolve2.Simulations). Current implementations:  
    - Modified [Moran Process](http://en.wikipedia.org/wiki/Moran_Process) as proposed by [Lieberman et al. (2005)](http://abel.math.harvard.edu/archive/153_fall_04/Additional_reading_material/evolutionary%20graph%20theory.pdf).

Future additions are likely to be:
* Allowing srucutral changes in the graph to occur during simulation
* More simulations
* Cleaner API (less need for generics, easier for the compiler to infer the type) 
* Performance improvements


For usage examples, see Evolve2/Program.cs (for now).
