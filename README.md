# Summary

This Blazor WASM website computes the most profitable crop to plant on any given day, in the game Stardew Valley.
It also tells you how much profit you will make (if you follow the given schedule). E.g., an "investment multiple" of 45x means if you started the season with 1000 g, then you will end with 45,000 g.

# Instructions

1. On the left, you can add and remove crops from the crop list.
1. When you're ready, press the "Compute Schedule" button. A planting schedule will appear on the calendar to the right.
1. If a crop is listed on the calendar (and not in parentheses), this is what you should plant on that day. The algorithm assumes you spend all of your harvest money purchasing and planting the prescribed seed on that day. But it's ok if you spend only as much as you're comfortable spending.
1. Non-plant days may still show the most profitable crop, but listed in parentheses. This is in case you find money through some other means, and want to know what the best crop is to buy with your additional wealth. The investment multiple does not account for these days.
1. Days with no crop listed are days you should **definitely not** plant on! Usually it's because the crop will not yield enough harvests to be profitable before the season is over and all your crops die. 

# Caveats

This program only takes into account crops. It does not account for:
* Crafting
* Energy meter
* Time expended tending to crops
* Giant crops


This is a good guide for beginner and intermediate players. More advanced players may want to account for other resources in their strategy, like crafting.  However, even the most advanced players can use this calendar as an objective measure of comparative crop profitability over a given time period.
