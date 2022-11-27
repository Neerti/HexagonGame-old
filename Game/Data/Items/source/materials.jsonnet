local Template = import 'template.libsonnet';

{
	Stick: Template + {
		display_name: "Stick",
		description: "A piece of wood. It was once part of a tree.",
	},

	Rock: Template + {
		display_name: "Rock",
		description: "An ordinary rock.",
	},

	Acorn: Template + {
		display_name: "Acorn"
		description: "A seed that came from an oak tree."
	},


}
