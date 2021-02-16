using System;
using Xunit;
using System.Collections.Generic;
using BookStore.Domain;

namespace BookStore.Tests
{
    public class OrderTests
    {
        // private Order o;
        // public OrderTests()
        // {
        //     o = new Order();
        // }
        // // test customer
        // [Theory]
        // [InlineData(1)]
        // [InlineData(2)]
        // public void Order_TestCustomer(int customerID)
        // {
        //     o.SetCustomer(customerID);
        //     int k = o.GetCustomer();
        //     Assert.Equal(customerID, k);
        // }

        // // test location
        // [Theory]
        // [InlineData(1)]
        // [InlineData(2)]
        // public void Order_TestLocation(int locationID)
        // {
        //     o.SetLocation(locationID);
        //     int l = o.GetLocation();
        //     Assert.Equal(locationID, l);
        // }

        // [Fact]
        // public void Order_TestItems()
        // {
        //     Dictionary<int, int> i = o.Items;

        //     Assert.NotNull(i);
        // }

        // // add item
        // [Theory]
        // [InlineData(3, 5)]
        // [InlineData(2, 10)]
        // public void Order_AddItem_Pass(int productID, int amt)
        // {
        //     o.AddItem(productID, amt);

        //     Assert.Equal(amt, o.Items[productID]);
        // }

        // [Theory]
        // [InlineData(-1, 2)]
        // [InlineData(5, 0)]
        // public void Order_AddItem_Fail(int productID, int amt)
        // {
        //     bool done = o.AddItem(productID, amt);

        //     Assert.False(done);
        // }

        // // remove item
        // [Fact]
        // public void Order_RemoveItem_Pass()
        // {
        //     bool done = o.AddItem(6. 10);
        //     Assert.True(done);
        //     done = o.RemoveItem(6);
        //     Assert.True(done);
        // }

        // [Fact]
        // public void Order_RemoveItem_Fail()
        // {
        //     bool done = l.RemoveItem(6);
        //     Assert.False(done);
        // }
        
        // [Fact]
        // public void Order_SetItemCount_Pass()
        // {
        //     bool done = l.AddItem(18, 5);
        //     Assert.True(done);
        //     done = o.SetItemCount(18, 3);
        //     Assert.True(done);
        // }

        // [Theory]
        // [InlineData(-4, 2)]
        // [InlineData(10, -3)]
        // public void Order_SetItemCount_Fail(int productID, int amt)
        // {
        //     bool done = o.SetItemCount(productID, amt);
        //     Assert.False(done);
        // }

        // // check price total
        // [Fact]
        // public void Order_GetPriceTotal()
        // {
        //     l.AddItem(22, 1);
        //     l.AddItem()
        // }
    }
}