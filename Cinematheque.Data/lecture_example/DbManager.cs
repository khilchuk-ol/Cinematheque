using Cinematheque.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Cinematheque.Data
{
    public static class DbManager
    {
        private static readonly Dictionary<string, TableManager> managers = new Dictionary<string, TableManager>();
        private static readonly DbProviderFactory providerFactory = DbProviderFactories.GetFactory("System.Data.OleDB");
        private static readonly DbConnection connection = providerFactory.CreateConnection();

        internal static DbProviderFactory ProviderFactory
        {
            get { return providerFactory; }
        }

        internal static DbConnection Connection
        {
            get
            {
                while (true)
                {
                    try
                    {
                        switch (connection.State)
                        {
                            case System.Data.ConnectionState.Broken:
                                connection.Close();
                                break;

                            case System.Data.ConnectionState.Closed:
                                connection.Open();
                                break;
                        }

                        return connection;
                    }
                    catch (Exception e)
                    {
                        LogWriter.Log($"Cannot connect to Data Base. \nException message: {e.Message}");
                    }
                }
            }
        }

        static DbManager()
        {
            //TODO: .....
            connection.ConnectionString = "";
        }

        public static TableManager GetTableManager(string tableName)
        {
            TableManager tm = null;

            try
            {
                tm = managers[tableName];
            }
            catch
            {
                try
                {
                    tm = new TableManager(tableName);
                    managers.Add(tableName, tm);
                }
                catch
                {
                    LogWriter.Log($"Failed to find or create table manager for table '{tableName}'");
                }
            }

            return tm;
        }
    }
}
