using System;
using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Order
    {
        public Order()
        {
            Items = new Dictionary<Product, int>();
        }

        public Order(int customerID, int locationID)
        {
            CustomerID = customerID;
            LocationID = locationID;
            Items = new Dictionary<Product, int>();
        }

        public Order(int id, int customerID, int locationID)
        {
            ID = id;
            CustomerID = customerID;
            LocationID = locationID;
            Items = new Dictionary<Product, int>();
        }

        /// <summary>
        /// An identifier for the Order.
        /// </summary>
        public int ID {get;}

        /// <summary>
        /// An identifier for the Customer who placed the Order.
        /// </summary>
        public int CustomerID {get; set;}

        /// <summary>
        /// An identifier for the Location where the Order was placed.
        /// </summary>
        public int LocationID {get; set;}

        /// <summary>
        /// The mapping of products in the Order, where the key is the Product and the value is the amount of that Product that was ordered.
        /// </summary>
        public Dictionary<Product, int> Items;

        /// <summary>
        /// The total price of the Order based on the items that were ordered.
        /// </summary>
        public double Total 
        {
            get
            {
                double t = 0.0;
                foreach(KeyValuePair<Product, int> entry in Items)
                {
                    double s = (double)entry.Key.Price * entry.Value;
                    t += s;
                }
                return t;
            }
        }

        /// <summary>
        /// The date and time that the Order was placed.
        /// </summary>
        public DateTimeOffset Time {get; set;}

        /// <summary>
        /// Sets the amount of the given Product that was ordered.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="amount"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool SetItemAmount(Product p, int amount)
        {
            if (p == null || amount < 0) 
            {
                return false;
            }
            Items[p] = amount;
            return true;
        }

        /// <summary>
        /// Sets the amount of the Product matching the given id that was ordered.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="amount"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool SetItemAmount(int productID, int amount)
        {
            if (productID < 1 || amount < 0) 
            {
                return false;
            }
            Product toAssign = null;
            foreach(KeyValuePair<Product, int> entry in Items)
            {
                if (entry.Key.ID == productID)
                {
                    toAssign = entry.Key;
                    break;
                }
            }
            if (toAssign != null) {
                Items[toAssign] = amount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the amount of the given Product that was ordered
        /// </summary>
        /// <param name="p"></param>
        /// <returns> int </returns>
        public int GetItemAmount(Product p)
        {
            if (p == null || !Items.ContainsKey(p)) 
            {
                return -1;
            }
            return Items[p];
        }

        /// <summary>
        /// Returns the amount of the Product matching the given ID that was ordered.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns> int </returns>
        public int GetItemAmount(int productID)
        {
            foreach(KeyValuePair<Product, int> entry in Items)
            {
                if (entry.Key.ID == productID)
                {
                    return entry.Value;
                }
            }
            return -1;
        }

        /// <summary>
        /// Removes the given Product from the Order.
        /// </summary>
        /// <param name="p"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool RemoveItem(Product p)
        {
            if (p == null)
            {
                return false;
            }
            return Items.Remove(p);
        }

        /// <summary>
        /// Removes the Product matching the given ID from the Order.
        /// </summary>
        /// <param name="productID"></param>
        /// <returns> bool indicating success or failure </returns>
        public bool RemoveItem(int productID)
        {
            Product toRemove = null;
            foreach(KeyValuePair<Product, int> entry in Items)
            {
                if (entry.Key.ID == productID)
                {
                    toRemove = entry.Key;
                    break;
                }
            }
            
            if (toRemove == null) {
                return false;
            }
            
            return Items.Remove(toRemove);
        }
    }
}