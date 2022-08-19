using System;
using Godot;
using System.Collections.Generic;

namespace Hexagon.Map.MapDisplays.HexagonalGrids
{
	public class HexagonalGrid : Node2D
	{
		public HexGrid MapToDisplay;
		private List<HexagonSprite> Sprites = new List<HexagonSprite>();
		
		private Chunk[,] _chunks;
		private List<List<Chunk>> _chunkLists;
		private PackedScene _packedChunkScene;

		private Vector2 _topLeftCornerPosition = new Vector2(-Chunk.ChunkWidth, -Chunk.ChunkHeight);

		private VectorHex _topLeftCornerCoordinate = new VectorHex(256, 256);

		/// <summary>
		/// How many vertical chunks are needed to completely cover the player's screen.
		/// </summary>
		public int ChunkResolutionX { get; private set; }
		
		/// <summary>
		/// How many vertical chunks are needed to completely cover the player's screen.
		/// </summary>
		public int ChunkResolutionY { get; private set; }
		
		private const float MapMovementSpeed = 400f;
		
		

		public override void _Ready()
		{
			_packedChunkScene =
				(PackedScene) ResourceLoader.Load("res://Code/Map/MapDisplay2/HexagonSprite/Chunk.tscn");
			
			GetTree().Root.Connect("size_changed", this, nameof(OnViewportSizeChanged));
			UpdateTileResolutions();
			
			TestMap();
			RedrawMap();
		}
		
		
		/// <summary>
		/// Recalculates the amount of tiles that should be rendered. Should be called if the game resolution
		/// changes, e.g. the window is resized, or the map is zoomed in/out.
		/// </summary>
		private void UpdateTileResolutions()
		{
			var chunkWidth = Chunk.ChunkWidth * Scale.x;
			var chunkHeight = Chunk.ChunkHeight * Scale.y;
			ChunkResolutionX = (int) (GetViewport().Size.x / chunkWidth) + 1;
			ChunkResolutionY = (int) (GetViewport().Size.y / chunkHeight);

			if (MapToDisplay is null)
			{
				return;
			}
			RedrawMap();
		}

		public void OnViewportSizeChanged()
		{
			UpdateTileResolutions();
		}

		private void RedrawMap()
		{
			var ySort = GetNode<YSort>("Chunks");
			
			// Delete all the sprites.
			foreach (var child in ySort.GetChildren())
			{
				var node = (Node)child;
				node.QueueFree();
			}

			// Make a new grid with the new sizes.
			var actualSizeX = ChunkResolutionX + 2;
			var actualSizeY = ChunkResolutionY + 2;
			
			_chunkLists = new List<List<Chunk>>();
			
			for (var i = 0; i < actualSizeX; i++)
			{
				var newList = new List<Chunk>();
				
				for (var j = 0; j < actualSizeY; j++)
				{
					var newChunk = MakeNewChunk(i, j);
					ySort.AddChild(newChunk);
					newList.Add(newChunk);
				}
				_chunkLists.Add(newList);
			}
			
			GD.Print($@"Finished redrawing map with a new size of {actualSizeX}, {actualSizeY}.");
		}

		private Chunk MakeNewChunk(int relativeXCoordinate, int relativeYCoordinate)
		{
			var newChunk = (Chunk) _packedChunkScene.Instance();
			newChunk.Name = $@"Chunk {_topLeftCornerCoordinate.X + relativeXCoordinate * Chunk.ChunkSize},{_topLeftCornerCoordinate.Y + relativeYCoordinate * Chunk.ChunkSize}";
			
			newChunk.Position = new Vector2(
				_topLeftCornerPosition.x + (relativeXCoordinate * Chunk.ChunkWidth), 
				_topLeftCornerPosition.y + (relativeYCoordinate * Chunk.ChunkHeight)
			);

			var offset = new VectorHex(
				_topLeftCornerCoordinate.X + (relativeXCoordinate * Chunk.ChunkSize),
				_topLeftCornerCoordinate.Y + (relativeYCoordinate * Chunk.ChunkSize)
			);
			newChunk.SetUp(offset, MapToDisplay);
			
			return newChunk;
		}

		private void TestMap()
		{
			var mapGen = new MapGenerator(2);
			MapToDisplay = mapGen.GenerateNewMap(512, 512);

			// Center the position.
			//DisplayOriginMapPosition = new Vector2(-512, -512);
			
			var img = MapToDisplay.GenerateMapImage();
			img.SavePng("res://height_test.png");
			img = MapToDisplay.GenerateHeightNoiseImage();
			img.SavePng("res://height_noise.png");
			img = MapToDisplay.GenerateHeatMap();
			img.SavePng("res://heat_map.png");
		}
		
		public override void _Process(float delta)
		{
			var direction = new Vector2();
			var speed = MapMovementSpeed / Scale.x;
			if(Input.IsActionPressed("move_map_up"))
			{
				direction -= Vector2.Up;
			}
			if (Input.IsActionPressed("move_map_down"))
			{
				direction -= Vector2.Down;
			}
			if (Input.IsActionPressed("move_map_left"))
			{
				direction -= Vector2.Left;
			}
			if (Input.IsActionPressed("move_map_right"))
			{
				direction -= Vector2.Right;
			}

			// Holding shift makes the map move faster.
			if (Input.IsPhysicalKeyPressed((int) KeyList.Shift))
			{
				speed *= 2;
			}
			
			Position += (direction.Normalized() * speed * delta);
			SnapBack();
		}

		/// <summary>
		/// When the map's position is shifted too far, it is instantly moved back, and an an offset is applied,
		/// which creates the illusion of continuous movement.
		/// </summary>
		private void SnapBack()
		{
			// Moving to the left.
			if (Position.x > Chunk.ChunkWidth * Scale.x)
			{
				Position = new Vector2(0, Position.y);

				// Remove the column on the right side of the screen.
				foreach (var chunk in _chunkLists[_chunkLists.Count - 1])
				{
					chunk.QueueFree();
				}
				_chunkLists.Remove(_chunkLists[_chunkLists.Count - 1]);
				
				// Move all remaining chunks towards the right.
				foreach (var list in _chunkLists)
				{
					foreach (var chunk in list)
					{
						chunk.Position += Vector2.Right * Chunk.ChunkWidth;
					}
				}
				_topLeftCornerCoordinate += new VectorHex(-Chunk.ChunkSize, 0);
				
				// Add new chunks on the left side.
				var newColumn = new List<Chunk>();
				var ySort = GetNode<YSort>("Chunks");
				for (int i = 0; i < ChunkResolutionY+2; i++)
				{
					var chunk = MakeNewChunk(0, i);
					ySort.AddChild(chunk);
					newColumn.Add(chunk);
				}
				_chunkLists.Insert(0, newColumn);


			}

			// Moving to the right.
			if (Position.x < -Chunk.ChunkWidth * Scale.x)
			{
				Position = new Vector2(0, Position.y);
				
				// Remove the column on the left side of the screen.
				foreach (var chunk in _chunkLists[0])
				{
					chunk.QueueFree();
				}
				_chunkLists.Remove(_chunkLists[0]);
				
				// Move all remaining chunks towards the left.
				foreach (var list in _chunkLists)
				{
					foreach (var chunk in list)
					{
						chunk.Position += Vector2.Left * Chunk.ChunkWidth;
					}
				}
				_topLeftCornerCoordinate += new VectorHex(Chunk.ChunkSize, 0);
				
				// Add new chunks on the right side.
				var newColumn = new List<Chunk>();
				var ySort = GetNode<YSort>("Chunks");
				for (int i = 0; i < ChunkResolutionY+2; i++)
				{
					var chunk = MakeNewChunk(ChunkResolutionX+1, i);
					ySort.AddChild(chunk);
					newColumn.Add(chunk);
				}
				_chunkLists.Add(newColumn);
			}

		}
		
		
		
		/*
		/// <summary>
		/// When the map's position is shifted too far, it is instantly moved back, and an an offset is applied,
		/// which creates the illusion of continuous movement.
		/// </summary>
		private void SnapBack()
		{
			var tileMap = GetNode<TileMap>("TileMap");
			var cellWidth = tileMap.CellSize.x * Scale.x;
			var cellHeight = tileMap.CellSize.y * Scale.y;
			
			// If this is set to true, the map is redrawn at the end.
			var moved = false;
			
			// Because every other row is offset by half, it needs to snap back after two rows and not one.
			if (Position.x > cellWidth * 2)
			{
				Position = new Vector2(0, Position.y);
				DisplayOriginMapPosition -= Vector2.Left * 2;
				moved = true;
			}

			if (Position.x < -cellWidth * 2)
			{
				Position = new Vector2(0, Position.y);
				DisplayOriginMapPosition -= Vector2.Right * 2;
				moved = true;
			}

			if (Position.y > cellHeight)
			{
				Position = new Vector2(Position.x, 0);
				DisplayOriginMapPosition -= Vector2.Up;
				moved = true;
			}
			
			if (Position.y < -cellHeight)
			{
				Position = new Vector2(Position.x, 0);
				DisplayOriginMapPosition -= Vector2.Down;
				moved = true;
			}

			if (moved)
			{
				RedrawMap();
			}
		}
		
		*/
	}
}


