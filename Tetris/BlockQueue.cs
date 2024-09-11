using System;
using Tetris.Blocks;

namespace Tetris
{
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }

        public BlockQueue()
        {
            this.NextBlock = RandomBlock();
        }

        private Block RandomBlock()
        {
            return this.blocks[this.random.Next(this.blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = this.NextBlock;
            do
            {
                this.NextBlock = this.RandomBlock();
            }
            while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
