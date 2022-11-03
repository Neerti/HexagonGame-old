local Template = import 'template.libsonnet';

local BaseDrink = Template + {
    need_fulfillment: [
        {need: "water", amount: 1},
    ],
};

{
	Water: BaseDrink + {
		display_name: "Water",
		description: "Consumed to avoid dying of thirst.",
	},

	ContaminatedWater: BaseDrink + {
		display_name: "Contaminated Water",
		description: "Not fit for consumption.",
	},

	Tea: BaseDrink + {
		display_name: "Tea",
		description: "Warm and delicious tea, prepared using tea leaves and hot water.",
	},
}
