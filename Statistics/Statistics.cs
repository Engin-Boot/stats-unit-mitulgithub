using System;
using System.Collections.Generic;

namespace Statistics
{
    public interface IAlerter
    {
        void work();
    }
    
    public class StatsComputer
    {
        public float average;
        public float min;
        public float max;
        public void CalculateStatistics(List<float> numbers) {
            //Implement statistics here
            float average = 0, min = float.MaxValue, max = float.MinValue;
            float sum = 0;
            for(int i = 0; i < numbers.Count; ++i)
            {
                sum += numbers[i];
                if(numbers[i] > max){
                    max = numbers[i];
                }
                if(numbers[i] < min)
                {
                    min = numbers[i];
                }
            }
            average = sum/numbers.Count;
        }
    }

        public class EmailAlert : IAlerter
        {
            public bool emailSent;
            public EmailAlert(){
                emailSent = false;
            }
            public void work()
            {
                emailSent = true;
            }
        }
        public class LEDAlert : IAlerter
        {
            public bool ledGlows;
            public LEDAlert(){
                ledGlows = false;
            }
            public void work()
            {
                ledGlows = true;
            }
        }

        public class StatsAlerter
        {
            public float maxThreshold;
            IAlerter[] alerter;
            public StatsAlerter(float maxThreshold, IAlerter[] alerters)
            {
                this.maxThreshold = maxThreshold;
                this.alerter = alerters;
            }

            public void checkAndAlert(List<float> thresholds)
            {
                for(int i = 0; i < thresholds.Count; ++i){
                    if(thresholds[i] > maxThreshold){
                        for(int j = 0; j < alerter.Length; ++j){
                            alerter[j].work();
                        }
                        break;
                    }
                }
            }
        }
    
}
