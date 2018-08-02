# Summary

This program determines the most profitable crop to plant in Stardew Valley on each day.
An "investment multiple" is given for following the schedule's plant days exactly. E.g., an investment multiple of 45 means you start the season with 1000g and end the season with 45,000g.

# Instructions

1. On the left, you can add and remove crops from the crop list.
1. When you're ready, press the "Compute Schedule" button. A planting schedule will appear on the calendar to the right.
1. When a crop is listed on the schedule (and not in parentheses), this is what you should plant on that day. The algorithm assumes you spend all of your harvest money purchasing the seed on that day and planting it on that day. You can spend as much your comfortable spending, though.
1. Non-plant days still show the most profitable crop, listed in parentheses. This is in case you find money through some other means, and want to know what the best crop is to buy. The investment multiple does not account for these days.
1. Days with no crop listed are days you should *definitely not* plant on. Usually it's because the crop will not yield enough harvests to be profitable before the season is over and all your crops die.

# Caveats

This program currently only takes into account crops. It does not account for:
* Energy
* Time/effort expended tending to crops
* Available tiles on farm
* Crafting
* Other sources of income (like mining and foraging)
* Periods of time shorter or longer than season

So **wheat** should not be included in the crop list. While technically the most profitable crop, it is so cheap and would consume so much time, space, and energy that it is not a viable option (at least not for this algorithm). If you don't have enough money or days left in the season, though, buying a little wheat may be a good option.

This is a good guide for beginner to intermediate players.  But more advanced players might want to account for these other resources in their strategy.

These other resources will likely be added to the algorithm in the future.
