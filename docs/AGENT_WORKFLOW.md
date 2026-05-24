# Agent Workflow Expectations

AI assistants and coding agents are allowed in this challenge. Strong candidates do not just ask an agent to write code. They create useful context, decompose work, review output, and verify results.

This document describes the artifacts we want to see if you use agents. Equivalent formats are fine; use your real workflow.

## Recommended artifact set

```text
agent-workflow/
  AGENTS.md
  PLAN.md
  AI_USAGE.md
  REVIEW.md
  VALIDATION.md
```

You may also use repo-root files such as `CLAUDE.md`, Cursor rules, Codex instructions, or tool-specific equivalents. If you already have a mature workflow, submit that instead.

## `AGENTS.md` or equivalent

This should tell a fresh agent how to work in the repo.

Good contents:

- Project overview.
- Relevant directories and files.
- Build/test commands.
- Coding conventions.
- Scope boundaries and areas not to touch.
- Validation checklist.
- Known Unity pitfalls.
- Git hygiene and generated-file warnings.

Weak contents:

- Generic “act like a senior engineer” instructions.
- No repo-specific commands.
- No validation steps.
- Instructions that encourage broad rewrites or unreviewed output.

## `PLAN.md`

Before implementation, capture:

- Files inspected.
- Root-cause hypothesis.
- Intended change.
- Test strategy.
- Risks.
- What is explicitly out of scope.

## `AI_USAGE.md`

Disclose enough to make your process reviewable:

- Tools/agents used.
- What you asked them to do.
- What context you provided.
- What they got wrong.
- What you rejected or rewrote.
- How you personally verified the result.

## `REVIEW.md`

Self-review the final diff like you are reviewing another engineer or agent:

- Summary of changes.
- Why this approach is safe.
- Edge cases considered.
- Files intentionally not changed.
- Risks / unknowns.
- Any suspicious generated code or asset churn removed before submission.

## `VALIDATION.md`

Include exact evidence:

- Commands run.
- Test results.
- Unity editor version or environment blocker.
- Manual gameplay checks.
- Screenshots/log paths if relevant.

“No errors seen” is not enough. A reviewer should be able to understand what was actually verified.

## Multi-agent exercise signal

If you used more than one agent, explain how you split responsibilities. Good decomposition usually looks like:

- Investigation agent: read the code and identify root cause/file touchpoints.
- Implementation agent: make a narrow change against the plan.
- Review agent: inspect the diff for bugs, overreach, missing tests, and Unity-generated noise.

Prevent telephone-game degradation by giving each agent the original source context and explicit output requirements, not only another agent's summary.

## Strong signal

- Context-first workflow: inspect, plan, implement, review, verify.
- Repo-specific instructions and validation gates.
- Clear examples of AI being wrong and the candidate catching it.
- Small code changes with strong tests and evidence.
- Markdown artifacts that a future engineer or agent could reuse.

## Reject signal

- “I used Cursor/Claude/Codex” with no details.
- Prompt-spamming until code compiled.
- No plan, no review notes, no validation record.
- Cannot explain the final patch without AI.
- Broad generated rewrites or duplicate/fallback paths.
- Generated Unity/IDE/cache files checked into the submission.
