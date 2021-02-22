using System.Collections.Generic;

namespace BookStore.Domain
{
    public class Location
    {
        public Location()
        {
            Inventory = new Dictionary<Product, int>();
        }

        public Location(int id)
        {
            Inventory = new Dictionary<Product, int>();
            ID = id;
        }

        public Location(string name)
        {
            Name = name;
            Inventory = new Dictionary<Product, int>();
        }

        public int ID {get;}
        public string Name {get; set;}
        public Dictionary<Product, int> Inventory;

        public bool SetProductAmount(Product p, int amount)
        {
            if (p == null || amount < 0) 
            {
                return false;
            }
            
            Inventory[p] = amount;
            return true;
        }

        public bool SetProductAmount(int productID, int amount)
        {
            if (productID < 1 || amount < 0) 
            {
                return false;
            }

            Product toAssign = null;
            foreach(KeyValuePair<Product, int> entry in Inventory)
            {
                if (entry.Key.ID == productID)
                {
                    toAssign = entry.Key;
                    break;
                }
            }
            if (toAssign != null) 
            {
                Inventory[toAssign] = amount;
                return true;
            }
            return false;
        }

        public int GetProductAmount(Product p)
        {
            if (p == null || !Inventory.ContainsKey(p)) 
            {
                return -1;
            }
            return Inventory[p];
        }

        public int GetProductAmount(int productID)
        {
            if (productID < 1)
            {
                return -1;
            }
            foreach(KeyValuePair<Product, int> entry in Inventory)
            {
                if (entry.Key.ID == productID)
                {
                    return entry.Value;
                }
            }
            return -1;
        }

        public bool WithdrawProduct(Product p, int amount)
        {
            if (p == null || amount < 1 || amount > Inventory[p])
            {
                return false;
            }
            else
            {
                Inventory[p] -= amount;
                return true;
            }
        }

        public bool WithdrawProduct(int productID, int amount)
        {
            if (productID < 1 || amount < 1)
            {
                return false;
            }
            foreach(KeyValuePair<Product, int> entry in Inventory)
            {
                if (entry.Key.ID == productID)
                {
                    if (amount > Inventory[entry.Key])
                    {
                        return false;
                    }
                    else
                    {
                        Inventory[entry.Key] -= amount;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RemoveProduct(Product p)
        {
            if (p == null)
            {
                return false;
            }
            return Inventory.Remove(p);
        }

        public bool RemoveProduct(int productID)
        {
            Product toRemove = null;
            foreach(KeyValuePair<Product, int> entry in Inventory)
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
            
            return Inventory.Remove(toRemove);
        }
    }
}