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

        public int ID {get;}
        public int CustomerID {get; set;}
        public int LocationID {get; set;}
        private Dictionary<Product, int> Items;
        public double Total 
        {
            get
            {
                double t = 0.0;
                foreach(KeyValuePair<Product, int> entry in Items)
                {
                    double s = entry.Key.Price * entry.Value;
                    t += s;
                }
                return t;
            }
        }
        public DateTimeOffset Time {get; set;}

        public void SetItemAmount(Product p, int amount)
        {
            Items[p] = amount;
        }

        public void SetItemAmount(int productID, int amount)
        {
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
            }
        }

        public int GetItemAmount(Product p)
        {
            return Items[p];
        }

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

        public bool RemoveItem(Product p)
        {
            return Items.Remove(p);
        }

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