using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernateSimpleDemoMVC
{
    public class CustomerMap : ClassMapping<Customer>
    {
        public CustomerMap()
        {
            Id(p => p.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            Property(x => x.Name, map => { map.Length(150); map.NotNullable(true); });

            Component(x => x.Address, c =>
            {
                // mappings for component's parts
                c.Property(x => x.Street);
                c.Property(x => x.City);
                c.Property(x => x.State);
                c.Property(x => x.Zip);
            });
        }
    }

    class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.Name, map => { map.NotNullable(true); map.Length(50); });

            Bag<Order>("_Orders",
                colmap => { colmap.Table("OrderItems"); colmap.Key(x => x.Column("[Product]")); colmap.Cascade(Cascade.All); },
                map => map.ManyToMany(x => x.Column("[Order]")));
        }
    }

    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Id(p => p.Id, m =>
            {
                m.Column("Id");
                m.Generator(Generators.Identity);
            });

            ManyToOne(x => x.Customer, p => { p.Cascade(Cascade.All); });
            Property(x => x.OrderDate);

            Bag<Product>("_OrderItems",
                colmap => { colmap.Table("OrderItems"); colmap.Key(x => x.Column("[Order]")); colmap.Cascade(Cascade.All); colmap.Inverse(true); },
                map => map.ManyToMany(x => x.Column("[Product]")));
        }
    }

    class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            Bag(x => x.Comments, colmap => { colmap.Key(x => x.Column("Post")); colmap.Cascade(Cascade.All | Cascade.DeleteOrphans); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }

    class CommentMap : ClassMapping<Comment>
    {
        public CommentMap()
        {
            Lazy(true);
            Cache(x => x.Usage(CacheUsage.ReadWrite));

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.Text, map => { map.NotNullable(true); map.Length(100); });

            ManyToOne(x => x.Post);
        }
    }
}
