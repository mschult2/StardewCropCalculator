using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewCropCalculator
{
    class Crop
    {
        public int timeToMaturity { get; set; }
        public int yieldRate { get; set; }
        public string name { get; set; }

        public float buyPrice { get; set; }
        public float sellPrice { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeToMaturity">time to crop's first yield</param>
        /// <param name="yieldRate">days between succesive yields after maturity</param>
        /// <param name="buyPrice">price that the seed was bought for</param>
        /// <param name="sellPrice">price that the crop will be sold at</param>
        public Crop(string name, int timeToMaturity, int yieldRate, float buyPrice, float sellPrice)
        {
            this.name = name;
            this.timeToMaturity = timeToMaturity;
            this.yieldRate = yieldRate;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
        }

        /// <summary>
        /// The measure of PROFIT (as a multiple of the original cost).
        /// So a return of 2.0 means your profit is double the cost, which means you tripled your money. A return of 0 means no profit was made (and no money lost).
        /// A return of -1 means all money was lost.
        /// </summary>
        /// <param name="day">day crop is planted</param>
        /// <param name="maxDays">days in the season</param>
        /// <returns></returns>
        public float CropReturn(int day, int maxDays)
        {
            return ((NumHarvests(day, maxDays) * sellPrice) - buyPrice) / buyPrice;
        }

        /// <summary>
        /// The number of harvests you get out of planting a crop on a given day.
        /// </summary>
        /// <param name="dayPlanted">day crop is planted</param>
        /// <param name="maxDays">days in the season</param>
        /// <returns></returns>
        public int NumHarvests(int dayPlanted, int maxDays)
        {
            int numHarvests = (int)((maxDays - dayPlanted - timeToMaturity + yieldRate) / yieldRate); // rounds to floor

            return numHarvests < 0 ? 0 : numHarvests;
        }
    }
}

