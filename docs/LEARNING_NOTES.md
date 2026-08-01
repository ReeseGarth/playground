# Key Learning Notes

## Unity components and hierarchy

- A C# `MonoBehaviour` file defines a component type; attaching it creates a component instance on a GameObject.
- Parent transforms define coordinate spaces for their children.
- An empty parent can provide a meaningful pivot, such as a door hinge.
- Scene hierarchy represents runtime composition; source folders represent code responsibility.

## Coordinate systems

- Unity conventionally treats one unit as approximately one meter.
- Local position and rotation are relative to the parent; world values include the entire ancestor chain.
- `transform.forward` is the object’s positive local Z direction expressed in world space.
- UI anchors select a reference in the parent; the pivot selects which point inside the child rectangle is placed relative to that anchor.

## Time and movement

- Speeds expressed per second multiply by `Time.deltaTime`.
- Input System mouse delta already represents accumulated pointer movement for the frame, so it is treated as a displacement rather than multiplied by `deltaTime` again.
- Direct transform assignment is authoritative and does not provide collision-aware navigation.
- `CharacterController.Move`, Rigidbody movement, and `NavMeshAgent` solve different movement problems.

## Physics and queries

- Colliders describe shapes; they do not independently prevent arbitrary transform assignments.
- Trigger colliders report overlaps without physically blocking movement.
- Raycasts can resolve behavior from a hit collider’s parent hierarchy.
- Gameplay raycasts should deliberately decide whether trigger volumes participate.
- Ground-based ranges often use planar distance so differing transform heights do not distort horizontal gameplay thresholds.

## Lifecycle

- Typical initial order is `Awake`, `OnEnable`, then `Start`.
- `Update` runs each rendered frame while active and enabled.
- `OnEnable` and `OnDisable` run on active-state transitions, not every frame.
- Event subscription in `OnEnable` should be paired with unsubscription in `OnDisable`.
- Play-mode scene edits are generally temporary unless explicitly applied or saved outside Play mode.

## Interfaces and events

- Interfaces act like ports: callers depend on a capability rather than a concrete component.
- A property is suitable when data depends only on the object; a method is suitable when calculating it requires interaction context.
- Events communicate that something happened without coupling the publisher to its consumers.
- The null-conditional invocation `ItemAdded?.Invoke(itemId)` skips invocation when there are no subscribers; it does not control duplicate inventory behavior.
- A static event can broadcast a transient gameplay fact without scene references between every publisher and listener, but listeners must still unsubscribe when disabled.

## Semantic audio

- Gameplay sound data can be independent of audible presentation: AI can reason about a stimulus position and radius without requiring an `AudioClip` or `AudioSource`.
- A small immutable `readonly struct` is suitable for transient value-like event data that does not need a GameObject or Unity lifecycle.
- `OnDrawGizmosSelected` provides editor-only spatial diagnostics without adding player-facing geometry.
- Separating a reusable emitter from temporary keyboard input lets player, environment, and test components share one publishing capability.
- Comparing the previous and current grounded states detects landing as a transition rather than treating a button press as the gameplay event.
- A first-sample guard prevents startup state from being mistaken for an airborne-to-grounded transition.

## Navigation and AI

- A baked NavMesh represents static traversable space, not live scene geometry.
- `NavMeshAgent` owns route calculation and steering; AI code supplies destinations.
- Perception can be composed as progressively more expensive tests: range, field of view, then line of sight.
- Different acquisition and tracking angles create hysteresis and make detection less brittle.
- Explicit states prevent patrol, chase, and investigation from issuing conflicting behavior.
- Last-known-position tracking is fairer than giving an enemy the player’s live position after visibility is lost.

## Reusability

- Generic names do not guarantee generic behavior; current components still assume specific input, hierarchy, physics, and UI choices.
- Reuse should be validated by a second consumer before extracting a stable package.
- Prefer several focused capability packages over one universal gameplay framework.
