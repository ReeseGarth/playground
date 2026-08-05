# Roadmap and Current Status

## Purpose

This repository is both a Unity learning environment and an incubator for reusable gameplay building blocks. The long-term game is **Night Shift**, a polished single-player experience that can prove mechanics later reusable by **Monster Janitors**.

The guiding process remains:

1. Introduce one Unity concept.
2. Build the smallest useful implementation.
3. Verify it in a sandbox.
4. Record limitations exposed by real use.
5. Refactor only when requirements justify it.

## Completed

- Transform experiments: movement, rotation, scaling, and material mutation.
- First-person movement using `CharacterController`.
- Mouse look with separate body yaw and camera pitch.
- Gravity and jumping.
- Raycast interaction with contextual prompts.
- Interface-based interactables.
- Smooth, hinged doors.
- Minimal unique-item inventory.
- Pickups and key-gated doors.
- Objective progression driven by inventory events and an exit trigger.
- TextMeshPro interaction, objective, and game-over UI.
- NavMesh baking and agent-driven patrol movement.
- AI perception using distance, field of view, line of sight, and tracking hysteresis.
- Patrol, chase, and last-known-position investigation states.
- Ordered patrol routes with any number of scene-authored waypoints.
- Arrival-based investigation scanning.
- Event-driven enemy state presentation using color and spatial audio cues.
- Semantic sound stimuli, distance-based enemy hearing, sound-driven investigation, and player landing noise.
- A runtime assembly boundary and Edit Mode tests for sound-driven enemy state transitions.
- Player capture, pause, and scene restart.
- Reorganization of reusable candidates under `Assets/Common`.

## Current playable loop

```text
Explore
  -> collect key
  -> unlock and open door
  -> avoid or trigger enemy detection
  -> reach the exit or get captured
  -> restart after capture
```

## Milestone cadence

Near-term work is divided into small, playable checkpoints. Each checkpoint should
be tested in the sandbox, reviewed as an uncommitted diff, documented, and committed
before beginning the next one. A checkpoint should usually introduce one concept or
one coherent behavior rather than an entire production-ready system.

## Near-term milestones

### 1. Night Shift vertical-slice kickoff

- Define one small objective-driven scenario.
- Create a Night Shift scene separate from the movement sandbox.
- Reuse Common components without treating them as stable package APIs.
- Feed real level requirements back into the sandbox components deliberately.

## Longer-term systems

- More expressive inventory and item definitions.
- Multiple objectives and objective composition.
- Doors that coordinate with navigation at runtime.
- Enemy state animation and audio.
- Saving and loading.
- Settings and input rebinding.
- Accessibility and broader UI polish.
- Packaging stable capabilities as custom Unity Package Manager packages.

## Ideas retained for later

- Scale landing-noise radius from fall or impact speed so larger drops create stronger stimuli.
- Scent trails made from decaying spatial samples.
- Multiple scent types with different monster preferences.
- Scent masking, contamination, cleaning chemicals, and environmental dispersal.
- First-, third-, and second-person starter sandboxes composed from focused packages rather than one large framework.

## Current limitations

- Input is polled directly from keyboard and mouse rather than configured through actions and rebinding.
- Inventory uses case-sensitive string IDs and supports membership, not quantities.
- The door assumes its authored starting rotation is closed.
- The dynamic door is not yet represented as a runtime NavMesh obstacle or AI-operable door.
- AI performs one full scan, then forgets the player without a broader search strategy.
- AI has direct serialized references to one player and a scene-authored patrol route.
- Audible enemy cues do not yet represent sounds that AI can perceive.
- Landing noise uses a fixed hearing radius regardless of fall distance or impact speed.
- Scene references are intentionally Inspector-wired and do not yet have validation tooling.
- The current components are reusable candidates, not a stable public package API.
