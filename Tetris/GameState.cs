namespace Tetris
{
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => this.currentBlock;
            private set
            {
                this.currentBlock = value;
                currentBlock.Reset();
                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }

        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get;  }
        public bool GameOver { get; private set; }

        public int Score { get; private set; }

        public Block HeldBlock { get; private set; }
        public bool CanHold { get; private set; }



        public GameState()
        {
            this.GameGrid = new GameGrid(22, 10);
            this.BlockQueue = new BlockQueue();
            this.CurrentBlock = BlockQueue.GetAndUpdate();
            this.CanHold = true;
        }

        private bool BlockFits()
        {
            foreach (Position p in this.CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.RowIndex, p.ColumnIndex))
                {
                    return false;
                }
            }

            return true;
        }

        public void HoldBlock()
        {
            if (!this.CanHold)
            {
                return;
            }

            if (this.HeldBlock == null)
            {
                this.HeldBlock = this.CurrentBlock;
                this.CurrentBlock = this.BlockQueue.GetAndUpdate();
            }
            else
            {
                Block tmpBlock = this.CurrentBlock;
                this.CurrentBlock = this.HeldBlock;
                this.HeldBlock = tmpBlock;
            }

            this.CanHold = false;
        }

        public void RotateBlockClockwise()
        {
            this.CurrentBlock.RotateClockwise();
            if (!this.BlockFits())
            {
                CurrentBlock.RotateCounterClockwise();
            }
        }

        public void RotateBlockCounterClockwise()
        {
            this.CurrentBlock.RotateCounterClockwise();
            if (!this.BlockFits())
            {
                CurrentBlock.RotateClockwise();
            }
        }

        public void MoveBlockLeft()
        {
            this.CurrentBlock.Move(0, -1);
            if (!this.BlockFits())
            {
                this.CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            this.CurrentBlock.Move(0, 1);
            if (!this.BlockFits())
            {
                this.CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(this.GameGrid.IsRowEmpty(0) && this.GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach (Position p in this.CurrentBlock.TilePositions())
            {
                this.GameGrid.SetUnit(p.RowIndex, p.ColumnIndex, CurrentBlock.Id);
            }

            this.Score += this.GameGrid.ClearFullRows();
            if (this.IsGameOver())
            {
                this.GameOver = true;
            }
            else
            {
                this.CurrentBlock = this.BlockQueue.GetAndUpdate();
                this.CanHold = true;
            }
        }

        public void MoveBlockDown()
        {
            this.CurrentBlock.Move(1, 0);
            if (!this.BlockFits())
            {
                this.CurrentBlock.Move(-1, 0);
                this.PlaceBlock();
            }
        }

        private int TileDropDistance(Position p)
        {
            int drop = 0;
            while (this.GameGrid.IsEmpty(p.RowIndex + drop + 1, p.ColumnIndex))
            {
                drop++;
            }

            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = this.GameGrid.NumberOfRows;
            foreach(Position p in this.CurrentBlock.TilePositions())
            {
                drop = Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        public void DropBlock()
        {
            this.CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
