#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT_DIR"

required_files=(
  "README.md"
  "APPROACH.md"
  "scripts/run_visible_tests.sh"
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

if ! grep -q "6000.3.3f1" ProjectSettings/ProjectVersion.txt; then
  echo "Unexpected Unity version. Expected 6000.3.3f1 in ProjectSettings/ProjectVersion.txt" >&2
  exit 1
fi

tracked_generated=$(git ls-files | grep -E '(^|/)(Library|Temp|Logs|UserSettings|obj)/|\.csproj$|\.sln$' || true)
if [[ -n "$tracked_generated" ]]; then
  echo "Generated Unity/IDE files are tracked and should not be submitted:" >&2
  echo "$tracked_generated" >&2
  exit 1
fi

git diff --check

echo "Repository hygiene check passed. This does not validate gameplay or the device build."
echo "Run bash scripts/run_visible_tests.sh for Unity EditMode tests."
