using Enums;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Data.ValueObject
{
    [System.Serializable]
    public class LevelVO
    {
        [OnValueChanged("CreateGrid")]
        public int GridWidth = 4;
        [OnValueChanged("CreateGrid")]
        public int GridHeight = 4;

        [ShowInInspector]
        [TableMatrix(SquareCells = true, DrawElementMethod = "DrawElement")]
        private TileVO[,] _editorGrid;
        [HideInInspector] public List<TileVO> Grid;

#if UNITY_EDITOR

        [OnInspectorInit]
        private void OnInit()
        {
            if (Grid != null)
            {
                _editorGrid = new TileVO[GridWidth, GridHeight];
                Deserialize();
            }
            else
            {
                CreateGrid();
            }
        }

        private TileVO DrawElement(Rect rect, TileVO tile)
        {
            switch (tile.TileType)
            {
                case TileTypes.Empty:
                    EditorGUI.DrawRect(rect.Padding(1), Color.white);
                    break;
                case TileTypes.Wall:
                    EditorGUI.DrawRect(rect.Padding(1), Color.red);
                    break;
                default:
                    break;
            }

            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                if (Event.current.button == 0)
                {
                    if (tile.TileType == TileTypes.Empty)
                    {
                        tile.TileType = TileTypes.Wall;
                    }
                    else
                    {
                        tile.TileType = TileTypes.Empty;
                    }
                }

                Serialize();
                GUI.changed = true;
                Event.current.Use();
            }

            return tile;
        }

        private void CreateGrid()
        {
            Grid = new List<TileVO>();
            _editorGrid = new TileVO[GridWidth, GridHeight];

            for (int i = 0; i < GridHeight * GridWidth; i++)
            {
                Grid.Add(new TileVO { TileType = TileTypes.Empty });
            }

            Deserialize();
        }

        private void Serialize()
        {
            for (int i = 0; i < GridWidth; i++)
            {
                for (int j = 0; j < GridHeight; j++)
                {
                    Grid[i * GridHeight + j] = _editorGrid[i, j];
                }
            }
        }
        private void Deserialize()
        {
            for (int i = 0; i < GridWidth; i++)
            {
                for (int j = 0; j < GridHeight; j++)
                {
                    _editorGrid[i, j] = Grid[i * GridHeight + j];
                }
            }
        }
#endif
    }
}