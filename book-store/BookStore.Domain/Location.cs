using System;
using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Location
    {
        public Location()
        {
            Inventory = new Dictionary<int, int>();
        }

        public Dictionary<int, int> Inventory {get;}

        public bool AddProduct(int productID, int quantity)
        {
            try 
            {
                Inventory.Add(productID, quantity);
            } catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public bool RemoveProduct(int productID)
        {
            return Inventory.Remove(productID);
        }

        public void IncreaseProduct(int productID, int incAmt)
        {
            Inventory[productID] += incAmt;
        }

        public void DecreaseProduct(int productID, int decAmt)
        {
            int current = Inventory[productID];
            if (current < decAmt) {
                throw new OutOfStockException("Insufficient stock for this product.", productID);
            } else {
                Inventory[productID] -= decAmt;
            }
        }
    }

    [System.Serializable]
    public class OutOfStockException : System.Exception
    {
        public int ProductID {get;}
        public OutOfStockException() { }
        public OutOfStockException(string message) : base(message) { }
        public OutOfStockException(string message, System.Exception inner) : base(message, inner) { }
        public OutOfStockException(string message, int productID) : this(message)
        {
            ProductID = productID;
        }
    }
}