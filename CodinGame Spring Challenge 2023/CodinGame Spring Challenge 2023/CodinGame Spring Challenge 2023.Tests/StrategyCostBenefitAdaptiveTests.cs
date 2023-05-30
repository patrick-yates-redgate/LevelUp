using System.Collections.Generic;
using CodinGame_Spring_Challenge_2023.Strategy;
using FluentAssertions;
using NUnit.Framework;

namespace CodinGame_Spring_Challenge_2023.Tests
{
    [TestFixture]
    public class StrategyCostBenefitAdaptiveTests
    {
        /*
         *
    public static float CalculatePotentialBenefit(List<(int index, float resources)> crystalResources,
        List<(int index, float resources)> eggResources, float myAnts,
        int numLocations, int numFrames, 0)
         * 
         */

        [Test]
        public void ThatWithOneCrystalResourceAndOneAntAtOneLocationOverOneFrames_WeGetAPotentialBenefitOf1()
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 1f) },
                new List<(int index, float resources)>(), 1, 1, 1, 0).Should().Be(1f);
        }

        [Test]
        public void ThatWeStillGetAPotentialBenefitOf1With2AntsDueToResourceLimit()
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 1f) },
                new List<(int index, float resources)>(), 2, 1, 1, 0).Should().Be(1f);
        }

        [Test]
        public void ThatWeStillGetAPotentialBenefitOf1WithExcessResourcesWithoutTheExtraAnt()
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 100f) },
                new List<(int index, float resources)>(), 1, 1, 1, 0).Should().Be(1f);
        }

        [Test]
        public void ThatWeGetAPotentialBenefitOf2WithExcessResourcesAnd2Ants()
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 2f) },
                new List<(int index, float resources)>(), 2, 1, 1, 0).Should().Be(2f);
        }

        [Test]
        public void ThatWeGetAPotentialBenefitOf1WithExcessResourcesAnd2AntsButSpreadOver2Locations()
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 2f) },
                new List<(int index, float resources)>(), 2, 2, 1, 0).Should().Be(1f);
        }

        [TestCase(1, 1f)]
        [TestCase(10, 10f)]
        [TestCase(100, 100f)]
        public void
            ThatWithLargeCrystalResourceAndOneAntAtOneLocationOverMultipleFrames_WeGetAPotentialBenefitRelativeToNumberOfFrames(
                int numFrames, float benefit)
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 200f) },
                new List<(int index, float resources)>(), 1, 1, numFrames, 0).Should().Be(benefit);
        }

        [TestCase(1, 1f)]
        [TestCase(2, 1f + (1f + 0.5f))]
        [TestCase(3, 2.5f + (1.5f + 0.75f))]
        public void ThatWhenWeHaveOneCrystalAndOneAntTheResourcesAreExponential(int numFrames, float benefit)
        {
            StrategyCostBenefitAdaptive.CalculatePotentialBenefit(new List<(int index, float resources)> { (0, 10f) },
                new List<(int index, float resources)> { (1, 10f) }, 2, 2, numFrames, 0).Should().Be(benefit);
        }
    }
}