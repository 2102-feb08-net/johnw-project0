using System;
using Xunit;
using System.Collections.Generic;
using BookStore.Domain;

namespace BookStore.Tests
{
    public class OrderTests
    {
        private Order o;
        private int testCustomerID = 1;
        private int testLocationID = 2;
        private double testProductPrice = 10.99;
        private int testAmount = 4;
        private Product p1;
        private Product p2;
        private Product p3;

        public OrderTests()
        {
            o = new Order(testCustomerID, testLocationID);

            p1 = new Product(1, "The Lord of the Rings: The Fellowship of the Ring", 10.99);
            p2 = new Product(2, "The Lord of the Rings: The Two Towers", 10.99);
            p3 = new Product(3, "The Lord of the Rings: The Return of the King", 10.99);

            o.SetItemAmount(p1, testAmount);
            o.SetItemAmount(p2, testAmount);
            o.SetItemAmount(p3, testAmount);
        }
        
        // test customer id
        [Fact]
        public void Order_TestGetCustomerID()
        {
            int c = o.CustomerID;
            Assert.Equal(testCustomerID, c);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Order_TestSetCustomerID(int newID)
        {
            o.CustomerID = newID;
            int c = o.CustomerID;
            Assert.Equal(newID, c);
        }

        // test location id
        [Fact]
        public void Order_TestGetLocationID()
        {
            int i = o.LocationID;
            Assert.Equal(testLocationID, i);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        public void Order_TestSetLocationID(int newID)
        {
            o.LocationID = newID;
            int i = o.LocationID;
            Assert.Equal(newID, i);
        }

        // test get item amount
        
        // test set item amount

        // test remove item

        // test price

        // test time
    }
}