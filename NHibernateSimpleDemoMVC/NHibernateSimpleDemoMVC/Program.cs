using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateSimpleDemoMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure log4net for logging
            log4net.Config.XmlConfigurator.Configure();

            ISession session = NHibernateHelper.GetCurrentSession();
            

            // Example: Persist Single Object to DB, Persisting simple properties;
            //PersistSingleObject(session);

            // Example: Table Per Subclass Inheritance mapping by NHibernate Mapping-by-Code
            //InheritanceMapping(session);

            // Example: collection mapping by NHibernate Mapping-by-Code
            CollectionMapping(session);


            // Example: Bi-directional NHibernate mapping by code
            //BiDirectionalMapping(session);

            Console.ReadLine();

            NHibernateHelper.CloseSession();
        }

        // Example: Persist Single Object to DB
        public static void PersistSingleObject(ISession session)
        {
            //In addition to opening the session, we want to wrap our statements in a "transaction" to decrease database overhead.
            // To create a transaction for our session, all we need to do is tell the session to begin a transaction for us
            using (var tx = session.BeginTransaction())
            {
                Employee employee = new Employee();
                employee.Name = "Bob";
                employee.Phone = "Marley";

                session.Save(employee);
                tx.Commit();
            }
        }

        // Example: collection mapping by NHibernate Mapping-by-Code
        public static void CollectionMapping(ISession session)
        {
            // Example: component mapping by NHibernate Mapping-by-Code
            //ComponentCollectionMapping(session);
            
            // Example: one-to-many bi-directional mapping
            One2ManyCollectionMapping(session);

            // Example: many-to-many mapping
            //Many2ManyCollectionMapping(session);
        }

        // Example: one-to-many bi-directional mapping
        public static void One2ManyCollectionMapping(ISession session)
        {
            using (var tx = session.BeginTransaction())
            {
                Post post = new Post();
                var comment1 = new Comment() { Text = "1st comment" };
                var comment2 = new Comment() { Text = "2nd comment" };
                post.AddComment(comment1);
                post.AddComment(comment2);
                session.Save(post);
                post = new Post();
                comment1 = new Comment() { Text = "3rd comment" };
                comment2 = new Comment() { Text = "4th comment" };
                post.AddComment(comment1);
                post.AddComment(comment2);

                session.Save(post);
                tx.Commit();
            }

            // Remove from Collection
            using (var tx = session.BeginTransaction())
            {
                List<Post> posts = session.Query<Post>().ToList();

                Post post = posts.FirstOrDefault();

                Comment commentToBeRemoved = post.Comments.FirstOrDefault();
                post.RemoveComment(commentToBeRemoved);
                session.Save(post);
                tx.Commit();
            }
        }

        // Example: many-to-many mapping
        public static void Many2ManyCollectionMapping(ISession session)
        {
            using (var tx = session.BeginTransaction())
            {  
                Customer customer = new Customer
                {
                    Name = "Mr Bob M",
                    Address = new Address
                    {
                        Street = "121 Main Street",
                        City = "Kansas City",
                        State = "KS",
                        Zip = "63017",
                    }
                };

                Order order = new Order
                {
                    Customer = customer,
                    OrderDate = DateTime.Today,

                };

                Product product = new Product
                {
                    Name = "iPhone", 
                };

                order.AddOrderItem(product);

                product = new Product
                {
                    Name = "Samsung Galaxy",
                };

                order.AddOrderItem(product);

                session.Save(order);
                tx.Commit();
            }
        }

        // Example: collection mapping by NHibernate Mapping-by-Code
        public static void ComponentCollectionMapping(ISession session)
        {

        }

        // Example: Table Per Subclass Inheritance mapping by NHibernate Mapping-by-Code
        public static void InheritanceMapping()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();

            JuridicalPerson juridicalPerson = new JuridicalPerson{Name = "Tom",LegalName = "Mr Tom"};
            PrivatePerson privatePerson = new PrivatePerson {Name = "John", Gender = "Male" };

            session.Save(juridicalPerson);
            session.Save(privatePerson);
            tx.Commit();

            List<Person> persons = session.Query<Person>().ToList();

            List<JuridicalPerson> juridicalPersons = session.Query<JuridicalPerson>().ToList();
            List<PrivatePerson> privatePersons = session.Query<PrivatePerson>().ToList();
            Console.ReadLine();

            NHibernateHelper.CloseSession();
        }


        // Bi-directional NHibernate mapping by code
        public static void BiDirectionalMapping(ISession session)
        {
            ITransaction tx = session.BeginTransaction();

            Parent parent = new Parent();
            parent.Name = "Bob";
            Child child = parent.AddChild();
            child.Name = "Jr Bob";

            child = parent.AddChild();
            child.Name = "Jr Jr Bob";

            session.Save(parent);
            tx.Commit();

            List<Parent> parents = session.Query<Parent>().ToList();
            Console.ReadLine();

            NHibernateHelper.CloseSession();
        }

        //        static void Main(string[] args)
        //        {
        //            // Configure log4net for logging
        //            log4net.Config.XmlConfigurator.Configure();
        //
        //            // 1. Entity that we want to persist in database.
        //            Employee employee = new Employee();
        //
        //            // 2. Configure NHibernate for your database (Microsoft SQL Server 2012)
        //            string connectionStringKey = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringKey"];
        //            string connectionString = String.IsNullOrWhiteSpace(connectionStringKey) ? "" : System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
        //
        //            var cfg = new Configuration();
        //            cfg.DataBaseIntegration(c =>
        //            {
        //                c.ConnectionString = connectionString; //Data Source=.\MSSQLSERVER2K14;Initial Catalog=NHibernateTutorial;Persist Security Info=True;user id=sa;password=sa123456#;
        //
        //                c.Dialect<MsSql2012Dialect>();
        //
        //                c.LogSqlInConsole = true;
        //                c.LogFormattedSql = true;
        //                c.AutoCommentSql = true;
        //            });
        //
        //            // 3. Create a mapping for the Employee entity (The mapping tells NHibernate how to map entity properties to database table columns.)
        //            var mapper = new ModelMapper();
        //
        //            // This expects an underlying database table called Employee, with columns 'Id', 'Name' and 'State', to exist.
        //            // There are a number of other ways to map an entity (e.g. XML, Fluent NHibernate), but here we use 'by code' mapping which is a convention-based approach.
        //            mapper.Class<Employee>(rc =>
        //            {
        //                rc.Lazy(true);
        //                rc.Cache(x => x.Usage(CacheUsage.ReadWrite));
        //
        //                rc.Id(x => x.Id, map => map.Generator(Generators.Identity));
        //
        //                rc.Property(x => x.Name, map => { map.NotNullable(true); map.Length(50); });
        //                rc.Property(x => x.State, map => { map.NotNullable(true); map.Length(50); });
        //            });
        //
        //            // Need to specify the assembly where one of the class resides. NHibernate will auto scan that assembly to find the rest
        //            mapper.AddMappings(Assembly.GetAssembly(typeof(Employee)).GetExportedTypes());
        //
        //            // 4. Add the mapping to the NHibernate configuration
        //            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        //
        //            // Export schema - i.e. create the Employee table
        //            new SchemaExport(cfg).Create(false, true);
        //
        //            // 5. To persist a new Employee entity
        //            using (var sessionFactory = cfg.BuildSessionFactory())
        //            {
        //                using (var session = sessionFactory.OpenSession())
        //                {
        //                    using (var tx = session.BeginTransaction())
        //                    {
        //                        List<Employee> employees = session.Query<Employee>().ToList();
        //                        int lastId = employees.Any() ? employees.Max(x => x.Id) : 0;
        //                        employee.Name = "Bob" + (lastId + 1);
        //                        employee.State = "Marley" + (lastId + 1);
        //
        //                        session.Save(employee);
        //                        tx.Commit();
        //                    }
        //                }
        //            }
        //
        //            Console.ReadLine();
        //
        //            // Drop schema - i.e. drop the Employee table
        //            new SchemaExport(cfg).Drop(false, true);
        //        }
    }
}
