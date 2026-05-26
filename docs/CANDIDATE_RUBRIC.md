# Candidate Rubric

Total: 100 points.

## Functional correctness: 25 points

- 7: Preserves existing runner/gate gameplay behavior where required.
- 6: Correctly handles `ADD`, `MULTIPLY`, and `DIVIDE` count transitions.
- 5: Handles max-crowd clamping, minimum-crowd behavior, and lethal outcomes deliberately.
- 4: Runtime code uses one clean rule path instead of duplicated or competing logic.
- 3: Avoids regressions in UI/audio/FOV/death side effects.

## Performance and on-device production readiness: 25 points

- 7: Builds, installs, and runs the game on a physical mobile device with clear evidence.
- 5: Identifies a real performance risk in the runner/gate path and improves it without broad speculative rewrites.
- 4: Captures useful device evidence: device/OS, build settings, gameplay path, logs, screenshots/recording, or profiler notes.
- 4: Spots production issues that editor-only testing can miss, such as device logs, startup/runtime errors, input/aspect problems, jank, GC, memory, or thermal symptoms.
- 3: Separates issues fixed in-scope from broader issues triaged for follow-up.
- 2: Avoids performance theater: no unmeasured architecture churn, fake benchmarks, or changes that make behavior harder to maintain.

## Tests and verification: 15 points

- 6: Adds focused EditMode tests for deterministic crowd-count rules.
- 4: Covers meaningful edge cases, not only happy paths.
- 3: Runs the provided visible checks or clearly documents local environment blockers.
- 2: Keeps generated Unity/IDE/cache files out of the submission.

## Code quality and architecture fit: 15 points

- 5: Small, readable, maintainable implementation with clear names.
- 4: Separates pure rules from MonoBehaviour scene side effects.
- 2: Fits the existing project style without unnecessary framework churn.
- 2: Avoids broad asset/prefab/scene changes unless justified.
- 2: Handles invalid or boundary inputs explicitly.

## Agent workflow and AI judgment: 15 points

- 3: Uses AI/agents with repo-specific context rather than vague prompts.
- 3: Provides useful workflow artifacts such as `AGENTS.md`, `PLAN.md`, `AI_USAGE.md`, `REVIEW.md`, `VALIDATION.md`, or equivalents when agents are used.
- 3: Shows investigation → plan → implementation → review → validation discipline.
- 2: Identifies what AI/agents got wrong, missed, or suggested that was rejected.
- 2: Reviews generated output critically and avoids unnecessary churn or fallback paths.
- 2: Explains how the workflow could be reused by a future engineer or agent.

## Engineering judgment, communication, and submission hygiene: 5 points

- 1: Private repository and pull request setup follows the README instructions.
- 1: `APPROACH.md` explains what changed and why.
- 1: Tradeoffs and limitations are honest and grounded in the code.
- 1: Candidate can explain the final solution without tool assistance.
- 1: Scope control is strong: fixes the task, not the whole repo.

## Strong hire signals

- Reads existing code before changing it.
- Makes deterministic game rules testable without loading a full Unity scene.
- Builds and validates on a real device, not just in the editor.
- Finds production/performance issues with concrete evidence.
- Chooses the smallest useful refactor and wires runtime through it cleanly.
- Uses AI as leverage but catches weak or hallucinated suggestions.
- Runs agents with clear task briefs, source context, and validation gates.
- Produces concise, evidence-backed handoff notes.
- Leaves reusable markdown artifacts that make future human/agent work faster.

## Reject signals

- Submission repository is public, missing reviewer access, missing a PR, or not based on the provided starter repo.
- Broad rewrite with weak tests.
- Parallel/fallback logic where old and new behavior diverge.
- Cannot explain their own solution.
- Touches third-party assets, scenes, or prefabs without a clear reason.
- Claims behavior is fixed without running tests or providing evidence.
- Does not build and run on a physical device.
- Provides vague performance claims with no device evidence.
- Ignores obvious production issues visible in device logs or gameplay.
- Checks in generated Unity cache/build/IDE files.
- Says “I used AI” but cannot show prompts, task briefs, review notes, or validation.
- Trusts agent output without review.
