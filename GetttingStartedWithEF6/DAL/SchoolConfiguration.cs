using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;

namespace GetttingStartedWithEF6.DAL
{
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());            
        }
    }
}