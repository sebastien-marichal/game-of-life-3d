using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class grid : Node3D
{
    [Export]
    public int Width = 5;

    [Export]
    public int Height = 5;

    [Export]
    public int Depth = 5;

    [Export]
    public int Step = 250;

    [Export]
    public PackedScene Cell;

    private Dictionary<Vector3, cell> actual_grid = new();
    // private Timer next;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // next = GetNode<Timer>("Next");
        for (int z = 0; z < Depth; z++)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var cellInstance = Cell.Instantiate<cell>();
                    cellInstance.Position = new Vector3(x - Width / 2.0f, y - Height / 2.0f, z - Depth / 2.0f);
                    actual_grid.Add(new Vector3(x, y, z), cellInstance);

                    foreach (var i in Enumerable.Range(0, 27))
                    {
                        var pos = new Vector3(x + i % 3 - 1, y + (i % 9) / 3 - 1, z + i / 9 - 1);
                        if (actual_grid.ContainsKey(pos))
                        {
                            cellInstance.AddNeighbour(actual_grid[pos]);
                            actual_grid[pos].AddNeighbour(cellInstance);
                        }
                    }

                    AddChild(cellInstance);
                }
            }
        }

        GD.Print(GetChildren().Count);
    }

    // // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(double delta)
    // {
    //     if (next.TimeLeft <= 0)
    //     {
    //         foreach (var i in Enumerable.Range(0, Width * Height * Depth))
    //         {
    //             var x = i % Width;
    //             var y = (i % (Width * Height)) / Width;
    //             var z = i / (Width * Height);
    //             var cell = actual_grid[new Vector3(x, y, z)];
    //             var count = CountNeighbours(x, y, z);
    //             cell.alive_next = count == 4 || (cell.Visible && (count == 5 || count == 6));
    //         }
    //
    //         next.Start();
    //     }
    // }

    // private int CountNeighbours(int x, int y, int z)
    // {
    //     int count = 0;
    //     foreach (var i in Enumerable.Range(0, 27))
    //     {
    //         var pos = new Vector3(x + i % 3 - 1, y + (i % 9) / 3 - 1, z + i / 9 - 1);
    //         if (pos != new Vector3(x, y, z) && actual_grid.ContainsKey(pos))
    //         {
    //             count += actual_grid[pos].Visible ? 1 : 0;
    //         }
    //     }
    //
    //     return count;
    // }
}