# CodeTest - Daniel Downes

This plan was laid out before the coding began:

1. Add the level geometry, a single Quad.
2. Add static collision objects to the surface of this Quad.
3. Add collectible objects to this surface, ensuring they do not collide with the static objects.
4. Add the player object to the Quad, again ensuring it does not collide with the static objects.
5. Allow collectible objects to be picked up by the player.
6. Increment the score based on this event.

## 1 - Generate level, bespoke Quad

Opted to generate a mesh using raw vertices and triangulation. Simply to demonstrate mesh generation code and allow better control of the shape.

## 2 - Add static collision objects

Next, cuboid colliders are added randomly over the Quad. It does not matter if they overlap as it simply creates a more interesting level. If textures were applied, we would see z-order fighting, however.

## 3 - Add collectible objects

In order to do this, it was considered to simply test for a collision while adding a collectible and then repeat until a position was found that did not cause a collision.

However, it was foreseen that a NavMesh that was generated and baked before placing the collectibles would demonstrate more coding skills and understanding of Unity. It would also allow for irregular level shapes other than a Quad, or even multi-stage levels, i.e., platforms at different heights.

Once the NavMesh is baked, its mesh is then extracted.

It is then passed to a class that I previously developed to choose random points on a mesh in a distributed manner. The code in RandomPointOnMesh considers the area of each triangle within a mesh and then randomly selects a point based on linear distribution of the mesh area in total.

## 4 - Add the player

The spec suggested allowing the player to move in the x and z plane, however, it did not suggest how to do this. For fun, a simple rollaball style mechanism was added from scratch. The idea of bouncing off the obstacles and a ball was a natural choice.

The jump mechanic was added. After a quick test iteration, it was clear that the restraint to only jump while grounded was needed.

The mechanisms and detections are added as single classes here and then coupled in the PlayerController class.

This better adheres to the Single Responsibility principle and makes it easy to play around with different implementations for each feature while iterating on game mechanics.

## 5 - Allow objects to be collected

Again, collectible objects are created purely by code rather than a prefab.
The Factory pattern is used to spawn these and apply a TriggerObjectDetection class to allow for an Action delegate to be hooked in. This is then captured by the Factory class and then relayed to a single event to allow easier listening.

## 6 - Score

The score is implemented with respect to some design patterns. Rather than a single class to track and display the score, we keep these generic and then bind them to the game implementation via an interactor (use case) class.

This demonstrates consideration for reusability. Even though the score class is simple, it could be extended. The score system could be extended using inheritance (e.g., to apply combos) with consideration of the Open-closed principle.
Such segregation also lays a clear path for Unit Testing.

# Code Structure

The code is structured in a way that allows for easy extension and modification. The code is divided into the following folders:

 - BallGame - Contains the main game logic and the PlayerController. This is considered the main game assembly that is very app specific.
 
 - ReusableCode - Contains code used/shared in other projects. This includes the NavMesh generation, RandomPointOnMesh, Score mechanics and some other minor Unity helper code.


As this is a 'code' test, it seems appropriate to generate everything via code as much as possible. For general prototype is may make sense, for the sake of time, to make code so verbose.


In the majority of the code, a 'code first' approach is taken, and less Unity Editor inspector wiring is used.
A 'code first' has the added benefit of being able to be easily tested with Unit Tests, transcribing to other game engines (perhaps using LLM tooling), can also mitigate scene conflicts in a very specific Unity use case.
A few exceptions are made where the Unity Editor is used to demonstrate the ability to use the Unity Editor and to show that the code can be easily modified in the Unity Editor.
By being verbose its also easier to see where changes happen in diff tools too, which is a nice bonus, the downsize is a lot more code but this can be easily managed by further class separation, eg using the idea of a View class.


# Manual Playtesting iterations

Upon playtesting, values such as speed and jump velocity were exposed in the inspector and adjusted and balanced with the ball Physics material.

There were a few edge cases that were encountered, such as the player being able to jump while in the air. This was quickly fixed by adding a check for the player being grounded before allowing a jump.
Another issue was the player falling off the side of the level. This was resolved by adding another check to see if the player had fallen off the level and then resetting the player to the original spawn position.


# Graphics

To add some color to the game, a simple shader was added to the Player.

https://github.com/omid3098/Unity-URP-ScreenSpaceRefraction

The colors of objects were chosen in code, including the Nav Mesh generation to see how collectibles are only positions on the generated Nav Mesh.


# Automated Testing

Automated testing is done via Unity Test Runner, and the tests are in the ReusableCode-PlayModeTests and ReusableCode-UnitTests folders.

No tests were added to the BallGame Assembly, but if they were to be added, they would have their own Assembly test folders,
e.g., BallGame-PlayModeTests and BallGame-UnitTests.

In order to save time and demonstrate good usage of tooling, ChatGPT was largely used to generate the automated test code. It is then manually reviewed to ensure the tests make sense.

The ChatGPT logs links are included in the relevant classes. 

Its believe LLM code generation should absolutely be embraced and this is included as example of this. With ethical consideration (including in teams), the developer should declare when larger chunks of code have been generated, so to facility code review, and also sustain trust among the team.
