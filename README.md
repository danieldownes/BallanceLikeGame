# CodeTest - Daniel Downes

As this is a 'code' test it seems appretiate to generate everything via code as much as possible.

To generate the distrubution of static barriers its considered closed areas within the avilable mesh
may be encountered. To mitigate this edge case it approritate to use a NavMesh and bake dynamically at 
runtime, which is possible with NavMeshSurface.

The plan for then is to:

1. Add the level geometry, single Quad.
2. Add static collision objects to the surface of this Quad.
3. Add collectable objects to this surface, ensuring they do not collide with the static objects.
4. Add the player object to the quad, again ensuring they do not collide with the static objects.
5. Allow collectable objects to be picked up by the player.
6. Increment score based on this event.
