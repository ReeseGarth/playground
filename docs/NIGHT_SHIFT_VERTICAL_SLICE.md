# Night Shift Vertical Slice

## Scenario

The player must finish the night shift by finding the maintenance key, reaching the secured staff exit, and avoiding the patrolling creature.

The first blockout intentionally reuses the proven sandbox loop. Its purpose is to validate Common components in a product-specific scene before adding bespoke Night Shift mechanics or production presentation.

## Scene

Open:

```text
Assets/NightShift/Scenes/NightShiftVerticalSlice.unity
```

The scene is included in the build scene list so capture restart reloads the active Night Shift scene rather than the movement sandbox.

## Current flow

```text
Start shift
  -> cross the patrolled room
  -> find the maintenance key
  -> reach and open the secured staff exit
  -> enter the exit zone or get captured
```

`MaintenanceShelfBlockout` is the first product-specific graybox object. It creates physical cover, blocks line of sight, and changes both player and enemy routes near the key.

## Shared and product-specific ownership

- `Assets/Common` owns reusable candidate components and the movement sandbox.
- `Assets/NightShift` owns the product scene, its authored layout, and its independent baked NavMesh data.
- The Night Shift scene references Common scripts directly; it does not duplicate their source.
- Scene-specific objective wording is configured through serialized fields on `ObjectiveTracker`.

## Verification checklist

1. The Night Shift scene opens and runs independently from the movement sandbox.
2. Movement, interaction, inventory, door, objective, enemy, capture, and restart behavior still work.
3. The Night Shift and movement sandbox scenes reference different NavMesh data assets.
4. The enemy and player route around both ends of the maintenance shelf.
5. The shelf blocks enemy line of sight.
6. Landing within hearing radius can send the enemy around the shelf to investigate.
7. Objective text progresses from maintenance key, to staff exit, to shift complete.
8. Capture and `R` reload the Night Shift scene.

## Current limitations

- The layout is a graybox built from primitives and reused sandbox geometry.
- The objective still exercises the existing key-and-exit loop rather than a bespoke janitorial task.
- Item IDs and interaction prompts still use the generic internal `key` wording.
- Lighting, art, environmental storytelling, and audio presentation are placeholders.
