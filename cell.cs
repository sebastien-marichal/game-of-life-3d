using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;

public partial class cell : Node3D
{
    [Export]
    public float step = 0.5f;

    public bool alive_next;

    private CsgBox3D cube;

    private List<cell> Neighbours = new();
    private bool need_update = false;


    public void AddNeighbour(cell neighbour)
    {
        if (neighbour is not null && this != neighbour && !Neighbours.Contains(neighbour))
        {
            Neighbours.Add(neighbour);
        }
    }
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var nextTimer = GetNode<NextTimer>("/root/NextTimer");
        nextTimer.NextStep += () => { need_update = true; };
        Visible = Random.Shared.Next(0, 2) % 2 == 0;
        alive_next = Visible;
        cube = GetNode<CsgBox3D>("Cube");
        cube.MaterialOverride = cube.MaterialOverride.Duplicate() as Material;
        (cube.MaterialOverride as StandardMaterial3D).AlbedoColor = Colors.Aqua;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Visible != alive_next)
        {
            if (alive_next)
                Visible = alive_next;
            (cube.MaterialOverride as StandardMaterial3D).AlbedoColor = alive_next ? Colors.Aqua : Colors.Red;
            var tween = CreateTween();
            tween.TweenProperty(cube, "scale", alive_next ? Vector3.One : Vector3.Zero, 0.8f);
            tween.TweenCallback(Callable.From(() => { Visible = alive_next; }));
        }

        if (need_update)
        {
            var count = CountNeighbours();
            alive_next = count == 4 || (Visible && (count == 5 || count == 6));
            need_update = false;
        }
    }

    private int CountNeighbours()
    {
        Debug.Assert(Neighbours.Count < 27, $"neighbours count is {Neighbours.Count}");
        int count = 0;

        foreach (var cell in Neighbours)
        {
            count = cell.Visible ? count + 1 : count;
        }

        return count;
    }
}