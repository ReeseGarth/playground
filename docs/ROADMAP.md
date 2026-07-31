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

## Near-term roadmap

1. Add enemy state presentation and basic audio cues.
2. Replace two fixed patrol fields with an ordered waypoint collection.
3. Add simple investigation scanning at the last known position.
4. Introduce sound as semantic AI perception, separate from audible playback.
5. Add lightweight automated tests where logic becomes sufficiently independent of scene setup.
6. Begin a small Night Shift vertical-slice scene only after the reusable sandbox remains understandable.

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

- Scent trails made from decaying spatial samples.
- Multiple scent types with different monster preferences.
- Scent masking, contamination, cleaning chemicals, and environmental dispersal.
- First-, third-, and second-person starter sandboxes composed from focused packages rather than one large framework.

## Current limitations

- Input is polled directly from keyboard and mouse rather than configured through actions and rebinding.
- Inventory uses case-sensitive string IDs and supports membership, not quantities.
- The door assumes its authored starting rotation is closed.
- The dynamic door is not yet represented as a runtime NavMesh obstacle or AI-operable door.
- AI forgets the player after a short investigation and does not search its surroundings.
- AI has direct serialized references to one player and two patrol points.
- Scene references are intentionally Inspector-wired and do not yet have validation tooling.
- The current components are reusable candidates, not a stable public package API.
