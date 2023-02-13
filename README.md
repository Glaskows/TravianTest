# TravianTest
This repo contains the coding test for Travian Games.

All rights are reserved to George Laskowsky Ziguilinsky.


## Description

The app consist on a procedurally generated island with different terrain tails.

You can build houses in grass tiles only. But building houses will deplete your lumber resource.

Destroying houses will give you some resources back.

## Usage

If you swipe left and right you will rotate the camera around the island.

When you click on an empty grass tile you will see a button to create a new house. The button will be deactivated if your resources are not enough. Building a house cost 5 lumber.

If the cursor is over a tile with a house a destroy button will be visible. Press it to destroy the house and get 2 lumber back.

## Source code analysis

This is a simple project, there is not much going on but some design decision where made early on.

- Separation of logic and rendering
- Configuration as ScriptableObjects
- Logic as simple classes. Input and rendering as MonoBehaviours (when possible)
- One initializer which injects dependencies. Normally I would use Zenject instead
- Try to decouple through events. Sadly due to a lack of a good messenger system (Zenject signals or TinyMessenger to name a couple). I had to use a mix of C# events and Unity events.
- An exception to the last point is normal references when communication between MonoBehaviours are needed. As long as they have a similar life cycle and live on the same scene/prefab, is a practical solution for small projects.

### Good

- Modularity of the solution and lack of coupling between the different components (resources, buildings, map)
- Logic mostly left in C# Unity agnostic code.
- Configuration files which allows to add more buildings and resources with relatively ease.
- Rotating the island feels good, it has a nice inertia to it.
- Arrow animation gives it a nice polish touch.

### Bad

- Not enough time to make the builder bottom menu flexible to add other building types. Logic wise was almost all there (notably missing the tile requirement for buildings) and the menu does adapt if new buttons are added, but the menu logic to populate with buttons is missing.

### Ugly

- Lack of cohesive event/messaging system.
- Primitive dependency installer.
- Use of enums as type identifier. An uid should be use to make the code design data oriented.
- Lack of simple view for resource UI and building button UI. Direct use of their components from parent view code is clunky and not less modular.
- Use of "classic" Unity input system.
- No explicit separation between data and logic (for example as models, data providers and controllers) which would make the code more client/server oriented.

## References

The following resources where used on this test:

- Hex coordinates information: https://www.redblobgames.com/grids/hexagons/
- Assets: https://kenney.nl 
- Water Shader: https://roystan.net/articles/toon-water/
