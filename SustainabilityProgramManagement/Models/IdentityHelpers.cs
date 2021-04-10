using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SustainabilityProgramManagement.Models
{
    public static class IdentityHelpers
    {
        public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: true);
        public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: false);

        private static Task SetIdentityInsert<T>(DbContext context, bool enable)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";
            var schema = entityType.GetSchema();
            if (schema == null)
                schema = entityType.GetDefaultSchema();
            if (schema == null)
                schema = "dbo";
            var sql = $"SET IDENTITY_INSERT {schema}.{entityType.GetTableName()} {value}";
            return context.Database.ExecuteSqlRawAsync(sql);
        }

        public static void SaveChangesWithIdentityInsert<T>(this DbContext context)
        {
            using var transaction = context.Database.BeginTransaction();
            context.EnableIdentityInsert<T>();
            context.SaveChanges();
            context.DisableIdentityInsert<T>();
            transaction.Commit();
        }

    }
}
