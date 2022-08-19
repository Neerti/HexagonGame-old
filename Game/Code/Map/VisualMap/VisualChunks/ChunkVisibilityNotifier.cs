using Godot;

namespace Hexagon.Map.VisualMap.VisualChunks
{
	public class ChunkVisibilityNotifier : VisibilityNotifier2D
	{
		public override void _Ready()
		{
			Connect("screen_exited", GetParent(), nameof(VisualChunk.OnScreenExited));
			Connect("screen_entered", GetParent(), nameof(VisualChunk.OnScreenEntered));
			
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
				(GlobalPosition.y % 1000 / 1000)
			);
		}
	}
}


