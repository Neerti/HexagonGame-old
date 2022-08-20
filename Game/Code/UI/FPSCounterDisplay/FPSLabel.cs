using Godot;
using System;
using static Godot.Performance;

namespace Hexagon.Map.MapDisplays
{
	public class FPSLabel : Label
	{
		public override void _Process(float delta)
		{
			Text = $@"Time
- FPS: {GetMonitor(Monitor.TimeFps)}
- Frame Allowance (60Hz): {(60f / GetMonitor(Monitor.TimeFps)) * 100:F2}%
- Frame Allowance (144Hz): {(144 / GetMonitor(Monitor.TimeFps)) * 100:F2}%
- Process Time: {GetMonitor(Monitor.TimeProcess) * 1000:F2} ms
- Phys. Process Time: {GetMonitor(Monitor.TimePhysicsProcess) * 1000:F2} ms

Memory
- Static: {(GetMonitor(Monitor.MemoryStatic) / 1024 / 1024):F2} MiB
- Max Static: {(GetMonitor(Monitor.MemoryStaticMax) / 1024 / 1024):F2} MiB
- Dynamic: {(GetMonitor(Monitor.MemoryDynamic) / 1024 / 1024):F2} MiB
- Max Dynamic: {(GetMonitor(Monitor.MemoryDynamicMax) / 1024 / 1024):F2} MiB
- Max Message Buffer: {(GetMonitor(Monitor.MemoryMessageBufferMax) / 1024 / 1024):F2} MiB

Objects
- Object Count: {GetMonitor(Monitor.ObjectCount)}
- Node Count: {GetMonitor(Monitor.ObjectNodeCount)}
- Resource Count: {GetMonitor(Monitor.ObjectResourceCount)}
- Orphaned Node Count: {GetMonitor(Monitor.ObjectOrphanNodeCount)}

";
		}
	}
}


