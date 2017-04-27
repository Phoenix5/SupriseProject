//CREATE TABLE [dbo].[Employee](
//	[Id] [int] IDENTITY(1,1) NOT NULL,
//	[Name] [varchar](50) NOT NULL,
//	[State] [varchar](50) NULL,
// CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
//(
//	[Id] ASC
//)
//) 

namespace NHibernateSimpleDemoMVC
{
    public class Employee
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }

        public Employee()
        {
        }
    }
}
