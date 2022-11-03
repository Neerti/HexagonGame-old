// This file pulls from all the other .jsonnet files,
// and produces a .json file with every item in the game.

local Drinks = import 'drinks.jsonnet';
local Food = import 'food.jsonnet';
local Materials = import 'materials.jsonnet';

Drinks + Food + Materials
