using Godot;
using System;
using static Godot.Performance;

namespace Hexagon.Map.Visual.VisualCells
{
	public class FPSLabel : Label
	{
		public override void _Process(float delta)
		{
			var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			Text = $@"Time
- FPS: {GetMonitor(Monitor.TimeFps)}
- Frame Allowance (60Hz): {(60f / GetMonitor(Monitor.TimeFps)) * 100f:F2}%
- Frame Allowance (144Hz): {(144f / GetMonitor(Monitor.TimeFps)) * 100f:F2}%
- Process Time: {GetMonitor(Monitor.TimeProcess) * 1000f:F2} ms
- Phys. Process Time: {GetMonitor(Monitor.TimePhysicsProcess) * 1000f:F2} ms

Memory (Godot)
- Static: {(GetMonitor(Monitor.MemoryStatic) / 1024f / 1024f):F2} MiB
- Max Static: {(GetMonitor(Monitor.MemoryStaticMax) / 1024f / 1024f):F2} MiB
- Dynamic: {(GetMonitor(Monitor.MemoryDynamic) / 1024f / 1024f):F2} MiB
- Max Dynamic: {(GetMonitor(Monitor.MemoryDynamicMax) / 1024f / 1024f):F2} MiB
- Max Message Buffer: {(GetMonitor(Monitor.MemoryMessageBufferMax) / 1024f / 1024f):F2} MiB

Memory (C#)
- Physical Memory Usage: {(currentProcess.WorkingSet64 / 1024f / 1024f):F2} MiB

Objects
- Object Count: {GetMonitor(Monitor.ObjectCount)}
- Node Count: {GetMonitor(Monitor.ObjectNodeCount)}
- Resource Count: {GetMonitor(Monitor.ObjectResourceCount)}
- Orphaned Node Count: {GetMonitor(Monitor.ObjectOrphanNodeCount)}

";
		}
	}
}


