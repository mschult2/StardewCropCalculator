# Install

Download Setup.msi from the [Release page](https://github.com/mschult2/StardewCropCalculator/releases), then double-click this file to begin installation.

# Summary

This Windows desktop application computes the most profitable crop to plant on any given day, in the game Stardew Valley.
It also tells you how much profit you will make (if you follow the given scheule). E.g., an "investment multiple" of 45x means if you started the season with 1000 g, then you will end with 45,000 g.

# Instructions

1. On the left, you can add and remove crops from the crop list.
1. When you're ready, press the "Compute Schedule" button. A planting schedule will appear on the calendar to the right.
1. If a crop is listed on the calendar (and not in parentheses), this is what you should plant on that day. The algorithm assumes you spend all of your harvest money purchasing and planting the prescribed seed on that day. But it's ok if you spend only as much as you're comfortable spending.
1. Non-plant days may still show the most profitable crop, but listed in parentheses. This is in case you find money through some other means, and want to know what the best crop is to buy with your additional wealth. The investment multiple does not account for these days.
1. Days with no crop listed are days you should **definitely not** plant on! Usually it's because the crop will not yield enough harvests to be profitable before the season is over and all your crops die. 

# Caveats

This program currently only takes into account crops. It does not account for:
* Crafting
* Energy meter
* Time expended tending to crops
* Available tiles
* Random variances in harvest output, like giant or multiple (although the user can account for this by putting an average price in the *Sell* column)
* Animals 
* Other sources of income (like mining and foraging)

For this reason, **wheat** should not be included in the crop list. While technically the most profitable crop during summer, it is so cheap and would consume so much time, space, and energy that it is not a viable option (at least not for this algorithm). If you don't have enough money or days left in the season, though, buying a little wheat (or another cheap-and-fast crop) may be a good option.

This is a good guide for beginner players.  But more advanced players will probably want to account for other resources in their strategy, like crafting.  However, even the most advanced players can use this calendar as an objective measure of comparative crop profitability over a given time period.
