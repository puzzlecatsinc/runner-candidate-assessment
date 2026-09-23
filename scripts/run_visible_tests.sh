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

UNITY_VERSION="$(grep '^m_EditorVersion:' "$PROJECT_VERSION_FILE" | head -1 | cut -d: -f2- | tr -d '[:space:]')"
if [[ -z "$UNITY_VERSION" ]]; then
  echo "Could not parse Unity version from ProjectSettings/ProjectVersion.txt." >&2
  exit 1
fi

UNITY_EDITOR="${UNITY_EDITOR:-}"
if [[ -z "$UNITY_EDITOR" ]]; then
  candidate="/Applications/Unity/Hub/Editor/${UNITY_VERSION}/Unity.app/Contents/MacOS/Unity"
  if [[ -x "$candidate" ]]; then
    UNITY_EDITOR="$candidate"
  fi
fi

if [[ -z "$UNITY_EDITOR" || ! -x "$UNITY_EDITOR" ]]; then
  cat >&2 <<MSG
Unity editor not found.

Install Unity ${UNITY_VERSION} or run with:

  UNITY_EDITOR="/path/to/Unity.app/Contents/MacOS/Unity" scripts/run_visible_tests.sh
MSG
  exit 1
fi

mkdir -p "$RESULTS_DIR" "$LOG_DIR"

echo "Running Unity EditMode tests with: $UNITY_EDITOR"
"$UNITY_EDITOR" \
  -batchmode \
  -nographics \
  -projectPath "$ROOT_DIR" \
  -runTests \
  -testPlatform EditMode \
  -testResults "$RESULTS_FILE" \
  -logFile "$LOG_FILE"

if [[ ! -s "$RESULTS_FILE" ]]; then
  echo "Unity exited without writing EditMode test results: $RESULTS_FILE" >&2
  echo "Unity log: $LOG_FILE" >&2
  exit 1
fi

echo "Unity EditMode test results: $RESULTS_FILE"
echo "Unity log: $LOG_FILE"
