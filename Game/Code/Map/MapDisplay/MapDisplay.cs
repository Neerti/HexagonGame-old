using System.Collections.Generic;
using Godot;

namespace Hexagon
{
	/// <summary>
	/// The MapDisplay object is used to visually represent the contents of a <see cref="HexGrid"/>.
	/// Since the actual grid might have hundreds or thousands of tiles, rendering each tile at once would be a bad
	/// idea. Instead, MapDisplay only shows a limited number of tiles at a time
	/// </summary>
	public class MapDisplay : Node2D
	{
		[Signal]
		public delegate void MapZoomed(int oldIndex, int newIndex);
		
		/// <summary>
		/// How many horizontal tiles can currently fit on the player's screen at once.
		/// </summary>
		public int TileResolutionX { get; private set; }

		/// <summary>
		/// How many vertical tiles can currently fit on the player's screen at once.
		/// </summary>
		public int TileResolutionY { get; private set; }
		
		/// <summary>
		/// Amount of extra tiles to render beyond what the player should be able to see. This is done to avoid tiles
		/// from 'popping in' when the map is being moved. This value applies the extra tiles on all four sides
		/// at the same time.
		/// </summary>
		readonly int _overscan = 3;

		/// <summary>
		/// Holds the <see cref="HexGrid"/> that is being displayed.
		/// </summary>
		public HexGrid MapToDisplay;

		private const float MapMovementSpeed = 400f;

		private readonly List<float> _zoomLevels = new List<float>
		{
			0.25f, 0.50f, 1.0f, 2.0f
		};

		private int _zoomIndex = 2;

		/// <summary>
		/// Scales the map using choices inside of <see cref="_zoomLevels"/>, by index.
		/// Input is automatically clamped and it cannot go out of bounds.
		/// </summary>
		/// <param name="newZoomIndex">Index of desired zoom level.</param>
		private void SetZoomLevel(int newZoomIndex)
		{
			int oldZoomIndex = _zoomIndex;
			_zoomIndex = Mathf.Clamp(newZoomIndex, 0, _zoomLevels.Count - 1);
			Scale = new Vector2(_zoomLevels[_zoomIndex], _zoomLevels[_zoomIndex]);
			if (oldZoomIndex == _zoomIndex) return;
			UpdateTileResolutions();
			EmitSignal(nameof(MapZoomed), oldZoomIndex, _zoomIndex);
		}

		/// <summary>
		/// Holds the relative coordinates for where the TileMap's origin would be on the actual map.
		/// It acts as an offset that is adjusted when the map is moved, so that redrawing the map will reflect
		/// the movement that occured.
		/// </summary>
		public Vector2 DisplayOriginMapPosition = Vector2.Zero;

		/// <summary>
		/// Recalculates the amount of tiles that should be rendered. Should be called if the game resolution
		/// changes, e.g. the window is resized, or the map is zoomed in/out.
		/// </summary>
		private void UpdateTileResolutions()
		{
			var tileMap = GetNode<TileMap>("TileMap");
			var tileWidth = tileMap.CellSize.x * Scale.x;
			var tileHeight = tileMap.CellSize.y * Scale.y;
			TileResolutionX = (int)(GetViewport().Size.x / tileWidth);
			TileResolutionY = (int)(GetViewport().Size.y / tileHeight);

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
			var tileMap = GetNode<TileMap>("TileMap");
			tileMap.Clear();

			// `tilemap_[x|y]` refers to the grid shown on the player's screen using Godot's tilemap object.
			// `map_[x|y]` refers to the actual map's data that the tilemap is supposed to represent.
			for (int tilemap_x = -_overscan; tilemap_x < TileResolutionX + _overscan; tilemap_x++)
			{
				for (int tilemap_y = -_overscan; tilemap_y < TileResolutionY + _overscan; tilemap_y++)
				{
					var map_x = tilemap_x - (int)DisplayOriginMapPosition.x;
					var map_y = tilemap_y - (int)DisplayOriginMapPosition.y;

					// Don't draw any tiles if no data exists.
					// Later on it would be nice to implement wrap around for the east and west sides.
					if (!MapToDisplay.HasHex(map_x, map_y)) continue;

					var tile = MapToDisplay.GetHex(map_x, map_y);
					tileMap.SetCell(tilemap_x, tilemap_y, TileTypeToTileMapInt(tile.tile_type));
				}
			}
			
		}

		private readonly Godot.Collections.Dictionary<Hex.TileType, int> _tileTypeToTileMap = new Godot.Collections.Dictionary<Hex.TileType, int>
		{
			{Hex.TileType.BASE, 0},
			{Hex.TileType.GRASS, 1},
			{Hex.TileType.DEEP_SALT_WATER, 2},
			{Hex.TileType.SHALLOW_SALT_WATER, 6},
			{Hex.TileType.ROCK, 3},
			{Hex.TileType.BEACH_SAND, 4},
			{Hex.TileType.FOREST, 5},
			{Hex.TileType.SNOW, 7},
			{Hex.TileType.SHALLOW_FRESH_WATER, 8},
			{Hex.TileType.DEEP_FRESH_WATER, 9}
		};

		private int TileTypeToTileMapInt(Hex.TileType type)
		{
			return _tileTypeToTileMap.TryGetValue(type, out var result) ? result : 0;
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

		public override void _UnhandledInput(InputEvent @event)
		{
			if(@event.IsActionReleased("zoom_map_out"))
			{
				SetZoomLevel(_zoomIndex - 1);
			}
			else if (@event.IsActionReleased("zoom_map_in"))
			{
				SetZoomLevel(_zoomIndex + 1);
			}
			else if (@event is InputEventMouseButton clickEvent)
			{
				if (clickEvent.Pressed && clickEvent.ButtonIndex == (int) ButtonList.Left)
				{
					GD.Print(clickEvent.Position);
				}
			}
		}

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

		public override void _Ready()
		{
			GetTree().Root.Connect("size_changed", this, nameof(OnViewportSizeChanged));
			SetZoomLevel(_zoomIndex);

			UpdateTileResolutions();
			// TODO
			TestMap();
			RedrawMap();

		}

		private void TestMap()
		{
			var mapGen = new MapGenerator(2);
			MapToDisplay = mapGen.GenerateNewMap(1024, 1024);

			// Center the position.
			DisplayOriginMapPosition = new Vector2(-512, -512);
			
			var img = MapToDisplay.GenerateMapImage();
			img.SavePng("res://height_test.png");
			img = MapToDisplay.GenerateHeightNoiseImage();
			img.SavePng("res://height_noise.png");
			img = MapToDisplay.GenerateHeatMap();
			img.SavePng("res://heat_map.png");
		}
	}
}

