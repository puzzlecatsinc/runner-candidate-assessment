# Runner Engineering Challenge

This repository is a Unity runner-game codebase used for a focused engineering assessment. The goal is not to rebuild the game or polish visuals. The goal is to show how you work inside an existing Unity project: read the code, make a small production-quality improvement, optimize for real mobile performance, find production issues that only show up on device, test it, explain the tradeoffs, and demonstrate how you use AI/agents without losing engineering judgment.

## Assignment

Start here:

1. Read `ISSUE.md` for the task and acceptance criteria.
2. Inspect the existing Unity scripts before changing code.
3. Identify the likely performance and production-risk touchpoints before implementation.
4. Make a focused implementation in the existing runner game code.
5. Add tests that prove the behavior you changed.
6. Build and run the game on a physical mobile device.
7. Complete `APPROACH.md` with decisions, validation evidence, AI/tool usage, and agent workflow artifacts.
8. If you use coding agents, include the workflow artifacts described in `docs/AGENT_WORKFLOW.md`.

Recommended timebox: **2-3 hours**. If you hit the timebox, stop cleanly and document what is done vs. what remains.

## What we are evaluating

We are not looking for the most code. We are looking for high judgment-per-token:

- Can you understand an existing codebase quickly?
- Can you make a small, correct, maintainable change?
- Can you verify behavior with evidence instead of vibes?
- Can you improve performance without speculative rewrites?
- Can you catch production/device issues that do not appear in editor-only testing?
- Can you use AI/agents as leverage without blindly trusting them?
- Can you leave behind workflow artifacts that make future work easier, reviewable, and reproducible?

## AI/tool policy

AI assistants and coding agents are allowed and encouraged. We care about whether you can use tools responsibly, not whether you can pretend they do not exist.

Rules:

- You may use AI for code reading, implementation help, test ideas, debugging, review, and documentation.
- You must understand and be able to explain every submitted change without tool assistance.
- Do not paste in broad rewrites you cannot justify.
- Disclose meaningful AI/tool usage in `APPROACH.md`.
- If you run agents, include the agent instructions, task briefs, plans, review notes, or equivalent markdown artifacts you used.
- Validate outputs with tests, logs, or direct Unity/editor evidence.

## Agent workflow requirement

If you use agents such as Claude Code, Codex, Cursor agents, OpenCode, or similar tools, submit an `agent-workflow/` folder or equivalent markdown files. See `docs/AGENT_WORKFLOW.md` for the expected shape.

Example artifact set:

```text
agent-workflow/
  AGENTS.md
  PLAN.md
  AI_USAGE.md
  REVIEW.md
  VALIDATION.md
```

You do not need to use these exact filenames if your workflow has an equivalent structure. We are not grading prompt polish. We are grading whether your workflow made the work more reliable, reviewable, and reproducible.

## Project setup

- Unity version: **2021.3.38f1**
- Open the Unity project at this repository root.
- Main game scene: `Assets/Crowd Runner Kit/Scenes/Game Scene.unity`
- Main gameplay scripts:
  - `Assets/Crowd Runner Kit/Scripts/PlayerController.cs`
  - `Assets/Crowd Runner Kit/Scripts/Gate.cs`
  - `Assets/Crowd Runner Kit/Scripts/GameManager.cs`
  - `Assets/Crowd Runner Kit/Scripts/Runner.cs`

This repo intentionally includes third-party/sample assets. You should not need to edit them for this challenge.

## Physical-device run required

You must create a development build, install it on a physical mobile device, and run through the relevant gameplay path on device. Editor-only validation, simulator-only validation, and “it compiled” are not enough for this assessment.

Include evidence in `APPROACH.md`:

- Platform and device model/OS version.
- Build type and Unity build settings used.
- Build/install result, with log path or copied command output where available.
- A screenshot, short recording, or clear written reproduction notes from the on-device run.
- Runtime logs from Android `logcat`, Xcode device console, or Unity player logs where available.
- Performance observations: FPS/jank, obvious GC spikes, memory pressure, loading stalls, touch/input responsiveness, or other device-only symptoms.
- Production issues found, fixed, or explicitly triaged.

If you do not have access to a physical device, tell us before starting. A final submission without on-device evidence is incomplete.

## Visible checks

Run the lightweight repo/doc smoke check:

```bash
scripts/smoke_check.sh
```

Run Unity EditMode tests:

```bash
scripts/run_visible_tests.sh
```

If Unity is installed somewhere non-standard, set `UNITY_EDITOR`:

```bash
UNITY_EDITOR="/path/to/Unity.app/Contents/MacOS/Unity" scripts/run_visible_tests.sh
```

Then perform the required physical-device build/run and document the evidence in `APPROACH.md`.

## Submission requirements

Submit a branch or PR containing:

- Your focused code changes.
- EditMode tests for the behavior you changed.
- Completed `APPROACH.md`.
- On-device build/run evidence and a short production/performance issue report.
- Agent workflow artifacts if you used agents, or a short note if you used AI only interactively.
- Validation evidence: command output, logs, screenshots, recordings, or exact environment blockers.
- No generated Unity cache/build files (`Library/`, `Temp/`, `Logs/`, `UserSettings/`, `.csproj`, `.sln`, etc.).

## Evaluation

See `docs/CANDIDATE_RUBRIC.md` for the public scoring rubric. In short: small, correct, tested, explainable, and reproducible beats ambitious and unverified.
