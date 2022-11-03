local Template = import 'template.libsonnet';

local BaseFood = Template + {
    need_fulfillment: [
        {need: "food", amount: 0.25},
    ],
};

{
	Apple: BaseFood + {
		display_name: "Apple",
		description: "A fruit that came from a tree.",
		sprite_texture_path: "foo.png",
		need_fulfillment: [
			{need: "food", amount: 0.25},
		],
	},

	Berry: BaseFood + {
		display_name: "Berry",
		description: "A small, tasty fruit. Not very filling, but easy to forage.",
		sprite_texture_path: "bar.png",
		need_fulfillment: [
			{need: "food", amount: 0.2},
		],
	},
}
