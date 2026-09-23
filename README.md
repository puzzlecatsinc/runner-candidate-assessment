# Runner engineering assessment

Make this runner more fun to play on a phone. Choose one worthwhile improvement, build it, and take it through testing and an on-device build. You decide the details: a new mechanic, a better choice during a run, pacing, controls, or feedback are all fair game.

Spend **4–6 hours**, excluding tool installation and initial setup. Keep it small, make reasonable assumptions, and tell us what you left out. Changes to code, scenes, prefabs, and UI are welcome. Reuse the supplied assets; custom art is not expected.

## What we expect

- **A finished improvement.** We should be able to discover it, play with touch controls, finish or lose, and retry. Check several runs and background/resume.
- **Attention to mobile performance.** This prototype has runtime issues. Investigate and improve one, which can be part of your feature. Choose a frame-rate target and show a before/after measurement on the same phone with comparable settings and gameplay load. Include a busy section; record the scenario, duration, and a profiler capture or equivalent evidence. Explain any limits to your results.
- **Reliable code.** Add automated tests for your changes, including a meaningful edge or failure case. Keep existing tests passing unless you intentionally change the behavior. Record manual checks and known problems.
- **AI-assisted work you understand.** Use an AI coding tool during the assessment. Choose where it helps and check its output. Briefly explain what it contributed and one example of how you verified or corrected it. No chat transcripts or special workflow documents are needed. Contact us beforehand if tool access is a problem.

You do not need to fix every issue in the starter. Prioritize, and explain your choices. We will review gameplay judgment, device delivery, measured performance, code quality, testing, and your understanding of the work. More features or extra hours do not earn extra credit.

## Get started

1. Open the project in **Unity 6000.3.3f1**, with Android or iOS build support. Choose one platform.
2. Open `Assets/Crowd Runner Kit/Scenes/Game Scene.unity` (already included in build settings). Gameplay code and existing tests are under `Assets/Crowd Runner Kit/`.
3. Build and launch the starter on a physical phone before beginning the timebox. Contact us if setup fails or you need a device or another arrangement.
4. Create a **private GitHub repository** from the starter. Keep the starter as your base, invite the reviewers from your assessment email, and work on a branch.

Run tests in Unity's Test Runner, or use a Bash-compatible shell:

```bash
bash scripts/smoke_check.sh
bash scripts/run_visible_tests.sh
```

The test script finds the matching Unity version in its standard macOS Hub location. Otherwise set `UNITY_EDITOR` to the editor executable. Run any additional PlayMode tests in Unity. The smoke check only checks repository hygiene.

## Send us

- A pull request against the starter in your private repository.
- An Android build or an agreed iOS installation route, shared privately, plus a short recording of your change and the start/play/end/retry loop on a physical phone.
- [APPROACH.md](APPROACH.md), completed with brief notes and links to your test and performance evidence.

Keep build binaries, Unity caches, local IDE files, and signing credentials out of Git. Include `.meta` files with changed assets.

We will spend about 40 minutes playing the game and discussing your decisions, tests, and measurements. We may ask you to work through a small change to your feature using your usual tools.
