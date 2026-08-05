# Architecture and Decisions

## Architectural direction

Unity composition is the primary structuring mechanism. `MonoBehaviour` components should have focused responsibilities and collaborate through explicit references, interfaces, or events.

The current code favors clarity and Inspector visibility over generalized frameworks.

## Project organization

`Assets/Common` contains gameplay building blocks that may prove reusable across features or projects. “Common” currently means **reusable candidate**, not a compatibility guarantee.

```text
Assets/Common/
├── Materials/
├── Scenes/
├── Scripts/
│   ├── AI/
│   ├── Environment/
│   ├── Interaction/
│   ├── Inventory/
│   ├── Objectives/
│   ├── Perception/
│   └── Player/
└── UI/
```

Stable cross-project sharing should eventually use focused Unity Package Manager packages. Extraction should follow a genuine second consumer so hidden assumptions can be discovered first.

`NightShift.Common` defines the runtime assembly boundary for scripts under `Assets/Common/Scripts`. The Edit Mode test assembly references this runtime assembly, while production code has no dependency on tests.

## Key decisions

### Character movement

`PlayerMovement` uses `CharacterController.Move` rather than directly assigning the transform. The controller supplies collision-aware character motion while the component owns horizontal intent, gravity, and jumping.

### Interaction contract

`IInteractable` accepts the interacting `GameObject`:

```csharp
string GetInteractionPrompt(GameObject interactor);
void Interact(GameObject interactor);
```

The context allows prompts and authorization to depend on player state without `PlayerInteraction` knowing concrete interactable types. `GetComponentInParent<IInteractable>()` lets visual child colliders delegate behavior to an owning parent.

### Inventory

`PlayerInventory` currently uses a `HashSet<string>`. This models unique item membership and makes duplicate addition explicit. String IDs are a deliberate minimum implementation; item assets or typed identifiers should wait for requirements such as metadata, quantities, persistence, or editor validation.

### Events

Inventory publishes `ItemAdded`, and the objective tracker subscribes during `OnEnable` and unsubscribes during `OnDisable`. This keeps inventory independent of objective and UI concerns and scopes listening to the component’s active lifecycle.

### Doors

The door behavior lives on a hinge parent while the visible mesh and collider live on its child. The initial local rotation is treated as the closed pose. The door calculates a target quaternion and rotates toward it over time.

### Objective ownership

`ExitZone` detects a physical overlap. `ObjectiveTracker` owns objective state. TextMeshPro owns presentation. This prevents the trigger from becoming a combined detection, state, and UI component.

### Enemy AI

AI responsibilities are composed from:

- `PlayerDetection`: range, field of view, line of sight, and acquisition/tracking angles.
- `EnemyHearing`: semantic sound range checks and investigation requests.
- `EnemyMovement`: patrol, chase, investigation, scanning state, and NavMesh destinations.
- `EnemyCapture`: capture proximity and authorization through current detection.
- `NavMeshAgent`: path calculation, steering, and obstacle avoidance.
- `EnemyStateIndicator`: visual presentation of state changes.
- `EnemyStateAudio`: spatial audio presentation of state changes.

State transitions are explicit through `EnemyState` and published through an event.
Last-known-position investigation prevents immediate omniscient tracking after line
of sight is lost, while a distinct scanning state represents the stationary search
after arrival. Presentation components observe those decisions without controlling
the state machine.

`EnemyStateTransitions` owns the pure sound-priority rule used by `EnemyMovement`: sound leads to investigation unless the enemy is already chasing. Keeping this decision free of GameObjects, NavMesh, and physics makes it suitable for fast Edit Mode tests while sandbox testing continues to verify Unity wiring and navigation behavior.

### Capture and game over

`PlayerCapture` owns the captured state and publishes one event. `GameOverUI` reacts by showing UI, pausing scaled time, releasing the cursor, and reloading the active build scene on restart.

### Semantic sound stimuli

`SoundStimulus` represents gameplay sound as an immutable world position and hearing radius. This semantic data is independent of `AudioClip` and `AudioSource`, which provide player-facing audio presentation.

`SoundStimulusEmitter` publishes stimuli through a static event so hearing components can observe sounds without direct references to every emitter. `EnemyHearing` filters those stimuli by distance and asks `EnemyMovement` to investigate accepted positions; active visual pursuit retains priority over sound.

The player emits a fixed-radius stimulus when transitioning from airborne to grounded. A separate `TestSoundStimulusInput` adapter provides manual keyboard emission in the sandbox, while a selected-object Gizmo visualizes each emitter's configured radius. Walls currently do not occlude semantic sound.

### Visual debugging

- `Debug.DrawRay` visualizes runtime interaction and sight checks.
- `OnDrawGizmosSelected` visualizes configured detection and capture ranges.
- Navigation visualization displays baked walkable space.

These diagnostics are development aids and are not player-facing presentation.

## Dependency rules

- Use `RequireComponent` plus `GetComponent` for mandatory collaborators on the same GameObject.
- Use serialized fields for scene-level collaborators configured elsewhere.
- Use interfaces where callers should not depend on concrete behavior.
- Use events when a publisher should announce a fact without knowing its consumers.
- Avoid global searches and singleton infrastructure until an actual cross-scene requirement exists.

## Deferred decisions

- ScriptableObject-backed item and enemy configuration.
- A general-purpose state-machine framework.
- Dependency injection containers.
- Saving architecture.
- Package boundaries and semantic versioning.

These may become useful, but the current project does not yet justify their cost.
