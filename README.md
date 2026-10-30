# SimpleAnim

Turn-based grid movement prototype built in **Unity 6** with **C#**.

The goal is to validate a movement system where the player advances **one cell per input**,
in **four directions**, controlled by **swipe on Android** and **keyboard / mouse drag on Windows**.

> **Status: work in progress.** This repository is the skeleton of the prototype, not a finished game.
> The roadmap below reflects what is planned, not what already works.

---

## Current state

- [x] Unity 6.3 project set up with URP and the new Input System
- [x] Initial movement test: a character walks through 3 waypoints
- [ ] Grid model (cells, world/cell conversion)
- [ ] One step per input, in 4 directions
- [ ] Unified input (swipe + keyboard) without platform `#if`
- [ ] Obstacles and blocked-cell feedback
- [ ] Undo of the last step
- [ ] Turn-based enemies

---

## Controls (current prototype)

| Action | Windows | Android |
| --- | --- | --- |
| Look at next point | Space (press) | Touch (press) |
| Move to next point | Space (release) | Touch (release) |

Controls will be replaced by grid-based steering once the movement layer is implemented.

---

## Tech stack

| | |
| --- | --- |
| Engine | Unity **6000.3.10f1** (Unity 6.3) |
| Render pipeline | URP 17.3 |
| Input | Input System 1.18 |
| Language | C# |

---

## Getting started

1. Install Unity **6000.3.10f1** through Unity Hub.
2. Clone the repository and open the folder with Unity Hub (`Add project from disk`).
3. Open `Assets/Game/Scenes/SampleScene.unity`.
4. Press **Play**.

The `Library` folder is not versioned; Unity regenerates it on first open, so the initial import takes a few minutes.

---

## Project structure

```
Assets/
└─ Game/
   ├─ Materials/
   ├─ Scenes/
   │  └─ SampleScene.unity
   └─ Scripts/
      └─ Character.cs        # current prototype script
Packages/
ProjectSettings/
```

Planned structure once the refactor lands:

```
Assets/Game/Scripts/
├─ Input/        # unified direction source (swipe / keyboard)
├─ Grid/         # cells, coordinates, grid map
├─ Commands/     # MoveCommand, CommandInvoker (undo)
├─ Movement/     # step validation
├─ Player/       # controller, motor, state machine
├─ Data/         # ScriptableObject configuration
└─ Events/       # player events
```

---

## Design notes

The prototype is intentionally small, but the movement layer is being designed with a few
patterns that are justified by the requirements, not by fashion:

- **Command** — every step is an undoable, queueable action.
- **State** — the player can only accept input when it is allowed to act (`Idle`, `Stepping`, `Blocked`).
- **Strategy** — input sources and cell validation are interchangeable.
- **Observer** — other systems react to a step without the player knowing about them.

Deliberately **not** used: DI containers, object pooling, ECS. There is nothing in a 10×10 board
that justifies their cost.

The logical position of the character is a cell (`Vector2Int`); the `Transform` is only its visual
representation. That separation is what makes undo, obstacles and turn order tractable.

---

## License

MIT — see [LICENSE](LICENSE).
