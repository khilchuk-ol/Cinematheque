using Cinematheque.Utils;
using System;
using System.Data;
using System.Data.Common;

namespace Cinematheque.Data
{
    public class TableManager
    {
        private readonly DbDataAdapter adapter;
        private readonly DataTable table;
        private readonly DataTable temp;
        private readonly DbCommand command;

        public DataTable Table { get { return table; } }

        internal TableManager(string tableName)
        {
            try
            {
                adapter = DbManager.ProviderFactory.CreateDataAdapter();

                command = DbManager.ProviderFactory.CreateCommand();
                DbCommandBuilder cb = DbManager.ProviderFactory.CreateCommandBuilder();
                command.Connection = DbManager.Connection;
                cb.ConflictOption = ConflictOption.OverwriteChanges;
                cb.DataAdapter = adapter;

                table = new DataTable();
                temp = new DataTable();
                table.TableName = temp.TableName = tableName;

                command.CommandText = "SELECT * FROM DBTest_" + tableName;

                adapter.SelectCommand = command;
                adapter.InsertCommand = cb.GetInsertCommand();
                adapter.DeleteCommand = cb.GetDeleteCommand();
                adapter.UpdateCommand = cb.GetUpdateCommand();

                Recharge("1 = 2");
            }
            catch(Exception e)
            {
                LogWriter.Log($"Failed to configure manager for table '{tableName}'. \nException message: {e.Message}");
            }
        }

        internal int Recharge(string query)
        {
            command.CommandText = $"DBTest_{Table.TableName}.* FROM DBTest_{Table.TableName}" + 
                ((query == "") ? "" : " WHERE " + query);

            try
            {
                return adapter.Fill(table);
            }
            catch(Exception e)
            {
                LogWriter.Log($"Failed to cache data from table '{Table.TableName}'. \nException message: {e.Message}");
            }

            return 0;
        }

        internal DataRowCollection GetIDs(string query)
        {
            command.CommandText = $"SELECT ID FROM Voll_{Table.TableName}" +
                ((query == "") ? "" : " WHERE " + query);

            try
            {
                adapter.Fill(temp);
                return temp.Rows;
            }
            catch(Exception e)
            {
                LogWriter.Log($"Failed to get IDs from table '{Table.TableName}'. \nException message: {e.Message}");
            }

            return null;
        }

        internal int Update(DataRow row)
        {
            return adapter.Update(new[] { row }) ;
        }

        internal int Update(DataRow[] rows)
        {
            return adapter.Update(rows);
        }
    }
}
