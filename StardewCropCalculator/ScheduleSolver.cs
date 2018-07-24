using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewCropCalculator
{
    /// <summary>
    /// This is the key class - it computes the planting schedule which maximizes profit.
    /// </summary>
    static class ScheduleSolver
    {
        /// <summary>
        /// The maximized wealth FACTOR from planting any crops throughout the season, starting from the day given. Note that return is a measure of total wealth. So 2.0 means money was doubled.
        /// </summary>
        /// <param name="day">The day to start planting</param>
        /// <param name="crops"></param>
        static public float MaxTotalWealth(List<Crop> crops, out PlantSchedule outSchedule, int day = 1, int maxDays = 28)
        {
            outSchedule = new PlantSchedule(maxDays);

            // The best known wealth multipler for this day.
            float maxTotalWealth = 1;

            foreach (Crop crop in crops)
            {
                PlantSchedule hypotheticalSchedule = new PlantSchedule(maxDays);
                hypotheticalSchedule.AddCrop(day, crop);

                // Extract some needed values
                float cropReturn = crop.CropReturn(day, maxDays);
                float cropWealth = cropReturn + 1;
                int numHarvests = crop.NumHarvests(day, maxDays);

                // Base case: return of 0.0 means no profit was made, and wealth stayed the same.  It then case, don't buy and instead just say wealth stayed at 1x.
                if (cropReturn <= 0 || numHarvests <= 0)
                {
                    continue;
                }

                float totalCropWealth = 0;

                // Add up the cumulative interest from a single planting
                for (int harvest = 1; harvest <= numHarvests; ++harvest)
                {
                    int nextDay = day + crop.timeToMaturity + (harvest - 1) * crop.yieldRate;

                    if (nextDay > maxDays)
                        AppUtils.Fail("Critical Error! NextDay exceed maxDays: " + nextDay);

                    // Reinvest return.
                    PlantSchedule reinvestmentSchedule;
                    float reinvestedCropWealth = reinvestedCropWealth = MaxTotalWealth(crops, out reinvestmentSchedule, nextDay, maxDays);

                    // Combine reinvestments for total solution from planting this crop on this day.
                    totalCropWealth += (cropWealth / numHarvests) * reinvestedCropWealth; // Compute total wealth, then subtract 1
                    hypotheticalSchedule.Merge(reinvestmentSchedule);
                }

                if (totalCropWealth > maxTotalWealth)
                {
                    maxTotalWealth = totalCropWealth;
                    outSchedule = new PlantSchedule(hypotheticalSchedule);
                }
            }

            return maxTotalWealth;
        }
    }
}
