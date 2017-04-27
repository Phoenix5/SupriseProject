using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateSimpleDemoMVC
{
    public sealed class NHibernateHelper
    {
        private static readonly ISessionFactory SessionFactory;
        private static readonly Configuration cfg;

        static NHibernateHelper()
        {
            string connectionStringKey = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringKey"];

            string connectionString = String.IsNullOrWhiteSpace(connectionStringKey)
                ? ""
                : System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            mapper.AddMappings(GetMappingsFromAssenbly(Assembly.GetExecutingAssembly()));

            cfg = new Configuration();
            cfg.DataBaseIntegration(c =>
            {
                c.ConnectionString = connectionString;

                c.Driver<SqlClientDriver>();
                c.Dialect<MsSql2012Dialect>();

                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;

#if DEBUG
                c.LogSqlInConsole = true;
                c.LogFormattedSql = true;
#else
                c.LogSqlInConsole = false;
                c.LogFormattedSql = false;
#endif
            });

            cfg.Mappings(x => x.DefaultSchema = "dbo");
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            // Drop existing schema - all tables
            new SchemaExport(cfg).Drop(false, true);

            // Export schema - i.e. create the Employee table
            new SchemaExport(cfg).Create(false, true);

            //The following line of code builds a Session Factory object from our configuration object that we'll use to create sessions
            SessionFactory = cfg.BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {
             // when we want to create a session, we just ask the session factory to open a session for us as follows
            ISession currentSession =  SessionFactory.OpenSession();

            return currentSession;
        }

        public static void CloseSession()
        {
            // Drop schema - all tables
            new SchemaExport(cfg).Drop(false, true);

            GetCurrentSession().Close();
        }

        public static void CloseSessionFactory()
        {
            if (SessionFactory != null)
            {
                SessionFactory.Close();
            }
        }

        private static IEnumerable<Type> GetMappingsFromAssenbly(Assembly assembly)
        {
            // Returns all classes derived from ClassMapping<> & which are not abstract.
            return assembly.GetTypes().Where(t => !t.IsAbstract
                                                  && t.GetBaseTypes().Any(bt => bt.IsGenericType && bt.GetGenericTypeDefinition() == typeof(ClassMapping<>))
                                            );
        }
    }
}
