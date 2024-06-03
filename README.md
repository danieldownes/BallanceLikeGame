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

## 1 - Generate level, bespoke Quad

Opted to generate a mesh using raw verticies and triangulation. Simply to demonstrate mesh generation code, and allow better control of the shape.

## 2 - Add static collision objects

Next cubiod colliders are added randomly over the quad. It does not matter if they overlap as it simply creates a more interesting level. If textures were applied we would see z-order fighting however.

## 3 - Add collectable objects

In order to do this, it was considered to simply test for a collission while adding a collectable and then repeat until a position was found that did not cause a collision.

However it was forseen that a NavMesh that was generated and baked before placing the collectables would demonstrate more coding skills and understanding of Unity. It would also allow for irregular level shapes other than a quad, or even multi stage levels. ie platforms at different heights.

Once the NavMesh is baked, its mesh is then extracted.

It is then passsed to a class that I previous developed to choose random points on a mesh in a distribted manner. The code in RandomPointOnMesh conisders the area of each triangle within a mesh and then randomly selects a point based on linearly distribusion of the mesh area in total.

## 4 - Add the player

The spec suggested to allow the player to move in the x and z plane, however it did not suggest how to do this, for fun a simple rollaball style mechinaism was added from scatch. The idea of bouncing off the obsticles and a ball was a natural choice.

The jump mechanic was added, after a quick test iteration is was clear that the restraint to only jump while grounded was needed.

The mechnisims and detections are added as single classes here and then coupled in the PlayerController class.

This better adhears to the Single Responsiblity princile, and makes it easy to play around with different implemenations for each feature while iterating on game mechanics.

## 5 - Allow objects to be collected

Again collectable objects are created purely by code rather than a prevab.
The Factory pattern is used to spawn these, and apply a TriggerObjectDetection class, to allow for a Action delegate to be hooked in. This is then captured by the Factory class and then relayed to a single event to allow easier listening.

## 6 - Score

The score is implemented with respect to some design patterns, rather than a single class to track and display the score, we keep these generic and then bind them to the game implementation via a interactor class.

This demonstrates consideration reusablity, even though the score class is simple it could be extended. The score system could be extended using inheritance (eg to apply combos) with consideration the Open-closed principle.
Such segregation also lays a clear path for Unit Testing.


# Graphics
https://github.com/omid3098/Unity-URP-ScreenSpaceRefraction