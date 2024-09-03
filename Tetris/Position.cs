﻿namespace Tetris
{
    public class Position
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        public Position(int rowIndex, int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }
    }
}
