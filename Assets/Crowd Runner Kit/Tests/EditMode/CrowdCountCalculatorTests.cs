using NUnit.Framework;

public class CrowdCountCalculatorTests
{
    [Test]
    public void PositiveAddClampsToMaxCrowd()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Add(currentCount: 95, amount: 10, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Changed, outcome.OutcomeType);
        Assert.AreEqual(100, outcome.TargetCount);
    }

    [Test]
    public void PositiveAddAtMaxIsNoOp()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Add(currentCount: 100, amount: 5, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.NoOp, outcome.OutcomeType);
        Assert.AreEqual(100, outcome.TargetCount);
    }

    [Test]
    public void NegativeAddRemovesWithoutUnderflow()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Add(currentCount: 5, amount: -2, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Changed, outcome.OutcomeType);
        Assert.AreEqual(3, outcome.TargetCount);
    }

    [Test]
    public void NegativeAddRemovingFinalRunnerIsIntentionallyLethal()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Add(currentCount: 1, amount: -1, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Dead, outcome.OutcomeType);
        Assert.AreEqual(0, outcome.TargetCount);
    }

    [Test]
    public void NegativeAddBeyondCrowdIsIntentionallyLethal()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Add(currentCount: 3, amount: -10, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Dead, outcome.OutcomeType);
        Assert.AreEqual(0, outcome.TargetCount);
    }

    [Test]
    public void MultiplyClampsToMaxCrowd()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Multiply(currentCount: 50, multiplier: 3, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Changed, outcome.OutcomeType);
        Assert.AreEqual(100, outcome.TargetCount);
    }

    [Test]
    public void DivideFloorsResult()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Divide(currentCount: 5, divisor: 2, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Changed, outcome.OutcomeType);
        Assert.AreEqual(2, outcome.TargetCount);
    }

    [Test]
    public void DivideNeverCreatesFewerThanOneRunner()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Divide(currentCount: 2, divisor: 10, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.Changed, outcome.OutcomeType);
        Assert.AreEqual(1, outcome.TargetCount);
    }

    [Test]
    public void DivideOneRunnerIsNoOpNotDeath()
    {
        CrowdCountOutcome outcome = CrowdCountCalculator.Divide(currentCount: 1, divisor: 2, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.NoOp, outcome.OutcomeType);
        Assert.AreEqual(1, outcome.TargetCount);
    }

    [Test]
    public void InvalidMultiplyAndDivideInputsAreNoOps()
    {
        CrowdCountOutcome multiplyOutcome = CrowdCountCalculator.Multiply(currentCount: 4, multiplier: 0, maxCrowd: 100);
        CrowdCountOutcome divideOutcome = CrowdCountCalculator.Divide(currentCount: 4, divisor: 0, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.NoOp, multiplyOutcome.OutcomeType);
        Assert.AreEqual(4, multiplyOutcome.TargetCount);
        Assert.AreEqual(CrowdCountOutcomeType.NoOp, divideOutcome.OutcomeType);
        Assert.AreEqual(4, divideOutcome.TargetCount);
    }

    [Test]
    public void BoundaryMaxCrowdNormalizesToOneSafely()
    {
        CrowdCountOutcome addOutcome = CrowdCountCalculator.Add(currentCount: 1, amount: 5, maxCrowd: 0);
        CrowdCountOutcome divideOutcome = CrowdCountCalculator.Divide(currentCount: 3, divisor: 2, maxCrowd: -10);

        Assert.AreEqual(CrowdCountOutcomeType.NoOp, addOutcome.OutcomeType);
        Assert.AreEqual(1, addOutcome.TargetCount);
        Assert.AreEqual(CrowdCountOutcomeType.Changed, divideOutcome.OutcomeType);
        Assert.AreEqual(1, divideOutcome.TargetCount);
    }

    [Test]
    public void BoundaryZeroOrNegativeCurrentDoesNotCreateRunners()
    {
        CrowdCountOutcome zeroOutcome = CrowdCountCalculator.Add(currentCount: 0, amount: 5, maxCrowd: 100);
        CrowdCountOutcome negativeOutcome = CrowdCountCalculator.Multiply(currentCount: -3, multiplier: 2, maxCrowd: 100);

        Assert.AreEqual(CrowdCountOutcomeType.NoOp, zeroOutcome.OutcomeType);
        Assert.AreEqual(0, zeroOutcome.TargetCount);
        Assert.AreEqual(CrowdCountOutcomeType.NoOp, negativeOutcome.OutcomeType);
        Assert.AreEqual(0, negativeOutcome.TargetCount);
    }
}
