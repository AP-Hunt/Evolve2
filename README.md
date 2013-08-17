Evolve2
=======

A second revision of my .NET evolutionary graph research framework. The original version (known as Evolve) was submitted as part of the dissertation for my software engineering degree under the supervision of [Dr George Mertzios](http://www.dur.ac.uk/george.mertzios/) at Durham University.

This framework provides tools for performing experiments on [evolving graphs](http://en.wikipedia.org/wiki/Evolutionary_graph_theory) with the intention of being as generically applicable (to the problem space) as possible without sacrificing a clean API and simple setup.

Over time, the tools provided will expand. As of last writing, tools include:
* Graph construction with support for subgraphs (and running simulations on *just* a subgraph)
* Stateful vertices (curently limited to binary state).
* A simple implementation of the modified [Moran Process](http://en.wikipedia.org/wiki/Moran_Process) as proposed by [Lieberman et al. (2005)](http://abel.math.harvard.edu/archive/153_fall_04/Additional_reading_material/evolutionary%20graph%20theory.pdf).

Future additions are likely to be:
* More fine grained control of vertex state
* Allowing srucutral changes in the graph to occur during simulation
* Helpers for common graph structures (such as cliques, wheels, paths etc)
* Performance improvements


For usage examples, see Evolve2/Program.cs (for now).
