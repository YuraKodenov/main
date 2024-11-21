using System;
using System.Collections.Generic;
using Xunit;

public class EscapeBaseTests
{
    [Fact]
    public void Test_SimplePath_NoWalls_NoTunnels()
    {
        int[,] layout = {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };
        var exits = new List<(int, int)> { (2, 2) };
        var escapeBase = new EscapeBase(3, 3, (0, 0), layout, exits, new Dictionary<(int, int), (int, int)>());
        int result = escapeBase.FindShortestEscapePath();
        Assert.Equal(4, result); // Expected path length
    }

    [Fact]
    public void Test_PathWithWall_BlockingPath()
    {
        int[,] layout = {
            { 0, 1, 0 },
            { 1, 1, 0 },
            { 0, 0, 0 }
        };
        var exits = new List<(int, int)> { (2, 2) };
        var escapeBase = new EscapeBase(3, 3, (0, 0), layout, exits, new Dictionary<(int, int), (int, int)>());
        int result = escapeBase.FindShortestEscapePath();
        Assert.Equal(5, result); // Expected path length avoiding walls
    }

    [Fact]
    public void Test_ExitIsUnreachable()
    {
        int[,] layout = {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        };
        var exits = new List<(int, int)> { (2, 2) };
        var escapeBase = new EscapeBase(3, 3, (0, 0), layout, exits, new Dictionary<(int, int), (int, int)>());
        int result = escapeBase.FindShortestEscapePath();
        Assert.Equal(-1, result); // No reachable exit
    }

    [Fact]
    public void Test_HyperTunnelShortcut()
    {
        int[,] layout = {
            { 0, 0, 0, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 }
        };
        var exits = new List<(int, int)> { (2, 3) };
        var tunnels = new Dictionary<(int, int), (int, int)>
        {
            { (0, 0), (2, 2) }
        };
        var escapeBase = new EscapeBase(3, 4, (0, 0), layout, exits, tunnels);
        int result = escapeBase.FindShortestEscapePath();
        Assert.Equal(2, result); // Tunnel provides direct path
    }

    [Fact]
    public void Test_MultipleExits_FindClosestExit()
    {
        int[,] layout = {
            { 0, 0, 0, 0 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 0 }
        };
        var exits = new List<(int, int)> { (2, 3), (0, 3) };
        var escapeBase = new EscapeBase(3, 4, (0, 0), layout, exits, new Dictionary<(int, int), (int, int)>());
        int result = escapeBase.FindShortestEscapePath();
        Assert.Equal(3, result); // Closest exit is at (0, 3)
    }
}
