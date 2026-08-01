# Movement Sandbox

## Scene

Open:

```text
Assets/Common/Scenes/MovementSandbox.unity
```

The scene is included in build settings so `GameOverUI` can reload it by build index.

## Controls

- `WASD`: move
- Mouse: look
- `Space`: jump
- `E`: interact
- `T`: emit a test sound stimulus
- `R`: restart when caught

## Important hierarchy relationships

```text
Player
├── CharacterController
├── PlayerMovement
├── SoundStimulusEmitter
├── PlayerInventory
├── PlayerCapture
└── CameraPivot
    └── Main Camera
        └── PlayerInteraction

Environment
├── Walls
├── DoorHinge
│   └── DoorVisual
└── interactable test objects

AI
├── Enemy (including EnemyHearing)
├── PatrolPoints
    ├── PointA
    ├── PointB
    └── PointC
└── TestSoundEmitter

Objectives
└── ExitZone

Navigation
└── NavMeshSurface

Canvas
├── InteractionPrompt
├── ObjectiveText
└── GameOverPanel
    └── GameOverText
```

The exact serialized hierarchy may contain additional grouping objects, but these ownership relationships should remain intact.

## Door setup

`DoorInteractable` belongs on `DoorHinge`. `DoorVisual` is offset horizontally from the hinge and owns the visible mesh and collider. The player interaction ray searches collider parents for `IInteractable`.

The doorway must be an actual gap between separate wall colliders. A visual frame placed over one solid wall collider does not create a traversable opening.

## Navigation setup

`Navigation` has an identity transform and hosts `NavMeshSurface`. Static geometry changes that affect traversability require rebaking. Patrol points must remain on the blue walkable surface.

The enemy agent currently uses:

- Height `2`
- Radius `0.5`
- Base Offset `1`

The offset aligns the centered two-unit capsule with the ground-level NavMesh position.

## UI setup

The Canvas uses screen-space UI. The interaction prompt is centered below the reticle area. Objective text uses a top-left anchor and top-left pivot with a small inward offset. `GameOverUI` belongs on the always-active Canvas rather than the panel it hides.

## Verification checklist

1. Movement collides with the room and supports jumping.
2. Interaction ray turns green over the cube, pickup, and door.
3. Prompts reflect the current interactable and door state.
4. The locked door rejects the player before key collection.
5. Key collection advances the objective and allows the door to open.
6. The exit trigger completes the objective.
7. The enemy patrols around baked obstacles.
8. Detection respects range, view angle, and wall occlusion.
9. Losing sight causes last-known-position investigation.
10. Arrival starts a visible full scan that can reacquire the player.
11. State colors and spatial voice cues match patrol, chase, investigation, and scanning.
12. Pressing `T` emits a semantic sound stimulus at the test emitter.
13. Selecting the test emitter shows its cyan hearing radius.
14. An in-range sound sends the enemy to investigate unless it is already chasing.
15. Landing emits one player stimulus; takeoff, walking, and startup do not.
16. Walls do not currently block sound, while emitters outside the radius are ignored.
17. Reaching the player shows game-over UI and pauses movement.
18. `R` reloads the sandbox and resets runtime state.
