### Initial Code design
- Initial design was to simply build up a path map for efficient calculation of costs across the map.
  - This will evaluate neighbours only initially and then each turn will expand its knowledge of the network.
  - We should be able to do multiple expansions in a single frame, but need to keep an eye on performance.
  - We use this map to do some rough cost/benefit analysis of each target (egg/crystal)
  - It has been "tuned" to favour eggs early on and crystals later on.
  
### 28th May Code design
- New approach is an adaptation of the minimum spanning tree
- The adaptation has to be to allow for us to ignore some nodes and allow for us to respond to the strategy of the other player
  - such as considering some crystals as safe and going after contested resources first.  