#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
PROJECT_VERSION_FILE="$ROOT_DIR/ProjectSettings/ProjectVersion.txt"
RESULTS_DIR="$ROOT_DIR/TestResults"
LOG_DIR="$ROOT_DIR/Logs"
RESULTS_FILE="$RESULTS_DIR/EditModeResults.xml"
LOG_FILE="$LOG_DIR/visible-editmode-tests.log"

if [[ ! -f "$PROJECT_VERSION_FILE" ]]; then
  echo "Could not find ProjectSettings/ProjectVersion.txt. Run this from the Unity project checkout." >&2
  exit 1
fi

UNITY_EDITOR="${UNITY_EDITOR:-}"
if [[ -z "$UNITY_EDITOR" ]]; then
  for candidate in     "/Applications/Unity/Hub/Editor/2021.3.38f1/Unity.app/Contents/MacOS/Unity"     "/Applications/Unity/Unity.app/Contents/MacOS/Unity"
  do
    if [[ -x "$candidate" ]]; then
      UNITY_EDITOR="$candidate"
      break
    fi
  done
fi

if [[ -z "$UNITY_EDITOR" || ! -x "$UNITY_EDITOR" ]]; then
  cat >&2 <<'MSG'
Unity editor not found.

Install Unity 2021.3.38f1 or run with:

  UNITY_EDITOR="/path/to/Unity.app/Contents/MacOS/Unity" scripts/run_visible_tests.sh
MSG
  exit 1
fi

mkdir -p "$RESULTS_DIR" "$LOG_DIR"

echo "Running Unity EditMode tests with: $UNITY_EDITOR"
"$UNITY_EDITOR"   -batchmode   -nographics   -quit   -projectPath "$ROOT_DIR"   -runTests   -testPlatform EditMode   -testResults "$RESULTS_FILE"   -logFile "$LOG_FILE"

echo "Unity EditMode test results: $RESULTS_FILE"
echo "Unity log: $LOG_FILE"
