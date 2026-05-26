using System;

/// <summary>
/// Describes the result of applying a deterministic crowd-count rule.
/// </summary>
public enum CrowdCountOutcomeType
{
    NoOp,
    Changed,
    Dead
}

/// <summary>
/// Pure value object returned by crowd-count calculations.
/// </summary>
public readonly struct CrowdCountOutcome
{
    public CrowdCountOutcome(int targetCount, CrowdCountOutcomeType outcomeType)
    {
        TargetCount = targetCount;
        OutcomeType = outcomeType;
    }

    public int TargetCount { get; }
    public CrowdCountOutcomeType OutcomeType { get; }

    public bool IsNoOp => OutcomeType == CrowdCountOutcomeType.NoOp;
    public bool IsChanged => OutcomeType == CrowdCountOutcomeType.Changed;
    public bool IsDead => OutcomeType == CrowdCountOutcomeType.Dead;
}

/// <summary>
/// Pure, deterministic crowd-count gate rules. This class intentionally has no Unity dependencies.
/// </summary>
public static class CrowdCountCalculator
{
    /// <summary>
    /// Applies an additive gate. Positive additions clamp to maxCrowd; subtracting all remaining runners is lethal.
    /// </summary>
    public static CrowdCountOutcome Add(int currentCount, int amount, int maxCrowd)
    {
        int normalizedCurrent = NormalizeCurrentCount(currentCount);
        if (normalizedCurrent == 0)
        {
            return new CrowdCountOutcome(0, CrowdCountOutcomeType.NoOp);
        }

        int normalizedMax = NormalizeMaxCrowd(maxCrowd);

        long requestedTarget = (long)normalizedCurrent + amount;
        if (amount < 0 && requestedTarget <= 0)
        {
            return new CrowdCountOutcome(0, CrowdCountOutcomeType.Dead);
        }

        int targetCount = ClampAliveTarget(requestedTarget, normalizedMax);
        return CreateNonLethalOutcome(normalizedCurrent, targetCount);
    }

    /// <summary>
    /// Applies a multiplicative gate. Non-positive multipliers are invalid and deliberately no-op.
    /// </summary>
    public static CrowdCountOutcome Multiply(int currentCount, int multiplier, int maxCrowd)
    {
        int normalizedCurrent = NormalizeCurrentCount(currentCount);
        if (multiplier <= 0 || normalizedCurrent == 0)
        {
            return new CrowdCountOutcome(normalizedCurrent, CrowdCountOutcomeType.NoOp);
        }

        int normalizedMax = NormalizeMaxCrowd(maxCrowd);
        long requestedTarget = (long)normalizedCurrent * multiplier;
        int targetCount = ClampAliveTarget(requestedTarget, normalizedMax);
        return CreateNonLethalOutcome(normalizedCurrent, targetCount);
    }

    /// <summary>
    /// Applies a division gate. Division floors and clamps live crowds to at least one runner.
    /// Non-positive divisors are invalid and deliberately no-op.
    /// </summary>
    public static CrowdCountOutcome Divide(int currentCount, int divisor, int maxCrowd)
    {
        int normalizedCurrent = NormalizeCurrentCount(currentCount);
        if (divisor <= 0 || normalizedCurrent == 0)
        {
            return new CrowdCountOutcome(normalizedCurrent, CrowdCountOutcomeType.NoOp);
        }

        int normalizedMax = NormalizeMaxCrowd(maxCrowd);
        int targetCount = normalizedCurrent / divisor;
        if (targetCount < 1)
        {
            targetCount = 1;
        }

        targetCount = ClampAliveTarget(targetCount, normalizedMax);
        return CreateNonLethalOutcome(normalizedCurrent, targetCount);
    }

    private static int NormalizeCurrentCount(int currentCount)
    {
        return Math.Max(0, currentCount);
    }

    private static int NormalizeMaxCrowd(int maxCrowd)
    {
        return Math.Max(1, maxCrowd);
    }

    private static int ClampAliveTarget(long requestedTarget, int maxCrowd)
    {
        if (requestedTarget < 1)
        {
            return 1;
        }

        if (requestedTarget > maxCrowd)
        {
            return maxCrowd;
        }

        return (int)requestedTarget;
    }

    private static CrowdCountOutcome CreateNonLethalOutcome(int currentCount, int targetCount)
    {
        CrowdCountOutcomeType outcomeType = targetCount == currentCount
            ? CrowdCountOutcomeType.NoOp
            : CrowdCountOutcomeType.Changed;

        return new CrowdCountOutcome(targetCount, outcomeType);
    }
}
