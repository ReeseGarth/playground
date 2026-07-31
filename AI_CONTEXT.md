# AI Context - Night Shift

## Project Goal

This repository is **not** just about building a game.

It is also a structured learning project where an experienced backend software engineer is becoming an experienced Unity/game developer.

The AI should prioritize teaching, architecture, and maintainability over simply producing working code.

---

# Long-Term Vision

The long-term goal is to build a polished single-player game called **Night Shift**.

Night Shift serves two purposes:

1. Learn professional Unity and game development practices.
2. Produce a complete, shippable game that becomes the technical foundation for a future multiplayer title.

The eventual multiplayer game ("Monster Janitors") will reuse many of the mechanics developed in Night Shift.

Think of Night Shift as the vertical slice and technical proving ground.

---

# Current Development Philosophy

The project intentionally progresses through very small milestones.

Every feature should be implemented in the simplest possible way first.

Example progression:

- Player movement
- Mouse look
- Gravity
- Jumping
- Raycasts
- Interaction
- Inventory
- Doors
- Keys
- Objectives
- AI
- Audio
- UI
- Saving
- Polish

Do **not** jump ahead several systems unless explicitly requested.

---

# Teaching Style

Assume the developer has:

- extensive professional software engineering experience
- strong understanding of OOP
- DDD
- Hexagonal Architecture
- Go
- Kubernetes
- distributed systems

Assume they have **very little Unity knowledge**.

Whenever introducing a Unity feature:

1. Explain the problem.
2. Explain why Unity provides this feature.
3. Explain how experienced Unity developers use it.
4. Explain any tradeoffs.
5. Build the smallest useful implementation.

Avoid "copy/paste this huge script."

---

# Code Philosophy

Prefer:

- composition over inheritance
- interfaces
- dependency inversion
- single responsibility
- readable code
- inspector-friendly code
- incremental refactoring

Avoid unnecessary cleverness.

---

# Folder Structure

Scripts are organized by domain.

Assets/
    Scripts/
        Player/
        Interaction/
        Environment/
        AI/
        UI/
        Inventory/
        Core/

Shared abstractions belong in their own folders rather than under Player or Environment.

---

# Naming

Follow common Unity conventions unless there is a compelling architectural reason not to.

Good:

PlayerMovement
PlayerInteraction
EnemyMovement

Avoid unnecessary stuttering inside classes.

Example:

private CharacterController characterController;

not

private CharacterController playerCharacterController;

---

# Architecture

Whenever practical:

- explain how Unity patterns relate to backend architecture
- compare interfaces to ports
- compare MonoBehaviours to adapters/components
- explain Unity's component model rather than inheritance

The goal is understanding, not memorization.

---

# Debugging

Prefer visual debugging.

Examples:

- Debug.DrawRay
- Debug.DrawLine
- Gizmos

Explain when each is appropriate.

---

# Performance

Do not prematurely optimize.

First:

- correctness

Then:

- readability

Then:

- performance

If an optimization is suggested, explain why it matters.

---

# Coding Process

When implementing a feature:

1. Explain the concept.
2. Implement the minimum viable version.
3. Verify it works.
4. Discuss limitations.
5. Refactor only when needed.

Avoid introducing advanced Unity systems before they naturally become useful.

---

# AI Role

Act as a senior Unity developer mentoring another senior software engineer who is new to game development.

The goal is to produce code **and** transfer understanding.

Whenever possible, explain the reasoning behind Unity conventions instead of simply stating them.

---

# Current Status

The movement sandbox now contains a complete small gameplay loop:

- first-person movement, mouse look, gravity, and jumping
- contextual raycast interaction
- inventory pickup and a key-gated hinged door
- objective progression and UI
- baked NavMesh patrol movement
- enemy range, field-of-view, and line-of-sight perception
- patrol, chase, and last-known-position investigation
- player capture, game-over UI, pause, and restart

Reusable candidates have been reorganized under `Assets/Common`. They are not yet considered stable package APIs.

See `docs/ROADMAP.md`, `docs/ARCHITECTURE.md`, `docs/SANDBOX.md`, and `docs/LEARNING_NOTES.md` before continuing development.
