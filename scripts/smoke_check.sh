#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT_DIR"

required_files=(
  "README.md"
  "ISSUE.md"
  "APPROACH.md"
  "docs/APPROACH_TEMPLATE.md"
  "docs/AGENT_WORKFLOW.md"
  "docs/CANDIDATE_RUBRIC.md"
  "ProjectSettings/ProjectVersion.txt"
  "Packages/manifest.json"
  "Assets/Crowd Runner Kit/Scripts/PlayerController.cs"
  "Assets/Crowd Runner Kit/Scripts/Gate.cs"
  "Assets/Crowd Runner Kit/Scripts/GameManager.cs"
)

for path in "${required_files[@]}"; do
  if [[ ! -f "$path" ]]; then
    echo "Missing required file: $path" >&2
    exit 1
  fi
done

if ! grep -q "2021.3.38f1" ProjectSettings/ProjectVersion.txt; then
  echo "Unexpected Unity version. Expected 2021.3.38f1 in ProjectSettings/ProjectVersion.txt" >&2
  exit 1
fi

if ! grep -q "Make Runner Crowd Math Testable" ISSUE.md; then
  echo "ISSUE.md does not appear to describe the runner crowd-math challenge." >&2
  exit 1
fi

if ! grep -q "Agent workflow requirement" README.md; then
  echo "README.md does not appear to include the agent workflow requirement." >&2
  exit 1
fi

if ! grep -q "Agent Workflow Expectations" docs/AGENT_WORKFLOW.md; then
  echo "docs/AGENT_WORKFLOW.md does not appear to include the expected agent workflow guidance." >&2
  exit 1
fi

tracked_generated=$(git ls-files | grep -E '(^|/)(Library|Temp|Logs|UserSettings|obj)/|\.csproj$|\.sln$' || true)
if [[ -n "$tracked_generated" ]]; then
  echo "Generated Unity/IDE files are tracked and should not be submitted:" >&2
  echo "$tracked_generated" >&2
  exit 1
fi

git diff --check -- README.md ISSUE.md APPROACH.md docs/APPROACH_TEMPLATE.md docs/AGENT_WORKFLOW.md docs/CANDIDATE_RUBRIC.md scripts/run_visible_tests.sh scripts/smoke_check.sh

echo "Smoke check passed. Run scripts/run_visible_tests.sh for Unity EditMode tests."
