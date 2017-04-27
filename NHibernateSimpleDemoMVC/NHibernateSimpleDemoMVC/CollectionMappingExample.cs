using System;
using System.Collections.Generic;

namespace NHibernateSimpleDemoMVC
{
    // Example: Component mapping classes - Customer, Address
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Address Address { get; set; }

        public Customer()
        {
        }
    }

    public class Address
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }

        public Address()
        {

        }
    }

    // Example: many-to-many mapping classes - Product, Order
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Order> _Orders { get; set; }
        
        public Product()
        {
            this._Orders = new List<Order>();
        }

        public virtual void AddOrder(Order order)
        {
            this._Orders.Add(order);
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

        public virtual void AddOrderItem(Product product)
        {
            this._OrderItems.Add(product);
            product.AddOrder(this);
        }
    }

    // Example: one-to-many bi-directional mapping
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual IList<Comment> Comments { get; set; }

        public Post()
        {
            this.Comments = new List<Comment>();
        }

        public virtual void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
            comment.Post = this;
        }

        public virtual void RemoveComment(Comment comment)
        {
            this.Comments.Remove(comment);
        }
    }

    public class Comment
    {
        public virtual int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual string Text { get; set; }

        public Comment()
        {
            
        }
    }
}
