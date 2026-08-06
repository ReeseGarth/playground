# Night Shift Vertical Slice

## Scenario

The player must finish the night shift by finding the maintenance key, reaching the secured staff exit, and avoiding the patrolling creature.

The first blockout intentionally reuses the proven sandbox loop. Its purpose is to validate Common components in a product-specific scene before adding bespoke Night Shift mechanics or production presentation.

## Game direction

Night Shift is an authored and systemic hybrid. The player is an overnight sanitation contractor working in facilities that conceal dangerous containment activity. A shift begins with routine work, develops through clues and encounters, and ends when the player chooses to extract.

Escape banks an outcome rather than merely ending a level:

- money broadens tactical options through equipment, supplies, rentals, and insurance
- contractor reputation changes available work, information, access, and contract terms
- recovered evidence advances the mystery, choices, and endings

Main story access should not require reputation grinding. Authored progress follows the narrative, while reputation creates alternatives and money changes how a contract can be approached.

The first vertical slice should prove one risk decision: leave after completing required work, or remain in danger for optional cleanup or evidence that changes the shift result.

The first implemented version uses two cleanable messes:

- an ordinary coffee spill represents required work in a relatively safe area
- containment residue represents optional hazard work near the enemy patrol

The player may extract with either task incomplete. Extraction snapshots the completed work, calculates provisional pay and contractor rating, pauses gameplay, and presents an end-of-shift result. These values are reported outcomes only; persistence, purchasing, and contract selection wait for real decisions that consume them.

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
9. Extraction reports required and optional cleanup independently.
10. Required cleanup alone pays `$150` with a `4/5` rating.
11. Completing both pays `$250` with a `5/5` rating.
12. Missing required cleanup produces `SHIFT ABANDONED` with a `1/5` rating.
13. The results panel pauses gameplay, releases the cursor, and restarts with `R`.

## Current limitations

- The layout is a graybox built from primitives and reused sandbox geometry.
- The objective still exercises the existing key-and-exit loop rather than a bespoke janitorial task.
- Item IDs and interaction prompts still use the generic internal `key` wording.
- Lighting, art, environmental storytelling, and audio presentation are placeholders.
- Cleaning is an instantaneous interaction with no tool, duration, animation, or sound.
- Pay and rating are calculated for the current extraction but are not persisted.
- Evidence recovery and its narrative consequences are not yet implemented.
