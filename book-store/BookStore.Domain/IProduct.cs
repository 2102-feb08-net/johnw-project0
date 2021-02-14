using System;

namespace BookStore.Domain
{
    public interface IProduct
    {
        string Name {get;}
        double Price {get;}
    }
}