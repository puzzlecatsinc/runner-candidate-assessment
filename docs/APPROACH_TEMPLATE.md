# Candidate Approach Template — Runner Crowd Math

Fill this out before submission. Keep it short, factual, and evidence-backed. If you used AI or coding agents, be specific enough that a reviewer can understand your workflow and verify that you owned the final result.

## Summary

- Changed:
  - TODO
- Why this scope:
  - TODO

## First 20 minutes / investigation notes

- Files inspected before implementation:
  - TODO
- Root-cause hypothesis:
  - TODO
- What I decided not to touch:
  - TODO

## Issues addressed

- [ ] Count math is isolated from Unity scene side effects.
- [ ] Add gates clamp positive adds to `MaxCrowd`.
- [ ] Negative add gates remove runners deliberately and treat final-runner removal as lethal.
- [ ] Multiply gates clamp to `MaxCrowd`.
- [ ] Divide gates floor results and do **not** kill a one-runner crowd for non-lethal divides.
- [ ] Invalid/boundary inputs are handled deliberately.
- [ ] Runtime methods still preserve UI/audio/FOV/death side effects.

## Code touched

- Files changed:
  - TODO
- Files intentionally not changed:
  - Third-party/sample assets
  - Prefabs/scenes/materials
  - Generated Unity/IDE/cache files

## Design decisions

- Pure rule layer shape:
  - TODO
- Runtime wiring through `PlayerController`:
  - TODO
- One-runner divide decision:
  - TODO
- Invalid input behavior:
  - TODO

## Tests and validation

Commands run:

```bash
scripts/smoke_check.sh
# result: TODO

scripts/run_visible_tests.sh
# result: TODO
```

If `scripts/run_visible_tests.sh` cannot run locally, include the exact blocker and evidence:

```text
TODO: e.g. Unity 2021.3.38f1 not installed at expected path; installed editors: ...
```

Focused test coverage:

- [ ] Positive add clamps to max.
- [ ] Negative add removes without underflow.
- [ ] Negative add lethal final removal.
- [ ] Multiply clamps to max.
- [ ] Divide floors result.
- [ ] Divide of one runner remains one for non-lethal divide.
- [ ] Invalid divisor/multiplier/boundary values handled deliberately.

Manual Unity checks, if any:

- TODO

Known limitations / follow-ups:

- TODO

## AI/tool usage

- Tools used:
  - TODO
- How I used them:
  - Code reading:
  - Implementation:
  - Debugging:
  - Review:
- Key prompts/task briefs or summary of prompts:
  - TODO
- What the tools got wrong or missed:
  - TODO
- AI/agent suggestions I rejected:
  - TODO
- How final output was validated by me:
  - TODO

## Agent workflow artifacts

If you used coding agents, list the markdown/workflow files you created or used. If you did not use agents, say so.

- Agent instructions used (`AGENTS.md`, `CLAUDE.md`, Cursor rules, Codex instructions, or equivalent):
  - TODO
- Plan/task brief files:
  - TODO
- Review/checklist files:
  - TODO
- Validation evidence files:
  - TODO
- What I would automate or codify next for future agents:
  - TODO
