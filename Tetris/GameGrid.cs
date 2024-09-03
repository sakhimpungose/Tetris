using System;
using System.Linq;

namespace Tetris
{
    public class GameGrid
    {
        private readonly int[,] Grid;
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }

        public int this[int rowIndex, int columnIndex]
        {
            get => this.Grid[rowIndex, columnIndex];
            set => this.Grid[rowIndex, columnIndex] = value;
        }

        public GameGrid(int numberOfRows, int numberOfColumns)
        {
            this.NumberOfRows = numberOfRows;
            this.NumberOfColumns = numberOfColumns;
            this.Grid = new int[this.NumberOfRows, this.NumberOfColumns];
        }

        public bool IsInside(int rowIndex, int columnIndex)
        {
            return rowIndex >= 0 && rowIndex < this.NumberOfRows && columnIndex >= 0 && columnIndex < this.NumberOfColumns;
        }

        public bool IsEmpty(int rowIndex, int columnIndex)
        {
            return this.IsInside(rowIndex, columnIndex) && this.Grid[rowIndex, columnIndex] == 0;
        }

        public void SetUnit(int rowIndex, int columnIndex, int value)
        {
            this.Grid[rowIndex, columnIndex] = value;
        }

        public void ClearUnit(int rowIndex, int columnIndex)
        {
            this.SetUnit(rowIndex, columnIndex, 0);
        }

        public void FillUnit(int rowIndex, int columnIndex)
        {
            this.SetUnit(rowIndex, columnIndex, 1);
        }

        public bool IsRowFull(int rowIndex)
        {
            return Enumerable.Range(0, this.NumberOfColumns - 1).All(columnIndex =>
            {
                return this.Grid[rowIndex, columnIndex] > 0;
            });
        }

        public bool IsRowEmpty(int rowIndex)
        {
            return Enumerable.Range(0, this.NumberOfColumns - 1).All(columnIndex =>
            {
                return this.Grid[rowIndex, columnIndex] == 0;
            });
        }

        public void ClearRow(int rowIndex)
        {
            Enumerable.Range(0, this.NumberOfColumns - 1).ToList().ForEach(columnIndex =>
            {
                this.ClearUnit(rowIndex, columnIndex);
            });
        }


        private void MoveRowDown(int rowIndex, int numberOfRows)
        {
            Enumerable.Range(0, this.NumberOfColumns - 1).ToList().ForEach(columnIndex =>
            {
                this.SetUnit(rowIndex + numberOfRows, columnIndex, this.Grid[rowIndex, columnIndex]);
                this.ClearUnit(rowIndex, columnIndex);
            });
        }

        public int ClearFullRows()
        {
            int numberOfClearedRows = 0;
            for(int rowIndex = this.NumberOfRows - 1; rowIndex >= 0; rowIndex--)
            {
                if (this.IsRowFull(rowIndex))
                {
                    this.ClearRow(rowIndex);
                    numberOfClearedRows++;
                }
                else if (numberOfClearedRows > 0)
                {
                    this.MoveRowDown(rowIndex, numberOfClearedRows);
                }
            }

            return numberOfClearedRows;
        }
    }
}
