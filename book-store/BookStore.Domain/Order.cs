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

        public bool SetItemAmount(Product p, int amount)
        {
            if (p == null || amount < 0) 
            {
                return false;
            }
            Items[p] = amount;
            return true;
        }

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

        public int GetItemAmount(Product p)
        {
            if (p == null || !Items.ContainsKey(p)) 
            {
                return -1;
            }
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
            if (p == null)
            {
                return false;
            }
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