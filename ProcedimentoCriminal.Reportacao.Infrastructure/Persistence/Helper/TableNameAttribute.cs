using System;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; }

        public TableNameAttribute(string name)
        {
            TableName = name;
        }
    }
}