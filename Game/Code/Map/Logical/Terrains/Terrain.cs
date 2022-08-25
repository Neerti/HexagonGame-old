using Godot;

namespace Hexagon.Map.Logical.Terrains
{
	public abstract class Terrain
	{
		public string Name { get; protected set; }

		public TerrainKinds TerrainKind { get; protected set; } = TerrainKinds.Base;
		
		public Color TileColor { get; protected set; }
		
		public Texture TileTexture { get; protected set; }

		public string TileTexturePath { get; protected set; } =
			"res://Code/Map/Visual/VisualCells/Sprites/generic_hexagon_64.png";
	}

	public class GrassTerrain : Terrain
	{
		public GrassTerrain()
		{
			Name = "Grass";
			TerrainKind = TerrainKinds.Grass;
			TileColor = new Color("#82bf40");
		}
	}
	
	public class ForestTerrain : Terrain
	{
		public ForestTerrain()
		{
			Name = "Tall Grass";
			TerrainKind = TerrainKinds.Forest;
			TileColor = new Color("#4b6f25");
		}
	}

	public class RockTerrain : Terrain
	{
		public RockTerrain()
		{
			Name = "Rock";
			TerrainKind = TerrainKinds.Rock;
			TileColor = new Color("#7d7d7d");
		}
	}
	
	public class SnowTerrain : Terrain
	{
		public SnowTerrain()
		{
			Name = "Snow";
			TerrainKind = TerrainKinds.Snow;
			TileColor = new Color("#d6f2ff");
		}
	}
	
	public class BeachSandTerrain : Terrain
	{
		public BeachSandTerrain()
		{
			Name = "Sand";
			TerrainKind = TerrainKinds.BeachSand;
			TileColor = new Color("#dcd0c3");
		}
	}
	
	public class ShallowOceanTerrain : Terrain
	{
		public ShallowOceanTerrain()
		{
			Name = "Shallow Salt Water";
			TerrainKind = TerrainKinds.ShallowSaltWater;
			TileColor = new Color("#aa85cacc");
			TileTexturePath = "res://Code/Map/Visual/VisualCells/Sprites/liquid_hexagon_64.png";
		}
	}
	
	public class DeepOceanTerrain : Terrain
	{
		public DeepOceanTerrain()
		{
			Name = "Deep Salt Water";
			TerrainKind = TerrainKinds.DeepSaltWater;
			TileColor = new Color("#aa456e95");
			TileTexturePath = "res://Code/Map/Visual/VisualCells/Sprites/liquid_hexagon_64.png";
		}
	}
	
	public class ShallowWaterTerrain : Terrain
	{
		public ShallowWaterTerrain()
		{
			Name = "Shallow Fresh Water";
			TerrainKind = TerrainKinds.ShallowFreshWater;
			TileColor = new Color("#aa85b5eb");
			TileTexturePath = "res://Code/Map/Visual/VisualCells/Sprites/liquid_hexagon_64.png";
		}
	}
	
	public class DeepWaterTerrain : Terrain
	{
		public DeepWaterTerrain()
		{
			Name = "Deep Fresh Water";
			TerrainKind = TerrainKinds.DeepFreshWater;
			TileColor = new Color("#aa2a7ed6");
			TileTexturePath = "res://Code/Map/Visual/VisualCells/Sprites/liquid_hexagon_64.png";
		}
	}
}