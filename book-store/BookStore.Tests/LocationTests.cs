using System;
using System.Collections.Generic;
using Xunit;
using BookStore.Domain;

namespace BookStore.Tests
{
    public class LocationTests
    {
        private Location l;
        private string testName = "Downtown";
        private int testAmount = 4;
        private Product p1;
        private Product p2;
        private Product p3;

        public LocationTests()
        {
            l = new Location(testName);
            
            p1 = new Product(1, "The Lord of the Rings: The Fellowship of the Ring", 10.99);
            p2 = new Product(2, "The Lord of the Rings: The Two Towers", 10.99);
            p3 = new Product(3, "The Lord of the Rings: The Return of the King", 10.99);

            l.SetProductAmount(p1, testAmount);
            l.SetProductAmount(p2, testAmount);
            l.SetProductAmount(p3, testAmount);
        }

        // test name
        [Fact]
        public void Location_TestGetName()
        {
            string n = l.Name;
            Assert.Equal(testName, n);
        }

        [Theory]
        [InlineData("Uptown")]
        public void Location_TestSetName(string newName)
        {
            l.Name = newName;
            string n = l.Name;
            Assert.Equal(newName, n);
        }

        // test get inventory
        [Fact]
        public void Location_TestGetInventory()
        {
            Dictionary<Product, int> x = l.Inventory;
            Assert.NotNull(x);
        }

        // test get product amt
        [Fact]
        public void Location_TestGetProductAmt_Object_Pass()
        {
            int i = l.GetProductAmount(p1);
            Assert.Equal(testAmount, i);

            i = l.GetProductAmount(p2);
            Assert.Equal(testAmount, i);

            i = l.GetProductAmount(p3);
            Assert.Equal(testAmount, i);
        }

        [Fact]
        public void Location_TestGetProductAmt_Object_Fail()
        {
            int i = l.GetProductAmount(null);
            Assert.Equal(-1, i);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Location_TestGetProductAmt_ID_Pass(int productID)
        {
            int i = l.GetProductAmount(productID);
            Assert.Equal(testAmount, i);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(22)]
        public void Location_TestGetProductAmt_ID_Fail(int productID)
        {
            int i = l.GetProductAmount(productID);
            Assert.Equal(-1, i);
        }

        // test set product amt
        [Theory]
        [InlineData(5)]
        [InlineData(20)]
        [InlineData(0)]
        public void Location_TestSetProductAmt_Object_Pass(int amount)
        {
            bool done = l.SetProductAmount(p1, amount);
            Assert.True(done);

            int x = l.GetProductAmount(p1);
            Assert.Equal(amount, x);
        }

        [Fact]
        public void Location_TestSetProductAmt_Object_Fail()
        {
            bool done = l.SetProductAmount(null, 2);
            Assert.False(done);

            done = l.SetProductAmount(null, -1);
            Assert.False(done);

            done = l.SetProductAmount(p1, -1);
            Assert.False(done);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 0)]
        [InlineData(3, 30)]
        public void Location_TestSetProductAmt_ID_Pass(int productID, int amount)
        {
            bool done = l.SetProductAmount(productID, amount);
            Assert.True(done);

            int x = l.GetProductAmount(productID);
            Assert.Equal(amount, x);
        }

        [Theory]
        [InlineData(-1, 3)]
        [InlineData(2, -1)]
        [InlineData(-1, -1)]
        public void Location_TestSetProductAmt_ID_Fail(int productID, int amount)
        {
            bool done = l.SetProductAmount(productID, amount);
            Assert.False(done);
        }

        // test withdraw product amt
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        public void Location_TestWithdrawProduct_Object_Pass(int amount)
        {
            bool done = l.WithdrawProduct(p1, amount);
            Assert.True(done);

            done = l.WithdrawProduct(p2, amount);
            Assert.True(done);

            done = l.WithdrawProduct(p3, amount);
            Assert.True(done);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(9)]
        public void Location_TestWithdrawProduct_Object_Fail(int amount)
        {
            bool done = l.WithdrawProduct(null, 1);
            Assert.False(done);

            done = l.WithdrawProduct(p2, amount);
            Assert.False(done);

            done = l.WithdrawProduct(p3, amount);
            Assert.False(done);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Location_TestWithdrawProduct_ID_Pass(int productID, int amount)
        {
            bool done = l.WithdrawProduct(productID, amount);
            Assert.True(done);
        }

        [Theory]
        [InlineData(-1, 2)]
        [InlineData(2, 10)]
        public void Location_TestWithdrawProduct_ID_Fail(int productID, int amount)
        {
            bool done = l.WithdrawProduct(productID, amount);
            Assert.False(done);
        }

        // test remove product
        [Fact]
        public void Location_TestRemoveProduct_Object_Pass()
        {
            bool done = l.RemoveProduct(p1);
            Assert.True(done);

            int i = l.GetProductAmount(p1);
            Assert.Equal(-1, i);

            done = l.RemoveProduct(p2);
            Assert.True(done);

            i = l.GetProductAmount(p2);
            Assert.Equal(-1, i);

            done = l.RemoveProduct(p3);
            Assert.True(done);

            i = l.GetProductAmount(p3);
            Assert.Equal(-1, i);
        }

        [Theory]
        [InlineData(null)]
        public void Location_TestRemoveProduct_Object_Fail(Product p)
        {
            bool done = l.RemoveProduct(p);
            Assert.False(done);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Location_TestRemoveProduct_ID_Pass(int productID)
        {
            bool done = l.RemoveProduct(productID);
            Assert.True(done);

            int i = l.GetProductAmount(productID);
            Assert.Equal(-1, i);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public void Location_TestRemoveProduct_ID_Fail(int productID)
        {
            bool done = l.RemoveProduct(productID);
            Assert.False(done);
        }
    }
}