using Godot;
using JetBrains.Annotations;

namespace Hexagon.Map.Visual.VisualChunks
{
	[UsedImplicitly]
	public class ChunkVisibilityNotifier : VisibilityNotifier2D
	{
		public override void _Ready()
		{
			Rect = new Rect2(
				-VisualChunk.SpriteWidth,
				-VisualChunk.SpriteHeight,
				VisualChunk.ChunkVisibleWidth,
				VisualChunk.ChunkVisibleHeight
			);

			var refRect = GetNode<ReferenceRect>("ReferenceRect");
			refRect.RectPosition = Rect.Position;
			refRect.RectSize = Rect.Size;
			refRect.BorderColor = Color.FromHsv(
				(GlobalPosition.x % VisualChunk.ChunkVisibleWidth * 2) / VisualChunk.ChunkVisibleWidth * 2,
				1,
				GlobalPosition.y % 1000 / 1000
			);
		}
	}
}


