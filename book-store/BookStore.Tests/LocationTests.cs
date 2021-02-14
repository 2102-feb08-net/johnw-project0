using System;
using System.Collections.Generic;
using Xunit;
using BookStore.Domain;

namespace BookStore.Tests
{
    public class LocationTests
    {
        private Location l;

        public LocationTests()
        {
            l = new Location();
            l.AddProduct(4, 10);
        }

        [Fact]
        public void Location_GetInventory()
        {
            Dictionary<int, int> i = l.Inventory;

            Assert.NotNull(i);
        }
        
        [Fact]
        public void Location_AddProduct_Pass()
        {
            l.AddProduct(1, 20);

            Assert.Equal(20, l.Inventory[1]);
        }

        [Fact]
        public void Location_AddProduct_Fail()
        {
            bool done = l.AddProduct(2, 2);
            Assert.True(done);
            done = l.AddProduct(2, 3);
            Assert.False(done);
        }

        [Fact]
        public void Location_RemoveProduct_Pass()
        {
            bool done = l.AddProduct(3, 10);
            Assert.True(done);
            done = l.RemoveProduct(3);
            Assert.True(done);
        }

        [Fact]
        public void Location_RemoveProduct_Fail()
        {
            bool done = l.RemoveProduct(3);
            Assert.False(done);
        }

        [Fact]
        public void Location_IncreaseProduct()
        {
            int current = l.Inventory[4];
            l.IncreaseProduct(4, 15);
            Assert.Equal(current+15, l.Inventory[4]);
        }

        [Fact]
        public void Location_DecreaseProduct()
        {
            int current = l.Inventory[4];
            l.DecreaseProduct(4, 2);
            Assert.Equal(current-2, l.Inventory[4]);
        }

        [Fact]
        public void Location_OutOfStock()
        {
            Assert.Throws<OutOfStockException>(() => l.DecreaseProduct(4, 9999));  
        }       
    }
}