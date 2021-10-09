using Dapper;
using Dapper.Mapper;
using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BD : IBD
    {

        // Declaracion de variable que guarda la variable
        public string Conn { get; set; }

        //Metodo de la base de datos para gaurdar na conexion 
        public BD(string conn)
        {
            Conn = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings[conn])
            {
                MultipleActiveResultSets = true,
                AsynchronousProcessing = true
            }.ConnectionString;
        }

        //Metodo de ejecucion que abre y cierra la base de datos
        public IDbConnection Exec()
        {
            IDbConnection sql = new SqlConnection();
            try
            {
                sql = new SqlConnection(Conn);
                return sql;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        //Metodos para obtener los datos para hacer un select en la Base
        public List<dynamic> Query(string Sp, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();
            try
            {
                var result = new List<dynamic>();
                result = sql.Query(sql: Sp, param: new DynamicParameters(Param),
                commandType: CommandType.StoredProcedure, commandTimeout: timeout).AsList();

                if (sql.State == ConnectionState.Open) sql.Close();

                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        //Metodo retornar lista estructurada sin relaciones

        public List<T> Query<T>(string Sp, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();
            try
            {
                var result = new List<T>();
                result = sql.Query<T>(sql: Sp, param: new DynamicParameters(Param),
                commandType: CommandType.StoredProcedure, commandTimeout: timeout).AsList();

                if (sql.State == ConnectionState.Open) sql.Close();

                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        // Metodo con dos relaciones
        public List<T> Query<T, Q>(string Sp, string split, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();
            try
            {
                var result = new List<T>();
                result = sql.Query<T, Q>(sql: Sp, param: new DynamicParameters(Param), splitOn: split,
                commandType: CommandType.StoredProcedure, commandTimeout: timeout).AsList();

                if (sql.State == ConnectionState.Open) sql.Close();

                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }
        //Metodo relacion para 3

        public List<T> Query<T, Q, S>(string Sp, string split, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();
            try
            {
                var result = new List<T>();
                result = sql.Query<T, Q, S>(sql: Sp, param: new DynamicParameters(Param), splitOn: split,
                commandType: CommandType.StoredProcedure, commandTimeout: timeout).AsList();

                if (sql.State == ConnectionState.Open) sql.Close();

                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        //Metodo con 4 relaciones

        public List<T> Query<T, Q, S, R>(string Sp, string split, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();
            try
            {
                var result = new List<T>();
                result = sql.Query<T, Q, S, R>(sql: Sp, param: new DynamicParameters(Param), splitOn: split,
                commandType: CommandType.StoredProcedure, commandTimeout: timeout).AsList();

                if (sql.State == ConnectionState.Open) sql.Close();

                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        // Metodo con 5 relaciones

        public List<T> Query<T, Q, S, R, E>(string Sp, string split, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();
            try
            {
                var result = new List<T>();
                result = sql.Query<T, Q, S, R, E>(sql: Sp, param: new DynamicParameters(Param), splitOn: split,
                commandType: CommandType.StoredProcedure, commandTimeout: timeout).AsList();

                if (sql.State == ConnectionState.Open) sql.Close();

                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        // Metodo dinamico

        public dynamic QueryFirst(string SP, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();

            try
            {
                var result = sql.QueryFirstOrDefault(sql: SP, param: new DynamicParameters(Param),
                commandType: CommandType.StoredProcedure, commandTimeout: timeout);

                if (sql.State == ConnectionState.Open) sql.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        //
        public T QueryFirst<T>(string SP, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();

            try
            {
                var result = sql.QueryFirstOrDefault<T>(sql: SP, param: new DynamicParameters(Param),
                commandType: CommandType.StoredProcedure, commandTimeout: timeout);

                if (sql.State == ConnectionState.Open) sql.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }

        //Metodo solo retorna una linea 
        public DBEntity QueryExecute(string SP, object Param = null, int? timeout = null)
        {
            IDbConnection sql = Exec();

            try
            {
                var result = (IDictionary<string, object>)sql.QueryFirstOrDefault(sql: SP, param: new DynamicParameters(Param),
                commandType: CommandType.StoredProcedure, commandTimeout: timeout);

                if (sql.State == ConnectionState.Open) sql.Close();

                int code = int.Parse(result.ElementAt(0).Value.ToString());
                string msg = result.ElementAt(1).Value.ToString();

                return new DBEntity { CodeError = code, MsgError = msg };
            }
            catch (Exception ex)
            {
                if (sql.State == ConnectionState.Open) sql.Close();
                throw (ex);
            }
        }
    }
}
