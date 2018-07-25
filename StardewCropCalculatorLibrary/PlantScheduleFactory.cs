using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewCropCalculatorLibrary
{
    public class PlantScheduleFactory
    {
        /// <summary>
        /// The number of days to schedule.  Generally the length of a season, or 28 days.
        /// </summary>
        public readonly int numDays;

        #region MemoTable This table holds the best planting schedule, as well as additional statistics about each day. Indexed by day; 0th element is unused.
        struct MemoTable
        {
            // The most profitable crop to plant on a particular day.
            public PlantSchedule schedule;

            /// The maximum CUMULATIVE investment multiple that can be obtained from planting this crop on a particular day. (E.g., 3x means tripling you money.)
            /// Accounts for reinvestment of interest.
            public float[] cumMultiple;

            /// Indicates the primary days you'll be able to buy seeds on, because you got a payday from a harvest.
            /// The other days are there just in case you get extra money some other way.
            /// (This schedule assumes you reinvestment all profit as instructed by the schedule)
            public bool[] canPlant;
        }
        #endregion

        private MemoTable memo;

        public PlantScheduleFactory(int numDays)
        {
            if (numDays < 1) AppUtils.Fail("PlantScheduleFactory(numDays) - numDays must be greater than 0");

            this.numDays = numDays;

            // Initialize memo table
            memo.schedule = new PlantSchedule(numDays);

            memo.cumMultiple = new float[numDays + 1]; // array goes from day 0 (unused) to day numDays for ease of access
            memo.cumMultiple = memo.cumMultiple.Select(x => 1F).ToArray(); // If you don't plant, your money multiplies by 1x (no change)

            memo.canPlant = new bool[numDays + 1];
            memo.canPlant = memo.canPlant.Select(x => false).ToArray();
        }

        /// <summary>
        /// Calculates the best crop to plant on each day.
        /// Uses a dynamic programming algorithm which memoizes the best schedule by iterating through it backward.
        /// </summary>
        /// <param name="crops">The crops which are available to be purchased</param>
        /// <param name="schedule">Outputs the best schedule</param>
        /// <param name="day">The day to start planting on</param>
        /// <returns></returns>
        public float GetBestSchedule(List<Crop> crops, out PlantSchedule schedule)
        {
            // Find the best crop for each day, starting from end of season.
            for (int day = numDays; day > 0; --day)
            {
                foreach (var crop in crops)
                {
                    int numHarvests = crop.NumHarvests(day, numDays);
                    float cropMultiple = crop.InvestmentMultiple(day, numDays); // sell price divided by cost

                    // Base case: no harvest
                    if (crop.NumHarvests(day, numDays) < 1)
                        continue;

                    // Calculate the cumulative investment multiple of the planting.
                    float cumInvestmentMultiple = 0;
                    var harvestDay = day + crop.timeToMaturity;
                    for (var harvest = 1; harvest <= numHarvests; ++harvest)
                    {
                        cumInvestmentMultiple += (cropMultiple/numHarvests) * memo.cumMultiple[harvestDay];

                        harvestDay += crop.yieldRate;
                    }

                    // Update best crop for the current day.
                    if (cumInvestmentMultiple > memo.cumMultiple[day])
                    {
                        memo.cumMultiple[day] = cumInvestmentMultiple;
                        memo.schedule.SetCrop(day, crop);
                    }
                }
            }

            // Return memo, which is now filled out with the best schedule.
            schedule = new PlantSchedule(memo.schedule);
            return memo.cumMultiple[1];
        }
    }
}
