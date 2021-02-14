using System;
using Xunit;
using BookStore.Domain;

namespace BookStore.Tests
{
    public class ProductTests
    {
        private Product p;
        private string testName;
        private double testPrice;

        public ProductTests()
        {
            testName = "Green Eggs & Ham";
            testPrice = 4.99; 
            p = new Product(testName, testPrice);
        }

        [Fact]
        public void Product_GetName()
        {
            string pName = p.Name;

            Assert.Equal(pName, testName);
        }

        [Fact]
        public void Product_GetPrice()
        {
            double pPrice = p.Price;

            Assert.Equal(pPrice, testPrice);
        }
    }
}