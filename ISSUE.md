# Candidate Task: Make Runner Crowd Math Testable, Faster, and Safer on Device

## Context

This is a small hyper-casual runner prototype. The core player loop is driven by a crowd of runners that grows or shrinks when the player passes through gates:

- `Gate.cs` chooses and applies `ADD`, `MULTIPLY`, and `DIVIDE` operations.
- `PlayerController.cs` mutates the live runner list, updates UI/audio/FOV side effects, and handles death.
- The current implementation mixes deterministic crowd-count rules with Unity scene side effects, making edge cases hard to test and easy to regress.

Your task is to improve this area without rewriting the game, while also proving the result on a physical device and looking for production issues that editor-only testing can miss.

Your submission must be made from your own private GitHub repository and pull request, following the setup instructions in `README.md`. Repository setup is part of the assessment.

## Task

Refactor the crowd-count/gate behavior so the rules are deterministic, testable, faster on real devices, and safer at edge cases, then wire the existing runtime behavior through that rule layer.

A good solution usually introduces a small pure C# rule/calculator class such as `CrowdCountCalculator`, but the exact shape is up to you. The important part is that the count math can be tested without instantiating the full scene.

## Requirements

### 1. Preserve the existing game loop

Keep the existing runner gameplay intact:

- Gates still add, multiply, and divide the crowd.
- Crowd size still respects `PlayerController.MaxCrowd`.
- The existing `PlayerController` methods remain usable from `Gate` and other scene scripts.
- Existing UI/audio/FOV side effects still happen when the crowd actually changes or dies.

### 2. Extract and test the rules

Move the crowd-count decision logic into code that can be covered by EditMode tests without loading the full game scene.

Cover at least these cases:

- Add positive runners up to `MaxCrowd`.
- Add negative runners without removing below zero.
- Negative add that would remove the final runner is handled intentionally.
- Multiply clamps to `MaxCrowd`.
- Divide floors as expected and never creates fewer than one runner unless the operation is explicitly lethal.
- Invalid or boundary inputs are handled deliberately rather than accidentally.

### 3. Fix the death/no-op edge case deliberately

Today `PlayerController.DivideCharacters` kills the player whenever the crowd count is one, even if division would otherwise clamp to one. Decide the correct behavior and implement it consistently.

Recommended behavior: a non-lethal divide gate should not kill a one-runner crowd; lethal outcomes should come from subtractive gates or runner/blocker collisions. If you choose a different behavior, justify it in `APPROACH.md` and cover it with tests.

### 4. Optimize a real runtime path

Do not optimize by guessing or rewriting the whole project. Inspect the existing runner/gate path and make at least one focused performance-minded improvement that fits the task. Examples of acceptable directions:

- Reduce avoidable allocations or repeated work in crowd/gate transitions.
- Keep deterministic count math out of MonoBehaviour side effects so it can be tested cheaply.
- Avoid duplicate logic that makes future runtime behavior harder to reason about.
- Preserve visual/gameplay behavior while making the underlying path simpler and cheaper.

In `APPROACH.md`, explain what you considered a performance risk, what you changed, and why the change is safe.

### 5. Build and run on a physical device

You must produce a development build and run the game on a physical mobile device. Simulator-only and editor-only validation do not satisfy this requirement.

Document:

- Platform and device model/OS version.
- Unity build target and build type.
- Whether the build installed and launched successfully.
- The exact gameplay path tested, including gates/crowd changes touched by your code.
- Any logs captured from Android `logcat`, Xcode device console, or Unity player logs.
- Performance observations such as FPS/jank, GC spikes, memory pressure, loading stalls, overheating, input lag, or visual/runtime glitches.

A submission without physical-device evidence is incomplete unless you contacted us before starting and got explicit approval for an alternate validation plan.

### 6. Spot production issues on device

While testing on device, look beyond the code path you changed. Note production risks you see, even if you do not fix all of them within the timebox. Good examples:

- Device-only exceptions, warnings, missing assets, shader/material issues, or logging noise.
- Bad first-run behavior, slow startup, touch/input problems, orientation/aspect-ratio issues, safe-area problems, or UI cutoffs.
- Frame hitches, obvious GC spikes, memory growth, battery/thermal issues, or heavy per-frame work.
- Build setting issues that would matter for release readiness.

Fix issues that are tightly connected to your code change. For broader issues, document the evidence, likely cause, and recommended next step.

### 7. Show your agent operating model

If you use AI assistants or coding agents, include the workflow artifacts that helped you run them responsibly. At minimum, your notes should make clear:

- What context you gave the agent before implementation.
- Whether you created or used repo-specific instructions such as `AGENTS.md`, `CLAUDE.md`, Cursor rules, Codex instructions, or task-specific markdown files.
- How you decomposed investigation, implementation, review, and validation.
- What AI/agent output you rejected or corrected.
- How you verified the final result yourself.

See `docs/AGENT_WORKFLOW.md` for examples. Equivalent formats are fine.

### 8. Keep scope tight

Do not turn this into a broad Unity cleanup.

Avoid:

- Rebuilding the game architecture.
- Replacing prefabs/scenes/materials unless absolutely necessary.
- Editing third-party/sample asset packages.
- Adding fallback/parallel code paths that leave old and new logic competing.
- Checking in generated Unity files or local IDE state.

## Acceptance criteria

A reviewer should be able to verify that:

- The candidate created a private GitHub submission repository from this starter repo.
- The reviewer GitHub accounts were invited with sufficient access.
- The work is submitted as a pull request from `candidate/<name>` into `main` in that private repo.
- The project opens in Unity 6000.3.3f1.
- `scripts/smoke_check.sh` passes.
- `scripts/run_visible_tests.sh` runs EditMode tests, or you document exactly why it cannot run in your environment.
- A development build was installed and run on a physical device, with evidence in `APPROACH.md`.
- The crowd-count rules are covered by focused tests.
- Runtime code uses the tested rule path.
- The submission includes performance reasoning and a device production-issue pass.
- `APPROACH.md` is completed with decisions, tradeoffs, and validation evidence.
- AI/tool usage is disclosed clearly.
- Agent workflow artifacts are included if agents were used, or the absence of agents is stated plainly.

## Hidden review focus

We will look for engineering judgment, not just final behavior:

- Did you read the existing code before changing it?
- Did you isolate deterministic logic from Unity side effects cleanly?
- Did you optimize a real runtime path rather than doing speculative cleanup?
- Did you build and run on device and catch production issues that editor-only testing would miss?
- Did you avoid broad churn in assets and third-party code?
- Did your tests catch the important edge cases?
- Can you explain what changed without leaning on AI output?
- Did you run agents with useful context and validation gates, or did you prompt-spam until something appeared to work?
- Did your markdown artifacts make the work easier for another engineer or agent to review tomorrow?

## Suggested workflow

1. Create your private GitHub submission repository from this starter repo.
2. Invite the requested reviewer GitHub usernames.
3. Create branch `candidate/<your-name>`.
4. Inspect `PlayerController.cs` and `Gate.cs`.
5. Write a short investigation/plan note before editing code.
6. If using agents, create or update agent-facing instructions/task briefs.
7. Write or sketch tests for the crowd-count rules.
8. Extract the smallest useful rule layer.
9. Wire `PlayerController` through that rule layer.
10. Review the diff critically, including any AI-generated code.
11. Run the smoke check and Unity EditMode tests.
12. Build, install, and play the relevant path on a physical device.
13. Fill out `APPROACH.md` and include agent workflow artifacts if relevant.
14. Open a pull request from `candidate/<your-name>` into `main` in your private repo.
