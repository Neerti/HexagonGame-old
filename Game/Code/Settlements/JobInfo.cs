namespace Hexagon.Settlements
{
	/// <summary>
	/// Holds information about a particular kind of <see cref="Job"/>.
	/// </summary>
	public abstract class JobInfo
	{
		public JobKinds JobKind { get; protected set; }
		
		/// <summary>
		/// The name of the job, shown to players in the UI.
		/// </summary>
		public string DisplayName { get; protected set; }
		
		/// <summary>
		/// A short description of what the job is about, shown to players in the UI.
		/// </summary>
		public string DisplayDescription { get; protected set; }
		
		/// <summary>
		/// Some jobs may require people working them to consume more.
		/// </summary>
		public float NeedRequirementMultiplier { get; protected set; }
	}

	public enum JobKinds
	{
		Base,
		Miner,
		Hunter
	}

	public class MinerJob : JobInfo
	{
		public MinerJob()
		{
			JobKind = JobKinds.Miner;
			NeedRequirementMultiplier = 1.40f;
			DisplayName = "Miner";
			DisplayDescription = "Miners dig underground, clearing rock to retrieve the minerals buried deep " +
			                     "within. It's a highly demanding and dangerous job.";
		}
	}
	
	public class HunterJob : JobInfo
	{
		public HunterJob()
		{
			JobKind = JobKinds.Hunter;
			DisplayName = "Hunter";
			DisplayDescription = "Hunters are tasked with killing animals for their spoils, such as " +
			                     "meat, fur, and leather. This can be rather risky if the creature " +
			                     "being hunted can fight back.";
		}
	}
}