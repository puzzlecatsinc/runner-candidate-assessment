# Candidate Rubric

Total: 100 points.

## Functional correctness: 30 points

- 8: Preserves existing runner/gate gameplay behavior where required.
- 7: Correctly handles `ADD`, `MULTIPLY`, and `DIVIDE` count transitions.
- 7: Handles max-crowd clamping, minimum-crowd behavior, and lethal outcomes deliberately.
- 4: Runtime code uses one clean rule path instead of duplicated or competing logic.
- 4: Avoids regressions in UI/audio/FOV/death side effects.

## Tests and verification: 20 points

- 8: Adds focused EditMode tests for deterministic crowd-count rules.
- 5: Covers meaningful edge cases, not only happy paths.
- 4: Runs the provided visible checks or clearly documents local environment blockers.
- 3: Keeps generated Unity/IDE/cache files out of the submission.

## Code quality and architecture fit: 20 points

- 7: Small, readable, maintainable implementation with clear names.
- 5: Separates pure rules from MonoBehaviour scene side effects.
- 3: Fits the existing project style without unnecessary framework churn.
- 3: Avoids broad asset/prefab/scene changes unless justified.
- 2: Handles invalid or boundary inputs explicitly.

## Agent workflow and AI judgment: 20 points

- 4: Uses AI/agents with repo-specific context rather than vague prompts.
- 4: Provides useful workflow artifacts such as `AGENTS.md`, `PLAN.md`, `AI_USAGE.md`, `REVIEW.md`, `VALIDATION.md`, or equivalents when agents are used.
- 4: Shows investigation → plan → implementation → review → validation discipline.
- 3: Identifies what AI/agents got wrong, missed, or suggested that was rejected.
- 3: Reviews generated output critically and avoids unnecessary churn or fallback paths.
- 2: Explains how the workflow could be reused by a future engineer or agent.

## Engineering judgment and communication: 10 points

- 3: `APPROACH.md` explains what changed and why.
- 3: Tradeoffs and limitations are honest and grounded in the code.
- 2: Candidate can explain the final solution without tool assistance.
- 2: Scope control is strong: fixes the task, not the whole repo.

## Strong hire signals

- Reads existing code before changing it.
- Makes deterministic game rules testable without loading a full Unity scene.
- Chooses the smallest useful refactor and wires runtime through it cleanly.
- Uses AI as leverage but catches weak or hallucinated suggestions.
- Runs agents with clear task briefs, source context, and validation gates.
- Produces concise, evidence-backed handoff notes.
- Leaves reusable markdown artifacts that make future human/agent work faster.

## Reject signals

- Broad rewrite with weak tests.
- Parallel/fallback logic where old and new behavior diverge.
- Cannot explain their own solution.
- Touches third-party assets, scenes, or prefabs without a clear reason.
- Claims behavior is fixed without running tests or providing evidence.
- Checks in generated Unity cache/build/IDE files.
- Says “I used AI” but cannot show prompts, task briefs, review notes, or validation.
- Trusts agent output without review.
