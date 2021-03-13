﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper
{
    public static class TableOperations
    {
        public static string BuildInsertStatement(this DatabaseEntity dbEntity)
        {
            var sqlBuilder = new StringBuilder($"INSERT INTO {dbEntity.GetTableName()} (");

            var valuesBuilder = new StringBuilder();

            foreach (var property in dbEntity.GetType().GetProperties())
            {
                var propertyName = property.Name;
                sqlBuilder.Append($"{propertyName},");
                valuesBuilder.Append($"@{propertyName},");
            }

            sqlBuilder.Length--;
            valuesBuilder.Length--;

            sqlBuilder.Append($") VALUES ({valuesBuilder});");

            return sqlBuilder.ToString();
        }

        public static string BuildDeleteStatement(this DatabaseEntity dbEntity) =>
            $"DELETE FROM {dbEntity.GetTableName()} WHERE id = @id";
    }
}