using System;
using Xunit;
using Statistics;
using System.Collections.Generic;

namespace Statistics.Test
{
    public class StatsUnitTest
    {
        public void ReportsAverageMinMax()
        {
            var statsComputer = new StatsComputer();
            var lis = new List<float>{1.5f, 8.9f, 3.2f, 4.5f};
            statsComputer.CalculateStatistics(lis);
            float epsilon = 0.001F;
            Assert.True(Math.Abs(statsComputer.average - 4.525) <= epsilon);
            Assert.True(Math.Abs(statsComputer.max - 8.9) <= epsilon);
            Assert.True(Math.Abs(statsComputer.min - 1.5) <= epsilon);
        }
        [Fact]
        public void ReportsNaNForEmptyInput()
        {
            var statsComputer = new StatsComputer();
            statsComputer.CalculateStatistics(new List<float>{});
            //All fields of computedStats (average, max, min) must be
            //double.NaN (not-a-number), as described in
            //https://docs.microsoft.com/en-us/dotnet/api/system.float.nan?view=netcore-3.1
        }
        [Fact]
        public void RaisesAlertsIfMaxIsMoreThanThreshold()
        {
            var emailAlert = new EmailAlert();
            var ledAlert = new LEDAlert();
            IAlerter[] alerters = {emailAlert, ledAlert};

            const float maxThreshold = 10.2f;
            var statsAlerter = new StatsAlerter(maxThreshold, alerters);
            statsAlerter.checkAndAlert(new List<float>{0.2f, 11.9f, 4.3f, 8.5f});

            Assert.True(emailAlert.emailSent);
            Assert.True(ledAlert.ledGlows);
        }
    }
}
