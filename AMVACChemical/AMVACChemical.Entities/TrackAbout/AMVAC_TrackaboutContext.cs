using AMVACChemical.Entities.TrackAbout.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace AMVACChemical.Entities.TrackAbout
{
    public class AMVAC_TrackaboutContext:DbContext
    {
        // Declared global variables
        private IConfigurationRoot _config;
        private AMVAC_TrackaboutContext _trackaboutContext;

        // create constructor 
        public AMVAC_TrackaboutContext(IConfigurationRoot config, DbContextOptions<AMVAC_TrackaboutContext> options) : base(options)
        {
            _config = config;
            _trackaboutContext = this;
        }

        public DbSet<Login> login { get; set; }

        #region
        // calling generic methods of entity framework :
        /// <summary>
        /// Method used to call list of ExecuteStoredProcedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExecuteStoredProcedure<T>(string storedProcedure,
           List<SqlParameter> parameters) where T : new()
        {
            using (var cmd =
               this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                // set some parameters of the stored procedure
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Method used to call list of ExecuteStoredProcedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExecuteStoredProcedure<T>(string storedProcedure,
           List<SqlParameter>[][] parameters) where T : new()
        {
            using (var cmd =
               this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                // set some parameters of the stored procedure
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Methos used to call ExecuteStoredProcedure with different parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="isTransactionEnable"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExecuteStoredProcedure<T>(string storedProcedure, bool isTransactionEnable,
           List<SqlParameter> parameters) where T : new()
        {
            using (var cmd =
               this.Database.GetDbConnection().CreateCommand())
            {
                SqlTransaction transaction = (SqlTransaction)this.Database.CurrentTransaction.GetDbTransaction();
                cmd.CommandText = storedProcedure;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = transaction;
                // set some parameters of the stored procedure
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Methos used to call ExecuteStoredProcedure with different parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="oTran"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> ExecuteStoredProcedure<T>(string storedProcedure, SqlTransaction oTran, List<SqlParameter> parameters) where T : new()
        {
            using (var cmd =
               this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandTimeout = 500;
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = oTran;
                // set some parameters of the stored procedure
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Methos used to call ExecuteStoredProcedure with different parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="oTran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<T> ExecuteStoredProcedure<T>(string storedProcedure, SqlTransaction oTran, SqlParameter parameter) where T : new()
        {
            using (var cmd =
               this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = storedProcedure;
                cmd.Transaction = oTran;
                cmd.CommandType = CommandType.StoredProcedure;
                parameter.SqlDbType = SqlDbType.Structured;
                //parameter.TypeName = typeName;
                cmd.Parameters.Add(parameter);
                //}

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Methos used to call ExecuteStoredProcedure with different parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public List<T> ExecuteStoredProcedure<T>(string storedProcedure) where T : new()
        {
            using (var cmd =
               this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }
            }
        }

        /// <summary>
        /// Methos used to call DataReaderMapToList with different parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static List<T> DataReaderMapToList<T>(DbDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!Equals(dr[prop.Name], DBNull.Value))
                    {

                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        #endregion
    }
}
