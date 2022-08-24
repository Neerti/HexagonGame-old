using System;
using Godot;
using Hexagon.Map.Visual.VisualChunks.HybridChunks;

namespace Hexagon.Map.Visual.VisualChunks.HybridChunks
{
	public class HybridChunk : VisualChunk
	{
		private SpriteGrid _sprites;
		private bool _onScreen;
		private ChunkStatus _chunkState;
		
		private enum ChunkStatus
		{
			/// <summary>
			/// The chunk currently has no sprites, but will have sprites if it becomes onscreen.
			/// </summary>
			Unloaded,
			
			/// <summary>
			/// The chunk moved offscreen and has sprites that are being deleted over time.
			/// </summary>
			Unloading,
			
			/// <summary>
			/// The chunk has sprites, and will continue to until the chunk is offscreen.
			/// </summary>
			Loaded
		}

		public override void _Ready()
		{
			_sprites = GetNode<SpriteGrid>("SpriteGrid");;
			
			var vis = GetNode<VisibilityNotifier2D>("ChunkVisibilityNotifier");
			vis.Connect("screen_exited", this, nameof(VisualChunk.OnScreenExited));
			vis.Connect("screen_entered", this, nameof(VisualChunk.OnScreenEntered));
		}

		private void LoadChunk()
		{
			// Make the sprites.
			_sprites.DrawChunk(Map, ChunkX, ChunkY);
			_chunkState = ChunkStatus.Loaded;

			GetNode<Label>("ChunkLabel").Text = $"Chunk {ChunkX},{ChunkY}";
		}

		public override void OnScreenExited()
		{
			//RemoveChild(_sprites);
			_onScreen = false;
		}

		public override void OnScreenEntered()
		{
			_onScreen = true;
		}

		public override void _Process(float delta)
		{
			switch (_chunkState)
			{
				case ChunkStatus.Unloaded:
					if (_onScreen)
					{
						LoadChunk();
					}
					break;
				case ChunkStatus.Unloading:
					var finished = GradualDelete();
					if (finished)
					{
						_chunkState = ChunkStatus.Unloaded;
					}
					break;
				case ChunkStatus.Loaded:
					if (!_onScreen)
					{
						_chunkState = ChunkStatus.Unloading;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
		}

		public override void _Notification(int what)
		{
			
			// Clean up when closing the game and avoid erroneous debug errors when quitting.
			if (what == MainLoop.NotificationWmQuitRequest)
			{
				GetNode<VisibilityNotifier2D>("ChunkVisibilityNotifier")
					.Disconnect("screen_entered", this, nameof(VisualChunk.OnScreenEntered));
				GetNode<VisibilityNotifier2D>("ChunkVisibilityNotifier")
					.Disconnect("screen_exited", this, nameof(VisualChunk.OnScreenExited));
				_sprites?.QueueFree();
			}
			
				
		}

		private bool GradualDelete()
		{
			if (_sprites == null || _sprites.GetChildren().Count == 0)
			{
				return true;
			}

			var maxIterations = _sprites?.GetChildren().Count < ChunkSize ? _sprites.GetChildren().Count : ChunkSize;
			for (var i = 0; i < maxIterations; i++)
			{
				_sprites.GetChild(i).QueueFree();
			}

			return false;
		}
		
	}
}