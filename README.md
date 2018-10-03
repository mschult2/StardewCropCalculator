# Summary

This program computes the most profitable crop to plant in Stardew Valley on each day.
It also tells you how much you profit if you follow the schedule exactly. E.g., an "investment multiple" of 45x means if you started the season with 1000 g, then you will end with 45,000 g.

# Instructions

1. On the left, you can add and remove crops from the crop list.
1. When you're ready, press the "Compute Schedule" button. A planting schedule will appear on the calendar to the right.
1. If a crop is listed on the calendar (and not in parentheses), this is what you should plant on that day. The algorithm assumes you spend all of your harvest money purchasing and planting the prescribed seed on that day. But you can spend as much as you're comfortable spending.
1. Non-plant days may still show the most profitable crop, listed in parentheses. This is in case you find money through some other means, and want to know what the best crop is to buy with your additional wealth. The investment multiple does not account for these days.
1. Days with no crop listed are days you should **definitely not** plant on. Usually it's because the crop will not yield enough harvests to be profitable before the season is over and all your crops die.

# Caveats

This program currently only takes into account crops. It does not account for:
* Energy
* Time/effort expended tending to crops
* Available tiles on farm
* Crafting
* Other sources of income (like mining and foraging)

So **wheat** should not be included in the crop list. While technically the most profitable crop, it is so cheap and would consume so much time, space, and energy that it is not a viable option (at least not for this algorithm). If you don't have enough money or days left in the season, though, buying a little wheat may be a good option.

This is a good guide for beginner to intermediate players.  But more advanced players might want to account for these other resources in their strategy.

These other resources will likely be added to the algorithm in the future.
