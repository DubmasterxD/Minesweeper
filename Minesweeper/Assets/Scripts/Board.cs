using UnityEngine;

namespace Minesweeper
{
    public class Board : MonoBehaviour
    {
        [SerializeField] Field fieldPrefab = null;

        public Options options { get; set; } = null;

        Field[,] fields = new Field[0,0];

        public void CreateFields(int rowsNumber, int columnsNumber)
        {
            fields = new Field[rowsNumber, columnsNumber];
            for (int row = 0; row < rowsNumber; row++)
            {
                for (int column = 0; column < columnsNumber; column++)
                {
                    fields[row, column] = Instantiate(fieldPrefab, transform);
                    fields[row, column].options = options;
                }
            }
            AssignNearbyFields();
        }

        public void SetMines(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomRow = Random.Range(0, fields.GetLength(0));
                int randomColumn = Random.Range(0, fields.GetLength(1));
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

        public void RevealAllMines()
        {
            for (int row = 0; row <= GetLastRowIndex(); row++)
            {
                for (int column = 0; column <= GetLastColumnIndex(); column++)
                {
                    if (fields[row, column].isMine)
                    {
                        fields[row, column].RevealMine();
                    }
                }
            }
        }

        public void Clear()
        {
            foreach(Field field in fields)
            {
                Destroy(field.gameObject);
            }
        }

        public int CountFieldsNotRevealed()
        {
            int sum = 0;
            foreach(Field field in fields)
            {
                if(!field.isRevealed)
                {
                    sum++;
                }
            }
            return sum;
        }

        private void AssignNearbyFields()
        {
            for(int row = 0; row <= GetLastRowIndex(); row++)
            {
                for (int column = 0; column <= GetLastColumnIndex(); column++)
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
                if (column < GetLastColumnIndex())
                {
                    fields[row, column].AssignNearbyField(fields[row - 1, column + 1]);
                }
            }
            if (row < GetLastRowIndex())
            {
                fields[row, column].AssignNearbyField(fields[row + 1, column]);

                if (column > 0)
                {
                    fields[row, column].AssignNearbyField(fields[row + 1, column - 1]);
                }
                if (column < GetLastColumnIndex())
                {
                    fields[row, column].AssignNearbyField(fields[row + 1, column + 1]);
                }
            }
            if (column > 0)
            {
                fields[row, column].AssignNearbyField(fields[row, column - 1]);
            }
            if (column < GetLastColumnIndex())
            {
                fields[row, column].AssignNearbyField(fields[row, column + 1]);
            }
        }

        private void IncreaseNeighbouringMinesCount(int row, int column)
        {
            foreach (Field field in fields[row,column].nearbyFields)
            {
                field.IncreaseNumberOfMinesNearby();
            }
        }

        private int GetLastRowIndex()
        {
            return fields.GetLength(0) - 1;
        }

        private int GetLastColumnIndex()
        {
            return fields.GetLength(1) - 1;
        }
    }
}
