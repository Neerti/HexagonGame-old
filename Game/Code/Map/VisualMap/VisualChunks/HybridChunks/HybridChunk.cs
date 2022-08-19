using Godot;
using Hexagon.Map.VisualMap.VisualChunks.LoadedChunks;

namespace Hexagon.Map.VisualMap.VisualChunks.HybridChunks
{
    public class HybridChunk : VisualChunk
    {
        private SpriteGrid _sprites;
        
        private void LoadChunk()
        {
            // Make the sprites.
            var spriteGrid = GetNode<SpriteGrid>("SpriteGrid");
            spriteGrid.DrawChunk(Map, ChunkX, ChunkY);
            _sprites = spriteGrid;

            GetNode<Label>("ChunkLabel").Text = $"Chunk {ChunkX},{ChunkY}";
        }

        public override void OnScreenExited()
        {
            RemoveChild(_sprites);
        //    var thread = new Thread();
        //    thread.Start(this, nameof(DeleteSprites), _sprites);
        }

        public override void OnScreenEntered()
        {
            if (_sprites is null)
            {
                LoadChunk();
            }
            else
            {
                AddChild(_sprites);
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
        
    }
}