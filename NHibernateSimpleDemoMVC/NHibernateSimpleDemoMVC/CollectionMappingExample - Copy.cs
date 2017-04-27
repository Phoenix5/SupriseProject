using System;
using System.Collections.Generic;

namespace NHibernateSimpleDemoMVC
{
    // Example: Component collection mapping classes - Customer, Address, Number
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual HomeAddress HomeAddress { get; set; }
        public virtual IList<Address> Addresses { get; set; }

        public Customer()
        {
            this.Addresses = new List<Address>();
        }
    }

    public class Address
    {
        public virtual Customer Owner { get; set; }
        public virtual string Street { get; set; }
        public virtual Number Number { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }

        public Address()
        {
            
        }
    }

    public class Number
    {
        public virtual Address OwnerAddress { get; set; }
        public virtual int Block { get; set; }

        public Number()
        {
            
        }
    }

    public class HomeAddress
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }

        public HomeAddress()
        {

        }
    }

    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Order> _Orders { get; set; }
        
        public Product()
        {
            this._Orders = new List<Order>();
        }
    }

    public class Order
    {
        public virtual int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual IList<Product> _OrderItems { get; set; }

        public Order()
        {
            this._OrderItems = new List<Product>();
        }
    }
}
