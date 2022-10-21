using System;
using Bomen;
using NUnit.Framework;

namespace BomenTests
{
    public class TreeTests
    {
        private Tree _tree; 

        [SetUp]
        public void Setup()
        {
            _tree = new Tree();
        }

        [Test]
        public void Water_Tree_1point5L_Not_Enough()
        {
            var result = _tree.WaterTree(1.5f);
            Assert.False(result);
        }

        [Test]
        public void Water_Tree_Twice_1point5L_Is_Enough()
        {
            var result1 = _tree.WaterTree(1.5f);
            var result2 = _tree.WaterTree(1.5f);
            Assert.False(result1);
            Assert.True(result2);
        }

        [Test]
        public void Water_Tree_Too_Much_Throw_Exception()
        {
            var ex = Assert.Throws<Exception>(() => _tree.WaterTree(3.1f));
            Assert.That(ex.Message, Is.EqualTo("The tree was given too much water!! This is probably not efficient because that costs extra time."));
        }
    }
}