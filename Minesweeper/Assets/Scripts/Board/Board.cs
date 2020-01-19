using Minesweeper.General;
using UnityEngine;

namespace Minesweeper.Board
{
    public class Board : MonoBehaviour
    {
        [SerializeField] Field fieldPrefab = null;

        Field[,] fields;

        GameManager game;
        Options options;
        
        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
            options = FindObjectOfType<Options>();
        }

        private void Start()
        {
            if (game != null)
            {
                game.onGameStart += GameStart;
                game.onGameEnd += GameOver;
                game.tileSize = (int)fieldPrefab.GetComponent<RectTransform>().rect.height;
            }
            CreateFields(options.MaxRows, options.MaxColumns);
            AssignNearbyFields();
            HideAllFields();
        }

        private void GameStart()
        {
            HideAllFields();
            ActivateFields(options.rowsNumber, options.columnsNumber);
            SetMines(options.minesNumber);
        }

        private void GameOver()
        {
            RevealAllMines();
        }

        private void CreateFields(int rowsNumber, int columnsNumber)
        {
            fields = new Field[rowsNumber, columnsNumber];
            for (int row = 0; row < rowsNumber; row++)
            {
                for (int column = 0; column < columnsNumber; column++)
                {
                    fields[row, column] = Instantiate(fieldPrefab, transform);
                }
            }
        }

        private void HideAllFields()
        {
            if (fields != null)
            {
                foreach (Field field in fields)
                {
                    field.gameObject.SetActive(false);
                }
            }
        }

        private void ActivateFields(int rowsNumber, int columnsNumber)
        {
            for (int row = 0; row < rowsNumber; row++)
            {
                for (int column = 0; column < columnsNumber; column++)
                {
                    fields[row, column].gameObject.SetActive(true);
                    fields[row, column].ResetField();
                }
            }
        }

        private void SetMines(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomRow = Random.Range(0, options.rowsNumber);
                int randomColumn = Random.Range(0, options.columnsNumber);
                if (fields[randomRow, randomColumn].isMine)
                {
                    i--;
                    continue;
                }
                else
                {
                    fields[randomRow, randomColumn].isMine = true;
                    IncreaseNeighbouringMinesCount(randomRow, randomColumn);
                }
            }
        }

        private void IncreaseNeighbouringMinesCount(int row, int column)
        {
            foreach (Field field in fields[row, column].GetNearbyFields())
            {
                if (field.gameObject.activeInHierarchy)
                {
                    field.IncreaseNumberOfMinesNearby();
                }
            }
        }

        public void RevealAllMines()
        {
            for (int row = 0; row < options.rowsNumber; row++)
            {
                for (int column = 0; column < options.columnsNumber; column++)
                {
                    if (fields[row, column].isMine)
                    {
                        fields[row, column].RevealMine();
                    }
                }
            }
        }

        private void AssignNearbyFields()
        {
            for (int row = 0; row < options.MaxRows; row++)
            {
                for (int column = 0; column < options.MaxColumns; column++)
                {
                    AssignNearbyFields(row, column);
                }
            }
        }

        private void AssignNearbyFields(int row, int column)
        {
            if (row > 0)
            {
                fields[row, column].AssignNearbyField(fields[row - 1, column]);

                if (column > 0)
                {
                    fields[row, column].AssignNearbyField(fields[row - 1, column - 1]);
                }
                if (column < options.MaxColumns - 1)
                {
                    fields[row, column].AssignNearbyField(fields[row - 1, column + 1]);
                }
            }
            if (row < options.MaxRows - 1)
            {
                fields[row, column].AssignNearbyField(fields[row + 1, column]);

                if (column > 0)
                {
                    fields[row, column].AssignNearbyField(fields[row + 1, column - 1]);
                }
                if (column < options.MaxColumns - 1)
                {
                    fields[row, column].AssignNearbyField(fields[row + 1, column + 1]);
                }
            }
            if (column > 0)
            {
                fields[row, column].AssignNearbyField(fields[row, column - 1]);
            }
            if (column < options.MaxColumns - 1)
            {
                fields[row, column].AssignNearbyField(fields[row, column + 1]);
            }
        }
    }
}
