using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesCommand : IGameplayCommand
{
    // Паттерн комманд.
    private ISharedData _data;          //injected
    private CellManager _battlefield;   //injected


}
