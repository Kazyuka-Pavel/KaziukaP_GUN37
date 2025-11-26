using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneByOneTurn : ITurn
{
    private int _index;
    private readonly IReadOnlyList<Team> _teams;

     public Team Current => _teams[_index];

    public void Next()
    {
        _index = (_index + 1) % _teams.Count;
    }

    private OneByOneTurn(IReadOnlyList<Team> teams)
    {
        _teams = teams;
        _index = UnityEngine.Random.Range(0, _teams.Count);
    }
}
