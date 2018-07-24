using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewCropCalculator
{
    /// <summary>
    /// Daily planting schedule for the given time period.
    /// </summary>
    class PlantSchedule
    {
        // The crops planted on a given day. Index represents the day, from day 1 to day n. 0 index is unused.
        // Technically there can be multiple crops planted on a given day, but the max-profit algorithm (cf: MaxTotalWealth()) will likely never decide to do so.
        List<Crop>[] plantingSchedule;
        int maxDays;

        public int MaxDays
        {
            get { return maxDays; }
        }

        public PlantSchedule(int maxDays)
        {
            this.maxDays = maxDays;
            plantingSchedule = new List<Crop>[maxDays + 1];

            // Empty-list-init all elements
            for (int day = 1; day <= maxDays; ++day)
            {
                plantingSchedule[day] = new List<Crop>();
            }
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public PlantSchedule(PlantSchedule otherSchedule)
        {
            this.maxDays = otherSchedule.maxDays;
            plantingSchedule = new List<Crop>[this.maxDays + 1];

            for (int day = 1; day <= this.maxDays; ++day)
            {
                this.plantingSchedule[day] = new List<Crop>(otherSchedule.GetCrops(day));
            }
        }

        /// <summary>
        /// Returns a copy of the crop list for that day
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public List<Crop> GetCrops(int day)
        {
            return new List<Crop>(plantingSchedule[day]);
        }

        /// <summary>
        /// Add crop to planting schedule.
        /// </summary>
        public void AddCrop(int day, Crop crop)
        {
            if (plantingSchedule[day] == null)
                plantingSchedule[day] = new List<Crop>();


            plantingSchedule[day].Add(crop);
        }

        /// <summary>
        /// Remove crop to planting schedule.
        /// </summary>
        //public void RemoveCrop(int day, Crop crop)
        //{
        //    if (plantingSchedule[day] == null)
        //        plantingSchedule[day] = new List<Crop>();


        //    plantingSchedule[day].Remove(crop); // Removes first occurence
        //}

        /// <summary>
        /// Add the input schedule to this schedule.
        /// </summary>
        /// <param name="otherSchedule"></param>
        public void Merge(PlantSchedule otherSchedule)
        {
            if (otherSchedule.maxDays != this.maxDays)
            {
                AppUtils.Fail("PlantingSchedule.Merge(otherSchedule) - otherSchedule and this schedule must have same number of days! Operation failed");
            }

            for (int day = 1; day <= otherSchedule.maxDays; ++day)
            {
                var otherDayCrops = otherSchedule.GetCrops(day);

                foreach (var crop in otherDayCrops)
                {
                    this.AddCrop(day, crop);
                }
            }
        }

        /// <summary>
        /// Print the crops planted on each day.
        /// </summary>
        public void PrettyPrint()
        {
            for (int day = 1; day < plantingSchedule.Length; ++day)
            {
                Debug.WriteLine("Day " + day + ": ");

                foreach (Crop crop in plantingSchedule[day])
                {
                    Debug.Write(crop.name + ", ");
                }

                Debug.WriteLine("--");
            }
        }
    }
}
