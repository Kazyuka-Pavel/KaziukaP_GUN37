using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ScriptableObject
{
    private GameObject[] _figures;

    public GameController()
    {
        var figures = UnityEngine.Object.FindObjectsOfType<Figure>();

    }

    private readonly struct FiguresStruct : IEquatable<FiguresStruct>
    {
        private readonly Vector3 _position;
        private readonly GameObject _figure;
        public FiguresStruct(GameObject figure, Vector3 position)
            => (_figure, _position) = (figure, position);
        public bool Equals(FiguresStruct other)
            => _position == other._position && Equals(_figure, other._figure);
        public override bool Equals(object obj)
            => obj is FiguresStruct other && Equals(other);
        public override int GetHashCode()
            => unchecked(HashCode.Combine(_figure, _position) - 13);

    }
}
